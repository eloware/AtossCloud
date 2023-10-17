# ATOSS Cloud Connection

> # IN DEVELOPMENT

This NuGet package provides a simple way to connect to the ATOSS Cloud API.

## Please feel free to contribute to this project.

## Functions

Take a look at the `IAtossClient` interface to see the available functions.
[IAtossClient](./AtossSoap/IAtossClient.cs)

## Contribution Guide

### Create new classes from the API
To create new classes from the API the following approach is recommended (e.g. for the EmployeeState class):

```csharp
var state = await _client.getEmployeeStateAsync(null, new[] { employeeId }, DateTime.Now, -1, 0);
Helper.StoreStructure("State", state.@return.First());
```

In the Helper class a method is implemented to store the structure of the object in a file. This file can be used to create the class.
The result of the `StoreStructure` method is a CSharp class and Markdown documentation. 
Both files are copied into the `Models` folder of the AtossSoap project.

If you are unhappy with the naming of the properties within the class, you can change the names. 
The mapper is case insensitive and will map the properties to the correct names.
If you decide to change the name entirely, you have to add the `AtossName` Attribute to the property.

```csharp
[AtossName("employee")]
public string? EmployeeId { get; set; }
```