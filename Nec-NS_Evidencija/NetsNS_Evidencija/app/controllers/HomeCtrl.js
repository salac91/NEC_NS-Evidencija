angular.module('MvcApp.HomeController', [])
 .controller('HomeCtrl', ['$scope', '$http', function ($scope, $http) {
    
     $scope.logedIn = false;
     $scope.role = "";

     $scope.setCurrentChildController = function (childScope) {
         $scope.childScope = childScope;
     }

     $http.get('/Account/IsLogedIn').success(function (isLoged) {
         $scope.logedIn = isLoged;
         if (isLoged) {
             $http.get('/Account/GetRole').success(function (role) {
                 $scope.role = role;
             });
         }
     });

     $scope.logout = function () {

         $scope.role = "";
         $scope.logedIn = false;

         $scope.childScope.logedIn = false;
         $scope.childScope.error = "";

         $http.post('/Account/LogOff').success()(function () {
            
         });
     }
 }]);