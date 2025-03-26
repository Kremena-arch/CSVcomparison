
using System;
using System.Collections.Generic;

class ReportGenerator
{
    public static void GenerateReport(ComparisonResult result, List<List<string>> parsedA, List<List<string>> parsedB)
    {
        Console.WriteLine($"=== Report for Sheet: {result.SheetName} ===");

        if (result.Errors.Count == 0)
        {
            Console.WriteLine($"Passed! Sheet '{result.SheetName}' is IDENTICAL!");
            Console.WriteLine($"Both sheets contain {result.RowCount} rows. The content of the corresponding rows is identical.");
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
            Console.WriteLine("Failed! Differences found:");
            foreach (var line in result.Errors)
            {
                Console.WriteLine($"[ERROR] Sheet '{result.SheetName}' → {line}");
            }

            if (parsedA?.Count == parsedB?.Count)
            {
                Console.WriteLine($"Number of rows is equal. Both sheets contain {result.RowCount} rows.");
            }
        }
    }
}
