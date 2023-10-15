namespace AtossSoap.Models;

public class Account {
    public string? Name { get; set; }
    public int DirectPosting { get; set; }
    public int Isabsence { get; set; }

    [AtossName("account")]
    public int AccountId { get; set; }

    public double Valuationfactor1 { get; set; }
}
