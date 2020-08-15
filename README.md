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

## Usage

In `TakNotify`, the system works around `INotification` object interface. Basically, you just need to:
1. Instantiate the object of `INotification`
2. Register providers into the object
3. Invoke the `Send` method to send the notification

### Object Instantiation

The `INotification` interface should be instantiated as a singleton to allow "setup once, use multiple times" model.
The `Notification` class is the implementation of `INotification` which has been designed with singleton usage in mind.
You cannot use the `new` keyword. Instead, you can use the `Notification.GetInstance()` method for this purpose:

```c#
var logger = LoggerFactory
    .Create(builder => builder.SetMinimumLevel(LogLevel.Debug).AddConsole())
    .CreateLogger<Notification>();

var notification = Notification.GetInstance(logger);
```

Note that the `GetInstance()` method requires `ILogger` as parameter as in the above example.

### Provider registration

The `INotification` only acts as an orchestrator object. It doesn't have any knowledge on how a notification should be sent.
You need to register one or more providers into the object depending on your needs. Just like the instantiation of `INotification` object,
the registration of TakNotify providers need to be done only once during the setup period:

```c#
var smtpProvider = new SmtpProvider();
notification.AddProvider(smtpProvider);
```

In the example above, we register the [SMTP provider](https://www.nuget.org/packages/TakNotify.Provider.Smtp/) which will send
email notification via SMTP. You can register as many providers as you want in the setup.

### Sending the notification

The generic way in sending notification is by using the `INotification.Send()` method. It requires you to pass the provider name and the message parameters that are specific to the provider:

```c#
var messageParameters = new MessageParameterCollection
{
    {"ToAddresses", "email@example.com"},
    {"Subject", "Test Email"}
};

var result = await notification.Send("smtp", messageParameters);
```

You can see how simple it is to invoke the `Send()` method as in the example above. However, if you use the official TakNotify providers, e.g. [TakNotify.Provider.Smtp](https://www.nuget.org/packages/TakNotify.Provider.Smtp/), it also provide an extension method to send the notification which makes the process is even easier:

```c#
var message = new EmailMessage
{
    ToAddresses = new List<string> { "email@example.com" },
    Subject = "Test Email"
};

var result = await notification.SendEmailWithSmtp(message);
```

In the example above, you can see that the message parameters are defined in a strongly typed object which helps you to define the parameters without having to remember the exact name of each of the item.

## Next

Please refer to the [project page](https://taknotify.github.io/) to get more info about the ecosystem of `TakNotify` library.
