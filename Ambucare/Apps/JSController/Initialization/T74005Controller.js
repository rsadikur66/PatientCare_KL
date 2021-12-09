
app.controller("T74005Controller", ["$scope", "$filter", "$http", "$window", "T74005Service", "Data","LabelService",
    function (scope, $filter, $http, $window, T74005Service, Data, LabelService) {
       
        scope.obj = {};
        scope.obj = Data;
        //scope.Emp = {};

        //For Entry User
        var EntryUser = T74005Service.EntryUser();
        EntryUser.then(function (data) {
            
            scope.obj.T_ENTRY_USER = data;
            scope.obj.T_ENTRY_DATE = $filter('date')(new Date(), 'dd-MMM-yyyy');
        });
        //============
        scope.Save_Click = function() {

            if (scope.obj.T_LANG1_NAME, scope.obj.T_LANG2_NAME) {

                var save = T74005Service.SaveData(scope.obj);
                save.then(function(msg) {
                    if (msg == "Data Save Successfully") {
                        alert(scope.getSingleMsg('S0012'));
                    } else if (msg == "Data Update Successfully") {
                        alert(scope.getSingleMsg('S0003'));
                    }
                    //alert(msg);
                    $window.location = "";
                    //for grid Load
                    var gridLoad = T74005Service.getGridData();
                    gridLoad.then(function(data) {
                        scope.EmployeeList = data;
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

       var gridLoad = T74005Service.getGridData();
        gridLoad.then(function(data) {
            scope.EmployeeList = data;
       });

        scope.Clear_Click = function() {
            $window.location = "";
        };

        scope.Delete_Click = function() {


            var deleteUser = $window.confirm('Are you absolutely sure you want to delete?');
            if (deleteUser) {
                var delet = T74005Service.Delete(scope.obj.T_EMP_TYP_ID);
                delet.then(function (msg) {
                    if (msg =="Data Delete Successfully") {
                        alert(scope.getSingleMsg('S0007'));
                    }

                    //alert(msg);
                    //scope.Emp.T_LANG1_NAME = "";
                    $window.location = "";
                    //for grid Load
                    var gridLoad = T74005Service.getGridData();
                    gridLoad.then(function(data) {
                        scope.EmployeeList = data;
                    });

                });
            } else {

                // scope.Emp.T_LANG1_NAME = "";
                $window.location = "";
            }

        };

        // Form Label Data Service Start 
        var labelData = LabelService.getlabeldata('T74005');
        labelData.then(function (data) {

            scope.entity = data;
        });
        // Form Label Data Service End 

        //For picking up data into text
        scope.setClickedRow = function (index, Z) {

            scope.selectedRow = index;
            scope.obj.T_EMP_TYP_ID = Z.T_EMP_TYP_ID;
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