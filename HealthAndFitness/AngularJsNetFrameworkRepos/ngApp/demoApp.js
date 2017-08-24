//(function () { })();
//(function () { 
//angular
//    .module('demoApp', [])
//    .controller('PageCtrl', ['$scope', function ($scope) {

//        $scope.name = 'World';

//    }]);
//})();
(function () { 
    angular.module('demoApp', []).controller('appHeaderLte', function ($scope) {
        $scope.names = [
            { name: 'Jani', country: 'Norway' },
            { name: 'Hege', country: 'Sweden' },
            { name: 'Kai', country: 'Denmark' },
            { name: 'Tyoky', country: 'Japan' },
        ];
    });
})();
//(function () {
//    'use strict';

//    angular.module('demoApp',
//        ['ngRoute',
//            'ngCookies',
//            'ngLocale',
//            'ngAnimate',
//            'demoApp.controllers',
//            'demoApp.directives',
//            'demoApp.services',
//            'ui.bootstrap', //ui boostrap must be loaded before ngStrap
//        ])
//        .config(['$routeProvider', function ($routeProvider) {

//            $routeProvider
//                .when('/dashboard', {
//                    controller: 'dashboard_index',
//                    templateUrl: 'ngApp/pages/samples/dashboard/index.html'
//                })
//                //.when('/charts/morris', {
//                //    controller: 'section_index',
//                //    templateUrl: 'ngApp/pages/samples/section/index.html'
//                //})
//                //.when('/fileupload', {
//                //    controller: 'fileupload_index',
//                //    templateUrl: 'ngApp/pages/samples/fileupload/index.html'
//                //})
//                //.when('/blank', {
//                //    controller: 'blank_index',
//                //    templateUrl: 'ngApp/pages/samples/blank/index.html'
//                //})
//                //.when('/fullcalendar', {
//                //    controller: 'fullcalendar_index',
//                //    templateUrl: 'ngApp/pages/samples/fullcalendar/index.html'
//                //})
//                .otherwise({
//                    redirectTo: '/dashboard'
//                });


//        }])
//        .config(['$translateProvider', function ($translateProvider) {

//            $translateProvider.useStaticFilesLoader({
//                prefix: demo.Web.translationsFolder,
//                suffix: '.json'
//            });

//            $translateProvider.preferredLanguage(demo.Settings.defaultLanguage);
//            $translateProvider.fallbackLanguage(demo.Settings.defaultLanguage);

//            $translateProvider.storagePrefix('demo');
//            $translateProvider.storageKey('lang');

//            $translateProvider.useCookieStorage();
//        }]);

//    angular.module('demoApp.services', []);
//    angular.module('demoApp.controllers', []);
//    angular.module('demoApp.directives', []);
//}());