
using System.Collections.Generic;

class ComparisonResult
{
    public List<string> Errors { get; set; } = new List<string>();
    public bool AreIdentical { get; set; }
    public int RowCount { get; set; }
    public string SheetName { get; set; } // NEW: Added sheet name to track discrepancies at sheet level
}
