app.controller("T74146Controller", ["$scope", "$rootScope", "$filter", "$http", "$window","$timeout", "LabelService", "uiGridConstants", "DTOptionsBuilder", "DTColumnBuilder", "Data", "T74146Service",
    function ($scope, $rootScope, $filter, $http, $window, $timeout, LabelService, uiGridConstants, DTOptionsBuilder, DTColumnBuilder, Data, T74146Service) {

        //For Gried Data
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.F = {};
        $scope.obj.T_FORM_CODE = '';
        angular.element('#txtMessageCode').focus();
        // Form Label Data Service Start 
        var labelData = LabelService.getlabeldata('T74146');
        labelData.then(function (data) {
            $scope.entity = data;
        });
        //For Store DropDown Start 
        var formData = T74146Service.formCodeList();
        formData.then(function (data) {
            $scope.FormCode = JSON.parse(data);
        });

        getAllData();
        function getAllData() {
            $scope.NewList = {};
            $scope.NewList = [];
            var info = T74146Service.getGridData();
            info.then(function (data) {
                var jsonData = JSON.parse(data);
                for (var i = 0; i < jsonData.length; i++) {
                    $scope.newObj = {};
                    $scope.newObj.T_MSG_ID = jsonData[i].T_MSG_ID;
                    $scope.newObj.T_MSG_CODE = jsonData[i].T_MSG_CODE;
                    $scope.newObj.T_LANG1_MSG = jsonData[i].T_LANG1_MSG;
                    $scope.newObj.T_LANG2_MSG = jsonData[i].T_LANG2_MSG;
                    $scope.newObj.T_ACTV_STTS = jsonData[i].T_ACTV_STTS;
                    $scope.newObj.T_FORM_CODE = jsonData[i].T_FORM_CODE;
                    $scope.NewList.push($scope.newObj);
                }
                $scope.obj.getAllGridData = $scope.NewList;
                $timeout(function() {
                    $scope.$apply();
                });

            });

        }
        //For picking up data into text
        $scope.setClickedRow = function (index, D) {

            $scope.selectedRow = index;
            $scope.obj.T_MSG_ID = D.T_MSG_ID;
            $scope.obj.T_MSG_CODE = D.T_MSG_CODE;
            $scope.obj.T_LANG1_MSG = D.T_LANG1_MSG;
            $scope.obj.T_LANG2_MSG = D.T_LANG2_MSG;
            $scope.obj.T_ACTV_STTS = D.T_ACTV_STTS;
            $scope.obj.T_FORM_CODE = D.T_FORM_CODE;

            $scope.obj.F.selected = { T_FORM_CODE: D.T_FORM_CODE, T_FORM_CODE: D.T_FORM_CODE };
        };
        $scope.$watch('selectedRow', function () {
            console.log('Do Some processing'); //runs the block whenever selectedRow is changed. 
        });
        // Form Label Data Service End 


        //For Insert Update Start
        $scope.Save_Click = function() {
            Save_Click();
        };

        function Save_Click() {
            if ($scope.obj.T_MSG_ID == null) {
                     var save = T74146Service.Insert($scope.obj.T_MSG_CODE,
                        $scope.obj.T_LANG1_MSG,
                         $scope.obj.T_LANG2_MSG,
                         $scope.obj.T_FORM_CODE,
                        $scope.obj.T_ACTV_STTS);
                    save.then(function (data) {
                       // alert("Data Save Succesfully");
                        alert($scope.getSingleMsg('S0012'));
                        getAllData();
                        clear();
                    });
                } else {
                var update = T74146Service.Update($scope.obj.T_MSG_ID,
                    $scope.obj.T_MSG_CODE,
                    $scope.obj.T_LANG1_MSG,
                    $scope.obj.T_LANG2_MSG,
                    $scope.obj.T_FORM_CODE,
                    $scope.obj.T_ACTV_STTS);
                update.then(function (data) {
                    //alert("Data Update Succesfully");
                    alert($scope.getSingleMsg('S0003'));
                    //alert($scope.getSingleMsg('R0002'));
                    getAllData();
                    clear();
                });
                }
            
        };

        var disable = $rootScope.$on('T74146Emit', function (event, data) {
            if (data == 'g') {
                Save_Click();
            } else if (data =='Clear') {
                clear();
            }
        });
        $scope.$on('$destroy', function () {
            disable();
        });
        //For Clear
        $scope.Clear = function() {
            clear();
        };
        function clear() {
            $scope.obj.T_MSG_CODE = '';
            $scope.obj.T_MSG_ID = '';
            $scope.obj.T_LANG1_MSG = '';
            $scope.obj.T_LANG2_MSG = '';
            $scope.obj.T_ACTV_STTS = '';
            $scope.obj.F.selected = '';
        };

        //For Delete
        //$scope.Delete_Click = function () {

        //    if ($scope.obj.T_MSG_ID != null) {
        //        if ($window.confirm('Are you sure to want delete ?')) {
        //            var dele = T74146Service.Delete($scope.obj.T_MSG_ID);
        //            dele.then(function (data) {
        //                alert("Data Deleted Succesfully");
        //                $window.location = "";

        //            });
        //        }
        //    } else {
        //        alert("Select a data for delete.");
        //    }
        //};

    }
]);