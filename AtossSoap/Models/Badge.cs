namespace AtossSoap.Models;

public class Badge {
    public string? Pin { get; set; }
    public string? Project { get; set; }
    public string? Id { get; set; }

    [AtossName("employee")]
    public string? EmployeeId { get; set; }

    public int Number { get; set; }
    public int Badgetype { get; set; }
    public int Chip { get; set; }
    public int Validationindex { get; set; }
    public int Options { get; set; }
    public int Update { get; set; }
    public int Locked { get; set; }
    public int Offlinetoggle { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
}
