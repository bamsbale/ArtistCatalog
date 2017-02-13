var app = angular.module('artistCatalogApp', ['ngRoute']);

app.config(function ($routeProvider) {
    $routeProvider.when("/", {
        controller: "artistController",
        templateUrl: "/app/views/artistView.html"
    });

    $routeProvider.when("/artist/:uid/releases", {
        controller: "releaseController",
        templateUrl: "/app/views/releaseView.html"
    });

    $routeProvider.otherwise({ reirectTo: "/" });
});