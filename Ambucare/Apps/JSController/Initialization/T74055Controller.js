
app.controller("T74055Controller", ["$scope", "$rootScope", "$filter", "$http", "$window", "uiGridConstants", "DTOptionsBuilder", "DTColumnBuilder", "Data", "T74055Service","LabelService",
    function ($scope, $rootScope, $filter, $http, $window, uiGridConstants, DTOptionsBuilder, DTColumnBuilder, Data, T74055Service, LabelService) {
        $scope.obj = {};
        $scope.obj = Data;
        $scope.dtInstance = {};

        // Form Label Data Service Start 
        var labelData = LabelService.getlabeldata('T74055');
        labelData.then(function (data) {
            $scope.entity = data;
        });
        // Form Label Data Service End 
        var EntryUser = T74055Service.EntryUser();
        EntryUser.then(function (data) {
            $scope.obj.T_ENTRY_USER = data;
            $scope.obj.T_ENTRY_DATE = $filter('date')(new Date(), 'dd-MMM-yyyy');
        });

        $scope.Save_Click = function() {
            Save_Click();
        };
        function Save_Click() {
            if ($scope.obj.T_LANG1_NAME == undefined || $scope.obj.T_LANG1_NAME == '') {
                // $window.confirm('Local Name is requered');
                alert($scope.getSingleMsg('M0002'));
                $scope.localName.show();
                return;
            }
            if ($scope.obj.T_LANG2_NAME == undefined || $scope.obj.T_LANG2_NAME == '') {
                // $window.confirm('English Name is requered');
                alert($scope.getSingleMsg('M0003'));
                $scope.localName.show();
                return;
            }
        if ($scope.obj.T_CH_COMP == undefined) {
                var save = T74055Service.Insert($scope.obj);
                save.then(function (data) {
                    //alert("Data saved Succesfully");
                   alert($scope.getSingleMsg('S0012'));
                    Clear();
                    $scope.dtInstance.reloadData();
                });
            }
            else {
                var save = T74055Service.Insert($scope.obj);
                save.then(function (data) {
                    //alert("Data updated Succesfully");
                    alert($scope.getSingleMsg('S0003'));
                    Clear();
                    $scope.dtInstance.reloadData();
                });
            }
        }

        var disable = $rootScope.$on('T74055Emit', function (event, data) {
            if (data === 'g') {
                Save_Click();
            }
        });

        var clearDisable = $rootScope.$on('ClearEmit',
            function(event, data) {
                if (data === 'c') {
                    Clear();
                }
            });
        

        $scope.$on('$destroy', function () {
            disable();
            clearDisable();
        });


        $scope.Delete = function () {
            if ($scope.obj.T_CH_COMP !== undefined) {
                if ($window.confirm('Are you sure ?')) {
                    var del = T74055Service.Delete($scope.obj.T_CH_COMP);
                    del.then(function (data) {
                        //alert("Data deleted Succesfully");
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

        $scope.Clear = function() {
            Clear();
        }

        function Clear() {
            $scope.obj.T_CH_COMP = '';
            $scope.obj.T_LANG1_NAME = '';
            $scope.obj.T_LANG2_NAME = '';

            $scope.obj.T_CH_COMP = undefined;
        };


        $scope.dtInstanceCallback = function(instance) {
            $scope.dtInstance = instance;
        }

        $scope.someClickHandler = someClickHandler;
        $scope.dtColumns = [
            DTColumnBuilder.newColumn("T_LANG2_NAME", "English Name").withOption('name', 'T_LANG2_NAME'),
            DTColumnBuilder.newColumn("T_LANG1_NAME", "Local Name").withOption('name', 'T_LANG1_NAME')
        ];

        $scope.dtOptions = DTOptionsBuilder.newOptions().withOption('ajax',
                {
                    dataSrc: "data",
                  url: $scope.vrDir+"/T74055/GetGridData",
                    type: "POST"
                })
            .withOption('rowCallback', rowCallback)
            .withOption('processing', true) //for show progress bar
            .withOption('serverSide', true) // for server side processing
            .withPaginationType(
                'full_numbers') // for get full pagination options // first / last / prev / next and page numbers
            .withDisplayLength(10) // Page size
            .withOption('aaSorting', [0, 'asc']); // for default sorting column // here 0 means first column

        function someClickHandler(info) {
            $scope.obj.T_CH_COMP = info.T_CH_COMP;
            $scope.obj.T_LANG2_NAME = info.T_LANG2_NAME;
            $scope.obj.T_LANG1_NAME = info.T_LANG1_NAME;
        }
        function rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
            $('td', nRow).unbind('click');
            $('td', nRow).bind('click', function () {
                $scope.$apply(function () {
                    $scope.someClickHandler(aData);
                });
            });
            return nRow;
        }
}]);