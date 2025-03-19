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
}
