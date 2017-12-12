//app-trips.js
(function () {

    "use strict";

    //Creating Module []
    angular.module("app-world", ["simpleControls", "ngRoute" ])
        .config(function ($routeProvider) {

            $routeProvider.when("/", {
                controller: "worldController",
                controllerAs: "vm",
                templateUrl: "/views/worldView.html"
            });
            

            $routeProvider.otherwise({ redirectTo: "/"});

        });

})();