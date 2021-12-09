app.filter('propsFilter', function () {
    return function (items, props) {
        var out = [];
        if (angular.isArray(items)) {
            var keys = Object.keys(props);
            items.forEach(function (item) {
                var itemMatches = false;
                for (var i = 0; i < keys.length; i++) {
                    var prop = keys[i];
                    var text = props[prop].toLowerCase();
                    if (item[prop].toString().toLowerCase().indexOf(text) !== -1) {
                        itemMatches = true;
                        break;
                    }
                }
                if (itemMatches) {
                    out.push(item);
                }
            });
        } else {
            // Let the output be the input untouched
            out = items;
        }
        return out;
    };
    //
});

app.controller("R74120Controller",
    ["$scope","$compile", "$filter", "$http", "$window", "LabelService", "R74120Service", "uiGridConstants", "DTOptionsBuilder", "DTColumnBuilder", "Data",
        function (scope, $compile, $filter, $http, $window, LabelService, R74120Service, uiGridConstants, DTOptionsBuilder, DTColumnBuilder, Data) {
            //For Instance 
            scope.obj = {};
            scope.obj = Data;
            scope.Div_store = true;
            scope.obj.flag = 3;
            scope.obj.t = {};
            scope.obj.T_SEND_TO_STORE_ID = 1;
            //scope.obj.t.selected = {};
            // Form Label Data Service Start 
            var labelData = LabelService.getlabeldata('R74120');
            labelData.then(function (data) {
                scope.entity = data;
            });
            //Get Store To Start
            var StoreDataTo = R74120Service.GetStoreListTo();
            StoreDataTo.then(function (data) {
                scope.StoreIdTo = data;
                scope.obj.t.selected = { T_STORE_ID: data[0].T_STORE_ID, T_LANG2_NAME: data[0].T_LANG2_NAME }; 
            });


            scope.check = function () {
                scope.store(scope.obj.T_SEND_TO_STORE_ID);
                //var StoreDataTo = R74120Service.GetGridData();
                //StoreDataTo.then(function (data) {
                //    scope.StoreIdTo = data;                    
                //});
            };
            //For Report
            scope.store = function (e) {
                if (scope.obj.redubaton === '1' || scope.obj.redubaton === '2') {
                    scope.Div_store = true;
                    scope.Div_patient = false;
                    scope.obj.T_SEND_TO_STORE_ID = e;
                }
                else if (scope.obj.redubaton === '3' || scope.obj.redubaton === '4') {
                    scope.obj.flag = scope.obj.redubaton === '3' ? 3 : 4;
                    scope.dtInstance.reloadData();
                    scope.Div_store = true;
                    scope.Div_patient = true;
                    scope.obj.T_REQUEST_ID = '';
                    scope.obj.PATIENT_NAME = '';
                    scope.obj.T_ADDRESS2 = '';
                    scope.obj.T_ALT_MOBILE_NO = '';
                    scope.obj.T_SEND_TO_STORE_ID = e;
                }
                
            }
            
            //For Grid Function Start 
            
            scope.someClickHandler = someClickHandler;
            scope.dtColumns = [
                //here We will add .withOption('name','column_name') for send column name to the server 
                DTColumnBuilder.newColumn("T_REQUEST_ID", "Request Id").withOption('name', 'T_REQUEST_ID'),
                DTColumnBuilder.newColumn("T_PAT_ID", "Pat Id").withOption('name', 'T_PAT_ID').notVisible(),
                DTColumnBuilder.newColumn("T_PAT_NO", "Pat No").withOption('name', 'T_PAT_NO').notVisible(),
                DTColumnBuilder.newColumn("PATIENT_NAME", "Patient Name").withOption('name', 'PATIENT_NAME'),
                DTColumnBuilder.newColumn("T_ADDRESS2", "Address").withOption('name', 'T_ADDRESS2'),
                DTColumnBuilder.newColumn("T_ALT_MOBILE_NO", "Mobile Number").withOption('name', 'T_ALT_MOBILE_NO')
            ];
            scope.reloadData = reloadData;
            scope.dtInstance = {};
            function reloadData() {
                scope.dtInstance.reloadData();
            }
             
            scope.dtOptions = DTOptionsBuilder.newOptions().withOption('ajax',
                    {
                        dataSrc: "data",
                        url: "/R74120/GetGridData",
                        type: "POST",
                        data: function (p) {
                            p.flag = scope.obj.flag;
                            p.ambuRegId = scope.obj.T_SEND_TO_STORE_ID;                            
                        }
                    })
                .withOption('rowCallback', rowCallback)
                .withOption('processing', true) //for show progress bar
                .withOption('serverSide', true) // for server side processing
                .withPaginationType(
                    'full_numbers') // for get full pagination options // first / last / prev / next and page numbers
                .withDisplayLength(10) // Page size
                .withOption('aaSorting', [0, 'asc']); // for default sorting column // here 0 means first column
            function someClickHandler(info) {
                scope.obj.T_REQUEST_ID = info.T_REQUEST_ID;
                scope.obj.T_PAT_ID = info.T_PAT_ID;
                scope.obj.T_PAT_NO = info.T_PAT_NO;
                scope.obj.PATIENT_NAME = info.PATIENT_NAME;
                scope.obj.T_ADDRESS2 = info.T_ADDRESS2;
                scope.obj.T_ALT_MOBILE_NO = info.T_ALT_MOBILE_NO ;
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
            scope.Print = function (storeid) {
                if (scope.obj.redubaton === '2') {
                    $window.open("../R74120/GetAllData?storeid=" + storeid, "popup", "width=600,height=600,left=50,top=50");
                }
                else if (scope.obj.redubaton === '1') {
                    $window.open("../T74027Report/GetGridAllDataReport?storeid=" + storeid, "popup", "width=600,height=600,left=50,top=50");
                }
                else if (scope.obj.redubaton === '3') {
                    $window.open("../R74046/R74046Report?T_REQUEST_ID=" + scope.obj.T_REQUEST_ID, "popup", "width=600,height=600,left=100,top=50");
                }
                else if (scope.obj.redubaton === '4') {
                    $window.open("../R74046/R74046ReportBill?T_REQUEST_ID=" + scope.obj.T_REQUEST_ID, "popup", "width=600,height=600,left=100,top=50");
                }
            }
        }]);

