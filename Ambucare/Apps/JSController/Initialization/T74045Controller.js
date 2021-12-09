app.controller("T74045Controller", ["$scope", "$filter", "$http", "$window", "T74045Service", "LabelService", "uiGridConstants", "DTOptionsBuilder", "DTColumnBuilder", "Data",
    function (scope, $filter, $http, $window, T74045Service, LabelService, uiGridConstants, DTOptionsBuilder, DTColumnBuilder, Data) {
        //For Instance 
        scope.obj = {};
        scope.obj = Data;
        scope.obj.T_SUPPLIER_ID = '';
        scope.obj.N = {};

        //EntryUser Function Start
        var EntryUser = T74045Service.EntryUser();
        EntryUser.then(function (data) {
            scope.obj.T_ENTRY_USER = data;
            scope.obj.T_ENTRY_DATE = $filter('date')(new Date(), 'dd-MMM-yyyy');
        });
        //EntryUser Function End
        // Form Label Data Service Start 
        var labelData = LabelService.getlabeldata('T74045');
        labelData.then(function (data) {
            scope.entity = data;
        });
        // Form Label Data Service End 
        //Save and Update Function Start 
        scope.Save = function () {
            var save = T74045Service.Insert_T74045(scope.obj);
            save.then(function (mesg) {
                if (mesg == "Data Save Successfully") {
                    alert($scope.getSingleMsg('S0012'));
                } else if (mesg == "Data Update Successfully") {
                    alert($scope.getSingleMsg('S0003'));
                }
                //alert(mesg);
                scope.obj = {};
                $window.location.reload();//For Page load After Save and Update
            });
        }
        //Save and Update Function End 

        //For Delete Function Start
        scope.Delete = function () {

            if (scope.obj.T_SUPPLIER_ID != null) {
                if ($window.confirm('Are you sure ?')) {
                    var dele = T74045Service.Deleted_T74045(scope.obj.T_SUPPLIER_ID);
                    dele.then(function (data) {
                        //alert("Data Deleted Succesfully");
                        alert(scope.getSingleMsg('S0007'));
                        scope.obj = {};
                        $window.location.reload();//For Page load After Deleted
                    });
                }
                return false;
            }
            else {
                //alert("Select a data for delete.");
                alert(scope.getSingleMsg('S0011'));
            }
        }
        //Delete Function End
        //Clear Function Start
        scope.Clear = function () {
            scope.obj.T_SUPPLIER_ID = '';
            scope.obj.N = {};
            scope.obj.T_LANG2_NAME = '';
            scope.obj.T_LANG1_NAME = '';
            scope.obj.T_PRES_ADDRESS = '';
            scope.obj.T_PER_ADDRESS = '';
            scope.obj.T_PHONE_NUMBER = '';
            scope.obj.T_MOBILE_NUMBER = '';
            scope.obj.T_EMAIL_ID = '';

            scope.obj.T_SUPPLIER_ID = undefined;
        };
        //Clear Function End
        //For Grid Function Start 
        scope.someClickHandler = someClickHandler;
        scope.dtColumns = [
            //here We will add .withOption('name','column_name') for send column name to the server 
            DTColumnBuilder.newColumn("T_SUPPLIER_ID", "Supplier ID").withOption('name', 'T_SUPPLIER_ID').notVisible(),
            DTColumnBuilder.newColumn("T_LANG2_NAME", "English Name").withOption('name', 'T_LANG2_NAME'),
            DTColumnBuilder.newColumn("T_LANG1_NAME", "Local Name").withOption('name', 'T_LANG1_NAME'),
            DTColumnBuilder.newColumn("T_PRES_ADDRESS", "Present Address").withOption('name', 'T_PRES_ADDRESS'),
            DTColumnBuilder.newColumn("T_PER_ADDRESS", "Permanant Address").withOption('name', 'T_PER_ADDRESS'),
            DTColumnBuilder.newColumn("T_PHONE_NUMBER", "Phone No").withOption('name', 'T_PHONE_NUMBER'),
            DTColumnBuilder.newColumn("T_MOBILE_NUMBER", "Mobile No").withOption('name', 'T_MOBILE_NUMBER'),
            DTColumnBuilder.newColumn("T_EMAIL_ID", "Email").withOption('name', 'T_EMAIL_ID')
        ];

        scope.dtOptions = DTOptionsBuilder.newOptions().withOption('ajax',
                {
                    dataSrc: "data",
                    url: "/T74045/GetGridData",
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
            scope.obj.T_SUPPLIER_ID = info.T_SUPPLIER_ID;
            scope.obj.T_LANG2_NAME = info.T_LANG2_NAME;
            scope.obj.T_LANG1_NAME = info.T_LANG1_NAME;
            scope.obj.T_PRES_ADDRESS = info.T_PRES_ADDRESS;
            scope.obj.T_PER_ADDRESS = info.T_PER_ADDRESS;
            scope.obj.T_PHONE_NUMBER = info.T_PHONE_NUMBER;
            scope.obj.T_MOBILE_NUMBER = info.T_MOBILE_NUMBER;
            scope.obj.T_EMAIL_ID = info.T_EMAIL_ID;
        }
        function rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
            // Unbind first in order to avoid any duplicate handler (see https://github.com/l-lin/angular-datatables/issues/87)
            $('td', nRow).unbind('click');
            $('td', nRow).bind('click', function () {
                scope.$apply(function () {
                    scope.someClickHandler(aData);
                });
            });
            return nRow;
        }

        //For Grid Function End 

    }]);