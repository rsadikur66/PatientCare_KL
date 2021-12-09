
//app.config(['$locationProvider', function ($locationProvider) {
//    $locationProvider.html5Mode({enabled: true}
//        );
//}]);

app.controller("T74116Controller", ["$scope", "$filter", "$http", "$window", "$location", "T74116Service", "uiGridConstants", "DTOptionsBuilder", "DTColumnBuilder", "UomFactory", "Data","LabelService",
    function (scope, $filter, $http, $window, $location, T74116Service, uiGridConstants, DTOptionsBuilder, DTColumnBuilder, UomFactory, Data, LabelService ) {

        

        scope.obj = {};
        scope.obj = Data;
        scope.obj.T74116 = {};
        scope.obj.T74060 = {};
        scope.obj.T74061 = {};
        scope.obj.T74031 = {};
        scope.obj.T74030 = {};
        scope.obj.U = {};

        //for getting Id from query sting start
        var url = $location.absUrl();
        var pursid = url.split("=").pop();
      
        //for getting Id from query sting end

        // Form Label Data Service Start 
        var labelData = LabelService.getlabeldata('T74116');
        labelData.then(function (data) {

            scope.entity = data;
        });
        // Form Label Data Service End 

        //============== for counting warranty start
      
        scope.obj.purProductList = [];
        scope.CountWarranty_click = function (id) {
            
            var d = scope.obj.purProductList[id].T_WARRANTY_END;
            var warEndDate = $filter('date')(d, 'yyyy,MM,dd');

             var toDay = $filter('date')(new Date(), 'yyyy,MM,dd');
             scope.obj.purProductList[id].T_WARRANTY = warranty(new Date(toDay), new Date(warEndDate)); // 'warranty' is a function

            
            //alert(warranty);
            
            //var dt1 = scope.obj.T74060.T_RECEIVE_DATE.split('-'),
            //    dt2 = warEndDate.split('-'),
            //    one = new Date(dt1[2], dt1[1] - 1, dt1[0]),
            //    two = new Date(dt2[2], dt2[1] - 1, dt2[0]);

            //var millisecondsPerDay = 1000 * 60 * 60 * 24 ;
            //var millisBetween = two.getTime() - one.getTime() ;
            //var days = millisBetween / millisecondsPerDay;
          
        }
        //============== for counting warranty end
        //===== for total start 
        scope.Total_Click = function (id) {
            var Check = parseInt( scope.obj.purProductList[id].T_RCV_QTY )+ parseInt(scope.obj.purProductList[id].Issue);
            if (Check !==0) {
                
             
            if (Check <= scope.obj.purProductList[id].T_PUR_QTY) {

                scope.obj.purProductList[id].T_TOTAL_VALUE = scope.obj.purProductList[id].T_UNIT_VALUE * scope.obj.purProductList[id].Issue;
                var total = 0;
                for (var i = 0; i < scope.obj.purProductList.length; i++) {

                    var intotal = scope.obj.purProductList[i].T_TOTAL_VALUE;
                    if (intotal !== undefined) {
                        total = parseInt( intotal )+  total;
                    }

                }
                if (scope.obj.T74060.T_DISCOUNT_AMOUNT === undefined) {
                    scope.obj.T74060.T_DISCOUNT_AMOUNT = 0;
                }
                if (scope.obj.T74060.T_VAT === undefined) {
                    scope.obj.T74060.T_VAT = 0;
                }

                scope.obj.T74060.T_GRAND_TOTAL_VALUE = total;
                //---------------------Discount start--------------------
                scope.obj.T74060.T_DISCOUNT_AMOUNT = (total * scope.obj.T74060.T_DISCOUNT_PERCENT)/100;
                //-----------------------Discount end-----------------
                scope.obj.T74060.T_TOTAL_VLU_AFTER_DISC = total - scope.obj.T74060.T_DISCOUNT_AMOUNT;
                //--------------------vat start----------------------
                scope.obj.T74060.T_VAT = (scope.obj.T74060.T_TOTAL_VLU_AFTER_DISC * scope.obj.T74060.T_VAT_PER) / 100;
                //----------------------vat end--------------------

                scope.obj.T74060.T_TOTAL_VALUE = scope.obj.T74060.T_TOTAL_VLU_AFTER_DISC + parseFloat(scope.obj.T74060.T_VAT);//parseInt(scope.obj.T74060.T_VAT);


            } else {
                //alert('Requisition quantity has been received  !!!');
                alert(scope.getSingleMsg('S0022'));
                scope.obj.purProductList[id].Issue = 0;
            }
            }
            else {

            }
        }
        //===== for total end
        // ======= for vat start ==========
        scope.Vat_Click = function(vat) {
            var total = 0;
            if (scope.obj.T74060.T_GRAND_TOTAL_VALUE == undefined) {
                total = 0;
            } else {
                total = scope.obj.T74060.T_GRAND_TOTAL_VALUE;
            }
           // var total = scope.obj.T74060.T_GRAND_TOTAL_VALUE;
            //---------------------Discount start--------------------
            scope.obj.T74060.T_DISCOUNT_AMOUNT = (total * scope.obj.T74060.T_DISCOUNT_PERCENT) / 100;
            //-----------------------Discount end-----------------
            scope.obj.T74060.T_TOTAL_VLU_AFTER_DISC = total - scope.obj.T74060.T_DISCOUNT_AMOUNT;
            //--------------------vat start----------------------
            scope.obj.T74060.T_VAT = (scope.obj.T74060.T_TOTAL_VLU_AFTER_DISC * vat) / 100;
            //----------------------vat end--------------------

            scope.obj.T74060.T_TOTAL_VALUE = scope.obj.T74060.T_TOTAL_VLU_AFTER_DISC + parseFloat(scope.obj.T74060.T_VAT);//parseInt(scope.obj.T74060.T_VAT);
        }
        //======== for vat end =========
        //========= for discound start =======
        scope.Discount_Click = function (discount) {
            var total = 0;
            if (scope.obj.T74060.T_GRAND_TOTAL_VALUE == undefined) {
                total = 0;
            } else {
                total = scope.obj.T74060.T_GRAND_TOTAL_VALUE;
            }
           //  total = scope.obj.T74060.T_GRAND_TOTAL_VALUE;
            //---------------------Discount start--------------------
            scope.obj.T74060.T_DISCOUNT_AMOUNT = (total * discount) / 100;
            //-----------------------Discount end-----------------
            scope.obj.T74060.T_TOTAL_VLU_AFTER_DISC = total - scope.obj.T74060.T_DISCOUNT_AMOUNT;
            //--------------------vat start----------------------
            scope.obj.T74060.T_VAT = (scope.obj.T74060.T_TOTAL_VLU_AFTER_DISC * scope.obj.T74060.T_VAT_PER) / 100;
            //----------------------vat end--------------------

            scope.obj.T74060.T_TOTAL_VALUE = scope.obj.T74060.T_TOTAL_VLU_AFTER_DISC + parseFloat(scope.obj.T74060.T_VAT);//parseInt(scope.obj.T74060.T_VAT);
        }
        //======== for discound end =======

        //=======for Removing row from Grid start
        
        //scope.RemoveRow = function (index) {
        //    scope.obj.purProductList.splice(index, 1);
        //}

        //=======for Removing row from Grid end
        //==============Get Purchase Data start
        //$window.location = "";
      
        scope.ProductList = [];
        var purProduct = T74116Service.purProduct(pursid);
        purProduct.then(function (data) {
            if (scope.obj.purProductList.length === 0) {
                var grandTotal = 0;
              //  scope.obj.purProductList = data;
                for (var i = 0; i < data.length; i++) {
                    scope.NewProductList = {};
                    scope.NewProductList.T_ISSUE_STS_DTL = data[i].T_ISSUE_STS_DTL;
                    scope.NewProductList.T_LANG2_NAME = data[i].T_LANG2_NAME;
                    scope.NewProductList.T_PUR_DTL_ID = data[i].T_PUR_DTL_ID;
                    scope.NewProductList.T_ITEM_ID = data[i].T_ITEM_ID;
                    scope.NewProductList.UM_T_LANG2_NAME = data[i].UM_T_LANG2_NAME;
                    scope.NewProductList.T_ITEM_UM_ID = data[i].T_ITEM_UM_ID;
                    scope.NewProductList.T_PUR_QTY = data[i].T_PUR_QTY;
                    scope.NewProductList.T_RCV_QTY = data[i].T_RCV_QTY;
                    scope.NewProductList.T_UNIT_VALUE = data[i].T_UNIT_VALUE;
                    scope.NewProductList.Issue = data[i].Issue;
                    scope.NewProductList.T_PRIORITY = data[i].T_PRIORITY;
                    scope.NewProductList.T_TOTAL_VALUE = data[i].T_TOTAL_VALUE;
                    scope.NewProductList.Reject = data[i].Reject;
                    scope.NewProductList.T_PUR_ID = data[i].T_PUR_ID;

                    grandTotal = data[i].T_TOTAL_VALUE + grandTotal;

                    scope.ProductList.push(scope.NewProductList);
                }

                scope.obj.purProductList = scope.ProductList;
                scope.obj.T74060.T_GRAND_TOTAL_VALUE = grandTotal;
            }
            

        });


        var purchase = T74116Service.getPursData(pursid );
        purchase.then(function(data) {
            scope.obj.T74116.Com_T_LANG2_NAME = data[0].Com_T_LANG2_NAME;
            scope.obj.T74060.T_COMPANY_ID = data[0].T_COMPANY_ID;
            scope.obj.T74116.Su_T_LANG2_NAME = data[0].Su_T_LANG2_NAME;
            scope.obj.T74060.T_SUPPLIER_ID = data[0].T_SUPPLIER_ID;
            scope.obj.T74116.Sto_T_LANG2_NAME = data[0].Sto_T_LANG2_NAME;
            scope.obj.T74060.T_STORE_ID = data[0].T_STORE_ID;
            scope.obj.T74116.Cur_T_LANG2_NAME = data[0].Cur_T_LANG2_NAME;
            scope.obj.T74060.T_CURRENCY_ID = data[0].T_CURRENCY_ID;

            sessionStorage.setItem("T_PUR_ID", data[0].T_PUR_ID);
          //  var value = sessionStorage.getItem("T_PUR_ID");
            scope.obj.T74060.T_VAT_PER = 0;
            scope.obj.T74060.T_DISCOUNT_PERCENT = 0;
            var myDate = new Date(data[0].T_PUR_REQ_DATE.match(/\d+/)[0] * 1);
            scope.obj.T74116.T_PUR_REQ_DATE = $filter('date')(myDate, "dd-MM-yyyy");
           // scope.obj.T74116.T_PUR_REQ_DATE = myDate;
            scope.obj.T74060.T_RECEIVE_DATE = $filter('date')(new Date(), 'dd-MM-yyyy');
        });

        //==============Get Purchase Data end

        

        scope.CloseReceivePopup = function (id) {
            document.getElementById(id).style.display = "none";
        }

        scope.Receive_Click = function (id) {

            var receiveBy = T74116Service.receiveBy(pursid); //scope.obj.T74060.T_STORE_ID
            receiveBy.then(function (data) {
                scope.obj.ReceiveList = data;
            });

            document.getElementById(id).style.display = "block";
        }
       // scope.obj.purProductList = [];
        scope.UOM_Popup = function (ind) {
           
            var url = '';
            var data = '';
          //  for (var i = 0; i < scope.obj.purProductList.length; i++) {
            var umId = scope.obj.purProductList[ind].T_ITEM_ID;
                UomFactory.getModal(url, data, umId, ind);
           // }
            
        }
        //=========== Reject start ==============

        scope.Reject_Click = function(ind) {
            if (scope.obj.purProductList[ind].Reject == '1') {
                //===========================  minus total =================================
                var total = 0;
                for (var i = 0; i < scope.obj.purProductList.length; i++) {
                    if (scope.obj.purProductList[i].Reject == 1) {
                        if (scope.obj.purProductList[i].T_TOTAL_VALUE !=0) {
                            total = scope.obj.purProductList[i].T_TOTAL_VALUE;
                        }
                       
                    }
                  
                }
                scope.obj.T74060.T_GRAND_TOTAL_VALUE = scope.obj.T74060.T_GRAND_TOTAL_VALUE - total;
                scope.obj.T74060.T_TOTAL_VLU_AFTER_DISC = scope.obj.T74060.T_TOTAL_VLU_AFTER_DISC - total;
                scope.obj.T74060.T_TOTAL_VALUE = scope.obj.T74060.T_TOTAL_VALUE - total;
                //============================================================
                document.getElementById("txtUnitPrice" + ind).readOnly = true;
                document.getElementById("txtIssueQty" + ind).readOnly = true;

                scope.obj.purProductList[ind].Issue = '';
                scope.obj.purProductList[ind].T_UNIT_VALUE = '';
                scope.obj.purProductList[ind].T_TOTAL_VALUE = 0;
            } else {
                document.getElementById("txtUnitPrice" + ind).readOnly = false;
                document.getElementById("txtIssueQty" + ind).readOnly = false;
               
            }
            
               // scope.obj.purProductList[ind].T_UNIT_VALUE = readOnly;
        }

        //=========== Reject end ==============

        //var EntryUser = T74116Service.EntryUser();
        //EntryUser.then(function (data) {
        //    scope.obj.T_ENTRY_USER = data;
        //    scope.obj.T_ENTRY_DATE = $filter('date')(new Date(), 'dd-MMM-yyyy');
        //});
       //===== Save ========Start
       
        scope.Save_Click = function () {
            scope.obj.T74061 = [];
            scope.obj.T74031 = [];
            scope.obj.T74027 = [];
            //var issue_sts_dtl = null;

            for (var i = 0; i < scope.obj.purProductList.length; i++) {
                scope.obj.T7_61 = {};
                scope.obj.T7_31 = {};
                scope.obj.T7_27 = {};

                scope.obj.T7_61.T_ITEM_ID = scope.obj.purProductList[i].T_ITEM_ID;
                scope.obj.T7_61.T_UOM_ID = scope.obj.purProductList[i].T_ITEM_UM_ID;
                scope.obj.T7_61.T_UNIT_VALUE = scope.obj.purProductList[i].T_UNIT_VALUE;
                scope.obj.T7_61.T_RCV_QTY = scope.obj.purProductList[i].Issue;
                scope.obj.T7_61.T_LOT_NO = scope.obj.purProductList[i].T_LOT_NO;
                scope.obj.T7_61.T_BATCH = scope.obj.purProductList[i].T_BATCH;
                scope.obj.T7_61.T_MANUFACTURE_DATE = scope.obj.purProductList[i].T_MANUFACTURE_DATE;
                scope.obj.T7_61.T_EXP_DATE = scope.obj.purProductList[i].T_EXP_DATE;
                scope.obj.T7_61.T_WARRANTY_END = scope.obj.purProductList[i].T_WARRANTY_END;
                scope.obj.T7_61.T_TOTAL_VALUE = scope.obj.purProductList[i].T_TOTAL_VALUE;
                scope.obj.T74061.push(scope.obj.T7_61);
                //======================== For T74027 start =======================
                scope.obj.T7_27.T_ITEM_ID = scope.obj.purProductList[i].T_ITEM_ID;
                scope.obj.T7_27.T_ITEM_UM_ID = scope.obj.purProductList[i].T_ITEM_UM_ID;
                scope.obj.T7_27.T_UNIT_VALUE = scope.obj.purProductList[i].T_UNIT_VALUE;
                scope.obj.T7_27.T_PURCHASE_QTY = scope.obj.purProductList[i].Issue;
                scope.obj.T7_27.T_TRNSACTION_QTY = scope.obj.purProductList[i].Issue;
                scope.obj.T7_27.T_EXPIRE_DATE = scope.obj.purProductList[i].T_EXP_DATE;
                scope.obj.T7_27.T_TOTAL_VALUE = scope.obj.purProductList[i].T_TOTAL_VALUE; //T_STOCK_DATE

                scope.obj.T7_27.T_STOCK_DATE =scope.obj.T74060.T_RECEIVE_DATE;
                scope.obj.T7_27.T_STORE_ID =scope.obj.T74060.T_STORE_ID;
                scope.obj.T7_27.T_SUPPLIER_ID =scope.obj.T74060.T_SUPPLIER_ID;
                scope.obj.T7_27.T_COMPANY_ID =scope.obj.T74060.T_COMPANY_ID;

                scope.obj.T74027.push(scope.obj.T7_27);
                //======================== For T74027 end =======================
              //  ===========================================
                scope.obj.T7_31.T_PUR_DTL_ID = scope.obj.purProductList[i].T_PUR_DTL_ID;
                scope.obj.T7_31.T_RCV_QTY = scope.obj.purProductList[i].T_RCV_QTY + scope.obj.purProductList[i].Issue;

                if (scope.obj.purProductList[i].T_PUR_QTY == parseInt(scope.obj.T7_31.T_RCV_QTY)) {
                    scope.obj.T7_31.T_ISSUE_STS_DTL = 'Received';
                } else if (scope.obj.purProductList[i].Reject == '1') {
                    scope.obj.T7_31.T_ISSUE_STS_DTL = 'Reject';
                }
                else {
                    scope.obj.T7_31.T_ISSUE_STS_DTL = 'Partial';
                }

                scope.obj.T74031.push(scope.obj.T7_31);
            }



            var T_ISSUE_STATUS = '1'; // T_ISSUE_STATUS = '2' (Partial) , T_ISSUE_STATUS = '1' (Received)
            for (var j = 0; j < scope.obj.T74031.length; j++) {
                if (scope.obj.T74031[j].T_ISSUE_STS_DTL == "Partial") {
                    T_ISSUE_STATUS = '2';
                }
            }

            var t30_purId = sessionStorage.getItem("T_PUR_ID");
            scope.obj.T74060.T_PR_ID = t30_purId;

            scope.obj.T74030.T_PUR_ID = t30_purId;
            scope.obj.T74030.T_ISSUE_STATUS = T_ISSUE_STATUS;

            var saveData = T74116Service.SaveData(scope.obj.T74060, scope.obj.T74061, scope.obj.T74031, scope.obj.T74030, scope.obj.T74027);
            saveData.then(function(data) {
                //alert("Save successfully");
                alert(scope.getSingleMsg('S0002'));
                scope.obj.purProductList = [];

                //--------------for grid load -------------------
                var purProduct = T74116Service.purProduct(pursid);
                purProduct.then(function (data) {
                    if (scope.obj.purProductList.length === 0) {
                        scope.obj.purProductList = data;
                    }


                });

            });

            
        }
        //===== Save ========End

        //For picking up data into text
        scope.setClickedRow = function (index, obj,id) {

            scope.selectedRow = index;
            scope.obj.T74060.T_RECEIVE_BY= obj.T_EMP_ID;
            scope.obj.T74116.T_FIRST_LANG2_NAME = obj.T_FIRST_LANG2_NAME;
            scope.Search = "";
            document.getElementById(id).style.display = "none";
        }
        scope.$watch('selectedRow', function () {
            console.log('Do Some processing'); //runs the block whenever selectedRow is changed. 
        });
        // ==== for modifing data start
        function warranty(toDay, warEndDate) {
            //convert to UTC
            var date2_UTC = new Date(Date.UTC(warEndDate.getUTCFullYear(), warEndDate.getUTCMonth(), warEndDate.getUTCDate()));
            var date1_UTC = new Date(Date.UTC(toDay.getUTCFullYear(), toDay.getUTCMonth(), toDay.getUTCDate()));


            var yAppendix, mAppendix, dAppendix;


            //--------------------------------------------------------------
            var days = date2_UTC.getDate() - date1_UTC.getDate();
            if (days < 0) {

                date2_UTC.setMonth(date2_UTC.getMonth() - 1);
                days += DaysInMonth(date2_UTC);
            }
            //--------------------------------------------------------------
            var months = date2_UTC.getMonth() - date1_UTC.getMonth();
            if (months < 0) {
                date2_UTC.setFullYear(date2_UTC.getFullYear() - 1);
                months += 12;
            }
            //--------------------------------------------------------------
            var years = date2_UTC.getFullYear() - date1_UTC.getFullYear();


            if (years > 1) yAppendix = " Y";
            else yAppendix = " Y";
            if (months > 1) mAppendix = " M";
            else mAppendix = " M";
            if (days > 1) dAppendix = " D";
            else dAppendix = " D";


            return years + yAppendix + ", " + months + mAppendix + ", " + days + dAppendix ;
        }
        function DaysInMonth(date2_UTC) {
            var monthStart = new Date(date2_UTC.getFullYear(), date2_UTC.getMonth(), 1);
            var monthEnd = new Date(date2_UTC.getFullYear(), date2_UTC.getMonth() + 1, 1);
            var monthLength = (monthEnd - monthStart) / (1000 * 60 * 60 * 24);
            return monthLength;
        }
        // ==== for modifing data end
         
        
    }]);

//app.directive('arrowSelector', ['$document', function ($document) {
//    return {
//        restrict: 'A',
//        link: function (scope, elem, attrs, ctrl) {
//            var elemFocus = false;
//            elem.on('mouseenter', function () {
//                elemFocus = true;
//            });
//            elem.on('mouseleave', function () {
//                elemFocus = false;
//            });
//            $document.bind('keydown', function (e) {
//                if (elemFocus) {
//                    if (e.keyCode == 38) {
//                        console.log(scope.selectedRow);
//                        if (scope.selectedRow == 0) {
//                            return;
//                        }
//                        scope.selectedRow--;
//                        scope.$apply();
//                        e.preventDefault();
//                    }
//                    if (e.keyCode == 40) {
//                        if (scope.selectedRow == scope.foodItems.length - 1) {
//                            return;
//                        }
//                        scope.selectedRow++;
//                        scope.$apply();
//                        e.preventDefault();
//                    }
//                }
//            });
//        }
//    };
//}]);





