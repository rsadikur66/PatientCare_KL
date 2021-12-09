app.controller("Q74145Controller", ["$scope", "$filter", "$http", "$window", "Q74145Service", "LabelService", "Data", "$interval",
    function ($scope, $filter, $http, $window, Q74145Service, LabelService, Data, $interval) {

        $scope.obj = {};
        //getAllGridData();
        today();
        // Form Label Data Service Start 
        var labelData = LabelService.getlabeldata('Q74145');
        labelData.then(function (data) {
            $scope.entity = data;
        });


        //$scope.obj.pInfo = {};
        
        //function getAllGridData() {
        //    var info = Q74145Service.GetAllAcceptRequest();
        //    info.then(function (data) {
        //        $scope.obj.getAllAcceptRequestData = JSON.parse(data);
        //       // $scope.$apply();
        //    });

        //}

        //$scope.goTo = function() {
        //    currentDate();
        //};
        $scope.getData = function getData() {
            var dateFrom = moment($scope.obj.T_FROM_DATE).format('DD-MMM-YY');
            var dateTo = moment($scope.obj.T_TO_DATE).format('DD-MMM-YY');
           
            var info = Q74145Service.GetAllAcceptRequest(dateFrom,dateTo);
            info.then(function (data) {
                $scope.obj.getAllAcceptRequestData = JSON.parse(data);
               
            });
        };
        $scope.getData();
        function currentDate() {
            $scope.date = new Date();
            alert($scope.date);
        }
        function today() {
            $scope.obj.T_FROM_DATE = moment().format('DD-MMM-YY');
            $scope.obj.T_TO_DATE = moment().format('DD-MMM-YY');
        }

        $scope.setClickedRow = function(index, d) {
            $scope.$broadcast('kjk', { message: d.FIRSTNAME });
            window.location = $scope.vrDir + "/Transaction/T74046?req=" + d.T_REQUEST_ID;

        };
        $scope.Disc_Report = function (index, d) {
            $window.open("../R74125/GetAllReportData?T_REQUEST_ID=" + d.T_REQUEST_ID, "popup", "width=600,height=600,left=50,top=50");
        }

    }]);