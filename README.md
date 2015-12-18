# Twitter Tools
Twitter Tools is a project of tools for server side use in aspnet5 MVC6 applications.  It provides a service which allows the developer to load twitter timelines for any user.  The authentication method used is [application-only](https://dev.twitter.com/oauth/application-only).

### Setup
Twitter Tools can be installed by adding a reference to the package in the `project.json` file of a VS2015 web project.  Additionally, it can be found in the **nuget** directory using the package manager.

1.  Install **ITeamSoftware.Tools.Twitter 1.0.0-*** package in your project.
2.  In `Startup.cs`, add the twitter services using the `AddTwitterTools` extension method on your `IServiceCollection`
```csharp
public IServiceProvider ConfigureServices(IServiceCollection services)
{
  ...
  services.AddTwitterTools(options => 
  {
    options.ConsumerKey = Configuration['TwitterConsumerKey'];
    options.ConsumerSecret = Configuration['TwitterConsumerSecret'];
  });
  ...
}
```

### Usage
1.  Inject `ITwitterApplicationOnlyService` into a controller or location where you wish to load a timeline
2.  Acquire a token using the `GetTokenAsync` method.
3.  Load the timeline using the `GetTimeline` method.
4.  ???
5.  Profit
