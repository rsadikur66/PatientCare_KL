app.controller("T74052Controller", ["$scope", "$filter", "$http", "$window", "T74052Service","LabelService","Data",
    function (scope, $filter, $http, $window, T74052Service, LabelService, Data) {
        //scope.zone = {};
        scope.obj = {};
        scope.obj = Data;

        var Zone = T74052Service.getZone();
        Zone.then(function (results) {
            scope.ZoneList = results ;
        });
        // Form Label Data Service Start 
        var labelData = LabelService.getlabeldata('T74052');
        labelData.then(function (data) {
           
            scope.entity = data;
        });
        // Form Label Data Service End 
        //For Entry User
        var EntryUser = T74052Service.EntryUser();
        EntryUser.then(function (data) {

            scope.obj.T_ENTRY_USER = data;
            scope.obj.T_ENTRY_DATE = $filter('date')(new Date(), 'dd-MMM-yyyy');
        });
        //============
        
        scope.Save_Click = function () {
            if (scope.obj.T_LANG1_NAME == undefined || scope.obj.T_LANG1_NAME == '') {
                // $window.confirm('Local Name is requered');
                alert(scope.getSingleMsg('M0002'));
                scope.localName.show();
                return;
            }
            if (scope.obj.T_LANG2_NAME == undefined || scope.obj.T_LANG2_NAME == '') {
                // $window.confirm('English Name is requered');
                alert(scope.getSingleMsg('M0003'));
                scope.localName.show();
                return;
            }

            T74052Service.saveData(scope.obj).then(function (result) {
                if (result == "Data Save Successfully") {
                    alert(scope.getSingleMsg('S001'));
                    $window.location.reload();
                }
                else if (result == "Data Update Successfully") {
                    alert(scope.getSingleMsg('S0003'));
                    $window.location.reload();
                }
                 //alert(result);
                // $window.location = "";
                var Zone = T74052Service.getZone(scope.obj);
                Zone.then(function (results) {
                    scope.ZoneList = results;
                });

            });
           
        }
        scope.Delete_Click = function() {
                var deleteUser = $window.confirm('Are you absolutely sure you want to delete?');
                if (deleteUser) {
                    var delet = T74052Service.Delete(scope.obj.T_ZONE_CODE);
                    delet.then(function (msg) {
                        if (msg == "Data Delete Successfully") {
                            alert(scope.getSingleMsg('S0007'));
                            $window.location.reload();
                        }
                        //alert(msg);
                       
                    });
                } else {
                    $window.location.reload();
                }
        }

        scope.Clear_Click = function() {
            $window.location = "";
        }
        scope.setClickedRow = function (index, Z) {
            
            scope.selectedRow = index;
            scope.obj.T_ZONE_CODE = Z.T_ZONE_CODE;
            scope.obj.T_SL_NO = Z.T_SL_NO;
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