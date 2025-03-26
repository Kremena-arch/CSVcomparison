
using System;
using System.Collections.Generic;
using ClosedXML.Excel;

class ExcelParser
{
    public static Dictionary<string, List<List<string>>> Parse(string filePath)
    {
        var sheetsData = new Dictionary<string, List<List<string>>>();

        using (var workbook = new XLWorkbook(filePath))
        {
            foreach (var sheet in workbook.Worksheets)
            {
                var rows = new List<List<string>>();

                foreach (var row in sheet.RowsUsed())
                {
                    var rowData = new List<string>();
                    foreach (var cell in row.CellsUsed())
                    {
                        rowData.Add(cell.Value.ToString()); // Convert all values to string
                    }
                    rows.Add(rowData);
                }

                sheetsData[sheet.Name] = rows;
            }
        }

        return sheetsData;
    }
}
