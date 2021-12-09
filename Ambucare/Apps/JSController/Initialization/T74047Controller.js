

app.controller("T74047Controller", ["$scope", "$rootScope", "$filter", "$http", "$window", "LabelService", "uiGridConstants", "DTOptionsBuilder", "DTColumnBuilder", "Data", "T74047Service",
    function ($scope, $rootScope, $filter, $http, $window, LabelService, uiGridConstants, DTOptionsBuilder, DTColumnBuilder, Data, T74047Service) {

        //For Gried Data
        $scope.obj = {};
        $scope.obj = Data;
        $scope.dtInstance = {};

        var Label = T74047Service.getLabel();
        Label.then(function (results) {
            $scope.LabelList = results;
        });
        // Form Label Data Service Start 
        var labelData = LabelService.getlabeldata('T74055');
        labelData.then(function (data) {
            $scope.entity = data;
        });
        // Form Label Data Service End 
        //For Entry User
        var EntryUser = T74047Service.EntryUser();
        EntryUser.then(function (data) {
            $scope.obj.T_ENTRY_USER = data;
            $scope.obj.T_ENTRY_DATE = $filter('date')(new Date(), 'dd-MMM-yyyy');
        });

        // Form Label Data Service Start 
        var labelData = LabelService.getlabeldata('T74047');
        labelData.then(function (data) {
            $scope.entity = data;
        });
        // Form Label Data Service End 

        //For Insert Update Start
        $scope.Save_Click = function () {
            Save_Click();
        };

        function Save_Click() {
            if ($scope.obj.T_LANG2_NAME !== undefined) {
                if ($scope.obj.T_AMBU_TYPE_ID != undefined) {

                    var update = T74047Service.InsertOrUpdate($scope.obj);
                    update.then(function (data) {
                        //alert("Data updated Succesfully");
                        alert($scope.getSingleMsg('S0003'));
                        $scope.dtInstance.reloadData();

                    });
                } else {
                    var save = T74047Service.InsertOrUpdate($scope.obj);

                    save.then(function (data) {
                        //alert("Data Save Succesfully");
                        alert($scope.getSingleMsg('S0012'));
                        $scope.dtInstance.reloadData();

                    });
                }
            } else {
                //alert('Engilsh Name is Required');
                alert($scope.getSingleMsg('M0003'));
                angular.element('#txtEnglishName').focus();
            }
        }
        var disable = $rootScope.$on('T74047Emit', function (event, data) {
            if (data == 'n') {
                Save_Click();
            }
        });
        $scope.$on('$destroy', function () {
            disable();
        });

        //For Clear
        $scope.Clear = function () {
            $scope.obj.T_AMBU_TYPE_ID = '';
            $scope.obj.T_LANG1_NAME = '';
            $scope.obj.T_LANG2_NAME = undefined;

            $scope.obj.T_AMBU_TYPE_ID = undefined;
        }

        //For Delete
        $scope.Delete_Click = function () {

            if ($scope.obj.T_AMBU_TYPE_ID != null) {
                if ($window.confirm('Are you sure to want delete ?')) {
                    var dele = T74047Service.deleteData($scope.obj.T_AMBU_TYPE_ID);
                    dele.then(function (data) {
                       // alert("Data Deleted Succesfully");
                        alert($scope.getSingleMsg('S0007'));
                        $scope.dtInstance.reloadData();

                    });
                }
            } else {
                //alert("Select a data for delete.");
                alert($scope.getSingleMsg('S0011'));
            }
        }

        //For Grid Function Start 

        $scope.dtInstanceCallback = function(instance) {
            $scope.dtInstance = instance;
        }

        $scope.someClickHandler = someClickHandler;
        $scope.dtColumns = [
            //here We will add .withOption('name','column_name') for send column name to the server 
            DTColumnBuilder.newColumn("RowNumber", "Id").withOption('name', 'T_AMBU_TYPE_ID'),
            DTColumnBuilder.newColumn("T_LANG2_NAME", "English Name").withOption('name', 'T_LANG2_NAME'),
            DTColumnBuilder.newColumn("T_LANG1_NAME", "Local Name").withOption('name', 'T_LANG1_NAME')
        ];
        $scope.dtOptions = DTOptionsBuilder.newOptions().withOption('ajax', {
                dataSrc: "data",
          url: $scope.vrDir+"/T74047/GetLabelData",
                type: "POST"
            })
            .withOption('rowCallback', rowCallback)
            .withOption('processing', true) //for show progress bar
            .withOption('serverSide', true) // for server side processing
            .withPaginationType('full_numbers') // for get full pagination options // first / last / prev / next and page numbers
            .withDisplayLength(10) // Page size
            .withOption('aaSorting', [0, 'asc']) // for default sorting column // here 0 means first column
        function someClickHandler(info) {
            $scope.obj.T_AMBU_TYPE_ID = info.T_AMBU_TYPE_ID;
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
    }
]);