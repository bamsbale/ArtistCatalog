'use strict';
app.controller("releaseController", function ($scope, $http, $routeParams) {
    $scope.releases = [];
    $scope.currentFavourites = [];

    $scope.is_authenticated = false;
    $scope.aspNetUserId = '';
    $scope.uid = $routeParams.uid;

    $scope.isBusy = true;
    $scope.isResultFound = true;

    $scope.init = function () {     
        set_authentication();
    };

    $scope.getMyClass = function (releaseId) {
        return $scope.currentFavourites.indexOf(releaseId) > -1 ? 'glyphicon glyphicon-star active' : 'glyphicon glyphicon-star';
    };

    $scope.addToFavourites = function (_release) {
        var _isActive = $scope.currentFavourites != null ? $scope.currentFavourites.indexOf(_release.releaseId) > -1 : false;
        var _param = {
            AspNetUserId: '',
            ReleaseId: _release.releaseId,
            Title: _release.title,
            ReleaseYear: _release.yearOfRelease,
            Label: _release.label,
            NoOfTracks: _release.numberOfTracks,
            ModifiedOn: '',
            IsActive: !_isActive
        };

        $scope.isBusy = true;

        $http.post('/favourites/add', _param)
        .then(function (result) {
            // Success
            set_authentication();
        },
        function () {
            // Error
        }).then(function () {
            // Finally
            $scope.isBusy = false;
        });
    };

    function set_authentication() {
        $http.get("/security/isauthenticated")
            .then(function (result) {
                // Success
                $scope.is_authenticated = result.data.isAuthenticated;
                $scope.aspNetUserId = result.data.aspNetUserId;

                get_favourites(result.data.aspNetUserId);
            },
            function () {
                // Error
            });
    };

    function get_favourites(_aspNetUserId) {
        var _params = {
            user_id: _aspNetUserId,
            is_active: true
        };

        var config = {
            params: _params,
            headers: { 'Accept': 'application/json' }
        };

        $http.get("/favourites", config)
        .then(function (result) {
            // Success
            $scope.currentFavourites = result.data;
        },
        function () {
            // Error
        }).then(function () {
            // Finally
            get_releases();
        });
    };

    function get_releases() {
        var config = {
            headers: { 'Accept': 'application/json' }
        };

        $http.get("/artist/" + $scope.uid + "/releases", config)
        .then(function (result) {
            // Success
            angular.copy(result.data.releases, $scope.releases);
        },
        function () {
            // Error
        })
        .then(function () {
            // Finally
            $scope.isBusy = false;
            $scope.isResultFound = $scope.releases.length > 0;
        });
    };
});