---
title: Analyzers
description: This contains the overview of all the analyzers
---
Below are the Dolittle specific rules that we are enforcing.
Rules can be disabled either through custom [rulesets](https://docs.microsoft.com/en-us/visualstudio/code-quality/how-to-create-a-custom-rule-set) or using the [-nowarn](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-options/nowarn-compiler-option) compiler option or using the
[NoWarn property](https://docs.microsoft.com/en-us/visualstudio/msbuild/common-msbuild-project-properties?view=vs-2019)
in your `.csproj` file.

Example of NoWarn property:

```xml
<NoWarn>$(NoWarn),DL1001,DL1002</NoWarn>
```

## Rules

| Id | Title |
| --- | ----- |
| [DL0001]({{< relref DL1001 >}}) | EventsMustBeImmutable |
