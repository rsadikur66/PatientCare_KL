app.controller("T74012Controller", ["$scope", "$filter", "$http", "$window", "T74012Service","LabelService", "uiGridConstants", "DTOptionsBuilder", "DTColumnBuilder","Data",
    function (scope, $filter, $http, $window, T74012Service, LabelService, uiGridConstants, DTOptionsBuilder, DTColumnBuilder,Data) {
        //For Instance 
        scope.obj = {};
        scope.obj = Data;
        scope.obj.T_DETAIL_ID = '';
        //EntryUser Function Start
        var EntryUser = T74012Service.EntryUser();
        EntryUser.then(function (data) {
            scope.obj.T_ENTRY_USER = data;
            scope.obj.T_ENTRY_DATE = $filter('date')(new Date(), 'dd-MMM-yyyy');
        });
        //EntryUser Function End
        // Form Label Data Service Start 
        var labelData = LabelService.getlabeldata('T74012');
        labelData.then(function (data) {
            scope.entity = data;
        });
        // Form Label Data Service End 
        //Save and Update Function Start 
        scope.Save = function() {
            var save = T74012Service.Insert_T74012(scope.obj);
            save.then(function(mesg) {
                if (mesg == "Data Save Successfully") {
                    alert(scope.getSingleMsg('S0012'));
                } else if (mesg == "Data Update Successfully") {
                    alert(scope.getSingleMsg('S0003'));
                }
                //alert(mesg);
                scope.obj = {};
                $window.location = ""; //For Page load After Save and Update
            });
        };
        //Save and Update Function End 

        //For Delete Function Start
        scope.Delete = function() {

            if (scope.obj.T_DETAIL_ID != null) {
                if ($window.confirm('Are you sure ?')) {
                    var dele = T74012Service.Deleted_T74012(scope.obj.T_DETAIL_ID);
                    dele.then(function(data) {
                        //alert("Data Deleted Succesfully");
                        alert(scope.getSingleMsg('S0007'));
                        scope.obj = {};
                        $window.location = ""; //For Page load After Deleted

                    });
                }
                return false;
            } else {
                //alert("Select a data for delete.");
                alert(scope.getSingleMsg('S0011'));
            }
        };
        //Delete Function End
        //Clear Function Start
        scope.Clear = function () {
            scope.obj.T_DETAIL_ID = '';
            scope.obj.T_REQUEST_ID = '';
            scope.obj.T_SITE_NO = '';
            scope.obj.T_USER_ID = '';

            scope.obj.T_DETAIL_ID = undefined;
        };
        //Clear Function End
        //For Grid Function Start 
        scope.someClickHandler = someClickHandler;
        scope.dtColumns = [
            //here We will add .withOption('name','column_name') for send column name to the server 
            DTColumnBuilder.newColumn("T_DETAIL_ID", "Detail ID").withOption('name', 'T_DETAIL_ID').notVisible(),
            DTColumnBuilder.newColumn("T_REQUEST_ID", "Request ID").withOption('name', 'T_REQUEST_ID'),
            DTColumnBuilder.newColumn("T_USER_ID", "User ID").withOption('name', 'T_USER_ID'),
            DTColumnBuilder.newColumn("T_SITE_NO", "Site No").withOption('name', 'T_SITE_NO')
        ];

        scope.dtOptions = DTOptionsBuilder.newOptions().withOption('ajax', {
                dataSrc: "data",
                url: "/T74012/GetGridData",
                type: "POST"
            })
            .withOption('rowCallback', rowCallback)
            .withOption('processing', true) //for show progress bar
            .withOption('serverSide', true) // for server side processing
            .withPaginationType('full_numbers') // for get full pagination options // first / last / prev / next and page numbers
            .withDisplayLength(10) // Page size
            .withOption('aaSorting', [0, 'asc']) // for default sorting column // here 0 means first column

        function someClickHandler(info) {
            scope.obj.T_DETAIL_ID = info.T_DETAIL_ID;
            scope.obj.T_REQUEST_ID = info.T_REQUEST_ID;
            scope.obj.T_USER_ID = info.T_USER_ID;
            scope.obj.T_SITE_NO = info.T_SITE_NO;
            
            //var prevUrl = history.length > 1 ? history.splice(-2)[0] : "/";
            //$location.path(prevUrl);
            //window.location.href = "/Menus/T04201?Pat_ID =" + info.CustomerID;
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

