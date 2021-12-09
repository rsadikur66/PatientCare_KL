
app.controller("T74053Controller", ["$scope", "$filter", "$http", "$window", "T74053Service", "Data","LabelService",
    function (scope, $filter, $http, $window, T74053Service, Data, LabelService) {
       // scope.Hospital = {};
        scope.obj = {};
        scope.obj = Data;

        //For Entry User
        var EntryUser = T74053Service.EntryUser();
        EntryUser.then(function (data) {
            scope.obj.T_ENTRY_USER = data;
            scope.obj.T_ENTRY_DATE = $filter('date')(new Date(), 'dd-MMM-yyyy');
        });
        //============


        var zoneCode1 = T74053Service.getZoneCode();
        zoneCode1.then(function (data) {
            scope.ZoneCode = data;
        });

        var getingHospitalData = T74053Service.getHospitalData();
        getingHospitalData.then(function (data) {
           
            scope.HospitalList = data;
        });
        scope.Hospital = {
            T_SITE_STATUS: 2,
            T_PRIVATE_FLAG :2
        }

        // Form Label Data Service Start 
        var labelData = LabelService.getlabeldata('T74053');
        labelData.then(function (data) {

            scope.entity = data;
        });
        // Form Label Data Service End 

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
            T74053Service.saveData(scope.obj).then(function (result) {
                if (result == "Data Save Successfully") {
                    alert(scope.getSingleMsg('S001'));
                    $window.location.reload();
                }
                else if (result == "Data Update Successfully") {
                    alert(scope.getSingleMsg('S0003'));
                    $window.location.reload();
                }
            });
        }
        scope.Delete_Click = function() {
            var deleteUser = $window.confirm('Are you absolutely sure you want to delete?');
            if (deleteUser) {
                var delet = T74053Service.Delete(scope.obj.T_SITE_NO);
                delet.then(function (msg) {
                   // alert(msg);
                    if (msg == "Data Delete Successfully") {
                        alert(scope.getSingleMsg('S0007'));
                        $window.location.reload();
                    }
                });
            } else {

                // scope.Emp.T_LANG1_NAME = "";
                $window.location.reload();
            }
        }
        scope.Clear_Click = function() {
            $window.location = "";
        }
        //For picking up data into text
        scope.setClickedRow = function (index, Z) {

            scope.selectedRow = index;
            scope.obj.T_SITE_NO = Z.T_SITE_NO;
            scope.obj.T_SITE_CODE = Z.T_SITE_CODE;
            scope.obj.T_LANG1_NAME = Z.T_LANG1_NAME;
            scope.obj.T_LANG2_NAME = Z.T_LANG2_NAME;
            scope.obj.T_IMPLEMENTATION_YEAR = Z.T_IMPLEMENTATION_YEAR;
            scope.obj.T_LATITUDE = Z.T_LATITUDE;
            scope.obj.T_SITE_STATUS = Z.T_SITE_STATUS;

            scope.obj.T_ZONE_CODE = Z.T_ZONE_CODE;
            scope.obj.T_LONGITUDE = Z.T_LONGITUDE;
            scope.obj.T_CONTACT_NAME = Z.T_CONTACT_NAME;
            scope.obj.T_CONTACT_MOBILE = Z.T_CONTACT_MOBILE;
            scope.obj.T_SITE_URL = Z.T_SITE_URL;
            scope.obj.T_PRIVATE_FLAG = Z.T_PRIVATE_FLAG;

        }
                // for selecting index
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