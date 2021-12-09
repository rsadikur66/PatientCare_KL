//{ T_STORE_ID: 95, T_LANG2_NAME: "TEAM7_236-9-2015" }



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

app.controller("Q74146Controller",
    ["$scope", "$compile", "$filter", "$http", "$window", "LabelService", "Q74146Service", "uiGridConstants", "DTOptionsBuilder", "DTColumnBuilder", "Data",
        function (scope, $compile, $filter, $http, $window, LabelService, Q74146Service, uiGridConstants, DTOptionsBuilder, DTColumnBuilder, Data) {
            //For Instance 
            scope.obj = {};
            scope.obj = Data;
            scope.Div_store = true;
            scope.obj.flag = 3;
            scope.obj.t = {};
            scope.obj.T_SEND_TO_STORE_ID = 1;
            //scope.obj.t.selected = {};
            scope.userId = '';
            var labelData = LabelService.getlabeldata('Q74146');
            labelData.then(function (data) {
                scope.entity = data;
            });
            //objArray = [{ T_QUERY_ID: 1, T_LANG2_NAME: "ACCEPTED" }, { T_QUERY_ID: 2, T_LANG2_NAME: "REJECTED" },
            //{ T_QUERY_ID: 3, T_LANG2_NAME: "MISCELLINIOUS" }];
            //Get ambulance list To Start
            var ambulanceList = Q74146Service.GetAmbulanceList();
            ambulanceList.then(function (data) {
                scope.ambList = JSON.parse(data);
                scope.ambList.push({ "T_USER_ID": "ALL" });
                //scope.obj.t.selected = { T_AMBU_REG_ID: data[0].T_AMBU_REG_ID, T_USER_ID: data[0].T_USER_ID };
            });       


            //scope.onEventSelect = function(e) {
            //    //alert(e);
            //    if (e == 1) {
            //        var CanceledRequestList = Q74146Service.RequestCanceledList();
            //        CanceledRequestList.then(function (data) {
            //            scope.obj.RequestCanceledList = data;
            //        });
            //    }
            //};

            scope.onEventSelect = function(e) {
                scope.userId = e;
            }
            //var stockItem = Q74146Service.GetStockItem();
            //stockItem.then(function (data) {
            //    //scope.ambList = JSON.parse(data);
            //    ////scope.obj.t.selected = { T_AMBU_REG_ID: data[0].T_AMBU_REG_ID, T_USER_ID: data[0].T_USER_ID };
            //});
            
            //For Grid Function End 
            scope.Print = function (storeid) {
                var fromDate = moment(scope.obj.T_FROM_DATE).format('DD-MMM-YY');
                var toDate = moment(scope.obj.T_TO_DATE).format('DD-MMM-YY');
                if (scope.obj.redubaton === '1') {
                    $window.open("../Q74146/GetAcceptedData?userid=" + scope.obj.T_USER_ID + "&FromDate=" + fromDate.toUpperCase() + "&ToDate=" + toDate.toUpperCase(), "popup", "width=600,height=600,left=50,top=50");
                }
                else if (scope.obj.redubaton === '2') {
                    $window.open("../Q74146/GetRejectedData?userid=" + scope.obj.T_USER_ID + "&FromDate=" + fromDate.toUpperCase() + "&ToDate=" + toDate.toUpperCase(), "popup", "width=600,height=600,left=50,top=50");
                }
                else if (scope.obj.redubaton === '3') {
                    var store = Q74146Service.getStorId(scope.obj.T_AMBU_REG_ID);
                    store.then(function(data) {
                        var st = JSON.parse(data);
                        var storeId = st[0].T_STORE_ID;
                        $window.open("../T74027Report/GetGridAllDataReport?storeid=" + storeId, "popup", "width=600,height=600,left=50,top=50");
                    });
                    //$window.open("../Q74146/GetStockItem?ambuId=" + scope.obj.T_AMBU_REG_ID, "popup", "width=600,height=600,left=100,top=50");
                }
                else if (scope.obj.redubaton === '4') {
                    $window.open("../Q74146/GetAllRequest?userid=" + scope.obj.T_USER_ID + "&FromDate=" + fromDate.toUpperCase() + "&ToDate=" + toDate.toUpperCase(), "popup", "width=600,height=600,left=100,top=50");
                }
                else if (scope.obj.redubaton === '5') {
                    $window.open("../Q74146/GetUsedMedicineByTeam?userid=" + scope.obj.T_USER_ID, "popup", "width=600,height=600,left=100,top=50");
                }
                else if (scope.obj.redubaton === '6') {
                    $window.open("../Q74146/GetUsedMedicineByRequest?requestId=" + scope.obj.T_REQUEST_ID, "popup", "width=600,height=600,left=100,top=50");
                }
            }
        }]);

