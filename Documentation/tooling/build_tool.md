---
title: Build Tool
description: Describes the Dolittle Build Tool for the .NET SDK
keywords: General, tooling, Build Tool
author: woksin
---

## Background
One of our main visions is to enable developers to build Line of Business products with high productivity while also building products that are scalable and easy to maintain. With tooling we can provide developers with functionalities that enables a better development experience by automatically doing some of the work that is tedious and / or error prone. We can also give a better development experience by providing guidance, tips and squiggly lines by, for example, for .Net utilizing the Roslyn compiler to give the developers warnings and suggestions when they are doing something that does not work well when developing products on our platform or to provide with tips and suggestions for improvements when they aren't utilizing the different tools that we're providing for them to write maintainable code. The DotNET Build Tool is one such tool. This tool is rather important not only for its quality of life functions, but first and foremost for automatically generating and maintaining vital information of the *Bounded Context* for the platform. 

The Dolittle platform needs to know several things related to the [*Application* and *Bounded Contexts*](https://dolittle.io/overview/bounded_context). The whole functionality of a *Bounded Context* is defined by its [*Artifacts*]**(LINK TO ARTIFACTS DOCS)**. These *Artifacts* are extremely vital and central to our platform, everything is dependent on them; the functionality of the *Bounded Context* itself, interaction with other *Bounded Contexts*, interaction with other *Applications* and several other important aspects. Because these *Artifacts* are so important we cannot rely on human to keep track of this, that's why we have a tool that does this for us. This is just one of the current functionalities of the Build Tool, later we'll explain this in more detail.

The .Net Build Tool can be found [here](https://github.com/dolittle/dotnet.sdk/tree/master/Source/Build). It's basically a .NetCore application that is executed each time a build is performed in the .csproj that has a reference to the *Build Tool* "entrypoint" defined [here](https://github.com/dolittle/DotNET.SDK/tree/master/Source/Artifacts.Build).

## The Bounded Context configuration
When you first set up a *Bounded Context* project you need to provide the platform with a few vital pieces of information. The *Build Tool* is expecting a bounded-context.json file describing the configuration in the root folder of the *Bounded Context's* source code. The bounded-context.json configuration needs the following information:
{{% notice warning %}}
May be subject to change
{{% /notice%}}
bounded-context.json:

```json
{
  "application": "0d577eb8-a70b-4e38-aca8-f85b3166bdc2",
  "boundedContext": "f660966d-3a74-44e6-8268-a9aefbae6115",
  "boundedContextName": "Shop",
  "useModules": true,
  "generateProxies": true,
  "proxiesBasePath": "Features",
  "namespaceSegmentsToStrip": {}
}
```

* application - The GUID of the Application that this *Bounded Context* belongs to
* boundedContext - The GUID of the *Bounded Context*
* useModules - Whether or not the 'Bounded Context' topology structure is Module-based or Feature-based. If it’s Module-based then the *Topology* will consist of a list of [*Modules*](https://dolittle.io/overview/bounded_context) with a list of [*Features*](https://dolittle.io/overview/bounded_context). If it’s Feature-based it’ll consist of just a list of *Features*.
* generateProxies - Whether or not we should generate, at the moment, ReadModel and Query proxy classes for the web interaction layer.
* proxiesBasePath - The base path relative to the path where the build is performed from where the proxies will be created.
* namespaceSegmentsToStrip - A dictionary of where the Value is a list of namespace-segments to strip from the namespace when creating the Artifacts when the first segment of the namespace is equal to the Key.


## Topology
One important aspect of *Bounded Contexts* is its topology. The platform needs to have some metadata for *Artifacts*, which *Feature* it belongs to is one such. To be able to map out a *Bounded Context's* *Features* we need to first define its *Topology*. When the *Build Tool* is referenced in a .csproj it will take the assembly and referenced assemblies and it will start discovering *Artifacts*. After it has discovered all the *Artifacts* of the *Bounded Context* it will try define the topology of the *Bounded Structure*. Based on the "useModules" and "namespaceSegmentsToStrip" options in the configuration the *Build Tool* will look at the type paths of the *Artifact* CLR types and create a topology structure based on this type path where *Artifacts* 

{{% notice warning %}}
For the *Build Tool* to work you actually also have to reference to Dolittle.DependencyInversion.Autofac in order for the assembly discovery and the IoC container mechanisms to work. You should get a runtime error in the *Build Tool* dll if this is not in place.
{{% /notice%}}
When the *Build Tool* has ran its course it will output a topology that would look something like this:
```json
{
  "topology": {
    "modules": [
      {
        "module": "8d5a724b-84eb-4085-a766-8d28e681743e",
        "name": "Carts",
        "features": [
          {
            "feature": "80f5e1a2-a2bc-4403-b7ec-8bd90920cf2a",
            "name": "Shopping",
            "subFeatures": []
          }
        ]
      },
      {
        "module": "c020195d-5675-4c17-9cc5-1a7539ce4680",
        "name": "SomeModule",
        "features": [
          {
            "feature": "728459c2-fab1-40c1-9ead-7122a1a890ea",
            "name": "SomeFeature",
            "subFeatures": [
              {
                "feature": "716259c2-fab1-40c1-9ead-7122a1a890ea",
                "name": "SomeSubFeature",
                "subFeatures": []
              },
              {
                "feature": "824459c2-fab1-40c1-9ead-7122a1a890ea",
                "name": "SomeOtherSubFeature",
                "subFeatures": []
              },
            ]
          }
        ]
      },
      {
        "module": "9291da5e-a5ad-4dc7-9037-5c97fad04046",
        "name": "Catalog",
        "features": [
          {
            "feature": "05b89f06-19c3-4502-b349-873ef7761a21",
            "name": "Listing",
            "subFeatures": []
          }
        ]
      }
    ],
    "features": []
  }
}
```
#### Structuring; Modules and Features
We currently support two ways of structuring a *Bounded Context*; one is with *Modules* (the topology definition above is the result of building a *Bounded Context* with topology defined with modules), the other way is with *Features* only.
{{% notice note %}}
There are not practical implications of using *Modules* over *Features*, or vica versa, from the platform's perspective. Currently it's only a matter of how you want to structure the *Bounded Context* internally.
{{% /notice %}}
The only thing that will happen is that the *Build Tool* will enforce the namespace naming convention of *Artifact* types, based on whether or not you use modules or not, so that you are consistent with the structuring. For example if you have defined the bounded context to use modules, the *Build Tool* will fail over if you define this artifact:
```csharp
namespace Domain.TheModule
{
    public class TheCommand : ICommand
    { }
}
```
The *Build Tool* will tell you that a particular *Artifact* cannot fit inside the topology. 
{{% notice note %}}
The reason for this is that every *Artifact* has to belong to a single *Feature*, a *Module* is not a *Feature* it is only a structure that groups *Features*. 
{{% /notice %}}
To correct this you would either have to set useModules to false (then a *Feature* called TheModule would appear in the topology), or you could solve it by simply adding another segment to the namespace, for example:
```csharp
namespace Domain.TheModule.TheFeature
{
    public class TheCommand : ICommand
    { }
}
```
If this was the only *Artifact* in the *Bounded Context* the topology would look like this:
```json
 {
  "topology": {
    "modules": [
      {
        "module": "<Generated GUID>",
        "name": "TheModule",
        "features": [
          {
            "feature": "<Generated GUID>",
            "name": "TheFeature",
            "subFeatures": []
          }
        ]
      }
    ],
    "features": []
  }
}
```
{{% notice note %}}
Note that the "Domain" part of the namespace is completely ignored. This is because the *Build Tool* is by convention ignoring the first segment of the namespace. This is because we think that the first part of the namespace is reserved to indicate the domain area of the type, i.e. "Domain", "Events", "Events.OtherBoundedContext", "Read", "Web", "Policy", etc...
{{% /notice %}}
##### "namespaceSegmentsToStrip"
namespaceSegmentsToStrip can be useful when you want a namespace to have a specific prefix, or if you have a namespace that has a namespace segment which is '.' separated, like for example "Events.Shop".

If you didn't provide any namespaceSegmentsToString Key-value pairs, useModules is true and you had this *Artifact*:
```csharp
namespace Events.OtherBoundedContext.TheModule
{
    [Artifact("<The Artifact's ArtifactId>")]
    public class IAmAnEventFromAnotherBoundedContext : IEvent
    { }
}
```
the *Build Tool* would not throw any errors and the topology would look like this:
```json
 {
  "topology": {
    "modules": [
      {
        "module": "<Generated GUID>",
        "name": "OtherBoundedContext",
        "features": [
          {
            "feature": "<Generated GUID>",
            "name": "TheModule",
            "subFeatures": []
          }
        ]
      }
    ],
    "features": []
  }
}
```
which is obviously not right, we would want the *Build Tool* to fail because we have not given the *Artifact* a real *Feature*. To fix this we could in the bounded-context.json file add namespaceSegmentsToStrip with the following definition:
```json
 {
  "namespaceSegmentsToStrip": {
    "Events": [
      "OtherBoundedContext"
    ]
}
```
then the *Build Tool* would fail saying that the *Artifact* IAmAnEventFromAnotherBoundedContext does not fit in the topology.

## Artifacts
TODO: