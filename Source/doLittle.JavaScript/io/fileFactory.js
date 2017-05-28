doLittle.namespace("doLittle.io", {
    fileFactory: doLittle.Singleton(function () {
        /// <summary>Represents a factory for creating instances of doLittle.io.File</summary>
        this.create = function (path, fileType) {
            /// <summary>Creates a new file</summary>
            /// <param name="path" type="String">Path of file</param>
            /// <param name="fileType" type="doLittle.io.fileType" optional="true">Type of file to use</param>
            /// <returns type="doLittle.io.File">An instance of a file</returns>

            var file = doLittle.io.File.create({ path: path });
            if (!doLittle.isNullOrUndefined(fileType)) {
                file.fileType = fileType;
            }
            return file;
        };
    })
});
doLittle.WellKnownTypesDependencyResolver.types.fileFactory = doLittle.io.fileFactory;