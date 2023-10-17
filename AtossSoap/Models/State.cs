namespace AtossSoap.Models;

public class State {
    public string? FreeIndex { get; set; }
    public string? Project { get; set; }
    public string? Employee { get; set; }
    public string? CostUnit { get; set; }
    public int Process { get; set; }
    public int IsPlanned { get; set; }
    public int Origin { get; set; }
    public int ClockIn { get; set; }
    public int Account { get; set; }
    public int Status { get; set; }
    public DateTime LastTimestamp { get; set; }
}
