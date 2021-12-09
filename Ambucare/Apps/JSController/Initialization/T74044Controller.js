app.controller("T74044Controller", ["$scope", "$rootScope", "$filter", "$http", "$window", "T74044Service", "LabelService", "uiGridConstants", "DTOptionsBuilder", "DTColumnBuilder", "Data",
    function ($scope, $rootScope, $filter, $http, $window, T74044Service, LabelService, uiGridConstants, DTOptionsBuilder, DTColumnBuilder, Data) {
        //for instance
        $scope.obj = {};
        $scope.obj = Data;
        $scope.dtInstance = {};
        $scope.obj.T74044 = {};
        $scope.obj.A = {};
        $scope.obj.A.selected = {};
        $scope.obj.R = {};
        $scope.obj.R.selected = {};
        $scope.obj.T = {};
        $scope.obj.T.selected = {};
        $scope.obj.S = {};
        $scope.obj.S.selected = {};
        $scope.obj.Required = true;
        $scope.obj.Disabled = false;

        $scope.StoreTypeList = [{ T_STORE_TYPE_ID: 1, T_STORE_TYPE_NAME: 'Ambulance' }, { T_STORE_TYPE_ID: 2, T_STORE_TYPE_NAME: 'Others' }];
        $scope.obj.A.selected = { T_STORE_TYPE_ID: 1, T_STORE_TYPE_NAME: 'Ambulance' };
        var labelData = LabelService.getlabeldata('T74044');
        labelData.then(function (data) {
            $scope.entity = data;
        });
        var getAmbReg = T74044Service.GetAmbReg();
        getAmbReg.then(function (data) {
            $scope.AmbRegList = data;
        });
        //var getMohTeam = T74044Service.GetMohTeam();
        //getMohTeam.then(function (data) {
        //    $scope.MohTeamList = data;
        //});
        //var getMohStation = T74044Service.GetMohStation();
        //getMohStation.then(function (data) {
        //    $scope.MohStationList = data;
        //});
        $scope.setStoreType = function (e) {
            if (e == 2) {
                $scope.obj.Required = false;
                $scope.obj.Disabled = true;
            }
            if (e == 1) {
                $scope.obj.Required = true;
                $scope.obj.Disabled = false;
            }
        }

        $scope.setAmbReg = function (e) {
            $scope.obj.T74044.T_AMBU_REG_ID = e;
            $scope.obj.T74044.T_AMBU_REG_NUM = $scope.AmbRegList.filter(function (a) { return a.T_AMBU_REG_ID == e })[0].T_AMBU_REG_NUM;
        }
        //$scope.setTeamDesc = function (e) {
        //    $scope.obj.T74044.T_TEAM_CODE = e;
        //    $scope.obj.T74044.TEAM_DESC = $scope.MohTeamList.filter(function (a) { return a.T_TEAM_CODE == e })[0].TEAM_DESC;
        //}
        //$scope.setStationDesc = function (e) {
        //    $scope.obj.T74044.T_STATION_CODE = e;
        //    $scope.obj.T74044.STATION_DESC = $scope.MohStationList.filter(function (a) { return a.T_STATION_CODE == e })[0].STATION_DESC;
        //}
        $scope.Save = function () {
            Save();
        };
        $scope.Clear = function () {
            Clear();
        }
        var disable = $rootScope.$on('T74044Emit', function (event, data) {
            if (data == 'a') {
                Save();
            }
        });
        var disableC = $rootScope.$on('T74044EmitC', function (event, data) {
            if (data == 'a') {
                Clear();
            }
        });
        $scope.$on('$destroy', function () {
            disable();
            disableC();
        });





        function Save() {
            if ($scope.obj.T74044.T_LANG1_NAME == undefined || $scope.obj.T74044.T_LANG1_NAME == '') {
                // $window.confirm('Local Name is requered');
                alert($scope.getSingleMsg('M0002'));
                return;
            }
            if ($scope.obj.T74044.T_LANG2_NAME == undefined || $scope.obj.T74044.T_LANG2_NAME == '') {
                // $window.confirm('English Name is requered');
                alert($scope.getSingleMsg('M0003'));
                return;
            } else {
                var save = T74044Service.AddStore($scope.obj.T74044);
                save.then(function (data) {
                    alert(data);
                    Clear();
                    $scope.dtInstance.reloadData();
                });
            }

        };

        function Clear() {
            $scope.obj.T74044 = {};
            $scope.obj.R = {};
            $scope.obj.R.selected = {};
            $scope.obj.T = {};
            $scope.obj.T.selected = {};
            $scope.obj.S = {};
            $scope.obj.S.selected = {};
            $scope.obj.T74044.T_STORE_ID = undefined;
            $scope.obj.A.selected = { T_STORE_TYPE_ID: 1, T_STORE_TYPE_NAME: 'Ambulance' };
        };
        //For Grid Function Start
        $scope.dtInstanceCallback = function (instance) {
            $scope.dtInstance = instance;
        }
        $scope.someClickHandler = someClickHandler;
        $scope.dtColumns = [
            DTColumnBuilder.newColumn("T_STORE_ID", "Store Id").withOption('name', 'T_STORE_ID').notVisible(),
            DTColumnBuilder.newColumn("T_AREA", "Area").withOption('name', 'T_AREA'),
            DTColumnBuilder.newColumn("T_LANG2_NAME", "English Name").withOption('name', 'T_LANG2_NAME'),
            DTColumnBuilder.newColumn("T_LANG1_NAME", "Local Name").withOption('name', 'T_LANG1_NAME'),
            DTColumnBuilder.newColumn("T_AMBU_REG_NUM", "Amb Reg No").withOption('name', 'T_AMBU_REG_NUM')
            //DTColumnBuilder.newColumn("T_TEAM_CODE", "Team Code").withOption('name', 'T_TEAM_CODE'),
            //DTColumnBuilder.newColumn("TEAM_DESC", "Team Name").withOption('name', 'TEAM_DESC'),
            //DTColumnBuilder.newColumn("T_STATION_CODE", "Station Code").withOption('name', 'T_STATION_CODE'),
            //DTColumnBuilder.newColumn("STATION_DESC", "Station Name").withOption('name', 'STATION_DESC')
        ];
        $scope.dtOptions = DTOptionsBuilder.newOptions().withOption('ajax', {
                dataSrc: "data",
                url: $scope.vrDir + "/T74044/GetStoreData",
                type: "POST"
            })
            .withOption('rowCallback', rowCallback)
            .withOption('processing', true) //for show progress bar
            .withOption('serverSide', true) // for server side processing
            .withPaginationType('full_numbers') // for get full pagination options // first / last / prev / next and page numbers
            .withDisplayLength(10) // Page size
            .withOption('aaSorting', [0, 'asc']) // for default sorting column // here 0 means first column
        function someClickHandler(info) {
            $scope.obj.T74044.T_STORE_ID = info.T_STORE_ID;
            $scope.obj.T74044.T_AREA = info.T_AREA;
            $scope.obj.T74044.T_LANG2_NAME = info.T_LANG2_NAME;
            $scope.obj.T74044.T_LANG1_NAME = info.T_LANG1_NAME;
            $scope.obj.T74044.T_AMBU_REG_ID = info.T_AMBU_REG_ID;
            //$scope.obj.T74044.T_TEAM_CODE = info.T_TEAM_CODE;
            //$scope.obj.T74044.TEAM_DESC = info.TEAM_DESC;
            //$scope.obj.T74044.T_STATION_CODE = info.T_STATION_CODE;
            //$scope.obj.T74044.STATION_DESC = info.STATION_DESC;
            $scope.obj.R.selected = { T_AMBU_REG_ID: info.T_AMBU_REG_ID, T_AMBU_REG_NUM: info.T_AMBU_REG_NUM };
            //$scope.obj.T.selected = { T_TEAM_CODE: info.T_TEAM_CODE, TEAM_DESC: info.TEAM_DESC };
            //$scope.obj.S.selected = { T_STATION_CODE: info.T_STATION_CODE, STATION_DESC: info.STATION_DESC };
        }
        function rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
            // Unbind first in order to avoid any duplicate handler (see https://github.com/l-lin/angular-datatables/issues/87)
            $('td', nRow).unbind('click');
            $('td', nRow).bind('click', function () {
                $scope.$apply(function () {
                    $scope.someClickHandler(aData);
                });
            });
            return nRow;
        }
        //For Grid Function End

        //$scope.stest=function(data) {
        //    alert(JSON.stringify(data.target.id + JSON.parse(sessionStorage.getItem("FCode"))));
        //}



        //$scope.Delete = function () {
        //    if ($scope.obj.T_STORE_ID !== undefined) {
        //        if ($window.confirm('Are you sure ?')) {
        //            var del = T74044Service.Delete_T74044($scope.obj);
        //            del.then(function (data) {
        //                //alert("Data Deleted Succesfully");
        //                alert($scope.getSingleMsg('S0007'));
        //                $scope.dtInstance.reloadData();
        //            });
        //        }
        //        return false;
        //    }
        //    else {
        //        //alert('Please select Item Brand');
        //        alert($scope.getSingleMsg('S0040'));
        //        return false;
        //    }
        //};

    }]);
