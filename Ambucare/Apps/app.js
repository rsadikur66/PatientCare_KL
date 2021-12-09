var app = angular.module('Ambucare', ['ui.grid.selection', 'ui.grid', 'ui.grid.pagination', 'ui.grid.exporter', 'datatables', 'angularUtils.directives.dirPagination', 'ngSanitize', 'ui.select', 'angularModalService', 'ngMap', 'gm', 'jkAngularRatingStars', 'akFileUploader', 'flash', 'ngRoute', 'naif.base64']);

app.run(function ($rootScope, $templateCache) {
	$rootScope.$on('$routeChangeStart', function (event, next, current) {
		if (typeof (current) !== 'undefined') {
			$templateCache.remove(current.templateUrl);
		}
	});
});
app.factory('Data', function () {
    return { obj: '' };
});
app.factory("vrtlDirr", ['$location',
    function ($location) {
        //alert($location.$$absUrl);
        //var k = 'http://localhost:5871/ambucare/';
        var k = '';
        var lastChar = $location.$$absUrl[$location.$$absUrl.length - 1];
        if (lastChar=='/') {
            k = $location.$$absUrl.slice(0, -1);
        } else {
            k = $location.$$absUrl;
            //sadik
            if (k.includes("http://localhost:5871") && k.includes("ReturnUrl=%2F")) {
                k = $location.$$absUrl.slice(0, 21);
            } else {
                var appName = $location.$$absUrl.split('/')[3];
                k = $location.$$protocol + "://" + $location.$$host + "/" + appName;
                
            }
            //sadik
        }


        
        var ssn = sessionStorage.getItem("vrtlDrr");
        var orUrl = sessionStorage.getItem("orUrl");
        var vrtlDrr = '';
        var a =k.split("/").length - 1;
        if ((ssn == null || ssn == undefined) || (orUrl == null || orUrl == undefined)) {
            if (k != 'http://localhost:5871') {
                if (k.includes("http://localhost:5871") && k.includes("ReturnUrl=%2F")) {
                    vrtlDrr = k.split('/')[1];
                } else {
                    vrtlDrr = '/' + k.split('/')[3];
                }
                sessionStorage.setItem("vrtlDrr", vrtlDrr);
            } else {
                vrtlDrr = '';
                sessionStorage.setItem("vrtlDrr", vrtlDrr);
            } 
            sessionStorage.setItem("orUrl",k);
        } else {
            vrtlDrr = ssn;
        }
        return vrtlDrr;
    }]);
app.config(['$provide', function ($provide) {
    $provide.decorator('$controller', ['$delegate', function ($delegate) {
        return function (constructor, locals, later, indent) {
            //&& !locals.$scope.controllerName
            if (typeof constructor === 'string' ) {
                locals.$scope.controllerName = constructor;
            }
            return $delegate(constructor, locals, later, indent);
        };
    }])
}]);
app.factory('$exceptionHandler', ['$injector', function ($injector) {
    return function (exception, cause) {
        try {
            var $http = $injector.get('$http');
            var $log = $injector.get('$log');
            var $location = $injector.get('$location');
            var a = $location.$$absUrl;
            if (exception.stack != undefined) {
                var vir = a.search('5871') < 0 ? '/' + a.split('/')[3] : '';
                var message = exception.stack.split('at')[0].trim();
                var source = exception.stack.split('at')[1].trim();
                $http.post(vir + '/Accounts/setClientErrorLog', { controller: '', action: '', message: message,source: source});
            }
            //$log.error.apply($log, arguments);
        } catch (e) {
            console.log(e.message);
        }
    };
}]);




