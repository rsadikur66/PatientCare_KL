app.controller("Q74143Controller", ["$scope", "$filter", "$http", "$window", "Q74143Service", "LabelService", "Data", "$interval",
    function ($scope, $filter, $http, $window, Q74143Service, LabelService, Data, $interval) {

        getAllGridData();
      $scope.obj = {};
        $scope.loader(true);
        // Form Label Data Service Start 
        var labelData = LabelService.getlabeldata('T74143');
        labelData.then(function (data) {
            $scope.entity = data;
        });


        //$scope.obj.pInfo = {};
        function getAllGridData() {
            var info = Q74143Service.GetNotificationData();
            info.then(function (data) {
                $scope.obj.getAllGridData = JSON.parse(data);
                $scope.loader(false);
                $scope.$apply();
            });

        }
      $scope.LoggedOut_Click = function ($index, T_USER_ID,dis) {
        $scope.loader(true);
          var con = "";
        if (dis === null) {
            var c = confirm('Patient is available');
             con = c === true ? "1" : "0";
        } else {
            con = "1";
        }
        if (con === "1") {
            //alert(T_USER_ID);
            var save = Q74143Service.update_loggedOut57(T_USER_ID);
            save.then(function(data) {
                //alert("Data update Succesfully");
                alert($scope.getSingleMsg('S0003'));
                getAllGridData();
                $scope.loader(false);
            });
        } else {
          $scope.loader(false);
        }
        };
        setInterval(function () {
            getAllGridData();
            $scope.$apply();
        }, 5000);
        $scope.Dicharged_Click = function ($index, T_REQUEST_ID) {
            //alert(T_REQUEST_ID);
            var save = Q74143Service.update_discharged41(T_REQUEST_ID);
            save.then(function (data) {
                //alert("Data update Succesfully");
                alert($scope.getSingleMsg('S0003'));
                getAllGridData();
            });
        };
    }]);