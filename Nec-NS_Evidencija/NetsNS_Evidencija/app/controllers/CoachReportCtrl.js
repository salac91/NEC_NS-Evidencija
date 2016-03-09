angular.module('MvcApp.CoachReportController', [])
 .controller('CoachReportCtrl', ['$scope', '$http', function ($scope, $http) {

     $scope.day1 = 1;
     $scope.month1 = 1;
     $scope.year1 = 2016;

     $scope.day2 = 1;
     $scope.month2 = 1;
     $scope.year2 = 2016;

     $scope.ReportModel = {};

     $http.get('/Coach/GetAllCoaches').success(function (data) {
         $scope.model = data;
         if (data != null)
             $scope.CoachForReport = data[0].Coach_Internal_Id;
     });

     $scope.prepareReportModel = function () {
         if (!CheckInputFields()) {
             var date_empty1 = "";
             var date_empty2 = "";
             var temp1 = ""; var temp2 = ""; var temp3 = ""; var temp4 = "";

             if ($scope.month1 <= 9) $scope.month1temp = temp1.concat("0", $scope.month1);
             if ($scope.month2 <= 9) $scope.month2temp = temp2.concat("0", $scope.month2);

             if ($scope.day1 <= 9) $scope.day1temp = temp3.concat("0", $scope.day1);
             if ($scope.day2 <= 9) $scope.day2temp = temp4.concat("0", $scope.day2);
          
             var coach = $scope.model[0];
             for (i = 0; i < $scope.model.length; i++)
                 if ($scope.model[i].Coach_Internal_Id == $scope.CoachForReport)
                     coach = $scope.model[i];
           
             $scope.ReportModel.Id = coach.Coach_Internal_Id;
             $scope.ReportModel.FromDate = date_empty1.concat($scope.year1, "-", $scope.month1temp, "-", $scope.day1temp);
             $scope.ReportModel.ToDate = date_empty2.concat($scope.year2, "-", $scope.month2temp, "-", $scope.day2temp);

         }
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

         return error;
     }
    
 }]);