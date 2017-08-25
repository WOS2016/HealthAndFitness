////https://stackoverflow.com/questions/31638885/anchorscroll-directive-not-working-in-ie-11
//// https://www.npmjs.com/package/angular-backtop
angular.module('demoApp').directive('ngBackTop', function ($anchorScroll, $location, $window, $timeout) {
    'use strict';
    var scrollToController = function () {
        var vm = this;
        vm.scrollTo = function (x) {
            $timeout(function() {
                    var newHash = x;
                    if ($location.hash() !== newHash) {
                        $location.hash(x);
                    } else {
                        $anchorScroll();
                    }
                },
                vm.speed);
        };
    }

    return {
        restrict: 'EA',
        bindToController: true,
        controller: scrollToController,
        controllerAs: 'vm',
        transclude: true,
        //replace: true,
        scope: {
            text: '@buttonText',
            speed: '@scrollSpeed',
            theme: '@buttonTheme'
        },
        link: function (scope, element, attrs) {
            scope.text = scope.text || scope.vm.text || 'Top'; //'Scroll top';
            scope.theme = scope.theme || scope.vm.theme || 'light';
            scope.speed = parseInt(scope.speed, 10) || parseInt(scope.vm.speed, 10) || 300;

            angular.element($window).bind('scroll', function () {
                if (this.pageYOffset > 100) {
                    scope.display = true;
                } else {
                    scope.display = false;
                }
                scope.$apply();
            });

        },
        templateUrl: '/ngApp/directives/commonUI/ngBackTop.html'

    }
});


