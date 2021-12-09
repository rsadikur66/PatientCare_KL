app.controller("T74046Controller",
    ["$scope", "$filter", "$http", "$window", "T74046Service", "T74041Service", "uiGridConstants", "DTOptionsBuilder",
        "DTColumnBuilder", "LabelService", "Data", "RequestFactory", "T74046ConLeveFactory", "ServiceFactory", '$timeout', '$interval',
        "$rootScope", "$location",
        function (scope,
            $filter,
            $http,
            $window,
            T74046Service,
            T74041Service,
            uiGridConstants,
            DTOptionsBuilder,
            DTColumnBuilder,
            LabelService,
            Data,
            RequestFactory,
            T74046ConLeveFactory,
            ServiceFactory,
            $timeout,
            $interval,
            $rootScope, $location) {
            //For Instance
            scope.obj = {};
            scope.obj = Data;
            scope.obj.T74046 = {};
            scope.obj.T74041 = {};
            scope.obj.T74043 = {};
            scope.obj.T74117 = {};
            scope.obj.e = {};
            scope.obj.e.selected = { T_GCS_ID: '', T_GCS_VALUE: 0, T_GCS_TEXT: 'Select' };
            scope.obj.m = {};
            scope.obj.m.selected = { T_GCS_ID: '', T_GCS_VALUE: 0, T_GCS_TEXT: 'Select' };
            scope.obj.v = {};
            scope.obj.v.selected = { T_GCS_ID: '', T_GCS_VALUE: 0, T_GCS_TEXT: 'Select' };
            scope.obj.T74041.T_EYE_OPEN = 0;
            scope.obj.T74041.T_BEST_MOTOR = 0;
            scope.obj.T74041.T_VERBAL_RESPONSE = 0;
            scope.places = [];

            scope.obj.R = {};
            scope.obj.G = {};
            scope.obj.D = {};
            scope.obj.M = {};
            //scope.obj.M.selected = {};
            scope.obj.N = {};
            scope.obj.Ch = {};
            scope.obj.Pr = {};
            scope.dtInstance = {};
            scope.Div_Service = false;
            scope.isShowImage = true;
            scope.isShowImageHospital = true;
            scope.isCaseArrival = true;
            // scope.obj.isSuggestedHospital = false;   
            scope.obj.T74046.T_REQUEST = 0;
            //----------Tab Toggling--------------Start
            var labelData = LabelService.getlabeldata('T74046');
            labelData.then(function (data) {
                scope.entity = data;
            });
            //Billing Part-------------Imran-------------Start
            scope.ddd = function () {
                document.getElementById('txtPat_Age').focus();
                //scope.$broadcast('ddlPat_MRIL');
                //alert();
            }
            scope.hhh = function () {
                document.getElementById('txtDuration').focus();
                //scope.$broadcast('ddlPat_MRIL');
                //alert();
            }
            scope.aaa = function () {
                document.getElementById('txtPat_NationalId').focus();
            }
            scope.bbb = function () {
                document.getElementById('txtAutoSearchTexBox').focus();
            }
            scope.ggg = function () {
                document.getElementById('txtPat_Address').focus();
            }

            scope.bill = function () {
                var getId = T74046Service.getId();
                getId.then(function (data) {
                    var prm = JSON.parse(data);
                    if (prm.length > 0) {
                        //var param = JSON.parse(sessionStorage.getItem("param"));
                        var param = { T_REQUEST_ID: prm[0].T_REQUEST_ID, T_Doc_ID: prm[0].T_DOC_ID };
                        scope.obj.param = param;
                        //sessionStorage.setItem("param", param);
                        scope.obj.P_PRESCRIPTION = param.T_REQUEST_ID;
                        //var param = { T_REQUEST_ID: 65, T_Doc_ID: 56 };


                        var ambulancePrice = T74046Service.AmbulancePrice(param.T_REQUEST_ID);
                        ambulancePrice.then(function (data) {

                            /// if (scope.obj.AmbulancePrice===undefined) {
                            //  scope.obj.commonTotal = 0;
                            scope.obj.AmbulancePrice = parseFloat(data[0].T_SALE_PRICE);
                            scope.obj.AmbulancePrice_DtlID = parseFloat(data[0].T_COST_TYPE_DTL_ID);

                            var AmbulancePrice = scope.obj.AmbulancePrice === undefined ? 0 : scope.obj.AmbulancePrice;
                            var DoctorPrice = scope.obj.DoctorPrice === undefined
                                ? 0
                                : JSON.parse(scope.obj.DoctorPrice);
                            scope.obj.commonTotal = DoctorPrice + AmbulancePrice;


                            var MedNetamount = scope.obj.MedNetamount === undefined ? 0 : scope.obj.MedNetamount;
                            var Netamount = scope.obj.Netamount === undefined ? 0 : scope.obj.Netamount;
                            var commonTotal = scope.obj.commonTotal === undefined ? 0 : scope.obj.commonTotal;
                            scope.obj.GrandTotal = MedNetamount + Netamount + commonTotal;


                            //scope.obj.AmbulancePrice = parseFloat(JSON.parse(data));
                            //scope.obj.commonTotal = scope.obj.DoctorPrice + scope.obj.AmbulancePrice;


                            // }

                            //---------------------------------Doc Start--------------------------------------------
                            var doctorPrice = T74046Service.DoctorPrice(param.T_Doc_ID);
                            doctorPrice.then(function (data) {
                                scope.obj.DoctorPrice = parseFloat(JSON.parse(data[0].T_PRICE));
                                scope.obj.DoctorPrice_dtlId = parseFloat(JSON.parse(data[0].T_COST_TYPE_DTL_ID));
                                var AmbulancePrice = scope.obj.AmbulancePrice === undefined
                                    ? 0
                                    : JSON.parse(scope.obj.AmbulancePrice);
                                var DoctorPrice = scope.obj.DoctorPrice === undefined ? 0 : scope.obj.DoctorPrice;
                                scope.obj.commonTotal = DoctorPrice + AmbulancePrice;


                                //scope.obj.commonTotal = scope.obj.DoctorPrice + scope.obj.AmbulancePrice;

                                var MedNetamount = scope.obj.MedNetamount === undefined ? 0 : scope.obj.MedNetamount;
                                var Netamount = scope.obj.Netamount === undefined ? 0 : scope.obj.Netamount;
                                var commonTotal = scope.obj.commonTotal === undefined ? 0 : scope.obj.commonTotal;
                                scope.obj.GrandTotal = MedNetamount + Netamount + commonTotal;

                                //---------------------------------Service Start--------------------------------------------


                                var servicePrice = T74046Service.ServicePrice(param.T_REQUEST_ID);
                                servicePrice.then(function (data) {
                                    scope.obj.ServicePrice = data;
                                    //---------------------------------Diagonosis Start--------------------------------------------
                                    scope.obj.DiagonosisPrice = 0;
                                    scope.obj.Dia_Discount = 0;
                                    scope.obj.Subtotal = 0;
                                    scope.obj.Vat = 0;
                                    scope.obj.Netamount = 0;
                                    var diagonosisByReq = T74046Service.DiagonosisByReq(param.T_REQUEST_ID);
                                    diagonosisByReq.then(function (data) {
                                        scope.obj.DiagonosisListByReq = data;
                                        scope.obj.DiagonosisPrice = 0;
                                        for (var i = 0; i < scope.obj.DiagonosisListByReq.length; i++) {
                                            scope.obj.DiagonosisPrice += scope.obj.DiagonosisListByReq[i].T_PRICE;
                                        }
                                        scope.obj.Dia_Discount = (scope.obj.DiagonosisPrice * scope.obj.Discount) / 100;
                                        scope.obj.Subtotal = scope.obj.DiagonosisPrice - scope.obj.Dia_Discount;

                                        scope.obj.Vat = (scope.obj.Subtotal * scope.obj.vat) / 100;
                                        scope.obj.Netamount = scope.obj.Subtotal + scope.obj.Vat;


                                        var MedNetamount = scope.obj.MedNetamount === undefined
                                            ? 0
                                            : scope.obj.MedNetamount;
                                        var Netamount = scope.obj.Netamount === undefined ? 0 : scope.obj.Netamount;
                                        var commonTotal = scope.obj.commonTotal === undefined
                                            ? 0
                                            : scope.obj.commonTotal;
                                        scope.obj.GrandTotal = MedNetamount + Netamount + commonTotal;
                                        //---------------------------------Medicine Start  --------------------------------------------
                                        scope.obj.PrescriptionList = [];
                                        var getMedicineList = T74046Service.GetMedicineList(param.T_REQUEST_ID);
                                        getMedicineList.then(function (data) {
                                            if (scope.obj.PrescriptionList.length === 0) {
                                                function UniqueArraybyId(collection, keyname) {
                                                    var output = [],
                                                        keys = [];
                                                    angular.forEach(collection,
                                                        function (item) {
                                                            var key = item[keyname];
                                                            if (keys.indexOf(key) === -1 || key === 'N/A') {
                                                                keys.push(key);
                                                                output.push(item);
                                                            }
                                                        });
                                                    return output;
                                                };

                                                scope.dt = UniqueArraybyId(JSON.parse(data), "T_ITEM_CODE");

                                                for (var i = 0; i < scope.dt.length; i++) {
                                                    if (scope.dt[i].T_ITEM_CODE === 'N/A') {
                                                        //JSON.stringify(sessionStorage.setItem('DOSE_QUANTITY', scope.dt[i].DOSE_QUANTITY));
                                                        var medList = T74046Service.GetMedicineListByGen(
                                                            param.T_REQUEST_ID,
                                                            scope.dt[i].T_GEN_CODE,
                                                            scope.dt[i].T_REQUEST_STRENGTH,
                                                            scope.dt[i].T_DRUG_ROUTE_CODE,
                                                            scope.dt[i].T_FORM_ID);
                                                        medList.then(function (data) {
                                                            function UniqueArray(collection, keyname) {
                                                                var output = [],
                                                                    keys = [];

                                                                angular.forEach(collection,
                                                                    function (item) {
                                                                        var key = item[keyname];
                                                                        if (keys.indexOf(key) === -1) {
                                                                            keys.push(key);
                                                                            output.push(item);
                                                                        }
                                                                    });
                                                                return output;
                                                            };

                                                            scope.dt_Med = UniqueArray(JSON.parse(data), "ITEM_CODE");
                                                            scope.obj.dummy = {};
                                                            scope.obj.dummy.M = {};
                                                            scope.obj.dummy.M.selected = { PRODUCT_DESC: 'Select' };
                                                            //scope.obj.dummy.DOSE_QUANTITY = JSON.parse(sessionStorage.getItem('DOSE_QUANTITY'));
                                                            scope.obj.dummy.DOSE_QUANTITY =
                                                                scope.dt_Med[0].DOSE_QUANTITY;
                                                            scope.obj.dummy.T_STORE_ID = scope.dt_Med[0].T_STORE_ID;
                                                            scope.obj.dummy.Unit = 'Box';
                                                            scope.obj.dummy.checkUnit = '2';
                                                            scope.obj.dummy.MedList = [];
                                                            for (var l = 0; l < scope.dt_Med.length; l++) {
                                                                scope.obj.demo2 = {};
                                                                scope.obj.demo2.T_COST_TYPE_DTL_ID = scope.dt_Med[l]
                                                                    .T_COST_TYPE_DTL_ID;
                                                                scope.obj.demo2.PRODUCT_DESC = scope.dt_Med[l]
                                                                    .PRODUCT_DESC;
                                                                scope.obj.demo2.T_PRICE = scope.dt_Med[l].T_SALE_PRICE;
                                                                scope.obj.demo2.ITEM_CODE = scope.dt_Med[l].ITEM_CODE;
                                                                scope.dt_Med1 = (JSON.parse(data));
                                                                scope.obj.demo2.chkList = [];
                                                                for (var k = 0; k < scope.dt_Med1.length; k++) {
                                                                    if (scope.obj.demo2.ITEM_CODE ===
                                                                        scope.dt_Med1[k].ITEM_CODE) {
                                                                        scope.obj.demo4 = {};
                                                                        scope.obj.demo4.T_ITEM_UM_ID = scope.dt_Med1[k]
                                                                            .T_ITEM_UM_ID;
                                                                        scope.obj.demo4.PACK_SIZE = scope.dt_Med1[k]
                                                                            .PACK_SIZE;
                                                                        scope.obj.demo4.STOCK = scope.dt_Med1[k].STOCK;
                                                                        scope.obj.demo4.BOX_QTY = scope.dt_Med1[k]
                                                                            .BOX_QTY;
                                                                        scope.obj.demo4.PCS_QTY = scope.dt_Med1[k]
                                                                            .PCS_QTY;
                                                                        scope.obj.demo4.T_ITEM_CODE = scope.dt_Med1[k]
                                                                            .ITEM_CODE;
                                                                        scope.obj.demo4.T_SALE_PRICE = scope.dt_Med1[k]
                                                                            .PCS_PRICE;
                                                                        scope.obj.demo4.T_PRICE_BOX = scope.dt_Med1[k]
                                                                            .T_SALE_PRICE;
                                                                        scope.obj.demo2.chkList.push(scope.obj.demo4);
                                                                    }
                                                                }
                                                                scope.obj.dummy.MedList.push(scope.obj.demo2);
                                                            }
                                                            scope.obj.PrescriptionList.push(scope.obj.dummy);

                                                        });

                                                    } else {
                                                        scope.obj.demo = {};
                                                        scope.obj.demo.M = {};
                                                        scope.obj.demo.MedList = [];
                                                        scope.obj.demo.DOSE_QUANTITY = scope.dt[i].DOSE_QUANTITY;
                                                        scope.obj.demo.T_ITEM_CODE = scope.dt[i].T_ITEM_CODE;
                                                        scope.obj.demo.T_STORE_ID = scope.dt[i].T_STORE_ID;
                                                        scope.obj.demo.Unit = 'Box';
                                                        scope.obj.demo.checkUnit = '2';
                                                        scope.obj.demo.chkList = [];
                                                        scope.dt1 = JSON.parse(data);
                                                        for (var j = 0; j < scope.dt1.length; j++) {
                                                            if (scope.obj.demo.T_ITEM_CODE ===
                                                                scope.dt1[j].T_ITEM_CODE) {
                                                                scope.obj.demo3 = {};
                                                                scope.obj.demo3.T_ITEM_UM_ID =
                                                                    scope.dt1[j].T_ITEM_UM_ID;
                                                                scope.obj.demo3.PACK_SIZE = scope.dt1[j].PACK_SIZE;
                                                                scope.obj.demo3.STOCK = scope.dt1[j].STOCK;
                                                                scope.obj.demo3.BOX_QTY = scope.dt1[j].BOX_QTY;
                                                                scope.obj.demo3.PCS_QTY = scope.dt1[j].PCS_QTY;
                                                                scope.obj.demo3.T_ITEM_CODE = scope.dt1[j].T_ITEM_CODE;
                                                                scope.obj.demo3.T_SALE_PRICE = scope.dt1[j].PCS_PRICE;
                                                                scope.obj.demo3.T_PRICE_BOX = scope.dt1[j].T_SALE_PRICE;
                                                                scope.obj.demo.chkList.push(scope.obj.demo3);

                                                            }
                                                        }
                                                        scope.obj.demo.M.selected = {
                                                            T_COST_TYPE_DTL_ID: scope.dt[i].T_COST_TYPE_DTL_ID,
                                                            PRODUCT_DESC: scope.dt[i].PRODUCT_DESC,
                                                            T_PRICE: scope.dt[i].T_SALE_PRICE,
                                                            chkList: scope.obj.demo.chkList

                                                        };
                                                        scope.obj.PrescriptionList.push(scope.obj.demo);
                                                    }
                                                }
                                            }
                                            //----------------------Service Update Start--------
                                            var chkBill = T74046Service.chkBill(param.T_REQUEST_ID);
                                            chkBill.then(function (dt) {
                                                if (dt > 0) {
                                                    var prvServices = T74046Service.prvServices(param.T_REQUEST_ID);
                                                    prvServices.then(function (data) {
                                                        var dt = JSON.parse(data);
                                                        scope.obj.ServiceList = [];
                                                        for (var i = 0; i < dt.length; i++) {
                                                            var t = {};
                                                            t.Name = dt[i].T_LANG2_NAME;
                                                            t.T_PRICE = dt[i].T_SALE_PRICE;
                                                            t.T_COST_TYPE_DTL_ID = dt[i].T_COST_TYPE_DTL_ID;
                                                            scope.obj.ServiceList.push(t);
                                                            scope.obj.commonTotal += parseFloat(dt[i].T_SALE_PRICE);
                                                            var MedNetamount = scope.obj.MedNetamount === undefined
                                                                ? 0
                                                                : scope.obj.MedNetamount;
                                                            var Netamount = scope.obj.Netamount === undefined
                                                                ? 0
                                                                : scope.obj.Netamount;
                                                            var commonTotal = scope.obj.commonTotal === undefined
                                                                ? 0
                                                                : scope.obj.commonTotal;
                                                            scope.obj.GrandTotal = MedNetamount +
                                                                Netamount +
                                                                commonTotal;
                                                            for (var j = 0; j < scope.obj.ServicePrice.length; j++) {
                                                                if (scope.obj.ServicePrice[j].T_COST_TYPE_DTL_ID ==
                                                                    dt[i].T_COST_TYPE_DTL_ID) {

                                                                    scope.obj.ServicePrice[j].ServiceChk = '1';
                                                                    document.getElementById('chk' + j).checked = true;
                                                                }
                                                                //else {
                                                                //    //scope.obj.ServicePrice[j].ServiceChk = false;
                                                                //    document.getElementById('chk' + j).checked = false;
                                                                //}
                                                            }
                                                        }
                                                        //----------------------Medicine Update Start--------

                                                        //----------------------Medicine Update End----------


                                                    });
                                                }
                                            });
                                            //----------------------Service Update Start--------


                                        });
                                        //---------------------------------Medicine End  --------------------------------------------

                                    });
                                    //---------------------------------Diagonosis End --------------------------------------------

                                });
                                //---------------------------------Service End  --------------------------------------------

                            });
                            //---------------------------------Doc End  --------------------------------------------


                        });


                    }


                });
            }
            scope.OpenServicePopup = function (id) {
                document.getElementById(id).style.display = "block";

            }
            scope.CloseServicePopup = function (id) {
                document.getElementById(id).style.display = "none";
            }

            scope.AddService = function (id) {
                if (scope.obj.ServiceList !== undefined) {
                    for (var j = 0; j < scope.obj.ServiceList.length; j++) {
                        scope.obj.commonTotal -= parseFloat(scope.obj.ServiceList[j].T_PRICE);
                    }


                    var MedNetamount = scope.obj.MedNetamount === undefined ? 0 : scope.obj.MedNetamount;
                    var Netamount = scope.obj.Netamount === undefined ? 0 : scope.obj.Netamount;
                    var commonTotal = scope.obj.commonTotal === undefined ? 0 : scope.obj.commonTotal;
                    scope.obj.GrandTotal = MedNetamount + Netamount + commonTotal;

                }
                scope.obj.ServiceList = [];
                scope.obj.ServiceTotal = 0;

                document.getElementById(id).style.display = "none";
                for (var i = 0; i < scope.obj.ServicePrice.length; i++) {
                    if (scope.obj.ServicePrice[i].ServiceChk == 1) {
                        scope.obj.ServiceList.push({
                            Name: scope.obj.ServicePrice[i].T_LANG2_NAME,
                            T_PRICE: scope.obj.ServicePrice[i].T_PRICE,
                            T_COST_TYPE_DTL_ID: scope.obj.ServicePrice[i].T_COST_TYPE_DTL_ID

                        });
                        scope.obj.ServiceTotal += parseFloat(scope.obj.ServicePrice[i].T_PRICE);


                    }
                }
                scope.obj.commonTotal += parseFloat(scope.obj.ServiceTotal);
                var MedNetamount = scope.obj.MedNetamount === undefined ? 0 : scope.obj.MedNetamount;
                var Netamount = scope.obj.Netamount === undefined ? 0 : scope.obj.Netamount;
                var commonTotal = scope.obj.commonTotal === undefined ? 0 : scope.obj.commonTotal;
                scope.obj.GrandTotal = MedNetamount + Netamount + commonTotal;
            }
            scope.RemoveService = function (index) {
                var removePrice = parseFloat(scope.obj.ServiceList[index].T_PRICE);
                scope.obj.commonTotal = scope.obj.commonTotal - removePrice;


                var MedNetamount = scope.obj.MedNetamount === undefined ? 0 : scope.obj.MedNetamount;
                var Netamount = scope.obj.Netamount === undefined ? 0 : scope.obj.Netamount;
                var commonTotal = scope.obj.commonTotal === undefined ? 0 : scope.obj.commonTotal;
                scope.obj.GrandTotal = MedNetamount + Netamount + commonTotal;


                scope.obj.ServiceList.splice(index, 1);
            }
            scope.RemoveMedicine = function (index) {

                if (scope.obj.TotalPrice !== undefined) {
                    scope.obj.TotalPrice -= scope.obj.PrescriptionList[index].T_LINE_TOTAL;
                } else {
                    scope.obj.TotalPrice = null;
                }
                scope.obj.PrescriptionList.splice(index, 1);
            }

            scope.packChange = function (id) {
                scope.obj.PrescriptionList[id].checkUnit = '2';
                scope.obj.PrescriptionList[id].Unit = 'Box';
                scope.obj.PrescriptionList[id].T_SELL_QTY = null;
                if (scope.obj.TotalPrice != undefined) {
                    scope.obj.TotalPrice -= scope.obj.PrescriptionList[id].T_LINE_TOTAL === undefined
                        ? 0
                        : scope.obj.PrescriptionList[id].T_LINE_TOTAL;


                    scope.obj.Med_Discount = (scope.obj.TotalPrice * scope.obj.Discount) / 100;
                    scope.obj.MedSubtotal = scope.obj.TotalPrice - scope.obj.Med_Discount;

                    scope.obj.MedVat = (scope.obj.MedSubtotal * scope.obj.vat) / 100;
                    scope.obj.MedNetamount = scope.obj.MedSubtotal + scope.obj.MedVat;
                    scope.obj.GrandTotal = scope.obj.MedNetamount + scope.obj.Netamount + scope.obj.commonTotal;
                } else {
                    scope.obj.TotalPrice = null;
                    scope.obj.Med_Discount = null;
                    scope.obj.MedSubtotal = null;
                    scope.obj.MedVat = null;
                    scope.obj.MedNetamount = null;
                    scope.obj.GrandTotal = scope.obj.Netamount + scope.obj.commonTotal;
                }
            }
            scope.lineTotal = function (index) {
                if (scope.obj.PrescriptionList[index].T_SELL_QTY != null) {
                    var stk_box = parseFloat(scope.obj.PrescriptionList[index].T_STOCK_QTY);
                    var stk_pcs = parseFloat(scope.obj.PrescriptionList[index].PCS_QTY);
                    var slQty = parseFloat(scope.obj.PrescriptionList[index].T_SELL_QTY);
                    var boxQty = parseFloat(scope.obj.PrescriptionList[index].BOX_QTY);

                    if (scope.obj.PrescriptionList[index].checkUnit === '2') {
                        var ttlqty = slQty * boxQty;
                        if (ttlqty % 1 !== 0) {

                            scope.obj.PrescriptionList[index].T_SELL_QTY = Math.ceil(ttlqty) / boxQty;
                        }
                        if (stk_box < slQty) {
                            // alert('Invalid input');
                            alert(scope.getSingleMsg('S0018'));
                            scope.obj.PrescriptionList[index].T_SELL_QTY = null;
                        } else {
                            scope.obj.PrescriptionList[index].T_LINE_TOTAL =
                                parseFloat(scope.obj.PrescriptionList[index].T_PRICE_BOX) * slQty;
                            if (scope.obj.TotalPrice !== undefined) {
                                var total = 0;
                                for (var i = 0; i < scope.obj.PrescriptionList.length; i++) {
                                    if (scope.obj.PrescriptionList[i].T_LINE_TOTAL != undefined) {
                                        total += scope.obj.PrescriptionList[i].T_LINE_TOTAL;
                                    }
                                }
                                scope.obj.TotalPrice = total;
                            } else {
                                scope.obj.TotalPrice = scope.obj.PrescriptionList[index].T_LINE_TOTAL;
                            }

                            scope.obj.Med_Discount = (scope.obj.TotalPrice * scope.obj.Discount) / 100;
                            scope.obj.MedSubtotal = scope.obj.TotalPrice - scope.obj.Med_Discount;

                            scope.obj.MedVat = (scope.obj.MedSubtotal * scope.obj.vat) / 100;
                            scope.obj.MedNetamount = scope.obj.MedSubtotal + scope.obj.MedVat;
                            scope.obj.GrandTotal = scope.obj.MedNetamount + scope.obj.Netamount + scope.obj.commonTotal;


                        }
                    } else if (scope.obj.PrescriptionList[index].checkUnit === '1') {
                        if (scope.obj.PrescriptionList[index].T_SELL_QTY.indexOf('.') !== -1) {
                            //alert('Invalid input');
                            alert(scope.getSingleMsg('S0018'));
                            scope.obj.PrescriptionList[index].T_SELL_QTY = null;
                            return;
                        }
                        if (stk_pcs < slQty) {
                            //alert('Invalid input');
                            alert(scope.getSingleMsg('S0018'));
                            scope.obj.PrescriptionList[index].T_SELL_QTY = null;
                        } else {
                            scope.obj.PrescriptionList[index].T_LINE_TOTAL =
                                parseFloat(scope.obj.PrescriptionList[index].T_PRICE) * slQty;
                            if (scope.obj.TotalPrice !== undefined) {
                                var totals = 0;
                                for (var i = 0; i < scope.obj.PrescriptionList.length; i++) {
                                    if (scope.obj.PrescriptionList[i].T_LINE_TOTAL != undefined) {
                                        totals += scope.obj.PrescriptionList[i].T_LINE_TOTAL;
                                    }
                                }
                                scope.obj.TotalPrice = totals;
                            } else {
                                scope.obj.TotalPrice = scope.obj.PrescriptionList[index].T_LINE_TOTAL;
                            }

                            scope.obj.Med_Discount = (scope.obj.TotalPrice * scope.obj.Discount) / 100;
                            scope.obj.MedSubtotal = scope.obj.TotalPrice - scope.obj.Med_Discount;

                            scope.obj.MedVat = (scope.obj.MedSubtotal * scope.obj.vat) / 100;
                            scope.obj.MedNetamount = scope.obj.MedSubtotal + scope.obj.MedVat;
                            scope.obj.GrandTotal = scope.obj.MedNetamount + scope.obj.Netamount + scope.obj.commonTotal;


                        }
                    }
                }

            }
            scope.checkUnit = function (id) {
                if (scope.obj.PrescriptionList[id].checkUnit === '1') {
                    scope.obj.PrescriptionList[id].Unit = 'Pcs';

                } else if (scope.obj.PrescriptionList[id].checkUnit === '2') {
                    scope.obj.PrescriptionList[id].Unit = 'Box';
                }
                if (scope.obj.PrescriptionList[id].T_SELL_QTY != null) {


                    scope.obj.PrescriptionList[id].T_SELL_QTY = null;
                    if (scope.obj.TotalPrice !== null) {
                        scope.obj.TotalPrice -= scope.obj.PrescriptionList[id].T_LINE_TOTAL === undefined
                            ? 0
                            : scope.obj.PrescriptionList[id].T_LINE_TOTAL;
                        scope.obj.PrescriptionList[id].T_LINE_TOTAL = null;
                        // scope.obj.TotalPrice -= scope.obj.PrescriptionList[id].T_LINE_TOTAL;


                        scope.obj.Med_Discount = (scope.obj.TotalPrice * scope.obj.Discount) / 100;
                        scope.obj.MedSubtotal = scope.obj.TotalPrice - scope.obj.Med_Discount;

                        scope.obj.MedVat = (scope.obj.MedSubtotal * scope.obj.vat) / 100;
                        scope.obj.MedNetamount = scope.obj.MedSubtotal + scope.obj.MedVat;
                        scope.obj.GrandTotal = scope.obj.MedNetamount + scope.obj.Netamount + scope.obj.commonTotal;
                    } else {
                        scope.obj.TotalPrice = null;
                        scope.obj.Med_Discount = null;
                        scope.obj.MedSubtotal = null;
                        scope.obj.MedVat = null;
                        scope.obj.MedNetamount = null;
                        scope.obj.GrandTotal = scope.obj.Netamount + scope.obj.commonTotal;
                    }
                }
            }


            scope.print = function () {
                if (scope.obj.P_PRESCRIPTION != undefined) {
                    var chkT39 = T74046Service.chkT39(scope.obj.P_PRESCRIPTION);
                    chkT39.then(function (data) {
                        if (JSON.parse(data) > 0) {
                            $window.open("../T74046Report/T74046Report?T_REQUEST_ID=" + scope.obj.P_PRESCRIPTION,
                                "popup",
                                "width=600,height=600,left=100,top=50");
                        }
                    });

                } else {
                    //alert("You have to save first before you print");
                    alert(scope.getSingleMsg('S0019'));
                }
            }

            scope.printBill = function () {
                if (scope.obj.P_REQUEST_ID != undefined) {
                    var chkT36 = T74046Service.chkT36(scope.obj.P_REQUEST_ID);
                    chkT36.then(function (data) {
                        if (JSON.parse(data) > 0) {
                            $window.open("../T74046Report/T74046ReportBill?T_REQUEST_ID=" + scope.obj.P_REQUEST_ID,
                                "popup",
                                "width=600,height=600,left=100,top=50");
                        }
                    });
                } else {
                    //alert("You have to save first before you print");
                    alert(scope.getSingleMsg('S0019'));
                }
            };

            scope.printPatient = function () {
                //if (scope.obj.T74046.T_REQUEST_ID !== undefined) {
                //   var aaa = T74046Service.GetPatReport(scope.obj.T74046.T_REQUEST_ID);
                //   aaa.then(function(data) {
                //       if (JSON.parse(data) > 0) {
                //           $window.open("../T74046Report/GetPatReport?T_REQUEST_ID=" + scope.obj.T74046.T_REQUEST_ID,
                //               "popup",
                //               "width=600,height=600,left=100,top=50");
                //       }

                //});
                //} else {
                //    alert("You have to save first before you print");
                //}
                $window.open("../T74046Report/GetPatReport?T_REQUEST_ID=" + scope.obj.T74046.T_REQUEST_ID,
                    "popup",
                    "width=600,height=600,left=100,top=50");
            }
            scope.Disc_Report = function () {
                $window.open("../R74125/GetAllReportData?T_REQUEST_ID=" + scope.obj.T74046.T_REQUEST_ID,
                    "popup",
                    "width=600,height=600,left=50,top=50");
            }
            //Billing Part-------------Imran-------------End
            scope.onChatClick = function () {
                $window.location = "/Transaction/T74046";
            }
            scope.obj.delightScore = 50;
            scope.tabs = [
                {
                    //title: scope.vrDir + '/Images/Pat Info.jpg',
                    title: scope.vrDir + '/Images/patientInfo.png',
                    url: 'tabPatInfo.tpl.html',
                    hin: 'EMP',
                    text: 'Patient Info',
                    //stl: { 'background': 'radial-gradient(#dd4444, #c04040)', 'color': '#ffffff' }
                    stl: { 'background': 'linear-gradient(to right, #d74341 0%, #bb3d3e 100%)', 'color': '#ffffff' }
                    //stl: { 'width': '90px' }
                },
                {
                    //title: scope.vrDir + '/Images/Vital Sign.jpg',
                    title: scope.vrDir + '/Images/VitalSign.png',
                    url: 'tabDiagnostic.tpl.html',
                    hin: 'DIA',
                    text: 'Vital Sign',
                    //stl: { 'background': 'radial-gradient(#606a7c, #62666d)', 'color': '#ffffff' }
                    stl: { 'background': 'linear-gradient(to right, #6b7785 0%, #6c737b 100%)', 'color': '#ffffff' }
                    //stl: { 'width': '80px' }
                },
                {
                    //title: scope.vrDir + '/Images/Health Screening.jpg',
                    title: scope.vrDir + '/Images/HealthScreening.png',
                    url: 'tabMajor.tpl.html',
                    hin: 'MAJ',
                    text: 'Health Screening',
                    //stl: { 'background': 'radial-gradient(#7cb655, #61a137)', 'color': '#ffffff', 'font-size': '13.5px' }
                    stl: { 'background': 'linear-gradient(to right, #68b5a1 0%, #42947e 100%)', 'color': '#ffffff', 'font-size': '13.5px' }
                    //stl: { 'width': '115px' }
                },

                {
                    //title: scope.vrDir + '/Images/Medicaltion.jpg',
                    title: scope.vrDir + '/Images/Medication.png',
                    url: scope.vrDir + '/PartPages/T74134.html',
                    hin: 'MED',
                    text: 'Medication',
                    //stl: { 'background': 'radial-gradient(#5897a9, #427e8f)', 'color': '#ffffff' }
                    stl: { 'background': 'linear-gradient(to right, #4d90a1 0%, #3b7a8b 100%)', 'color': '#ffffff' }
                    //stl: { 'width': '80px' }
                },
                {
                    //title: scope.vrDir + '/Images/Services.jpg',
                    title: scope.vrDir + '/Images/Services.png',
                    url: scope.vrDir + '/PartPages/T74139.html',
                    hin: 'SER',
                    text: 'Services',
                    //stl: { 'background': 'radial-gradient(#f0b540, #eeaf36)', 'color': '#ffffff' }
                    stl: { 'background': 'linear-gradient(to right, #dab439 0%, #e79946 100%)', 'color': '#ffffff' }
                    //stl: { 'width': '80px' }
                },
                {
                    //title: scope.vrDir + '/Images/Hospital.jpg',
                    title: scope.vrDir + '/Images/Hospital1.png',
                    url: scope.vrDir + '/PartPages/T74138.html',
                    hin: 'Hos',
                    text: 'Hospital',
                    //stl: { 'background': 'radial-gradient(#904242, #7a2f2f)', 'color': '#ffffff' }
                    stl: { 'background': 'linear-gradient(to right, #955959 0%, #864c4b 100%)', 'color': '#ffffff' }

                    //stl: { 'width': '80px' }
                },
                {
                    //title: scope.vrDir + '/Images/Discharge.jpg',
                    title: scope.vrDir + '/Images/Discharge.png',
                    url: scope.vrDir + '/PartPages/T74140.html',
                    hin: 'DIS',
                    text: 'Discharge',
                    //stl: { 'background': 'radial-gradient(#4f535c, #353535)', 'color': '#ffffff' }
                    stl: { 'background': 'linear-gradient(to right, #464f56 0%, #2f3334 100%)', 'color': '#ffffff' }
                    //stl: { 'width': '80px' }
                }
                //{ title: 'Prescription', url: '/PartPages/T74113.html', hin: 'PRE' },
                //{ title: 'Bill', url: 'tabBill.tpl.html', hin: 'BILL' }
            ];
            //scope.tabsChat = [
            //    { title: 'Chat', url: 'tabPatInfo.tpl.html', hin: 'EMP' },
            //    { title: 'Health Screening', url: 'tabMajor.tpl.html', hin: 'MAJ' },
            //    { title: 'Vital Sign', url: 'tabDiagnostic.tpl.html', hin: 'DIA' },
            //    { title: 'Medication', url: '/PartPages/T74134.html', hin: 'MED' },
            //    { title: 'Services', url: '/PartPages/T74139.html', hin: 'SER' },
            //    { title: 'Hospital', url: '/PartPages/T74138.html', hin: 'Hos' },
            //    { title: 'Discharge', url: '/PartPages/T74140.html', hin: 'DIS' }
            //    //{ title: 'Prescription', url: '/PartPages/T74113.html', hin: 'PRE' },
            //    //{ title: 'Bill', url: 'tabBill.tpl.html', hin: 'BILL' }

            //];
            var param = JSON.parse(sessionStorage.getItem("param"));
            if (param === null) {
                scope.obj.Hidenfield = 'EMP';
                scope.currentTab = 'tabPatInfo.tpl.html';
                //scope.obj.Hidenfield = 'MAJ';
                //scope.currentTab = 'tabMajor.tpl.html';

            } else {
                scope.obj.Hidenfield = param.hin;
                scope.currentTab = param.url;
                scope.bill();

            }
            scope.currentTabChat = scope.vrDir + '/PartPages/T74141.html';
            scope.onClickTab = function (tab) {
                if (scope.isShowImage === true && scope.tabStatus === '1') {

                    scope.obj.Hidenfield = tab.hin;
                    scope.currentTab = tab.url;


                    if (tab.hin === 'EMP') {
                        if (scope.obj.T_START_TIME === null) {
                            scope.isShowImageHospital = false;
                        } else {
                            scope.isShowImageHospital = true;
                        }
                        scope.bill();
                    } else if (tab.hin === 'DIA') {
                        healthScreen();
                        mews();
                        gethealthData();
                        if (scope.obj.T_START_TIME === null) {
                            scope.isShowImageHospital = false;
                        } else {
                            scope.isShowImageHospital = true;
                        }

                    } else if (tab.hin === 'MAJ') {
                        healthScreen();
                        //gethealthData();
                        if (scope.obj.T_START_TIME === null) {
                            scope.isShowImageHospital = false;
                        } else {
                            scope.isShowImageHospital = true;
                        }

                    } else if (tab.hin === 'DIS') {
                        scope.isShowImageHospital = true;
                        sessionStorage.setItem("pat_125", JSON.stringify(scope.obj.T74046.T_PAT_ID));

                    } else if (tab.hin === 'Hos') {

                        if (scope.obj.T_START_TIME === null) {
                            scope.isShowImageHospital = false;
                        } else {
                            scope.isShowImageHospital = true;
                        }


                    } else if (tab.hin === 'SER') {
                        if (scope.obj.T_START_TIME === null) {
                            scope.isShowImageHospital = false;
                        } else {
                            scope.isShowImageHospital = true;
                        }

                    } else if (tab.hin === 'MED') {
                        if (scope.obj.T_START_TIME === null) {
                            scope.isShowImageHospital = false;
                        } else {
                            scope.isShowImageHospital = true;
                        }

                    }
                } else if (scope.isShowImage === true && scope.tabStatus === '2') {

                    if (tab.hin === 'Hos' || tab.hin === 'DIS') {
                        scope.obj.Hidenfield = tab.hin;
                        scope.currentTab = tab.url;
                        sessionStorage.setItem("pat_125", JSON.stringify(scope.obj.T74046.T_PAT_ID));
                        //if (scope.obj.T_START_TIME === null) {
                        //    scope.isShowImageHospital = false;
                        //} else {
                        //    scope.isShowImageHospital = true;
                        //}
                    }
                }
            }
            scope.isActiveTab = function (tabUrl) {
                return tabUrl === scope.currentTab;
            }

            //----------Tab Toggling--------------End

            // For Dropdown List Start
            var maritalLoad = T74046Service.MaritalData();
            maritalLoad.then(function (data) {

                scope.MaritalList = data;
            });

            var religionLoad = T74046Service.ReligionData();
            religionLoad.then(function (data) {

                scope.ReligionList = data;
            });

            var bloodGroupLoad = T74046Service.BloodGroupData();
            bloodGroupLoad.then(function (data) {

                scope.BloodGruopList = data;
            });

            var genderLoad = T74046Service.GenderData();
            genderLoad.then(function (data) {

                scope.GenderList = data;
            });

            var Nationality = T74046Service.NationalityData();
            Nationality.then(function (data) {

                scope.NationalityList = JSON.parse(data); 
            });

            var designation = T74046Service.DesignationData();
            designation.then(function (data) {

                scope.DesignationList = data;
            });

            var chiefComplaint = T74046Service.ChiefComplaintData();
            chiefComplaint.then(function (data) {
                scope.ChiefComplaintList = data;
            });

            var problemType = T74046Service.ProblemTypeData();
            problemType.then(function (data) {
                scope.ProblemTypeList = data;

            });


            //timer msg
            var alrtMsg = function () {
                var duration = T74046Service.getArrivedDuration(scope.obj.T74046.T_REQUEST_ID);
                duration.then(function (data) {
                    scope.du = data;

                    if (scope.du === 'true') {
                        alert("Please Select Next Destionation");
                    }
                });
            };


            //var startTime = function() {
            //    var interval = $interval(function() {
            //        alrtMsg();
            //        },
            //        5000);

            //    //$timeout(function() {
            //    //    $interval.cancel(interval);
            //    //        alert("2");
            //    //    },
            //    //    60000);

            //};

            //$timeout(function () {

            //    startTime();
            //},5000);

            //end timer msg

            scope.obj.T74041.T_GCS = 0;
            var gcs = T74046Service.getGCS();
            gcs.then(function (data) {
                scope.GCS = data.sort(function (a, b) { return (b.T_GCS_VALUE - a.T_GCS_VALUE) });
                scope.GCSEye = scope.GCS.filter(function (a) { return a.T_GCS_TYPE == 'E'; });
                scope.GCSMotor = scope.GCS.filter(function (a) { return a.T_GCS_TYPE == 'M'; });
                scope.GCSVerbal = scope.GCS.filter(function (a) { return a.T_GCS_TYPE == 'V'; });
            });
            scope.onEyeSelect = function () { GCSValue(); }
            scope.onMotorSelect = function () { GCSValue(); }
            scope.onVerbalSelect = function () { GCSValue(); }

            function GCSValue() {
                var a = parseFloat(scope.obj.e.selected.T_GCS_VALUE) +
                    parseFloat(scope.obj.m.selected.T_GCS_VALUE) +
                    parseFloat(scope.obj.v.selected.T_GCS_VALUE);
                scope.obj.T74041.T_GCS = a;
            }

            // For Dropdown List End
            scope.GetPrescription = function () {
                $window.location = "/Transaction/T74113";


            };
            scope.Instanttreatment = function () {
                $window.location = "/Transaction/T74134";
            };
            //------------- Validation start -------------

            scope.Temp_Click = function (e) {
                scope.obj.chekValidat = '1';
                if (e == 2) {
                    document.getElementById('txtEcg').focus();
                }

            }
            var distroyDischarge = $rootScope.$on('DischargeEvent',
                function (event, args) {
                    scope.obj.Hidenfield = 'Hos';
                    scope.currentTab = scope.vrDir + '/PartPages/T74138.html';


                });
            scope.$on('$destroy',
                function () {
                    distroyDischarge();
                });

            scope.$on('kjk', function (event, args) {
                scope.obj.T74046.T_FIRST_LANG2_NAME = args.message;
                console.log(scope.message);
            });
            //scope.BPSys_Click = function () {

            //    if (scope.obj.T74043.T_BP_SYS != "") {
            //        if (scope.obj.T74043.T_BP_SYS >= 50 && scope.obj.T74043.T_BP_SYS <= 150) {
            //            scope.obj.T74043.T_BP_SYS = scope.obj.T74043.T_BP_SYS;

            //        } else {

            //            alert('Please input 50 to 150 ');
            //            scope.obj.T74043.T_BP_SYS = '';

            //        }
            //    } else {
            //        // angular.element('#txtBPSys').focus();
            //    }

            //}
            //scope.BPDia_Click = function () {
            //    if (scope.obj.T74043.T_BP_DIA != "") {
            //        if (scope.obj.T74043.T_BP_DIA >= 50 && scope.obj.T74043.T_BP_DIA <= 150) {
            //            scope.obj.T74043.T_BP_DIA = scope.obj.T74043.T_BP_DIA;

            //        } else {

            //            alert('Please input 50 to 150 ');
            //            scope.obj.T74043.T_BP_DIA = '';

            //        }
            //    } else {
            //        //angular.element('#txtBPDia').focus();
            //    }

            //}
            //scope.Pulse_Click = function () {
            //    if (scope.obj.T74043.T_PULS != "") {
            //        if (scope.obj.T74043.T_PULS >= 50 && scope.obj.T74043.T_PULS <= 80) {
            //            scope.obj.T74043.T_PULS = scope.obj.T74043.T_PULS;

            //        } else {

            //            alert('Please input 50 to 150 ');
            //            scope.obj.T74043.T_PULS = '';

            //        }
            //    } else {
            //        // angular.element('#txtPulse').focus();
            //    }

            //}

            //---------Validation end------------ 

            var url = $location.absUrl();
            var reqid = url.split("=").pop();//NaN
            scope.requestIdFromQueristing = parseInt(reqid);
            if (reqid !== url) {
                scope.requestID = scope.requestIdFromQueristing;

            } else {
                var reqstId = T74046Service.getRequestID();
                reqstId.then(function (re) {
                    scope.requestID = re[0].T_REQUEST_ID;
                    // amCuRe(scope.requestID);
                });
            }


            scope.Request_Click = function () {

                var url = '';
                var data = '';
                RequestFactory.getModal(url, data);
            }

            scope.disableClnBtn = false;

            scope.obj.countRequest = 0;
            scope.obj.couPageload = 0;
            setInterval(function () {
                if (scope.obj.countRequest == 0) {
                    var reqstId = T74046Service.getRequestID();
                    reqstId.then(function (re) {
                        scope.requestID = re[0].T_REQUEST_ID;
                        // amCuRe(scope.requestID);
                        amCuRe();
                        mews();
                        scope.$apply();
                    });

                }
                if (document.getElementById('chartContainer') != undefined) {
                    gethealthData();
                    mews();
                    // healthScreen();
                }
                var newreqt = T74046Service.getNewReqId();
                newreqt.then(function (t) {
                    var c = t.length;
                    var m = t[0].T_REQUEST_ID;
                    if (scope.obj.couPageload == 0) {
                        if (scope.requestID !== m && c !== 0 && scope.requestID !==undefined) {
                            scope.obj.couPageload = 1;
                            alert(scope.getSingleMsg('S0136')); //Your request for Cancelation or Clean has not been approved
                            $window.location.reload();
                        }
                    }
                    
                });
                },
                5000);


            amCuRe();

            //--------- Ambulance current request start ----------
            function amCuRe() {
                if (scope.requestID != null) {


                    var amCuRe = T74046Service.getAmCuRe(scope.requestID);
                    amCuRe.then(function (data) {
                        scope.obj.countRequest = data.length;
                        scope.obj.T74117.T_REQUEST_ID = data[0].T_REQUEST_ID;
                        if (scope.obj.countRequest === 0) {
                            scope.isShowImage = true;
                        } else {
                            if (data[0].T_SEEN_TIME !== null) {
                                scope.isShowImage = true;
                                scope.tabStatus = '2';
                                if (data[0].T_CASE_ARRIVAL !== null) {
                                    scope.isCaseArrival = true;
                                    scope.tabStatus = '1';
                                    if (data[0].T_START_TIME === null) {
                                        scope.isShowImageHospital = false;
                                    } else {
                                        scope.isShowImageHospital = true;
                                    }
                                } else {
                                    scope.isCaseArrival = false;
                                }

                            } else {
                                if (data[0].T_ACCEPT_STATUS !== null) {
                                    scope.isShowImage = false;
                                } else {
                                    if (data[0].T_EVENT_FLAG === 5 || data[0].T_EVENT_FLAG === 2 || data[0].T_CAN_DATE != null) {
                                        scope.isShowImage = true;
                                    } else {
                                        scope.isShowImage = true;
                                    }

                                }
                            }

                            //disable clean button
                            scope.disableClnBtn = true;

                        }


                        if (data[0].T_ACCEPT_STATUS !== null) {

                            scope.obj.P_PRESCRIPTION = data[0].T_REQUEST_ID;
                            scope.obj.P_REQUEST_ID = data[0].T_REQUEST_ID;
                            scope.obj.T_START_TIME = data[0].T_START_TIME;
                            scope.obj.T_CASE_ARRIVAL = data[0].T_CASE_ARRIVAL;

                            scope.obj.T74046.T_PAT_ID = data[0].T_PAT_ID;
                            scope.obj.T74046.T_REQUEST_ID = data[0].T_REQUEST_ID;
                            scope.obj.T74046.T_REQUEST = data[0].T_REQUEST_ID;
                            scope.obj.T74046.T_FIRST_LANG2_NAME = data[0].T_FIRST_LANG2_NAME;
                            scope.obj.T74046.T_FATHER_LANG2_NAME = data[0].T_FATHER_LANG2_NAME;
                            scope.obj.T74046.T_MOTHER_LANG2_NAME = data[0].T_MOTHER_LANG2_NAME;
                            scope.obj.T74046.T_GFATHER_LANG2_NAME = data[0].T_GFATHER_LANG2_NAME;
                            scope.obj.T74046.T_FAMILY_LANG2_NAME = data[0].T_FAMILY_LANG2_NAME;
                            scope.obj.T74046.T_MOBILE_NO = data[0].T_MOBILE_NO;
                            scope.obj.T74046.T_AMBU_REG_ID = data[0].T_AMBU_REG_ID;
                            scope.obj.T_PROBLEM = data[0].T_PROBLEM;
                            scope.obj.T_PROBLEM_DURATION = data[0].T_PROBLEM_DURATION;

                            scope.obj.T74046.T_FATHER_LANG1_NAME = data[0].T_FATHER_LANG1_NAME;
                            scope.obj.T74046.T_FIRST_LANG1_NAME = data[0].T_FIRST_LANG1_NAME;
                            scope.obj.T74046.T_MOTHER_LANG1_NAME = data[0].T_MOTHER_LANG1_NAME;
                            scope.obj.T74046.T_GFATHER_LANG1_NAME = data[0].T_GFATHER_LANG1_NAME;
                            scope.obj.T74046.T_FAMILY_LANG1_NAME = data[0].T_FAMILY_LANG1_NAME;


                            scope.obj.R.selected = {
                                T_LANG2_NAME: data[0].Re_T_LANG2_NAME,
                                T_RLGN_CODE: data[0].T_RLGN_CODE
                            };
                            scope.obj.M.selected = {
                                T_LANG2_NAME: data[0].MRTL_T_LANG2_NAME,
                                T_MRTL_STATUS_CODE: data[0].T_MRTL_STATUS
                            };
                            scope.obj.G.selected =
                                { T_LANG2_NAME: data[0].Ge_T_LANG2_NAME, T_SEX_CODE: data[0].T_SEX_CODE };
                            scope.obj.N.selected = {
                                T_LANG2_NAME: data[0].Nationality_T_LANG2_NAME,
                                T_NTNLTY_ID: data[0].T_NTNLTY_ID
                            };
                            if (data[0].T_BIRTH_DATE === '/Date(-62135596800000)/') {
                                // T_BIRTH_DATE: "/Date(1546970400000)/"
                                // /Date(1231956000000)/
                            } else {
                                scope.obj.T74046.T_BIRTH_DATE = data[0].T_BIRTH_DATE;
                                scope.obj.T74046.T_BIRTH_DATE_PICK = data[0].T_BIRTH_DATE;
                            }

                            scope.obj.T74046.T_NTNLTY_ID = data[0].T_NTNLTY_ID;
                            scope.obj.T74041.T_AGE = data[0].T_AGE;
                            scope.obj.T74046.T_ADDRESS1 = data[0].T_ADDRESS1;
                            scope.obj.T74046.T_ADDRESS2 = data[0].T_ADDRESS2;
                            scope.obj.T74046.T_PASSPORT_NO = data[0].T_PASSPORT_NO;
                            scope.obj.T74046.T_POSTAL_CODE = data[0].T_POSTAL_CODE;
                            scope.obj.T74046.T_NATIONAL_ID = data[0].T_NATIONAL_ID;
                            scope.obj.T74046.T_OFFICE_NAME = data[0].T_OFFICE_NAME;
                            scope.obj.T74046.T_PHONE_WORK = data[0].T_PHONE_WORK;

                            scope.obj.patLat = data[0].patLat;
                            scope.obj.patLng = data[0].patLng;
                            scope.obj.teamLat = data[0].teamLat;
                            scope.obj.teamLng = data[0].teamLng;
                            scope.obj.T_APPRX_TIME = data[0].T_APPRX_TIME;
                            scope.obj.T_APPRX_DIST = data[0].T_APPRX_DIST;
                            var latlng = new google.maps.LatLng(scope.obj.patLat, scope.obj.patLng);
                            var geocoder = new google.maps.Geocoder();
                            geocoder.geocode({ 'latLng': latlng },
                                function (results, status) {
                                    if (status === google.maps.GeocoderStatus.OK) {
                                        if (results[0]) {
                                            scope.obj.ApprxLocation = results[0].formatted_address;
                                        }
                                    }
                                });
                            //var a = {};
                            //a = http://100.43.0.184:9090/ambucare/ambulance/v0/update/getGoogleMapsApi/ +
                            //    'TEAM6/' + '123/' + scope.obj.patLat + ',' + scope.obj.patLng + '/' + scope.obj.teamLat +
                            //    ',' + scope.obj.teamLng;

                        } else {
                            if (data[0].T_EVENT_FLAG === 5 || data[0].T_EVENT_FLAG === 2 || data[0].T_CAN_DATE != null) {
                                scope.obj.Hidenfield = 'Hos';
                                scope.currentTab = scope.vrDir + '/PartPages/T74138.html';
                            } else {
                                scope.obj.CurrentPatienRequest = data;
                                scope.obj.T_START_TIME = data[0].T_START_TIME;
                                //scope.isShowImage = true;
                            }

                        }
                        scope.$apply();
                    });
                }
            }

            function healthScreen(parameters) {
                var ecg = T74046Service.getECGImg(scope.obj.T74046.T_REQUEST_ID);
                ecg.then(function (e) {
                    scope.EcgList = e;
                });
                var health = T74046Service.getHealthScreenData(scope.obj.T74046.T_REQUEST_ID);
                health.then(function (data) {

                    T74046Service.getGCS().then(function (d) {
                        var e = data[0].T_EYE_OPEN == null ? 1 : data[0].T_EYE_OPEN;
                        var v = data[0].T_BEST_MOTOR == null ? 5 : data[0].T_BEST_MOTOR;
                        var m = data[0].T_VERBAL_RESPONSE == null ? 11 : data[0].T_VERBAL_RESPONSE;
                        var eye = d.filter(function (a) { return a.T_GCS_ID == e; });
                        var mot = d.filter(function (a) { return a.T_GCS_ID == v; });
                        var ver = d.filter(function (a) { return a.T_GCS_ID == m });

                        scope.obj.e.selected = {
                            T_GCS_ID: eye[0].T_GCS_ID,
                            T_GCS_VALUE: eye[0].T_GCS_VALUE,
                            T_GCS_TEXT: eye[0].T_GCS_TEXT
                        };
                        scope.obj.m.selected = {
                            T_GCS_ID: mot[0].T_GCS_ID,
                            T_GCS_VALUE: mot[0].T_GCS_VALUE,
                            T_GCS_TEXT: mot[0].T_GCS_TEXT
                        };
                        scope.obj.v.selected = {
                            T_GCS_ID: ver[0].T_GCS_ID,
                            T_GCS_VALUE: ver[0].T_GCS_VALUE,
                            T_GCS_TEXT: ver[0].T_GCS_TEXT
                        };
                        scope.obj.T74041.T_EYE_OPEN = eye[0].T_GCS_ID;
                        scope.obj.T74041.T_BEST_MOTOR = mot[0].T_GCS_ID;
                        scope.obj.T74041.T_VERBAL_RESPONSE = ver[0].T_GCS_ID;
                        scope.obj.T74041.T_EYE_OPEN_VALUE = eye[0].T_GCS_VALUE;
                        scope.obj.T74041.T_BEST_MOTOR_VALUE = mot[0].T_GCS_VALUE;
                        scope.obj.T74041.T_VERBAL_RESPONSE_VALUE = ver[0].T_GCS_VALUE;
                        GCSValue();
                        //scope.GCS = data.sort(function (a, b) { return (b.T_GCS_VALUE - a.T_GCS_VALUE) });
                        //scope.GCSEye = scope.GCS.filter(function (a) { return a.T_GCS_TYPE == 'E'; });
                        //scope.GCSMotor = scope.GCS.filter(function (a) { return a.T_GCS_TYPE == 'M'; });
                        //scope.GCSVerbal = scope.GCS.filter(function (a) { return a.T_GCS_TYPE == 'V'; });
                        empty74043();
                        scope.obj.chekValidat = '';
                        // scope.obj.T74043.T_PCHECKUP_ID = data[0].T_PCHECKUP_ID;
                        // scope.obj.T74043.T_TEMP = data[0].T_TEMP;
                        //// scope.obj.T74043.T_HIGHT = data[0].T_HIGHT;
                        // scope.obj.T74043.T_WEIGHT = data[0].T_WEIGHT;
                        // scope.obj.T74043.T_BP_SYS = data[0].T_BP_SYS;
                        // scope.obj.T74043.T_BP_DIA = data[0].T_BP_DIA;
                        // scope.obj.T74043.T_PULS = data[0].T_PULS;
                        // scope.obj.T74043.T_RESP = data[0].T_RESP;
                        // scope.obj.T74043.T_OS = data[0].T_OS;

                        scope.obj.T74041.T_PROBLEM = data[0].T_PROBLEM;
                        scope.obj.T74041.T_PROB_DETAILS = data[0].T_PROB_DETAILS;
                        scope.obj.T74041.T_PROBLEM_DURATION = data[0].T_PROBLEM_DURATION;
                        scope.obj.T74041.T_ICD10_SHORT_DESC2 = data[0].T_ICD10_SHORT_DESC2;
                        scope.obj.T74041.T_ICD10_CODE = data[0].T_ICD10_CODE;

                        scope.obj.Pr.selected = {
                            T_LANG2_NAME: data[0].PROBLEM_TYPE,
                            T_SPCLTY_ID: data[0].T_PROB_TYPE_ID
                        };
                        scope.obj.Ch.selected = { T_LANG2_NAME: data[0].CH_COM, T_CH_COMP: data[0].T_CH_COMP };
                        //if (data[0].T_SEEN_TIME !== null) {
                        //    scope.obj.T74041.Seen = '1';
                        //} else {
                        //    scope.obj.T74041.Seen = '';
                        //}
                        // scope.obj. = data[0].T_ECG_TEST;

                    });


                });
            }

            function mews() {
               
                var m = T74046Service.getMewsData(scope.requestID);
                m.then(function(me) {
                    var jsonData = JSON.parse(me);

                    scope.result = jsonData.filter(i => i.T_SCORE === 0 || i.T_SCORE === 1 || i.T_SCORE === 2);
                    if (scope.result.length>0) {
                        scope.Image = 'mm' + scope.result[0].T_SCORE;
                    } else {
                        if (jsonData[0].T_SCORE_1 === jsonData[0].T_SCORE_2) {
                            scope.Image = 'mm' + jsonData[0].T_SCORE+'C';
                        } else if (jsonData[0].T_SCORE_1 === 100) {
                            scope.Image = 'mm' + jsonData[0].T_SCORE;
                        }
                        else {
                            scope.Image = 'mm' + jsonData[0].T_SCORE;
                        }
                        //jsonData[0].T_SCORE;
                    }
                });
            }
            function empty74043() {
                scope.obj.T74043.T_TEMP = '';
                scope.obj.T74043.T_PULS = '';
                scope.obj.T74043.T_BP_SYS = '';
                scope.obj.T74043.T_BP_DIA = '';
                scope.obj.T74043.T_RESP = '';
                scope.obj.T74043.T_OS = '';
                scope.obj.T74043.T_WEIGHT = '';
                scope.obj.T74043.T_ECG_TEST = '';
                scope.obj.T74043.T_URINE_TEST = '';
                scope.obj.T74043.T_CONCIUS_LEVEL = '';
                scope.obj.C = {};
                scope.obj.C.selected = { T_CONS_LEVEL: '', T_LANG1_NAME: 'Select' };
            }

            function gethealthData(parameters) {
                var ecg = T74046Service.getECGImg(scope.obj.T74046.T_REQUEST_ID);
                ecg.then(function (e) {
                    scope.EcgList = e;
                });
                var health = T74046Service.healthScreenAllData(scope.obj.T74046.T_REQUEST_ID);
                health.then(function (data) {
                    scope.obj.HealthScreenList = data;
                    if (data.length > 0) {

                        //var a = scope.obj.HealthScreenList.sort(function (bb, aa) { return (bb.T_TIME - aa.T_TIME) });

                        var a = scope.obj.HealthScreenList;
                        scope.obj.VitalGraph = a;
                        var puls = [];
                        var temp = [];
                        var resp = [];
                        var syst = [];
                        var dyst = [];
                        for (var i = 0; i < a.length; i++) {
                            var p = {};
                            p.y = a[i].T_PULS;
                            p.x = a[i].T_TIME;
                            var t = {};
                            t.y = a[i].T_TEMP;
                            t.x = a[i].T_TIME;
                            var r = {};
                            r.y = a[i].T_RESP;
                            r.x = a[i].T_TIME;
                            var s = {};
                            s.y = a[i].T_BP_SYS;
                            s.x = a[i].T_TIME;
                            var d = {};
                            d.y = a[i].T_BP_DIA;
                            d.x = a[i].T_TIME;
                            puls.push(p);
                            temp.push(t);
                            resp.push(r);
                            syst.push(s);
                            dyst.push(d);
                        }

                        //------------------------------------------Chart Start---------------------
                        $(function () {
                            var chart = new CanvasJS.Chart("chartContainer",
                                {
                                    title: {},
                                    animationEnabled: true,
                                    //toolTip: {
                                    //    shared: true,
                                    //    content: function (e) {
                                    //        var body;
                                    //        var head;
                                    //        head = "<span style = 'color:DodgerBlue; '><strong>" +
                                    //            (e.entries[0].dataPoint.x) +
                                    //            " sec</strong></span><br/>";

                                    //        body = "<span style= 'color:" +
                                    //            e.entries[0].dataSeries.color +
                                    //            "'> " +
                                    //            e.entries[0].dataSeries.name +
                                    //            "</span>: <strong>" +
                                    //            e.entries[0].dataPoint.y +
                                    //            "</strong><br/> <span style= 'color:" +
                                    //            e.entries[1].dataSeries.color +
                                    //            "'> " +
                                    //            e.entries[1].dataSeries.name +
                                    //            "</span>: <strong>" +
                                    //            e.entries[1].dataPoint.y +
                                    //            "</strong>" +
                                    //            "<br/> <span style='color:" +
                                    //            e.entries[2].dataSeries.color +
                                    //            "'> " +
                                    //            e.entries[2].dataSeries.name +
                                    //            "</span>: <strong>" +
                                    //            e.entries[2].dataPoint.y +
                                    //            "</strong>" +
                                    //            "<br /> <span style='color:" +
                                    //            e.entries[3].dataSeries.color +
                                    //            "'> " +
                                    //            e.entries[3].dataSeries.name +
                                    //            "</span>: <strong>" +
                                    //            e.entries[3].dataPoint.y +
                                    //            "</strong><br /><span style='color:" +
                                    //            e.entries[4].dataSeries.color +
                                    //            "'> " +
                                    //            e.entries[4].dataSeries.name +
                                    //            "</span>: <strong>" +
                                    //            e.entries[4].dataPoint.y +
                                    //            "</strong>";

                                    //        return (head.concat(body));
                                    //    }
                                    //},

                                    axisY: {
                                        title: "Raw Data",
                                        includeZero: false,
                                        suffix: "",
                                        lineColor: "#369EAD",
                                        startValue: 0,
                                        endValue: 40
                                    },
                                    axisY2: {
                                        title: "Distance",
                                        includeZero: false,
                                        suffix: " m",
                                        lineColor: "#C24642"
                                    },

                                    axisX: {
                                        title: "Time",
                                        suffix: "",
                                        startValue: 0,
                                        endValue: 800,
                                        labelFormatter: function (e) {
                                            return (Math.floor(parseFloat(e.value) / 60)) +
                                                ":" +
                                                (Math.floor(parseFloat(e.value) % 60));
                                        }
                                    },
                                    data: [
                                        {
                                            type: "spline",
                                            name: "Resp",
                                            dataPoints: resp
                                        },
                                        {
                                            type: "spline",
                                            //axisYType: "secondary",
                                            name: "Temp",
                                            dataPoints: temp
                                        },
                                        {
                                            type: "spline",
                                            //axisYType: "Pulse",
                                            name: "Pulse",
                                            dataPoints: puls
                                        },
                                        {
                                            type: "spline",
                                            //axisYType: "Pulse",
                                            name: "Systole",
                                            dataPoints: syst
                                        },
                                        {
                                            type: "spline",
                                            //axisYType: "Pulse",
                                            name: "Diastole",
                                            dataPoints: dyst
                                        }
                                    ]
                                });

                            chart.render();
                        });
                        //------------------------------------------Chart End-----------------------
                    }
                });
            }


            scope.ShowRequest_Click = function (id) {
                if (scope.obj.T74046.T_REQUEST_ID === undefined && scope.obj.countRequest === 1) {
                    document.getElementById(id).style.display = "block";
                }

            }
            scope.CloseShowRequestPopup = function (id) {
                document.getElementById(id).style.display = "none";
            }
            scope.Accept_Click = function (id) {
                scope.getLatLong();
                if (scope.confLatLong() == 'ok') {

                    for (var i = 0; i < scope.obj.CurrentPatienRequest.length; i++) {

                        scope.obj.T74046.T_PAT_ID = scope.obj.CurrentPatienRequest[i].T_PAT_ID;
                        scope.obj.T74046.T_REQUEST_ID = scope.obj.CurrentPatienRequest[i].T_REQUEST_ID;
                        scope.obj.T74046.T_REQUEST = scope.obj.CurrentPatienRequest[i].T_REQUEST_ID;
                        scope.obj.T74046.T_FIRST_LANG2_NAME = scope.obj.CurrentPatienRequest[i].T_FIRST_LANG2_NAME;
                        scope.obj.T74046.T_FATHER_LANG2_NAME = scope.obj.CurrentPatienRequest[i].T_FATHER_LANG2_NAME;
                        scope.obj.T74046.T_MOTHER_LANG2_NAME = scope.obj.CurrentPatienRequest[i].T_MOTHER_LANG2_NAME;
                        scope.obj.T74046.T_GFATHER_LANG2_NAME = scope.obj.CurrentPatienRequest[i].T_GFATHER_LANG2_NAME;
                        scope.obj.T74046.T_FAMILY_LANG2_NAME = scope.obj.CurrentPatienRequest[i].T_FAMILY_LANG2_NAME;
                        scope.obj.T74046.T_MOBILE_NO = scope.obj.CurrentPatienRequest[i].T_MOBILE_NO;
                        scope.obj.T74046.T_AMBU_REG_ID = scope.obj.CurrentPatienRequest[i].T_AMBU_REG_ID;
                        scope.obj.T_PROBLEM = scope.obj.CurrentPatienRequest[i].T_PROBLEM;
                        scope.obj.T_PROBLEM_DURATION = scope.obj.CurrentPatienRequest[i].T_PROBLEM_DURATION;
                        scope.obj.T74041.T_AGE = scope.obj.CurrentPatienRequest[i].T_AGE;

                        scope.obj.T74046.T_FATHER_LANG1_NAME = scope.obj.CurrentPatienRequest[i].T_FATHER_LANG1_NAME;
                        scope.obj.T74046.T_FIRST_LANG1_NAME = scope.obj.CurrentPatienRequest[i].T_FIRST_LANG1_NAME;
                        scope.obj.T74046.T_MOTHER_LANG1_NAME = scope.obj.CurrentPatienRequest[i].T_MOTHER_LANG1_NAME;
                        scope.obj.T74046.T_GFATHER_LANG1_NAME = scope.obj.CurrentPatienRequest[i].T_GFATHER_LANG1_NAME;
                        scope.obj.T74046.T_FAMILY_LANG1_NAME = scope.obj.CurrentPatienRequest[i].T_FAMILY_LANG1_NAME;


                        scope.obj.R.selected = {
                            T_LANG2_NAME: scope.obj.CurrentPatienRequest[i].Re_T_LANG2_NAME,
                            T_RLGN_CODE: scope.obj.CurrentPatienRequest[i].T_RLGN_CODE
                        };
                        scope.obj.M.selected = {
                            T_LANG2_NAME: scope.obj.CurrentPatienRequest[i].MRTL_T_LANG2_NAME,
                            T_MRTL_STATUS_CODE: scope.obj.CurrentPatienRequest[i].T_MRTL_STATUS
                        };
                        scope.obj.G.selected = {
                            T_LANG2_NAME: scope.obj.CurrentPatienRequest[i].Ge_T_LANG2_NAME,
                            T_SEX_CODE: scope.obj.CurrentPatienRequest[i].T_SEX_CODE
                        };
                        scope.obj.N.selected = {
                            T_LANG2_NAME: scope.obj.CurrentPatienRequest[i].Nationality_T_LANG2_NAME,
                            T_NTNLTY_ID: scope.obj.CurrentPatienRequest[i].T_NTNLTY_ID
                        };
                        if (scope.obj.CurrentPatienRequest[i].T_BIRTH_DATE === '/Date(-62135596800000)/') {

                        } else {
                            scope.obj.T74046.T_BIRTH_DATE = scope.obj.CurrentPatienRequest[i].T_BIRTH_DATE;
                        }

                        scope.obj.T74046.T_NTNLTY_ID = scope.obj.CurrentPatienRequest[i].T_NTNLTY_ID;
                        scope.obj.T74046.T_ADDRESS1 = scope.obj.CurrentPatienRequest[i].T_ADDRESS1;
                        scope.obj.T74046.T_ADDRESS2 = scope.obj.CurrentPatienRequest[i].T_ADDRESS2;
                        scope.obj.T74046.T_PASSPORT_NO = scope.obj.CurrentPatienRequest[i].T_PASSPORT_NO;
                        scope.obj.T74046.T_POSTAL_CODE = scope.obj.CurrentPatienRequest[i].T_POSTAL_CODE;
                        scope.obj.T74046.T_NATIONAL_ID = scope.obj.CurrentPatienRequest[i].T_NATIONAL_ID;
                        scope.obj.T74046.T_OFFICE_NAME = scope.obj.CurrentPatienRequest[i].T_OFFICE_NAME;
                        scope.obj.T74046.T_PHONE_WORK = scope.obj.CurrentPatienRequest[i].T_PHONE_WORK;

                        var acc = T74046Service.acceptPatient(scope.obj.T74046.T_REQUEST_ID);
                        acc.then(function (data) {
                            document.getElementById(id).style.display = "none";
                            amCuRe();
                            var a = scope.getLatLong();
                            var t74026 = {};
                            t74026.T_REQUEST_ID = scope.obj.T74046.T_REQUEST_ID;
                            t74026.T_EVENT_ID = 4;
                            t74026.T_LATITUDE = a.lt;
                            t74026.T_LONGITUDE = a.ln;
                            T74046Service.save26(t74026);
                            //if (data) {
                            // alert('Docotor request has been sent to ' + data);
                            // }




                        });

                        scope.isShowImage = false;
                        //scope.tabStatus = '1';
                    }
                } else {
                    alert(scope.confLatLong());
                }
                // scope.obj.CurrentPatienRequest 

            }
            scope.Triage_Click = function (imag, value) {
                scope.Images = imag;
                //scope.obj.T74043.T_VERIFY_LEVEL = value;
                scope.obj.T74043.T_TRIAGE_LEVEL = value;
            }

            // document.getElementById("show").style.display = "block";
            scope.Seen_Click = function () {
                scope.getLatLong();
                if (scope.confLatLong() == 'ok') {
                    //if (scope.obj.T74041.Seen === '1') {
                    var dd = confirm("Are you sure that you have reached to patient ?");
                    if (dd === true) {
                        var acc = T74046Service.seenPatient(scope.obj.T74046.T_REQUEST_ID);
                        acc.then(function (data) {
                            scope.isShowImage = true;
                            // scope.tabStatus = '1';
                            scope.isCaseArrival = false;
                            // scope.isShowImageHospital = false;
                            var a = scope.getLatLong();
                            var t74026 = {};
                            t74026.T_REQUEST_ID = scope.obj.T74046.T_REQUEST_ID;
                            t74026.T_EVENT_ID = 6;
                            t74026.T_LATITUDE = a.lt;
                            t74026.T_LONGITUDE = a.ln;
                            T74046Service.save26(t74026);
                            // document.getElementById("show").style.display = "none";
                        });
                    } else {
                        alert(scope.confLatLong());
                    }

                }

                //} else {
                //    scope.obj.T74041.Seen = '1';
                //}


            }
            scope.GoForHospital_Click = function () {
                scope.getLatLong();
                if (scope.confLatLong() == 'ok') {
                    var con = confirm("Are you sure that you want to start for Destination ?");
                    if (con === true) {
                        var des = T74046Service.getDestination(1);
                        des.then(function (data) {
                            scope.cx = {
                                latitude: JSON.parse(data)[0].latitude,
                                longitude: JSON.parse(data)[0].longitude
                            };
                            scope.lat = JSON.parse(data)[0].latitude;
                            scope.lng = JSON.parse(data)[0].longitude;
                            var places = T74046Service.GetAllUserLatlong();
                            places.then(function (data) {
                                scope.places = JSON.parse(data);
                                var temp = [];
                                for (var i = 0; i < scope.places.length; i++) {
                                    var t = {};
                                    t.T_SITE_CODE = scope.places[i].T_SITE_CODE;
                                    t.latitude = scope.places[i].latitude;
                                    t.longitude = scope.places[i].longitude;
                                    t.distance =
                                        getDistanceFromLatLonInKm(scope.lat, scope.lng, t.latitude, t.longitude);
                                    temp.push(t);
                                }
                                var sing = temp[0].distance;
                                var st = temp[0].T_SITE_CODE;
                                for (var j = 0; j < temp.length; j++) {
                                    if (sing > temp[j].distance) {
                                        st = temp[j].T_SITE_CODE;
                                    }
                                }
                                var p = temp.filter(function (ar) {
                                    return ar.T_SITE_CODE == st;
                                });
                                scope.testLat = { latitude: p[0].latitude, longitude: p[0].longitude }
                                T74046Service.setHandoverHospital(st);
                                var a = scope.getLatLong();
                                var t74026 = {};
                                t74026.T_REQUEST_ID = scope.obj.T74046.T_REQUEST_ID;
                                t74026.T_EVENT_ID = 7;
                                t74026.T_LATITUDE = a.lt;
                                t74026.T_LONGITUDE = a.ln;
                                T74046Service.save26(t74026);


                                scope.isShowImageHospital = true;
                                scope.obj.T_START_TIME = '1';
                                scope.$apply();


                            });

                            // document.getElementById("show").style.display = "none";
                        });
                    }
                } else {
                    alert(scope.confLatLong());
                }

            }
            scope.CaseArrival_Click = function () {
                scope.getLatLong();
                if (scope.confLatLong() == 'ok') {
                    var dd = confirm("Have you come into view the case?");
                    if (dd === true) {
                        var acc = T74046Service.caseRecieved(scope.obj.T74046.T_REQUEST_ID);
                        acc.then(function (data) {
                            scope.isShowImage = true;
                            scope.tabStatus = '1';
                            scope.isCaseArrival = true;
                            scope.isShowImageHospital = false;
                            var a = scope.getLatLong();
                            var t74026 = {};
                            t74026.T_REQUEST_ID = scope.obj.T74046.T_REQUEST_ID;
                            t74026.T_EVENT_ID = 30;
                            t74026.T_LATITUDE = a.lt;
                            t74026.T_LONGITUDE = a.ln;
                            T74046Service.save26(t74026);
                            // document.getElementById("show").style.display = "none";
                        });
                    } else {
                        var cancel = confirm("Are you sure that patient is not Available ?");
                        if (cancel === true) {
                            sessionStorage.setItem("pat_125", JSON.stringify(scope.obj.T74046.T_PAT_ID));
                            scope.isCaseArrival = true;
                            scope.tabStatus = '2';
                            scope.obj.Hidenfield = 'DIS';
                            scope.currentTab = scope.vrDir + '/PartPages/T74140.html';
                        }
                    }
                } else {
                    alert(scope.confLatLong());
                }

            }
            scope.SuggestedHospital_Click = function () {
                document.getElementById("SuggestedHospital").style.display = "block";
            };


            scope.CleanAmbulance = function () {
                var cleanAmbulance = T74046Service.cleanAmbulance();

                cleanAmbulance.then(function (data) {
                    var dt = JSON.parse(data);
                    alert(dt);
                });
            }

            scope.CleanConfirmAmbulance = function () {
                var cleanAmbulance = T74046Service.cleanConfirmAmbulance(scope.obj.T74117);
                cleanAmbulance.then(function(data) {
                    var dt = JSON.parse(data);
                    alert(dt);
                    $window.location.reload();
                    if (scope.obj.T74117.T_REQUEST_ID !== null && scope.obj.T74117.T_REQUEST_ID !== undefined) {
                        var a = scope.getLatLong();
                        var t74026 = {};
                        t74026.T_REQUEST_ID = scope.obj.T74117.T_REQUEST_ID;
                        t74026.T_EVENT_ID = 32;
                        t74026.T_LATITUDE = a.lt;
                        t74026.T_LONGITUDE = a.ln;
                        T74046Service.save26(t74026);
                    }
                   
                });
            };

            scope.Accept_SuggestedHospital = function () {
                scope.getLatLong();
                if (scope.confLatLong() == 'ok') {
                    var con = confirm("Are you sure that you want to start for Destination ?");
                    if (con === true) {
                        var DocHos = T74046Service.confirmDocHos(scope.obj.T74046.T_REQUEST_ID, scope.SuggestedHosCode, scope.T_ROLE_CODE);

                        DocHos.then(function (data) {
                            if (data == 's') {
                                scope.obj.isSuggestedHospital = false;
                                document.getElementById("SuggestedHospital").style.display = "none";
                                if (scope.obj.Hidenfield === 'Hos') {
                                    $rootScope.$emit('T74138Emit', 'H');
                                }
                                var a = scope.getLatLong();
                                var t74026 = {};
                                t74026.T_REQUEST_ID = scope.obj.T74046.T_REQUEST_ID;
                                t74026.T_EVENT_ID = 9;
                                t74026.T_LATITUDE = a.lt;
                                t74026.T_LONGITUDE = a.ln;
                                T74046Service.save26(t74026);
                            }
                        });
                    }
                } else {
                    alert(scope.confLatLong());
                }
            };
            scope.Cancel_SuggestedHospital = function () {
                var ddl = scope.obj.T74041.T_CANCEL_TYPE_ID;
                if (ddl !== undefined) {
                    var canc = T74046Service.Cancel_Suggested_Hospital(scope.obj.T74046.T_REQUEST_ID, scope.SuggestedHosCode);
                    canc.then(function () {
                        scope.obj.Ca = {};
                        scope.obj.Ca.selected = '';
                        scope.obj.isSuggestedHospital = false;
                        document.getElementById("SuggestedHospital").style.display = "none";
                        alert('Cancel Successfully');
                    });
                } else {
                    alert('Select Cancel Reason !!!!');
                }
            };

            scope.SuggestedHospitalByOperator_Click = function () {
                scope.getLatLong();
                if (scope.confLatLong() == 'ok') {
                    var con = confirm("Are you sure that you want to start for Destination ?");
                    if (con === true) {
                        var OpeHos = T74046Service.reqAcceptofOper(scope.obj.T74046.T_REQUEST_ID,
                            scope.SuggestedHosCodeByOperator);

                        OpeHos.then(function (data) {
                            //  if (data == 's') {
                            scope.obj.isSuggestedHospitalByOperator = false;
                            if (scope.obj.Hidenfield === 'Hos') {
                                $rootScope.$emit('T74138Emit', 'H');
                            }
                            var a = scope.getLatLong();
                            var t74026 = {};
                            t74026.T_REQUEST_ID = scope.obj.T74046.T_REQUEST_ID;
                            t74026.T_EVENT_ID = 26;
                            t74026.T_LATITUDE = a.lt;
                            t74026.T_LONGITUDE = a.ln;
                            T74046Service.save26(t74026);
                            // }
                        });
                    }
                } else {
                    alert(scope.confLatLong());
                }
            }

            function deg2rad(deg) {
                return deg * (Math.PI / 180);
            }

            function getDistanceFromLatLonInKm(lat1, lon1, lat2, lon2) {
                var R = 6371; // Radius of the earth in km
                var dLat = deg2rad(parseFloat(lat2) - parseFloat(lat1)); // deg2rad below
                var dLon = deg2rad(parseFloat(lon2) - parseFloat(lon1));
                var a = Math.sin(dLat / 2) * Math.sin(dLat / 2) +
                    Math.cos(deg2rad(lat1)) * Math.cos(deg2rad(lat2)) * Math.sin(dLon / 2) * Math.sin(dLon / 2);
                var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
                var d = R * c; // Distance in km
                return d;
            }

            var canReason = T74046Service.getCancelReasonData();
            canReason.then(function (data) {
                scope.CancelReasonList = data;
            });
            //Changed for MOH
            //scope.Cancel_Click = function () {
            //    scope.getLatLong();
            //    if (scope.confLatLong() == 'ok') {
            //        for (var i = 0; i < scope.obj.CurrentPatienRequest.length; i++) {
            //            if (scope.obj.CurrentPatienRequest[i].T_CANCEL_REASON === undefined ||
            //                scope.obj.CurrentPatienRequest[i].T_CANCEL_REASON === '') {
            //                //alert('Why do you want to cancel? Please fill up cancel reason');
            //                alert(scope.getSingleMsg('S0020'));
            //            } else {
            //                scope.obj.T74041.T_CANCEL_REASON = scope.obj.CurrentPatienRequest[i].T_CANCEL_REASON;
            //                scope.obj.T74041.T_REQUEST_ID = scope.obj.CurrentPatienRequest[i].T_REQUEST_ID;
            //                // scope.obj.T74041.T_USER_ID = scope.obj.CurrentPatienRequest[i].T_USER_ID;


            //                //-----------------------
            //                var sendQlist = T74046Service.cancelAndRerequest('0', scope.obj.T74041);
            //                sendQlist.then(function (data) {
            //                    document.getElementById('RequestShow').style.display = "none";
            //                    amCuRe();

            //                    var a = scope.getLatLong();
            //                    var t74026 = {};
            //                    t74026.T_REQUEST_ID = scope.obj.T74041.T_REQUEST_ID;
            //                    t74026.T_EVENT_ID = 5;
            //                    t74026.T_LATITUDE = a.lt;
            //                    t74026.T_LONGITUDE = a.ln;
            //                    var kk = T74046Service.save26(t74026);
            //                    kk.then(function (data) {
            //                        scope.obj.Hidenfield = 'Hos';
            //                        scope.currentTab = scope.vrDir + '/PartPages/T74138.html';
            //                    });
            //                });
            //                //-----------------------




            //            }
            //        }


            //    } else {
            //        alert(scope.confLatLong());
            //    }
            //};

            scope.Cancel_Click = function (id) { // it is previous 
                scope.getLatLong();
                if (scope.confLatLong() == 'ok') {
                    for (var i = 0; i < scope.obj.CurrentPatienRequest.length; i++) {
                        if (scope.obj.CurrentPatienRequest[i].T_CANCEL_REASON === undefined ||
                            scope.obj.CurrentPatienRequest[i].T_CANCEL_REASON === '') {
                            //alert('Why do you want to cancel? Please fill up cancel reason');
                            alert(scope.getSingleMsg('S0020'));
                        } else {
                            scope.obj.T74041.T_CANCEL_REASON = scope.obj.CurrentPatienRequest[i].T_CANCEL_REASON;
                            scope.obj.T74041.T_REQUEST_ID = scope.obj.CurrentPatienRequest[i].T_REQUEST_ID;

                            var places = T74041Service.GetAllUserLatlong();
                            places.then(function (data) {
                                scope.places = JSON.parse(data);
                                if (scope.places.length > 0) {
                                    shortDistance();
                                } else {
                                    var con = confirm('Ambulance is not Available. Do you want to send Pending list?');
                                    if (con === true) {
                                        var sendQlist = T74046Service.cancelAndRerequest('0', scope.obj.T74041);
                                        sendQlist.then(function (data) {
                                            document.getElementById('RequestShow').style.display = "none";
                                            amCuRe();

                                            var a = scope.getLatLong();
                                            var t74026 = {};
                                            t74026.T_REQUEST_ID = scope.obj.T74041.T_REQUEST_ID;
                                            t74026.T_EVENT_ID = 5;
                                            t74026.T_LATITUDE = a.lt;
                                            t74026.T_LONGITUDE = a.ln;
                                            var kk = T74046Service.save26(t74026);
                                            kk.then(function (data) {
                                                scope.obj.Hidenfield = 'Hos';
                                                scope.currentTab = scope.vrDir + '/PartPages/T74138.html';
                                            });
                                        });
                                    }
                                }
                                //scope.$apply();
                                // console.log(scope.places);

                            });


                        }
                    }


                } else {
                    alert(scope.confLatLong());
                }
            };

            function shortDistance() {
                var temp = [];
                for (var i = 0; i < scope.places.length; i++) {
                    var t = {};
                    t.T_AMBU_REG_ID = scope.places[i].T_AMBU_REG_ID;
                    t.AMB_CAPASITY = scope.places[i].AMB_CAPASITY;
                    t.latitude = scope.places[i].latitude;
                    t.longitude = scope.places[i].longitude;
                    t.distance = getDistanceFromLatLonInKm(scope.lat, scope.lng, t.latitude, t.longitude);

                    temp.push(t);
                }

                var sing = temp[0].distance;
                var st = temp[0].T_AMBU_REG_ID;
                for (var j = 0; j < temp.length; j++) {
                    if (sing > temp[j].distance) {
                        st = temp[j].T_AMBU_REG_ID;
                    }
                }
                var p = temp.filter(function (ar) {
                    return ar.T_AMBU_REG_ID == st;
                });
                scope.testLat = { latitude: p[0].latitude, longitude: p[0].longitude }
                var reRequest = T74046Service.cancelAndRerequest(p[0].T_AMBU_REG_ID, scope.obj.T74041);
                reRequest.then(function (data) {
                    document.getElementById('RequestShow').style.display = "none";
                    amCuRe();

                    var a = scope.getLatLong();
                    var t74026 = {};
                    t74026.T_REQUEST_ID = scope.obj.T74041.T_REQUEST_ID;
                    t74026.T_EVENT_ID = 5;
                    t74026.T_LATITUDE = a.lt;
                    t74026.T_LONGITUDE = a.ln;
                    var dd = T74046Service.save26(t74026);
                    dd.then(function (data) {
                        scope.obj.Hidenfield = 'Hos';
                        scope.currentTab = scope.vrDir + '/PartPages/T74138.html';
                    });
                });
                scope.$apply();

            }

            function VitalGraph(q) {
                var a = scope.obj.VitalGraph;
                var dt = [];
                var h_name = "";
                var title = "";
                var syst = [];
                var dyst = [];
                for (var i = 0; i < a.length; i++) {
                    switch (q) {
                        case 't':
                            var t = {};
                            t.y = a[i].T_TEMP;
                            t.x = a[i].T_TIME;
                            dt.push(t);
                            h_name = "Temp";
                            title = "Temperature";
                            break;
                        case 'p':
                            var p = {};
                            p.y = a[i].T_PULS;
                            p.x = a[i].T_TIME;
                            dt.push(p);
                            h_name = "Pulse";
                            title = "Pulse";
                            break;
                        case 'ds':
                            var s = {};
                            s.y = a[i].T_BP_SYS;
                            s.x = a[i].T_TIME;
                            var d = {};
                            d.y = a[i].T_BP_DIA;
                            d.x = a[i].T_TIME;
                            syst.push(s);
                            dyst.push(d);
                            title = "Blood Pressure";
                            break;
                        case 'r':
                            var r = {};
                            r.y = a[i].T_RESP;
                            r.x = a[i].T_TIME;
                            dt.push(r);
                            h_name = "Resp";
                            title = "Respiretory";
                            break;

                    }
                }
                var k = [];
                if (q != 'ds') {
                    k = [{ type: "spline", name: h_name, dataPoints: dt }];
                } else {
                    k = [
                        { type: "spline", name: 'Systole', dataPoints: syst },
                        { type: "spline", name: 'Diastole', dataPoints: dyst }
                    ];
                }

                //------------------------------------------Chart Start---------------------
                $(function () {
                    var chart = new CanvasJS.Chart("chartContainer",
                        {
                            title: {},
                            animationEnabled: true,
                            //toolTip: {
                            //    shared: true,
                            //    content: function(e) {
                            //        var body;
                            //        var head;
                            //        head = "<span style = 'color:DodgerBlue; '><strong>" +
                            //            (e.entries[0].dataPoint.x) +
                            //            " sec</strong></span><br/>";

                            //        //body = "<span style= 'color:" +
                            //        //    e.entries[0].dataSeries.color +
                            //        //    "'> " +
                            //        //    e.entries[0].dataSeries.name +
                            //        //    "</span>: <strong>" +
                            //        //    e.entries[0].dataPoint.y +
                            //        //    "</strong>";
                            //        body = "";
                            //        for (var j = 0; j < e.entries.length; j++) {
                            //            body += "<span style= 'color:" +
                            //                e.entries[j].dataSeries.color +
                            //                "'> " +
                            //                e.entries[j].dataSeries.name +
                            //                "</span>: <strong>" +
                            //                e.entries[j].dataPoint.y +
                            //                "</strong></br>";
                            //        }

                            //        return (head.concat(body));
                            //    }
                            //},

                            axisY: {
                                title: title,
                                includeZero: false,
                                suffix: "",
                                lineColor: "#369EAD",
                                startValue: 0,
                                endValue: 40
                            },
                            axisY2: {
                                title: "Distance",
                                includeZero: false,
                                suffix: " m",
                                lineColor: "#C24642"
                            },

                            axisX: {
                                title: "Time",
                                suffix: "",
                                startValue: 0,
                                endValue: 800,
                                labelFormatter: function (e) {
                                    return (Math.floor(parseFloat(e.value) / 60)) +
                                        ":" +
                                        (Math.floor(parseFloat(e.value) % 60));
                                }
                            },
                            data: k
                        });

                    chart.render();
                });
                //------------------------------------------Chart End-----------------------
            }

            scope.onVitalSelect = function (e) {
                VitalGraph(e);
            }
            //--------- Ambulance current request end ----------

            //For Insert Update Start
            scope.Save_Click = function () {
                var t43 = '';
                var t41 = '';
                scope.obj.T74041.T_REQUEST_ID = scope.obj.T74046.T_REQUEST_ID;
                if (scope.obj.Hidenfield == 'EMP') {
                    sessionStorage.setItem("RequestId", JSON.stringify(scope.obj.T74046.T_REQUEST_ID));
                    if (scope.obj.T74046.T_REQUEST !== 0) {
                        var Employee = T74046Service.SaveEmployee(scope.obj.T74046, scope.obj.T74041);
                        Employee.then(function (data) {
                            alert(data);
                            amCuRe();
                        });
                    } else {
                        alert("Data is not Available !!!!");
                    }

                } else if (scope.obj.Hidenfield === 'DIA') {
                    sessionStorage.setItem("RequestId", JSON.stringify(scope.obj.T74043.T_REQUEST_ID));
                    if (scope.obj.T74043.T_TEMP === '') {
                        scope.obj.T74043.T_TEMP = scope.obj.HealthScreenList[0].T_TEMP;
                    }
                    if (scope.obj.T74043.T_PULS === '') {
                        scope.obj.T74043.T_PULS = scope.obj.HealthScreenList[0].T_PULS;
                    }
                    if (scope.obj.T74043.T_BP_SYS === '') {
                        scope.obj.T74043.T_BP_SYS = scope.obj.HealthScreenList[0].T_BP_SYS;
                    }
                    if (scope.obj.T74043.T_BP_DIA === '') {
                        scope.obj.T74043.T_BP_DIA = scope.obj.HealthScreenList[0].T_BP_DIA;
                    }
                    if (scope.obj.T74043.T_RESP === '') {
                        scope.obj.T74043.T_RESP = scope.obj.HealthScreenList[0].T_RESP;
                    }
                    if (scope.obj.T74043.T_OS === '') {
                        scope.obj.T74043.T_OS = scope.obj.HealthScreenList[0].T_OS;
                    }
                    // if (scope.obj.T74043.T_ECG_TEST === '') { scope.obj.T74043.T_ECG_TEST = scope.obj.HealthScreenList[0].T_ECG_TEST; }
                    if (scope.obj.T74043.T_URINE_TEST === '') {
                        scope.obj.T74043.T_URINE_TEST = scope.obj.HealthScreenList[0].T_URINE_TEST;
                    }
                    if (scope.obj.T74043.T_CONCIUS_LEVEL === '') {
                        scope.obj.T74043.T_CONCIUS_LEVEL = scope.obj.HealthScreenList[0].T_CONCIUS_LEVEL_id;
                    }

                    // if (scope.obj.T74043.T_WEIGHT === '') { scope.obj.T74043.T_WEIGHT = scope.obj.HealthScreenList[0].T_WEIGHT; }

                    if (scope.obj.chekValidat === '1') {
                        var Diagnosis = T74046Service.SaveDiagnosis(scope.obj.T74043);
                        scope.loader(true);
                        Diagnosis.then(function (data) {
                            // alert(scope.getSingleMsg('S0012'));
                            alert(data);
                            healthScreen();
                            mews();
                            gethealthData();
                            empty74043();
                            scope.obj.chekValidat = '';
                            //scope.obj.T74043.T_TEMP = '';
                            //scope.obj.T74043.T_PULS = '';
                            //scope.obj.T74043.T_BP_SYS = '';
                            //scope.obj.T74043.T_BP_DIA = '';
                            //scope.obj.T74043.T_RESP = '';
                            //scope.obj.T74043.T_OS = '';
                            //scope.obj.T74043.T_WEIGHT = '';
                            //  t43 = '1';
                            scope.loader(false);
                        });
                    } else {
                        empty74043();
                        //scope.obj.T74043.T_TEMP = '';
                        //scope.obj.T74043.T_PULS = '';
                        //scope.obj.T74043.T_BP_SYS = '';
                        //scope.obj.T74043.T_BP_DIA = '';
                        //scope.obj.T74043.T_RESP = '';
                        //scope.obj.T74043.T_OS = '';
                        //scope.obj.T74043.T_WEIGHT = '';
                    }


                } else if (scope.obj.Hidenfield == 'BILL') {
                    var param = JSON.parse(sessionStorage.getItem("param"));

                    //scope.obj.T74027 = [];
                    //for (var i = 0; i < scope.obj.PrescriptionList.length; i++) {
                    //    if (scope.obj.PrescriptionList[i].T_SELL_QTY!=null) {
                    //        scope.obj.t74027 = {};
                    //        scope.obj.t74027.T_ITEM_CODE = scope.obj.PrescriptionList[i].T_ITEM_CODE;
                    //        scope.obj.t74027.T_STORE_ID = scope.obj.PrescriptionList[i].T_STORE_ID;
                    //        scope.obj.t74027.T_ITEM_UM_ID = scope.obj.PrescriptionList[i].T_ITEM_UM_ID;
                    //        scope.obj.T74027.push(scope.obj.t74027);

                    //    }

                    //}
                    scope.obj.T74036 = {};
                    scope.obj.T74036.T_DOC_CODE = scope.obj.param.T_Doc_ID;
                    scope.obj.T74036.T_REQUEST_ID = scope.obj.param.T_REQUEST_ID;
                    scope.obj.T74036.T_STORE_ID = scope.obj.PrescriptionList[0].T_STORE_ID;
                    scope.obj.T74036.T_DISCOUNT = scope.obj.Discount;
                    scope.obj.T74036.T_TOTAL = scope.obj.TotalPrice;
                    scope.obj.T74036.T_VAT = scope.obj.vat;
                    scope.obj.T74036.T_GRAND_TOTAL = scope.obj.MedNetamount;


                    scope.obj.T74037 = [];
                    for (var j = 0; j < scope.obj.PrescriptionList.length; j++) {
                        if (scope.obj.PrescriptionList[j].T_SELL_QTY != null) {
                            scope.obj.t74037 = {};
                            scope.obj.t74037.T_ITEM_ID = scope.obj.PrescriptionList[j].T_ITEM_CODE;
                            scope.obj.t74037.T_UOM_ID = scope.obj.PrescriptionList[j].T_ITEM_UM_ID;

                            if (scope.obj.PrescriptionList[j].checkUnit === '2') {
                                scope.obj.t74037.T_QUANTITY = scope.obj.PrescriptionList[j].T_SELL_QTY;
                                scope.obj.t74037.T_SALE_PRICE = scope.obj.PrescriptionList[j].T_PRICE_BOX;
                            } else if (scope.obj.PrescriptionList[j].checkUnit === '1') {
                                scope.obj.t74037.T_QUANTITY =
                                    scope.obj.PrescriptionList[j].T_SELL_QTY / scope.obj.PrescriptionList[j].BOX_QTY;
                                scope.obj.t74037.T_SALE_PRICE = scope.obj.PrescriptionList[j].T_PRICE *
                                    scope.obj.PrescriptionList[j].BOX_QTY;
                            }
                            scope.obj.t74037.T_PCS_BOX = scope.obj.PrescriptionList[j].checkUnit;

                            scope.obj.T74037.push(scope.obj.t74037);
                        }

                    }

                    scope.obj.T74074 = {};
                    scope.obj.T74074.T_REQUEST_ID = scope.obj.param.T_REQUEST_ID;
                    scope.obj.T74074.T_TOTAL_PRICE = scope.obj.GrandTotal;

                    scope.obj.T74079 = [];
                    for (var l = 0; l < scope.obj.DiagonosisListByReq.length; l++) {
                        if (scope.obj.DiagonosisListByReq[l].T_PRICE != null) {
                            scope.obj.t74079 = {};
                            scope.obj.t74079.T_COST_TYPE_DTL_ID = scope.obj.DiagonosisListByReq[l].T_COST_TYPE_DTL_ID;
                            scope.obj.t74079.T_PRICE = scope.obj.DiagonosisListByReq[l].T_PRICE;
                            scope.obj.t74079.T_DIAGONOSIS_ID = scope.obj.DiagonosisListByReq[l].T_DIAGONOSIS_ID;
                            scope.obj.t74079.T_VAT = scope.obj.vat;
                            scope.obj.t74079.T_DISCOUNT = scope.obj.Discount;

                            scope.obj.T74079.push(scope.obj.t74079);
                        }
                    }
                    if (scope.obj.AmbulancePrice != null) {
                        scope.obj.t74079 = {};
                        scope.obj.t74079.T_COST_TYPE_DTL_ID = scope.obj.AmbulancePrice_DtlID;
                        scope.obj.t74079.T_PRICE = scope.obj.AmbulancePrice;
                        scope.obj.T74079.push(scope.obj.t74079);
                    }
                    if (scope.obj.DoctorPrice != null) {
                        scope.obj.t74079 = {};
                        scope.obj.t74079.T_COST_TYPE_DTL_ID = scope.obj.DoctorPrice_dtlId;
                        scope.obj.t74079.T_PRICE = scope.obj.DoctorPrice;
                        scope.obj.T74079.push(scope.obj.t74079);
                    }

                    if (scope.obj.ServiceList != undefined) {
                        for (var k = 0; k < scope.obj.ServiceList.length; k++) {
                            if (scope.obj.ServiceList[k].T_PRICE != null) {
                                scope.obj.t74079 = {};
                                scope.obj.t74079.T_COST_TYPE_DTL_ID = scope.obj.ServiceList[k].T_COST_TYPE_DTL_ID;
                                scope.obj.t74079.T_PRICE = scope.obj.ServiceList[k].T_PRICE;
                                scope.obj.T74079.push(scope.obj.t74079);
                            }
                        }
                    }


                    //var t27 = scope.obj.T74027;
                    var t36 = scope.obj.T74036;
                    var t37 = scope.obj.T74037;
                    var t74 = scope.obj.T74074;
                    var t79 = scope.obj.T74079;
                    var Bill = T74046Service.SaveBill(t36, t37, t74, t79);
                    Bill.then(function (data) {
                        alert("Data Saved Sucessfully");
                        scope.clear();
                        scope.obj.P_REQUEST_ID = scope.obj.param.T_REQUEST_ID;
                        sessionStorage.removeItem("param");

                    });

                } else if (scope.obj.Hidenfield == 'MED') {
                    $rootScope.$emit('T74134Emit', 'm');
                } else if (scope.obj.Hidenfield == 'SER') {

                    $rootScope.$emit('T74139Emit', 's');
                } else if (scope.obj.Hidenfield == 'PRE') {
                    $rootScope.$emit('T74113Emit', 'p');
                } else if (scope.obj.Hidenfield == 'MAJ') {
                    scope.obj.T74041.T_REQUEST_ID = scope.obj.T74046.T_REQUEST_ID;
                    var diag = T74046Service.SaveDiagT74041(scope.obj.T74041);
                    diag.then(function (data) {
                        // alert(scope.getSingleMsg('S0012'));
                        alert(data);
                        // t43 = '2';
                    });

                }

            }
            scope.clear = function () {
                scope.obj.ServiceList = [];
                scope.obj.ServicePrice = [];
                scope.obj.PrescriptionList = [];
                scope.obj.DiagonosisListByReq = [];
                scope.obj.AmbulancePrice = null;
                scope.obj.DoctorPrice = null;
                scope.obj.commonTotal = null;
                scope.obj.MedNetamount = null;
                scope.obj.Netamount = null;
                scope.obj.GrandTotal = null;
                scope.obj.Vat = null;
                scope.obj.Dia_Discount = null;
                scope.obj.Subtotal = null;
                scope.obj.DiagonosisPrice = null;
                scope.obj.TotalPrice = null;
                scope.obj.Med_Discount = null;
                scope.obj.Subtotal = null;
                scope.obj.vat = null;
                scope.obj.Discount = null;
                scope.obj.MedVat = null;
                scope.obj.Discount = null;
                scope.obj.MedSubtotal = null;
            }
            //For Insert Update End
            //-------------- For Autosearch start--------------
            scope.OpenSearchText_Click = function () {
                scope.obj.T74041.T_ICD10_SHORT_DESC2 = '';
                // document.getElementById('txtAutoSearchTexBox').focus();
                // scope.AutoSearchTexBox = true;
                document.getElementById('txtAutoSearchTexBox').focus();
                scope.obj.ICD10 = '';
            }

            scope.AutoSearch_Click = function (icd) {
                var geticd = T74046Service.getICD10DataInSearch(icd);
                geticd.then(function (data) {
                    if (data.length !== 0) {
                        scope.obj.ICDList = data;
                        scope.SearTable = true;
                    } else {
                        scope.SearTable = false;
                    }


                });
            }
            scope.SelectSearchcc_Click = function (slobj) {
                scope.obj.T74041.T_ICD10_SHORT_DESC2 = slobj.T_ICD10_SHORT_DESC2;
                scope.obj.T74041.T_ICD10_CODE = slobj.T_ICD10_CODE;
                scope.SearTable = false;
                //  scope.obj.ICD10 = '';
                scope.AutoSearchTexBox = false;
            }
            //-------------------For Autosearch start---------------------------
            //  For Common===============
            scope.$watch('obj.T_AMBU_REG_ID',
                Change_Click = function () {

                    if (scope.obj.T_AMBU_REG_ID != null) {

                        var ambulance = T74046Service.getAmbulance(scope.obj.T_AMBU_REG_ID);
                        ambulance.then(function (data) {
                            scope.obj.AmbulList = data;
                            var Price = 0;
                            for (var i = 0; i < data.length; i++) {
                                Price = Price + data[i].T_PRICE;
                            }
                            scope.obj.commonTotal = Price;
                        });

                        var service = T74046Service.GetService(scope.obj.T_AMBU_REG_ID);
                        service.then(function (data) {
                            scope.obj.ServiceList = data;
                            var Price = 0;
                            for (var i = 0; i < data.length; i++) {
                                Price = Price + data[i].T_PRICE;
                            }
                            scope.obj.ServiceTotal = Price;

                        });

                    }
                });


            //===============

            // for vat start===================
            var vatAmount = 0;
            var discountAmount = 0;
            var vat_Discount = T74046Service.getVet_Discount();
            vat_Discount.then(function (data) {
                for (var i = 0; i < data.length; i++) {
                    if (data[i].T_DESCRIPTION === 'VAT' && data[i].T_ACTIVE === 1) {
                        vatAmount = data[i].T_AMOUNT;
                    } else {
                        scope.obj.vat = 0;
                    }

                    if (data[i].T_DESCRIPTION === 'Discount' && data[i].T_ACTIVE === 1) {
                        discountAmount = data[i].T_AMOUNT;
                    } else {
                        scope.obj.vat = 0;
                    }
                    scope.obj.vat = vatAmount;
                    scope.obj.Discount = discountAmount;

                }

                //scope.obj.vat = data;
                //scope.obj.Discount = data;

            });

            // for vat end===================


            //Diagnosis ===================

            scope.$watch('obj.T_REQUEST_ID',
                BillRequestChange_Click = function () {
                    //  var TotalDiaPirct = 0;
                    if (scope.obj.T_REQUEST_ID != null) {

                        var diagnosis = T74046Service.GetDiagnosis(scope.obj.T_REQUEST_ID);
                        diagnosis.then(function (data) {
                            scope.obj.DiagnosisList = data;

                            var Diagprice = 0;
                            for (var i = 0; i < data.length; i++) {
                                Diagprice = Diagprice + data[i].T_PRICE;
                            }
                            scope.obj.ToTal_Diag_Price = Diagprice;
                            // TotalDiaPirct = Diagprice;

                            scope.obj.Dia_Discount = (scope.obj.ToTal_Diag_Price * scope.obj.Discount) / 100;
                            scope.obj.Subtotal = scope.obj.ToTal_Diag_Price - scope.obj.Dia_Discount;

                            scope.obj.Vat = (scope.obj.Subtotal * scope.obj.vat) / 100;
                            scope.obj.Netamount = scope.obj.Subtotal + scope.obj.Vat;

                        });

                        //var pharmacy = T74046Service.GetGeneric(scope.obj.T_REQUEST_ID);
                        //pharmacy.then(function(data) {
                        //    scope.GenericList = data;
                        //});


                    }


                });
            //============================
            var con = T74046Service.GetConLevel();
            con.then(function (data) {
                scope.ConseceList = JSON.parse(data);
            });
            //-------- Ruhul Amin -----for GPS start-------------
            var error = '';
            var lati = '';
            var longi = '';

            function getLocation() {
                if (navigator.geolocation) {
                    navigator.geolocation.getCurrentPosition(showPosition);
                } else {
                    error = "Geolocation is not supported by this browser.";
                }
            }

            function showPosition(position) {
                var latitude = position.coords.latitude;
                var longitude = position.coords.longitude;
                // $scope.latitude_Last = latitude;
                // if ($scope.latitude_Last !== latitude) {
                var gps = T74046Service.GPS_Insert(latitude, longitude);
                gps.then(function (data) {
                    latitude = '';
                    longitude = '';
                });
                // }

            }

            scope.$watch('myVar',
                function () {
                    //  getLocation();
                    // alert(lati);
                });

            setInterval(function () {
                //   getLocation();
            },
                60000); //60000 milisecond == 1 minute

            //-------- Ruhul Amin -----for GPS end-------------
            var canclRes = T74046Service.getCancelReasonDataForType3();
            canclRes.then(function (data) {
                scope.CancelReasonForType3 = JSON.parse(data);
            });


            scope.check = '';
            scope.checkByOperator = '';
            //scope.isSuggestedHospital = false;
            setInterval(function () {
                if (scope.check === '' || scope.check === undefined) {
                    if (scope.obj.T74046.T_REQUEST_ID != null) {


                        var gps = T74046Service.getSuggestedHospital(scope.obj.T74046.T_REQUEST_ID);
                        gps.then(function (dt) {
                            var data = JSON.parse(dt);
                            scope.HospitalList = data;
                            if (data.length > 0) {
                                scope.SuggestedHos = data[0].T_LANG2_NAME;
                                scope.T_NAME = data[0].T_NAME;
                                scope.T_ROLE_CODE = data[0].T_ROLE_CODE;
                                scope.T_LANG2_NAME = data[0].T_LANG2_NAME;
                                scope.SUGGEST_NAME = data[0].SUGGEST_NAME;
                                scope.SuggestedHosCode = data[0].T_HOS_SITE_CODE;
                                scope.T_HOS_REQ_USER_ID = data[0].T_HOS_REQ_USER_ID;
                                // scope.check = data[0].T_HOS_SITE_CODE;  //data[0].T_DOC_REQ_ACPT_DATE;
                                if (data[0].T_HOS_SITE_CODE !== null) {
                                    scope.obj.isSuggestedHospital = true;
                                } else {
                                    scope.obj.isSuggestedHospital = false;
                                }
                            }


                        });
                    }

                }
                // It is for Suggested Hospital by Operator start
                // if (scope.checkByOperator === '') {
                //var sugOp = T74046Service.getSuggestedHospitalOper(scope.obj.T74046.T_REQUEST_ID);
                //sugOp.then(function(dt) {
                //    var data = JSON.parse(dt);
                //    if (data.length > 0) {
                //        scope.SuggestedHosOperator = data[0].T_LANG2_NAME;
                //        scope.SuggestedHosCodeByOperator = data[0].T_OPER_DES_CODE;
                //        scope.checkByOperator = data[0].T_OPER_DES_CODE;
                //        if (data[0].T_OPER_ACPT_DATE === null) {
                //            scope.obj.isSuggestedHospitalByOperator = true;
                //        } else {
                //            scope.obj.isSuggestedHospitalByOperator = false;
                //        }
                //    }


                //});
                //  }
                // It is for Suggested Hospital by Operator end
                if (scope.obj.T74046.T_REQUEST_ID != null) {
                    var sugOp = T74046Service.getStationArrivalTime(scope.obj.T74046.T_REQUEST_ID);
                    sugOp.then(function (dt) {
                        var data = JSON.parse(dt);
                        var minute = parseFloat(data[0].TOTAL_MINUTE);
                        if (minute > 15) {
                            scope.StationArrival = 'You have crossed 15 minute after arriving to destination' + ' Time :' + minute;
                            // scope.SuggestedHosCodeByOperator = data[0].T_OPER_DES_CODE;
                            // scope.checkByOperator = data[0].T_OPER_DES_CODE;
                            if (minute !== null) {
                                scope.obj.isSuggestedHospitalByOperator = true;
                            } else {
                                scope.obj.isSuggestedHospitalByOperator = false;
                            }
                        }


                    });
                }


            },
                5000); //60000 milisecond == 1 minute

            //For Delete Start
            scope.Delete_Click = function () {


                if (scope.obj.T_SEX_CODE != "") {
                    var delet = $window.confirm("Are you sure to delete it?");
                    if (delet) {
                        var dele = T74046Service.deleteData(scope.obj.T_SEX_CODE);
                        dele.then(function (data) {
                            //alert("Data Deleted Succesfully");
                            alert(scope.getSingleMsg('S0007'));
                            $window.location = "";
                        });
                    }

                } else {
                    //alert("Select a data for delete.");
                    alert(scope.getSingleMsg('S0011'));

                }
            }
            //For Delete End


            //For Gender Grid Function Start 
            scope.clickHandlerEmployee = clickHandlerEmployee;

            scope.dtColumns = [
                //here We will add .withOption('name','column_name') for send column name to the server 
                DTColumnBuilder.newColumn("T_PAT_ID", "Patient ID").withOption('name', 'T_PAT_ID').notVisible(),
                DTColumnBuilder.newColumn("T_PAT_NO", "Patient No").withOption('name', 'T_PAT_NO').notVisible(),
                DTColumnBuilder.newColumn("T_FIRST_LANG2_NAME", "FNme Eng").withOption('name', 'T_FIRST_LANG2_NAME'),
                DTColumnBuilder.newColumn("T_FIRST_LANG1_NAME", "FNme Local").withOption('name', 'T_FIRST_LANG1_NAME')
                    .notVisible(),
                DTColumnBuilder.newColumn("T_FATHER_LANG2_NAME", "FaN Eng").withOption('name', 'T_FATHER_LANG2_NAME'),
                DTColumnBuilder.newColumn("T_FATHER_LANG1_NAME", "FaNme Local")
                    .withOption('name', 'T_FATHER_LANG1_NAME').notVisible(),
                DTColumnBuilder.newColumn("T_MOTHER_LANG2_NAME", "MN Eng").withOption('name', 'T_MOTHER_LANG2_NAME'),
                DTColumnBuilder.newColumn("T_MOTHER_LANG1_NAME", "MoNme Local")
                    .withOption('name', 'T_MOTHER_LANG1_NAME').notVisible(),
                DTColumnBuilder.newColumn("T_GFATHER_LANG2_NAME", "GFNme English")
                    .withOption('name', 'T_GFATHER_LANG2_NAME').notVisible(),
                DTColumnBuilder.newColumn("T_GFATHER_LANG1_NAME", "GFNme Local")
                    .withOption('name', 'T_GFATHER_LANG1_NAME').notVisible(),
                DTColumnBuilder.newColumn("T_BIRTH_DATE", "Birth date").withOption('name', 'T_BIRTH_DATE').notVisible(),
                DTColumnBuilder.newColumn("T_RLGN_CODE", "Religion").withOption('name', 'T_RLGN_CODE').notVisible(),
                DTColumnBuilder.newColumn("T_MRTL_STATUS", "MRIL Status").withOption('name', 'T_MRTL_STATUS')
                    .notVisible(),
                // DTColumnBuilder.newColumn("T_MRTL_STATUS_CODE", "MRIL Code").withOption('name', 'T_MRTL_STATUS_CODE').notVisible(),
                DTColumnBuilder.newColumn("T_SEX_CODE", "Gender").withOption('name', 'T_SEX_CODE').notVisible(),
                DTColumnBuilder.newColumn("T_NTNLTY_ID", "Nationality ID").withOption('name', 'T_NTNLTY_ID')
                    .notVisible(),
                DTColumnBuilder.newColumn("T_ADDRESS1", "Address").withOption('name', 'T_ADDRESS1'),
                DTColumnBuilder.newColumn("T_POSTAL_CODE", "Postal Code").withOption('name', 'T_POSTAL_CODE')
                    .notVisible(),
                DTColumnBuilder.newColumn("T_NATIONAL_ID", "National ID").withOption('name', 'T_NATIONAL_ID')
                    .notVisible(),
                DTColumnBuilder.newColumn("T_PASSPORT_NO", "Passport No").withOption('name', 'T_PASSPORT_NO')
                    .notVisible(),
                DTColumnBuilder.newColumn("T_MOBILE_NO", "Mobile").withOption('name', 'T_MOBILE_NO'),
                DTColumnBuilder.newColumn("T_OFFICE_NAME", "Office Name").withOption('name', 'T_OFFICE_NAME'),
                DTColumnBuilder.newColumn("T_PHONE_WORK", "Office Phone").withOption('name', 'T_PHONE_WORK')
                    .notVisible(),
                DTColumnBuilder.newColumn("T_ADDRESS2", "Office Address").withOption('name', 'T_ADDRESS2').notVisible(),
                DTColumnBuilder.newColumn("T_EMP_DESI_ID", "Emp Desi ID").withOption('name', 'T_EMP_DESI_ID')
                    .notVisible(),
                DTColumnBuilder.newColumn("Re_T_LANG2_NAME", "Religion").withOption('name', 'Re_T_LANG2_NAME'),
                DTColumnBuilder.newColumn("GE_T_LANG2_NAME", "Gender").withOption('name', 'GE_T_LANG2_NAME'),
                //DTColumnBuilder.newColumn("De_T_LANG2_NAME", "Designatio").withOption('name', 'De_T_LANG2_NAME'),
                DTColumnBuilder.newColumn("Ma_T_LANG2_NAME", "MR Status").withOption('name', 'Ma_T_LANG2_NAME'),
                DTColumnBuilder.newColumn("Na_T_LANG2_NAME", "Nationality").withOption('name', 'Na_T_LANG2_NAME')
            ];
            scope.dtOptions = DTOptionsBuilder.newOptions().withOption('ajax',
                {
                    dataSrc: "data",
                    url: "/T74046/GetEmployeeData",
                    type: "POST"
                })
                .withOption('rowCallback', rowCallback)
                .withOption('processing', true) //for show progress bar
                .withOption('serverSide', true) // for server side processing
                .withPaginationType(
                    'full_numbers') // for get full pagination options // first / last / prev / next and page numbers
                .withDisplayLength(10) // Page size
                .withOption('aaSorting', [0, 'asc']); // for default sorting column // here 0 means first column

            function clickHandlerEmployee(info) {

                scope.obj.T74046.T_PAT_ID = info.T_PAT_ID;
                scope.obj.T74046.T_PAT_NO = info.T_PAT_NO;
                scope.obj.T74046.T_FIRST_LANG2_NAME = info.T_FIRST_LANG2_NAME;
                scope.obj.T74046.T_FIRST_LANG1_NAME = info.T_FIRST_LANG1_NAME;
                scope.obj.T74046.T_FATHER_LANG2_NAME = info.T_FATHER_LANG2_NAME;
                scope.obj.T74046.T_FATHER_LANG1_NAME = info.T_FATHER_LANG1_NAME;
                scope.obj.T74046.T_MOTHER_LANG2_NAME = info.T_MOTHER_LANG2_NAME;
                scope.obj.T74046.T_MOTHER_LANG1_NAME = info.T_MOTHER_LANG1_NAME;
                scope.obj.T74046.T_GFATHER_LANG2_NAME = info.T_GFATHER_LANG2_NAME;
                scope.obj.T74046.T_GFATHER_LANG1_NAME = info.T_GFATHER_LANG1_NAME;

                var myDate = new Date(info.T_BIRTH_DATE.match(/\d+/)[0] * 1);
                scope.obj.T74046.T_BIRTH_DATE = $filter('date')(myDate, "yyyy-MM-dd");
                // scope.obj.T_BIRTH_DATE= info.T_BIRTH_DATE;
                scope.obj.T74046.T_RLGN_CODE = info.T_RLGN_CODE;
                scope.obj.T74046.T_MRTL_STATUS = info.T_MRTL_STATUS;
                scope.obj.T74046.T_SEX_CODE = info.T_SEX_CODE;

                scope.obj.T74046.T_PASSPORT_NO = info.T_PASSPORT_NO;
                scope.obj.T74046.T_NTNLTY_ID = info.T_NTNLTY_ID;
                scope.obj.T74046.T_ADDRESS1 = info.T_ADDRESS1;
                scope.obj.T74046.T_POSTAL_CODE = info.T_POSTAL_CODE;
                scope.obj.T74046.T_MOBILE_NO = info.T_MOBILE_NO;
                scope.obj.T74046.T_NATIONAL_ID = info.T_NATIONAL_ID;

                scope.obj.T74046.T_OFFICE_NAME = info.T_OFFICE_NAME;
                scope.obj.T74046.T_PHONE_WORK = info.T_PHONE_WORK;
                scope.obj.T74046.T_ADDRESS2 = info.T_ADDRESS2;
                scope.obj.T74046.T_EMP_DESI_ID = info.T_EMP_DESI_ID;

                scope.obj.R.selected = { T_LANG2_NAME: info.Re_T_LANG2_NAME, T_RLGN_CODE: info.T_RLGN_CODE };
                scope.obj.G.selected = { T_LANG2_NAME: info.GE_T_LANG2_NAME, T_SEX_CODE: info.T_SEX_CODE };
                scope.obj.D.selected = { T_LANG2_NAME: info.De_T_LANG2_NAME, T_EMP_DESI_ID: info.T_EMP_DESI_ID }
                scope.obj.M.selected = { T_LANG2_NAME: info.Ma_T_LANG2_NAME, T_MRTL_STATUS_CODE: info.T_MRTL_STATUS };
                scope.obj.N.selected = { T_LANG2_NAME: info.Na_T_LANG2_NAME, T_NTNLTY_ID: info.T_NTNLTY_ID };


            }
            //For Gender GT_GFATHER_LANG2_NAME"rid Function End

            //====================================================
            //For Marital Grid Function Start 

            var PatientID = 0;
            scope.$watch('obj.T74046.T_PAT_ID',
                ChangeID_Click = function () {

                    if (scope.obj.T74046.T_PAT_ID != null) {

                        PatientID = scope.obj.T74046.T_PAT_ID;

                        // scope.dtInstance.reloadData();
                    }


                });

            scope.clickHandlerDiagnosis = clickHandlerDiagnosis;
            scope.dtColumnDiagnosis = [
                //here We will add .withOption('name','column_name') for send column name to the server 
                DTColumnBuilder.newColumn("T_PAT_ID", "Patient ID").withOption('name', 'T_PAT_ID').notVisible(),
                DTColumnBuilder.newColumn("T_PCHECKUP_ID", "Patient ID").withOption('name', 'T_PCHECKUP_ID')
                    .notVisible(),
                DTColumnBuilder.newColumn("T_PAT_NO", "Patient No").withOption('name', 'T_PAT_NO'),
                DTColumnBuilder.newColumn("T_FIRST_LANG2_NAME", "FNme Eng").withOption('name', 'T_FIRST_LANG2_NAME'),
                DTColumnBuilder.newColumn("T_FIRST_LANG1_NAME", "FNme Local").withOption('name', 'T_FIRST_LANG1_NAME'),
                DTColumnBuilder.newColumn("T_REQUEST_ID", "Request").withOption('name', 'T_REQUEST_ID'),
                DTColumnBuilder.newColumn("T_TEMP", "Temp").withOption('name', 'T_TEMP'),
                DTColumnBuilder.newColumn("T_HIGHT", "Hight").withOption('name', 'T_HIGHT'),
                DTColumnBuilder.newColumn("T_WEIGHT", "Weight").withOption('name', 'T_WEIGHT'),
                DTColumnBuilder.newColumn("T_BP_SYS", "BL Sys").withOption('name', 'T_BP_SYS'),
                DTColumnBuilder.newColumn("T_BP_DIA", "BL Dia").withOption('name', 'T_BP_DIA'),
                DTColumnBuilder.newColumn("T_PULS", "Puls").withOption('name', 'T_PULS'),
                DTColumnBuilder.newColumn("T_BSUGAR_F", "BL Sugar").withOption('name', 'T_BSUGAR_F'),
                DTColumnBuilder.newColumn("T_ECG_TEST", "ECG ").withOption('name', 'T_ECG_TEST'),
                DTColumnBuilder.newColumn("T_ENTRY_DATE", "Date").withOption('name', 'T_ENTRY_DATE').renderWith(
                    function (data, type) {
                        var myDate = new Date(data.match(/\d+/)[0] * 1);
                        return $filter('date')(myDate, "dd/MMM/yyyy");
                    }),
                DTColumnBuilder.newColumn("T_CH_COMP", "Chief").withOption('name', 'T_CH_COMP').notVisible(),
                DTColumnBuilder.newColumn("T_PROB_TYPE_ID", "ProbID").withOption('name', 'T_PROB_TYPE_ID').notVisible(),
                DTColumnBuilder.newColumn("T_PROB_DETAILS", "PDeta").withOption('name', 'T_PROB_DETAILS').notVisible(),
                DTColumnBuilder.newColumn("Chie_T_LANG2_NAME", "ChfCom").withOption('name', 'Chie_T_LANG2_NAME')
                    .notVisible(),
                DTColumnBuilder.newColumn("Prob_T_LANG2_NAME", "ProbType").withOption('name', 'Prob_T_LANG2_NAME')
                    .notVisible()
            ];
            //var PatientID = 0;
            scope.dtOptionDiagnosis = DTOptionsBuilder.newOptions().withOption('ajax',
                {
                    dataSrc: "data",
                    url: "/T74046/GetDiagnosisData", // Controller/ActionResult
                    type: "POST",
                    data: function (p) {
                        p.PatId = PatientID;
                    }
                })
                .withOption('rowCallback', rowCallback)
                .withOption('processing', true) //for show progress bar
                .withOption('serverSide', true) // for server side processing
                .withPaginationType(
                    'full_numbers') // for get full pagination options // first / last / prev / next and page numbers
                .withDisplayLength(10) // Page size
                .withOption('aaSorting', [0, 'asc']); // for default sorting column // here 0 means first column

            function clickHandlerDiagnosis(v) {

                var myDate = new Date(v.T_ENTRY_DATE.match(/\d+/)[0] * 1);
                var T_ENTRY_DATE = $filter('date')(myDate, "dd-MM-yyyy");

                var date = $filter('date')(new Date(), 'dd-MM-yyyy');;
                if (date == T_ENTRY_DATE) {
                    scope.obj.T74043.T_PAT_ID = v.T_PAT_ID;
                    scope.obj.T74043.T_REQUEST_ID = v.T_REQUEST_ID;
                    scope.obj.T74043.T_PCHECKUP_ID = v.T_PCHECKUP_ID;
                    scope.obj.T74043.T_TEMP = v.T_TEMP;
                    scope.obj.T74043.T_HIGHT = v.T_HIGHT;
                    scope.obj.T74043.T_WEIGHT = v.T_WEIGHT;
                    scope.obj.T74043.T_BP_SYS = v.T_BP_SYS;
                    scope.obj.T74043.T_BP_DIA = v.T_BP_DIA;
                    scope.obj.T74043.T_PULS = v.T_PULS;
                    scope.obj.T74043.T_BSUGAR_F = v.T_BSUGAR_F;
                    scope.obj.T74043.T_ECG_TEST = v.T_ECG_TEST;
                    scope.obj.T74043.T_URINE_TEST = v.T_URINE_TEST;
                    scope.obj.T74043.T_PROB_DETAILS = v.T_PROB_DETAILS;
                    scope.obj.Ch.selected = { T_LANG2_NAME: v.Chie_T_LANG2_NAME, T_CH_COMP: v.T_CH_COMP };
                    scope.obj.Pr.selected = { T_LANG2_NAME: v.Prob_T_LANG2_NAME, T_PROB_TYPE_ID: v.T_PROB_TYPE_ID };
                }
            }

            //For Marital Grid Function End


            //For Nationality Grid Function Start

            scope.clickHandlerNationality = clickHandlerNationality;
            scope.dtColumnsNationality = [
                //here We will add .withOption('name','column_name') for send column name to the server 
                DTColumnBuilder.newColumn("T_NTNLTY_CODE", "Nationality Code").withOption('name', 'T_NTNLTY_CODE'),
                DTColumnBuilder.newColumn("T_LANG1_NAMEen", "English Name").withOption('name', 'T_LANG1_NAMEen'),
                DTColumnBuilder.newColumn("T_LANG1_NAMlo", "Local Name").withOption('name', 'T_LANG1_NAMlo'),
                DTColumnBuilder.newColumn("T_PRIM_LANG", "Primary Language").withOption('name', 'T_PRIM_LANG'),
                DTColumnBuilder.newColumn("T_SECOND_LANG", "Local Language").withOption('name', 'T_SECOND_LANG')
            ];
            scope.dtOptionsNationality = DTOptionsBuilder.newOptions().withOption('ajax',
                {
                    dataSrc: "data",
                    url: "/T74112/GetNationalityItem", // Controller/ActionResult
                    type: "POST"
                })
                .withOption('rowCallback', rowCallback)
                .withOption('processing', true) //for show progress bar
                .withOption('serverSide', true) // for server side processing
                .withPaginationType(
                    'full_numbers') // for get full pagination options // first / last / prev / next and page numbers
                .withDisplayLength(10) // Page size
                .withOption('aaSorting', [0, 'asc']); // for default sorting column // here 0 means first column

            function clickHandlerNationality(d) {
                scope.T74058.T_NTNLTY_CODE = d.T_NTNLTY_CODE;
                scope.T74058.T_LANG1_NAMEen = d.T_LANG1_NAMEen;
                scope.T74058.T_LANG1_NAMlo = d.T_LANG1_NAMlo;
                scope.T74058.T_PRIM_LANG = d.T_PRIM_LANG; //primary
                scope.T74058.T_SECOND_LANG = d.T_SECOND_LANG; //second
                //var prevUrl = history.length > 1 ? history.splice(-2)[0] : "/";
                //$location.path(prevUrl);
                //window.location.href = "/Menus/T04201?Pat_ID =" + info.CustomerID;
            }

            //For Nationality Grid Function End
            //======================================================================
            //For Religion Grid Function Start
            scope.clickHandlerReligion = clickHandlerReligion;
            scope.dtColumnsReligion = [
                //here We will add .withOption('name','column_name') for send column name to the server 
                DTColumnBuilder.newColumn("T_RLGN_CODE", "Religion Code").withOption('name', 'T_RLGN_CODE'),
                DTColumnBuilder.newColumn("T_Eng", "English Name").withOption('name', 'T_Eng'),
                DTColumnBuilder.newColumn("T_Loc", "Local Name").withOption('name', 'T_Loc')
            ];
            scope.dtOptionsReligion = DTOptionsBuilder.newOptions().withOption('ajax',
                {
                    dataSrc: "data",
                    url: "/T74112/GetReligionItem", // Controller/ActionResult
                    type: "POST"
                })
                .withOption('rowCallback', rowCallback)
                .withOption('processing', true) //for show progress bar
                .withOption('serverSide', true) // for server side processing
                .withPaginationType(
                    'full_numbers') // for get full pagination options // first / last / prev / next and page numbers
                .withDisplayLength(10) // Page size
                .withOption('aaSorting', [0, 'asc']); // for default sorting column // here 0 means first column

            //scope.conLevel = function() {
            //    //var id = 'V';
            //        var url = '';
            //        var data = '';
            //    T74046ConLeveFactory.getModal(url, data);
            //}
            function clickHandlerReligion(a) {
                scope.T74059.T_RLGN_CODE = a.T_RLGN_CODE;
                scope.T74059.T_Eng = a.T_Eng;
                scope.T74059.T_Loc = a.T_Loc;

                //var prevUrl = history.length > 1 ? history.splice(-2)[0] : "/";
                //$location.path(prevUrl);
                //window.location.href = "/Menus/T04201?Pat_ID =" + info.CustomerID;
            }
            //For Religion Grid Function End

            function rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                // Unbind first in order to avoid any duplicate handler (see https://github.com/l-lin/angular-datatables/issues/87)
                $('td', nRow).unbind('click');
                $('td', nRow).bind('click',
                    function () {
                        scope.$apply(function () {

                            if (scope.obj.Hidenfield == 'EMP') {
                                scope.clickHandlerEmployee(aData);
                            } else if (scope.obj.Hidenfield == 'DIA') {
                                scope.clickHandlerDiagnosis(aData);
                            } else {

                            }
                            //{

                            //}


                            // scope.someClickHandler(aData);
                            // scope.clickHandlerNationality(aData);
                            // scope.clickHandlerReligion(aData);
                        });
                    });
                return nRow;
            }


            scope.GlassgowShow_Click = function (poup) {
                document.getElementById(poup).style.display = "block";
            };
            scope.ECG_Select = function (poup, b64) {
                scope.ecgData = b64;
                document.getElementById(poup).style.display = "block";

            };

            scope.goTo = function () {
                var a = $location.host();
                var k = '';
                if (scope.vrDir != "") {
                    k += scope.vrDir;
                } else {
                    //http://localhost:5871/Transaction/T74131
                    k += ':5871';
                }
                var s = a + k + '/Queries/Q74145';
                $window.location = 'http://' + s;
            }
            scope.Current_Patient = function () {
                var a = $location.host();
                var k = '';
                if (scope.vrDir != "") {
                    k += scope.vrDir;
                } else {
                    //http://localhost:5871/Transaction/T74131
                    k += ':5871';
                }
                var s = a + k + '/Transaction/T74046';
                $window.location = 'http://' + s;
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
});
app.directive('productionQty', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attr, ngModelCtrl) {
            function fromUser(text) {
                var i = 0;
                var transformedInput = text.replace(/[^0-9]/g, function (match) {
                    return match === "." ? (i++ === 0 ? '.' : '') : '';
                });
                //console.log(transformedInput);
                if (transformedInput !== text) {
                    ngModelCtrl.$setViewValue(transformedInput);
                    ngModelCtrl.$render();
                }
                return transformedInput;
            }
            ngModelCtrl.$parsers.push(fromUser);

        }
    };
});
app.directive('delightMeter', function () {

    /*
    @params Default angular parameters $scope, $element, $attrs.
    Link function for angular directive.
    @returns The delight meter to be rendered when the directive is used.
    */
    function link($scope, $element, $attrs) {

        /* Class for DelightMeter */

        function DelightMeterArc() {

            /*
            @param X Co-ordiante of center, Y Co-ordiante of center, radius of the arc, angle in degrees.
            Converts the polar cordinates to cartesian cordinates
            @returns Cartesian value of Polar co-ordinates.
            */

            this.polarToCartesian = function polarToCartesian(centerX, centerY, radius, angleInDegrees) {
                this.angleInRadians = (angleInDegrees - 90) * Math.PI / 180.0;
                return {
                    x: centerX + (radius * Math.cos(this.angleInRadians)),
                    y: centerY + (radius * Math.sin(this.angleInRadians))
                };
            }

            /*
            @param X co-ordinate of center, Y co-ordinate of center, radius of the arc, starting angle of the arc, ending angle of the arc.
            To supply attributes to SVG path element to describe the arc.
            @returns An array containing attributes for SVG path to describe the arc.
            */
            this.describeArc = function describeArc(x, y, radius, startAngle, endAngle) {
                this.start = this.polarToCartesian(x, y, radius, endAngle);
                this.end = this.polarToCartesian(x, y, radius, startAngle);
                this.arcSweep = endAngle - startAngle <= 180 ? "0" : "1";
                this.d = [
                    "M", this.start.x, this.start.y,
                    "A", radius, radius, 0, this.arcSweep, 0, this.end.x, this.end.y
                ].join(" ");
                return this.d;
            }

            /*
            @params Delight Score read from the scope variable (from range bar in this case).
            Rotates the needle to the specified score.
            @returns nothing.
            */
            this.scoreRotateNeedle = function scoreRotateNeedle(delightScore) {
                if (isNaN(delightScore)) {
                    alert("Please enter an integer value");
                }
                if (delightScore < 0 || delightScore > 100) {
                    alert("We would like you to rate us in 0-100 please");
                    return false;
                }
                /* To convert the delight score into corresponding degree for needle to rotate */
                delightScore = delightScore * 1.8;
                /*
                To rotate the needle to the desired score.
                */
                $('.needleset').css({
                    "transform": "rotate(" + delightScore + "deg)",
                    "transform-origin": "40px 40px"
                });
                /*
                To keep the number in the needle tip without rotating (by rotating it in reverse direction)
                */
                $('.scoreInCircle').css({
                    "transform": "rotate(" + delightScore * -1 + "deg)",
                    "transform-origin": "25px 25px"
                });
            }
        }

        /* Creating object for DelightMeterArc class */
        var objDelightMeterArc = new DelightMeterArc();

        /* Drawing the 5 arcs of the meter by supplying(start co-ordinateX, start co-ordinateY, radius, start angle, end angle)
        to describeArc function. 
        1 degree in between every arc left for white space. */
        document.getElementById("arc1").setAttribute("d", objDelightMeterArc.describeArc(200, 200, 100, -90, -54));
        document.getElementById("arc2").setAttribute("d", objDelightMeterArc.describeArc(200, 200, 100, -53, -18));
        document.getElementById("arc3").setAttribute("d", objDelightMeterArc.describeArc(200, 200, 100, -17, 18));
        document.getElementById("arc4").setAttribute("d", objDelightMeterArc.describeArc(200, 200, 100, 19, 54));
        document.getElementById("arc5").setAttribute("d", objDelightMeterArc.describeArc(200, 200, 100, 55, 90));

        /*
        Function to be implemented whenever value of score changes.Calls scoreRotateNeedle with the changed value of score
        */
        scope.$watch('score', function () {
            objDelightMeterArc.scoreRotateNeedle(scope.score);
        });
    }
    return {
        restrict: 'E',
        templateUrl: '/PartPages/svgmeter.html',
        scope: {
            score: '=ngModel'
        },
        link: link
    };
});




