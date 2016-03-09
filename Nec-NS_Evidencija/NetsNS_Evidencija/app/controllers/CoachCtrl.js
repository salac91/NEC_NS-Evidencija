/// <reference path="CoachCtrl.js" />
angular.module('MvcApp.CoachController', [])
 .controller('CoachCtrl', ['$scope', '$http', '$window', function ($scope, $http, $window) {

     //Init
     $scope.sortType = 'FirstName'; // set the default sort type
     $scope.sortReverse = false;  // set the default sort order
     $scope.searchCoach = '';     // set the default search/filter term

     $http.get('/Coach/GetAllCoaches').success(function (data) {
         $scope.model = data;
     });

     $scope.new = {
         Coach: {}
     }

     $scope.mode = "Dodaj";
     //Init end

     $scope.selectedRow = null;
     $scope.setClickedRow = function (index, coach) {  //function that sets the value of selectedRow to current index

         if (index == $scope.selectedRow) {
             $scope.mode = "Dodaj";
             $scope.selectedRow = null;

             clearInputFields();
         }
         else {
             $scope.selectedRow = index;

             $scope.new.Coach.Coach_Internal_Id = coach.Coach_Internal_Id;
             $scope.new.Coach.FirstName = coach.FirstName;
             $scope.new.Coach.LastName = coach.LastName;
             $scope.new.Coach.Email = coach.Email;
             $scope.new.Coach.Telephone = coach.Telephone;
             $scope.new.Coach.PayOffRate = coach.PayOffRate;
             $scope.new.Coach.PaymentRate = coach.PaymentRate;

             $scope.mode = "Izmeni"

         }
     }

     $scope.createNewCoach = function () {
         
         if ($scope.mode == "Dodaj") {
             if(!CheckInputFields()) {
                 $scope.new.Coach.Coach_Internal_Id = generateUUID();
                 $http.post('/Coach/CreateCoach', $scope.new.Coach).success(function (status) {


                     $http.get('/Coach/GetAllCoaches').success(function (data) {
                         $scope.model = data;
                     });

                     clearInputFields();
                 });
             }
             else $window.alert("Niste dobro popunili polja!");
         }

         if ($scope.mode == "Izmeni") {
             if (!CheckInputFields()) {
                 $http.post('/Coach/UpdateCoach', $scope.new.Coach).success(function (status) {


                     $http.get('/Coach/GetAllCoaches').success(function (data) {
                         $scope.model = data;
                     });


                 });
             } else $window.alert("Niste dobro popunili polja!");
         }

     }

     $scope.deleteCoach = function (coachId) {
         if ($window.confirm("Da li ste sigurni?")) {
             $http.delete('/Coach/DeleteCoach?id=' + coachId).success(function (status) {

                if (status == "error") $window.alert("Nije moguće brisanje željenog trenera zbog zavisnost sa treninzima!");
                 $http.get('/Coach/GetAllCoaches').success(function (data) {
                     $scope.model = data;

                     $scope.mode = "Dodaj";
                     $scope.selectedRow = null;

                     clearInputFields();
                 });
             });
         }
     }

     function generateUUID() {
         var d = new Date().getTime();
         if (window.performance && typeof window.performance.now === "function") {
             d += performance.now(); //use high-precision timer if available
         }
         var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
             var r = (d + Math.random() * 16) % 16 | 0;
             d = Math.floor(d / 16);
             return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
         });
         return uuid;
     }

     function clearInputFields() {
         $scope.new.Coach.FirstName = null;
         $scope.new.Coach.LastName = null;
         $scope.new.Coach.Email = null;
         $scope.new.Coach.Telephone = null;
         $scope.new.Coach.PayOffRate = null;
         $scope.new.Coach.PaymentRate = null;
     }

     function CheckInputFields() {
         var error = false;

         if ($scope.new.Coach.FirstName == null) error = true;
         if ($scope.new.Coach.LastName == null) error = true;
         if ($scope.new.Coach.Email == null) error = true;
         if ($scope.new.Coach.Telephone == null) error = true;
         if ($scope.new.Coach.PayOffRate == null) error = true;
         if ($scope.new.Coach.PaymentRate == null) error = true;

         return error;

     }

 }]);