app.controller("T74133Controller", ["$scope", "$filter", "$http", "$window", "T74133Service", "LabelService",  "Data",
    function ($scope, $filter, $http, $window, T74133Service, LabelService, Data) {
        //for instance
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.t = {};
        $scope.obj.T_AMBU_REG_ID = 1;
        $scope.obj.t.selected = {};
        $scope.obj.t.T_AMBU_REG_ID = {};
        $scope.obj.data = {};
        
        
        var labelData = LabelService.getlabeldata('T74133');
        labelData.then(function (data) {
            $scope.entity = data;
        });
        $scope.stest = function(data) {
            alert(JSON.stringify(data.target.id + JSON.parse(sessionStorage.getItem("FCode"))));
        };

        $scope.Save = function() {
            if ($scope.obj.T_PER_ID === undefined) {
                var save = T74133Service.Insert_T74133($scope.obj);
                save.then(function(data) {
                   // alert("Data saved Succesfully");
                    alert($scope.getSingleMsg('S0012'));
                   $window.location.reload();

                });
            } else {
                var saveD = T74133Service.Insert_T74133($scope.obj);
                saveD.then(function(data) {
                    //alert("Data updated Succesfully");
                    alert(scope.getSingleMsg('S0003'));
                    $window.location.reload();
                });
            }
        };

        //For Store DropDown Start 
        var ZoneData = T74133Service.GetZone();
        ZoneData.then(function (data) {
            $scope.zoneList = data;
        });

        var StoreDataTo = T74133Service.GetStoreListTo();
        StoreDataTo.then(function (data) {
            $scope.StoreIdTo = data;
            $scope.obj.t.selected = { T_AMBU_REG_ID: data[0].T_AMBU_REG_ID, T_LANG2_NAME: data[0].T_LANG2_NAME };
        });
        $scope.check = function () {
            $scope.store($scope.obj.T_AMBU_REG_ID);
        };

        $scope.GetGridAllData = function (ambuId,zone) {
            var GridData = T74133Service.GetGridAllData(ambuId,zone);
            GridData.then(function(data) {
                $scope.obj.ambulanceInfo = [];
                for (var i = 0; i < data.length; i++) {
                    $scope.obj.test = {};
                    $scope.obj.test.T_SITE_CODE = data[i].T_SITE_CODE;
                    $scope.obj.test.T_LANG2_NAME = data[i].T_LANG2_NAME;
                    $scope.obj.test.T_ACTIVE = data[i].T_ACTIVE;
                    $scope.obj.ambulanceInfo.push($scope.obj.test);
                } 

            });

        };

        $scope.Save_Click = function() {
                $scope.obj.t74096 = [];
                for (var i = 0; i < $scope.obj.ambulanceInfo.length; i++) {
                    $scope.obj._t74096 = {};
                    $scope.obj._t74096.T_SITE_CODE = $scope.obj.ambulanceInfo[i].T_SITE_CODE;
                    $scope.obj._t74096.T_ACTIVE = $scope.obj.ambulanceInfo[i].T_ACTIVE;
                    $scope.obj._t74096.T_AMBU_REG_ID = $scope.obj.T_AMBU_REG_ID;
                    $scope.obj.t74096.push($scope.obj._t74096);
                }
                //End the loop
                var t96 = $scope.obj.t74096;
                var data = T74133Service.Insert_T74133(t96);
                data.then(function(dt) {
                   //alert('Data saved sucessfully');
                    alert($scope.getSingleMsg('S0003'));
                });
        };

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
            if (letterMatch.test(item.T_LANG2_NAME) || letterMatch.test(item.T_SITE_CODE)) {
                // return items;
                filtered.push(item);
            }
        }
        return filtered;


    };
});