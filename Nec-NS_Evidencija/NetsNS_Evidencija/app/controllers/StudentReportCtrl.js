angular.module('MvcApp.StudentReportController', [])
 .controller('StudentReportCtrl', ['$scope', '$http', function ($scope, $http) {

     $scope.day1 = 1;
     $scope.month1 = 1;
     $scope.year1 = 2016;

     $scope.day2 = 1;
     $scope.month2 = 1;
     $scope.year2 = 2016;

     $scope.day3 = 1;
     $scope.month3 = 1;
     $scope.year3 = 2016;

     $scope.day4 = 1;
     $scope.month4 = 1;
     $scope.year4 = 2016;

     $scope.ReportModel1 = {};
     $scope.ReportModel2 = {};


     $http.get('/Student/GetAllPaymentPerHourStudents').success(function (data) {
         $scope.model = data;
         if (data != null) {
             $scope.StudentForReport = data[0].Student_Internal_Id;
             $scope.StudentForReport2 = data[0].Student_Internal_Id;
         }
     });

     $scope.prepareReportModel1 = function () {

         if (!CheckInputFields1()) {

             var date_empty1 = "";
             var date_empty2 = "";
             var temp1 = ""; var temp2 = ""; var temp3 = ""; var temp4 = "";

             if ($scope.month1 <= 9) $scope.month1temp = temp1.concat("0", $scope.month1);
             if ($scope.month2 <= 9) $scope.month2temp = temp2.concat("0", $scope.month2);

             if ($scope.day1 <= 9) $scope.day1temp = temp3.concat("0", $scope.day1);
             if ($scope.day2 <= 9) $scope.day2temp = temp4.concat("0", $scope.day2);

             var student = $scope.model[0];
             for (i = 0; i < $scope.model.length; i++)
                 if ($scope.model[i].Student_Internal_Id == $scope.StudentForReport)
                     student = $scope.model[i];

             var name = "";

             $scope.ReportModel1.Id = student.Student_Internal_Id;
             $scope.ReportModel1.Name = name.concat(student.FirstName.trim(), " ", student.LastName.trim());
             $scope.ReportModel1.FromDate = date_empty1.concat($scope.year1, "-", $scope.month1temp, "-", $scope.day1temp);
             $scope.ReportModel1.ToDate = date_empty2.concat($scope.year2, "-", $scope.month2temp, "-", $scope.day2temp);
         }
     }

     $scope.prepareReportModel2 = function () {

         if (!CheckInputFields2()) {

             var date_empty1 = "";
             var date_empty2 = "";
             var temp1 = ""; var temp2 = ""; var temp3 = ""; var temp4 = "";

             if ($scope.month1 <= 9) $scope.month3temp = temp1.concat("0", $scope.month3);
             if ($scope.month2 <= 9) $scope.month4temp = temp2.concat("0", $scope.month4);

             if ($scope.day1 <= 9) $scope.day3temp = temp3.concat("0", $scope.day3);
             if ($scope.day2 <= 9) $scope.day4temp = temp4.concat("0", $scope.day4);

             var student = $scope.model[0];
             for (i = 0; i < $scope.model.length; i++)
                 if ($scope.model[i].Student_Internal_Id == $scope.StudentForReport2)
                     student = $scope.model[i];
           
             $scope.ReportModel2.Id = student.Student_Internal_Id;
             $scope.ReportModel2.FromDate = date_empty1.concat($scope.year3, "-", $scope.month3temp, "-", $scope.day3temp);
             $scope.ReportModel2.ToDate = date_empty2.concat($scope.year4, "-", $scope.month4temp, "-", $scope.day4temp);
         }
     }

     function CheckInputFields1() {
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

         return error;
     }

     function CheckInputFields2() {
         var error = false;

         if (!angular.isNumber($scope.day3)) error = true;
         if (!angular.isNumber($scope.month3)) error = true;
         if (!angular.isNumber($scope.year3)) error = true;

         if ($scope.day3 < 1 || $scope.day3 > 31) error = true;
         if ($scope.month3 < 1 || $scope.month3 > 12) error = true;
         if ($scope.year3 < 1900 || $scope.year3 > 2900) error = true;

         if (!angular.isNumber($scope.day4)) error = true;
         if (!angular.isNumber($scope.month4)) error = true;
         if (!angular.isNumber($scope.year4)) error = true;

         if ($scope.day4 < 1 || $scope.day4 > 31) error = true;
         if ($scope.month4 < 1 || $scope.month4 > 12) error = true;
         if ($scope.year4 < 1900 || $scope.year4 > 2900) error = true;

         return error;
     }
 }]);