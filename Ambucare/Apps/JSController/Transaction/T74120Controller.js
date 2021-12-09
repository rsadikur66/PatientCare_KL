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

app.controller("T74120Controller",
    ["$scope", "$filter", "$http", "$window", "LabelService", "T74120Service", "T74114ItemTypeFactory", "T74114ItemNameFactory","uiGridConstants", "DTOptionsBuilder",
        "DTColumnBuilder", "T74120PersonTypeFactory", "T74120PersonNameFactory", "Data",
        function (scope, $filter, $http, $window, LabelService, T74120Service, T74114ItemTypeFactory, T74114ItemNameFactory,  uiGridConstants, DTOptionsBuilder, DTColumnBuilder, T74120PersonTypeFactory, T74120PersonNameFactory, Data) {
            //For Instance 
            scope.obj = {};
            scope.obj = Data;
            scope.obj.t74080 = {};
            scope.obj.t74081 = {};
            
            // Form Label Data Service Start 
            var labelData = LabelService.getlabeldata('T74120');
            labelData.then(function (data) {
                scope.entity = data;
            });

            //For Supplier DropDownList Start
            var SupplierData = T74120Service.GetSupplierList();
            SupplierData.then(function (data) {
                scope.supplierId = data;
            });

            //Get Store From Start
            var StoreDataFrom = T74120Service.GetStoreListFrom();
            StoreDataFrom.then(function (data) {
                scope.StoreIdFrom = data;
            });


            //Get Store To Start
            var StoreDataTo = T74120Service.GetStoreListTo();
            StoreDataTo.then(function (data) {
                scope.StoreIdTo = data;
            });

            //For Check Two Dropdown Value are Same  Start
            scope.checkStoreTo = function () {
                if (scope.obj.t74080.T_REQ_SET_BY_STORE_ID === scope.obj.t74080.T_SEND_TO_STORE_ID) {
                    //alert("Stores can not be same");
                    alert(scope.getSingleMsg('S0023'));
                    scope.obj.t.selected = { T_LANG2_NAME: 'Select' };
                }
            }
            scope.checkStoreFrom = function () {
                if (scope.obj.t74080.T_REQ_SET_BY_STORE_ID === scope.obj.t74080.T_SEND_TO_STORE_ID) {
                    //alert("Stores can not be same");
                    alert(scope.getSingleMsg('S0023'));
                    scope.obj.f.selected = { T_LANG2_NAME: 'Select' };
                }
            }
            //For Person Type function
            scope.PersonType = function () {
                var url = '';
                var data = '';
                T74120PersonTypeFactory.getModal(url, data);
            }
            //For Person Name function
            scope.PersonName = function (T_EMP_TYP_ID) {
                var url = '';
                var data = '';
                T74120PersonNameFactory.getModal(url, data, T_EMP_TYP_ID);
            }

            //Get Currency List Start
            var CurrencyData = T74120Service.GetCurrencyList();
            CurrencyData.then(function (data) {
                scope.CurrencyId = data;
            });

            //Button Add and remove starts
            scope.obj.TransferInfo = [];
            scope.Addrow = function() { scope.obj.TransferInfo.push({});}
            scope.RemoveRow = function (index) { scope.obj.TransferInfo.splice(index, 1); }


            //Item Type popup Start
            scope.ItemList = function (index) {
                var url = '';
                var data = '';
                T74114ItemTypeFactory.getModal(url, data, index);
            };

            //Item Name popup Start
            scope.ItemName = function (index) {
                var id = scope.obj.TransferInfo[index].T_COST_TYPE_ID;
                var url = '';
                var data = '';
                T74114ItemNameFactory.getModal(url, data, index, id);
            };

            //For UOM Function start
            scope.UOM = function (index) {
                var uom = T74120Service.GetUom(scope.obj.TransferInfo[index].T_COST_TYPE_ID);
                uom.then(function (data) {
                    scope.uom = data;
                });
            }

            //For stock Information Function 
            scope.currentStockfrom=function(index) {
                var currentstockfm = T74120Service.GetStock(scope.obj.TransferInfo[index].T_ITEM_ID,
                    scope.obj.TransferInfo[index].u.selected.T_ITEM_UM_ID,
                    scope.obj.t74080.T_REQ_SET_BY_STORE_ID);
                currentstockfm.then(function(from) {
                    scope.obj.TransferInfo[index].currentstockfm = from;
                });
                var currentstockto = T74120Service.GetStock(scope.obj.TransferInfo[index].T_ITEM_ID,
                    scope.obj.TransferInfo[index].u.selected.T_ITEM_UM_ID,
                    scope.obj.t74080.T_SEND_TO_STORE_ID);
                currentstockto.then(function (to) {
                    scope.obj.TransferInfo[index].currentstockto = to;
                });
                var PurchaPrice = T74120Service.GetPurPrice(scope.obj.TransferInfo[index].T_ITEM_ID,
                    scope.obj.TransferInfo[index].u.selected.T_ITEM_UM_ID);
                PurchaPrice.then(function (pur) {
                    scope.obj.TransferInfo[index].T_UNIT_VALUE = pur;
                });
            }

            scope.keypressevt = function (trans) {
                for (var j = 0; j < scope.obj.TransferInfo.length; j++) {
                    if (scope.obj.TransferInfo[j].currentstockfm < trans) {
                        //alert("This Quantity is not Availble");
                        alert($scope.getSingleMsg('S0026'));
                        scope.obj.TransferInfo[j].T_TRANSFER_QTY = '';
                    }
                    else {
                        scope.obj.TransferInfo[j].T_TOTAL_VALUE =parseFloat(scope.obj.TransferInfo[j].T_TRANSFER_QTY *
                            scope.obj.TransferInfo[j].T_UNIT_VALUE).toFixed(2);
                        scope.obj.TransferInfo[j].T_TOTAL_VALUE = parseFloat(scope.obj.TransferInfo[j].T_TRANSFER_QTY *
                            scope.obj.TransferInfo[j].T_UNIT_VALUE).toFixed(2);
                    }
                }
            }
            //Save Function Start
            scope.Save = function () {
                scope.obj.t74081 = [];
                for (var i = 0; i < scope.obj.TransferInfo.length; i++) {
                    scope.obj._t74081 = {};
                    scope.obj._t74081.T_ITEM_ID = scope.obj.TransferInfo[i].T_ITEM_ID;
                    scope.obj._t74081.T_UOM_ID = scope.obj.TransferInfo[i].T_UOM_ID;
                    scope.obj._t74081.T_TRANSFER_PRORITY = scope.obj.TransferInfo[i].T_TRANSFER_PRORITY;
                    scope.obj._t74081.T_TRANSFER_QTY = scope.obj.TransferInfo[i].T_TRANSFER_QTY;
                    scope.obj._t74081.T_UNIT_VALUE = scope.obj.TransferInfo[i].T_UNIT_VALUE;
                    scope.obj._t74081.T_TOTAL_VALUE = scope.obj.TransferInfo[i].T_TOTAL_VALUE;
                    scope.obj.t74081.push(scope.obj._t74081);
                }
                scope.obj.t74080.T_REQ_DATE = scope.obj.t74080.T_REQ_DATE == undefined ? $filter('date')(new Date(), 'dd-MMM-yyyy') : scope.obj.t74080.T_REQ_DATE;
                var t80 = scope.obj.t74080;
                var t81 = scope.obj.t74081;

                var data = T74120Service.Insert_T74120(t80, t81);
                data.then(function (dt) {
                    //alert('Data saved sucessfully');
                    alert($scope.getSingleMsg('S0012'));
                    $window.location = "/Transaction/T74120";
                    scope.obj.t74080.T_TRANSFER_REQ_NO = dt;
                });
            }

            //scope.today = function () {
            //    scope.obj.t74080.T_REQ_DATE = new Date();
            //};
            //scope.today();


        }]);



