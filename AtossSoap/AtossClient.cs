﻿using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.ServiceModel.Channels;
using AtossSoap.ATCWebService;
using AtossSoap.Models;
using Microsoft.Extensions.Logging;

[assembly: InternalsVisibleTo("SoapConnectionTest")]

namespace AtossSoap;

internal class AtossClient : IAtossClient {
    private readonly ILogger? _logger;
    private string Username { get; set; }
    private string Password { get; set; }
    private string ServerAddress { get; set; }

    private ATCWebClient? _client;
    private const string NotLoggedIn = "Not logged in. Please call Login() first";

    public AtossClient(string username, string password, string serverAddress, ILogger? logger = null) {
        _logger = logger;
        Username = username;
        Password = password;
        ServerAddress = serverAddress;
    }

    private void LogMessage(Exception e, LogLevel level = LogLevel.Error) {
        if (_logger != null) {
            _logger.Log(level, e, e.Message);
        }
        else {
            Console.WriteLine($"{DateTime.Now} {level}: {e.Message}");
        }
    }

    private void LogMessage(string message, LogLevel level = LogLevel.Information) {
        if (_logger != null) {
            _logger.Log(level, message);
        }
        else {
            Console.WriteLine($"{DateTime.Now} {level}: {message}");
        }
    }

    /// <summary>
    /// Logs in to the server
    /// </summary>
    public async Task Login() {
        var binding = new BasicHttpsBinding(BasicHttpsSecurityMode.Transport) {
            Name = "WebIAPIServiceSoapBinding",
            // enable cookies for session handling
            AllowCookies = true,
            ReaderQuotas = {
                // set encoding
                // increase quota sizes
                MaxNameTableCharCount = 2147483647,
                MaxArrayLength = 2147483647,
                MaxStringContentLength = 2147483647,
                MaxBytesPerRead = 2147483647
            }
        };
        binding.ReaderQuotas.MaxNameTableCharCount = 2147483647;
        binding.MaxBufferSize = 2147483647;
        binding.MaxBufferPoolSize = 2147483647;
        binding.MaxReceivedMessageSize = 2147483647;
// enable http basic auth
        binding.Security.Mode = BasicHttpsSecurityMode.Transport;
        binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
        binding.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.Basic;
        // binding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
        // binding.Security.Message.AlgorithmSuite = System.ServiceModel.Security.SecurityAlgorithmSuite.Default;
        CustomBinding customBinding = new CustomBinding(binding);
        SecurityBindingElement element =
            customBinding.Elements.Find<SecurityBindingElement>(); // set endpoint address

        _client = new ATCWebClient(binding, new EndpointAddress(ServerAddress));
        _client.ClientCredentials.UserName.UserName = Username;
        _client.ClientCredentials.UserName.Password = Password;
        // await client.OpenAsync();
        try {
            await _client.loginAsync();
        }
        catch (Exception e) {
            LogMessage("Login failed");
            LogMessage(e);
            throw;
        }

        LogMessage($"Logged in, client state {_client.State}");
    }


    /// <summary>
    /// Returns all employees found in the system
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<List<Employee>> GetAllEmployeesAsync() {
        if (_client == null) {
            throw new Exception(NotLoggedIn);
        }

        var apiResult = await _client.getEmployeesAsync(0, 0, null, null, null, null, null, null);
        var employeeData = apiResult.@return.ToList();

        var result = new List<Employee>();

        foreach (var employee in employeeData) {
            var convertedEmployee = Helper.Convert<Employee>(employee);
            result.Add(convertedEmployee);
        }

        return result;
    }


    /// <summary>
    /// Retrieves an employee with the specified employee ID.
    /// </summary>
    /// <param name="employeeId">The ID of the employee to retrieve.</param>
    /// <returns>The employee object with the specified employee ID.</returns>
    /// <exception cref="Exception">Thrown when the client is not logged in.</exception>
    public async Task<Employee?> GetEmployee(string employeeId) {
        if (_client == null) {
            throw new Exception(NotLoggedIn);
        }

        try {
            var apiResult = await _client.getEmployeesAsync(0, 0, new []{employeeId}, null, null, null, null, null);
            var employeeData = apiResult?.@return?.ToList();

            var result = new List<Employee>();

            foreach (var employee in employeeData) {
                var convertedEmployee = Helper.Convert<Employee>(employee);
                result.Add(convertedEmployee);
            }

            return result.FirstOrDefault();
        }
        catch (Exception e) {
            LogMessage(e);
            throw;
        }
    }

