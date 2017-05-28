---
title: About Http Modules
description: Learn how doLittle works with HTTP modules
keywords: ASP.NET
author: einari
---
# OWIN

doLittle hooks into the [OWIN](http://owin.org) way of starting up and taking over the main initialization.
The [BootStrapper](../../api/doLittle.Web.BootStrapper.html) in doLittle uses the [WebActivatorEx](https://github.com/davidebbo/WebActivator)
to hook up the `PreApplicationStart` and `Start` methods. With this, the pipeline is configured and doLittle uses
its different conventions to discover and configure your application. The two most important being the
[ICanConfigure](../../api/doLittle.Configuration.ICanConfigure.html) and [ICanCreateContainer](../../api/doLittle.Configuration.ICanCreateContainer.html)
interfaces. Read more about configuration [here](../Backend/configuration.md) and containers [here](../Backend/container.md).

## Web.config

In order for this mechanism to work, you need to disable the automatic discovery of app startup. This is done by
adding the following to your Web.config.

```xml
<appSettings>
    <add key="owin:AutomaticAppStartup" value="false" />
</appSettings>
```

