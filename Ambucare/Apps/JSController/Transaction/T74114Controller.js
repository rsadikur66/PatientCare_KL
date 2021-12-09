//For Dropdown List
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
});

app.controller("T74114Controller",
    ["$scope", "$compile", "$filter", "$http", "$window", "T74114Service", "LabelService", "T74114ItemTypeFactory", "T74114ItemNameFactory","Data",
        function (scope, $compile, $filter, $http, $window, T74114Service, LabelService, T74114ItemTypeFactory, T74114ItemNameFactory, Data) {
            //for instance
            scope.obj = {};
            scope.obj = Data;
            scope.obj.T74030 = {};
            scope.obj.T74031 = {};

            // Form Label Data Service Start
            var labelData = LabelService.getlabeldata('T74114');
            labelData.then(function (data) {
                scope.entity = data;
            });

            //Get Supplier List Start
            var SupplierData = T74114Service.GetSupplierList();
            SupplierData.then(function (data) {
                scope.SupplierId = data;
            });
            //Get Supplier List End

            //Get Store List Start
            var StoreData = T74114Service.GetStoreList();
            StoreData.then(function (data) {
                scope.StoreId = data;
            });
            //Get Store List End

            //Get Currency List Start
            var CurrencyData = T74114Service.GetCurrencyList();
            CurrencyData.then(function (data) {
                scope.CurrencyId = data;
            });
            //Get Currency List End

            //Button Add and remove starts
            scope.obj.stockInfo = [];
            scope.Addrow = function () { scope.obj.stockInfo.push({}); }
            scope.RemoveRow = function (index) { scope.obj.stockInfo.splice(index, 1); }
            //Button Add and remove end

            //Item Type popup Start
            scope.ItemList = function (index) {
                var url = '';
                var data = '';
                T74114ItemTypeFactory.getModal(url, data, index);
            };
            //Item Type popup End

            //Item Name popup Start
            scope.ItemName = function (index) {
                var id = scope.obj.stockInfo[index].T_COST_TYPE_ID;
                var url = '';
                var data = '';
                T74114ItemNameFactory.getModal(url, data, index,id);
            };
            //UOM popup Start
          scope.UOM =function(index) {
                var uom = T74114Service.GetUom(scope.obj.stockInfo[index].T_COST_TYPE_ID);
                uom.then(function (data) {
                    scope.uom = data;
                });
            }
            //Save Function Start
            scope.Save = function () {
                scope.obj.t74031 = [];
                for (var i = 0; i < scope.obj.stockInfo.length; i++) {
                    scope.obj._t74031 = {};
                    
                    scope.obj._t74031.T_ITEM_ID = scope.obj.stockInfo[i].T_ITEM_ID;
                    scope.obj._t74031.T_PRIORITY = scope.obj.stockInfo[i].T_PRIORITY;
                    scope.obj._t74031.T_PUR_QTY = scope.obj.stockInfo[i].T_PUR_QTY;
                    scope.obj._t74031.T_ITEM_UM_ID = scope.obj.stockInfo[i].T_ITEM_UM_ID;
                    //scope.obj._t74031.T_ENTRY_USER = scope.obj.T74030.T_ENTRY_USER;
                    //scope.obj._t74031.T_ENTRY_DATE = scope.obj.T74030.T_ENTRY_DATE;
                    scope.obj.t74031.push(scope.obj._t74031);
                }
                scope.obj.T74030.T_PUR_REQ_DATE = scope.obj.T74030.T_PUR_REQ_DATE == undefined ? $filter('date')(new Date(), 'dd-MMM-yyyy') : scope.obj.T74030.T_PUR_REQ_DATE;
                var t30 = scope.obj.T74030;
                var t31 = scope.obj.t74031;

                var data = T74114Service.insert_T74114(t30, t31);
                data.then(function (dt) {
                    //alert('Data saved sucessfully');
                    alert(scope.getSingleMsg('S0012'));
                    scope.obj.T74030.T_PUR_NO = dt;
                });
            }
            //Save Function End

            scope.Clear = function () {
                scope.obj.T74030.T_PUR_NO = '';
                scope.obj.T74030.T_PUR_REQ_DATE =null;
                scope.obj.T74030.T_ISSUE_STATUS = '';
                scope.obj.T74030.T_PUR_ID = '';
                //var myDate = new Date();
                scope.obj.T74030.T_EXP_DLVRY_DATE =null;
                scope.obj.b.selected = '';
                scope.obj.c.selected = '';
                scope.obj.d.selected = '';
                scope.obj.stockInfo = '';
                scope.obj.stockInfo = [];

            }
        }]);