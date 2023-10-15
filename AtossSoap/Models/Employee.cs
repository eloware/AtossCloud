namespace AtossSoap.Models;

public class Employee {
    public string Systemusername { get; set; }
    public string Firstname { get; set; }
    public string City { get; set; }
    public string Latitude { get; set; }

    [AtossName("externalid")]
    public string ExternalId { get; set; }

    public string Language { get; set; }

    [AtossName("employee")]
    public string EmployeeId { get; set; }

    public string Latitude2 { get; set; }
    public string Street { get; set; }
    public string Mainproject { get; set; }
    public string Fax { get; set; }
    public string Mainworkplace { get; set; }
    public string Email { get; set; }
    public string Methodofpayment { get; set; }
    public string Longitude { get; set; }
    public string Licence { get; set; }
    public string Postcode { get; set; }
    public string Mobile { get; set; }
    public string Telephone { get; set; }
    public string Maincostcenter { get; set; }
    public string Lastname { get; set; }
    public string Alttimerecordingclerk { get; set; }
    public string Selection1 { get; set; }
    public string Selection3 { get; set; }
    public string Longitude2 { get; set; }
    public string Selection2 { get; set; }
    public string Timerecordingclerk { get; set; }
    public string Selection5 { get; set; }
    public string Selection4 { get; set; }
    public string Selection7 { get; set; }
    public string Selection6 { get; set; }
    public int Commercial { get; set; }
    public int Holidaycalendar { get; set; }
    public int Setbalancetozerotype { get; set; }
    public int Overrunbalanceaccount { get; set; }
    public int Workingtimemodelstartday { get; set; }
    public int Mainqualification { get; set; }
    public int Kindofhourlywagerate { get; set; }
    public int Passwordneedschange { get; set; }
    public int Redirectaccountdest { get; set; }
    public int Workdaysweek { get; set; }
    public int Client { get; set; }
    public int Mandantorybreakonmasterproject { get; set; }
    public int Company { get; set; }
    public int Setbalancetozeroflag { get; set; }
    public int Overrunaccount { get; set; }
    public int Watchcarryovermonth { get; set; }
    public int Locked { get; set; }
    public int Costcenter { get; set; }
    public int Redirectaccountsource { get; set; }
    public int Ignorebalanceoftimecredit { get; set; }
    public int Freebalanceofholiday { get; set; }
    public int Maximumsumofbalanceaccount { get; set; }
    public int Setbalancetozeroaccount { get; set; }
    public int Empgroup { get; set; }
    public int Stopaccount { get; set; }
    public int Setbalancetozeroday { get; set; }
    public int Accesspriority { get; set; }
    public int Workingtimemodel { get; set; }
    public int Areatype { get; set; }
    public double Hourlywagerateextern { get; set; }
    public double Vacationentitlementdays { get; set; }
    public double Radius { get; set; }
    public double Hourlywagerate { get; set; }
    public TimeSpan Maximumbalancemonth { get; set; }
    public TimeSpan Maximumsumofbalance { get; set; }
    public TimeSpan Debit09 { get; set; }
    public TimeSpan Debit08 { get; set; }
    public TimeSpan Debit07 { get; set; }
    public TimeSpan Debit06 { get; set; }
    public TimeSpan Maximumday { get; set; }
    public TimeSpan Debit05 { get; set; }
    public TimeSpan Debit04 { get; set; }
    public TimeSpan Debit03 { get; set; }
    public TimeSpan Debit02 { get; set; }
    public TimeSpan Debit01 { get; set; }
    public TimeSpan Debit12 { get; set; }
    public TimeSpan Debit11 { get; set; }
    public TimeSpan Debit10 { get; set; }
    public TimeSpan Maximummonth { get; set; }
    public TimeSpan Debitweek { get; set; }
    public TimeSpan Maximumcarryovermonth { get; set; }
    public TimeSpan Maximumweek { get; set; }
    public TimeSpan Endofmonthvalue { get; set; }
    public DateTime Dateofbirth { get; set; }
    public DateTime Endregistration { get; set; }
    public DateTime Leave { get; set; }
    public DateTime Startregistration { get; set; }
    public DateTime Entrance { get; set; }
    public DateTime Startofleaveyear { get; set; }
}
