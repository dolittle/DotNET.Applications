# Read Me

This is build tasks for dealing with Artifacts.
The tasks are built with knowledge from [this](https://blog.rsuter.com/implement-custom-msbuild-tasks-and-distribute-them-via-nuget/),
[this](https://www.natemcmaster.com/blog/2017/07/05/msbuild-task-in-nuget/) and [this](https://www.natemcmaster.com/blog/2017/11/11/msbuild-task-with-dependencies/).

## Purpose

The purpose of the build tasks is to automatically generate configuration of artifacts when there are new artifacts in
code that does not sit in the configuration file.

It follows a simple convention and relies on the topology configuration for the bounded context to be able to do this.
