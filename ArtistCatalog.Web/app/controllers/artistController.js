'use strict';
app.controller("artistController", function ($scope, $http) {
    $scope.artists = [];
    $scope.search_criteria = null;
    $scope.page = 0;

    $scope.isBusy = true;
    $scope.isResultFound = true;

    $scope.next = function () {
        $scope.page = $scope.artists !== null ? ($scope.artists.page < $scope.artists.numberOfPages - 1 ? $scope.artists.page + 1 : $scope.artists.numberOfPages - 1) : 0;
        get_artists();
    };

    $scope.previous = function () {
        $scope.page = $scope.artists !== null ? ($scope.artists.page > 1 ? $scope.artists.page - 1 : 0) : 0;
        get_artists();
    };

    $scope.search = function () {
        $scope.page = 0;
        get_artists();
    };

    $scope.init = function () {
        get_artists();
    };

    function get_artists() {
        var _params = {
            search_criteria: $scope.search_criteria,
            page_number: $scope.page
        };

        var config = {
            params: _params,
            headers: { 'Accept': 'application/json' }
        };

        $http.get("/artist/search", config)
        .then(function (result) {
            // Success
            angular.copy(result.data, $scope.artists);
        },
        function () {
            // Error
        })
        .then(function () {
            // Finally
            $scope.isBusy = false;
            $scope.isResultFound = $scope.artists.results.length > 0;
        });
    };
});