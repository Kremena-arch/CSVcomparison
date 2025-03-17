using System;
using System.Collections.Generic;

class CSVComparer
{
    public static ComparisonResult Compare(List<List<string>> csvA, List<List<string>> csvB)
    {
        var result = new ComparisonResult();

        if (csvA.Count != csvB.Count)
        {
            result.Errors.Add($"Row count mismatch: {csvA.Count} <---> {csvB.Count}");
        }

        int rowCount = Math.Min(csvA.Count, csvB.Count);

        for (int i = 0; i < rowCount; i++)
        {
            if (csvA[i].Count != csvB[i].Count)
            {
                result.Errors.Add($"Row {i + 1}: Column count mismatch ({csvA[i].Count} <---> {csvB[i].Count})");
                continue;
            }

            for (int j = 0; j < csvA[i].Count; j++)
            {
                if (csvA[i][j] != csvB[i][j])
                {
                    result.Errors.Add($"Row {i + 1}, Column {j + 1}: '{csvA[i][j]}' <---> '{csvB[i][j]}'"); //≠
                }
            }
        }

        return result;
    }
}

