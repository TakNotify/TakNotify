# TakNotify

`TakNotify` is a library for .NET Core apps to simplify sending notifications into
various targets.

## Usage

`TakNotify` requires you to install at least a notification provider into your apps 
(you can find the list of available providers in the [providers page](./providers.md)).

Install a provider from NuGet, e.g. the `SMTP Provider`:

```powershell
dotnet add package TakNotify.Provider.Smtp
```

When working with ASP.NET Core apps, you can also install `TakNotify.AspNetCore` 
package to help you setting up `TakNotify` into the Dependency Injection system.

```powershell
dotnet add package TakNotify.AspNetCore
```

In the `Startup.cs`, you can add the following code to the `ConfigureServices` 
method:

```c#
public void ConfigureServices(IServiceCollection services)
{
    ...

    services
        .AddGoNotify()
        .AddProvider<SmtpProvider, SmtpProviderOptions>(options =>
        {
            options.Server = Configuration.GetValue<string>("Smtp:Server");
            options.Port = Configuration.GetValue<int>("Smtp:Port");
            options.Username = Configuration.GetValue<string>("Smtp:Username");
            options.Password = Configuration.GetValue<string>("Smtp:Password");
            options.UseSSL = Configuration.GetValue<bool>("Smtp:UseSSL");
        });

    ...
}
```

The `AddGoNotify()` method will register the `INotification` service which can be 
be used to send the notifications.

The `AddProvider<>()` method will register the notification provider along with its 
specific options. In the sample code above, the option values are taken from 
the `Configuration` object. You can add multiple providers in the set up.

## Build

If you want to build `TakNotify` from source code, the easiest way is by executing 
the `build.ps1` script:

```powershell
.\build.ps1
```

The only pre-requisite to build `TakNotify` is that you have .NET Core SDK 3.1 
installed in your machine.