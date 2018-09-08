---
title: About Startup
description: Learn how to work effectively
keywords: ASP.NET Core
author: einari
---
# Startup

To get started with Dolittle for a Web project, all you need to do is add a dependency to the `Dolittle.Web` NuGet package:

```cli
dotnet add package Dolittle.Web
```

Once this is done, you can get Dolittle quite easily configured in your `Startup.cs` file:

```csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRouting();
        services.AddDolittle();
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
        loggerFactory.AddConsole();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseDolittle(env);
    }
}
```

Notice that Dolittle relies on the routing mechanism in ASP.NET Core through the `.AddRouting` call.
With this in place you can get started with everything else, have a look at the [end to end tutorial](../Tutorials/end_to_end.md).

