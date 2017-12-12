//organizationsController.js
(function () {

    "use strict";

    // Getting existing module
    angular.module("app-trips")
        .controller("organizationsController", organizationsController);

    function organizationsController($http) {

        var vm = this;

        vm.organizations = [];

        vm.newOrganization = {};

        vm.errorMessage = "";
        vm.isBusy = true;

        $http.get("/api/organizations")
            .then(function (response) {
                // Success            

                angular.copy(response.data, vm.organizations);

                vm.organizations.sort(function (a, b) {
                    var alc = a.name.toLowerCase(), blc = b.name.toLowerCase();
                    return alc > blc ? 1 : alc < blc ? -1 : 0;
                });
                var obj = {};

                for (var i = 0, len = vm.organizations.length; i < len; i++) {
                    obj[vm.organizations[i]['name']] = vm.organizations[i];
                }

                vm.organizations = new Array();
                for (var key in obj) {
                    vm.organizations.push(obj[key]);
                }


            }, function () {
                // Failure
                vm.errorMessage = "Failed to load data : " + error;
            })
            .finally(function () {
                vm.isBusy = false;
            });



        vm.addOrganization = function () {
            vm.isBusy = true;
            vm.errorMessage = "";

            $http.post("/api/organizations", vm.newOrganization)
                .then(function (response) {
                    // success
                    vm.organizations.push(response.data);
                    vm.newOrganization = {};
                }, function () {
                    // failure
                    vm.errorMessage = "Failed to save new organization: " + error;
                })
                .finally(function () {
                    vm.isBusy = false;
                });

        };


    }

})();