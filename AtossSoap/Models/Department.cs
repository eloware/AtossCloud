namespace AtossSoap.Models; 

public class Department{
    public string? Officer { get; set; }
    public string? Name { get; set; }
    public int Client { get; set; }
    public int Allocationtreshold { get; set; }
    public int Id { get; set; }
    public int Maindepartment { get; set; }
    public double Minallocation { get; set; }
}