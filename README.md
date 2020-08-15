[![Build Status](https://dev.azure.com/taknotify/TakNotify/_apis/build/status/TakNotify?branchName=master)](https://dev.azure.com/taknotify/TakNotify/_build/latest?definitionId=1&branchName=master)
![Nuget](https://img.shields.io/nuget/v/taknotify)

# TakNotify

This is the core library of the `TakNotify` notification system. It contains some contracts, 
base classess, and the implementation of `INotification` interface.

In order to make the notification system works, it should be accompanied by, at least, one provider.
In fact, all `TakNotify` providers have a dependency to this core library.

## NuGet Package

The `TakNotify` core library is available as a NuGet package in https://www.nuget.org/packages/TakNotify.

It can be installed easily via the `Manage NuGet Packages` menu in Visual Studio or by using the
`dotnet add package` command in command line. However, in practice, you don't need to reference 
this package directly in your application. When you add one or more `TakNotify` providers, you will 
also indirectly add the core library to your application.

## Build the source code

If for some reasons you need to build the `TakNotify` library from the code yourself, you can 
always use the usual `dotnet build` command because basically it's just a .NET Core class library.

```powershell
dotnet build .\src\TakNotify
```

However, we recommend you to just use the `build.ps1` script because it will not only help you
to build the code, but also publish it into a ready to use NuGet package.

```powershell
.\build.ps1
```

## Next

Please refer to the [project page](https://taknotify.github.io/) to get more details about `TakNotify`
library.
