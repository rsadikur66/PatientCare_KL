app.controller("T74009Controller", ["$scope", "$filter", "$http", "$window", "T74009Service", "LabelService", "uiGridConstants", "DTOptionsBuilder", "DTColumnBuilder", "Data",
    function (scope, $filter, $http, $window, T74009Service, LabelService, uiGridConstants, DTOptionsBuilder, DTColumnBuilder, Data) {
        //for instance
        scope.obj = {};
        scope.obj = Data;
        scope.obj.T_MEDIC_TYPE_ID = '';
        scope.obj.T_COMPANY_ID = '';
        // Form Label Data Service Start 
        var labelData = LabelService.getlabeldata('T74009');
        labelData.then(function (data) {
            scope.entity = data;
        });
        //For custom grid
        scope.someClickHandler = someClickHandler;
        scope.dtColumns = [
            //here We will add .withOption('name','column_name') for send column name to the server 
            DTColumnBuilder.newColumn("T_MEDIC_TYPE_ID", "Medical Type").withOption('name', 'T_MEDIC_TYPE_ID'),
            DTColumnBuilder.newColumn("T_LANG2_NAME", "English Name").withOption('name', 'T_LANG2_NAME'),
            DTColumnBuilder.newColumn("T_LANG1_NAME", "Local Name").withOption('name', 'T_LANG1_NAME'),
            DTColumnBuilder.newColumn("T_COMPANY_ID", "Company Id").withOption('name', 'T_COMPANY_ID')
        ];

        scope.dtOptions = DTOptionsBuilder.newOptions().withOption('ajax', {
            dataSrc: "data",
            url: "/T74009/GetMedicalData",
            type: "POST"
        })
            .withOption('rowCallback', rowCallback)
            .withOption('processing', true) //for show progress bar
            .withOption('serverSide', true) // for server side processing
            .withPaginationType('full_numbers') // for get full pagination options // first / last / prev / next and page numbers
            .withDisplayLength(10) // Page size
            .withOption('aaSorting', [0, 'asc']) // for default sorting column // here 0 means first column

        function someClickHandler(info) {
            scope.obj.T_MEDIC_TYPE_ID = info.T_MEDIC_TYPE_ID;
            scope.obj.T_LANG2_NAME = info.T_LANG2_NAME;
            scope.obj.T_LANG1_NAME = info.T_LANG1_NAME;
            scope.obj.T_COMPANY_ID = info.T_COMPANY_ID;
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

        //end custom grid 
        //for save and update
        scope.Save = function () {
            if (scope.obj.T_LANG1_NAME == undefined || scope.obj.T_LANG1_NAME == '') {
                //$window.confirm('Local Name is requered');
                alert(scope.getSingleMsg('M0002'));
                scope.localName.show();
                return;
            }
            if (scope.obj.T_LANG2_NAME == undefined || scope.obj.T_LANG2_NAME == '') {
               // $window.confirm('English Name is requered');
                alert(scope.getSingleMsg('M0003'));
                scope.localName.show();
                return;
            }
            if (scope.obj.T_COMPANY_ID == '' || scope.obj.T_COMPANY_ID == undefined) {
                //$window.confirm('Company Id is requered');
                alert(scope.getSingleMsg('S0043'));
                scope.localName.show();
                return;
            }

            var save = T74009Service.insert_t74009(scope.obj);
            save.then(function (data) {
                if (data == "Data Save Successfully") {
                    alert(scope.getSingleMsg('S0012'));
                } else if (data == "Data Update Successfully") {
                    alert(scope.getSingleMsg('S0003'));
                }
                //alert(data);
                scope.obj = {};
                $window.location = "";
            });
        };
        //end save and update

        //for delete
        scope.Delete = function () {
            if (scope.obj.T_MEDIC_TYPE_ID !== undefined) {
                if ($window.confirm('Are you sure ?')) {
                    var save = T74009Service.delete_t74009(scope.obj);
                    save.then(function (data) {
                        if (data != null) {
                            alert(scope.getSingleMsg('S0007'));
                        }
                        //alert(data);
                        scope.obj = {};
                        $window.location = "";
                    });
                }
                return false;
            } else {
                //alert('Please select Result Type Id');
                alert(scope.getSingleMsg('S0044'));
            }
        };
        scope.Clear = function () {
            scope.obj.T_MEDIC_TYPE_ID = '';
            scope.obj.T_LANG1_NAME = '';
            scope.obj.T_LANG2_NAME = '';
            scope.obj.T_COMPANY_ID = '';
            scope.obj.T_MEDIC_TYPE_ID = undefined;
        };
    }]);