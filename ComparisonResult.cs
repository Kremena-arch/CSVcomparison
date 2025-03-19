using System.Collections.Generic;

class ComparisonResult
{
    public List<string> Errors { get; set; } = new List<string>();
    public bool AreIdentical { get; set; }
    public int RowCount { get; set; }
}


