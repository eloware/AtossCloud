using AtossSoap.Models;

namespace AtossSoap;

public interface IAtossClient {
    /// <summary>
    /// Logs in to the server
    /// </summary>
    Task Login();

    /// <summary>
    /// Returns all employees found in the system
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    Task<List<Employee>> GetAllEmployeesAsync();

    /// <summary>
    /// Returns the employee ID for the given tag or null if no employee is found
    /// </summary>
    /// <param name="rfidTagNumber">RfID Tag number</param>
    /// <returns>Employee ID if found otherwise null</returns>
    /// <exception cref="Exception"></exception>
    Task<string?> IdentifyEmployeeWithTag(string rfidTagNumber);

    /// <summary>
    /// Returns all tables found in the system
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    Task<List<Table>> GetTables();

    /// <summary>
    /// Returns the badges for the given employeeId. If no employeeId is given, all badges are returned
    /// </summary>
    /// <param name="employeeId">Optional Employee ID</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    Task<List<Badge>> GetBadges(string? employeeId = null);

    /// <summary>
    /// Returns all Departments found in the system
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    Task<List<Department>> GetDepartments();

    /// <summary>
    /// Returns all accounts found in the system
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    Task<List<Account>> GetAccounts();

    /// <summary>
    /// Returns all bookings for the given time period
    /// </summary>
    /// <param name="from"></param>
    /// <param name="until"></param>
    /// <param name="employeeId">Optional employee id</param>
    /// <returns></returns>
    Task<List<Booking>> GetBookings(DateTime from, DateTime until, string? employeeId = null);

    /// <summary>
    /// Returns the current state of the employee
    /// </summary>
    /// <param name="employeeId"></param>
    /// <returns></returns>
    Task<State> GetEmployeeState(string employeeId);

    /// <summary>
    /// Retrieves an employee with the specified employee ID.
    /// </summary>
    /// <param name="employeeId">The ID of the employee to retrieve.</param>
    /// <returns>A Task representing the asynchronous operation. The Task will contain the retrieved Employee object.</returns>
    Task<Employee?> GetEmployee(string employeeId);
}
