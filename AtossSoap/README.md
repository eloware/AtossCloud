# ATOSS Cloud Connection

> # IN DEVELOPMENT

This NuGet package provides a simple way to connect to the ATOSS Cloud API.

## Please feel free to contribute to this project.

The source code is freely available on [GitHub](https://github.com/eloware/AtossCloud)

## Functions

This packages provides and easy to use and well documented abstraction layer to access ATOSS Cloud data.

## Usage

```csharp
var client = new AtossClient(
            "Username",
            "Password",
            "Server Url");

await client.Login();
```

After the login you can use the client to access the data.

```csharp
var employees = await client.GetAllEmployeesAsync();
```

> After releasing the version 1.0.0 the list of functions will be extended.