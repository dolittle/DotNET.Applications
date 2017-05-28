describe("when setting container and url matches mapper", function () {
    var container = $("<div/>")[0];
    var uriMapper = {
        resolve: function (input) {
            if (input == "Something") return "ThePath";
        },
        hasMappingFor: function () {
            return true;
        }
    };
    var home = "The Home";
    var history = {
        Adapter: {
            bind: function () { }
        },
        getState: function() {
            return {
                url:"http://localhost/Something"
            }
        }
    };

    var frame = doLittle.navigation.NavigationFrame.create({
        uriMapper: uriMapper,
        home: home,
        history: history
    });

    frame.configureFor(container);

    it("should current uri to the path given", function () {
        expect(frame.currentUri()).toBe("Something");
    });
});