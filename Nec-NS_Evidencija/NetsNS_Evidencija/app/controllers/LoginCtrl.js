angular.module('MvcApp.LoginController', [])
 .controller('LoginCtrl', ['$scope', '$http', '$timeout', function ($scope, $http, $timeout) {

     $scope.new = {
         LoginModel: {}
     }

     $scope.ready = false;

     $http.get('/Account/IsLogedIn').success(function (isLoged) {
         $scope.logedIn = isLoged;
         $scope.logedIn = $scope.$parent.logedIn;
     });

     $timeout(function () { $scope.ready = true; }, 500);

     $scope.logedIn = $scope.$parent.logedIn;

     $scope.login = function () {

         $http.post('/Account/Login', $scope.new.LoginModel).success(function (status) {
             $http.get('/Account/GetRole').success(function (role) {
                 if (status != "failed") {
                     $scope.$parent.role = role;
                     $scope.$parent.logedIn = true;
                     $scope.logedIn = $scope.$parent.logedIn;
                  
                 }
                 else {
                     $scope.error = "Korisničko ime ili šifra su netačni!";
                 }
             });
          
         });
     }

     $scope.$parent.setCurrentChildController($scope);

 }]);