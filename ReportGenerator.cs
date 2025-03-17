using System;

class ReportGenerator
{
    public static void GenerateReport(ComparisonResult result)
    {
        if (result.Errors.Count == 0)
        {
            Console.WriteLine("Passed! Files are IDENTICAL!");//✅
        }
        else
        {
            Console.WriteLine("Failed! Files are DIFFERENT! Issues found:");//❌
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"  - {error}");
            }
        }
    }
}
