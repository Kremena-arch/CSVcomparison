using System;

class ReportGenerator
{
    public static void GenerateReport(ComparisonResult result, List<List<string>> parsedA, List<List<string>> parsedB)
    {
        if (result.Errors.Count == 0)
        {
            Console.WriteLine($"Passed! Files are IDENTICAL!");
            Console.WriteLine($"Both files contain {result.RowCount} rows. The content of the corresponding rows is identical.");
            for (int i = 0; i < result.RowCount; i++)
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
            foreach (var line in result.Errors)
            {
                Console.WriteLine(line);
            }

            if (parsedA.Count == parsedB.Count)
            {
                Console.WriteLine($"Number of rows is equal. Both files contain {result.RowCount} rows.");
            }
        }
    }
}
