'use strict';

angular.module('DemoApp').provider('ngColorPickerConfig', function () {
    var templateUrl = '/Scripts/ng/directives/UI/ngColorPicker.html';
    var defaultColors = [
		'#CFF7CD',     //PriceModelColorAvailable,
		'#DBDBDB',     //PriceModelColorUnavailable,
		'#FFE169',     //PriceModelColorPending,
		'#FFC069',     //PriceModelColorSoldRescissionPeriod, PriceModelColorSoldWithSubjects,
        '#cef8fa',     //PriceModelColorReserved
        '#dbadff',
        '#5484ed',     // blue
        '#a4bdfc',
        '#46d6db',
        '#7ae7bf',
        '#7bd148',
        '#51b749',
		'#EB635B',     //PriceModelColorSoldFirm
        '#dc2127'
    ];
    this.setTemplateUrl = function (url) {
        templateUrl = url;
        return this;
    };
    this.setDefaultColors = function (colors) {
        defaultColors = colors;
        return this;
    };
    this.$get = function () {
        return {
            templateUrl: templateUrl,
            defaultColors: defaultColors
        }
    }
})
.directive('ngColorPicker', function (ngColorPickerConfig) {

    return {
        templateUrl: ngColorPickerConfig.templateUrl,
        restrict: 'AE', 
        scope: {
            selected: '=',
            customizedColors: '=colors'
        },
        link: function(scope) {
            // set colors Object.
            scope.colors = ngColorPickerConfig.defaultColors || scope.customizedColors;

            // find the color on  defaultColors, if there is not, add to Colors!
            if (scope.selected != null &&  !scope.colors.some(arrVal => arrVal.toLowerCase() === scope.selected.toLowerCase()))
            {
                scope.colors.push(scope.selected);
            }

            // set selected color highlight.
            scope.selected = scope.selected == null ? '#FFFFFF' : scope.selected; 

            scope.pick = function(color) {
                scope.selected = color.toUpperCase();
            };
        }
    }
});

