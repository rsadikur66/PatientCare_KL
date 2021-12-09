app.controller('T74151Controller', ["$scope", "$filter", "$http", "$window", "LabelService", "T74151Service", "Data",
    function ($scope, $filter, $http, $window, LabelService, T74151Service, Data) {

        $scope.obj = {};
        $scope.obj = Data;

        function clear() {
            $scope.obj.T_RESP = '';
            $scope.obj.T_RESP_LOWER = '';
            $scope.obj.T_RESP_UPPER = '';
        }

        var labelData = LabelService.getlabeldata('T74151');
        labelData.then(function (data) {
            $scope.entity = data;
        });

        var info = T74151Service.getRespData();
        info.then(function (data) {
            $scope.obj.getAllRespData = JSON.parse(data);
        });


        $scope.SaveClick = function () {
            var data = T74151Service.InsertResp($scope.obj);
            data.then(function (d) {
                var res = JSON.parse(d);
                alert(res);
                clear();
                $window.location = "";
            });

        }

        $scope.setClickedRow = function (index, D) {
            $scope.selectedRow = index;
            $scope.obj.T_RESP = D.T_RESP;
            $scope.obj.T_RESP_LOWER = D.T_RESP_LOWER;
            $scope.obj.T_RESP_UPPER = D.T_RESP_UPPER;

        }

        $scope.Clear = function() {
            clear();
        }

    }]);