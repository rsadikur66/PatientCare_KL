app.controller("T74054Controller", ["$scope", "$filter", "$http", "$window", "T74054Service","LabelService", "uiGridConstants", "DTOptionsBuilder", "DTColumnBuilder", "Data",
    function (scope, $filter, $http, $window, T74054Service, LabelService, uiGridConstants, DTOptionsBuilder, DTColumnBuilder, Data) {
        //for instance
        scope.obj = {};
        scope.obj = Data;
        // Form Label Data Service Start 
        var labelData = LabelService.getlabeldata('T74054');
        labelData.then(function (data) {
            scope.entity = data;
        });
        scope.stest = function (data) {
            alert(JSON.stringify(data.target.id + JSON.parse(sessionStorage.getItem("FCode"))));
        }
        scope.Save = function () {
            if (scope.obj.T_COMPANY_ID === undefined) {
                var save = T74054Service.insert_t74054(scope.obj);
                save.then(function (data) {
                    //alert("Data saved Succesfully");
                    alert(scope.getSingleMsg('S001'));
                    $window.location.reload();

                });
            }
            else {
                var save = T74054Service.insert_t74054(scope.obj);
                save.then(function (data) {
                    //alert("Data updated Succesfully");
                    alert(scope.getSingleMsg('S0003'));
                    $window.location.reload();
                });
            }
        }
        scope.Delete = function () {
            if (scope.obj.T_COMPANY_ID !== undefined) {
                if ($window.confirm('Are you sure ?')) {
                    var del = T74054Service.delete_t74054(scope.obj);
                    del.then(function (data) {
                        //alert("Data delete Succesfully");
                        alert(scope.getSingleMsg('S0007'));
                        $window.location.reload();
                    });
                }
                return false;
            }
            else {
                //alert('Please select Item Brand');
                alert(scope.getSingleMsg('S0040'));
                return false;
            }
        };
        scope.Clear = function () {
            scope.obj.T_COMPANY_ID = '';
            scope.obj.T_LANG1_NAME = '';
            scope.obj.T_LANG2_NAME = '';
            scope.obj.T_LANG1_ADRS_NAME = '';
            scope.obj.T_LANG2_ADRS_NAME = '';
            scope.obj.T_EMAIL = '';
            scope.obj.T_PHONE = '';
            scope.obj.T_WEB_URL = '';

            scope.obj.T_COMPANY_ID = undefined;
        };
        //For Grid Function Start
        scope.someClickHandler = someClickHandler;
        scope.dtColumns = [
            //here We will add .withOption('name','column_name') for send column name to the server 
            DTColumnBuilder.newColumn("T_COMPANY_ID", "Item Brand").withOption('name', 'T_COMPANY_ID'),
            DTColumnBuilder.newColumn("T_LANG2_NAME", "English Name").withOption('name', 'T_LANG2_NAME'),
            DTColumnBuilder.newColumn("T_LANG1_NAME", "Local Name").withOption('name', 'T_LANG1_NAME'),
            DTColumnBuilder.newColumn("T_LANG1_ADRS_NAME", "Address Local").withOption('name', 'T_LANG1_ADRS_NAME'),
            DTColumnBuilder.newColumn("T_LANG2_ADRS_NAME", "Address English").withOption('name', 'T_LANG2_ADRS_NAME'),
            DTColumnBuilder.newColumn("T_EMAIL", "Email").withOption('name', 'T_EMAIL'),
            DTColumnBuilder.newColumn("T_PHONE", "Phone").withOption('name', 'T_PHONE'),
            DTColumnBuilder.newColumn("T_WEB_URL", "Web Link").withOption('name', 'T_WEB_URL')

        ];

        scope.dtOptions = DTOptionsBuilder.newOptions().withOption('ajax', {
            dataSrc: "data",
            url: "/T74054/GetItemBrandData",
            type: "POST"
        })
            .withOption('rowCallback', rowCallback)
            .withOption('processing', true) //for show progress bar
            .withOption('serverSide', true) // for server side processing
            .withPaginationType('full_numbers') // for get full pagination options // first / last / prev / next and page numbers
            .withDisplayLength(10) // Page size
            .withOption('aaSorting', [0, 'asc']) // for default sorting column // here 0 means first column

        function someClickHandler(info) {
            scope.obj.T_COMPANY_ID = info.T_COMPANY_ID;
            scope.obj.T_LANG2_NAME = info.T_LANG2_NAME;
            scope.obj.T_LANG1_NAME = info.T_LANG1_NAME;
            scope.obj.T_LANG1_ADRS_NAME = info.T_LANG1_ADRS_NAME;
            scope.obj.T_LANG2_ADRS_NAME = info.T_LANG2_ADRS_NAME;
            scope.obj.T_EMAIL = info.T_EMAIL;
            scope.obj.T_PHONE = info.T_PHONE;
            scope.obj.T_WEB_URL = info.T_WEB_URL;

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
