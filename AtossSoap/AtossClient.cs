﻿using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using AtossSoap.ATCWebService;
using AtossSoap.Models;

namespace AtossSoap;

public class AtossClient {
    public string Username { get; set; }
    public string Password { get; set; }
    public string ServerAddress { get; set; }

    private ATCWebClient client;

    public AtossClient(string username, string password, string serverAddress) {
        Username = username;
        Password = password;
        ServerAddress = serverAddress;
    }

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

        client = new ATCWebClient(binding, new EndpointAddress(ServerAddress));
        client.ClientCredentials.UserName.UserName = Username;
        client.ClientCredentials.UserName.Password = Password;
        // await client.OpenAsync();
        try {
            await client.loginAsync();
        }
        catch (Exception e) {
            Console.WriteLine("Login failed");
            Console.WriteLine(e);
            throw;
        }

        Console.WriteLine($"Logged in, client state {client.State}");
    }


    public async Task<List<Employee>> GetAllEmployeesAsync() {
        var apiResult = await client.getEmployeesAsync(0, 0, null, null, null, null, null, null);
        var employeeData = apiResult.@return.ToList();

        var result = new List<Employee>();

        foreach (var employee in employeeData) {
            var convertedEmployee = Helper.Convert<Employee>(employee);
            result.Add(convertedEmployee);
        }

        return result;
    }

    public async Task<string?> IdentifyEmployeeWithTag(string employeeId) {
        try {
            var result = await client.getBadgeEmployeeAsync(employeeId, "", false);
            return result.@return;
        }
        catch (Exception e) {
            return null;
        }
    }
}
