doLittle.namespace("doLittle.Visualizer.QualityAssurance", {
    index: doLittle.views.ViewModel.extend(function (allProblems) {
        var self = this;

        this.allProblems = allProblems.all().execute();


        this.getSeverityImageSrc = function (severity) {
            return "/doLittle/Visualizer/QualityAssurance/warning.png";
        };
    })
});