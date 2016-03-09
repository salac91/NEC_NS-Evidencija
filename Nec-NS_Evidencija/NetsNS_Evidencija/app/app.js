var app = angular.module('app', ['ngRoute', 'ngAnimate', 'ngFileUpload', 'ui.bootstrap', 'MvcApp.HomeController', 'MvcApp.LoginController', 'MvcApp.RegisterController', 'MvcApp.StudentController',
'MvcApp.CoachController', 'MvcApp.TrainingController', 'MvcApp.MontlyPaymentController', 'MvcApp.StudentDetailsContoller', 'MvcApp.CoachReportController', 'MvcApp.PaymentPerHourController',
'MvcApp.StudentReportController']);
app.config(function ($routeProvider, $locationProvider) {
    $routeProvider
        .when('/', {
            redirectTo: function () {
                return '/login';
            }
        })
        .when('/login', { templateUrl: 'Template/Login.html', controller: 'LoginCtrl' })
        .when('/register', { templateUrl: 'Template/Register.html', controller: 'RegisterCtrl' })       
        .when('/students', { templateUrl: 'Template/Students.html', controller: 'StudentCtrl' })
        .when('/coaches', { templateUrl: 'Template/Coaches.html', controller: 'CoachCtrl' })
        .when('/trainings', { templateUrl: 'Template/Trainings.html', controller: 'TrainingCtrl' })
        .when('/montlypayments', { templateUrl: 'Template/MontlyPayments.html', controller: 'MontlyPaymentCtrl' })
        .when('/studentDetails', { templateUrl: 'Template/StudentDetails.html', controller: 'StudentDetailsCtrl' })
         .when('/coachReport', { templateUrl: 'CoachReport/CoachReport' })
        .when('/studentReport', { templateUrl: 'StudentReport/StudentReport' })
        .when('/paymentPerHour', { templateUrl: 'Template/PaymentPerHour.html', controller: 'PaymentPerHourCtrl' })
        .otherwise({ redirectTo: '/login' })

    $locationProvider.html5Mode(false).hashPrefix('!');

});

app.service('sharedProperties', function () {
    var property = "";

    return {
        getProperty: function () {
            return property;
        },
        setProperty: function (value) {
            property = value;
        }
    };
});

