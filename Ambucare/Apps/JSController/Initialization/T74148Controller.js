app.controller("T74148Controller", ["$scope", "$rootScope", "$filter", "$http","$timeout", "$window", "LabelService","Data", "T74148Service",
    function ($scope, $rootScope, $filter, $http,$timeout, $window, LabelService, Data, T74148Service) {
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj._t74148 = {};
        angular.element('#txtMessageId').focus();

        // Form Label Data Service Start 
        var labelData = LabelService.getlabeldata('T74148');
        labelData.then(function (data) {
            $scope.entity = data;
        });
        getAllData();
        function getAllData() {
            $scope.NewList = {};
            $scope.NewList = [];
            var info = T74148Service.getGridData();
            info.then(function (data) {
                var jsonData = JSON.parse(data);
                for (var i = 0; i < jsonData.length; i++) {
                    $scope.newObj = {};
                    $scope.newObj.T_SPCLTY_ID = jsonData[i].T_SPCLTY_ID;
                    $scope.newObj.T_SPCLTY_CODE = jsonData[i].T_SPCLTY_CODE;
                    $scope.newObj.T_LANG2_NAME = jsonData[i].T_LANG2_NAME;
                    $scope.newObj.T_LANG1_NAME = jsonData[i].T_LANG1_NAME;
                    $scope.newObj.T_MAIN_SPCLTY = jsonData[i].T_MAIN_SPCLTY;
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
            $scope.obj._t74148.T_SPCLTY_ID = D.T_SPCLTY_ID;
            $scope.obj._t74148.T_SPCLTY_CODE = D.T_SPCLTY_CODE;
            $scope.obj._t74148.T_LANG2_NAME = D.T_LANG2_NAME;
            $scope.obj._t74148.T_LANG1_NAME = D.T_LANG1_NAME;
            $scope.obj._t74148.T_MAIN_SPCLTY = D.T_MAIN_SPCLTY;
        };
        // Form Label Data Service End 
       // var EntryUser = T74148Service.EntryUser();
        //EntryUser.then(function (data) {
        //    $scope.obj.T_ENTRY_USER = data;
        //    $scope.obj.T_ENTRY_DATE = $filter('date')(new Date(), 'dd-MMM-yyyy');
        //});
        $scope.Save_Click = function () {
            Save_Click();
        };
        function Save_Click() {
            if ($scope.obj.T_SPCLTY_ID == undefined) {
                var save = T74148Service.InsertOrUpdate($scope.obj._t74148);
                save.then(function (data) {
                    //alert("Data saved Succesfully");
                    alert($scope.getSingleMsg('S0012'));
                    getAllData();
                });
            }
            else {
                var update = T74148Service.InsertOrUpdate($scope.obj._t74148);
                update.then(function (data) {
                    //alert("Data updated Succesfully");
                    alert($scope.getSingleMsg('S0003'));
                    getAllData();
                });
            }
        }
        var disable = $rootScope.$on('T74148Emit', function (event, data) {
            if (data == 'm') {
                Save_Click();
            }
        });

        var clearDisable = $rootScope.$on('T74148ClearEmit',
            function (event, data) {
                if (data === 'd') {
                    Clear();
                }
            });

        $scope.$on('$destroy', function () {
            disable();
            clearDisable();
        });


        $scope.Delete = function () {
            if ($scope.obj.T_SPCLTY_ID !== undefined) {
                if ($window.confirm('Are you sure ?')) {
                    var del = T74148Service.Delete($scope.obj.T_SPCLTY_ID);
                    del.then(function (data) {
                        //alert("Data delete Succesfully");
                        alert($scope.getSingleMsg('S0007'));
                        $window.location.reload();
                    });
                }
                return false;
            }
            else {
                //alert("Select a data for delete.");
                alert($scope.getSingleMsg('S0011'));
                return false;
            }
        };

        $scope.Clear = function () {
            Clear();
        };

        function Clear() {
            $scope.obj._t74148.T_SPCLTY_CODE = '';
            $scope.obj._t74148.T_LANG1_NAME = '';
            $scope.obj._t74148.T_LANG2_NAME = '';
            $scope.obj._t74148.T_MAIN_SPCLTY = '';

            $scope.obj._t74148.T_SPCLTY_ID = undefined;
        }







    //    $scope.someClickHandler = someClickHandler;
    //    $scope.dtColumns = [
    //    ];
    //    $scope.dtOptions = DTOptionsBuilder.newOptions().withOption('ajax',
    //            {
    //                dataSrc: "data",
    //                url: "/T74148/GetGridData",
    //                type: "POST"
    //            })
    //        .withOption('rowCallback', rowCallback)
    //        .withOption('processing', true) //for show progress bar
    //        .withOption('serverSide', true) // for server side processing
    //        .withPaginationType(
    //            'full_numbers') // for get full pagination options // first / last / prev / next and page numbers
    //        .withDisplayLength(10) // Page size
    //        .withOption('aaSorting', [0, 'asc']); // for default sorting column // here 0 means first column

    //    function someClickHandler(info) {
    //        $scope.obj.T_SPCLTY_ID = info.T_SPCLTY_ID;
    //        $scope.obj.T_LANG2_NAME = info.T_LANG2_NAME;
    //        $scope.obj.T_LANG1_NAME = info.T_LANG1_NAME;
    //    }
    //    function rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
    //        $('td', nRow).unbind('click');
    //        $('td', nRow).bind('click', function () {
    //            $scope.$apply(function () {
    //                $scope.someClickHandler(aData);
    //            });
    //        });
    //        return nRow;
    //    }
       
    }]);