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
app.controller("T74121Controller", ["$rootScope","$scope", "$compile", "$filter", "$http", "$window", "T74121Service", "Data", "GenFactory","LabelService",
    function ($rootScope,scope, $compile, $filter, $http, $window, T74121Service, Data, GenFactory, LabelService) {
        //for instance
        scope.obj = {};
        scope.obj = Data;
        scope.obj.SetProfitValue = '';
        // Form Label Data Service Start
        var labelData = LabelService.getlabeldata('T74121');
        labelData.then(function (data) {
            scope.entity = data;
        });
        scope.Generic_Click = function() {

            var url = '';
            var data = '';

            GenFactory.getModal(url, data);

        }

        var type = T74121Service.getTypeData();
        type.then(function(data) {
            scope.TypeList = data;
        });

        scope.Type_Click = function (id) {
            if (id === 23) {
                scope.ItemType_List = false;
               
                // document.getElementById('Div_Generic').visibility = true;
                 scope.Generic = true;
                 scope.Phar_List = true;
                 scope.obj.ProductList = [];
                 scope.obj.ItemTypeList = [];
                scope.obj.Hidden = 'Ph';
                // chkGrid = 'Ph';
            } else {
                // scope.ProductList = [];
                scope.obj.GEN_DESC = '';
                scope.obj.GEN_CODE = '';
                scope.obj.SetProfitValue = '';
                scope.Generic = false;
                scope.Phar_List = false;
              //  scope.ChekPriceSet = '2';
                scope.obj.ChekPriceSet = '2';
                //scope.obj.UomList = [];
                scope.ProductList = [];
                scope.obj.Hidden = 'Ty';
                var productType = T74121Service.getProTypeData(id);
                productType.then(function(data) {
                    scope.obj.ItemTypeList = data;
                    scope.obj.itemtypeListSave = data;
                    scope.Phar_List = false;
                    scope.ItemType_List = true;
                });
            }
        }

        scope.Save_Click = function() {
            Save_Click();
        };
        //scope.SalePrice_Click = function (price, ite, um) {

        //    scope.ProductSetPricList = [];
        //    for (var i = 0; i < scope.obj.ProductList.length; i++) {
        //        scope.obj.Uom = {};
        //        scope.obj.Uom.PList = [];
        //        scope.obj.Uom.T_ITEM_ID = scope.obj.ProductList[i].T_ITEM_ID;
        //        scope.obj.Uom.ITEM_NAME = scope.obj.ProductList[i].ITEM_NAME;
        //        scope.obj.Uom.T_COST_TYPE_DTL_ID = scope.obj.ProductList[i].T_COST_TYPE_DTL_ID;
        //        for (var k = 0; k < scope.obj.ProductList[i].PList.length; k++) {
        //            scope.obj.newObject = {};
        //            //---------------------
        //            if (scope.obj.ProductList[i].T_ITEM_ID === ite && scope.obj.ProductList[i].PList[k].T_ITEM_UM_ID === um) {
        //                scope.obj.newObject.chkRowPrice = '1';
        //               // scope.obj.SetFocus = true;
        //                //scope.obj.newObject.chkRowPrice = scope.obj.ProductList[i].PList[k].chkRowPrice; //scope.obj.UomList[i].ChekPriceSet;
        //                scope.obj.newObject.T_ITEM_UM_ID = scope.obj.ProductList[i].PList[k].T_ITEM_UM_ID;
        //                scope.obj.newObject.PACK_SIZE = scope.obj.ProductList[i].PList[k].PACK_SIZE;
        //                scope.obj.newObject.PUR_PRICE = scope.obj.ProductList[i].PList[k].PUR_PRICE;
        //                scope.obj.newObject.SALE_PRICE = scope.obj.ProductList[i].PList[k].SALE_PRICE;
        //               // document.getElementById('inputPrice' + ite + um).element.focus();
        //                //angular.element('inputPrice' + ite + um).focus();
                       
        //            } else {
        //                scope.obj.newObject.chkRowPrice = scope.obj.ProductList[i].PList[k].chkRowPrice;
        //                scope.obj.newObject.T_ITEM_UM_ID = scope.obj.ProductList[i].PList[k].T_ITEM_UM_ID;
        //                scope.obj.newObject.PACK_SIZE = scope.obj.ProductList[i].PList[k].PACK_SIZE;
        //                scope.obj.newObject.PUR_PRICE = scope.obj.ProductList[i].PList[k].PUR_PRICE;
        //                scope.obj.newObject.SALE_PRICE = scope.obj.ProductList[i].PList[k].SALE_PRICE;
        //            }
                    
        //            //------------------
                   
        //            scope.obj.Uom.PList.push(scope.obj.newObject);
        //        }
        //        scope.ProductSetPricList.push(scope.obj.Uom);
        //    }
        //    scope.obj.ProductList = scope.ProductSetPricList;
        //   // angular.element(document.getElementById('txtSetProfitValue')).focus();
        //    //angular.element('inputPrice' + ite + um).focus();
        //    //var dd = $window.document.getElementById('inputPrice' + ite + um);
        //   // dd.focus= true;
            
        //}
        //scope.CloseReceivePopup = function (id) {
        //    document.getElementById(id).style.display = "none";
        //}
        
        scope.$watch('obj.GEN_CODE', getGridValue = function () {
            
            if (scope.obj.GEN_CODE !== undefined) {
                if (scope.obj.GEN_CODE !== "") {
               
                    scope.obj.ProductList = [];
                   
                var product = T74121Service.GetProductList(scope.obj.GEN_CODE);
                product.then(function (data) {
                    scope.dt = data;
                    if (scope.obj.ProductList.length === 0) {
                       
                        for (var i = 0; i < scope.dt.length; i++) {
                           
                            var uomList = T74121Service.getUomList('23', scope.obj.GEN_CODE, scope.dt[i].T_ITEM_ID);
                            uomList.then(function (uom) {
                                scope.obj.dummy = {};
                                scope.obj.dummy.PList = [];
                                scope.obj.dummy.T_ITEM_ID = uom[0].T_ITEM_ID;
                                scope.obj.dummy.ITEM_NAME = uom[0].ITEM_NAME;
                                scope.obj.dummy.T_COST_TYPE_DTL_ID = uom[0].T_COST_TYPE_DTL_ID;
                               
                                for (var j = 0; j < uom.length; j++) {
                                    scope.obj.demo2 = {};
                                    scope.obj.demo2.T_ITEM_UM_ID = uom[j].T_ITEM_UM_ID;
                                    scope.obj.demo2.PACK_SIZE = uom[j].PACK_SIZE;
                                    scope.obj.demo2.PUR_PRICE = uom[j].PUR_PRICE;
                                    scope.obj.demo2.SALE_PRICE = uom[j].SALE_PRICE;

                                    scope.obj.dummy.PList.push(scope.obj.demo2);
                                }
                                if (scope.dt.length > scope.obj.ProductList.length) {
                                    scope.obj.ProductList.push(scope.obj.dummy);
                                }
                              
                               
                            });
                           
                            
                        }
                     
                    }
                   
                });
               
                }
            }
        });

        //scope.ProductList = [];
      // scope.obj.UomList = [];
        scope.SetPrice_Click = function () {
            if (scope.obj.ChekPriceSet === '1') {
                if (scope.obj.Hidden === 'Ph') {
                    scope.ProductSetPricList = [];
                    for (var i = 0; i < scope.obj.ProductList.length; i++) {
                        scope.obj.Uom = {};
                        scope.obj.Uom.PList = [];
                        scope.obj.Uom.T_ITEM_ID = scope.obj.ProductList[i].T_ITEM_ID;
                        scope.obj.Uom.ITEM_NAME = scope.obj.ProductList[i].ITEM_NAME;
                        scope.obj.Uom.T_COST_TYPE_DTL_ID = scope.obj.ProductList[i].T_COST_TYPE_DTL_ID;
                        for (var k = 0; k < scope.obj.ProductList[i].PList.length; k++) {
                            scope.obj.newObject = {};
                            scope.obj.newObject.chkRowPrice = '1'; //scope.obj.UomList[i].ChekPriceSet;
                            scope.obj.newObject.T_ITEM_UM_ID = scope.obj.ProductList[i].PList[k].T_ITEM_UM_ID;
                            scope.obj.newObject.PACK_SIZE = scope.obj.ProductList[i].PList[k].PACK_SIZE;
                            scope.obj.newObject.PUR_PRICE = scope.obj.ProductList[i].PList[k].PUR_PRICE;
                            var purPrice = scope.obj.ProductList[i].PList[k].PUR_PRICE;
                            scope.obj.newObject.SALE_PRICE = (purPrice * scope.obj.SetProfitValue) / 100 + purPrice;
                            scope.obj.Uom.PList.push(scope.obj.newObject);
                        }
                        scope.ProductSetPricList.push(scope.obj.Uom);
                    }
                    scope.obj.ProductList = scope.ProductSetPricList; //.push(scope.obj.Uom);
                   
                    //scope.obj.UomList = [];
                    //scope.obj.UomList = scope.obj.Uom;
                } else {
                    scope.obj.ItemType = [];
                    for (var j = 0; j < scope.obj.ItemTypeList.length; j++) {
                        scope.obj.newObject = {};
                        scope.obj.newObject.Type_ID = scope.obj.ItemTypeList[j].Type_ID;
                        scope.obj.newObject.Type_NAME = scope.obj.ItemTypeList[j].Type_NAME;
                        scope.obj.newObject.Item_Name = scope.obj.ItemTypeList[j].Item_Name;

                        scope.obj.newObject.T_ITEM_UM_ID = scope.obj.ItemTypeList[j].T_ITEM_UM_ID;
                        scope.obj.newObject.T_LANG2_NAME = scope.obj.ItemTypeList[j].T_LANG2_NAME;

                        scope.obj.newObject.Pre_Price = scope.obj.ItemTypeList[j].Pre_Price;
                        var prePrice = scope.obj.ItemTypeList[j].Pre_Price;
                        var currentPrice = ((prePrice * scope.obj.SetProfitValue) / 100 + prePrice).toFixed(3);
                        scope.obj.newObject.Curr_Price = parseFloat(currentPrice);
                        scope.obj.ItemType.push(scope.obj.newObject);
                       
                    }
                    scope.obj.ItemTypeList = [];
                    scope.obj.ItemTypeList = scope.obj.ItemType;
                    
                }
                
            } else {

                if (scope.obj.Hidden === 'Ph') {
                    scope.ProductSetPricList = [];
                    for (var a = 0; a < scope.obj.ProductList.length; a++) {
                        scope.obj.Uom = {};
                        scope.obj.Uom.PList = [];
                        scope.obj.Uom.T_ITEM_ID = scope.obj.ProductList[a].T_ITEM_ID;
                        scope.obj.Uom.ITEM_NAME = scope.obj.ProductList[a].ITEM_NAME;
                        scope.obj.Uom.T_COST_TYPE_DTL_ID = scope.obj.ProductList[a].T_COST_TYPE_DTL_ID;
                        for (var b = 0; b < scope.obj.ProductList[a].PList.length; b++) {
                            scope.obj.newObject = {};
                            scope.obj.newObject.chkRowPrice = '2'; //scope.obj.UomList[i].ChekPriceSet;
                            scope.obj.newObject.T_ITEM_UM_ID = scope.obj.ProductList[a].PList[b].T_ITEM_UM_ID;
                            scope.obj.newObject.PACK_SIZE = scope.obj.ProductList[a].PList[b].PACK_SIZE;
                            scope.obj.newObject.PUR_PRICE = scope.obj.ProductList[a].PList[b].PUR_PRICE;
                           // var purPrice = scope.obj.ProductList[a].PList[b].PUR_PRICE;
                            scope.obj.newObject.SALE_PRICE = scope.obj.ProductList[a].PList[b].PUR_PRICE;  //(purPrice * scope.obj.SetProfitValue) / 100 + purPrice;
                            scope.obj.Uom.PList.push(scope.obj.newObject);
                        }
                        scope.ProductSetPricList.push(scope.obj.Uom);
                    }
                    scope.obj.ProductList = scope.ProductSetPricList; //.push(scope.obj.Uom);
                    scope.obj.SetProfitValue = '';
                    //scope.obj.UomList = [];
                    //scope.obj.UomList = scope.obj.Uom;
                } else {
                    scope.obj.ItemType = [];
                    for (var c = 0; c < scope.obj.ItemTypeList.length; c++) {
                        scope.obj.newObject = {};
                        scope.obj.newObject.Type_ID = scope.obj.ItemTypeList[c].Type_ID;
                        scope.obj.newObject.Type_NAME = scope.obj.ItemTypeList[c].Type_NAME;
                        scope.obj.newObject.Item_Name = scope.obj.ItemTypeList[c].Item_Name;
                        scope.obj.newObject.Pre_Price = scope.obj.ItemTypeList[c].Pre_Price;
                       // var prePrice = scope.obj.ItemTypeList[c].Pre_Price;
                       // var currentPrice = ((prePrice * scope.obj.SetProfitValue) / 100 + prePrice).toFixed(3);
                        scope.obj.newObject.Curr_Price = scope.obj.ItemTypeList[c].Pre_Price; //parseFloat(currentPrice);
                        scope.obj.ItemType.push(scope.obj.newObject);

                    }
                    scope.obj.ItemTypeList = [];
                    scope.obj.ItemTypeList = scope.obj.ItemType;
                    scope.obj.SetProfitValue = '';
                }


            }
            
        }
        scope.RowChk_Click = function (item, um) {
            document.getElementById('inputPrice' + item + um).focus();
            document.getElementById('inputPrice' + item + um).readOnly = false;//scope.PriceSet
            
        }

        

        //scope.ProductList = [];
        //scope.obj.UomList = [];
        //scope.setClickedRow = function (pindex, chin, ob) {
        //    for (var i = 0; i < scope.ProductList.length; i++) {
        //        if (pindex === i) {
        //           var tableId = angular.element(document.getElementById('tbl{{ParentIn }}')).val();
        //            for (var j = 0; j < scope.obj.UomList.length; j++) {
        //                    scope.obj.newObj = {};
        //                    scope.obj.newObj.SALE_PRICE = scope.obj.UomList[j].SALE_PRICE;
        //            }
        //            scope.obj.UomList.push(scope.obj.newObj);
        //        }
        //    }
        //   // document.getElementById(id).style.display = "none";
        //}

        scope.$watch('selectedRow', function () {
            console.log('Do Some processing'); //runs the block whenever selectedRow is changed. 
        });
        var disable = $rootScope.$on('T74121Emit', function (event, data) {
            if (data == 'p') {
                Save_Click();
            }
        });
        scope.$on('$destroy', function () {
            disable();
        });


        function Save_Click() {
            scope.obj.T74089 = [];
            if (scope.obj.Hidden === 'Ty') {
                for (var i = 0; i < scope.obj.ItemTypeList.length; i++) {
                    if (scope.obj.ItemTypeList[i].Curr_Price !== scope.obj.ItemTypeList[i].Pre_Price) {

                        scope.newObjItemType = {};
                        scope.newObjItemType.T_COST_TYPE_DTL_ID = scope.obj.ItemTypeList[i].Type_ID;
                        scope.newObjItemType.T_ITEM_UM_ID = scope.obj.ItemTypeList[i].T_ITEM_UM_ID;
                        scope.newObjItemType.T_PUR_PRICE = scope.obj.ItemTypeList[i].Curr_Price;
                        scope.newObjItemType.T_SALE_PRICE = scope.obj.ItemTypeList[i].Curr_Price;

                        scope.obj.T74089.push(scope.newObjItemType);

                    }

                }

            } else {
                for (var j = 0; j < scope.obj.ProductList.length; j++) {

                    // var T_ITEM_ID = scope.obj.ProductList[j].T_ITEM_ID;
                    var T_COST_TYPE_DTL_ID = scope.obj.ProductList[j].T_COST_TYPE_DTL_ID;
                    for (var k = 0; k < scope.obj.ProductList[j].PList.length; k++) {
                        scope.newObjItemType = {};
                        var ck = scope.obj.ProductList[j].PList[k].chkRowPrice;
                        if (ck === '1') {
                            scope.newObjItemType.T_COST_TYPE_DTL_ID = T_COST_TYPE_DTL_ID;
                            scope.newObjItemType.T_ACTIVE = ck;
                            scope.newObjItemType.T_ITEM_UM_ID = scope.obj.ProductList[j].PList[k].T_ITEM_UM_ID;
                            scope.newObjItemType.T_PUR_PRICE = scope.obj.ProductList[j].PList[k].PUR_PRICE;
                            scope.newObjItemType.T_SALE_PRICE = scope.obj.ProductList[j].PList[k].SALE_PRICE;
                            scope.obj.T74089.push(scope.newObjItemType);
                        }
                    }
                }
            }

            if (scope.obj.T74089.length !== 0) {
                var save = T74121Service.saveData(scope.obj.T74089);
                save.then(function (data) {
                    //alert('Save Successfully');
                    alert(scope.getSingleMsg('S0012'));
                    scope.obj.T = {};
                    scope.obj.ProductList = [];
                    scope.obj.SetProfitValue = '';
                    scope.obj.ChekPriceSet = '2';
                    scope.Search = '';
                    scope.obj.GEN_DESC = '';
                    scope.Phar_List = false;
                    scope.obj.T.selected = '';
                    $window.location.reload();
                });
            } else {
                // alert('You did not change any value !!!!');
                var asd = scope.getSingleMsg('S0027');
                alert(scope.getSingleMsg('S0027'));
            }
        };
    }]);

app.directive('arrowSelector', ['$document', function ($document) {
    return {
        restrict: 'A',
        link: function (scope, elem, attrs, ctrl) {
            var elemFocus = false;
            elem.on('mouseenter', function () {
                elemFocus = true;
            });
            elem.on('mouseleave', function () {
                elemFocus = false;
            });
            $document.bind('keydown', function (e) {
                if (elemFocus) {
                    if (e.keyCode == 38) {
                        console.log(scope.selectedRow);
                        if (scope.selectedRow == 0) {
                            return;
                        }
                        scope.selectedRow--;
                        scope.$apply();
                        e.preventDefault();
                    }
                    if (e.keyCode == 40) {
                        if (scope.selectedRow == scope.foodItems.length - 1) {
                            return;
                        }
                        scope.selectedRow++;
                        scope.$apply();
                        e.preventDefault();
                    }
                }
            });
        }
    };
}]);