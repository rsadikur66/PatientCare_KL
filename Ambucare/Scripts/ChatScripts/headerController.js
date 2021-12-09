(function () {
    'use strict'
    app.controller("headerController", function ($scope, $rootScope, $filter) {
        $rootScope.RootPrivateMessages = [];
        $scope.groups = [];
        $rootScope.updateMessage = function ()
            {          
                $scope.groups = _.groupBy($rootScope.RootPrivateMessages, "from");
                $scope.messageCount = $rootScope.RootPrivateMessages.length;             
            }        
    });
})();