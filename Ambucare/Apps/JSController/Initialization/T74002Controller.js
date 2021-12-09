app.controller("T74002Controller", ["$scope", "$rootScope", "$filter", "$http", "$window", "T74002Service", "LabelService", "uiGridConstants", "DTOptionsBuilder", "DTColumnBuilder", "Data",
    function ($scope,$rootScope, $filter, $http, $window, T74002Service, LabelService,uiGridConstants, DTOptionsBuilder, DTColumnBuilder,Data) {
        //for instance
        $scope.obj = {};
        $scope.obj = Data;
        $scope.dtInstance = {};


        // Form Label Data Service Start 
        var labelData = LabelService.getlabeldata('T74002');
        labelData.then(function (data) {
            $scope.entity = data;
        });
        $scope.stest=function(data) {
            alert(JSON.stringify(data.target.id + JSON.parse(sessionStorage.getItem("FCode"))));
        }
        $scope.Save_Click = function () {
            Save_Click();
        };

        function Save_Click() {
            if ($scope.obj.T_LANG1_NAME == undefined || $scope.obj.T_LANG1_NAME == '') {
                //$window.confirm('Local Name is requered');
                alert($scope.getSingleMsg('M0002'));
                $scope.localName.show();
                return;
            }
            if ($scope.obj.T_LANG2_NAME == undefined || $scope.obj.T_LANG2_NAME == '') {
                //$window.confirm('English Name is requered');
                alert($scope.getSingleMsg('M0003'));
                scope.localName.show();
                return;
            }
            if ($scope.obj.T_ITEM_BRA_ID === undefined) {
                var save = T74002Service.insert_t74002($scope.obj);
                save.then(function (data) {
                    //alert("Data saved Succesfully");
                    alert($scope.getSingleMsg('S0012'));
                    $scope.dtInstance.reloadData();
                });
            }
            else {
                var save = T74002Service.insert_t74002($scope.obj);
                save.then(function (data) {
                    //alert("Data updated Succesfully");
                    alert($scope.getSingleMsg('S0003'));
                    $scope.dtInstance.reloadData();
                });
            }
        }

        var disable = $rootScope.$on('T74002Emit', function (event, data) {
            if (data == 'm') {
                Save_Click();
            }
        });
        $scope.$on('$destroy', function () {
            disable();
        });

        $scope.Delete = function () {
            if ($scope.obj.T_ITEM_BRA_ID !== undefined) {
                if ($window.confirm('Are you sure ?')) {
                    var del = T74002Service.delete_t74002($scope.obj);
                    del.then(function (data) {
                        //alert("Data delete Succesfully");
                        alert($scope.getSingleMsg('S0007'));
                        $scope.dtInstance.reloadData();
                    });
                }
                return false;
            }
            else {
                //alert('Please select Item Brand');
                alert($scope.getSingleMsg('S0040'));
                return false;
            }
        };
        $scope.Clear = function () {
            $scope.obj.T_ITEM_BRA_ID = '';
            $scope.obj.T_LANG1_NAME = '';
            $scope.obj.T_LANG2_NAME = '';

            $scope.obj.T_ITEM_BRA_ID = undefined;
        };
        $scope.Print = function() {
            var print = T74002Service.print_t74002();
        };

        //For Grid Function Start

        $scope.dtInstanceCallback = function(instance) {
            $scope.dtInstance = instance;
        }

        $scope.someClickHandler = someClickHandler;
        $scope.dtColumns = [
            DTColumnBuilder.newColumn("T_ITEM_BRA_ID", "Item Brand").withOption('name', 'T_ITEM_BRA_ID'),
            DTColumnBuilder.newColumn("T_LANG2_NAME", "English Name").withOption('name', 'T_LANG2_NAME'),
            DTColumnBuilder.newColumn("T_LANG1_NAME", "Local Name").withOption('name', 'T_LANG1_NAME')
        ];

        $scope.dtOptions = DTOptionsBuilder.newOptions().withOption('ajax', {
            dataSrc: "data",
          url: $scope.vrDir+ "/T74002/GetItemBrandData",
            type: "POST"
        })
            .withOption('rowCallback', rowCallback)
            .withOption('processing', true) //for show progress bar
            .withOption('serverSide', true) // for server side processing
            .withPaginationType('full_numbers') // for get full pagination options // first / last / prev / next and page numbers
            .withDisplayLength(10) // Page size
            .withOption('aaSorting', [0, 'asc']) // for default sorting column // here 0 means first column

        function someClickHandler(info) {
            $scope.obj.T_ITEM_BRA_ID = info.T_ITEM_BRA_ID;
            $scope.obj.T_LANG2_NAME = info.T_LANG2_NAME;
            $scope.obj.T_LANG1_NAME = info.T_LANG1_NAME;
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
    }]);
