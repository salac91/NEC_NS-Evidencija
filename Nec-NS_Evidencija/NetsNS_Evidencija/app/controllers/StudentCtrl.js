angular.module('MvcApp.StudentController', [])
 .controller('StudentCtrl', ['$scope', '$http', '$document','$window', 'sharedProperties', function ($scope, $http, $document, $window, sharedProperties) {

     //Init
     $scope.sortType = 'FirstName'; // set the default sort type
     $scope.sortReverse = false;  // set the default sort order
     $scope.searchStudent = '';     // set the default search/filter term

     $http.get('/Student/GetAllStudents').success(function (data) {
         $scope.model = data;
     });

     $scope.new = {
         Student: {}
     }
     
     $scope.mode = "Dodaj";
     //Init end

     $scope.selectedRow = null;
     $scope.setClickedRow = function (index, student) {  //function that sets the value of selectedRow to current index
         if (index == $scope.selectedRow) {
             $scope.mode = "Dodaj";
             $scope.selectedRow = null;

             clearInputFields();
         }
         else {
             $scope.selectedRow = index;
             $scope.new.Student.Student_Internal_Id = student.Student_Internal_Id;
             $scope.new.Student.FirstName = student.FirstName;
             $scope.new.Student.LastName = student.LastName;
             $scope.new.Student.Email = student.Email;
             $scope.new.Student.Telephone = student.Telephone;
             $scope.new.Student.Discount = student.Discount;
             if (student.PaymentType.trim() == "Po satu")
                 $scope.new.Student.PaymentType = "Po satu";
             else
                 $scope.new.Student.PaymentType = "Mesečno";

             $scope.mode = "Izmeni"
         }
     }

     $scope.new.Student.PaymentType = "Mesečno";

     $scope.setStudentForDetails = function (student) {

         clearInputFields();
         
         if (student.Jmbg == null) student.Jmbg = "";
         if (student.Address == null) student.Address = "";
         if (student.BornDate == null) student.BornDate = "";
         if (student.Country == null) student.Country = "";
         if (student.ParentFirstName == null) student.ParentFirstName = "";
         if (student.ParentLastName == null) student.ParentLastName = "";
         if (student.ParentsMail == null) student.ParentsMail = "";
         if (student.ParentsTelephone == null) student.ParentsTelephone = "";

         sharedProperties.setProperty(student);
     }

     $scope.createNewStudent = function () {
   
         if ($scope.mode == "Dodaj") {
             if (!CheckInputFields()) {
                 $scope.new.Student.Student_Internal_Id = generateUUID();

                 $http.post('/Student/CreateStudent', $scope.new.Student).success(function (status) {


                     $http.get('/Student/GetAllStudents').success(function (data) {
                         $scope.model = data;

                         clearInputFields();
                     });


                 });
             }
             else $window.alert("Niste dobro popunili polja!");
         }

         if ($scope.mode == "Izmeni") {
             if (!CheckInputFields()) {
                 $http.post('/Student/UpdateStudent', $scope.new.Student).success(function (status) {


                     $http.get('/Student/GetAllStudents').success(function (data) {
                         $scope.model = data;
                     });


                 });
             }
             else $window.alert("Niste dobro popunili polja!");
         }

     }

     $scope.deleteStudent = function (studentId) {
         if ($window.confirm("Da li ste sigurni?")) {
             $http.delete('/Student/DeleteStudent?id=' + studentId).success(function (data) {
                 $http.get('/Student/GetAllStudents').success(function (data) {
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
         $scope.new.Student.FirstName = null;
         $scope.new.Student.LastName = null;
         $scope.new.Student.Email = null;
         $scope.new.Student.Telephone = null;
         $scope.new.Student.Discount = null;
         $scope.new.Student.PaymentType = "Mesečno";
     }

     function CheckInputFields() {
         var error = false;

         if ($scope.new.Student.FirstName == null) error = true;
         if ($scope.new.Student.LastName == null) error = true;
         if ($scope.new.Student.Email == null) error = true;     
         if ($scope.new.Student.Telephone == null) error = true;
         if ($scope.new.Student.Discount == null) error = true;

         return error;
        
     }

 }]);