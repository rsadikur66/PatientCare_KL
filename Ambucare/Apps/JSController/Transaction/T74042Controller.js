
app.controller("T74042Controller", ["$scope", "$window", "$location", "T74042Service", "NgMap", "Data", "$filter","LabelService",
    function ($scope, $window, $location, T74042Service, NgMap, Data, $filter, LabelService) {
            $scope.obj = {};
            $scope.obj = Data;
            $scope.obj.G = {};
            $scope.obj.k = {};
            $scope.cx = {};
            $scope.obj.H = {};
        var labelData = LabelService.getlabeldata('T74041');
        labelData.then(function (data) {
            $scope.entity = data;
        });
        // ----- Ruhul Amin start---------------
        //for getting Id from query sting start

        var url = $location.absUrl();
        var reqid = url.split("=").pop();//NaN
        $scope.requestId = parseInt(reqid);
        if (reqid !== url) {
            $scope.requestId = parseInt(reqid);
           
            var places = T74042Service.getAllHospitalLatlong();
            places.then(function (data) {
                $scope.HospitalList = data; //JSON.parse(data);
               
                patientInformation();

            });

        } else {
            $scope.isAmbulance = true;
        }
        $scope.onHospitalSearch = function (lat, long, sitecode) {
            $scope.Hospital = { latitude: lat, longitude: long };
            $scope.latHos = lat;
            $scope.lngHos = long;
            $scope.$apply();
        };

        function patientInformation() {
            var getPatIn = T74042Service.getPatientInformation($scope.requestId);
            getPatIn.then(function (data) {
                $scope.obj.t74041.T_REQUEST_ID = data[0].T_REQUEST_ID;
                $scope.obj.t74046.T_FIRST_LANG2_NAME = data[0].T_FIRST_LANG2_NAME;
                $scope.obj.t74046.T_FATHER_LANG2_NAME = data[0].T_FATHER_LANG2_NAME;
                $scope.obj.t74046.T_GFATHER_LANG2_NAME = data[0].T_GFATHER_LANG2_NAME;
                $scope.obj.t74046.T_MOBILE_NO = data[0].T_MOBILE_NO;
                $scope.obj.t74046.T_NATIONAL_ID = data[0].T_NATIONAL_ID;
                $scope.obj.t74046.T_ALT_MOBILE_NO = data[0].T_ALT_MOBILE_NO;
                $scope.obj.t74041.T_AGE = data[0].T_AGE;
                $scope.obj.t74041.T_PROBLEM = data[0].T_PROBLEM;
                $scope.obj.t74041.T_PROBLEM_DURATION = data[0].T_PROBLEM_DURATION;

                $scope.obj.T_SITE_DIS_CODE = data[0].T_SITE_DIS_CODE;
                $scope.obj.T_OPER_DES_CODE = data[0].T_OPER_DES_CODE;
                // $scope.autocomplete = data[0].T_MAP_LOC;

                $scope.obj.G.selected = { T_LANG2_NAME: data[0].GENDER, T_SEX_CODE: data[0].T_SEX_CODE };
                $scope.obj.H.selected = { T_LANG2_NAME: data[0].HOSPITAL_NAME, T_SITE_CODE: data[0].T_SITE_DIS_CODE };

                //$scope.latAmbu = data[0].latitude;
                //$scope.lngAmbu = data[0].longitude;
                $scope.cx.latitude = data[0].latitude;
                $scope.cx.longitude = data[0].longitude;
                $scope.lat = data[0].HOS_T_LATITUDE; //bind with autosearch
                $scope.lng = data[0].HOS_T_LONGITUDE; //bind with autosearch

                //$scope.Hospital = { latitude: data[0].HOS_T_LATITUDE, longitude: data[0].HOS_T_LONGITUDE };
                $scope.latHos = data[0].HOS_T_LATITUDE;
                $scope.lngHos = data[0].HOS_T_LONGITUDE;

                //$scope.latHos = lat;
                //$scope.lngHos = long;
                var latlng = new google.maps.LatLng(data[0].latitude, data[0].longitude);
                var geocoder = geocoder = new google.maps.Geocoder();
                geocoder.geocode({ 'latLng': latlng }, function (results, status) {
                    if (status == google.maps.GeocoderStatus.OK) {
                        if (results[0]) {
                            $scope.autocomplete = results[0].formatted_address;
                            $scope.lat = data[0].latitude;
                            $scope.lng = data[0].longitude;

                        }
                    }
                });
                $scope.$apply();

            });

        }
        // ----- Ruhul Amin end ---------------
        //-----------------------Previous One---------------Start-------
        //$scope.Save = function() {
        //    if ($scope.obj.T_SITE_DIS_CODE !== null &&$scope.obj.T_SITE_DIS_CODE == $scope.obj.T_OPER_DES_CODE) {
        //        //alert('Ambulance has received your previous request');
        //        alert($scope.getSingleMsg('S0017'));
        //        $window.location.href = $scope.vrDir+'/Queries/Q74136';
        //    } else {
        //        var doc = $scope.obj.t74041.T_OPER_DES_CODE;
        //        var operator = T74042Service.updateByOperator($scope.requestId, doc);
        //        operator.then(function (data) {
        //            //alert('Save successfully');
        //            alert($scope.getSingleMsg('S0002'));
        //            var a = $scope.getLatLong();
        //            var t74026 = {};
        //            t74026.T_REQUEST_ID = $scope.requestId;
        //            t74026.T_EVENT_ID = 10;
        //            t74026.T_LATITUDE = a.lt;
        //            t74026.T_LONGITUDE = a.ln;
        //            T74042Service.save26(t74026);
        //            $window.location.href = $scope.vrDir+'/Queries/Q74136';
        //        });
        //    }

        //};
        //-----------------------Previous One---------------End-------
        $scope.Save = function () {
            //if ($scope.obj.T_SITE_DIS_CODE !== null && $scope.obj.T_SITE_DIS_CODE == $scope.obj.T_OPER_DES_CODE) {
            //    //alert('Ambulance has received your previous request');
            //    alert($scope.getSingleMsg('S0017'));
            //    $window.location.href = $scope.vrDir + '/Queries/Q74136';
            //} else {
                
            //}
          
            var doc = $scope.obj.t74041.T_OPER_DES_CODE;
            var chkReqHos = T74042Service.chkReqHos($scope.requestId);
                chkReqHos.then(function (data) {
                    var dt = JSON.parse(data);
                    if (dt == '') {
                        var hospital = T74042Service.getPatientInformation($scope.requestId);
                        hospital.then(function (data1) {
                            $scope.HosCode = data1[0].T_SITE_DIS_CODE;
                            if ($scope.HosCode !== doc) {
                                var operator = T74042Service.updateByOperator($scope.requestId, doc);
                                operator.then(function (data) {
                                    //alert('Save successfully');
                                    alert($scope.getSingleMsg('S0002'));
                                    var a = $scope.getLatLong();
                                    var t74026 = {};
                                    t74026.T_REQUEST_ID = $scope.requestId;
                                    t74026.T_EVENT_ID = 10;
                                    t74026.T_LATITUDE = a.lt;
                                    t74026.T_LONGITUDE = a.ln;
                                    T74042Service.save26(t74026);
                                    $window.location.href = $scope.vrDir + '/Queries/Q74136';
                                });
                            } else {
                                //alert();
                                alert($scope.getSingleMsg('S0140'));
                            }
                        });
                        
                    } else {
                        alert(dt);
                    }

                });
           

        };
        //-------- Ruhul Amin -----for GPS start-------------
        var error = '';
        function getLocation() {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(function (p) {
                    $scope.$evalAsync(function() {
                        $scope.lat = p.coords.latitude;
                        $scope.lan = p.coords.longitude;
                    });
                });
               
            }
           // return { l: $scope.lat, lo: $scope.lan};
            //navigator.geolocation.getCurrentPosition(showPosition);
            //function showPosition(position) {
            //    $scope.latitude = position.coords.latitude;
            //    $scope.longitude = position.coords.longitude;
            //}
            
        }
       
        ////setInterval(function () {
        ////    getLocation();
            

        ////}, 5000); //60000 second == 1 minute

        //----------------------- get Address start------not checked by me-------------------------------
      //var  Geocoder geocoder;
      //  List < Address > addresses;
      //var  geocoder = new Geocoder(this, Locale.getDefault());

      //var  addresses = geocoder.getFromLocation(latitude, longitude, 1); // Here 1 represent max location result to returned, by documents it recommended 1 to 5

      //  var address = addresses.get(0).getAddressLine(0); // If any additional address line present than only, check with max available address lines by getMaxAddressLineIndex()
      //  var city = addresses.get(0).getLocality();
      //  var state = addresses.get(0).getAdminArea();
      //  var country = addresses.get(0).getCountryName();
      //  var postalCode = addresses.get(0).getPostalCode();
      //  var knownName = addresses.get(0).getFeatureName(); // Only if available else return NULL

        //----------------------get Address end---------------------------------------

        //-------- Ruhul Amin -----for GPS end-------------

    }
]);
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
});