app.controller("T74144Controller", ["$scope", "$rootScope", "$filter", "$http", "$window", "LabelService", "uiGridConstants", "DTOptionsBuilder", "DTColumnBuilder", "Data", "T74144Service",
    function ($scope, $rootScope, $filter, $http, $window, LabelService, uiGridConstants, DTOptionsBuilder, DTColumnBuilder, Data, T74144Service) {

        //For Gried Data
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.A = {};
        $scope.obj.t74101 = {};
        $scope.dtInstance = {};

        $scope.cancelType = [{ CancelType: 1, CancelText: 'Operator' }, { CancelType: 2, CancelText: 'Team' }, { CancelType: 3, CancelText: 'Hospital Cancel' }];
        var Label = T74144Service.getLabel();
        Label.then(function (results) {
            $scope.LabelList = results;
        });
        //For Entry User
        var EntryUser = T74144Service.EntryUser();
        EntryUser.then(function (data) {
            $scope.obj.t74101.T_ENTRY_USER = data;
            $scope.obj.t74101.T_ENTRY_DATE = $filter('date')(new Date(), 'dd-MMM-yyyy');
        });

        // Form Label Data Service Start 
        var labelData = LabelService.getlabeldata('T74144');
        labelData.then(function (data) {
            $scope.entity = data;
        });
        // Form Label Data Service End 

        //For Insert Update Start

        $scope.Save_Click = function() {
            Save_Click();
        };
        function Save_Click() {
        if ($scope.obj.t74101.T_LANG2_NAME !== undefined) {
                if ($scope.obj.t74101.T_CANCEL_REASON_ID != undefined) {
                    var update = T74144Service.InsertOrUpdate($scope.obj.t74101);
                    update.then(function(data) {
                        //alert("Data Update Succesfully");
                        alert($scope.getSingleMsg('S0003'));
                        clear();
                        $scope.dtInstance.reloadData();
                    });
                } else {
                    var save = T74144Service.InsertOrUpdate($scope.obj.t74101);
                    save.then(function(data) {
                        //alert("Data Save Succesfully");
                        alert($scope.getSingleMsg('S0012'));
                        clear();
                        $scope.dtInstance.reloadData();
                    });
                }
            } else {
            // $window.confirm('English Name is requered');
            alert($scope.getSingleMsg('M0003'));
                angular.element('#txtEnglishName').focus();
            }
        };
        var disable = $rootScope.$on('T74144Emit', function (event, data) {
            if (data == 'n') {
                Save_Click();
            } else if (data == 'Clear') {
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
            $scope.obj.t74101.T_CANCEL_REASON_ID = '';
            $scope.obj.t74101.T_LANG1_NAME = '';
            $scope.obj.A = {};
            $scope.obj.t74101.T_LANG2_NAME = undefined;
            $scope.obj.t74101.T_CANCEL_REASON_ID = undefined;
        };

        //For Delete
        $scope.Delete_Click = function() {

            if ($scope.obj.t74101.T_CANCEL_REASON_ID != null) {
                if ($window.confirm('Are you sure to want delete ?')) {
                    var dele = T74144Service.Delete($scope.obj.t74101.T_CANCEL_REASON_ID);
                    dele.then(function(data) {
                        //alert("Data delete Succesfully");
                        alert($scope.getSingleMsg('S0007'));
                        $window.location.reload();

                    });
                }
            } else {
                //alert("Select a data for delete.");
                alert($scope.getSingleMsg('S0011'));
            }
        };

        //For Grid Function Start

        $scope.dtInstanceCallback = function (instance) {
            $scope.dtInstance = instance;
        }

        $scope.someClickHandler = someClickHandler;
        $scope.dtColumns = [
            //here We will add .withOption('name','column_name') for send column name to the server 
            DTColumnBuilder.newColumn("T_CANCEL_REASON_ID", "Id").withOption('name', 'T_CANCEL_REASON_ID').notVisible(),
            DTColumnBuilder.newColumn("T_LANG2_NAME", "English Name").withOption('name', 'T_LANG2_NAME'),
            DTColumnBuilder.newColumn("T_LANG1_NAME", "Local Name").withOption('name', 'T_LANG1_NAME'),
            DTColumnBuilder.newColumn("T_CANCEL_TYPE_ID", "Type").withOption('name', 'T_CANCEL_TYPE_ID').notVisible(),
        ];
        $scope.dtOptions = DTOptionsBuilder.newOptions().withOption('ajax', {
                dataSrc: "data",
          url: $scope.vrDir +"/T74144/GetLabelData",
                type: "POST"
            })
            .withOption('rowCallback', rowCallback)
            .withOption('processing', true) //for show progress bar
            .withOption('serverSide', true) // for server side processing
            .withPaginationType('full_numbers') // for get full pagination options // first / last / prev / next and page numbers
            .withDisplayLength(10) // Page size
            .withOption('aaSorting', [0, 'asc']) // for default sorting column // here 0 means first column
        function someClickHandler(info) {
            $scope.obj.t74101.T_CANCEL_REASON_ID = info.T_CANCEL_REASON_ID;
            $scope.obj.t74101.T_LANG2_NAME = info.T_LANG2_NAME;
            $scope.obj.t74101.T_LANG1_NAME = info.T_LANG1_NAME;
            $scope.obj.t74101.T_CANCEL_TYPE_ID = info.T_CANCEL_TYPE_ID;
            $scope.obj.A.selected =
                {
                    CancelType: info.T_CANCEL_TYPE_ID, CancelText: ddlText(info.T_CANCEL_TYPE_ID)
        };
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
       
        function ddlText(e) {
            var a = $scope.cancelType.filter(function (a) { return a.CancelType == e; });
            return a[0].CancelText;
        }
        //For Grid Function End 
    }

]);