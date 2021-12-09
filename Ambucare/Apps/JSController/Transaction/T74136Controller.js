app.controller("T74136Controller", ["$scope", "$filter", "$window", "Data", "$rootScope", "T74136Service", "T74041Service", "LabelService",
    function ($scope, $filter, $window, Data, $rootScope, T74136Service, T74041Service, LabelService) {
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.T74057 = {};
        $scope.obj.T74136 = {};
        $scope.obj.UserType = 1;
        $scope.autocomplete = '';
        disableDDL();
        GridLoad();

        //var ambUserId = T74136Service.maxUserId();
        //ambUserId.then(function (data) {
        //    var dt = JSON.parse(data);
        //    $scope.obj.t74057.T_USER_ID = dt;
        //});

        // Form Label Data Service Start 
        var labelData = LabelService.getlabeldata('T74136');
        labelData.then(function (data) {
            $scope.entity = data;
        });

        var roleList = T74136Service.getRole();
        roleList.then(function (data) {
            var dt = JSON.parse(data);
            $scope.obj.roleList = dt;
        });
        var zoneList = T74136Service.GetZone();
        zoneList.then(function (data) {
            var dt = JSON.parse(data);
            $scope.obj.zoneList = dt;
        });
        function disableDDL() {
            $scope.obj.z = {};
            $scope.obj.z.selected = { T_REGION_CODE: '', T_LANG2_NAME: 'Select' };
            $scope.obj.s = {};
            $scope.obj.s.selected = { T_SITE_CODE: '', T_LANG2_NAME: 'Select' };
        }


        $scope.clearHosList = function () {
            disableDDL();
        };

        $scope.showHosList = function () {
            var zoneList = T74136Service.GetZone();
            zoneList.then(function (dt) {
                $scope.obj.zoneList = dt.data;
            });
        };
        $scope.obj.SiteList = function (u) {
            var siteList = T74136Service.GetSite(u.T_REGION_CODE);
            siteList.then(function (dt) {
                $scope.obj.siteList = dt.data;
            });
        };

        $scope.getUserList = function () {
            var empList = T74136Service.getEmpList();
            empList.then(function (data) {
                var dt = JSON.parse(data);
                $scope.obj.empList = dt;
                document.getElementById('divEmp').style.display = 'block';
            });
        };
        $scope.obj.CloseUserPopup = function () {
            document.getElementById('divEmp').style.display = 'none';
        };
        $scope.obj.onUserSelect = function (index, k) {
            $scope.obj.T74057.EMP_NAME = k.EMP_NAME;
            maxValue(k.TYPE_ID, k.T_SHORT_NAME);
            
            $scope.obj.T74057.T_EMP_ID = k.T_EMP_ID;
            $scope.obj.T74057.T_COMPANY_CODE = k.T_COMPANY_CODE;
            document.getElementById('divEmp').style.display = 'none';
        };

        function maxValue(type,shortName) {
            var mx = T74136Service.getMaxValue(type);
            mx.then(function (ma) {
                var b = JSON.parse(ma)[0].MAX_1;
                $scope.obj.T74057.T_USER_ID = shortName + b;
            });
        }
        //For Grid Load
        function GridLoad() {
            var gridLoad = T74136Service.GetGridData();
            gridLoad.then(function (data) {
                $scope.EmployeeList = JSON.parse(data);
            });
        }

        function Clear() {
            $scope.obj.T74057.EMP_NAME = '';
            $scope.obj.T74057.T_USER_ID = '';
            $scope.obj.T74057.T_PWD = '';
            $scope.obj.B = {};
            $scope.obj.R = {};
            $scope.obj.T74057.T_ACTIVE_FLAG = '';
        };

        //$scope.Confirm_Pass = function() {
        //    var pas = $scope.obj.T74057.T_PWD;
        //    var confPass = $scope.obj.T74057.ConfirmPassword;
        //    if (pas === confPass) {
        //        alert('ok');
        //    } else {
        //        alert('Make sure Confirm Password');
        //    }
        //}

        var disable = $rootScope.$on('T74136Emit', function (event, data) {
            if (data == 'aut') {

                var pas = $scope.obj.T74057.T_PWD;
                var confPass = $scope.obj.T74057.ConfirmPassword;
                if (pas === confPass) {
                    Save_Click();
                } else {
                    alert('Make sure Confirm Password');
                    return;
                }

            } else if (data == 'Clear') {
                Clear();
            }
        });

        $scope.$on('$destroy', function () {
            disable();
        });
        function Save_Click() {
            var test = $scope.obj.T74057.T_USER_ID;
            $scope.obj.T74057.T_USER_ID = test.toUpperCase();
            var save = T74136Service.saveUser($scope.obj.T74057);
            save.then(function (msg) {
                if (msg == '1') {
                    alert($scope.getSingleMsg('S0012'));
                } else if (msg == '2') {
                    alert($scope.getSingleMsg('S0003'));
                }


                GridLoad();
                $scope.obj.T74057 = {};
                $scope.obj.R.selected = {};
                $scope.obj.B.selected = {};

            });
        }

        //$scope.userIdCheck = function () {
        //    var a = $scope.obj.T74057.T_USER_ID.toUpperCase();
        //    if (a != '') {
        //        var userid = T74136Service.CheckUserId(a);
        //        userid.then(function (data) {
        //            var jsondata = JSON.parse(data);
        //            if (jsondata.length != 0) {
        //                if (a == jsondata[0].T_USER_ID) {
        //                    //alert('This Id Already Insert');
        //                    alert($scope.getSingleMsg('S0037'));
        //                    $scope.obj.t74057.T_USER_ID = '';
        //                }
        //            }

        //        });
        //    }

        //};
        $scope.setClickedRow = function (index, Z) {

            $scope.selectedRow = index;
            $scope.obj.T74057.T_USER_ID = Z.T_USER_ID;
            $scope.obj.T74057.T_EMP_ID = Z.T_EMP_ID;
            $scope.obj.T74057.EMP_NAME = Z.EMP_NAME;
            $scope.obj.T74057.T_PWD = Z.T_PWD;
            $scope.obj.T74057.ConfirmPassword = Z.T_PWD;
            $scope.obj.T74057.T_ACTIVE_FLAG = Z.T_ACTIVE_FLAG;
            $scope.obj.T74057.T_COMPANY_ID = Z.T_COMPANY_ID;
            $scope.obj.R.selected = { NAME: Z.ROLE_NAME, T_ROLE_CODE: Z.T_ROLE_CODE };
            $scope.obj.B.selected = { T_ZONE_NAME: Z.T_ZONE_NAME, T_ZONE_CODE: Z.T_ZONE_CODE };
            $scope.lat = Z.T_LATITUDE;
            $scope.lng = Z.T_LONGITUDE;

            var latlng = new google.maps.LatLng($scope.lat, $scope.lng);
            var geocoder = geocoder = new google.maps.Geocoder();
            geocoder.geocode({ 'latLng': latlng },
                function (results, status) {
                    if (status == google.maps.GeocoderStatus.OK) {
                        if (results[0]) {
                            //$scope.obj.t74041.T_MAP_LOC = results[0].formatted_address;
                            $scope.obj.autocomplete = results[0].formatted_address;
                            // alert("Location: " + results[1].formatted_address);
                        }
                    }
                });

            //var location = $scope.obj.autocomplete.getPlace().geometry.location;
            //$scope.lat = location.lat();
            //$scope.lng = location.lng();
            $scope.cx.latitude = $scope.lat;
            $scope.cx.longitude = $scope.lng;
            $scope.obj.DispDir = true;
            $scope.obj.DispMrkr = true;
            $scope.MrkrImg = 'PatientLoc';
            $scope.$apply();
            document.getElementById('txtUserID').readOnly = true;
        };
        $scope.$watch('selectedRow', function () {
            console.log('Do Some processing'); //runs the block whenever selectedRow is changed. 
        });


        var cx = T74041Service.GetLoginUserLatlong();
        cx.then(function (data) {
            $scope.cx = { latitude: JSON.parse(data)[0].latitude, longitude: JSON.parse(data)[0].longitude };
        });

        $scope.$on('gmPlacesAutocomplete::placeChanged', function () {
            var location = $scope.obj.autocomplete.getPlace().geometry.location;
            $scope.lat = location.lat();
            $scope.lng = location.lng();
            $scope.cx.latitude = location.lat();
            $scope.cx.longitude = location.lng();
            $scope.obj.DispDir = true;
            $scope.obj.DispMrkr = true;
            $scope.MrkrImg = 'PatientLoc';
            $scope.$apply();
            //console.log($scope.obj.t74136.T_LATITUDE, $scope.obj.t74136.T_LONGITUDE);
            //-----------------Ruhul Amin-----------------
            //  function GetAddress() {
            //var lat = $scope.lat;
            // var lng =  $scope.lng;
            ////sadik
            //var latlng = new google.maps.LatLng($scope.lat, $scope.lng);
            //var geocoder = geocoder = new google.maps.Geocoder();
            //geocoder.geocode({ 'latLng': latlng }, function (results, status) {
            //    if (status == google.maps.GeocoderStatus.OK) {
            //        if (results[0]) {
            //            $scope.obj.t74041.T_MAP_LOC = results[0].formatted_address;
            //            // $scope.obj.t74041.T_MAP_LOC = results[1].formatted_;
            //            // alert("Location: " + results[1].formatted_address);
            //        }
            //    }
            //});
            ////sadik
            // }
            //-----------------------------------

        });
        $scope.getPos = function (event) {
            $scope.lat = event.latLng.lat();
            $scope.lng = event.latLng.lng();
            $scope.cx.latitude = event.latLng.lat();
            $scope.cx.longitude = event.latLng.lng();
            $scope.obj.DispDir = true;
            $scope.obj.DispMrkr = true;
            //ambulatlong();
            $scope.$apply();

            //-----------------Ruhul Amin-----------------
            //  function GetAddress() {
            //var lat = $scope.lat;
            // var lng =  $scope.lng;
            var latlng = new google.maps.LatLng($scope.lat, $scope.lng);
            var geocoder = geocoder = new google.maps.Geocoder();
            geocoder.geocode({ 'latLng': latlng },
                function (results, status) {
                    if (status == google.maps.GeocoderStatus.OK) {
                        if (results[0]) {
                            //$scope.obj.t74041.T_MAP_LOC = results[0].formatted_address;
                            $scope.obj.autocomplete = results[0].formatted_address;
                            // alert("Location: " + results[1].formatted_address);
                        }
                    }
                });
        };
    }]);







