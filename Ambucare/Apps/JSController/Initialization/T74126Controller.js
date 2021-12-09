app.controller("T74126Controller",
    ["$scope", "$filter", "$http", "$window", "$location", "LabelService", "T74126Service", "$cacheFactory", "uiGridConstants",
        "DTOptionsBuilder",
        "DTColumnBuilder", "Data",
        function (scope, $filter, $http, $window, $location, LabelService, T74126Service, $cacheFactory, Data) {
            //For Instance 
            scope.obj = {};
            scope.obj = Data;

            scope.obj.T74126 = {};
            scope.obj.T74093 = {};

            scope.obj.Itemlist = [];
            scope.obj.T74093.T_REGI_NO = '';
            //document.getElementById('btnDeleteProduct').visibility = false;
            scope.myFunc = function () {
                scope.count++;
                if (scope.obj.T74093.T_REGI_NO !== '') {
                    document.getElementById('buttonSave').disabled = false;
                }
            };


            //scope.obj.T74080 = {};
            //var url = $location.absUrl();
            //var param = url.split("=").pop();
            //scope.obj.transferList = [];


            // Form Label Data Service Start 
            var labelData = LabelService.getlabeldata("T74126");
            labelData.then(function (data) {
                scope.entity = data;
            });

            //scope.tabs = [
            //    { title: 'Pat Info From Ambulance', url: 'tabPatInfo.tpl.html', hin: 'EMP' },
            //    { title: 'Diagnostic', url: 'tabDiagnostic.tpl.html', hin: 'DIA' },
            //    { title: 'Bill', url: 'tabBill.tpl.html', hin: 'BILL' }
            //];

            scope.ShowProduct_Click = function () {
                scope.currentTab = 'tabPatInfo';
                scope.obj.T74093.T_EMP_ID = scope.obj.T74126.T_EMP_ID;
            }

            var emp = T74126Service.getEmployee();
            emp.then(function (data) {
                scope.EmployeeList = data;
            });

            scope.Close = function () {
                scope.currentTab = '';
                clear2();
            }

            scope.Save_Click = function () {
                if (scope.obj.T74126.T_EMP_ID != null) {
                    var update = T74126Service.insert_T74126(scope.obj.T74126, scope.obj.Itemlist);
                    update.then(function (data) {
                        $window.location = "";
                    });

                    //alert("Data Update Succesfully");
                    alert(scope.getSingleMsg('S0003'));
                } else {
                    var saveData = T74126Service.insert_T74126(scope.obj.T74126, scope.obj.Itemlist);
                    saveData.then(function (data) {
                       // alert("Save successfully");
                        alert(scope.getSingleMsg('S0012'));
                        //scope.obj.transferList = [];

                    });
                }
            }

            scope.NewItemList = [];
            scope.save = function (index, item) {
                // scope.NewObject = {};

                if (scope.obj.T74093.T_REGI_NO == '') {
                    document.getElementById('buttonSave').disabled = true;
                } else {
                    if (scope.obj.Itemlist.length !== 0) {
                        for (var i = 0; i < scope.obj.Itemlist.length; i++) {
                            if (scope.obj.T74093.T_REGI_NO !== scope.obj.Itemlist[i].T_REGI_NO) {
                               

                                scope.New2 = {};
                                scope.New2.T_REGI_NO = scope.obj.Itemlist[i].T_REGI_NO;
                                scope.New2.T_MODEL_NO = scope.obj.Itemlist[i].T_MODEL_NO;
                                scope.New2.T_ENGIN_NO = scope.obj.Itemlist[i].T_ENGIN_NO;
                                scope.New2.T_CHESES_NO = scope.obj.Itemlist[i].T_CHESES_NO;
                                scope.New2.T_DESC = scope.obj.Itemlist[i].T_DESC;
                                scope.New2.T_EMP_ID = scope.obj.Itemlist[i].T_EMP_ID; 
                                //scope.NewObject.LicenceNo1 = item.LicenceNo;
                                scope.New2.T_ITEM_ID = scope.obj.Itemlist[i].T_ITEM_ID;
                                scope.NewItemList.push(scope.New2);



                            } 
                            //scope.obj.Itemlist = scope.NewItemList;
                        }
                        scope.NewObject = {};
                        scope.I = {};
                        scope.NewObject.T_REGI_NO = item.T_REGI_NO;
                        scope.NewObject.T_MODEL_NO = item.T_MODEL_NO;
                        scope.NewObject.T_ENGIN_NO = item.T_ENGIN_NO;
                        scope.NewObject.T_CHESES_NO = item.T_CHESES_NO;
                        scope.NewObject.T_DESC = item.T_DESC;
                        scope.NewObject.T_EMP_ID = item.T_EMP_ID; 
                        //scope.NewObject.LicenceNo1 = scope.obj.Itemlist[i].LicenceNo1;
                        scope.NewObject.T_ITEM_ID = item.T_ITEM_ID;
                        scope.NewItemList.push(scope.NewObject);
                        //------------------------------------

                        scope.obj.Itemlist = scope.NewItemList;
                        scope.NewItemList = [];
                        scope.currentTab = '';
                        clear2();
                        document.getElementById('btnDeleteProduct').visibility = true;
                    } else { //
                        scope.NewObject2 = {};
                        scope.NewObject2.T_REGI_NO = item.T_REGI_NO;
                        scope.NewObject2.T_MODEL_NO = item.T_MODEL_NO;
                        scope.NewObject2.T_ENGIN_NO = item.T_ENGIN_NO;
                        scope.NewObject2.T_CHESES_NO = item.T_CHESES_NO;
                        scope.NewObject2.T_DESC = item.T_DESC;
                        scope.NewObject2.T_EMP_ID = item.T_EMP_ID; 
                        //scope.NewObject.LicenceNo1 = item.LicenceNo;
                        scope.NewObject2.T_ITEM_ID = item.T_ITEM_ID;
                        scope.NewItemList.push(scope.NewObject2);
                        scope.obj.Itemlist = scope.NewItemList;
                        scope.NewItemList = [];
                    }
                }
            }

            function clear() {
                scope.obj.T74126.T_FIRST_LANG2_NAME = '';
                scope.obj.T74126.T_FATHER_LANG2_NAME = '';
                scope.obj.T74126.T_ADDRESS1 = '';
                scope.obj.T74126.T_ADDRESS2 = '';
                scope.obj.T74126.T_MOBILE_NO = '';
                scope.obj.T74126.T_EMAIL_ID = '';
                scope.obj.T74126.T_EMP_STATUS = '';
            }

            function clear2() {
                scope.obj.T74093.T_REGI_NO = '';
                scope.obj.T74093.T_MODEL_NO = '';
                scope.obj.T74093.T_ENGIN_NO = '';
                scope.obj.T74093.T_CHESES_NO = '';
                scope.obj.T74093.T_DESC = '';
            }

            scope.GetEmProData = function (Em) {
                var EmProData = T74126Service.GetEmProData(Em.T_EMP_ID);
                EmProData.then(function (data) {
                    //scope.obj.F.selected = {};
                    scope.obj.T74126.T_EMP_ID = data[0].T_EMP_ID;
                    scope.obj.T74126.T_FIRST_LANG2_NAME = data[0].T_FIRST_LANG2_NAME;
                    scope.obj.T74126.T_FATHER_LANG2_NAME = data[0].T_FATHER_LANG2_NAME;
                    scope.obj.T74126.T_ADDRESS1 = data[0].T_ADDRESS1;
                    scope.obj.T74126.T_ADDRESS2 = data[0].T_ADDRESS2;
                    scope.obj.T74126.T_MOBILE_NO = data[0].T_MOBILE_NO;
                    scope.obj.T74126.T_EMAIL_ID = data[0].T_EMAIL_ID;
                    scope.obj.T74126.T_EMP_STATUS = data[0].T_EMP_STATUS;
                    scope.obj.Itemlist = [];
                    for (var i = 0; i < data.length; i++) {
                        scope.obj.test = {};
                        scope.obj.test.T_REGI_NO = data[i].T_REGI_NO;
                        scope.obj.test.T_CHESES_NO = data[i].T_CHESES_NO;
                        scope.obj.test.T_ENGIN_NO = data[i].T_ENGIN_NO;
                        scope.obj.test.T_MODEL_NO = data[i].T_MODEL_NO;
                        scope.obj.test.T_DESC = data[i].T_DESC;
                        scope.obj.test.T_EMP_ID = data[i].T_EMP_ID;
                        scope.obj.test.T_ITEM_ID = data[i].T_ITEM_ID;
                        //scope.obj.test.T_LOT_NO = data[i].Lot_no;
                        ////scope.obj.T_STOCK_DATE = data[i].stock_date;


                        //scope.obj.test.T_ITEM_UM_ID = data[i].UM_id;
                        //scope.obj.test.UomName = data[i].UM_Name;
                        scope.obj.Itemlist.push(scope.obj.test);
                    }//end for loop
                });
            }

            scope.EditProduct_Click = function ($index) {
                scope.obj.T74093.T_REGI_NO = scope.obj.Itemlist[$index].T_REGI_NO;
                scope.obj.T74093.T_CHESES_NO = scope.obj.Itemlist[$index].T_CHESES_NO;
                scope.obj.T74093.T_ENGIN_NO = scope.obj.Itemlist[$index].T_ENGIN_NO;
                scope.obj.T74093.T_MODEL_NO = scope.obj.Itemlist[$index].T_MODEL_NO;
                scope.obj.T74093.T_DESC = scope.obj.Itemlist[$index].T_DESC;
                scope.obj.T74093.T_EMP_ID = scope.obj.Itemlist[$index].T_EMP_ID;
                scope.obj.T74093.T_ITEM_ID = scope.obj.Itemlist[$index].T_ITEM_ID;

                //scope.obj.productno = $index;

                document.getElementById('showPanel').visibility = false;
                scope.currentTab = 'tabPatInfo';
            }
            scope.DeleteProduct_Click = function (index) {
                debugger;
                //scope.obj.Itemlist.push({ T_EMP_ID: scope.obj.Itemlist[index].T_EMP_ID });
                scope.obj.Itemlist.splice(index, 1);
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
