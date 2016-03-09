angular.module('MvcApp.PaymentPerHourController', [])
 .controller('PaymentPerHourCtrl', ['$scope', '$http', '$window', function ($scope, $http, $window) {

     //Init
     $scope.StudentForPaymentPerHour = {};
     $scope.StudentForPaymentPerHour.ToPay = 0;

     $scope.TrainerForPayOffPerHour = {};
     $scope.TrainerForPayOffPerHour.PayOff = 0;

     $scope.PaidTrainer = 0;
     $scope.StudentPaid = 0;
     $scope.AddToPay = 0;
     $scope.AddToGetPaid = 0;

     $http.get('/Student/GetAllPaymentPerHourStudents').success(function (data) {

         $scope.StudentsPaymentPerHour = data;
         if (data.length != 0) {
             $scope.StudentForPaymentPerHour = data[0].Student_Internal_Id;
             $scope.StudentForPaymentPerHourTemp = data[0];
         }
     });

     $http.get('/Coach/GetAllCoaches').success(function (data) {

         $scope.TrainersForPayOffPerHour = data;
         if (data.length != 0) {
             $scope.TrainerForPayOffPerHour = data[0].Coach_Internal_Id;
             $scope.TrainerForPayOffPerHourTemp = data[0];
         }

     });

     //Init end

     $scope.findStudent = function () {      
       for (i = 0; i < $scope.StudentsPaymentPerHour.length; i++)
         if ($scope.StudentsPaymentPerHour[i].Student_Internal_Id == $scope.StudentForPaymentPerHour)
             $scope.StudentForPaymentPerHourTemp  = $scope.StudentsPaymentPerHour[i];
     }

     $scope.findTrainer = function () {
         for (i = 0; i < $scope.TrainersForPayOffPerHour.length; i++)
             if ($scope.TrainersForPayOffPerHour[i].Coach_Internal_Id == $scope.TrainerForPayOffPerHour)
                 $scope.TrainerForPayOffPerHourTemp = $scope.TrainersForPayOffPerHour[i];
     }

     $scope.payDebt = function () {
         if ($window.confirm("Da li ste sigurni?")) {
             if (!CheckInputFields1()) {
                 
                 $scope.StudentForPaymentPerHourTemp.ToPay = $scope.StudentForPaymentPerHourTemp.ToPay - $scope.StudentPaid + 
                     $scope.AddToPay;
                
                 var student = $scope.StudentsPaymentPerHour[0];
                 for (i = 0; i < $scope.StudentsPaymentPerHour.length; i++)
                     if ($scope.StudentsPaymentPerHour[i].Student_Internal_Id == $scope.StudentForPaymentPerHour)
                         student = $scope.StudentsPaymentPerHour[i];

                 var resTemp = student.BornDate;
                 var date = resTemp.split(" ");
                 var res = date[0].split("/");
                 $scope.day1 = Number(res[0]); $scope.month1 = Number(res[1]); $scope.year1 = Number(res[2]);
                 var temp1 = ""; var temp2 = ""; var date_empty1 = "";
                 if ($scope.month1 <= 9) $scope.month1temp = temp1.concat("0", $scope.month1);
                 if ($scope.day1 <= 9) $scope.day1temp = temp2.concat("0", $scope.day1);
              
                 student.BornDate = date_empty1.concat($scope.year1, "-", $scope.day1temp, "-", $scope.month1temp);

                 $http.post('/Student/UpdateStudent', student).success(function (status) {
                     $scope.StudentPaid = 0;
                     $scope.AddToPay = 0;

                     $http.get('/Student/GetAllPaymentPerHourStudents').success(function (data) {

                         $scope.StudentsPaymentPerHour = data;
                         $scope.StudentForPaymentPerHour = student.Student_Internal_Id;                                             
                       
                     });
                 });
             }
             else $window.alert("Niste dobro popunili polja!");
         }
     }

     $scope.getPaid = function () {
         if ($window.confirm("Da li ste sigurni?")) {
             if (!CheckInputFields2()) {
                 $scope.TrainerForPayOffPerHourTemp.PayOff = $scope.TrainerForPayOffPerHourTemp.PayOff - $scope.PaidTrainer +
                     $scope.AddToGetPaid;

                 var coach = $scope.TrainersForPayOffPerHour[0];
                 for (i = 0; i < $scope.TrainersForPayOffPerHour.length; i++)
                     if ($scope.TrainersForPayOffPerHour[i].Coach_Internal_Id == $scope.TrainerForPayOffPerHour)
                         coach = $scope.TrainersForPayOffPerHour[i];

                 $http.post('/Coach/UpdateCoach', coach).success(function (status) {
                     $scope.PaidTrainer = 0;
                     $scope.AddToGetPaid = 0;

                     $http.get('/Coach/GetAllCoaches').success(function (data) {

                         $scope.TrainersForPayOffPerHour = data;
                         $scope.TrainerForPayOffPerHour = coach.Coach_Internal_Id;
                         
                     });
                 });
             }
             else $window.alert("Niste dobro popunili polja!");
         }
     }

     function CheckInputFields1() {
         var error = false;

         if ($scope.StudentForPaymentPerHour == null) error = true;      
         if (!angular.isNumber($scope.StudentPaid)) error = true;
         if (!angular.isNumber($scope.AddToPay)) error = true;
        
         return error;

     }

     function CheckInputFields2() {
         var error = false;

         if ($scope.TrainerForPayOffPerHour == null) error = true;      
         if (!angular.isNumber($scope.PaidTrainer)) error = true;
         if (!angular.isNumber($scope.AddToGetPaid)) error = true;

         return error;

     }
 }]);