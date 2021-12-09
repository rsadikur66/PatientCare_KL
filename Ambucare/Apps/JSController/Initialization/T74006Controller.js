

app.controller("T74006Controller", ["$scope", "$filter", "$http", "$window", "T74006Service", "Data","LabelService",
    function (scope, $filter, $http, $window, T74006Service, Data, LabelService) {
        scope.obj = {};
        scope.obj = Data;
       // scope.EmpDes = {};

        //For Entry User
        var EntryUser = T74006Service.EntryUser();
        EntryUser.then(function (data) {
            scope.obj.T_ENTRY_USER = data;
            scope.obj.T_ENTRY_DATE = $filter('date')(new Date(), 'dd-MMM-yyyy');
        });


        scope.Save_Click = function() {
            if (scope.obj.T_LANG1_NAME, scope.obj.T_LANG2_NAME) {
                var save = T74006Service.SaveData(scope.obj);
                save.then(function(msg) {
                    if (msg == "Data Save Successfully") {
                        alert(scope.getSingleMsg('S0012'));
                    } else if (msg == "Data Update Successfully") {
                        alert(scope.getSingleMsg('S0003'));
                    }
                    //alert(msg);

                    var gridLoad = T74006Service.getGridData();
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

        scope.Clear_Click = function() {
            $window.location = "";
        }

        scope.Delete_Click = function () {
            
            var deleteUser = $window.confirm('Are you absolutely sure you want to delete?');

            if (deleteUser) {
              
                var delet = T74006Service.Delete(scope.obj.T_EMP_DESI_ID);
                delet.then(function (msg) {
                    if (msg == "Data Delete Successfully") {
                        alert(scope.getSingleMsg('S0007'));
                    }

                    //alert(msg);
                    $window.location = "";
                    // for grid load
                    var gridLoad = T74006Service.getGridData();
                    gridLoad.then(function (data) {
                        scope.EmpDesList = data;
                    });
                });
            }
           
        }

        // Form Label Data Service Start 
        var labelData = LabelService.getlabeldata('T74006');
        labelData.then(function (data) {

            scope.entity = data;
        });
        // Form Label Data Service End 

        var gridLoad = T74006Service.getGridData();
        gridLoad.then(function (data) {
            scope.EmpDesList = data;
        });

        //For picking up data into text
        scope.setClickedRow = function (index, Z) {

            scope.selectedRow = index;
            scope.obj.T_EMP_DESI_ID = Z.T_EMP_DESI_ID;
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