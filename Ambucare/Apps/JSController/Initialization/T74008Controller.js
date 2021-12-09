
app.controller("T74008Controller", ["$scope", "$filter", "$http", "$window", "T74008Service", "Data","LabelService",
    function (scope, $filter, $http, $window, T74008Service, Data, LabelService) {
       // scope.ProType = {};
        scope.obj = {};
        scope.obj = Data;

        //For Entry User
        var EntryUser = T74008Service.EntryUser();
        EntryUser.then(function (data) {
            scope.obj.T_ENTRY_USER = data;
            scope.obj.T_ENTRY_DATE = $filter('date')(new Date(), 'dd-MMM-yyyy');
        });

        scope.Save_Click = function() {
            if (scope.obj.T_LANG1_NAME, scope.obj.T_LANG2_NAME) {
                var save = T74008Service.SaveData(scope.obj);
                save.then(function(msg) {
                    if (msg == "Data Save Successfully") {
                        alert(scope.getSingleMsg('S0012'));
                    } else if (msg == "Data Update Successfully") {
                        alert(scope.getSingleMsg('S0003'));
                    }
                    // alert(msg);
                    // for grid load
                    var gridLoad = T74008Service.getGridData();
                    gridLoad.then(function(data) {
                        scope.EmployeeList = data;
                        $window.location = "";
                    });
                });
            } else if (scope.obj.T_LANG1_NAME === undefined) {
                //$window.confirm('Local Name is requered');
                alert(scope.getSingleMsg('M0002'));
                scope.localName.show();
            } else {
                angular.element('#txtEnglishName').focus();
                //$window.confirm('English Name is requered');
                alert(scope.getSingleMsg('M0003'));
            }
        };

        scope.Delete_Click = function () {
            var delet = T74008Service.Delete(scope.obj.T_TYPE_ID);
            delet.then(function (msg) {
                if (msg == "Data Delete Successfully") {
                    alert(scope.getSingleMsg('S0007'));
                }
                //alert(msg);
                // for grid load
                var gridLoad = T74008Service.getGridData();
                gridLoad.then(function (data) {
                    scope.EmployeeList = data;
                    $window.location = "";
                });

            });
        }
        scope.Clear_Click = function() {
            $window.location = "";
        }
        // for grid load

        var gridLoad = T74008Service.getGridData();
        gridLoad.then(function (data) {

            scope.ProTypeList = data;
        });

        //===
        // Form Label Data Service Start 
        var labelData = LabelService.getlabeldata('T74008');
        labelData.then(function (data) {

            scope.entity = data;
        });
        // Form Label Data Service End 

        //For picking up data into text
        scope.setClickedRow = function (index, Z) {

            scope.selectedRow = index;
            scope.obj.T_TYPE_ID = Z.T_TYPE_ID;
            scope.obj.T_LANG1_NAME = Z.T_LANG1_NAME;
            scope.obj.T_LANG2_NAME = Z.T_LANG2_NAME;
        }
        scope.$watch('selectedRow', function () {
            console.log('Do Some processing'); //runs the block whenever selectedRow is changed. 
        });
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