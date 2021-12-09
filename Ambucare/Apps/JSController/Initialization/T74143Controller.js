app.controller("T74143Controller", ["$scope", "$filter", "$http", "$window", "T74143Service", "LabelService", "Data", "$interval",
    function ($scope, $filter, $http, $window, T74143Service, LabelService, Data, $interval) {

        getAllGridData();
        $scope.obj = {};
        // Form Label Data Service Start 
        var labelData = LabelService.getlabeldata('T74143');
        labelData.then(function (data) {
            $scope.entity = data;
        });


        //$scope.obj.pInfo = {};
        function getAllGridData() {
            var info = T74143Service.GetNotificationData();
            info.then(function (data) {
                $scope.obj.getAllGridData = JSON.parse(data);

                $scope.$apply();
            });

        }
        $scope.LoggedOut_Click = function ($index, T_USER_ID) {
            //alert(T_USER_ID);
            var save = T74143Service.update_loggedOut57(T_USER_ID);
            save.then(function (data) {
                //alert("Data update Succesfully");
                alert($scope.getSingleMsg('S0003'));
                getAllGridData();
            });

        };
        setInterval(function () {
            getAllGridData();
            scope.$apply();
        }, 5000);
        $scope.Dicharged_Click = function ($index, T_REQUEST_ID) {
            //alert(T_REQUEST_ID);
            var save = T74143Service.update_discharged41(T_REQUEST_ID);
            save.then(function (data) {
                //alert("Data update Succesfully");
                alert($scope.getSingleMsg('S0003'));
                getAllGridData();
            });
        };
    }]);

