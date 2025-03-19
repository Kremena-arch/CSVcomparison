using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("CSV Comparison Tool");
        Console.WriteLine("Choose input method: \n1 - Load from local files\n2 - Load from URLs");
        string choice = Console.ReadLine()!;

        string fileAContent = "";
        string fileBContent = "";
        string fileAName = "";
        string fileBName = "";

        if (choice == "1")
        {
            Console.Write("Enter path for first CSV file: ");
            string pathA = Console.ReadLine()!;
            Console.Write("Enter path for second CSV file: ");
            string pathB = Console.ReadLine()!;

            fileAContent = FileLoader.LoadFromFile(pathA);
            fileBContent = FileLoader.LoadFromFile(pathB);
            fileAName = Path.GetFileName(pathA);
            fileBName = Path.GetFileName(pathB);
        }
        else if (choice == "2")
        {
            Console.Write("Enter URL for first CSV file: ");
            string urlA = Console.ReadLine()!;
            Console.Write("Enter URL for second CSV file: ");
            string urlB = Console.ReadLine()!;

            fileAContent = await FileLoader.LoadFromUrlAsync(urlA!);
            fileBContent = await FileLoader.LoadFromUrlAsync(urlB!);
            fileAName = GetFileNameFromUrl(urlA);
            fileBName = GetFileNameFromUrl(urlB);
        }
        else
        {
            Console.WriteLine("Invalid choice. Exiting...");
            return;
        }

        if (string.IsNullOrEmpty(fileAContent))
        {
            Logger.LogError($"File {fileAName} is empty. Please check the input and try again.");
            return;
        }

        if (string.IsNullOrEmpty(fileBContent))
        {
            Logger.LogError($"File {fileBName} is empty. Please check the input and try again.");
            return;
        }

        var parsedA = CSVParser.Parse(fileAContent);
        var parsedB = CSVParser.Parse(fileBContent);

        var comparisonResult = CSVComparer.Compare(parsedA, parsedB);

        if (comparisonResult.AreIdentical)
        {
            Console.WriteLine($"Passed! Files are IDENTICAL!");
            Console.WriteLine($"Both files contain {comparisonResult.RowCount} rows. The content of the corresponding rows is identical.");
            for (int i = 0; i < comparisonResult.RowCount; i++)
            {
                var row = parsedA[i];
                int columnCount = row.Count;
                int rowLength = row.Sum(col => col.Length);
                Console.WriteLine($"Row {i + 1}: {columnCount} Columns, {rowLength} Total Row Length");
            }
        }
        else
        {
            Console.WriteLine("Failed! Files are DIFFERENT! Issues found:");
            foreach (var line in comparisonResult.Errors)
            {
                Console.WriteLine(line);
            }

            if (parsedA.Count == parsedB.Count)
            {
                Console.WriteLine($"Number of rows is equal. Both files contain {comparisonResult.RowCount} rows.");
            }
        }

        ReportGenerator.GenerateReport(comparisonResult);
    }

    private static string GetFileNameFromUrl(string url)
    {
        return new Uri(url).Segments.Last();
    }
}