    /// <summary>
    /// Returns the employee ID for the given tag or null if no employee is found
    /// </summary>
    /// <param name="rfidTagNumber">RfID Tag number</param>
    /// <returns>Employee ID if found otherwise null</returns>
    /// <exception cref="Exception"></exception>
    public async Task<string?> IdentifyEmployeeWithTag(string rfidTagNumber) {
        if (_client == null) {
            throw new Exception(NotLoggedIn);
        }

        try {
            var result = await _client.getBadgeEmployeeAsync(rfidTagNumber, "", false);
            return result.@return;
        }
        catch (Exception e) {
            return null;
        }
    }

    /// <summary>
    /// Returns all tables found in the system
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<List<Table>> GetTables() {
        if (_client == null) {
            throw new Exception(NotLoggedIn);
        }

        var result = new List<Table>();
        var tables = await _client.getTablesAsync("");
        foreach (var table in tables.@return) {
            result.Add(Helper.Convert<Table>(table));
        }

        return result;
    }

    /// <summary>
    /// Returns the badges for the given employeeId. If no employeeId is given, all badges are returned
    /// </summary>
    /// <param name="employeeId">Optional Employee ID</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<List<Badge>> GetBadges(string? employeeId = null) {
        if (_client == null) {
            throw new Exception(NotLoggedIn);
        }

        var employeeArray = employeeId != null ? new string[] { employeeId } : new string[] { };

        var badges = await _client.getBadgesAsync("", employeeArray, "");
        var result = new List<Badge>();
        foreach (var badge in badges.@return) {
            result.Add(Helper.Convert<Badge>(badge));
        }

        return result;
    }


    /// <summary>
    /// Returns all Departments found in the system
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<List<Department>> GetDepartments() {
        if (_client == null) {
            throw new Exception(NotLoggedIn);
        }

        var departments = await _client.getDepartmentsAsync(null, 0, 0, null, null, -1);
        var result = new List<Department>();
        foreach (var department in departments.@return) {
            result.Add(Helper.Convert<Department>(department));
        }

        return result;
    }

    /// <summary>
    /// Returns all accounts found in the system
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<List<Account>> GetAccounts() {
        if (_client == null) {
            throw new Exception(NotLoggedIn);
        }

        var items = await _client.getAccountsAsync(-1, null, null, null, 0);

        var result = new List<Account>();
        foreach (var item in items.@return) {
            result.Add(Helper.Convert<Account>(item));
        }

        return result;
    }


    /// <summary>
    /// Gets a list of bookings within a specified time range.
    /// </summary>
    /// <param name="from">The starting date and time of the range.</param>
    /// <param name="until">The ending date and time of the range.</param>
    /// <param name="employeeId">Optional. The ID of the employee to filter the bookings (default is null).</param>
    /// <returns>A list of bookings.</returns>
    /// <exception cref="Exception">Thrown when the client is not logged in.</exception>
    public async Task<List<Booking>> GetBookings(DateTime from, DateTime until, string? employeeId = null) {
        if (_client == null) {
            throw new Exception(NotLoggedIn);
        }

        var employeeArray = employeeId != null ? new string[] { employeeId } : new string[] { };

        var result = new List<Booking>();
        var items = await _client.getBookingsAsync(employeeArray, from, until, 0, null, null, null, null, null, null,
            0);

        foreach (var item in items.@return) {
            result.Add(Helper.Convert<Booking>(item));
        }

        return result;
    }

    /// <summary>
    /// Retrieves the state of an employee.
    /// </summary>
    /// <param name="employeeId">The ID of the employee.</param>
    /// <returns>A Task object representing the asynchronous operation. The result of the task is the state of the employee.</returns>
    /// <exception cref="Exception">Thrown when the client is not logged in.</exception>
    public async Task<State> GetEmployeeState(string employeeId) {
        if (_client == null) {
            throw new Exception(NotLoggedIn);
        }

        var state = await _client.getEmployeeStateAsync(null, new[] { employeeId }, DateTime.Now, -1, 0);
        var result = Helper.Convert<State>(state.@return.First());

        return result;
    }

    public async Task Dummy() {
        DateTime from = DateTime.Now.AddDays(-20);
        DateTime until = DateTime.Now;
        string? employeeId = "418";

        var employeeArray = employeeId != null ? new string[] { employeeId } : new string[] { };

        var items = await _client.getBookingsAsync(employeeArray, from, until, 0, null, null, null, null, null, null,
            0);

        Helper.StoreStructure("Booking", items.@return.First());
    }
}
