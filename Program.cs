using System;
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

        if (choice == "1")
        {
            Console.Write("Enter path for first CSV file: ");
            string pathA = Console.ReadLine()!;
            Console.Write("Enter path for second CSV file: ");
            string pathB = Console.ReadLine()!;

            fileAContent = FileLoader.LoadFromFile(pathA);
            fileBContent = FileLoader.LoadFromFile(pathB);
        }
        else if (choice == "2")
        {
            Console.Write("Enter URL for first CSV file: ");
            string urlA = Console.ReadLine()!;
            Console.Write("Enter URL for second CSV file: ");
            string urlB = Console.ReadLine()!;

            fileAContent = await FileLoader.LoadFromUrlAsync(urlA!);
            fileBContent = await FileLoader.LoadFromUrlAsync(urlB!);
        }
        else
        {
            Console.WriteLine("Invalid choice. Exiting...");
            return;
        }

        if (string.IsNullOrEmpty(fileAContent) || string.IsNullOrEmpty(fileBContent))
        {
            Console.WriteLine("One or both files failed to load. Please check the input and try again.");
            return;
        }

        var parsedA = CSVParser.Parse(fileAContent);
        var parsedB = CSVParser.Parse(fileBContent);

        var comparisonResult = CSVComparer.Compare(parsedA, parsedB);

        ReportGenerator.GenerateReport(comparisonResult);
    }
}