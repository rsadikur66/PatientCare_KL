app.controller("T74027Controller",
    ["$rootScope","$scope", "$filter", "$http", "$window", "T74027Service", "LabelService", "T74114Service", "T74114ItemTypeFactory", "T74114ItemNameFactory", "T74027ItemUomListFactory", "uiGridConstants", "DTOptionsBuilder",
        "DTColumnBuilder", "Data",
        function ($rootScope,scope, $filter, $http, $window, T74027Service, LabelService, T74114Service, T74114ItemTypeFactory, T74114ItemNameFactory, T74027ItemUomListFactory, uiGridConstants, DTOptionsBuilder, DTColumnBuilder, Data) {
            //For Instance 
            scope.obj = {};
            scope.obj = Data;
            scope.obj.stockInfo = {};
            scope.obj._t74027List = [];
            scope.obj.P = {};
            scope.obj.C = {};
            scope.obj.N = {};
            scope.obj.F = {};
            //scope.obj.UpdateBox = '';
            scope.obj.P.selected = {};
            scope.obj.C.selected = {};
            scope.obj.F.selected = {};

            scope.obj.T_STOCK_DATE = new Date();


            // Form Label Data Service Start 
            var labelData = LabelService.getlabeldata('T74027');
            labelData.then(function (data) {
                scope.entity = data;
            });
            
            scope.Update_Click = function () {

                if (scope.obj.Update === '2') {
                    scope.obj.stockInfo = '';
                    scope.obj.stockInfo = [];
                    scope.Addrow = function () {
                        scope.obj.stockInfo.push({
                            T_COMPANY_ID: scope.obj.T_COMPANY_ID,
                            T_STORE_ID: scope.obj.T_STORE_ID,
                            T_STOCK_DATE: scope.obj.T_STOCK_DATE,
                            T_BATCH: scope.obj.T_BATCH,
                            T_SUPPLIER_ID: scope.obj.T_SUPPLIER_ID,
                            T_LOT_NO: scope.obj.T_LOT_NO,
                            T_ITEM_ID: scope.obj.T_ITEM_ID,
                            T_UNIT_VALUE: '',
                            T_TOTAL_VALUE: '',
                            T_PURCHASE_QTY: '',
                            T_TRNSACTION_QTY: '',
                            //var d = data[i].T_MF_DATE;
                            //result.push(moment(Number(d.match(/\d+/)[0])).format('MM/DD/YYYY'));
                            //scope.obj.test.T_MF_DATE = moment(Number(d.match(/\d+/)[0])).format('MM/DD/YYYY');
                            T_MF_DATE: scope.obj.T_MF_DATE,
                            T_EXPIRE_DATE: scope.obj.T_EXPIRE_DATE
                        });
                    }
                } else if (scope.obj.UpdateBox === '1') {
                    if (scope.obj.T_COMPANY_ID != null) {
                        var GridData = T74027Service.GetGridAllData(scope.obj.T_STORE_ID);
                        GridData.then(function(data) {
                            scope.obj.F.selected = {};
                            scope.obj.stockInfo = [];
                            for (var i = 0; i < data.length; i++) {
                                scope.obj.test = {};
                                scope.obj.test.T_CUR_STOCK_ID = data[i].T_CUR_STOCK_ID;
                                scope.obj.test.T_COST_TYPE_ID = data[i].Type_Id;
                                scope.obj.test.T_LANG2_NAME = data[i].Type_Name;
                                scope.obj.test.T_ITEM_ID = data[i].T_ITEM_ID;
                                scope.obj.test.CostType = data[i].itemName;
                                scope.obj.test.T_BATCH = data[i].Batch;
                                scope.obj.test.T_LOT_NO = data[i].Lot_no;
                                //scope.obj.T_STOCK_DATE = data[i].stock_date;
                                if (data[i].stock_date == null) {
                                    //var myDate = new Date();
                                    //scope.obj.test.T_MF_DATE = $filter('date')(myDate, '');
                                } else {
                                    var stockdate = data[i].stock_date;
                                    scope.obj.T_STOCK_DATE = moment(Number(stockdate.match(/\d+/)[0])).format('');
                                }
                                scope.obj.F.selected.T_SUPPLIER_ID = data[i].T_SUPPLIER_ID;
                                scope.obj.F.selected.T_LANG2_NAME = data[i].T_LANG2_NAME;
                                if (data[i].T_MF_DATE == null) {
                                    //var myDate = new Date();
                                    //scope.obj.test.T_MF_DATE = $filter('date')(myDate, '');
                                } else {
                                    var e = data[i].T_MF_DATE;
                                    scope.obj.test.T_MF_DATE = moment(Number(e.match(/\d+/)[0])).format('');
                                }

                                if (data[i].T_EXPIRE_DATE == null) {
                                    //var myDate1 = new Date();
                                    //scope.obj.test.T_EXPIRE_DATE = $filter('date')(myDate1, '');
                                } else {
                                    var f = data[i].T_EXPIRE_DATE;
                                    scope.obj.test.T_EXPIRE_DATE = moment(Number(f.match(/\d+/)[0])).format('');
                                }

                                scope.obj.test.T_ITEM_UM_ID = data[i].UM_id;
                                scope.obj.test.UomName = data[i].UM_Name;
                                //var d = data[i].T_MF_DATE;
                                //scope.obj.test.T_MF_DATE = moment(Number(d.match(/\d+/)[0])).format('MM/DD/YYYY');
                                //var e = data[i].T_EXPIRE_DATE;
                                //scope.obj.test.T_EXPIRE_DATE = moment(Number(e.match(/\d+/)[0])).format('MM/DD/YYYY');
                                //scope.obj.test.T_EXPIRE_DATE = json.stringify(data[i].T_EXPIRE_DATE);

                                scope.obj.test.T_UNIT_VALUE = data[i].T_UNIT_VALUE;
                                scope.obj.test.T_TRNSACTION_QTY = data[i].T_TRNSACTION_QTY;
                                scope.obj.test.T_PURCHASE_QTY = data[i].T_PURCHASE_QTY;
                                scope.obj.test.T_TOTAL_VALUE = data[i].T_TOTAL_VALUE;

                                scope.obj.stockInfo.push(scope.obj.test);
                            } //end for loop
                        });
                    } else {
                        alert("Must Select Company ID !!!");
                    }
                }
            };

            //For Company DropDown Start 
            var companyData = T74027Service.GetCompanysData();
            companyData.then(function (data) {
                if (data.length>0) {
                    scope.compId = data;
                    scope.obj.T_COMPANY_ID = data[0].T_COMPANY_ID;
                    scope.obj.C.selected = { T_COMPANY_ID: data[0].T_COMPANY_ID, T_LANG2_NAME: data[0].T_LANG2_NAME };
                }

                
            });


            //For Store DropDown Start 
            var StoreData = T74027Service.GetStoresData();
            StoreData.then(function (data) {
                scope.storesId = data;
            });

            //For Supplier DropDownList Start
            var SupplierData = T74027Service.GetSupplierData();
            SupplierData.then(function (data) {
                if (data.length>0) {
                    scope.supplierId = data;
                    scope.obj.T_SUPPLIER_ID = data[0].T_SUPPLIER_ID;
                    scope.obj.F.selected = { T_SUPPLIER_ID: data[0].T_SUPPLIER_ID, T_LANG2_NAME: data[0].T_LANG2_NAME };
                }
                
            });

            //Item Type popup Start
            scope.ItemTypeList = function (index) {
                var url = '';
                var data = '';
                T74114ItemTypeFactory.getModal(url, data, index);
            };

            //Item Name popup Start
            scope.ItemName = function (index) {
                var id = scope.obj.stockInfo[index].T_COST_TYPE_ID;
                var url = '';
                var data = '';
                T74114ItemNameFactory.getModal(url, data, index, id);

            };


            //Item UOM Name popup Start
            scope.ItemUOMname = function (index, kkl) {
              

                var id = scope.obj.stockInfo[index].T_COST_TYPE_ID;
                var url = '';
                var data = '';
                T74027ItemUomListFactory.getModal(url, data, index, id);


            };

            scope.checkItemUM = function (index, umid, itemid) {
            
                for (i = 0; i < scope.obj.stockInfo.length; i++) {
                    var item = scope.obj.stockInfo[i].T_ITEM_ID;
                    var um = scope.obj.stockInfo[i].T_ITEM_UM_ID;
                    if (itemid == item && umid == um && index != i) {
                        scope.obj.stockInfo[index].UomName = '';
                        scope.obj.stockInfo[index].T_ITEM_UM_ID = '';
                        alert("Item and Pack size is already exist!!");
                    }
                }
            }


            //all grid data start
          scope.CheckGridAllData = function (g) {
              scope.loader(true);
                if (scope.obj.UpdateBox === '1') {
                    if (scope.obj.T_COMPANY_ID != null) {
                        var GridData = T74027Service.GetGridAllData(g.T_STORE_ID, scope.obj.T_COMPANY_ID);
                        GridData.then(function (data) {
                            scope.obj.F.selected = {};
                            scope.obj.stockInfo = [];
                            for (var i = 0; i < data.length; i++) {
                                scope.obj.test = {};
                                scope.obj.test.T_CUR_STOCK_ID = data[i].T_CUR_STOCK_ID;
                                scope.obj.test.T_COST_TYPE_ID = data[i].Type_Id;
                                scope.obj.test.T_LANG2_NAME = data[i].Type_Name;
                                scope.obj.test.T_ITEM_ID = data[i].T_ITEM_ID;
                                scope.obj.test.CostType = data[i].itemName;
                                scope.obj.test.T_BATCH = data[i].Batch;
                                scope.obj.test.T_LOT_NO = data[i].Lot_no;
                                //scope.obj.T_STOCK_DATE = data[i].stock_date;
                                if (data[i].stock_date == null) {
                                    //var myDate = new Date();
                                    //scope.obj.test.T_MF_DATE = $filter('date')(myDate, '');
                                } else {
                                    var stockdate = data[i].stock_date;
                                    scope.obj.T_STOCK_DATE = moment(Number(stockdate.match(/\d+/)[0])).format('');
                                }
                                scope.obj.F.selected.T_SUPPLIER_ID = data[i].T_SUPPLIER_ID;
                                scope.obj.F.selected.T_LANG2_NAME = data[i].T_LANG2_NAME;
                                if (data[i].T_MF_DATE == null) {
                                    //var myDate = new Date();
                                    //scope.obj.test.T_MF_DATE = $filter('date')(myDate, '');
                                } else {
                                    var e = data[i].T_MF_DATE;
                                    scope.obj.test.T_MF_DATE = moment(Number(e.match(/\d+/)[0])).format('');
                                }

                                if (data[i].T_EXPIRE_DATE == null) {
                                    //var myDate1 = new Date();
                                    //scope.obj.test.T_EXPIRE_DATE = $filter('date')(myDate1, '');
                                } else {
                                    var f = data[i].T_EXPIRE_DATE;
                                    scope.obj.test.T_EXPIRE_DATE = moment(Number(f.match(/\d+/)[0])).format('');
                                }

                                scope.obj.test.T_ITEM_UM_ID = data[i].UM_id;
                                scope.obj.test.UomName = data[i].UM_Name;

                                //var d = data[i].T_MF_DATE;
                                //scope.obj.test.T_MF_DATE = moment(Number(d.match(/\d+/)[0])).format('MM/DD/YYYY');

                                //var e = data[i].T_EXPIRE_DATE;
                                //scope.obj.test.T_EXPIRE_DATE = moment(Number(e.match(/\d+/)[0])).format('MM/DD/YYYY');

                                //scope.obj.test.T_EXPIRE_DATE = json.stringify(data[i].T_EXPIRE_DATE);
                                scope.obj.test.T_UNIT_VALUE = data[i].T_UNIT_VALUE;
                                scope.obj.test.T_TRNSACTION_QTY = data[i].T_TRNSACTION_QTY;
                                scope.obj.test.T_PURCHASE_QTY = data[i].T_PURCHASE_QTY;
                                scope.obj.test.T_TOTAL_VALUE = data[i].T_TOTAL_VALUE;

                              scope.obj.stockInfo.push(scope.obj.test);
                                scope.loader(false);
                            }//end for loop
                        });

                    } else { alert(scope.getSingleMsg('S0013')); }
                }
            }

            //Button Add and remove starts
            scope.obj.stockInfo = [];
            scope.Addrow = function () {
                scope.obj.stockInfo.push({
                    T_COMPANY_ID: scope.obj.T_COMPANY_ID,
                    T_STORE_ID: scope.obj.T_STORE_ID,
                    T_STOCK_DATE: scope.obj.T_STOCK_DATE,
                    T_BATCH: scope.obj.T_BATCH,
                    T_SUPPLIER_ID: scope.obj.T_SUPPLIER_ID,
                    T_LOT_NO: scope.obj.T_LOT_NO,
                    T_ITEM_ID: scope.obj.T_ITEM_ID,
                    T_UNIT_VALUE: '',
                    T_TOTAL_VALUE: '',
                    T_PURCHASE_QTY: '',
                    T_TRNSACTION_QTY: '',
                    //var d = data[i].T_MF_DATE;
                    //result.push(moment(Number(d.match(/\d+/)[0])).format('MM/DD/YYYY'));
                    //scope.obj.test.T_MF_DATE = moment(Number(d.match(/\d+/)[0])).format('MM/DD/YYYY');
                    T_MF_DATE: scope.obj.T_MF_DATE,
                    T_EXPIRE_DATE: scope.obj.T_EXPIRE_DATE
                });
            }

            //Button Add and remove end
            scope.obj.assignList = [];
            scope.RemoveRow = function (index) {
                scope.obj.assignList.push({ T_CUR_STOCK_ID: scope.obj.stockInfo[index].T_CUR_STOCK_ID });
                scope.obj.stockInfo.splice(index, 1);
            }

            //For Report
            scope.Print = function (companyid, storeid) {
                $window.open("../T74027Report/GetGridAllDataReport?storeid=" + storeid, "popup", "width=600,height=600,left=50,top=50");


            }

            scope.Save_Click = function() {
                save_click();
            };


            var disable = $rootScope.$on('T74027Emit', function (event, data) {
                if (data == 's') {
                    save_click();
                }
            });
            scope.$on('$destroy', function () {
                disable();
            });

            function save_click() {
                if (scope.obj.T_COMPANY_ID != null) {
                    scope.obj.T_CUR_STOCK_ID = '';

                    if (scope.obj.T_CUR_STOCK_ID != null) {
                        if (scope.obj.T_TYPE_ID != null) {
                            scope.obj.stockInfo.forEach(function (obj) {
                                var update = T74027Service.insert_T74027(scope.obj.stockInfo);
                                update.then(function (data) {
                                    $window.location = "";
                                });
                            });
                            //alert("Data Update Succesfully");
                            alert(scope.getSingleMsg('S0002'));
                        } else {
                            if (scope.obj.assignList.length > 0) {
                                scope.obj._t74027Del = scope.obj.assignList;
                                var Del = T74027Service.Del_t74027(scope.obj._t74027Del);
                            }

                            scope.obj._t74027List = scope.obj.stockInfo;
                            var save = T74027Service.insert_T74027(scope.obj._t74027List);
                            save.then(function (data) {
                                //alert("Data Save Succesfully");
                                alert(scope.getSingleMsg('S0003'));
                                $window.location = "";

                            });

                        }
                    } else {
                        //var index = 0;
                        //scope.obj.stockInfo.forEach(function (obj) {
                        //    var update = T74027Service.insert_T74027(scope.obj.stockInfo);
                        //    update.then(function (data) {
                        //        $window.location = "";

                        //    });
                        //});
                        //alert("Data Update Succesfully");
                        alert("kkkk");
                    }

                } else {
                    alert(scope.getSingleMsg('S0013'));
                }
            }
        }]);


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
