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

app.controller("T74117Controller",
    ["$scope", "$compile", "$filter", "$http", "$window", "T74117Service", "LabelService", "T74114ItemTypeFactory", "T74114ItemNameFactory", "T74117PersonalTypeFactory","T74117PersonalNameFactory", "Data",
        function (scope, $compile, $filter, $http, $window, T74117Service, LabelService, T74114ItemTypeFactory, T74114ItemNameFactory, T74117PersonalTypeFactory, T74117PersonalNameFactory, Data) {
            //for instance
            scope.obj = {};
            scope.obj = Data;
            scope.obj.T74048 = {};
            scope.obj.T74049 = {};
            //scope.Div_Person = false;
            
            scope.obj.a = {};
            scope.obj.f = {};
            scope.obj.t = {};
            scope.obj.p = {};
            // Form Label Data Service Start
            var labelData = LabelService.getlabeldata('T74117');
            labelData.then(function (data) {
                scope.entity = data;
            });

            //Get Supplier List Start
            var SupplierData = T74117Service.GetSupplierList();
            SupplierData.then(function (data) {
                scope.SupplierId = data;
            });
            //Get Supplier List End

            //Get Currency List Start
            var CurrencyData = T74117Service.GetCurrencyList();
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
                T74114ItemNameFactory.getModal(url, data, index, id);
            };
            //Item Name popup End
            scope.UOM = function (index) {
                var uom = T74117Service.GetUom(scope.obj.stockInfo[index].T_COST_TYPE_ID);
                uom.then(function (data) {
                    scope.uom = data;
                });
            }
            //Save Function Start
            scope.Save = function () {
                scope.obj.t74049 = [];
                for (var i = 0; i < scope.obj.stockInfo.length; i++) {
                    scope.obj._t74049 = {};
                    scope.obj._t74049.T_ITEM_ID = scope.obj.stockInfo[i].T_ITEM_ID;
                    scope.obj._t74049.T_PRIORITY = scope.obj.stockInfo[i].T_PRIORITY;
                    scope.obj._t74049.T_SL_QTY = scope.obj.stockInfo[i].T_SL_QTY;
                    scope.obj._t74049.T_ITEM_UM_ID = scope.obj.stockInfo[i].T_ITEM_UM_ID;
                    scope.obj.t74049.push(scope.obj._t74049);
                }
                scope.obj.T74048.T_SL_REQ_DATE = scope.obj.T74048.T_SL_REQ_DATE == undefined ? $filter('date')(new Date(), 'dd-MMM-yyyy') : scope.obj.T74048.T_SL_REQ_DATE;
                var t48 = scope.obj.T74048;
                var t49 = scope.obj.t74049;

                var data = T74117Service.insert_T74117(t48, t49);
                data.then(function (dt) {
                   // alert('Data saved sucessfully');
                    alert(scope.getSingleMsg('S0012'));
                    scope.obj.T74048.T_SL_REQ_NO = dt;
                });
            }
            //Save Function End

            //Get Store From Start
            var StoreDataFrom = T74117Service.GetStoreListFrom();
            StoreDataFrom.then(function (data) {
                scope.StoreIdFrom = data;
            });
            //Get Store From End

            //Get Store To Start
            var StoreDataTo = T74117Service.GetStoreListTo();
            StoreDataTo.then(function (data) {
                scope.StoreIdTo = data;
            });
            //Get Store To End
            //For Check Two Dropdown Value are Same  Start
            scope.checkStoreTo = function () {
                if (scope.obj.T74048.T_STR_FR_ID === scope.obj.T74048.T_STR_TO_ID) {
                    //alert("Stores can not be same");
                    alert(scope.getSingleMsg('S0023'));
                    scope.obj.t.selected = { T_LANG2_NAME: 'Select' };
                }
            }
            scope.checkStoreFrom = function () {
                if (scope.obj.T74048.T_STR_FR_ID === scope.obj.T74048.T_STR_TO_ID) {
                    //alert("Stores can not be same");
                    alert($scope.getSingleMsg('S0023'));
                    scope.obj.f.selected = { T_LANG2_NAME: 'Select' };
                }
            }
            //For Check Two Dropdown Value are Same  Start

            scope.ChekPersonal_Click = function () {
                if (scope.check == 1) {
                    scope.Div_Person = true;
                    scope.obj.T74048.T_LANG2_NAME = '';
                    scope.obj.T74048.T_EMP_TYP_ID = '';
                    scope.obj.T74048.T_NAME = '';
                    scope.obj.T74048.T_EMP_ID = '';
                } else {
                    scope.Div_Person = false;
                }
            }

            scope.PersonalType = function () {
                var url = '';
                var data = '';
                T74117PersonalTypeFactory.getModal(url, data);
            }

            scope.PersonName = function (T_EMP_TYP_ID) {
                var url = '';
                var data = '';
                T74117PersonalNameFactory.getModal(url, data, T_EMP_TYP_ID);
            }

            //Clear Function start
            scope.Clear = function() {
                scope.obj.T74048.T_SL_REQ_NO = '';
                scope.obj.T74048.T_SL_REQ_DATE = '';
                scope.obj.T74048.T_EXP_DLVRY_DATE = '';
                scope.obj.a.selected = '';
                scope.obj.T74048.T_LANG2_NAME = '';
                scope.obj.f.selected = '';
                scope.obj.t.selected = '';
                scope.check = 2;
                scope.obj.p.selected = '';
                scope.obj.T74048.T_NAME = '';
                //scope.obj.T74048.T_SL_REQ_DATE == undefined ? $filter('date')(new Date(), 'dd-MMM-yyyy') : scope.obj.T74048.T_SL_REQ_DATE;
            }
        }]);