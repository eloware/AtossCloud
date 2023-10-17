using AtossSoap;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Xunit.Abstractions;

namespace SoapConnectionTest;

public class SimpleConnectionTests {
    private readonly IAtossClient _client;
    private readonly IConfigurationRoot _config;
    private readonly ITestOutputHelper output;

    public SimpleConnectionTests(ITestOutputHelper output) {
        this.output = output;
        _config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        _client = ClientFactory.Create(
            _config["Atoss:Username"]!,
            _config["Atoss:Password"]!,
            _config["Atoss:ServerAddress"]!);

        _client.Login().Wait();
    }

    [Fact]
    public async Task GetAllEmployees() {
        var employees = await _client.GetAllEmployeesAsync();
        employees.Should().NotBeNull();

        output.WriteLine($"Found {employees.Count.ToString()} Employees");

        output.WriteLine("| EmployeeId | Name | Dateofbirth | Maximumbalancemonth |");
        output.WriteLine("| --- | --- | --- | --- |");
        foreach (var employee in employees) {
            output.WriteLine(
                $"| {employee.EmployeeId} | {employee.Firstname} | {employee.Lastname} | {employee.Dateofbirth} | {employee.Maximumbalancemonth} |");
        }
    }

    [Fact]
    public async Task GetEmployeeForTag() {
        var result = await _client.IdentifyEmployeeWithTag(_config["TestData:RfID"]!);
        result.Should().NotBeNull();
        result.Should().Be(_config["TestData:EmployeeId"]);
    }

    [Fact]
    public async Task TestGetTables() {
        await _client.GetTables();
    }

    [Fact]
    public async Task GetBadgeForEmployee() {
        var badge = await _client.GetBadges(_config["TestData:EmployeeId"]!);
        badge.Should().NotBeNull();
        badge.Count.Should().BeGreaterThan(0);
        badge.Where(b => b.Id == _config["TestData:RfID"]).Should().NotBeNull();
    }

    [Fact]
    public async Task TestGetBadge() {
        var badged = await _client.GetBadges();
        badged.Should().NotBeNull();
        badged.Count.Should().BeGreaterThan(0);

        badged.ForEach(b => b.EmployeeId.Should().NotBeNull());

        foreach (var badge in badged.Take(100)) {
            output.WriteLine($"Badge {badge.Id} for {badge.EmployeeId}");
        }
    }

    [Fact]
    public async Task GetDepartments() {
        var departments = await _client.GetDepartments();
        departments.Should().NotBeNull();
        departments.Count.Should().BeGreaterThan(0);
        departments.ForEach(d => output.WriteLine($"{d.Name}"));
    }

    /// <summary>
    /// Getting all accounts
    /// </summary>
    [Fact]
    public async Task GetAccounts() {
        var accounts = await _client.GetAccounts();
        accounts.Should().NotBeNull();
        accounts.Count.Should().BeGreaterThan(0);
        accounts.ForEach(a => output.WriteLine($"{a.Name}"));
    }

    /// <summary>
    /// Getting all Bookings for a given employee within the last 20 days
    /// </summary>
    [Fact]
    public async Task GetBookings() {
        var bookings =
            await _client.GetBookings(DateTime.Now.AddDays(-20), DateTime.Now, _config["TestData:EmployeeId"]!);
        bookings.Should().NotBeNull();
        bookings.Count.Should().BeGreaterThan(0);
        bookings.ForEach(b => output.WriteLine($"{b.Date} {b.Duration} {b.Remark}"));
    }

    [Fact]
    public async Task GetStateForEmployee() {
        var state = await _client.GetEmployeeState(_config["TestData:EmployeeId"]!);
        output.WriteLine($"Account id of the current state {state.Account}");
    }

    // [Fact]
    public async Task Dummy() {
        await (_client as AtossClient)!.Dummy();
    }
}
