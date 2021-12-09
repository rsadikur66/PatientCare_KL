app.controller("T74130Controller", ["$scope", "$filter", "$http", "$window", "T74130Service", "LabelService", "T74130PatFactory", "T74015Service", "uiGridConstants", "DTOptionsBuilder", "DTColumnBuilder", "Data",
    function (scope, $filter, $http, $window, T74130Service, LabelService, T74130PatFactory, T74015Service, uiGridConstants, DTOptionsBuilder, DTColumnBuilder, Data) {
        //for instance
        scope.obj = {};
        scope.obj = Data;
        // Form Label Data Service Start 
        var labelData = LabelService.getlabeldata('T74130');
        labelData.then(function (data) {
            scope.entity = data;
        });
        var GridData = T74130Service.getAmbPatList_t74130();
            GridData.then(function (data) {
                scope.obj.ambulanceInfo = [];
                for (var i = 0; i < data.length; i++) {
                    scope.obj.test = {};
                    scope.obj.test.T_AMBU_REG_ID = data[i].T_AMBU_REG_ID;
                    scope.obj.test.AmbulanceName = data[i].AmbulanceName;
                    scope.obj.test.T_PAT_ID = data[i].T_PAT_ID;
                    scope.obj.test.patientname = data[i].patientname;                   
                    if (data[i].T_REQUEST_DATE != null) {
                        var Test_date = new Date(data[i].T_REQUEST_DATE.match(/\d+/)[0] * 1);
                        scope.obj.test.T_REQUEST_DATE = $filter('date')(Test_date, "dd/MM/yyyy");
                    }
                    else {
                        scope.obj.test.T_REQUEST_DATE = '';
                    }
                    scope.obj.test.T_CANCEL_STATUS = data[i].T_CANCEL_STATUS;
                    scope.obj.test.T_DISCH_STATUS = data[i].T_DISCH_STATUS;  
                    scope.obj.ambulanceInfo.push(scope.obj.test);

                }//end for loop
            });



        scope.stest = function (data) {
            alert(JSON.stringify(data.target.id + JSON.parse(sessionStorage.getItem("FCode"))));
        }
        
        scope.Save = function () {
            if (scope.obj.T_PER_ID === undefined) {
                var save = T74130Service.insert_T74130(scope.obj);
                save.then(function (data) {
                    //alert("Data saved Succesfully");
                    alert(scope.getSingleMsg('S0012'));
                    $window.location = "/Initialization/T74130";

                });
            }
            else {
                var save = T74130Service.insert_T74130(scope.obj);
                save.then(function (data) {
                    //alert("Data updated Succesfully");
                    alert(scope.getSingleMsg('S0003'));
                    $window.location = "/Initialization/T74130";
                });
            }
        }
        scope.Delete = function () {
            if (scope.obj.T_PER_ID !== undefined) {
                if ($window.confirm('Are you sure ?')) {
                    var del = T74130Service.delete_T74130(scope.obj);
                    del.then(function (data) {
                        //alert("Data delete Succesfully");
                        alert(scope.getSingleMsg('S0007'));
                        $window.location = "/Initialization/T74130";
                    });
                }
                return false;
            }
            else {
                //alert('Please select Item Brand');
                alert(scope.getSingleMsg('S0015'));
                return false;
            }
        };
        scope.Clear = function () {
            scope.obj.T_PER_ID = '';
            scope.obj.T_LANG1_NAME = '';
            scope.obj.T_LANG2_NAME = '';

            scope.obj.T_PER_ID = undefined;
        }

        //scope.GetGridAllData = function () {
        //    var GridData = T74130Service.getAmbPatList();
        //    GridData.then(function (data) {
        //        scope.obj.ambulanceInfo = [];
        //        for (var i = 0; i < data.length; i++) {
        //            scope.obj.test = {};
        //            scope.obj.test.T_AMBU_REG_ID = data[i].T_AMBU_REG_ID;
        //            scope.obj.test.AmbulanceName = data[i].AmbulanceName;
        //            scope.obj.test.T_PAT_ID = data[i].T_PAT_ID;
        //            scope.obj.test.patientname = data[i].patientname;
        //            scope.obj.test.T_REQUEST_ID = data[i].T_REQUEST_ID;
        //            scope.obj.test.T_SITE_CODE = data[i].T_SITE_CODE;
        //            scope.obj.test.T_ACTIVE = data[i].T_SITE_CODE === g ? data[i].T_ACTIVE : '2';
        //            scope.obj.ambulanceInfo.push(scope.obj.test);
                   
        //        }//end for loop
        //    });



        //}


        scope.Save_Click = function () {
            if (scope.obj.T_SITE_CODE !== undefined) {
                scope.obj.T74130 = [];
                for (var i = 0; i < scope.obj.ambulanceInfo.length; i++) {
                    if (scope.obj.ambulanceInfo[i].T_ACTIVE_previou !== scope.obj.ambulanceInfo[i].T_ACTIVE) {
                    scope.obj._T74130 = {};
                        scope.obj._T74130.T_REQUEST_ID = scope.obj.ambulanceInfo[i].T_REQUEST_ID;
                        scope.obj._T74130.T_ACTIVE = scope.obj.ambulanceInfo[i].T_ACTIVE;
                        scope.obj._T74130.T_SITE_CODE = scope.obj.ambulanceInfo[i].T_SITE_CODE;
                        scope.obj.T74130.push(scope.obj._T74130);
                    } 
                }//End the loop
                var t96 = scope.obj.T74130;
                    var data = T74130Service.Insert_T74130(t96);
                    data.then(function (dt) {
                        //alert('Data saved sucessfully');
                        alert(scope.getSingleMsg('S0012'));
                    });
            } else {
                //alert("Please select site!!!");
                alert(scope.getSingleMsg('S0035'));
            }
        }//End the save function

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

app.filter('cartypefilter', function () {
    return function (items, search) {
        if (!search) {
            return items;
        }
        var filtered = [];
        var letterMatch = new RegExp(search, 'i');


        for (var i = 0; i < items.length; i++) {
            var item = items[i];
            if (letterMatch.test(item.AmbulanceName) || letterMatch.test(item.patientname))  {
                // return items;
                filtered.push(item);
            }
        }
        return filtered;


    };
});