app.controller("T74039Controller", ["$scope", "$filter", "$http", "$window", "T74039Service", "LabelService", "uiGridConstants", "DTOptionsBuilder", "DTColumnBuilder", "Data",
    function (scope, $filter, $http, $window, T74039Service, LabelService, uiGridConstants, DTOptionsBuilder, DTColumnBuilder, Data) {
        //For Instance 
        scope.obj = {};
        scope.obj = Data;
        scope.obj.T_CURRENCY_ID = '';
        scope.obj.N = {};

        //EntryUser Function Start
        var EntryUser = T74039Service.EntryUser();
        EntryUser.then(function (data) {
            scope.obj.T_ENTRY_USER = data;
            scope.obj.T_ENTRY_DATE = $filter('date')(new Date(), 'dd-MMM-yyyy');
        });
        //EntryUser Function End
        // Form Label Data Service Start 
        var labelData = LabelService.getlabeldata('T74039');
        labelData.then(function (data) {
            scope.entity = data;
        });
        // Form Label Data Service End 
        //Save and Update Function Start 
        scope.Save = function () {
            var save = T74039Service.Insert_T74039(scope.obj);
            save.then(function (mesg) {
                if (mesg == "Data Save Successfully") {
                    alert($scope.getSingleMsg('S0012'));
                } else if (mesg == "Data Update Successfully") {
                    alert($scope.getSingleMsg('S0003'));
                }
               // alert(mesg);
                scope.obj = {};
                $window.location.reload();//For Page load After Save and Update
            });
        }
        //Save and Update Function End 

        //For Delete Function Start
        scope.Delete = function () {

            if (scope.obj.T_CURRENCY_ID != null) {
                if ($window.confirm('Are you sure ?')) {
                    var dele = T74039Service.Deleted_T74039(scope.obj.T_CURRENCY_ID);
                    dele.then(function (data) {
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
            scope.obj.T_CURRENCY_ID = '';
            scope.obj.N = {};
            scope.obj.T_LANG2_NAME = '';
            scope.obj.T_LANG1_NAME = '';
            scope.obj.T_CURRENCY_NAME = '';
            scope.obj.T_CONVERTION_VALUE_TK = '';

            scope.obj.T_CURRENCY_ID = undefined;
        };
        //Clear Function End
        //For Grid Function Start 
        scope.someClickHandler = someClickHandler;
        scope.dtColumns = [
            //here We will add .withOption('name','column_name') for send column name to the server 
            DTColumnBuilder.newColumn("T_CURRENCY_ID", "Currency ID").withOption('name', 'T_CURRENCY_ID').notVisible(),
            DTColumnBuilder.newColumn("T_LANG2_NAME", "English Name").withOption('name', 'T_LANG2_NAME'),
            DTColumnBuilder.newColumn("T_LANG1_NAME", "Local Name").withOption('name', 'T_LANG1_NAME'),
            DTColumnBuilder.newColumn("T_CURRENCY_NAME", "Currency Type").withOption('name', 'T_CURRENCY_NAME'),
            DTColumnBuilder.newColumn("T_CONVERTION_VALUE_TK", "Convertion (Taka) ").withOption('name', 'T_CONVERTION_VALUE_TK')
        ];

        scope.dtOptions = DTOptionsBuilder.newOptions().withOption('ajax',
                {
                    dataSrc: "data",
                    url: "/T74039/GetGridData",
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
            scope.obj.T_CURRENCY_ID = info.T_CURRENCY_ID;
            scope.obj.T_LANG2_NAME = info.T_LANG2_NAME;
            scope.obj.T_LANG1_NAME = info.T_LANG1_NAME;
            scope.obj.T_CURRENCY_NAME = info.T_CURRENCY_NAME;
            scope.obj.T_CONVERTION_VALUE_TK = info.T_CONVERTION_VALUE_TK;
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