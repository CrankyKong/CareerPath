//jobTitlesController.js
(function () {

    "use strict";

    // Getting existing module
    angular.module("app-trips")
        .controller("jobTitlesController", jobTitlesController);

    function jobTitlesController($http) {

        var vm = this;

        vm.jobTitles = [];

        vm.newJobTitle = {};

        vm.errorMessage = "";
        vm.isBusy = true;

        $http.get("/api/jobTitles")
            .then(function (response) {
                // Success
                angular.copy(response.data, vm.jobTitles);

                vm.jobTitles.sort(function (a, b) {
                    var alc = a.name.toLowerCase(), blc = b.name.toLowerCase();
                    return alc > blc ? 1 : alc < blc ? -1 : 0;
                });

                var obj = {};

                for (var i = 0, len = vm.jobTitles.length; i < len; i++) {
                    obj[vm.jobTitles[i]['name']] = vm.jobTitles[i];
                }

                vm.jobTitles = new Array();
                for (var key in obj) {
                    vm.jobTitles.push(obj[key]);
                }

            }, function () {
                // Failure
                vm.errorMessage = "Failed to load data : " + error;
            })
            .finally(function () {
                vm.isBusy = false;
            });



        vm.addJobTitle = function () {
            vm.isBusy = true;
            vm.errorMessage = "";

            $http.post("/api/jobTitles", vm.newJobTitle)
                .then(function (response) {
                    // success
                    vm.jobTitles.push(response.data);
                    vm.newJobTitle = {};
                }, function () {
                    // failure
                    vm.errorMessage = "Failed to save new jobTitle: " + error;
                })
                .finally(function () {
                    vm.isBusy = false;
                });

        };


    }

})();