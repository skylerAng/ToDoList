
var myapp = angular.module('build', [])

myapp.controller('', function ($scope, $http) {

    $http.get('/ToDoes/BuildToDoTable', function (response) {

        $scope.emptable = response.data;
    })

})