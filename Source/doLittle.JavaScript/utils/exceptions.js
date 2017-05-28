doLittle.namespace("doLittle");
doLittle.Exception.define("doLittle.LocationNotSpecified","Location was not specified");
doLittle.Exception.define("doLittle.InvalidUriFormat", "Uri format specified is not valid");
doLittle.Exception.define("doLittle.ObjectLiteralNotAllowed", "Object literal is not allowed");
doLittle.Exception.define("doLittle.MissingTypeDefinition", "Type definition was not specified");
doLittle.Exception.define("doLittle.AsynchronousDependenciesDetected", "You should consider using Type.beginCreate() or dependencyResolver.beginResolve() for systems that has asynchronous dependencies");
doLittle.Exception.define("doLittle.UnresolvedDependencies", "Some dependencies was not possible to resolve");