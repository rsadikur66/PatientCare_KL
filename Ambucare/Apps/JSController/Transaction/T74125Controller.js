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
            out = items;
        }

        return out;
    };
});
app.controller("T74125Controller", ["$scope", "$rootScope", "$location", "$compile", "$filter", "$http", "$window", "T74125Service", "LabelService", "Data",
    function (scope, rootScope, $location, $compile, $filter, $http, $window, T74125Service,LabelService, Data) {
        //for instance
        scope.obj = {};
        scope.obj = Data;

        var patId = 0;
        //for getting Id from query sting start
        var url = $location.absUrl();
        var id = url.split("/").pop();
        var pId = parseInt(id);
        if (pId === null) {
            patId = pId;
        } else {
            patId = JSON.parse(sessionStorage.getItem("pat_125"));  
        }
        //for getting Id from query sting end
       
        var patientDetails = T74125Service.getPatientDetails(patId);
        patientDetails.then(function (data) {
            scope.obj.RequeID = data[0].RequeID;
            scope.obj.Name = data[0].Name;
            scope.obj.FatherName = data[0].FatherName;
            scope.obj.MotherName = data[0].MotherName;
            scope.obj.Mobile = data[0].Mobile;
            scope.obj.Problem = data[0].Problem;
            scope.obj.T_DISCH_RSN_REMARKS = data[0].T_DISCH_RSN_REMARKS;
            //scope.obj.PatentId = data[0].PatentId;
            scope.obj.Ambulance = data[0].Ambulance;
            scope.obj.disStts = data[0].T_DISCH_STATUS;
            scope.obj.T_EVENT_FLAG = data[0].T_EVENT_FLAG;

            var toDay = $filter('date')(new Date(), 'yyyy,MM,dd');
            // var bD = data[i].BirthDate;
            var bD = new Date(data[0].BirthDate.match(/\d+/)[0] * 1);
            var birthDate = $filter('date')(bD, 'yyyy,MM,dd');
            scope.obj.Age = ageCalculet(new Date(birthDate), new Date(toDay)); // 'ageCalculet' is a function
            scope.obj.Age = data[0].BirthDate == null ? '' : data[0].BirthDate + ' Yrs';

            scope.obj.T_DISCH_RSN_CODE = data[0].T_DISCH_RSN_CODE;
            scope.obj.D.selected = { T_DISCH_ID: data[0].T_DISCH_RSN_CODE, T_LANG2_NAME:data[0].T_DISCH_NAME}
        });

        var reason = T74125Service.getDischargeReason();
        reason.then(function(data) {
            scope.reasonList = JSON.parse(data); 
        });

        //var bill = T74125Service.getBill(patId);
        //bill.then(function (data) {
        //    scope.obj.Bill = data[0].Bill;
        //    scope.obj.Request = data[0].Request;
        //});

        var labelData = LabelService.getlabeldata('T74125');
        labelData.then(function(data) {
            scope.entity = data;
        });

        scope.BillDetails_Click = function (id, request) {
            scope.testTotal=0;

            var billDetails = T74125Service.getBillDetails(request);
            billDetails.then(function (data) {
                scope.Newstore = [];
                var grandTotalPrice = 0;
                var grandTotalVat = 0;
                var grandTotalDiscount = 0;
                for (var i = 0; i < data.length; i++) {
                    scope.Newobje = {};
                    scope.Newobje.T_LANG2_NAME = data[i].T_LANG2_NAME;
                    scope.Newobje.T_PRICE = data[i].T_PRICE;
                    var price = data[i].T_PRICE;
                    grandTotalPrice = price + grandTotalPrice;

                    scope.Newobje.T_DISCOUNT = data[i].T_DISCOUNT;
                    var discount = (data[i].T_DISCOUNT * data[i].T_PRICE) / 100;
                    var excludDiscount = data[i].T_PRICE - discount;

                    scope.Newobje.T_VAT = data[i].T_VAT;
                    var vat = (data[i].T_VAT * excludDiscount) / 100;
                    grandTotalVat = grandTotalVat + vat;

                    grandTotalDiscount = grandTotalDiscount + discount;
                    scope.Newstore.push(scope.Newobje);
                    scope.showTable = 'Table';
                }
                scope.obj.BillDetails = scope.Newstore;
                scope.obj.SubTotalPrice = grandTotalPrice.toFixed(3);
                scope.obj.SubTotalVat = grandTotalVat.toFixed(3);
                scope.obj.SubTotalDiscount = grandTotalDiscount.toFixed(3);
                var excludDiscountAndIncludVat = (grandTotalPrice - grandTotalDiscount) + grandTotalVat;
                scope.obj.TotalPrice = excludDiscountAndIncludVat.toFixed(3);
                scope.testTotal = parseFloat(excludDiscountAndIncludVat);

                //------------------------------------------------

                var issueSumary = T74125Service.getIssueSumary(request);
                issueSumary.then(function (data) {
                    var d = '';
                    scope.pharmacyTotal = 0;
                    scope.Discount = 0;
                    scope.totalDiscount = 0;
                    scope.vat = 0;
                    scope.totalVat = 0;
                    scope.GrandTotal = 0;
                    scope.FinalTotal = 0;
                    if (data.length > 0) {
                        for (var j = 0; j < data.length; j++) {
                           // scope.Newobj = {};
                            //scope.pharmacyTotal += data[j].T_TOTAL;
                            scope.pharmacyTotal += data[j].T_TOTAL;
                            scope.Discount = data[j].T_DISCOUNT;
                            var discont = (data[j].T_DISCOUNT * data[j].T_TOTAL) / 100;
                            scope.totalDiscount += discont;
                            scope.vat = data[j].T_VAT;
                            var totalvalue = data[j].T_TOTAL - discont;
                            var vat = (data[j].T_VAT * totalvalue) / 100;
                            scope.totalVat += vat;
                            scope.GrandTotal += data[j].T_GRAND_TOTAL;
                            var total = parseFloat(data[j].T_GRAND_TOTAL);
                           // var final = total;
                            scope.FinalTotal += total;
                            d = '1';
                        }
                        if (d === '1') {
                            scope.obj.pharmacyTotal = scope.pharmacyTotal.toFixed(3);
                            scope.obj.Discount = scope.Discount;
                            scope.obj.totalDiscount = scope.totalDiscount;
                            scope.obj.vat = scope.vat;
                            scope.obj.totalVat = scope.totalVat.toFixed(3);
                            scope.obj.GrandTotal = scope.GrandTotal;
                            var final = scope.FinalTotal + scope.testTotal;
                            scope.obj.FinalTotal = final.toFixed(3);
                        }
                       
                    }
                    //scope.obj.pharmacyTotal = data[0].T_TOTAL;
                    //scope.obj.Discount = data[0].T_DISCOUNT;
                    //var discont = (data[0].T_DISCOUNT * data[0].T_TOTAL) / 100;
                    //scope.obj.totalDiscount = discont.toFixed(3);
                    //scope.obj.vat = data[0].T_VAT;
                    //var totalvalue = data[0].T_TOTAL - discont;
                    //var vat = (data[0].T_VAT * totalvalue) / 100;
                    //scope.obj.totalVat = vat.toFixed(3);
                    //scope.obj.GrandTotal = data[0].T_GRAND_TOTAL;
                    //var total = parseFloat(data[0].T_GRAND_TOTAL);
                    //var final = total + testTotal;
                    //scope.obj.FinalTotal = final.toFixed(3);
                });


            });
            var issueDetails = T74125Service.getIssueDetails(request);
            issueDetails.then(function (data) {
                scope.obj.IssueDetails = data;

            });


            document.getElementById(id).style.display = "block";
        }

        scope.CloseReceivePopup = function (id) {
            document.getElementById(id).style.display = "none";

            scope.obj.BillDetails = [];
            scope.obj.IssueDetails = [];
        }

        scope.openHospitalList = function (id) {
            document.getElementById(id).style.display = "block";
            var zoneList = T74125Service.GetZone();
            zoneList.then(function (dt) {
                scope.zoneList = dt.data;

            });

        }

        scope.SiteList = function (id) {

            var siteList = T74125Service.GetSite(id);
            siteList.then(function (dt) {
                scope.siteList = dt.data;
                scope.obj.s = {};
                scope.obj.s.selected = { T_SITE_CODE: '', T_LANG2_NAME: 'Select' };
            });
        }
        scope.closeHospitalList = function (id) { document.getElementById(id).style.display = "none"; }
        scope.hospitalAddmission = function () {
            var updT74046 = T74125Service.UpdateT74046(patId, scope.t74041.T_DISCH_DEST.gm_accessors_.place.Uc.formattedPrediction);
            updT74046.then(function (ut) {
                //alert("data Saved sucessfully");
                alert(scope.getSingleMsg('S0012'));
            });
            scope.Save_Click();

            //var patData = T74125Service.PatData(patId);
            //patData.then(function (dt) {
            //    if (dt.length>0) {
            //        scope.t74046 = {};
            //        scope.t74041 = {};
            //        scope.t74043 = {};
            //        scope.t02003 = {};
            //        var myDate = new Date(dt[0].T_ENTRY_DATE.match(/\d+/)[0] * 1);
            //        scope.t74046.T_ENTRY_DATE = $filter('date')(myDate, "dd-MMM-yyyy");
            //        scope.t74046.T_ENTRY_USER = dt[0].T_ENTRY_USER;
            //        scope.t74046.T_FIRST_LANG2_NAME = dt[0].T_FIRST_LANG2_NAME;
            //        scope.t74046.T_FIRST_LANG1_NAME = dt[0].T_FIRST_LANG1_NAME;
            //        scope.t74046.T_FATHER_LANG2_NAME = dt[0].T_FATHER_LANG2_NAME;
            //        scope.t74046.T_FATHER_LANG1_NAME = dt[0].T_FATHER_LANG1_NAME;
            //        scope.t74046.T_GFATHER_LANG2_NAME = dt[0].T_GFATHER_LANG2_NAME;
            //        scope.t74046.T_GFATHER_LANG1_NAME = dt[0].T_GFATHER_LANG1_NAME;
            //        scope.t74046.T_FAMILY_LANG2_NAME = dt[0].T_FAMILY_LANG2_NAME;
            //        scope.t74046.T_FAMILY_LANG1_NAME = dt[0].T_FAMILY_LANG1_NAME;
            //        scope.t74046.T_MOTHER_LANG2_NAME = dt[0].T_MOTHER_LANG2_NAME;
            //        scope.t74046.T_MOTHER_LANG1_NAME = dt[0].T_MOTHER_LANG1_NAME;
            //        var birthDate = new Date(dt[0].T_BIRTH_DATE.match(/\d+/)[0] * 1);
            //        scope.t74046.T_BIRTH_DATE = $filter('date')(birthDate, "dd-MMM-yyyy");
            //        scope.t74046.T_RLGN_CODE = dt[0].T_RLGN_CODE;
            //        scope.t74046.T_MRTL_STATUS = dt[0].T_MRTL_STATUS;
            //        scope.t74046.T_SEX_CODE = dt[0].T_SEX_CODE;
            //        scope.t02003.T_NTNLTY_CODE = dt[0].T_NTNLTY_CODE;
            //        scope.t74046.T_NTNLTY_ID = dt[0].T_NTNLTY_ID;
            //        scope.t74046.T_PAT_ID = dt[0].T_PAT_ID;
            //        scope.t74046.T_ADDRESS1 = dt[0].T_ADDRESS1;
            //        scope.t74046.T_POSTAL_CODE = dt[0].T_POSTAL_CODE;
            //        scope.t74046.T_PHONE_HOME = dt[0].T_PHONE_HOME;
            //        scope.t74046.T_PASSPORT_NO = dt[0].T_PASSPORT_NO;
            //        scope.t74043.T_REQUEST_ID = dt[0].T_REQUEST_ID;
            //        scope.t74043.T_HIGHT = dt[0].T_HIGHT;
            //        scope.t74043.T_WEIGHT = dt[0].T_WEIGHT;
            //        scope.t74043.T_BP_SYS = dt[0].T_BP_SYS;
            //        scope.t74043.T_BP_DIA = dt[0].T_BP_DIA;
            //        scope.t74043.T_TEMP = dt[0].T_TEMP;
            //        scope.t74043.T_PULS = dt[0].T_PULS;
            //        scope.t74041.T_CH_COMP = dt[0].T_CH_COMP;
            //        scope.t74041.T_PROB_TYPE_ID = dt[0].T_PROB_TYPE_ID;

            //        var patInfo = T74125Service.SavePatInfo(scope.t74046, scope.t74041, scope.t74043, scope.t02003, scope.obj.T_SITE_CODE);
            //        patInfo.then(function (pt) {
            //            var updT74046 = T74125Service.UpdateT74046(patId, scope.t74041.T_DISCH_DEST);
            //            updT74046.then(function (ut) {
            //                alert("data Saved sucessfully");
            //            });
            //            scope.Save_Click();

            //        });
            //    }


            //});
        }

        //------------------------------
        function ageCalculet(toDay, warEndDate) {
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


            return years + yAppendix + ", " + months + mAppendix + ", " + days + dAppendix;
        }
        function DaysInMonth(date2_UTC) {
            var monthStart = new Date(date2_UTC.getFullYear(), date2_UTC.getMonth(), 1);
            var monthEnd = new Date(date2_UTC.getFullYear(), date2_UTC.getMonth() + 1, 1);
            var monthLength = (monthEnd - monthStart) / (1000 * 60 * 60 * 24);
            return monthLength;
        }
        //-------------------------------

        scope.Save_Click = function() {
            scope.T74041 = {};
            scope.T74041.T_REQUEST_ID = scope.obj.RequeID;
            scope.T74041.T_DISCH_STATUS = '1';
            scope.T74041.T_DISCHARGE_DATE = $filter('date')(new Date(), 'dd-MM-yyyy');
            scope.T74041.T_DISCHARGE_TIME = $filter('date')(new Date(), 'HH:mm:ss'); //HH:mm:ss

            var update41 = T74125Service.update_T74041(scope.T74041);
            update41.then(function(data) {
                //alert('Discharge Successfully');
                alert(scope.getSingleMsg('S0028'));
                $window.location = "/Menu";
            });

        }
        scope.Death_Click = function () {
            scope.T74041 = {};
            scope.T74041.T_REQUEST_ID = scope.obj.RequeID;
            scope.T74041.T_DISCH_STATUS = '1';
            scope.T74041.T_DISCH_RSN_REMARKS = 'Death';
            scope.T74041.T_DISCHARGE_DATE = $filter('date')(new Date(), 'dd-MM-yyyy');
            scope.T74041.T_DISCHARGE_TIME = $filter('date')(new Date(), 'HH:mm:ss'); //HH:mm:ss

            var update41 = T74125Service.update_T74041(scope.T74041);
            update41.then(function (data) {
                //alert('Discharge Successfully');
                alert(scope.getSingleMsg('S0028'));
                $window.location = "/Menu";
            });

        }
        scope.Cure_Click = function () {
            scope.getLatLong();
            if (scope.confLatLong() == 'ok') {
                if (scope.obj.disStts == null) {

                    var com = confirm('Are you sure to discharge this patient ?');
                    if (com === true) {
                        if (scope.obj.T_DISCH_RSN_CODE == null) {
                            alert('Please select reason');
                            return;
                        }
                       
                        scope.T74041 = {};
                        scope.T74041.T_REQUEST_ID = scope.obj.RequeID;
                        scope.T74041.T_DISCH_STATUS = '1';
                        scope.T74041.T_DISCH_RSN_CODE = scope.obj.T_DISCH_RSN_CODE;
                        scope.T74041.T_DISCH_RSN_REMARKS = scope.obj.T_DISCH_RSN_REMARKS;
                        scope.T74041.T_EVENT_FLAG = scope.obj.T_EVENT_FLAG;
                        // scope.T74041.T_DISCHARGE_DATE = $filter('date')(new Date(), 'dd-MM-yyyy');
                        // scope.T74041.T_DISCHARGE_TIME = $filter('date')(new Date(), 'HH:mm:ss'); //HH:mm:ss

                        var update41 = T74125Service.update_T74041(scope.T74041);
                        update41.then(function (data) {
                            if (data) {
                                if (data === "None") {
                                    scope.obj.disStts = '1';
                                    var a = scope.getLatLong();
                                    var t74026 = {};
                                    t74026.T_REQUEST_ID = scope.obj.RequeID;
                                    t74026.T_EVENT_ID = 13;
                                    t74026.T_LATITUDE = a.lt;
                                    t74026.T_LONGITUDE = a.ln;
                                    var dd = T74125Service.save26(t74026);
                                    dd.then(function(da) {
                                        alert(scope.getSingleMsg('S0028'));
                                        rootScope.$broadcast('DischargeEvent');
                                       
                                    });
                                   
                                } else {
                                    scope.obj.disStts = '1';
                                    var a = scope.getLatLong();
                                    var t74026A = {};
                                    t74026A.T_REQUEST_ID = scope.obj.RequeID;
                                    t74026A.T_EVENT_ID = 31;
                                    t74026A.T_LATITUDE = a.lt;
                                    t74026A.T_LONGITUDE = a.ln;
                                    var kk = T74125Service.save26(t74026A);
                                    kk.then(function(da) {
                                        var a = scope.getLatLong();
                                        var t74026b = {};
                                        t74026b.T_REQUEST_ID = scope.obj.RequeID;
                                        t74026b.T_EVENT_ID = 32;
                                        t74026b.T_LATITUDE = a.lt;
                                        t74026b.T_LONGITUDE = a.ln;
                                      var jj=  T74125Service.save26(t74026b);
                                        jj.then(function(d) {
                                            $window.location = "/Menu";
                                        });
                                        
                                    });

                                   
                                }

                                //scope.obj.Hidenfield = 'Hos';
                                //scope.currentTab = '/PartPages/T74140.html';
                            }

                            //$window.location = "/Menu";
                        });
                    }
                } else {
                    //alert('This Patient has been Discharged');
                    alert(scope.getSingleMsg('S0029'));
                }
            } else {
                alert(scope.confLatLong());
            }




            
         
        }

        scope.Disc_Report = function() {
            $window.open("../R74125/GetAllReportData?T_REQUEST_ID=" + scope.obj.RequeID, "popup", "width=600,height=600,left=50,top=50");
        }




        //----------------Verify----Discharge Report---------------------
        scope.verify = function () {
            var verify = T74125Service.verifySummeryReport(scope.obj.RequeID);
            verify.then(function (data) {
                var dt = JSON.parse(data);
                alert(dt);
            });

        }
    }]);