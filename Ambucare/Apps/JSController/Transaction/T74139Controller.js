
app.controller("T74139Controller", ["$scope", "$window", "T74134Service", "LabelService", "Data", "$filter","$rootScope",
    function ($scope, $window, T74134Service,LabelService, Data, $filter, $rootScope) {
            $scope.obj = {};
            $scope.obj = Data;
        $scope.obj.t74134 = {};
        $scope.obj.T74074 = {};
        $scope.obj.ItemList = {};

        var labelData = LabelService.getlabeldata('T74125');
        labelData.then(function(data) {
            $scope.entity = data;
        });

        //$scope.PatInfo = function() {
        //======================== For Tab start ====================
        //$scope.tabs = [
        //    { title: 'Medication', url: 'tabMedication.tpl.html', hin: 'MEDICAL' },
        //    { title: 'Service', url: 'tabService.tpl.html', hin: 'SER' }
        //    //{ title: 'Medication', url: '/PartPages/T74134.html', hin: 'MED' },
        //    //{ title: 'Prescription', url: '/PartPages/T74113.html', hin: 'PRE' },
        //    //{ title: 'Bill', url: 'tabBill.tpl.html', hin: 'BILL' }

        //];
       // $scope.currentTabService = 'tabMedication.tpl.html';
       // $scope.obj.HidenfieldService = 'MEDICAL';
        //var param = JSON.parse(sessionStorage.getItem("param"));
        //if (param === null) {
        //    $scope.obj.HidenfieldService = 'MEDICAL';
        //    $scope.currentTabService = 'tabMedication.tpl.html';

        //} else {
        //    $scope.obj.HidenfieldService = param.hin;
        //    $scope.currentTabService = param.url;
        //   // $scope.bill();

        //}

        //$scope.onClickTab = function(tab) {

        //    $scope.obj.HidenfieldService = tab.hin;
        //    $scope.currentTabService = tab.url;
        //    if (tab.hin === 'SER') {
        //        ambulanceservice();
               
        //    } 
        //    //else if (tab.hin === 'DIA') {
        //    //    healthScreen();
        //    //    gethealthData();
        //    //}


        //};
        //$scope.isActiveTab = function (tabUrl) { return tabUrl === $scope.currentTabService; };
        //======================== For Tab end ====================

            var patdata = T74134Service.getPatInfo();
            patdata.then(function(d) {
                if (d.length !== 0) {
                    $scope.obj.t74134.T_REQUEST_ID = d[0].T_REQUEST_ID;
                    $scope.obj.t74134.T_AMBU_REG_ID = d[0].T_AMBU_REG_ID;
                    $scope.obj.t74134.T_PAT_ID = d[0].T_PAT_ID;
                    $scope.obj.t74134.FullName = d[0].T_FIRST_LANG2_NAME +
                        ' ' +
                        d[0].T_FATHER_LANG2_NAME +
                        ' ' +
                        d[0].T_GFATHER_LANG2_NAME +
                        ' ' +
                        d[0].T_FAMILY_LANG2_NAME;
                    $scope.obj.t74134.Gender = d[0].Gender;
                    $scope.obj.t74134.Nationality = d[0].Nationality;
                    $scope.obj.t74134.MaritalStatus = d[0].MaritalStatus;
                    //$scope.obj.t74113.T_HIGHT = d[0].T_HIGHT;
                    $scope.obj.t74134.T_WEIGHT = d[0].T_WEIGHT;
                    $scope.obj.t74134.T_PRESCRIPTION_ID = d[0].T_PRESCRIPTION_ID;
                    if (d[0].Age !== null) {
                        var myDate = new Date(d[0].Age.match(/\d+/)[0] * 1);
                        var dt = $filter('date')(myDate, "yyyy,MM,dd");
                        $scope.obj.t74134.Age = $filter('ageFilter')(new Date(dt));
                    }

                    var vital = T74134Service.getVitalSign($scope.obj.t74134.T_REQUEST_ID );
                    vital.then(function (data) {
                        $scope.obj.t74134.T_TEMP = data[0].T_TEMP;
                        // scope.obj.T74043.T_HIGHT = data[0].T_HIGHT;
                        $scope.obj.t74134.T_WEIGHT = data[0].T_WEIGHT;
                        $scope.obj.t74134.T_BP_SYS = data[0].T_BP_SYS;
                        $scope.obj.t74134.T_BP_DIA = data[0].T_BP_DIA;
                        $scope.obj.t74134.T_PULS = data[0].T_PULS;
                        $scope.obj.t74134.T_RESP = data[0].T_RESP;
                        $scope.obj.t74134.T_OS = data[0].T_OS;
                    });
                    var item = T74134Service.getItem($scope.obj.t74134.T_AMBU_REG_ID);
                    item.then(function (i) {
                        $scope.dataList = [];
                        var jsonItem = JSON.parse(i);
                      //  $scope.obj.ItemList = jsonItem;
                        for (var j = 0; j < jsonItem.length; j++) {
                            $scope.masterObj = {};
                            $scope.masterObj.T_STORE_ID = jsonItem[j].T_STORE_ID;
                            $scope.masterObj.T_ITEM_ID = jsonItem[j].T_ITEM_ID;
                            $scope.masterObj.ISSED_ITEM = jsonItem[j].ISSED_ITEM;
                            $scope.masterObj.T_LANG2_NAME = jsonItem[j].T_LANG2_NAME;
                            $scope.masterObj.STOCK = (jsonItem[j].STOCK).toFixed();
                            $scope.masterObj.ISSU_QTY = 0;
                            $scope.masterObj.CHECK = 'NO';
                            $scope.dataList.push($scope.masterObj);
                        }
                        $scope.obj.ItemList = $scope.dataList;

                    });
                    var serv = T74134Service.getServiceData($scope.obj.t74134.T_REQUEST_ID);
                    serv.then(function (data) {
                        $scope.obj.ServiceList = JSON.parse(data);
                        $scope.obj.T74074.totalPrice = 0;
                    });
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
        $scope.PutQty_Click = function (ind) {
            $scope.NewQtyData = {};
            $scope.NewQtyData = [];
            for (var i = 0; i < $scope.obj.ItemList.length; i++) {
                if (ind === i) {
                   $scope.NewObj = {};
                    $scope.NewObj.T_STORE_ID = $scope.obj.ItemList[i].T_STORE_ID;
                    $scope.NewObj.T_ITEM_ID = $scope.obj.ItemList[i].T_ITEM_ID;
                    $scope.NewObj.T_LANG2_NAME = $scope.obj.ItemList[i].T_LANG2_NAME;
                    $scope.NewObj.STOCK = $scope.obj.ItemList[i].STOCK;
                    $scope.NewObj.ISSU_QTY = $scope.obj.ItemList[i].ISSU_QTY;
                    $scope.NewObj.CHECK = 'YES';
                    $scope.NewQtyData.push($scope.NewObj);

               } else {
                   $scope.NewObj = {};
                    $scope.NewObj.T_STORE_ID = $scope.obj.ItemList[i].T_STORE_ID;
                    $scope.NewObj.T_ITEM_ID = $scope.obj.ItemList[i].T_ITEM_ID;
                    $scope.NewObj.T_LANG2_NAME = $scope.obj.ItemList[i].T_LANG2_NAME;
                    $scope.NewObj.STOCK = $scope.obj.ItemList[i].STOCK;
                    $scope.NewObj.ISSU_QTY = $scope.obj.ItemList[i].ISSU_QTY;
                    $scope.NewObj.CHECK = $scope.obj.ItemList[i].CHECK;
                    $scope.NewQtyData.push($scope.NewObj);
               }
            }
            $scope.obj.ItemList = $scope.NewQtyData;
            $scope.NewQtyData = [];
        }

        //function Save_Click() {
        //   // scope.obj.Hidenfield == 'EMP'
        //    if ($scope.obj.HidenfieldService === 'MEDICAL') {
        //    $scope.t74037 = {};
        //    $scope.t74036 = {};
        //    $scope.t74037 = [];
        //    $scope.t74036.T_REQUEST_ID= $scope.obj.t74134.T_REQUEST_ID;
        //    $scope.t74036.T_STORE_ID= $scope.obj.t74134.T_AMBU_REG_ID;
        //    $scope.t74036.T_PAT_NO = $scope.obj.t74134.T_PAT_ID;

        //    for (var i = 0; i < $scope.obj.ItemList.length; i++) {
        //        if ($scope.obj.ItemList[i].CHECK === 'YES') {
        //            $scope.ItemObj = {};
        //            $scope.t74036.T_STORE_ID = $scope.obj.ItemList[i].T_STORE_ID;
        //            $scope.ItemObj.T_STORE_ID = $scope.obj.ItemList[i].T_STORE_ID;
        //            $scope.ItemObj.T_ITEM_ID = $scope.obj.ItemList[i].T_ITEM_ID;
        //          //  $scope.ItemObj.T_LANG2_NAME = $scope.obj.ItemList[j].T_LANG2_NAME;
        //            $scope.ItemObj.T_QUANTITY = $scope.obj.ItemList[i].ISSU_QTY;
        //            $scope.t74037.push($scope.ItemObj);
        //        }
        //    }

        //    var save = T74134Service.saveData($scope.t74036,$scope.t74037);
        //    save.then(function(data) {
        //        alert(data);
        //        loadItem();
        //    });
        //    } else if ($scope.obj.HidenfieldService === 'SER') {
        //        $scope.t74074 = {};
        //        $scope.t74079= {};
        //        $scope.t74079= [];
        //        $scope.t74074.T_REQUEST_ID = $scope.obj.t74134.T_REQUEST_ID;
        //        $scope.t74074.T_TOTAL_PRICE = $scope.obj.T74074.totalPrice;

        //        for (var j = 0; j < $scope.obj.ServiceList.length; j++) {
        //            if ($scope.obj.ServiceList[j].CHECK === 'YES') {
        //                $scope.ServiceObj = {};
        //                $scope.ServiceObj.T_COST_TYPE_DTL_ID = $scope.obj.ServiceList[j].T_COST_TYPE_DTL_ID;
        //                $scope.ServiceObj.T_PRICE = $scope.obj.ServiceList[j].T_SALE_PRICE;
        //                $scope.t74079.push($scope.ServiceObj);
        //            }
        //        }
        //        var ser = T74134Service.saveServiceData($scope.t74074, $scope.t74079);
        //        ser.then(function (data) {
        //            alert(data);
        //           // loadItem();
        //        });

        //    }


        //};
        
        //$rootScope.$on('T74134Emit', function (event, data) {
            
        //    if (data == 'm') {
        //        $scope.t74037 = {};
        //        $scope.t74036 = {};
        //        $scope.t74037 = [];
        //        $scope.t74036.T_REQUEST_ID = $scope.obj.t74134.T_REQUEST_ID;
        //        $scope.t74036.T_STORE_ID = $scope.obj.t74134.T_AMBU_REG_ID;
        //        $scope.t74036.T_PAT_NO = $scope.obj.t74134.T_PAT_ID;

        //        for (var i = 0; i < $scope.obj.ItemList.length; i++) {
        //            if ($scope.obj.ItemList[i].CHECK === 'YES') {
        //                $scope.ItemObj = {};
        //                $scope.t74036.T_STORE_ID = $scope.obj.ItemList[i].T_STORE_ID;
        //                $scope.ItemObj.T_STORE_ID = $scope.obj.ItemList[i].T_STORE_ID;
        //                $scope.ItemObj.T_ITEM_ID = $scope.obj.ItemList[i].T_ITEM_ID;
        //                //  $scope.ItemObj.T_LANG2_NAME = $scope.obj.ItemList[j].T_LANG2_NAME;
        //                $scope.ItemObj.T_QUANTITY = $scope.obj.ItemList[i].ISSU_QTY;
        //                $scope.t74037.push($scope.ItemObj);
        //            }
        //        }

        //        var save = T74134Service.saveData($scope.t74036, $scope.t74037);
        //        save.then(function (data) {
        //            alert(data);
        //            loadItem();
                    
        //        });

        //    }
           
        //});
        
        var customEvent39=$rootScope.$on('T74139Emit', function (event, data) {
            if (data == 's') {
                $scope.t74074 = {};
                $scope.t74079 = {};
                $scope.t74079 = [];
                $scope.t74074.T_REQUEST_ID = $scope.obj.t74134.T_REQUEST_ID;
                $scope.t74074.T_TOTAL_PRICE = $scope.obj.T74074.totalPrice;

                for (var j = 0; j < $scope.obj.ServiceList.length; j++) {
                    if ($scope.obj.ServiceList[j].CHECK === 'YES') {
                        $scope.ServiceObj = {};
                        $scope.ServiceObj.T_COST_TYPE_DTL_ID = $scope.obj.ServiceList[j].T_COST_TYPE_DTL_ID;
                        $scope.ServiceObj.T_PRICE = $scope.obj.ServiceList[j].T_SALE_PRICE;
                        $scope.t74079.push($scope.ServiceObj);
                    }
                }
                if ($scope.t74079.length>0) {
                    var ser = T74134Service.saveServiceData($scope.t74074, $scope.t74079);
                    ser.then(function (d) {
                        alert(d);
                        loadService();
                        //if (d === '1') {
                        //    alert($scope.getSingleMsg('S0012'));
                        //    loadService();
                        //} else {
                        //   // alert(scope.getSingleMsg('S0012'));
                        //    loadService();
                        //}
                      

                    });
                } else {
                    alert('Please check Service !!');
                }
                
            }
        });
        $scope.$on('$destroy', function () {
            customEvent39();
        });
        function loadItem() {
            var item = T74134Service.getItem($scope.obj.t74134.T_AMBU_REG_ID);
            item.then(function (i) {
                $scope.dataList = [];
                var jsonItem = JSON.parse(i);
                //  $scope.obj.ItemList = jsonItem;
                for (var j = 0; j < jsonItem.length; j++) {
                    $scope.masterObj = {};
                    $scope.masterObj.T_STORE_ID = jsonItem[j].T_STORE_ID;
                    $scope.masterObj.T_ITEM_ID = jsonItem[j].T_ITEM_ID;
                    $scope.masterObj.ISSED_ITEM = jsonItem[j].ISSED_ITEM;
                    $scope.masterObj.T_LANG2_NAME = jsonItem[j].T_LANG2_NAME;
                    $scope.masterObj.STOCK = (jsonItem[j].STOCK).toFixed();
                    $scope.masterObj.ISSU_QTY = 0;
                    $scope.masterObj.CHECK = 'NO';
                    $scope.dataList.push($scope.masterObj);
                }
                $scope.obj.ItemList = $scope.dataList;

            });
        }

        function loadService() {
            var serv = T74134Service.getServiceData($scope.obj.t74134.T_REQUEST_ID);
            serv.then(function (data) {
                $scope.obj.ServiceList = JSON.parse(data);
                $scope.obj.T74074.totalPrice = 0;
            });
        }
        ambulanceservice();
        function ambulanceservice() {
            var serv = T74134Service.getServiceData($scope.obj.t74134.T_REQUEST_ID);
            serv.then(function (data) {
                $scope.obj.ServiceList = JSON.parse(data);
                $scope.obj.T74074.totalPrice = 0;
            });
        }
        $scope.CheckForQuanty = function (ind) {
            if ($scope.obj.ItemList[ind].CHECK==='YES') {
                $scope.obj.ItemList[ind].ISSU_QTY += 1;
            } else {
                $scope.obj.ItemList[ind].ISSU_QTY = 0;
            }
           
        }
        $scope.obj.T74074.totalPrice = 0;
        var value = 0;
        $scope.CheckBox_Click = function(i) {
            var dd = $scope.obj.ServiceList[i].T_SALE_PRICE;
            if ($scope.obj.ServiceList[i].CHECK === 'YES') {
                $scope.Price = $scope.obj.T74074.totalPrice;
                   value = parseFloat($scope.Price);
                   value += dd;
                $scope.obj.T74074.totalPrice = value.toFixed(3);
                value = 0;
            } else {
                $scope.Price = $scope.obj.T74074.totalPrice;
                value = parseFloat($scope.Price);
                value -= dd;
                $scope.obj.T74074.totalPrice = value.toFixed(3);
                value = 0;
            }
           
        };
       
        $scope.Number_Click = function(btn, i) {
            if (btn === 'plus') {
                $scope.obj.ItemList[i].ISSU_QTY += 1;
                $scope.obj.ItemList[i].CHECK = 'YES';
            } else {
                if ($scope.obj.ItemList[i].ISSU_QTY > 0) {
                    $scope.obj.ItemList[i].ISSU_QTY -= 1;
                    if ($scope.obj.ItemList[i].ISSU_QTY === 0) {
                        $scope.obj.ItemList[i].CHECK = 'NO';
                    }
                } else {
                    $scope.obj.ItemList[i].CHECK = 'NO';
                }

            }

        };
      

        var item = T74134Service.getItem();
        item.then(function(data) {

        });
        $scope.Show_MedicinePopup = function() {
            
            var prevMedicine = T74134Service.getPreviousMedicine($scope.obj.t74134.T_REQUEST_ID);
            prevMedicine.then(function(data) {
                $scope.obj.PreviousMedicineList = JSON.parse(data);
               // var jsonData = JSON.parse(data);
                //calculateRows();
            });
            document.getElementById("ShowPreviusMedication").style.display = "block";
        };
        $scope.ClosePatientPopup = function () {
           // var dd = $scope.obj.showPreviusMedicen;
            document.getElementById("ShowPreviusMedication").style.display = "none";
            $scope.obj.showPreviusMedicen='NO';
        };

        //------------------------------
        function calculateRows() {
        
            if ($scope.obj.PreviousMedicineList.length > 0) {
                $scope.obj.PreviousMedicineList[0].matchPreviousRow = false;
                for (var i = 0; i < $scope.obj.PreviousMedicineList.length; i++) {
                    var field = $scope.obj.PreviousMedicineList[i].T_DOSE;
                    var t_index = 1;
                    for (var j = i + 1; j < $scope.obj.PreviousMedicineList.length; j++) {
                        if ($scope.obj.PreviousMedicineList[j].T_DOSE === field && !$scope.obj.PreviousMedicineList[j].matchPreviousRow) {
                            t_index++;
                            $scope.obj.PreviousMedicineList[j].matchPreviousRow = true;
                        } else {
                            $scope.obj.PreviousMedicineList[j].matchPreviousRow = false;
                            break;
                        }
                    }
                    $scope.obj.PreviousMedicineList[i].t_index = t_index;
                }
            }
        }
        //--------------------------------
        //======================
       

        //======================

    }
]);
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
app.filter('cartypefilter', function () {
    return function (items, search) {
        if (!search) {
            return items;
        }
        var filtered = [];
        var letterMatch = new RegExp(search, 'i');


        for (var i = 0; i < items.length; i++) {
            var item = items[i];
            if (letterMatch.test(item.T_LANG2_NAME)) { //|| letterMatch.test(item.Mobile.substring(0, 5))
                // return items;
                filtered.push(item);
            }
        }
        return filtered;


    };
});