doLittle.namespace("doLittle.Visualizer", {
    index: doLittle.views.ViewModel.extend(function () {
        var self = this;

        this.categories = [
            { name: "QualityAssurance", displayName: "Quality Assurance", description: "" },
            { name: "Tasks", displayName: "Tasks", description: "" }
        ];

        this.currentCategory = ko.observable("Visualizer/"+this.categories[0].name+"/index");

        this.selectCategory = function (category) {
            self.currentCategory("Visualizer/" + category.name + "/index");
        };
    })
});

ko.bindingHandlers.sidebar = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        $("body").mouseover(function (e) {
            if ($(e.target).closest("table.doLittleSidebarIcons").length != 1) {
                $("#icons").removeClass("doLittleSidebarWithContent");
                $("#icons").removeClass("doLittleSidebarFullSize");
            }

        });

        $("#sidebar").mouseover(function () {
            $("#icons").addClass("doLittleSidebarIconsVisible");
        });

        $("#sidebar").mouseout(function (e) {
            $("#icons").removeClass("doLittleSidebarIconsVisible");

        });

        $("#icons").mouseover(function () {
            //$("#sidebar").addClass("doLittleSidebarFullSize");
        });

        $("#icons").mouseout(function (e) {
            $("#icons").addClass("doLittleSidebarFullSize");
        });


        $("#icons").click(function () {
            $("#icons").addClass("doLittleSidebarWithContent");
        });
    }
}
