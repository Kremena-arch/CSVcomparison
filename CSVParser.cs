//using System;
//using System.Collections.Generic;
//using System.Linq;

//class CSVParser
//{
//    public static List<List<string>> Parse(string csvContent)
//    {
//        var rows = new List<List<string>>();
//        var lines = csvContent.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

//        foreach (var line in lines)
//        {
//            var columns = line.Split(',').ToList();
//            rows.Add(columns);
//        }

//        return rows;
//    }
//}


using System;
using System.Collections.Generic;
using System.Linq;

class CSVParser
{
    public static List<List<string>> Parse(string csvContent)
    {
        var rows = new List<List<string>>();
        var lines = csvContent.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            var columns = line.Split(new[] { ',', ';' }).ToList();
            rows.Add(columns);
        }

        return rows;
    }
}