namespace AtossSoap.Models; 

public class Booking{
    public string? Maincost { get; set; }
    public string? Freeindex { get; set; }
    public string? Cost { get; set; }
    public string? Project { get; set; }
    public string? Remark { get; set; }
    public string? Employee { get; set; }
    public string? Userremark { get; set; }
    public string? Workplace { get; set; }
    public int Bookingtype { get; set; }
    public int Process { get; set; }
    public int Offset { get; set; }
    public int Usehourlywage { get; set; }
    public int Negativebooking { get; set; }
    public int Timecreditbooking { get; set; }
    public int Transferflag { get; set; }
    public int Costtype { get; set; }
    public int Mandantorybreak { get; set; }
    public int Place { get; set; }
    public int Daypart { get; set; }
    public int Completedmessage { get; set; }
    public int Account { get; set; }
    public int Status { get; set; }
    public double Externhourlywagerate { get; set; }
    public double Vatrate { get; set; }
    public double Yield { get; set; }
    public double Wasteamount { get; set; }
    public double Hourlywagerate { get; set; }
    public TimeSpan Duration { get; set; }
    public DateTime Date { get; set; }
    public DateTime Realin { get; set; }
    public DateTime Realout { get; set; }
    public DateTime Correlationdate { get; set; }
    public DateTime Time { get; set; }
}