app.controller("T74025Controller", ["$scope", "$filter", "$http", "$window", "T74025Service","LabelService", "uiGridConstants", "DTOptionsBuilder", "DTColumnBuilder", "Data",
    function (scope, $filter, $http, $window, T74025Service, LabelService, uiGridConstants, DTOptionsBuilder, DTColumnBuilder, Data) {
        //for instance
        scope.obj = {};
        scope.obj = Data;
        // Form Label Data Service Start 
        var labelData = LabelService.getlabeldata('T74025');
        labelData.then(function (data) {
            scope.entity = data;
        });
        scope.Save = function() {
            var save = T74025Service.insert_t74025(scope.obj);
            save.then(function(data) {
                //alert("Data saved Succesfully");
                alert(scope.getSingleMsg('S0012'));
                $window.location.reload();
            });
        };

        scope.Delete = function() {
            var save = T74025Service.delete_t74025(scope.obj);
            save.then(function(data) {
                //alert("Data deleted Succesfully");
                alert(scope.getSingleMsg('S0007'));
                $window.location.reload();
            });
        };
        scope.Clear = function () {
            scope.obj.T_USER_TYPE_ID = '';
            scope.obj.T_TYPE_NAME1 = '';
            scope.obj.T_TYPE_NAME2 = '';

            scope.obj.T_USER_TYPE_ID = undefined;
        };
        //For Grid Function Start 
        scope.someClickHandler = someClickHandler;
        scope.dtColumns = [
            //here We will add .withOption('name','column_name') for send column name to the server 
            DTColumnBuilder.newColumn("T_USER_TYPE_ID", "User Type").withOption('name', 'T_USER_TYPE_ID'),
            DTColumnBuilder.newColumn("T_TYPE_NAME1", "Type Name 1").withOption('name', 'T_TYPE_NAME1'),
            DTColumnBuilder.newColumn("T_TYPE_NAME2", "Type Name 2").withOption('name', 'T_TYPE_NAME2')
        ];

        scope.dtOptions = DTOptionsBuilder.newOptions().withOption('ajax', {
                dataSrc: "data",
                url: "/T74025/GetItemBrandData",
                type: "POST"
            })
            .withOption('rowCallback', rowCallback)
            .withOption('processing', true) //for show progress bar
            .withOption('serverSide', true) // for server side processing
            .withPaginationType('full_numbers') // for get full pagination options // first / last / prev / next and page numbers
            .withDisplayLength(10) // Page size
            .withOption('aaSorting', [0, 'asc']) // for default sorting column // here 0 means first column

        function someClickHandler(info) {
            scope.obj.T_USER_TYPE_ID = info.T_USER_TYPE_ID;
            scope.obj.T_TYPE_NAME1 = info.T_TYPE_NAME1;
            scope.obj.T_TYPE_NAME2 = info.T_TYPE_NAME2;
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