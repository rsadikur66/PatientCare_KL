

app.controller("T74069Controller", ["$scope", "$filter", "$http", "$window", "T74069Service", "Data","LabelService", 
    function (scope, $filter, $http, $window, T74069Service, Data, LabelService) {

        scope.obj = {};
        scope.obj = Data;
        //scope.Emp = {};

        //For Entry User
        var EntryUser = T74069Service.EntryUser();
        EntryUser.then(function (data) {
          
            scope.obj.T_ENTRY_USER = data;
            scope.obj.T_ENTRY_DATE = $filter('date')(new Date(), 'dd-MMM-yyyy');
        });
        //============

        scope.Save_Click = function () {
            if (scope.obj.T_SHOT_DESC1 == undefined || scope.obj.T_SHOT_DESC1 == '') {
                // $window.confirm('Local Name is requered');
                alert(scope.getSingleMsg('M0002'));
                scope.localName.show();
                return;
            }
            if (scope.obj.T_SHOT_DESC2 == undefined || scope.obj.T_SHOT_DESC2 == '') {
                // $window.confirm('English Name is requered');
                alert(scope.getSingleMsg('M0003'));
                scope.localName.show();
                return;
            }
            if (scope.obj.T_BLOOD_GORUP == undefined || scope.obj.T_BLOOD_GORUP == '') {
                $window.confirm('Blood group is requered');
                scope.localName.show();
                return;
            }

            var save = T74069Service.SaveData(scope.obj);
            save.then(function(msg) {
                //alert(msg);
                if (msg == "Data Save Successfully") {
                    alert(scope.getSingleMsg('S0012'));
                    $window.location.reload();
                } else if (msg == "Data Update Successfully") {
                    alert(scope.getSingleMsg('S0003'));
                    $window.location.reload();
                }
            });
        };

        //For Grid Load
        var gridLoad = T74069Service.getGridData();
        gridLoad.then(function (data) {
            scope.BloodGroupList = data;
        });
       // ====
        scope.Clear_Click = function () {
            scope.obj.T_BOOD_GROUP_ID = "";
            scope.obj.T_SHOT_DESC1 = "";
            scope.obj.T_SHOT_DESC2 = "";
            scope.obj.T_BLOOD_GORUP = "";
        }
        //====
        scope.Delete_Click = function () {
            debugger;
            if (scope.obj.T_BLD_GROUP_ID !== undefined) {
                if ($window.confirm('Are you sure ?')) {
                    var del = T74069Service.Delete(scope.obj.T_BLD_GROUP_ID);
                    del.then(function (data) {
                        //alert("Data delete Succesfully");
                        alert(scope.getSingleMsg('S0007'));
                        $window.location.reload();
                    });
                }
                //return false;
            }
            else {
                //alert("Select a data for delete.");
                alert(scope.getSingleMsg('S0011'));
                return false;
            }
        };
         //Form Label Data Service Start 

        var labelData = LabelService.getlabeldata('T74069');
        labelData.then(function (data) {

            scope.entity = data;
        });

        // Form Label Data Service End 

        //For picking up data into text
        scope.setClickedRow = function (index, Z) {

            scope.selectedRow = index;
            scope.obj.T_BLD_GROUP_ID = Z.T_BLD_GROUP_ID;
            scope.obj.T_SHOT_DESC1 = Z.T_SHOT_DESC1;
            scope.obj.T_SHOT_DESC2 = Z.T_SHOT_DESC2;
            scope.obj.T_BLOOD_GORUP = Z.T_BLOOD_GORUP;
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
