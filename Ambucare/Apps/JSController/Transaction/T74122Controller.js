app.controller("T74122Controller",
    ["$scope", "$filter", "$http", "$window", "$location", "T74122Service", "LabelService", "uiGridConstants",
        "DTOptionsBuilder",
        "DTColumnBuilder", "Data",
        function(scope,$filter,$http,$window,$location,T74122Service,LabelService,uiGridConstants,DTOptionsBuilder,DTColumnBuilder,
            Data) {
            //For Instance 
            scope.obj = {};
            scope.obj = Data;
            scope.obj.T74084 = {};
            //scope.obj.T74080 = {};
            var url = $location.absUrl();
            var param = url.split("=").pop();
            scope.obj.transferList = [];

            // Form Label Data Service Start 
            var labelData = LabelService.getlabeldata("T74122");
            labelData.then(function(data) {
                scope.entity = data;
            });

            //Start point For Retrieve Data from t74080 and t74081
            scope.TransferData = function() {
                var transfer = T74122Service.GetTransferData(param);
                transfer.then(function(data) {
                    if (scope.obj.transferList.length === 0) {
                        scope.obj.T74084.T_TRANSFER_REQ_ID = data[0].T_TRANSFER_REQ_ID;
                        scope.obj.T74084.T_TRANSFER_REQ_NO = data[0].T_TRANSFER_REQ_NO; //84

                        scope.obj.T74084.T_RCV_FRM_STR_ID = data[0].T_REQ_SET_BY_STORE_ID;
                        scope.obj.T_STORE_FROM = data[0].T_STORE_FROM_LANG2; //84

                        scope.obj.T74084.T_CURRENCY_ID = data[0].T_CURRENCY_ID;
                        scope.obj.T74084.T_CURRENCY_NAME = data[0].T_CURRENCY_NAME; //84

                        //scope.obj.T74084.T_RCV_TO_STR_ID = data[0].T_SEND_TO_STORE_ID; 
                        scope.obj.T74084.T_RCV_TO_STR_ID = data[0].T_STORE_ID_TO;
                        scope.obj.T_STORE_TO = data[0].T_STORE_TO_LANG2; //84

                        scope.obj.T74084.T_RCV_FRM_STR_ID = data[0].T_STORE_ID_FROM;
                        scope.obj.T_STORE_FROM = data[0].T_STORE_FROM_LANG2;

                        scope.obj.T74084.T_RCV_DATE = $filter("date")(new Date(), "dd/MM/yyyy"); //84

                        scope.obj.T_SUPPLIER_ID = data[0].T_SUPPLIER_ID;
                        scope.obj.T_SUPPLIER_NAME = data[0].T_SUPPLIER_NAME; //85

                        scope.obj.T_TRANSFER_DELIVERY_METHOD = data[0].T_TRANSFER_DELIVERY_METHOD;
                        //scope.obj.T_REQ_DATE = $filter('date')(data[0].T_REQ_DATE, 'dd-MMM-yyyy');
                        var reqDate = new Date(data[0].T_REQ_DATE.match(/\d+/)[0] * 1);
                        scope.obj.T_REQ_DATE = $filter("date")(reqDate, "dd/MM/yyyy");
                        //scope.obj.T_TRANSFER_FOR = data[0].T_TRANSFER_FOR;
                        //scope.obj.T_TRANSFER_REQ_DTL_ID = data[0].T_TRANSFER_REQ_DTL_ID;
                        // scope.obj.T_TOTAL_VALUE = data[0].TotalAmount;

                        for (var i = 0; i < data.length; i++) {
                            scope.obj.demo = {};
                            scope.obj.demo.T_ITEM_ID = data[i].T_ITEM_ID;
                            scope.obj.demo.T_ITEM_NAME = data[i].T_ITEM_NAME;
                            scope.obj.demo.T_UOM_ID = data[i].T_UOM_ID;
                            scope.obj.demo.T_UOM_NAME = data[i].T_UM_NAME;
                            scope.obj.demo.TRANSFER_QTY = data[i].Transfer_Quantity;
                            scope.obj.demo.T_RECEIVE_TYPE = data[i].T_RECEIVE_TYPE;
                            scope.obj.demo.T_UNIT_VALUE = data[i].T_UNIT_VALUE;
                            //scope.obj.demo.T_TOTAL_VALUE = data[i].T_TOTAL_VALUE;
                            scope.obj.demo.T_TOTAL_VALUE = data[i].TotalAmount;
                            scope.obj.demo.T_PRIORITY = data[i].T_TRANSFER_PRORITY;
                            scope.obj.demo.T_TRANSFER_REQ_DTL_ID = data[i].T_TRANSFER_REQ_DTL_ID;
                            //scope.obj.demo.T_CUR_STOCK_ID = data[i].T_CUR_STOCK_ID;
                            scope.obj.transferList.push(scope.obj.demo);
                        }
                    }
                });
            }
            //End point For Retrieve Data from t74080 and t74081


            //Pop up for Received By Field
            scope.CloseReceivePopup = function(id) {
                document.getElementById(id).style.display = "none";
            }
            scope.Receive_Click = function(id) {
                debugger;
                var receiveBy = T74122Service.ReceiveBy(scope.obj.T74084.T_RCV_TO_STR_ID); //scope.obj.T74060.T_STORE_ID
                receiveBy.then(function(data) {
                    scope.obj.ReceiveList = data;
                });

                document.getElementById(id).style.display = "block";
            }
            scope.setClickedRow = function(index, obj, id) {

                scope.selectedRow = index;
                scope.obj.T_RECEIVE_BY = obj.T_EMP_ID;
                scope.obj.T_FIRST_LANG2_NAME = obj.T_FIRST_LANG2_NAME;
                scope.Search = "";
                document.getElementById(id).style.display = "none";
            }
            //Pop up END


            //===== Save ========Start
            scope.Save_Click = function() {
                debugger;
                scope.obj.T74085 = [];
                //scope.obj.T74084 = [];
                scope.obj.T74081 = [];
                scope.obj.T74027 = [];
                scope.obj.T74080 = [];
                for (var i = 0; i < scope.obj.transferList.length; i++) {
                    scope.obj.T_84 = {};
                    scope.obj.T_85 = {};
                    scope.obj.T_81 = {};
                    scope.obj.T_27 = {};
                    scope.obj.T_80 = {};

                    scope.obj.T_85.T_ITEM_ID = scope.obj.transferList[i].T_ITEM_ID;
                    scope.obj.T_85.T_UOM_ID = scope.obj.transferList[i].T_UOM_ID;
                    scope.obj.T_85.T_TRNS_QTY = scope.obj.transferList[i].TRANSFER_QTY;
                    scope.obj.T_85.T_CUR_STOCK_ID = scope.obj.transferList[i].T_CUR_STOCK_ID;
                    scope.obj.T_85.T_TRANSFER_REQ_DTL_ID = scope.obj.transferList[i].T_TRANSFER_REQ_DTL_ID;
                    scope.obj.T_85.T_SUPPLIER_ID = scope.obj.T_SUPPLIER_ID;
                    scope.obj.T74084.T_TOTAL_VALUE = scope.obj.transferList[i].T_TOTAL_VALUE;
                    scope.obj.T74084.T_UNIT_VALUE = scope.obj.transferList[i].T_UNIT_VALUE;
                    
                    scope.obj.T74085.push(scope.obj.T_85);
                    //scope.obj.T74084.push(scope.obj.T74084.T_TOTAL_VALUE);

                    //777

                    ////======================== For T74027 start =======================
                    //var t27 = obj.T74027.Where(p => p.T_CUR_STOCK_ID == transferList[i].T_CUR_STOCK_ID).FirstOrDefault();
                    //if (t27 != 0) {
                    //    scope.obj.T_85.T_CUR_STOCK_ID = scope.obj.transferList[i].T_CUR_STOCK_ID;
                    //    scope.obj.T_27.T_ITEM_ID = scope.obj.transferList[i].T_ITEM_ID;
                    //    scope.obj.T_27.T_ITEM_UM_ID = scope.obj.transferList[i].T_ITEM_UM_ID;
                    //    scope.obj.T_27.T_TRNS_QTY = scope.obj.transferList[i].TRANSFER_QTY;
                    //    scope.obj.T_27.T_UNIT_VALUE = scope.obj.transferList[i].T_UNIT_VALUE;
                    //    scope.obj.T_27.T_STORE_ID = scope.obj.T74084.T_RCV_TO_STR_ID;
                    //}
                    //scope.obj.T_85.T_TRANSFER_REQ_DTL_ID = scope.obj.transferList[i].T_TRANSFER_REQ_DTL_ID;
                    scope.obj.T_85.T_CUR_STOCK_ID = scope.obj.transferList[i].T_CUR_STOCK_ID;
                    scope.obj.T_27.T_ITEM_ID = scope.obj.transferList[i].T_ITEM_ID;
                    scope.obj.T_27.T_ITEM_UM_ID = scope.obj.transferList[i].T_UOM_ID;
                    scope.obj.T_27.T_TRNSACTION_QTY = scope.obj.transferList[i].TRANSFER_QTY;
                    scope.obj.T_27.T_PURCHASE_QTY = scope.obj.transferList[i].TRANSFER_QTY;
                    //scope.obj.T_27.T_PURCHASE_QTY = scope.obj.transferList[i].
                    scope.obj.T_27.T_UNIT_VALUE = scope.obj.transferList[i].T_UNIT_VALUE;
                    scope.obj.T_27.T_TOTAL_VALUE = scope.obj.transferList[i].T_TOTAL_VALUE;
                    scope.obj.T_27.T_STORE_ID = scope.obj.T74084.T_RCV_TO_STR_ID;
                    
                    //scope.obj.T_27.T_PURCHASE_QTY = scope.obj.purProductList[i].Issue;
                    //scope.obj.T_27.T_TRNSACTION_QTY = scope.obj.purProductList[i].Issue;
                    //scope.obj.T_27.T_EXPIRE_DATE = scope.obj.purProductList[i].T_EXP_DATE;
                    //scope.obj.T_27.T_TOTAL_VALUE = scope.obj.purProductList[i].T_TOTAL_VALUE; //T_STOCK_DATE

                    //scope.obj.T_27.T_STOCK_DATE = scope.obj.T74060.T_RECEIVE_DATE;
                    //scope.obj.T_27.T_STORE_ID = scope.obj.T74060.T_STORE_ID;
                    //scope.obj.T_27.T_SUPPLIER_ID = scope.obj.T74060.T_SUPPLIER_ID;
                    //scope.obj.T_27.T_COMPANY_ID = scope.obj.T74060.T_COMPANY_ID;

                    scope.obj.T74027.push(scope.obj.T_27);
                    ////======================== For T74027 end =======================

                    scope.obj.T_80.T_STATUS = scope.obj.T74084.T_RCV_STATUS;
                    scope.obj.T_80.T_TRANSFER_REQ_ID = scope.obj.T74084.T_TRANSFER_REQ_ID;
                    scope.obj.T74080.push(scope.obj.T_80);

                }


                var saveData = T74122Service.SaveData(scope.obj.T74080,scope.obj.T74084, scope.obj.T74085, scope.obj.T74027);
                saveData.then(function(data) {
                    //alert("Save successfully");
                    alert(scope.getSingleMsg('S0012'));
                    //scope.obj.transferList = [];

                });
           
        }
            //===== Save ========End


        }]);

app.filter("propsFilter", function () {
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
