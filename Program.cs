
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Comparison Tool");
        Console.WriteLine("Choose input method: \n1 - Load from local files\n2 - Load from URLs");
        string choice = Console.ReadLine();

        string fileAContent = "";
        string fileBContent = "";
        string fileAName = "";
        string fileBName = "";
        bool isExcel = false;

        if (choice == "1")
        {
            Console.Write("Enter path for first file: ");
            string pathA = Console.ReadLine();
            Console.Write("Enter path for second file: ");
            string pathB = Console.ReadLine();

            fileAName = Path.GetFileName(pathA);
            fileBName = Path.GetFileName(pathB);

            if (Path.GetExtension(fileAName).Equals(".xlsx") || Path.GetExtension(fileAName).Equals(".xls"))
            {
                isExcel = true;
            }

            if (Path.GetExtension(fileBName).Equals(".xlsx") || Path.GetExtension(fileBName).Equals(".xls"))
            {
                if (!isExcel)
                {
                    Logger.LogError("The two input files have different formats and cannot be compared!");
                    return;
                }
                fileAContent = pathA;
                fileBContent = pathB;
            }
            else if (Path.GetExtension(fileBName).Equals(".csv"))
            {
                if (isExcel)
                {
                    Logger.LogError("The two input files have different formats and cannot be compared!");
                    return;
                }
                fileAContent = FileLoader.LoadFromFile(pathA);
                fileBContent = FileLoader.LoadFromFile(pathB);
            }
            else
            {
                Logger.LogError("Unsupported file format.");
                return;
            }
        }
        else if (choice == "2")
        {
            Console.Write("Enter URL for first file: ");
            string urlA = Console.ReadLine();
            Console.Write("Enter URL for second file: ");
            string urlB = Console.ReadLine();

            fileAName = GetFileNameFromUrl(urlA);
            fileBName = GetFileNameFromUrl(urlB);

            fileAContent = await FileLoader.LoadFromUrlAsync(urlA);
            fileBContent = await FileLoader.LoadFromUrlAsync(urlB);
        }
        else
        {
            Console.WriteLine("Invalid choice. Exiting...");
            return;
        }

        if (isExcel)
        {
            var parsedA = ExcelParser.Parse(fileAContent);
            var parsedB = ExcelParser.Parse(fileBContent);

            var comparisonResults = CSVComparer.CompareExcel(parsedA, parsedB);

            foreach (var result in comparisonResults)
            {
                ReportGenerator.GenerateReport(result, parsedA[result.SheetName], parsedB.ContainsKey(result.SheetName) ? parsedB[result.SheetName] : null);
            }
        }
        else
        {
            var parsedA = CSVParser.Parse(fileAContent);
            var parsedB = CSVParser.Parse(fileBContent);

            var comparisonResult = CSVComparer.Compare(parsedA, parsedB);
            ReportGenerator.GenerateReport(comparisonResult, parsedA, parsedB);
        }
    }

    private static string GetFileNameFromUrl(string url)
    {
        return new Uri(url).Segments.Last();
    }
}
