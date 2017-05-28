doLittle.namespace("doLittle.io", {
    File: doLittle.Type.extend(function (path) {
        /// <summary>Represents a file</summary>

        /// <field name="type" type="doLittle.io.fileType">Type of file represented</field>
        this.type = doLittle.io.fileType.unknown;

        /// <field name="path" type="doLittle.Path">The path representing the file</field>
        this.path = doLittle.Path.create({ fullPath: path });
    })
});