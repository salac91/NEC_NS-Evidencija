angular.module('MvcApp.MontlyPaymentController', [])
 .controller('MontlyPaymentCtrl', ['$scope', '$http', '$window', function ($scope, $http, $window) {

     //Init
     $scope.sortType = 'Student.FirstName'; // set the default sort type
     $scope.sortReverse = false;  // set the default sort order
     $scope.searchStudent = '';     // set the default search/filter term

     $scope.model = [];
     $scope.studentsNew = [];

     $scope.selectedRow = null;
     $scope.mode = "Dodaj";

     $scope.day1 = 1;
     $scope.month1 = 1;
     $scope.year1 = 2016;

     $scope.day2 = 1;
     $scope.month2 = 1;
     $scope.year2 = 2016;

     $scope.StudentForUpdate = null;
     //Init end

     $scope.setClickedRow = function (index, montlyPayment) {  //function that sets the value of selectedRow to current index

         if (index == $scope.selectedRow) {
             $scope.mode = "Dodaj";
             $scope.selectedRow = null;

             clearInputFields();

             $http.get('/Student/GetAllStudentsForMontlyPayment').success(function (data) {

                 $scope.studentsNew = data;

             });

         }
         else {
             $scope.selectedRow = index;

             $scope.StudentForUpdate = montlyPayment.Student;
             $scope.new.MontlyPayment.Amount = montlyPayment.Amount;

             var str = montlyPayment.StartDate.trim();
             var res = str.split("/");
             $scope.day1 = Number(res[0]); $scope.month1 = Number(res[1]); $scope.year1 = Number(res[2]);

             var str2 = montlyPayment.EndDate.trim();;
             var res2 = str2.split("/");
             $scope.day2 = Number(res2[0]); $scope.month2 = Number(res2[1]); $scope.year2 = Number(res2[2]);

             $scope.new.MontlyPayment.MontlyPayment_Internal_Id = montlyPayment.MontlyPayment_Internal_Id;
             $scope.new.StudentForMontlyPayment = montlyPayment.Student.Student_Internal_Id;
             $scope.new.MontlyPayment.StartDate = montlyPayment.StartDate;
             $scope.new.MontlyPayment.EndDate = montlyPayment.EndDate;

             $scope.mode = "Izmeni"
         }
     }

     $http.get('/MontlyPayment/GetAllMontlyPayments').success(function (data) {
         $scope.model = data;
     });

     $http.get('/Student/GetAllStudentsForMontlyPayment').success(function (data) {

         $scope.studentsNew = data;
         if (data.length != 0) 
             $scope.new.StudentForMontlyPayment = data[0].Student_Internal_Id;
            
       
     });

     $scope.new = {
         MontlyPayment: {}
     }

     $scope.new.MontlyPaymentModel = {
         MontlyPayment: {},
         Student: {}
     }

     $scope.createNewMontlyPayment = function () {
         var date_empty1 = "";
         var date_empty2 = "";
         var temp1 = ""; var temp2 = ""; var temp3 = ""; var temp4 = "";

         if ($scope.mode == "Dodaj") {
             if (!CheckInputFields()) {

                 $scope.new.MontlyPayment.MontlyPayment_Internal_Id = generateUUID();

                 if ($scope.month1 <= 9) $scope.month1temp = temp1.concat("0", $scope.month1);
                 if ($scope.month2 <= 9) $scope.month2temp = temp2.concat("0", $scope.month2);

                 if ($scope.day1 <= 9) $scope.day1temp = temp3.concat("0", $scope.day1);
                 if ($scope.day2 <= 9) $scope.day2temp = temp4.concat("0", $scope.day2);

                 $scope.new.MontlyPayment.StartDate = date_empty1.concat($scope.year1, "-", $scope.day1temp, "-", $scope.month1temp);
                 $scope.new.MontlyPayment.EndDate = date_empty2.concat($scope.year2, "-", $scope.day2temp, "-", $scope.month2temp);

                 var student = $scope.studentsNew[0];
                 for (i = 0; i < $scope.studentsNew.lenght; i++)
                     if ($scope.studentsNew[i].Student_Internal_Id == $scope.new.StudentForMontlyPayment)
                         student = $scope.studentsNew[i];

                 var Indata = { 'MontlyPayment': $scope.new.MontlyPayment, 'Student': student };

                 $http.post('/MontlyPayment/CreateMontlyPayment', Indata).success(function (status) {

                     clearInputFields();

                     $http.get('/MontlyPayment/GetAllMontlyPayments').success(function (data) {
                         $scope.model = data;
                     });

                     $http.get('/Student/GetAllStudentsForMontlyPayment').success(function (data) {

                         $scope.studentsNew = data;

                     });


                 });
             }
             else $window.alert("Niste dobro popunili polja!");
         }

         if ($scope.mode == "Izmeni") {
             if (!CheckInputFields()) {
                 if ($scope.month1 <= 9) $scope.month1temp = temp1.concat("0", $scope.month1);
                 if ($scope.month2 <= 9) $scope.month2temp = temp2.concat("0", $scope.month2);

                 if ($scope.day1 <= 9) $scope.day1temp = temp3.concat("0", $scope.day1);
                 if ($scope.day2 <= 9) $scope.day2temp = temp4.concat("0", $scope.day2);

                 $scope.new.MontlyPayment.StartDate = date_empty1.concat($scope.year1, "-", $scope.day1temp, "-", $scope.month1temp);
                 $scope.new.MontlyPayment.EndDate = date_empty2.concat($scope.year2, "-", $scope.day2temp, "-", $scope.month2temp);

                 var Indata = { 'MontlyPayment': $scope.new.MontlyPayment, 'Student': $scope.StudentForUpdate };

                 $http.post('/MontlyPayment/UpdateMontlyPayment', Indata).success(function (status) {

                     $http.get('/MontlyPayment/GetAllMontlyPayments').success(function (data) {
                         $scope.model = data;
                     });

                     $scope.new.MontlyPayment.StartDate = null;
                     $scope.new.MontlyPayment.EndDate = null;

                 });
             } else $window.alert("Niste dobro popunili polja!");
         }

     }

     $scope.deleteMontlyPayment = function (montlyPaymentInternalId) {
         if ($window.confirm("Da li ste sigurni?")) {
             $http.delete('/MontlyPayment/DeleteMontlyPayment?id=' + montlyPaymentInternalId).success(function (data) {
                 $http.get('/MontlyPayment/GetAllMontlyPayments').success(function (data) {
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
         $scope.new.StudentForMontlyPayment = null;
         $scope.new.MontlyPayment.Amount = null;
         $scope.new.MontlyPayment.StartDate = null;
         $scope.new.MontlyPayment.EndDate = null;

         $scope.day1 = 1;
         $scope.month1 = 1;
         $scope.year1 = 2016;

         $scope.day2 = 1;
         $scope.month2 = 1;
         $scope.year2 = 2016;
     }

     function CheckInputFields() {

         var error = false;

         if (!angular.isNumber($scope.day1)) error = true;
         if (!angular.isNumber($scope.month1)) error = true;
         if (!angular.isNumber($scope.year1)) error = true;

         if ($scope.day1 < 1 || $scope.day1 > 31) error = true;
         if ($scope.month1 < 1 || $scope.month1 > 12) error = true;
         if ($scope.year1 < 1900 || $scope.year1 > 2900) error = true;

         if (!angular.isNumber($scope.day2)) error = true;
         if (!angular.isNumber($scope.month2)) error = true;
         if (!angular.isNumber($scope.year2)) error = true;

         if ($scope.day2 < 1 || $scope.day2 > 31) error = true;
         if ($scope.month2 < 1 || $scope.month2 > 12) error = true;
         if ($scope.year2 < 1900 || $scope.year2 > 2900) error = true;

         if ($scope.new.MontlyPayment.Amount == null) error = true;
         if ($scope.new.StudentForMontlyPayment == null) error = true;

         return error;
     }
 }]);