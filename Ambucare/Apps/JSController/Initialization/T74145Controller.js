app.controller("T74145Controller", ["$scope", "$rootScope", "$filter", "$http", "$window", "LabelService", "uiGridConstants", "DTOptionsBuilder", "DTColumnBuilder", "Data", "T74145Service",
    function ($scope, $rootScope, $filter, $http, $window, LabelService, uiGridConstants, DTOptionsBuilder, DTColumnBuilder, Data, T74145Service) {

        //For Gried Data
        $scope.obj = {};
        $scope.obj = Data;

        // Form Label Data Service Start 
        var labelData = LabelService.getlabeldata('T74145');
        labelData.then(function (data) {
            $scope.entity = data;
        });
        getAllData();
        function getAllData() {
            var info = T74145Service.getGridData();
            info.then(function (data) {
                $scope.obj.getAllGridData = JSON.parse(data);
                $scope.$apply();
            });

        }
        //For picking up data into text
        $scope.setClickedRow = function (index, D) {

            $scope.selectedRow = index;
            $scope.obj.T_DISCH_ID = D.T_DISCH_ID;
            $scope.obj.T_LANG1_NAME = D.T_LANG1_NAME;
            $scope.obj.T_LANG2_NAME = D.T_LANG2_NAME;
        }
        $scope.$watch('selectedRow', function () {
            console.log('Do Some processing'); //runs the block whenever selectedRow is changed. 
        });
        // Form Label Data Service End 

        //For Insert Update Start

        $scope.Save_Click = function() {
            Save_Click();
        };
        function Save_Click() {
                if ($scope.obj.T_DISCH_ID != null) {
                    var update = T74145Service.Update($scope.obj.T_DISCH_ID, $scope.obj.T_LANG1_NAME, $scope.obj.T_LANG2_NAME );
                    update.then(function (data) {
                       //alert("Data Update Succesfully");
                        alert($scope.getSingleMsg('S0003'));
                        getAllData();
                        clear();
                    });
                } else {
                    var save = T74145Service.Insert($scope.obj.T_LANG1_NAME, $scope.obj.T_LANG2_NAME );
                    save.then(function (data) {
                        //alert("Data Save Succesfully");
                        alert($scope.getSingleMsg('S0012'));
                        getAllData();
                        clear();
                    });
                }
        };

        var disable = $rootScope.$on('T74145Emit', function (event, data) {
            if (data == 'r') {
                Save_Click();
            } else if (data == 'Clear') {
                clear();
            }
        });
        $scope.$on('$destroy', function () {
            disable();
        });

        //For Clear
        function clear() {
            $scope.obj.T_DISCH_ID = '';
            $scope.obj.T_LANG1_NAME = '';
            $scope.obj.T_LANG2_NAME = undefined;

            $scope.obj.T_DISCH_ID = undefined; 
        }

        $scope.Clear = function() {
            clear();
        };

        //For Delete
        $scope.Delete_Click = function () {

            if ($scope.obj.T_DISCH_ID != null) {
                if ($window.confirm('Are you sure to want delete ?')) {
                    var dele = T74145Service.Delete($scope.obj.T_DISCH_ID);
                    dele.then(function (data) {
                       // alert("Data Deleted Succesfully");
                        alert($scope.getSingleMsg('S0007'));
                        $window.location.reload();

                    });
                }
            } else {
                //alert("Select a data for delete.");
                alert($scope.getSingleMsg('S0011'));
            }
        }

    }
]);