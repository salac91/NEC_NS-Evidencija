angular.module('MvcApp.StudentDetailsContoller', [])
 .controller('StudentDetailsCtrl', ['$scope', '$http', '$window', '$location', 'Upload', 'sharedProperties', function ($scope, $http, $window, $location, Upload, sharedProperties) {

     //Init
     $scope.day1 = 1;
     $scope.month1 = 1;
     $scope.year1 = 2016;

     $scope.showForm = false;
     $scope.text = "Ažuriraj detalje";
     $scope.imageUrl = "../Content/Images/Unknown-person.gif"

     $scope.StudentDetails = {};
     $scope.StudentDetails = sharedProperties.getProperty();
     if ($scope.StudentDetails == "")
         $location.path("/students");

     var tempDate = $scope.StudentDetails.BornDate.trim();
     var res = tempDate.split(" ");
     $scope.dateToShow = res[0];

     if($scope.StudentDetails.imageUrl != null)
         $scope.StudentDetails.ImageUrl = $scope.imageUrl;
     
     $scope.new = {
         StudentDetailsNew : {}
     }
     //Init end

     $scope.showFormF = function () {
         if ($scope.showForm) {
             $scope.showForm = false;
             $scope.text = "Ažuriraj detalje";
         }
         else {
             $scope.showForm = true;
             $scope.text = "Zatvori formu";
         }
     }

     $scope.updateStudent = function () {
         if (!CheckInputFields()) {
             $scope.new.StudentDetailsNew.Student_Internal_Id = $scope.StudentDetails.Student_Internal_Id;
             $scope.new.StudentDetailsNew.FirstName = $scope.StudentDetails.FirstName;
             $scope.new.StudentDetailsNew.LastName = $scope.StudentDetails.LastName;
             $scope.new.StudentDetailsNew.Email = $scope.StudentDetails.Email;
             $scope.new.StudentDetailsNew.Telephone = $scope.StudentDetails.Telephone;
             $scope.new.StudentDetailsNew.PaymentType = $scope.StudentDetails.PaymentType;
             $scope.new.StudentDetailsNew.Discount = $scope.StudentDetails.Discount;

             if ($scope.new.StudentDetailsNew.Jmbg == null) $scope.new.StudentDetailsNew.Jmbg = $scope.StudentDetails.Jmbg;
             if ($scope.new.StudentDetailsNew.Address == null) $scope.new.StudentDetailsNew.Address = $scope.StudentDetails.Address;
             if ($scope.new.StudentDetailsNew.Country == null) $scope.new.StudentDetailsNew.Country = $scope.StudentDetails.Country;
             if ($scope.new.StudentDetailsNew.ParentFirstName == null) $scope.new.StudentDetailsNew.ParentFirstName = $scope.StudentDetails.ParentFirstName;
             if ($scope.new.StudentDetailsNew.ParentLastName == null) $scope.new.StudentDetailsNew.ParentLastName = $scope.StudentDetails.ParentLastName;
             if ($scope.new.StudentDetailsNew.ParentsMail == null) $scope.new.StudentDetailsNew.ParentsMail = $scope.StudentDetails.ParentsMail;
             if ($scope.new.StudentDetailsNew.ParentsTelephone == null) $scope.new.StudentDetailsNew.ParentsTelephone = $scope.StudentDetails.ParentsTelephone;
             if ($scope.new.StudentDetailsNew.ImageUrl == null) $scope.new.StudentDetailsNew.ImageUrl = $scope.StudentDetails.ImageUrl;
             $scope.new.StudentDetailsNew.ToPay = $scope.StudentDetails.ToPay;

             var date_empty1 = "";
             var temp1 = ""; var temp2 = "";
             if ($scope.month1 <= 9) $scope.month1temp = temp1.concat("0", $scope.month1);
             if ($scope.day1 <= 9) $scope.day1temp = temp2.concat("0", $scope.day1);

             $scope.new.StudentDetailsNew.BornDate = date_empty1.concat($scope.year1, "-", $scope.day1temp, "-", $scope.month1temp);

             UploadFile();

             $http.post('/Student/UpdateStudent', $scope.new.StudentDetailsNew).success(function (status) {
                 $http.get('/Student/GetStudent?studentId=' + $scope.StudentDetails.Student_Internal_Id).success(function (data) {

                     $scope.StudentDetails = data;                  
                    
                     var tempDate = data.BornDate.trim();
                     var resTemp = tempDate.split(" ");
                     $scope.dateToShow = resTemp[0];
                     var res = resTemp[0].split("/");
                     $scope.day1 = Number(res[0]); $scope.month1 = Number(res[1]); $scope.year1 = Number(res[2]);
                     $scope.imageUrl = $scope.StudentDetails.imageUrl;
                 });
             });
         }
         else $window.alert("Datum je pogrešan!");
     }

     //upload image
     $scope.fileList = [];
     $scope.curFile;
     $scope.ImageProperty = {
         file: ''
     }

     $scope.setFile = function (element) {
         $scope.fileList = [];
         // get the files
         var files = element.files;
         for (var i = 0; i < files.length; i++) {
             $scope.ImageProperty.file = files[i];

             $scope.fileList.push($scope.ImageProperty);
             $scope.ImageProperty = {};
             $scope.$apply();

         }
     }

     function UploadFile() {

         for (var i = 0; i < $scope.fileList.length; i++) {

             $scope.new.StudentDetailsNew.ImageUrl = "../Content/Images/" + $scope.fileList[i].file.name; //cuvaj u bazi

             $scope.UploadFileIndividual($scope.fileList[i].file,
                                         $scope.fileList[i].file.name,
                                         $scope.fileList[i].file.type,
                                         $scope.fileList[i].file.size,
                                         i);
         }

     }

     $scope.UploadFileIndividual = function (fileToUpload, name, type, size, index) {
         //Create XMLHttpRequest Object
         var reqObj = new XMLHttpRequest();

         //open the object and set method of call(get/post), url to call, isAsynchronous(true/False)
         reqObj.open("POST", "/ImageUpload/Upload", true);

         //set Content-Type at request header.for file upload it's value must be multipart/form-data
         reqObj.setRequestHeader("Content-Type", "multipart/form-data");

         //Set Other header like file name,size and type
         reqObj.setRequestHeader('X-File-Name', name);
         reqObj.setRequestHeader('X-File-Type', type);
         reqObj.setRequestHeader('X-File-Size', size);

         // send the file
         reqObj.send(fileToUpload);

     }


     //$scope.uploadFile = function (files) {
     //    var deferred = $q.defer();
     //    $upload.upload({
     //        url: "ImageUpload/Upload",
     //        method: "POST",
     //        file: files[0],
     //        data: args
     //    }).progress(function (evt) {
     //        // get upload percentage
     //        console.log('percent: ' + parseInt(100.0 * evt.loaded / evt.total));
     //    }).success(function (data, status, headers, config) {
     //        // file is uploaded successfully
     //        deferred.resolve(data);
     //    }).error(function (data, status, headers, config) {
     //        // file failed to upload
     //        deferred.reject();
     //    });

     //    return deferred.promise;
     //}
   

     function CheckInputFields() {
         var error = false;

         if (!angular.isNumber($scope.day1)) error = true;
         if (!angular.isNumber($scope.month1)) error = true;
         if (!angular.isNumber($scope.year1)) error = true;

         if ($scope.day1 < 1 || $scope.day1 > 31) error = true;
         if ($scope.month1 < 1 || $scope.month1 > 12) error = true;
         if ($scope.year1 < 1900 || $scope.year1 > 2900) error = true;

         return error;
     }
  
 }]);