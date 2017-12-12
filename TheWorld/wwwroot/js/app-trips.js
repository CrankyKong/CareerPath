//app-trips.js
(function () {

    "use strict";

    //Creating Module []
    angular.module("app-trips", ["simpleControls", "ngRoute" ])
        .config(function ($routeProvider) {

            $routeProvider.when("/", {
                controller: "tripsController",
                controllerAs: "vm",
                templateUrl: "/views/tripsView.html"
            });

            $routeProvider.when("/editor/:tripName", {
                controller: "tripEditorController",
                controllerAs: "vm",
                templateUrl: "/views/tripEditorView.html"
            });

            $routeProvider.when("/organizations", {
                controller: "organizationsController",
                controllerAs: "vm",
                templateUrl: "/views/organizationsView.html"
            });

            $routeProvider.when("/jobTitles", {
                controller: "jobTitlesController",
                controllerAs: "vm",
                templateUrl: "/views/jobTitlesView.html"
            });

            $routeProvider.otherwise({ redirectTo: "/"});

        });

})();