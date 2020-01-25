---
title: Code Analysis
description: .NET SDK Code Analysis
repository: https://github.com/dolittle-runtime/DotNET.SDK
---
The .NET SDK has rules that makes sure that the artifacts that has associated rules with
how they are expected to be written are followed. These rules will run as part of the build
and if any rules are broken, you'll see these as compiler errors and pin-pointed to where
it is happening in the code.

As part of taking a dependency to the [SDK package](https://www.nuget.org/packages/Dolittle.SDK/),
you'll have the rules automatically injected into the build pipeline.

Look [here]({{< relref analyzers >}}) for a list of rules and what they mean.
