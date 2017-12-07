//tripEditorController
(function () {
    "use strict";

    angular.module("app-trips")
        .controller("tripEditorController", tripEditorController);

    function tripEditorController($routeParams, $http) {
        var vm = this;

        vm.tripName = $routeParams.tripName;
        vm.stops = [];
        vm.organizations = [];
        vm.jobTitles = [];
        vm.errorMessage = "";
        vm.isBusy = true;
        vm.newStop = {};

        var url = "/api/organizations";

        $http.get(url)
            .then(function (response) {
                //success
                angular.copy(response.data, vm.organizations);
                vm.organizations.sort(function (a, b) {
                    var alc = a.name.toLowerCase(), blc = b.name.toLowerCase();
                    return alc > blc ? 1 : alc < blc ? -1 : 0;
                });
            }, function (err) {
                //error
                vm.errorMessage = "Failed to load organizations";
            })
            .finally(function () {
                vm.isBusy = false;
            }); 

        url = "/api/jobtitles";

        $http.get(url)
            .then(function (response) {
                //success
                angular.copy(response.data, vm.jobTitles);
                vm.jobTitles.sort(function (a, b) {
                    var alc = a.name.toLowerCase(), blc = b.name.toLowerCase();
                    return alc > blc ? 1 : alc < blc ? -1 : 0;
                });
            }, function (err) {
                //error
                vm.errorMessage = "Failed to load job titles";
            })
            .finally(function () {
                vm.isBusy = false;
            }); 

        url = "/api/trips/" + vm.tripName + "/stops";

        $http.get(url)
            .then(function (response) {
                //success
                angular.copy(response.data, vm.stops);
                _showMap(vm.stops);                
            }, function (err) {
                //error
                vm.errorMessage = "Failed to load stops";
            })
            .finally(function () {
                vm.isBusy = false;
            }); 

        

        vm.addStop = function () {
            vm.newStop.order = vm.stops.length + 1;
            vm.isBusy = true;
            $http.post(url, vm.newStop)
                .then(function (response) {
                    //success
                    vm.stops.push(response.data);
                    _showMap(vm.stops);
                    vm.newStop = {};
                }, function (err) {
                    //failure
                    vm.errorMessage = "Failed to add new stops";
                })
                .finally(function () {
                    vm.isBusy = false;
                });

        };
    }

    function _showMap(stops) {

        if (stops && stops.length > 0) {
            var mapStops = _.map(stops, function (item) {
                return {
                    lat: item.latitude,
                    long: item.longitude,
                    info: item.name
                };
            });

            //Show Map
            travelMap.createMap({
                stops: mapStops,
                selector: "#map",
                currentStop: 0,
                initialZoom: 3
            });
        }

    }

})();