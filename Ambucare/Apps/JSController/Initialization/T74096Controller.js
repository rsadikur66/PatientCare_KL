app.controller("T74096Controller", ["$scope", "$filter", "$http", "$window", "T74096Service", "LabelService", "T74096PatFactory", "T74015Service", "uiGridConstants", "DTOptionsBuilder", "DTColumnBuilder", "Data",
    function (scope, $filter, $http, $window, T74096Service, LabelService, T74096PatFactory, T74015Service, uiGridConstants, DTOptionsBuilder, DTColumnBuilder, Data) {
        //for instance
        scope.obj = {};
        scope.obj = Data;
        // Form Label Data Service Start    
        var labelData = LabelService.getlabeldata('T74096');
        labelData.then(function (data) {
            scope.entity = data;
        });
        scope.stest = function (data) {
            alert(JSON.stringify(data.target.id + JSON.parse(sessionStorage.getItem("FCode"))));
        }
        scope.Save = function () {
            if (scope.obj.T_PER_ID === undefined) {
                var save = T74096Service.insert_T74096(scope.obj);
                save.then(function (data) {
                    //alert("Data saved Succesfully");
                    //$window.location = "/Initialization/T74096";
                    alert(scope.getSingleMsg('S0012'));
                    $window.location.reload();

                });
            }
            else {
                var saveD = T74096Service.insert_T74096(scope.obj);
                saveD.then(function (data) {
                    //alert("Data updated Succesfully");
                    //$window.location = "/Initialization/T74096";
                    alert(scope.getSingleMsg('S0003'));
                    $window.location.reload();
                });
            }
        }
        scope.Delete = function () {
            if (scope.obj.T_PER_ID !== undefined) {
                if ($window.confirm('Are you sure ?')) {
                    var del = T74096Service.delete_T74096(scope.obj);
                    del.then(function (data) {
                        //alert("Data delete Succesfully");
                        //$window.location = "/Initialization/T74096";

                        alert(scope.getSingleMsg('S0007'));
                        $window.location.reload();
                    });
                }
                return false;
            }
            else {
                //alert('Please select Item Brand');
                alert(scope.getSingleMsg('S0011'));
                return false;
            }
        };
        scope.Clear = function () {
            scope.obj.T_PER_ID = '';
            scope.obj.T_LANG1_NAME = '';
            scope.obj.T_LANG2_NAME = '';

            scope.obj.T_PER_ID = undefined;
        }

        //scope.Print = function () {
        //    var print = T74096Service.print_T74096();
        //}

        scope.PatInfo = function () {
            var url = '';
            var data = '';
            T74096PatFactory.getModal(url, data);
        }


        //For Store DropDown Start 
        var ZoneData = T74096Service.GetZone();
        ZoneData.then(function (data) {
            scope.zoneList = data;
        });

        scope.SiteList = function (id) {
            var SiteData = T74096Service.GetSite(id);
            SiteData.then(function (data) {
                scope.sss = data;
                scope.obj.s = {};
                //scope.obj.s.selected = { T_SITE_CODE: '', T_LANG2_NAME: 'Select' };
            });
        };


        scope.GetGridAllData = function (siteCode) {
            var GridData = T74096Service.getAmbPatList(siteCode);
            GridData.then(function (data) {
                scope.obj.ambulanceInfo = [];
                for (var i = 0; i < data.length; i++) {
                    scope.obj.test = {};
                    scope.obj.test.T_AMBU_REG_ID = data[i].T_AMBU_REG_ID;
                    scope.obj.test.AmbulanceName = data[i].AmbulanceName;
                    //scope.obj.test.T_ACTIVE = data[i].T_ACTIVE;
                    //scope.obj.test.T_PAT_ID = data[i].T_PAT_ID;
                    //scope.obj.test.patientname = data[i].patientname;
                    //scope.obj.test.T_REQUEST_ID = data[i].T_REQUEST_ID;
                    //scope.obj.test.T_SITE_CODE = data[i].T_SITE_CODE;                    
                    scope.obj.test.T_ACTIVE = data[i].T_SITE_CODE === scope.obj.T_SITE_CODE ? data[i].T_ACTIVE : '2';
                    scope.obj.ambulanceInfo.push(scope.obj.test);



                    //scope.obj.test.T_ACTIVE_previou = data[i].T_SITE_CODE === g ? data[i].T_ACTIVE : '2';

                }//end for loop

            });

        }//main fucntion of GetGridalldata


        scope.Save_Click = function () {
            if (scope.obj.T_SITE_CODE !== undefined) {
                scope.obj.t74096 = [];
                for (var i = 0; i < scope.obj.ambulanceInfo.length; i++) {
                    //if (scope.obj.ambulanceInfo[i].T_ACTIVE_previou !== scope.obj.ambulanceInfo[i].T_ACTIVE) {
                        scope.obj._t74096 = {};

                        //scope.obj._t74096.T_AMBU_REG_ID = scope.obj.ambulanceInfo[i].T_AMBU_REG_ID;
                        //scope.obj._t74096.AmbulanceName = scope.obj.ambulanceInfo[i].AmbulanceName;
                        //scope.obj._t74096.T_PAT_ID = scope.obj.ambulanceInfo[i].T_PAT_ID;
                        //scope.obj._t74096.patientname = scope.obj.ambulanceInfo[i].patientname;

                        scope.obj._t74096.T_AMBU_REG_ID = scope.obj.ambulanceInfo[i].T_AMBU_REG_ID;
                        scope.obj._t74096.T_ACTIVE = scope.obj.ambulanceInfo[i].T_ACTIVE;
                    scope.obj._t74096.T_SITE_CODE = scope.obj.T_SITE_CODE;

                        //if (scope.obj.ambulanceInfo[i].T_SITE_CODE === null) {
                        //    scope.obj._t74096.T_SITE_CODE = scope.obj.T_SITE_CODE;
                        //} else {
                        //    if (scope.obj.ambulanceInfo[i].T_SITE_CODE === scope.obj.T_SITE_CODE) {
                        //        scope.obj._t74096.T_SITE_CODE = scope.obj.ambulanceInfo[i].T_SITE_CODE;
                        //    } else {
                        //        scope.obj._t74096.T_SITE_CODE = scope.obj.T_SITE_CODE;
                        //    }

                        //}
                        //  === null ? scope.obj.ambulanceInfo[i].T_SITE_CODE : scope.obj.T_SITE_CODE;
                        // scope.obj._t74096.T_SITE_CODE = scope.obj.T_SITE_CODE;
                        scope.obj.t74096.push(scope.obj._t74096);
                    }
              //  }//End the loop
                var t96 = scope.obj.t74096;
                //if (t96.length != 0) {
                var data = T74096Service.Insert_T74096(t96);
                data.then(function (dt) {
                    //alert('Data saved sucessfully');
                    alert(scope.getSingleMsg('S0003'));
                });
                //} else {
                //  alert('Please select one record.');
                //}
            } else {
                //alert("Please select site!!!");
                alert(scope.getSingleMsg('S0011'));
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
            if (letterMatch.test(item.AmbulanceName) || letterMatch.test(item.patientname)) {
                // return items;
                filtered.push(item);
            }
        }
        return filtered;


    };
});