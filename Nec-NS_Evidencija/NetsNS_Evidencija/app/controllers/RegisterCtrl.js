angular.module('MvcApp.RegisterController', [])
 .controller('RegisterCtrl', ['$scope', '$http', '$window', function ($scope, $http, $window) {

     //Init
     $scope.sortType = 'UserName'; // set the default sort type
     $scope.sortReverse = false;  // set the default sort order
     $scope.searchAccount = '';     // set the default search/filter term

     $http.get('/Account/RegisteredUsers').success(function (data) {
         $scope.model = data;
     });

     $scope.new = {
         RegisterModel: {}
     }
     //Init end

     $scope.selectedRow = null;
     $scope.setClickedRow = function (index) {  //function that sets the value of selectedRow to current index
         $scope.selectedRow = index;
     }

     $scope.new.RegisterModel.Role = "Coach";

     $scope.createNewAccount = function () {
         $http.post('/Account/Register', $scope.new.RegisterModel).success(function (errors) {

             $scope.errors = errors;

             if (errors != "no errors") $window.alert("Niste dobro popunili polja!");

             $http.get('/Account/RegisteredUsers').success(function (data) {
                 $scope.model = data;
             });

             clearInputFields();

         });
          
     }

     $scope.deleteAccount = function (username) {
         if ($window.confirm("Da li ste sigurni?")) {
             $http.delete('/Account/DeleteAccount?id=' + username).success(function (data) {
                 $http.get('/Account/RegisteredUsers').success(function (data) {
                     $scope.model = data;
                 });
             });
         }
     }

     function clearInputFields() {
         $scope.new.RegisterModel.UserName = null;
         $scope.new.RegisterModel.Password = null;
         $scope.new.RegisterModel.ConfirmPassword = null;
         $scope.new.RegisterModel.Role = "Coach";
     }

 }]);