angular.module('MvcApp.TrainingController', [])
 .controller('TrainingCtrl', ['$scope', '$http', '$window', function ($scope, $http, $window) {

     //Init
     $scope.sortType = 'TrainingType'; // set the default sort type
     $scope.sortReverse = false;  // set the default sort order
     $scope.searchTraining = '';     // set the default search/filter term

     $scope.mode = "Dodaj";
     $scope.selectedRow = null;

     $scope.day1 = 1;
     $scope.month1 = 1;
     $scope.year1 = 2016;

     $scope.new = {
         Training: {}
     }

     $scope.new.Training.TrainingType = "1";
     $scope.new.Training.TrainingTheme = "1";
     $scope.button_mode = "Zakaži trening";
    
     $http.get('/Training/GetAllTrainings').success(function (data) {
         $scope.model = data;
         for(i = 0; i<$scope.model.length; i++) {
             var dateShow = "";
             var res = $scope.model[i].StartDate.split("/");
             $scope.model[i].DateShow = dateShow.concat(res[1], "/", res[0], "/", res[2]);
             if ($scope.model[i].TrainingType == "1") $scope.model[i].TrainingTypeShow = "ind.";
             if ($scope.model[i].TrainingType == "2") $scope.model[i].TrainingTypeShow = "dvoje";
             if ($scope.model[i].TrainingType == "3") $scope.model[i].TrainingTypeShow = "troje";
             if ($scope.model[i].TrainingType == "4") $scope.model[i].TrainingTypeShow = "četv.";
         }

         $scope.TrainingTypeShow
     });

     $http.get('/Coach/GetAllCoaches').success(function (data) {
         $scope.coaches = data;
         if (data.length != 0)
             $scope.new.Training.Coach = data[0].Coach_Internal_Id;
     });

     $http.get('/Student/GetAllStudents').success(function (data) {
         $scope.students = data;
         if (data.length != 0)
             $scope.new.StudentId = data[0].Student_Internal_Id;
     });

     $scope.StudentsForTraining = [];
     $scope.textModel = "";
     $scope.new.Coach = {};

     //Init end

     $scope.setClickedRow = function (index,training) {  //function that sets the value of selectedRow to current index
         if (index == $scope.selectedRow) {
             $scope.mode = "Dodaj";
             $scope.button_mode = "Zakaži trening";
             $scope.selectedRow = null;

             clearInputFields();
         }
         else {
             $scope.selectedRow = index;

             $scope.new.Training.Training_Internal_Id = training.Training_Internal_Id;
             $scope.new.Training.TrainingTheme = training.TrainingTheme;
             $scope.new.Training.TrainingType =String(training.TrainingType);
             $scope.new.Training.Duration = training.Duration;
             $scope.new.Training.Notes = training.Notes;
             $scope.new.Training.Coach = training.Coach.Coach_Internal_Id;
             $scope.new.Training.TrainingQuality = training.TrainingQuality;
             $scope.new.Training.Relationship = training.Relationship;

             var str = training.StartDate.trim();
             var res = str.split("/");
             $scope.day1 = Number(res[1]); $scope.month1 = Number(res[0]); $scope.year1 = Number(res[2]);

             $http.get('/Training/GetAllStudentsInTraining?id=' + training.Training_Internal_Id).success(function (data) {
                 $scope.StudentsForTraining = data;
                 $scope.textModel = "";
                 for(i=0; i<data.length; i++)
                     $scope.textModel = $scope.textModel.concat(data[i].FirstName.trim(), " ", data[i].LastName.trim(), "\n");
             });

             $scope.mode = "Izmeni"
             $scope.button_mode = "Izmeni trening";
         }
     }

     $scope.createNewTraining = function () {
         if ($scope.mode == "Dodaj") {
             if (!CheckInputFields()) {
                 $scope.new.Training.Training_Internal_Id = generateUUID();

                 var date_empty1 = "";
                 var temp1 = ""; var temp2 = "";

                 if ($scope.month1 <= 9) $scope.month1temp = temp1.concat("0", $scope.month1);
                 if ($scope.day1 <= 9) $scope.day1temp = temp2.concat("0", $scope.day1);


                 $scope.new.Training.StartDate = date_empty1.concat($scope.year1, "-", $scope.month1temp, "-", $scope.day1temp);

                 var coach = $scope.coaches[0];
                 for (i = 0; i < $scope.coaches.length; i++)
                     if ($scope.coaches[i].Coach_Internal_Id == $scope.new.Training.Coach)
                         coach = $scope.coaches[i];

                 var Indata = { 'Training': $scope.new.Training, 'Coach': coach, 'Students': $scope.StudentsForTraining };

                 $http.post('/Training/CreateTraining', Indata).success(function (status) {

                     $http.get('/Training/GetAllTrainings').success(function (data) {
                         $scope.model = data;
                         for (i = 0; i < $scope.model.length; i++) {
                             var dateShow = "";
                             var res = $scope.model[i].StartDate.split("/");
                             $scope.model[i].DateShow = dateShow.concat(res[1], "/", res[0], "/", res[2]);
                             if ($scope.model[i].TrainingType == "1") $scope.model[i].TrainingTypeShow = "ind.";
                             if ($scope.model[i].TrainingType == "2") $scope.model[i].TrainingTypeShow = "dvoje";
                             if ($scope.model[i].TrainingType == "3") $scope.model[i].TrainingTypeShow = "troje";
                             if ($scope.model[i].TrainingType == "4") $scope.model[i].TrainingTypeShow = "četv.";
                         }
                     });

                     clearInputFields();
                 });
             }
             else $window.alert("Niste dobro popunili polja!");
         }

         if ($scope.mode == "Izmeni") {
             if (!CheckInputFields()) {
                 var date_empty1 = "";
                 var temp1 = ""; var temp2 = "";

                 if ($scope.month1 <= 9) $scope.month1temp = temp1.concat("0", $scope.month1);
                 if ($scope.day1 <= 9) $scope.day1temp = temp2.concat("0", $scope.day1);


                 $scope.new.Training.StartDate = date_empty1.concat($scope.year1, "-", $scope.month1temp, "-", $scope.day1temp);
                 var coach = $scope.coaches[0];
                 for (i = 0; i < $scope.coaches.length; i++)
                     if ($scope.coaches[i].Coach_Internal_Id == $scope.new.Training.Coach)
                         coach = $scope.coaches[i];

                 var Indata = { 'Training': $scope.new.Training, 'Coach': coach, 'Students': $scope.StudentsForTraining };

                 $http.post('/Training/UpdateTraining', Indata).success(function (status) {

                     $http.get('/Training/GetAllTrainings').success(function (data) {
                         $scope.model = data;
                         for (i = 0; i < $scope.model.length; i++) {
                             var dateShow = "";
                             var res = $scope.model[i].StartDate.split("/");
                             $scope.model[i].DateShow = dateShow.concat(res[1], "/", res[0], "/", res[2]);
                             if ($scope.model[i].TrainingType == "1") $scope.model[i].TrainingTypeShow = "ind.";
                             if ($scope.model[i].TrainingType == "2") $scope.model[i].TrainingTypeShow = "dvoje";
                             if ($scope.model[i].TrainingType == "3") $scope.model[i].TrainingTypeShow = "troje";
                             if ($scope.model[i].TrainingType == "4") $scope.model[i].TrainingTypeShow = "četv.";
                         }
                     });

                     clearInputFields();
                 });
             }
             else $window.alert("Niste dobro popunili polja!");
         }

     }

     $scope.addStudentForTraining = function () {
         var students = $scope.students;
         var studentId = $scope.new.StudentId;
         for (i = 0; i < students.length; i++) {
             if (students[i].Student_Internal_Id == studentId) {
                 var student = students[i];
                 var alreadyContains = false;

                 for (j = 0; j < $scope.StudentsForTraining.length; j++) {
                     if ($scope.StudentsForTraining[j].FirstName == student.FirstName && $scope.StudentsForTraining[j].LastName == student.LastName)
                         alreadyContains = true;
                 }
                 if (!alreadyContains) {
                     $scope.StudentsForTraining.push(student);
                     $scope.textModel = $scope.textModel.concat(student.FirstName.trim(), " ", student.LastName.trim(), "\n");
                 }
             }
         }
     }

     $scope.removeStudentsForTraining = function () {
         $scope.textModel = "";
         $scope.StudentsForTraining = [];
     }

     $scope.deleteTraining = function (trainingId) {
         if ($window.confirm("Da li ste sigurni?")) {
             $http.delete('/Training/DeleteTraining?id=' + trainingId).success(function (data) {
                 $http.get('/Training/GetAllTrainings').success(function (data) {
                     $scope.model = data;
                     $scope.mode = "Dodaj";
                     $scope.button_mode = "Zakaži trening";
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

         $scope.new.Training.TrainingType = null;
         $scope.new.Training.TrainingTheme = null;
         $scope.new.Training.TrainingQuality = null;
         $scope.new.Training.Relationship = null;

         $scope.day1 = 1;
         $scope.month1 = 1;
         $scope.year1 = 2016;

         $scope.new.Training.Duration = null;
         $scope.new.Training.Notes = null;
         $scope.new.StudentId = null;

         $scope.textModel = "";
     }

     function CheckInputFields() {
         var error = false;

         if (!angular.isNumber($scope.day1)) error = true;
         if (!angular.isNumber($scope.month1)) error = true;
         if (!angular.isNumber($scope.year1)) error = true;

         if ($scope.day1 < 1 || $scope.day1 > 31) error = true;
         if ($scope.month1 < 1 || $scope.month1 > 12) error = true;
         if ($scope.year1 < 1900 || $scope.year1 > 2900) error = true;

         if ($scope.new.Training.TrainingType == null) error = true;
         if ($scope.new.Training.TrainingTheme == null) error = true;
         if ($scope.new.Training.Duration == null) error = true;
         if ($scope.new.Training.Notes == null) error = true;
         if ($scope.new.Training.TrainingType == null) error = true;
         if ($scope.new.Training.Coach == null) error = true;

         return error;
     }

 }]);