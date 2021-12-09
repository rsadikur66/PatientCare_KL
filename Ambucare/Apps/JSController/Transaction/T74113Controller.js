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
app.filter('ageFilter', function () {
    function calculateAge(birthday) {
        var ageDifMs = Date.now() - birthday.getTime();
        var ageDate = new Date(ageDifMs);
        return Math.abs(ageDate.getUTCFullYear() - 1970);
    }

    function monthDiff(d1, d2) {
        if (d1 < d2) {
            var months = d2.getMonth() - d1.getMonth();
            return months <= 0 ? 0 : months;
        }
        return 0;
    }


    return function (birthdate) {
        var age;
        if (monthDiff(birthdate, new Date()) === 0) {
            age = calculateAge(birthdate) + ' Yrs';
        } else {
            age = calculateAge(birthdate) + ' Yrs ' + monthDiff(birthdate, new Date()) + ' Mos';
        }
        return age;
    };
});
app.controller("T74113Controller", ["$scope", "T74113Service", "Data", "$filter", "T74113DiagonosisListFactory", "LabelService", "T74113TradeListFactory", "T74113GenListFactory", "$window", "$rootScope",
    function ($scope, T74113Service, Data, $filter, T74113DiagonosisListFactory, LabelService, T74113TradeListFactory, T74113GenListFactory, $window, $rootScope) {
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.t74039 = {};
        $scope.obj.t74078 = {};










        $scope.obj.t74113 = {};
        $scope.obj.doc = {};
        $scope.obj.D = {};
        $scope.obj.DiagonosisList = [];
        $scope.obj.t74040 = {};

        $scope.obj.MedicineList = [];
        if ($scope.obj.DiagonosisList.length === 0) {
            $scope.obj.DiagonosisList.push({});
            $scope.obj.DiagonosisList.push({});
            $scope.obj.TotalPrice = 0;
        }
        var labelData = LabelService.getlabeldata('T74113');
        labelData.then(function (data) {
            $scope.entity = data;
        });
        var EntryUser = T74113Service.EntryUser();
        EntryUser.then(function (data) {
            $scope.obj.T_ENTRY_USER = data;
            $scope.obj.T_ENTRY_DATE = $filter('date')(new Date(), 'dd-MMM-yyyy');
        });
        $scope.onStrClick = function (i) {
            var gn = $scope.obj.MedicineList[i].T_GEN_CODE;
            var pr = $scope.obj.MedicineList[i].T_PRESCRIPTION_ID;
            var it = $scope.obj.MedicineList[i].T_ITEM_CODE;
            if (pr != null && it == null) {
                var strength = T74113Service.GetStrengthByGen(gn);
                strength.then(function (dt) {
                    $scope.obj.MedicineList[i].StrengthList = JSON.parse(dt);
                });
            }
        }
        $scope.onFrmClick = function (i) {
            var gn = $scope.obj.MedicineList[i].T_GEN_CODE;
            var pr = $scope.obj.MedicineList[i].T_PRESCRIPTION_ID;
            var it = $scope.obj.MedicineList[i].T_ITEM_CODE;
            if (pr != null && it == null) {
                var from = T74113Service.GetFormByGen(gn);
                from.then(function (dt) {
                    $scope.obj.MedicineList[i].FormList = JSON.parse(dt);
                });
            }
        }
        $scope.onRtClick = function (i) {
            var gn = $scope.obj.MedicineList[i].T_GEN_CODE;
            var pr = $scope.obj.MedicineList[i].T_PRESCRIPTION_ID;
            var it = $scope.obj.MedicineList[i].T_ITEM_CODE;
            if (pr != null && it == null) {
                var route = T74113Service.GetRouteByGen(gn);
                route.then(function (dt) {
                    $scope.obj.MedicineList[i].RouteList = JSON.parse(dt);
                });
            }
        }
        $scope.onDurationClick = function (frequencyQty, i) {
            var pr = $scope.obj.MedicineList[i].T_PRESCRIPTION_ID;
            if (pr != null) {
                var duration = T74113Service.GetDuration(frequencyQty);
                duration.then(function (dt) {
                    $scope.DurationList = dt;

                });
            }

        }
        $scope.PatInfo = function () {
            //alert(sessionStorage.getItem("RequestId"));
            //$scope.obj.t74039.T_REQUEST_ID = JSON.parse(sessionStorage.getItem("RequestId"));
            var data = T74113Service.GetPatInfo();
            data.then(function (d) {
                if (d.length !== 0) {
                    $scope.obj.t74039.T_REQUEST_ID = d[0].T_REQUEST_ID;
                    $scope.obj.t74113.FullName = d[0].T_FIRST_LANG2_NAME +
                        ' ' +
                        d[0].T_FATHER_LANG2_NAME +
                        ' ' +
                        d[0].T_GFATHER_LANG2_NAME +
                        ' ' +
                        d[0].T_FAMILY_LANG2_NAME;
                    $scope.obj.t74113.Gender = d[0].Gender;
                    var myDate = new Date(d[0].Age.match(/\d+/)[0] * 1);
                    var dt = $filter('date')(myDate, "yyyy,MM,dd");
                    $scope.obj.t74113.Age = $filter('ageFilter')(new Date(dt));
                    $scope.obj.t74113.Nationality = d[0].Nationality;
                    $scope.obj.t74113.MaritalStatus = d[0].MaritalStatus;
                    $scope.obj.t74113.T_HIGHT = d[0].T_HIGHT;
                    $scope.obj.t74113.T_WEIGHT = d[0].T_WEIGHT;
                    $scope.obj.t74113.T_PRESCRIPTION_ID = d[0].T_PRESCRIPTION_ID;
                    $scope.obj.t74039.T_REMARKS = d[0].T_REMARKS;
                    var doc = T74113Service.GetDocInfoByReq(d[0].T_AMBU_REG_ID);
                    doc.then(function (dt) {
                        if (dt.length !== 0) {
                            if (dt.length === 1) {
                                $scope.obj.D.selected = { T_EMP_ID: dt[0].T_EMP_ID, Full_Name: dt[0].Full_Name };
                                $scope.DocList = dt;
                                $scope.GetDept($scope.obj.D.selected.T_EMP_ID);
                            } else {
                                $scope.obj.D.selected = { Full_Name: 'Select' }
                                $scope.DocList = dt;
                                $scope.obj.t74113.Doc_Dept = '';

                            }
                        } else {
                            $scope.DocList = [];
                        }

                    });
                    if ($scope.obj.t74113.T_PRESCRIPTION_ID != null) {
                        var diag = T74113Service.GetDiagonosisByPres($scope.obj.t74113.T_PRESCRIPTION_ID);
                        diag.then(function (dt) {
                            var a = 0;
                            for (var i = 0; i < dt.length; i++) {
                                a += dt[i].T_PRICE;
                            }
                            $scope.obj.TotalPrice = a;
                            $scope.obj.DiagonosisList = dt;
                            if ($scope.obj.DiagonosisList.length > 0) {
                                $scope.obj.T_DIAGONOSIS = $scope.obj.DiagonosisList.length + " Tests" + " , Total Ammount " + $scope.obj.TotalPrice;
                            } else {
                                $scope.obj.T_DIAGONOSIS = 'Add Diagonosis';
                            }
                        });
                        var med = T74113Service.GetMedListByPres($scope.obj.t74113.T_PRESCRIPTION_ID);
                        med.then(function (dtMed) {
                            var m = JSON.parse(dtMed);
                            for (var b = 0; b < m.length; b++) {
                                var l = {};
                                l.T_PRESCRIPTION_ID = $scope.obj.t74113.T_PRESCRIPTION_ID;
                                l.T_ITEM_CODE = m[b].T_ITEM_CODE;
                                l.T_TRADE_CODE = m[b].T_TRADE_CODE;
                                l.T_GEN_CODE = m[b].T_GEN_CODE;
                                l.T_MEDICATION_NAME = m[b].T_ITEM_CODE == null ? m[b].GEN_DESC : m[b].PRODUCT_DESC;
                                l.S = {};
                                l.S.selected = {};
                                l.S.selected.STRENGTH = m[b].T_ITEM_CODE == null ? 'Select' : m[b].T_REQUEST_STRENGTH;
                                l.T_REQUEST_STRENGTH = m[b].T_ITEM_CODE == null ? '' : m[b].T_REQUEST_STRENGTH;
                                l.Fr = {};
                                l.Fr.selected = {};
                                l.Fr.selected = {
                                    FORM_CODE: m[b].T_ITEM_CODE == null ? '' : m[b].T_FORM_ID,
                                    FORM_DESC: m[b].T_ITEM_CODE == null ? 'Select' : m[b].FORM_DESC
                                }
                                l.T_FORM_ID = m[b].T_ITEM_CODE == null ? '' : m[b].T_FORM_ID;
                                l.R = {};
                                l.R.selected = {};
                                l.R.selected = {
                                    ROUTE_CODE: m[b].T_ITEM_CODE == null ? '' : m[b].T_DRUG_ROUTE_CODE,
                                    ROUTE_DESC: m[b].T_ITEM_CODE == null ? 'Select' : m[b].ROUTE_DESC
                                }
                                l.T_DRUG_ROUTE_CODE = m[b].T_ITEM_CODE == null ? '' : m[b].T_DRUG_ROUTE_CODE;
                                l.T_MORNING_DOSE_UNIT = m[b].T_MORNING_DOSE_UNIT;
                                l.T_NOON_DOSE_UNIT = m[b].T_NOON_DOSE_UNIT;
                                l.T_NIGHT_DOSE_UNIT = m[b].T_NIGHT_DOSE_UNIT;
                                l.Ins = {};
                                l.Ins.selected = { T_MEAL_INSTRUCTION: m[b].T_MEAL_INSTRUCTION, T_INSTRUCTION_DESC: m[b].T_MEAL_INSTRUCTION_DESC };
                                l.T_MEAL_INSTRUCTION = m[b].T_MEAL_INSTRUCTION;
                                l.T_INS_TIME = m[b].T_INS_TIME;
                                l.Saturday = m[b].T_SATURDAY == '1';
                                l.Sunday = m[b].T_SUNDAY == '1';
                                l.Monday = m[b].T_MONDAY == '1';
                                l.Tuesday = m[b].T_TUESDAY == '1';
                                l.Wednesday = m[b].T_WEDNESDAY == '1';
                                l.Thursday = m[b].T_THURSDAY == '1';
                                l.Friday = m[b].T_FRIDAY == '1';
                                l.T_SATURDAY = m[b].T_SATURDAY;
                                l.T_SUNDAY = m[b].T_SUNDAY;
                                l.T_MONDAY = m[b].T_MONDAY;
                                l.T_TUESDAY = m[b].T_TUESDAY;
                                l.T_WEDNESDAY = m[b].T_WEDNESDAY;
                                l.T_THURSDAY = m[b].T_THURSDAY;
                                l.T_FRIDAY = m[b].T_FRIDAY;
                                l.T_FREQUENCY_CODE = m[b].T_FREQUENCY_CODE;
                                l.F = {};
                                l.F.selected = {};
                                l.F.selected = { T_FREQUENCY_CODE: m[b].T_FREQUENCY_INS_ID, T_LANG2_NAME: m[b].T_FREQUENCY_DESC, T_QTY_MON: m[b].T_QTY_MON };
                                l.T_FREQUENCY_INS_ID = m[b].T_FREQUENCY_INS_ID;
                                l.T_DOSE_DURATION_CODE = m[b].T_DOSE_DURATION_CODE;
                                l.D = {};
                                l.D.selected = {};
                                l.D.selected = { T_DOSE_DURATION_CODE: m[b].T_DOSE_DURATION_INS_ID, T_LANG2_NAME: m[b].T_DOSE_DURATION_DESC };
                                l.T_DOSE_DURATION_INS_ID = m[b].T_DOSE_DURATION_INS_ID;
                                l.T_DRUG_INACTIVE_FLAG = m[b].T_DRUG_INACTIVE_FLAG;
                                l.DRUG_INACTIVE_FLAG = m[b].T_DRUG_INACTIVE_FLAG == '1';
                                l.listOfAdvice = [];
                                if (m[b].T_REMARKS_DTL != "" && m[b].T_REMARKS_DTL !== null) {

                                    var r = m[b].T_REMARKS_DTL.split('.');
                                    for (var k = 0; k < r.length; k++) {

                                        if (r[k] != "") {
                                            var t = {};
                                            t.singleAdvice = r[k];
                                            l.listOfAdvice.push(t);
                                        }
                                    }


                                }

                                //l.adviceList = r.split('.');
                                //l.showAdviceTable = true;
                                $scope.obj.MedicineList.push(l);
                            }
                        });
                    }

                } else {
                    $scope.obj.t74113.FullName = '';
                    $scope.obj.t74113.Gender = '';
                    $scope.obj.t74113.Age = '';
                    $scope.obj.t74113.Nationality = '';
                    $scope.obj.t74113.MaritalStatus = '';
                    $scope.obj.t74113.T_HIGHT = '';
                    $scope.obj.t74113.T_WEIGHT = '';
                    $scope.obj.t74113.Doc_Dept = '';
                    $scope.DocList = [];
                    $scope.obj.D.selected = { Full_Name: 'Select' };

                }
            });
        }
        $scope.GetDept = function () {
            var data = T74113Service.GetDocDept($scope.obj.D.selected.T_EMP_ID);
            data.then(function (dt) {
                $scope.obj.t74113.Doc_Dept = dt[0].Dept_Name;
            });
        }


        $scope.Add_Diagonosis_Row = function () {
            $scope.obj.DiagonosisList.push({});
            if ($scope.obj.TotalPrice === undefined) {
                $scope.obj.TotalPrice = 0;
            }
        }
        $scope.Remove_Diagonosis = function (index) {
            $scope.obj.TotalPrice -= $scope.obj.DiagonosisList[index].T_PRICE;
            $scope.obj.DiagonosisList.splice(index, 1);
            if ($scope.obj.DiagonosisList.length > 0) {
                $scope.obj.T_DIAGONOSIS = $scope.obj.DiagonosisList.length + " Tests" + " , Total Ammount " + $scope.obj.TotalPrice;
            } else {
                $scope.obj.T_DIAGONOSIS = 'Add Diagonosis';
            }
        };
        $scope.Diagonosis = function (index) {
            var diagonosisList = [];
            for (var i = 0; i < $scope.obj.DiagonosisList.length; i++) {
                if ($scope.obj.DiagonosisList.length !== undefined) {
                    var demo = {};
                    demo.T_COST_TYPE_DTL_ID = $scope.obj.DiagonosisList[i].T_COST_TYPE_DTL_ID;
                    diagonosisList.push(demo);
                }
            }
            var url = '';
            var data = '';
            T74113DiagonosisListFactory.getModal(url, data, index, $scope.obj.t74039.T_REQUEST_ID, diagonosisList);

        };
        $scope.Add_Prescription_Row = function () {
            $scope.obj.MedicineList.push({});
        }
        $scope.Remove_Prescription = function (index) {
            $scope.obj.MedicineList.splice(index, 1);
        };
        var frequency = T74113Service.GetFrequency();
        frequency.then(function (dt) {
            $scope.FrequencyList = dt;
        });

        $scope.onDurationSelect = function (frequencyQty, durationQty, doseUnit, index) {
            var scope1 = angular.element($("#ddlDurationCode" + index)).scope();
            scope1.obj.T_DOSE_DURATION_CODE =
                Math.round(parseFloat((parseFloat(frequencyQty) * parseFloat(durationQty) * parseFloat(doseUnit)) /
                    30));
        }
        $scope.onFrequencySelect = function (frequencyQty, index) {
            var scope1 = angular.element($("#ddlDurationId" + index)).scope();
            scope1.obj.D = {};
            scope1.obj.D.selected = { T_DOSE_DURATION_CODE: '', T_LANG2_NAME: 'Select', T_QTY: '' }
            var scope2 = angular.element($("#ddlDurationCode" + index)).scope();
            scope2.obj.T_DOSE_DURATION_CODE = '';
            var duration = T74113Service.GetDuration(frequencyQty);
            duration.then(function (dt) {
                $scope.DurationList = dt;

            });
        }
        //--------------------------------------------------------------------------
        $scope.onFocusLost = function (index) {
            var scope1 = angular.element($("#txtAdvice" + index)).scope();
            scope1.obj.T_LANG2_NAME = '';
        }

        $scope.onAdviceSearch = function (e, index) {

            var AdviceList = T74113Service.GetAdviceList(e);
            AdviceList.then(function (data) {
                var scope2 = angular.element($("#tbladvice" + index)).scope();
                if (data.length !== 0) {
                    scope2.obj.adviceList = data;
                    scope2.showAdviceTable = true;


                } else {
                    scope2.showAdviceTable = false;
                }


            });
        }

        $scope.onAdviceSelect = function (singleAdvice, parentIndex, index) {
            var chk = 0;
            var scope2 = angular.element($("#tbladvice" + parentIndex)).scope();
            scope2.showAdviceTable = false;
            //var scope4 = angular.element($("#txtRemarksDtl" + parentIndex)).scope();
            var scope3 = angular.element($("#trAdviceList" + parentIndex)).scope();

            if (scope3.obj.listOfAdvice.length === 0) {
                scope3.obj.listOfAdvice.push({ singleAdvice: singleAdvice.T_LANG2_NAME, singleAdviceId: singleAdvice.T_ADVICE_ID });
                // scope4.obj.T_REMARKS_DTL = singleAdvice.T_LANG2_NAME;
            } else {
                for (var i = 0; i < scope3.obj.listOfAdvice.length; i++) {
                    if (scope3.obj.listOfAdvice[i].singleAdviceId === singleAdvice.T_ADVICE_ID) {
                        chk++;
                    }
                }
                if (chk === 0) {
                    scope3.obj.listOfAdvice.push({ singleAdvice: singleAdvice.T_LANG2_NAME, singleAdviceId: singleAdvice.T_ADVICE_ID });
                    //scope4.obj.T_REMARKS_DTL += '<<*@*>>'+ singleAdvice.T_LANG2_NAME;
                }
            }
            var scope1 = angular.element($("#txtAdvice" + parentIndex)).scope();
            scope1.obj.T_LANG2_NAME = '';
        }
        $scope.onMouseOver = function (parentIndex, index) {
            document.getElementById("btnBullet" + parentIndex + index).style.background = "url(../Images/Reject.png)";
            document.getElementById("btnBullet" + parentIndex + index).style.backgroundSize = "15px";
            document.getElementById("btnBullet" + parentIndex + index).style.backgroundRepeat = "no-repeat";
            document.getElementById("btnBullet" + parentIndex + index).style.backgroundPosition = "center";
            document.getElementById("btnBullet" + parentIndex + index).style.width = "15px";
            document.getElementById("btnBullet" + parentIndex + index).style.borderColor = "#FBFBFB";
        }
        $scope.onMouseLeave = function (parentIndex, index) {
            document.getElementById("btnBullet" + parentIndex + index).style.background = "url(../Images/Accept.png)";
            document.getElementById("btnBullet" + parentIndex + index).style.backgroundSize = "15px";
            document.getElementById("btnBullet" + parentIndex + index).style.backgroundRepeat = "no-repeat";
            document.getElementById("btnBullet" + parentIndex + index).style.backgroundPosition = "center";
            document.getElementById("btnBullet" + parentIndex + index).style.width = "15px";
            document.getElementById("btnBullet" + parentIndex + index).style.borderColor = "#FBFBFB";
        }
        $scope.onDeleteAdvice = function (parentIndex, index) {
            var scope3 = angular.element($("#trAdviceList" + parentIndex)).scope();
            scope3.obj.listOfAdvice.splice(index, 1);
        }
        $scope.onAdviceEditStart = function (parentIndex, index) {
            document.getElementById("lblsingleAdvice" + parentIndex + index).style.display = "none";
            document.getElementById("txtsingleAdvice" + parentIndex + index).style.display = "block";
        }
        $scope.onAdviceEditEnd = function (parentIndex, index) {
            document.getElementById("lblsingleAdvice" + parentIndex + index).style.display = "block";
            document.getElementById("txtsingleAdvice" + parentIndex + index).style.display = "none";
        }
        //--------------------------------------------------------------------------
        $scope.OpenIns = function (index) {

            document.getElementById("test" + index).style.display = "block";
            var scope1 = angular.element($("#ddlFrequencyCode" + index)).scope();
            scope1.obj.T_FREQUENCY_CODE = 1;
            var scope3 = angular.element($("#trAdviceList" + index)).scope();
            if (scope3.obj.listOfAdvice === undefined) {
                scope3.obj.listOfAdvice = [];
            }
        }
        $scope.TradeList = function (index) {
            var tradeList = [];
            for (var i = 0; i < $scope.obj.MedicineList.length; i++) {
                if ($scope.obj.MedicineList.length !== undefined) {
                    var demo = {};
                    demo.T_TRADE_CODE = $scope.obj.MedicineList[i].T_TRADE_CODE;
                    tradeList.push(demo);
                }
            }
            var url = '';
            var data = '';
            T74113TradeListFactory.getModal(url, data, index, tradeList);
        };
        $scope.GenList = function (index) {
            var url = '';
            var data = '';
            T74113GenListFactory.getModal(url, data, index);

        };

        $scope.InstructionList = [
            { T_INSTRUCTION_CODE: 1, T_INSTRUCTION_DESC: "Before Meal" },
            { T_INSTRUCTION_CODE: 2, T_INSTRUCTION_DESC: "During  Meal" },
            { T_INSTRUCTION_CODE: 3, T_INSTRUCTION_DESC: "After  Meal" }
        ];
        $scope.CloseIns = function (index) {
            document.getElementById("test" + index).style.display = "none";
        }
        $scope.chkSaturday = function (index) {
            var scope1 = angular.element($("#chkSaturday" + index)).scope();
            if (scope1.obj.Saturday === true) {
                scope1.obj.T_SATURDAY = 1;
            }
            else if (scope1.obj.Saturday === false) {
                scope1.obj.T_SATURDAY = undefined;
            }
        }
        $scope.chkSunday = function (index) {
            var scope1 = angular.element($("#chkSunday" + index)).scope();
            if (scope1.obj.Sunday === true) {
                scope1.obj.T_SUNDAY = 1;
            }
            else if (scope1.obj.Sunday === false) {
                scope1.obj.T_SUNDAY = undefined;
            }
        }
        $scope.chkMonday = function (index) {
            var scope1 = angular.element($("#chkMonday" + index)).scope();
            if (scope1.obj.Monday === true) {
                scope1.obj.T_MONDAY = 1;
            }
            else if (scope1.obj.Monday === false) {
                scope1.obj.T_MONDAY = undefined;
            }
        }
        $scope.chkTuesday = function (index) {
            var scope1 = angular.element($("#chkTuesday" + index)).scope();
            if (scope1.obj.Tuesday === true) {
                scope1.obj.T_TUESDAY = 1;
            }
            else if (scope1.obj.Tuesday === false) {
                scope1.obj.T_TUESDAY = undefined;
            }
        }

        $scope.chkWednesday = function (index) {
            var scope1 = angular.element($("#chkWednesday" + index)).scope();
            if (scope1.obj.Wednesday === true) {
                scope1.obj.T_WEDNESDAY = 1;
            }
            else if (scope1.obj.Wednesday === false) {
                scope1.obj.T_WEDNESDAY = undefined;
            }
        }

        $scope.chkThursday = function (index) {
            var scope1 = angular.element($("#chkThursday" + index)).scope();
            if (scope1.obj.Thursday === true) {
                scope1.obj.T_THURSDAY = 1;
            }
            else if (scope1.obj.Thursday === false) {
                scope1.obj.T_THURSDAY = undefined;
            }
        }
        $scope.chkFriday = function (index) {
            var scope1 = angular.element($("#chkFriday" + index)).scope();
            if (scope1.obj.Friday === true) {
                scope1.obj.T_FRIDAY = 1;
            }
            else if (scope1.obj.Friday === false) {
                scope1.obj.T_FRIDAY = undefined;
            }
        }

        $scope.chkInactive = function (index) {
            var scope1 = angular.element($("#chkInactive" + index)).scope();
            if (scope1.obj.DRUG_INACTIVE_FLAG === true) {
                scope1.obj.T_DRUG_INACTIVE_FLAG = 1;
            }
            else if (scope1.obj.DRUG_INACTIVE_FLAG === false) {
                scope1.obj.T_DRUG_INACTIVE_FLAG = undefined;
            }
        }
        $scope.frequency = function (i) {
            var intmorning, intnoon, intnight;
            intmorning = 0;
            intnoon = 0;
            intnight = 0;

            if ($scope.obj.MedicineList[i].T_MORNING_DOSE_UNIT != null) {
                intmorning = parseInt($scope.obj.MedicineList[i].T_MORNING_DOSE_UNIT);
            }
            if ($scope.obj.MedicineList[i].T_NOON_DOSE_UNIT != null) {
                intnoon = parseInt($scope.obj.MedicineList[i].T_NOON_DOSE_UNIT);
            }
            if ($scope.obj.MedicineList[i].T_NIGHT_DOSE_UNIT != null) {
                intnight = parseInt($scope.obj.MedicineList[i].T_NIGHT_DOSE_UNIT);
            }
            $scope.obj.MedicineList[i].T_FREQUENCY_CODE = intmorning + intnoon + intnight;
        }


        function Save() {

            $scope.obj.t74039.T_ENTRY_USER = $scope.obj.T_ENTRY_USER;
            $scope.obj.t74039.T_ENTRY_DATE = $scope.obj.T_ENTRY_DATE;
            $scope.obj.t74039.T_PRESCRIPTION_ID = $scope.obj.t74113.T_PRESCRIPTION_ID;


            $scope.obj.t74040 = [];
            for (var i = 0; i < $scope.obj.MedicineList.length; i++) {
                $scope.obj.demo_t74040 = {};
                $scope.obj.demo_t74040.T_ITEM_CODE = $scope.obj.MedicineList[i].T_ITEM_CODE;
                $scope.obj.demo_t74040.T_TRADE_CODE = $scope.obj.MedicineList[i].T_TRADE_CODE;
                $scope.obj.demo_t74040.T_GEN_CODE = $scope.obj.MedicineList[i].T_GEN_CODE;
                $scope.obj.demo_t74040.T_REQUEST_STRENGTH = $scope.obj.MedicineList[i].T_REQUEST_STRENGTH;
                $scope.obj.demo_t74040.T_FORM_ID = $scope.obj.MedicineList[i].T_FORM_ID;
                $scope.obj.demo_t74040.T_DRUG_ROUTE_CODE = $scope.obj.MedicineList[i].T_DRUG_ROUTE_CODE;

                $scope.obj.demo_t74040.T_MORNING_DOSE_UNIT = $scope.obj.MedicineList[i].T_MORNING_DOSE_UNIT === undefined || $scope.obj.MedicineList[i].T_MORNING_DOSE_UNIT === "" ? 0 : $scope.obj.MedicineList[i].T_MORNING_DOSE_UNIT;
                $scope.obj.demo_t74040.T_NOON_DOSE_UNIT = $scope.obj.MedicineList[i].T_NOON_DOSE_UNIT === undefined || $scope.obj.MedicineList[i].T_NOON_DOSE_UNIT === "" ? 0 : $scope.obj.MedicineList[i].T_NOON_DOSE_UNIT;
                $scope.obj.demo_t74040.T_NIGHT_DOSE_UNIT = $scope.obj.MedicineList[i].T_NIGHT_DOSE_UNIT === undefined || $scope.obj.MedicineList[i].T_NIGHT_DOSE_UNIT === "" ? 0 : $scope.obj.MedicineList[i].T_NIGHT_DOSE_UNIT;
                //$scope.obj.demo_t74040.T_NOON_DOSE_UNIT = $scope.obj.MedicineList[i].T_NOON_DOSE_UNIT;
                //$scope.obj.demo_t74040.T_NIGHT_DOSE_UNIT = $scope.obj.MedicineList[i].T_NIGHT_DOSE_UNIT;
                $scope.obj.demo_t74040.T_MEAL_INSTRUCTION = $scope.obj.MedicineList[i].T_MEAL_INSTRUCTION;
                $scope.obj.demo_t74040.T_INS_TIME = $scope.obj.MedicineList[i].T_INS_TIME;
                $scope.obj.demo_t74040.T_SATURDAY = $scope.obj.MedicineList[i].T_SATURDAY;
                $scope.obj.demo_t74040.T_SUNDAY = $scope.obj.MedicineList[i].T_SUNDAY;
                $scope.obj.demo_t74040.T_MONDAY = $scope.obj.MedicineList[i].T_MONDAY;
                $scope.obj.demo_t74040.T_TUESDAY = $scope.obj.MedicineList[i].T_TUESDAY;
                $scope.obj.demo_t74040.T_WEDNESDAY = $scope.obj.MedicineList[i].T_WEDNESDAY;
                $scope.obj.demo_t74040.T_THURSDAY = $scope.obj.MedicineList[i].T_THURSDAY;
                $scope.obj.demo_t74040.T_FRIDAY = $scope.obj.MedicineList[i].T_FRIDAY;
                $scope.obj.demo_t74040.T_REMARKS_DTL = '';
                if ($scope.obj.MedicineList[i].listOfAdvice !== undefined) {
                    var remarks = $scope.obj.MedicineList[i].listOfAdvice;
                    if (remarks.length !== 0) {
                        for (var k = 0; k < remarks.length; k++) {
                            $scope.obj.demo_t74040.T_REMARKS_DTL += remarks[k].singleAdvice + ". ";
                        }
                    }
                }

                //$scope.obj.demo_t74040.T_REMARKS_DTL = $scope.obj.MedicineList[i].T_REMARKS_DTL;
                $scope.obj.demo_t74040.T_FREQUENCY_CODE = $scope.obj.MedicineList[i].T_FREQUENCY_CODE;
                $scope.obj.demo_t74040.T_FREQUENCY_INS_ID = $scope.obj.MedicineList[i].T_FREQUENCY_INS_ID;
                $scope.obj.demo_t74040.T_DOSE_DURATION_CODE = $scope.obj.MedicineList[i].T_DOSE_DURATION_CODE;
                $scope.obj.demo_t74040.T_DOSE_DURATION_INS_ID = $scope.obj.MedicineList[i].T_DOSE_DURATION_INS_ID;
                $scope.obj.demo_t74040.T_DRUG_INACTIVE_FLAG = $scope.obj.MedicineList[i].T_DRUG_INACTIVE_FLAG;
                $scope.obj.demo_t74040.T_ENTRY_USER = $scope.obj.T_ENTRY_USER;
                $scope.obj.demo_t74040.T_ENTRY_DATE = $scope.obj.T_ENTRY_DATE;


                $scope.obj.t74040.push($scope.obj.demo_t74040);
            }


            $scope.obj.t74078 = [];
            for (var j = 0; j < $scope.obj.DiagonosisList.length; j++) {
                if ($scope.obj.DiagonosisList[j].T_COST_TYPE_DTL_ID !== undefined) {
                    $scope.obj.demo_t74078 = {};
                    $scope.obj.demo_t74078.T_REQUEST_ID = $scope.obj.DiagonosisList[j].T_REQUEST_ID;
                    $scope.obj.demo_t74078.T_COST_TYPE_DTL_ID = $scope.obj.DiagonosisList[j].T_COST_TYPE_DTL_ID;
                    $scope.obj.demo_t74078.T_ENTRY_USER = $scope.obj.T_ENTRY_USER;
                    $scope.obj.demo_t74078.T_ENTRY_DATE = $scope.obj.T_ENTRY_DATE;
                    $scope.obj.t74078.push($scope.obj.demo_t74078);
                }
            }
            var t39 = $scope.obj.t74039;
            var t40 = $scope.obj.t74040;
            var t78 = $scope.obj.t74078;
            var param = {};
            param.T_Doc_ID = $scope.obj.t74039.T_DOC_ID;
            param.T_REQUEST_ID = $scope.obj.t74039.T_REQUEST_ID;
            param.title = 'Bill';
            param.url = 'tabBill.tpl.html';
            param.hin = 'BILL';
            var data = T74113Service.Insert(t39, t40, t78);
            data.then(function (dt) {
                sessionStorage.setItem("param", JSON.stringify(param));
                if (dt == "Save successfully") {
                    alert($scope.getSingleMsg('S0002'));
                }
                if (dt == "Update successfully") {
                    alert($scope.getSingleMsg('S0003'));
                }
                if (dt == "you are not able to Update because of Billing has been completed") {
                    alert($scope.getSingleMsg('S0021'));
                }

                //alert(dt);
                //$window.location = "/Transaction/T74046";

            });

        }
        $rootScope.$on('T74113Emit', function (event, data) {
            if (data == 'p') {
                Save();
            }
        });

    }]);