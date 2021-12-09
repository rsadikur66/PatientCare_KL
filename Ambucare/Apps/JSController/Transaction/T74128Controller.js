
app.controller("T74128Controller",["$scope", "$location", "$compile", "$filter", "$http", "$window", "T74128Service", "Data",
        function(scope, $location, $compile, $filter, $http, $window, T74128Service, Data) {
            //for instance
            scope.obj = {};
            scope.obj = Data;
            scope.t74036 = {};
            scope.obj.ChkBox = '2';
            scope.obj.InDue = '2';
            scope.obj.ProductList = {};

            //------------- for vat ---------
            var vat = T74128Service.getVat();
            vat.then(function (data) {
                scope.obj.vatId = data[0].T_ID;
                scope.obj.vatAmount = data[0].T_AMOUNT;

            });
            //--------------------------------------

            //-------------- For trade --------------------------
            scope.Trade_Click = function(id) {

                var trade = T74128Service.GetTrade();
                trade.then(function (data) {
                    scope.obj.TradeList = data;
                    //scope.obj.TradeList = JSON.parse(data);
                });

                document.getElementById(id).style.display = "block";
            }
            scope.CloseReceivePopup = function(id) {
                document.getElementById(id).style.display = "none";
            }
            
           
            scope.setClickedRow = function (index, obj, id) {

                scope.selectedRow = index;
                scope.obj.PRODUCT_DESC = obj.T_LANG2_NAME;
                scope.obj.T_COST_TYPE_DTL_ID = obj.T_COST_TYPE_DTL_ID;
                scope.obj.T_STORE_ID = obj.T_STORE_ID;
                scope.obj.T_ITEM_ID = obj.T_ITEM_ID;
                scope.Search = "";
                document.getElementById(id).style.display = "none";
            }
            //--------------------------------------------------------
            //--------------------- for Pack Size ---------------------------
            scope.PackSize_Click = function (id, cosTyDel, storeid) {

                var trade = T74128Service.getPackSize(cosTyDel, storeid);
                trade.then(function (data) {
                    scope.obj.PackSizeList = data;
                });

                document.getElementById(id).style.display = "block";
            }

            scope.setClickedRowPackSize = function (index, obj, id) {

                scope.selectedRow = index;

                var stock = T74128Service.getStock(scope.obj.T_ITEM_ID, scope.obj.T_STORE_ID,obj.T_ITEM_UM_ID);
                stock.then(function (data) {
                    var box = data;
                    var pices = obj.T_QTY * data;
                    scope.obj.Stock = 'Box :' + box +'; Pieces :' + pices;
                    scope.obj.Stock_Box = 'Box :' + box;
                    scope.obj.Stock_Pieces = ' Pieces :' + pices;
                    scope.obj.Box = data;
                    scope.obj.Pieces = obj.T_QTY * data;
                    scope.obj.QTY = obj.T_QTY;
                });

                var price = T74128Service.getPrice(scope.obj.T_COST_TYPE_DTL_ID, obj.T_ITEM_UM_ID);
                price.then(function(data) {
                    scope.obj.T_SALE_PRICE = data[0].T_SALE_PRICE;
                    scope.obj.PiecePrice = data[0].PiecePrice;
                    scope.obj.Price = data[0].PiecePrice;
                });

                scope.obj.T_LANG2_NAME = obj.T_LANG2_NAME;
                scope.obj.T_ITEM_UM_ID = obj.T_ITEM_UM_ID;
                scope.Search = "";
                document.getElementById(id).style.display = "none";
            }
            //-------------------------------------------------------
            //--------------------- for Patiens ------------------------------
            scope.Patien_Click = function (id) {
                var pat = T74128Service.getPatienRequest();
                pat.then(function(data) {
                    scope.obj.PatientList = data;
                });
                document.getElementById(id).style.display = "block";
            }

            scope.setClickedRowForPatien = function (ind, obj, popupId) {

                scope.obj.T_REQUEST_ID = obj.T_REQUEST_ID;
                scope.obj.T_FIRST_LANG2_NAME = obj.T_FIRST_LANG2_NAME;
                scope.obj.T_MOBILE_NO = obj.T_MOBILE_NO;
                scope.obj.T_ADDRESS1 = obj.T_ADDRESS1;
                scope.obj.T_PAT_ID = obj.T_PAT_ID;
                scope.Search = "";

                document.getElementById(popupId).style.display = "none";
            }
            //------------------------------------------------------------
            scope.Check_Click = function() {
                if (scope.obj.ChkBox == '1') {
                    scope.obj.Price = scope.obj.T_SALE_PRICE;
                   // scope.obj.Stock = scope.obj.Stock_Box;
                    scope.obj.SaleQuantity = '';
                    scope.obj.Total = '';
                } else {
                    scope.obj.Price = scope.obj.PiecePrice;
                   // scope.obj.Stock = scope.obj.Stock_Pieces;
                    scope.obj.SaleQuantity = '';
                    scope.obj.Total = '';
                }
            }
            scope.Quantity_Click = function() {
                if (scope.obj.ChkBox == '1') {
                    if (scope.obj.Box >= scope.obj.SaleQuantity) {
                        scope.obj.SaleQuantity = scope.obj.SaleQuantity;
                        scope.obj.Total = scope.obj.SaleQuantity * scope.obj.Price;

                    } else {
                        alert("Quantity is not valid. your total stock is  " + scope.obj.Box+ "  Boxs");
                        var len = scope.obj.SaleQuantity.toFixed().length;
                        var sub = scope.obj.SaleQuantity.toFixed().substring(0, len - 1);
                        scope.obj.SaleQuantity = parseFloat(sub);

                        // scope.obj.SaleQuantity = '';
                        scope.obj.Total = '';
                    }
                } else 
                    if (scope.obj.Pieces >= scope.obj.SaleQuantity) {
                        scope.obj.SaleQuantity = scope.obj.SaleQuantity;
                        scope.obj.Total = scope.obj.SaleQuantity * scope.obj.Price;
                    } else {
                        alert("Quantity is not valid. your total stock is  " + scope.obj.Pieces + "  Pieces");
                        var len = scope.obj.SaleQuantity.toFixed().length;
                        var sub = scope.obj.SaleQuantity.toFixed().substring(0, len - 1);
                        scope.obj.SaleQuantity = parseFloat(sub);
                        // scope.obj.SaleQuantity = '';
                        scope.obj.Total = '';
                    }
            }

            //------------ for Add in Grid --------------------------
            scope.obj.NewProductList = [];
            scope.Add_Click = function () {
                if (scope.obj.SaleQuantity !== '' && scope.obj.SaleQuantity !== undefined) {
                
                if (scope.obj.ProductList.length===undefined) {
                    scope.NewObject = {};
                    // if (scope.obj.ProductList.length === 0) {
                    scope.NewObject.PRODUCT_DESC_1 = scope.obj.PRODUCT_DESC;
                    scope.NewObject.T_LANG2_NAME_1 = scope.obj.T_LANG2_NAME;//
                    scope.NewObject.SaleQuantity_1 = scope.obj.SaleQuantity;
                    scope.NewObject.Price_1 = scope.obj.Price;
                    scope.NewObject.Total_1 = scope.obj.Total;
                    scope.NewObject.DisCntPer_1 = scope.obj.Discount ? scope.obj.Discount:0;
                    //-------------------------------------------------------
                    scope.NewObject.T_ITEM_ID_1 = scope.obj.T_ITEM_ID;
                    scope.NewObject.T_ITEM_UM_ID_1 = scope.obj.T_ITEM_UM_ID;
                    scope.NewObject.Stock_1 = scope.obj.Stock;
                    scope.NewObject.PiecePrice_1 = scope.obj.PiecePrice;
                    scope.NewObject.T_SALE_PRICE_1 = scope.obj.T_SALE_PRICE;
                    scope.NewObject.Box_1 = scope.obj.Box;
                    scope.NewObject.Pieces_1 = scope.obj.Pieces;//QTY
                    scope.NewObject.ChkBox_1 = scope.obj.ChkBox;
                    scope.NewObject.QTY_1 = scope.obj.QTY;


                    //-----------------------------------------------------------
                    var disValue = scope.obj.Discount ? scope.obj.Discount : 0;
                    var dis = (scope.obj.Total * disValue ) / 100;
                    scope.NewObject.Discount_1 = dis;

                    scope.NewObject.AftDcount_1 = scope.obj.Total - dis; //scope.obj.Price * scope.obj.SaleQuantity;
                    var afterDiscount = scope.obj.Total - dis;

                    scope.obj.NewProductList.push(scope.NewObject);
                    //  }
                    scope.obj.ProductList = scope.obj.NewProductList;


                    var sub = parseFloat(scope.obj.SubTotal ? scope.obj.SubTotal : 0);
                    var tl = afterDiscount + sub;
                    scope.obj.SubTotal = tl;
                    // -------------vat----------
                    var vat_per = (scope.obj.SubTotal * scope.obj.vatAmount) / 100;
                    scope.obj.Totalvat = vat_per;
                    scope.obj.NetTotal = scope.obj.SubTotal + vat_per;

                    scope.obj.PRODUCT_DESC = '';
                    scope.obj.T_LANG2_NAME = '';
                    scope.obj.SaleQuantity = '';
                    scope.obj.SaleQuantity = '';
                    scope.obj.Total = '';
                    scope.obj.Price = '';
                    scope.obj.Discount = '';
                    scope.obj.Stock = '';
                    scope.obj.T_ITEM_ID = '';
                    scope.obj.T_ITEM_UM_ID = '';
                    scope.obj.Stock = '';
                    scope.obj.T_SALE_PRICE = '';
                    scope.obj.PiecePrice = '';
                    scope.obj.ChkBox = '2';
                    scope.obj.Box = '';
                    scope.obj.Pieces = '';
                    scope.Table = true;
                } else {
                    var chk = 0;
                for (var i = 0; i < scope.obj.ProductList.length; i++) {

                    if (scope.obj.ProductList[i].T_ITEM_ID_1 === scope.obj.T_ITEM_ID && scope.obj.ProductList[i].T_ITEM_UM_ID_1 === scope.obj.T_ITEM_UM_ID ) {

                        chk = 1;

                    }
                    else {

                        //alert('Same Item and Pack Size is exists');

                        //scope.obj.PRODUCT_DESC = '';
                        //scope.obj.T_LANG2_NAME = '';
                        //scope.obj.SaleQuantity = '';
                        //scope.obj.SaleQuantity = '';
                        //scope.obj.Total = '';
                        //scope.obj.Price = '';
                        //scope.obj.Discount = '';
                        //scope.obj.Stock = '';
                        //scope.obj.T_ITEM_ID = '';
                        //scope.obj.T_ITEM_UM_ID = '';
                        //scope.obj.Stock = '';
                        //scope.obj.T_SALE_PRICE = '';
                        //scope.obj.PiecePrice = '';
                        //scope.obj.ChkBox = '2';
                        //scope.obj.Box = '';
                        //scope.obj.Pieces = '';
                    }
                   
                    }

                    if (chk === 0) {
                        scope.NewObject = {};
                        // if (scope.obj.ProductList.length === 0) {
                        scope.NewObject.PRODUCT_DESC_1 = scope.obj.PRODUCT_DESC;
                        scope.NewObject.T_LANG2_NAME_1 = scope.obj.T_LANG2_NAME;//
                        scope.NewObject.SaleQuantity_1 = scope.obj.SaleQuantity;
                        scope.NewObject.Price_1 = scope.obj.Price;
                        scope.NewObject.Total_1 = scope.obj.Total;
                        scope.NewObject.DisCntPer_1 = scope.obj.Discount ? scope.obj.Discount : 0;
                        //-------------------------------------------------------
                        scope.NewObject.T_ITEM_ID_1 = scope.obj.T_ITEM_ID;
                        scope.NewObject.T_ITEM_UM_ID_1 = scope.obj.T_ITEM_UM_ID;
                        scope.NewObject.Stock_1 = scope.obj.Stock;
                        scope.NewObject.PiecePrice_1 = scope.obj.PiecePrice;
                        scope.NewObject.T_SALE_PRICE_1 = scope.obj.T_SALE_PRICE;
                        scope.NewObject.Box_1 = scope.obj.Box;
                        scope.NewObject.Pieces_1 = scope.obj.Pieces;
                        scope.NewObject.ChkBox_1 = scope.obj.ChkBox;
                        scope.NewObject.QTY_1 = scope.obj.QTY;
                        //-----------------------------------------------------------
                        var disValue = scope.obj.Discount ? scope.obj.Discount : 0;
                        var dis = (scope.obj.Total * disValue) / 100;
                        scope.NewObject.Discount_1 = dis;

                        scope.NewObject.AftDcount_1 = scope.obj.Total - dis; //scope.obj.Price * scope.obj.SaleQuantity;
                        var afterDiscount = scope.obj.Total - dis;

                        scope.obj.NewProductList.push(scope.NewObject);
                        //  }
                        scope.obj.ProductList = scope.obj.NewProductList;

                        var sub = parseFloat(scope.obj.SubTotal ? scope.obj.SubTotal : 0);
                        var tl = afterDiscount + sub;
                        scope.obj.SubTotal = tl;
                        // -------------vat----------
                        var vat_per = (scope.obj.SubTotal * scope.obj.vatAmount) / 100;
                        scope.obj.Totalvat = vat_per;
                        scope.obj.NetTotal = scope.obj.SubTotal + vat_per;

                        scope.obj.PRODUCT_DESC = '';
                        scope.obj.T_LANG2_NAME = '';
                        scope.obj.SaleQuantity = '';
                        scope.obj.SaleQuantity = '';
                        scope.obj.Total = '';
                        scope.obj.Price = '';
                        scope.obj.Discount = '';
                        scope.obj.Stock = '';
                        scope.obj.T_ITEM_ID = '';
                        scope.obj.T_ITEM_UM_ID = '';
                        scope.obj.Stock = '';
                        scope.obj.T_SALE_PRICE = '';
                        scope.obj.PiecePrice = '';
                        scope.obj.ChkBox = '2';
                        scope.obj.Box = '';
                        scope.obj.Pieces = '';
                    }
                    else {
                       // alert('Same Item and Pack Size is exists');
                        alert(scope.getSingleMsg('S0030'));
                        scope.obj.PRODUCT_DESC = '';
                        scope.obj.T_LANG2_NAME = '';
                        scope.obj.SaleQuantity = '';
                        scope.obj.SaleQuantity = '';
                        scope.obj.Total = '';
                        scope.obj.Price = '';
                        scope.obj.Discount = '';
                        scope.obj.Stock = '';
                        scope.obj.T_ITEM_ID = '';
                        scope.obj.T_ITEM_UM_ID = '';
                        scope.obj.Stock = '';
                        scope.obj.T_SALE_PRICE = '';
                        scope.obj.PiecePrice = '';
                        scope.obj.ChkBox = '2';
                        scope.obj.Box = '';
                        scope.obj.Pieces = '';

                    }

             }
                }
                else {
                    //alert('Please you put Quantity');
                    alert(scope.getSingleMsg('S0031'));
                }
               
            }
            //------------------------------------------------------
            // ----------------- for Remove from grid ----------------
            scope.Remove_Click = function (ind,obj) {
                scope.obj.PRODUCT_DESC = obj.PRODUCT_DESC_1;
                scope.obj.T_LANG2_NAME = obj.T_LANG2_NAME_1;
                scope.obj.SaleQuantity = obj.SaleQuantity_1;
                scope.obj.Total = obj.Total_1;
                scope.obj.Price = obj.Price_1;
                scope.obj.Discount = obj.DisCntPer_1;
                scope.obj.SubTotal = scope.obj.SubTotal - obj.AftDcount_1;
                // -------------vat----------
                var vat_per = (scope.obj.SubTotal * scope.obj.vatAmount) / 100;
                scope.obj.Totalvat = vat_per;
                scope.obj.NetTotal = scope.obj.SubTotal + vat_per;

                //-----------------------------------------------
                scope.obj.T_ITEM_ID = obj.T_ITEM_ID_1;
                scope.obj.T_ITEM_UM_ID = obj.T_ITEM_UM_ID_1;
                scope.obj.Stock = obj.Stock_1;
                scope.obj.PiecePrice = obj.PiecePrice_1;
                scope.obj.T_SALE_PRICE = obj.T_SALE_PRICE_1;
                scope.obj.Box = obj.Box_1;
                scope.obj.Pieces = obj.Pieces_1;
                scope.obj.ChkBox = obj.ChkBox_1;
                scope.obj.QTY = obj.QTY_1;
                //-----------------------------------------------
                scope.obj.ProductList.splice(ind, 1);

            }
            // ------------- for Given Amount -------------
            scope.GivenAmount_Click = function() {
                if (scope.obj.NetTotal <= scope.obj.Given_Amount) {
                    scope.obj.Given_Amount = scope.obj.Given_Amount;
                    var retn = scope.obj.Given_Amount - scope.obj.NetTotal;
                    scope.obj.Return_Amount = retn.toFixed(3);
                } else {
                    alert('Your amount should not be less than  ' + scope.obj.NetTotal.toFixed(3));
                    scope.obj.Given_Amount = '';
                }
            }
            //---------------------------------------------
            //--------------- For Paid Amount --------------
            scope.PaidAmount_Click = function() {
              //  scope.obj.Paid_Amount = scope.obj.Paid_Amount;
                var retn = scope.obj.NetTotal - scope.obj.Paid_Amount;
                scope.obj.Due_Amount = retn.toFixed(3);
            }
            //---------------------------------------
            //----------------------------------------
            scope.DisAmount_Click = function () {
                if (scope.obj.SubTotal >= scope.obj.DiscountAmount) {

                    var subtl_afDis = scope.obj.SubTotal - scope.obj.DiscountAmount;

                    // -------------vat----------
                    var vat_per = (subtl_afDis * scope.obj.vatAmount) / 100;
                    scope.obj.Totalvat = vat_per;
                    scope.obj.NetTotal = subtl_afDis + vat_per;

                    //-----------------------------------------------
                } else {
                    alert('You can not give discount more than ' + scope.obj.SubTotal);
                    
                    var len = scope.obj.DiscountAmount.toFixed().length;
                    var sub = scope.obj.DiscountAmount.toFixed().substring(0, len - 1);
                    scope.obj.DiscountAmount = parseFloat(sub);
                }
               
            }
            //-----------------------------------------
            //-------------- for Saving ---------------------------
            scope.Save_Click = function () {
                if (scope.obj.InDue=== '1') {
                    scope.t74036.T_REQUEST_ID = scope.obj.T_REQUEST_ID;
                    scope.t74036.T_PAT_ID = scope.obj.T_PAT_ID;
                }
                scope.t74036.T_STORE_ID = scope.obj.T_STORE_ID;
                scope.t74036.T_DIS_AMOUNT = scope.obj.DiscountAmount;
                var total = scope.obj.SubTotal.toFixed(2);
                var gtotal = scope.obj.NetTotal.toFixed(2);
                scope.t74036.T_TOTAL = parseFloat(total);
                scope.t74036.T_VAT = scope.obj.vatAmount;
                scope.t74036.T_GRAND_TOTAL = parseFloat(gtotal);//T_STORE_ID
                scope.t74037 = [];
                for (var i = 0; i < scope.obj.ProductList.length; i++) {
                    scope.newObj = {};
                    if (scope.obj.ProductList[i].ChkBox_1==='1') {
                        scope.newObj.T_ITEM_ID = scope.obj.ProductList[i].T_ITEM_ID_1;
                        scope.newObj.T_QUANTITY = scope.obj.ProductList[i].SaleQuantity_1;
                        scope.newObj.T_SALE_PRICE = scope.obj.ProductList[i].Price_1;
                        scope.newObj.T_UOM_ID = scope.obj.ProductList[i].T_ITEM_UM_ID_1;
                        scope.newObj.T_DISCOUNT = scope.obj.ProductList[i].DisCntPer_1;
                        scope.newObj.T_TOTAL_AMOUNT = scope.obj.ProductList[i].AftDcount_1;
                        scope.newObj.T_PCS_BOX = scope.obj.ProductList[i].ChkBox_1;
                        scope.t74037.push(scope.newObj);
                    } else {
                        scope.newObj.T_ITEM_ID = scope.obj.ProductList[i].T_ITEM_ID_1;
                        scope.newObj.T_QUANTITY = scope.obj.ProductList[i].SaleQuantity_1 / scope.obj.ProductList[i].QTY_1;
                        scope.newObj.T_SALE_PRICE = scope.obj.ProductList[i].Price_1;
                        scope.newObj.T_UOM_ID = scope.obj.ProductList[i].T_ITEM_UM_ID_1;
                        scope.newObj.T_DISCOUNT = scope.obj.ProductList[i].DisCntPer_1;
                        scope.newObj.T_TOTAL_AMOUNT = scope.obj.ProductList[i].AftDcount_1;
                        scope.newObj.T_PCS_BOX = scope.obj.ProductList[i].ChkBox_1;
                        scope.t74037.push(scope.newObj);
                    }
                   

                }


                var save = T74128Service.saveData(scope.t74036, scope.t74037);
                save.then(function(data) {
                    //alert('Save successfully');
                    alert(scope.getSingleMsg('S0012'));
                });
            }

            //----------------------------------------------------
        }
    ]);

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