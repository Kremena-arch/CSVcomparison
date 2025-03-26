
using System;
using System.Collections.Generic;

class CSVComparer
{
    public static ComparisonResult Compare(List<List<string>> csvA, List<List<string>> csvB)
    {
        var result = new ComparisonResult();
        result.AreIdentical = true;
        result.RowCount = Math.Max(csvA.Count, csvB.Count);

        for (int i = 0; i < result.RowCount; i++)
        {
            if (i >= csvA.Count)
            {
                result.Errors.Add($"Row {i + 1}: Missing in first file.");
                result.AreIdentical = false;
                continue;
            }

            if (i >= csvB.Count)
            {
                result.Errors.Add($"Row {i + 1}: Missing in second file.");
                result.AreIdentical = false;
                continue;
            }

            var rowA = csvA[i];
            var rowB = csvB[i];

            if (rowA.Count != rowB.Count)
            {
                result.Errors.Add($"Row {i + 1}: Column count mismatch ({rowA.Count} <---> {rowB.Count})");
                result.AreIdentical = false;
            }

            int maxColumns = Math.Max(rowA.Count, rowB.Count);

            for (int j = 0; j < maxColumns; j++)
            {
                string valueA = j < rowA.Count ? rowA[j] : "MISSING";
                string valueB = j < rowB.Count ? rowB[j] : "MISSING";

                if (valueA != valueB)
                {
                    result.Errors.Add($" - Row {i + 1}, Column {j + 1}: '{valueA}' <---> '{valueB}'");
                    result.AreIdentical = false;
                }
            }
        }

        return result;
    }

    public static List<ComparisonResult> CompareExcel(Dictionary<string, List<List<string>>> excelA, Dictionary<string, List<List<string>>> excelB)
    {
        var results = new List<ComparisonResult>();

        foreach (var sheet in excelA.Keys)
        {
            var result = new ComparisonResult { SheetName = sheet };
            if (!excelB.ContainsKey(sheet))
            {
                result.Errors.Add($"Sheet '{sheet}' is missing in second file.");
                result.AreIdentical = false;
                results.Add(result);
                continue;
            }

            var csvA = excelA[sheet];
            var csvB = excelB[sheet];
            var comparisonResult = Compare(csvA, csvB);
            comparisonResult.SheetName = sheet;
            results.Add(comparisonResult);
        }

        foreach (var sheet in excelB.Keys)
        {
            if (!excelA.ContainsKey(sheet))
            {
                var result = new ComparisonResult
                {
                    SheetName = sheet,
                    AreIdentical = false
                };
                result.Errors.Add($"Sheet '{sheet}' is missing in first file.");
                results.Add(result);
            }
        }

        return results;
    }
}
