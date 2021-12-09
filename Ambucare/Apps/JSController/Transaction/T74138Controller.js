app.controller("T74138Controller", ["$scope", "$filter", "$http", "$window", "T74138Service", "LabelService", "Data", "$rootScope",
    function ($scope, $filter, $http, $window, T74138Service, LabelService, Data, $rootScope) {
        //For Instance
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.G = {};
        $scope.obj.k = {};
        $scope.obj.k.selected = {};
        //document.getElementById('txtPat_NationalId').focus();
        //$scope.onExit = function () {
        //    alert('sddd');
        //}
        
        var labelData = LabelService.getlabeldata('T74041');
        labelData.then(function (data) {
            $scope.entity = data;
        });
        var cx = T74138Service.GetLoginUserLatlong();
        cx.then(function (data) {
            var dt = JSON.parse(data);
            $scope.obj.T_TEAM_ARVL_HOS = dt[0].T_TEAM_ARVL_HOS;
            $scope.obj.T_TEAM_DEPT_HOS = dt[0].T_TEAM_DEPT_HOS;
            $scope.obj.T_TEAM_ARVL_STN = dt[0].T_TEAM_ARVL_STN;
            $scope.obj.T_RDY_FOR_NXT_PAT = dt[0].T_RDY_FOR_NXT_PAT;
            $scope.obj.T_REQUEST_ID = dt[0].T_REQUEST_ID;
            
            $scope.cx = { latitude: dt[0].latitude, longitude: dt[0].longitude };
        });
        var places = T74138Service.GetAllUserLatlong();
        places.then(function (data) {
            $scope.places = JSON.parse(data);
            onGoDestination();
        });


        var customEvent38 = $rootScope.$on('T74138Emit', function(event, data) {
            if (data == 'h') {
                $scope.obj = {};
                $scope.obj = Data;
            }
            if (data==='H') {
                var places = T74138Service.GetAllUserLatlong();
                places.then(function (data) {
                    $scope.places = JSON.parse(data);
                    onGoDestination();
                });

            }
            
        });
        $scope.$on('$destroy', function () {
            customEvent38();
        });

        
        //$scope.hideMarker = function () {
        //    //alert(id);
        //    //this.hide();
        //    //document.getElementById(id).style.display.hide();
        //    //visibility: hidden
        //    //document.getElementById('temp_Marker').style.display = "none";
        //    //document.getElementById('txtsearchAmb').style.display = "none";
        //}

        $scope.onAmbSearch = function (lat, long, sitecode) {
            $scope.testLat = { latitude: lat, longitude: long };
            $scope.obj.t74041.T_SITE_CODE = sitecode;
            $scope.$apply();


            //var city = { latitude: lat, longitude: long }
            //$scope.new = city;
            //$scope.cx.latitude = city.latitude;
            //$scope.cx.longitude = city.longitude;
        };

        function onGoDestination() {
            var cx = T74138Service.getDestination(2);
            cx.then(function (data) {
                var docHos = T74138Service.getDocSite();
                docHos.then(function (dSite) {
                    var dtJson = JSON.parse(dSite);
                    $scope.cx = { latitude: JSON.parse(data)[0].latitude, longitude: JSON.parse(data)[0].longitude };
                    //var location = $scope.autocomplete.getPlace().geometry.location;
                    $scope.lat = JSON.parse(data)[0].latitude;
                    $scope.lng = JSON.parse(data)[0].longitude;
                    $scope.cx.latitude = $scope.lat;
                    $scope.cx.longitude = $scope.lng;
                    var temp = [];
                    for (var i = 0; i < $scope.places.length; i++) {
                        var t = {};
                        t.T_SITE_CODE = $scope.places[i].T_SITE_CODE;
                        t.latitude = $scope.places[i].latitude;
                        t.longitude = $scope.places[i].longitude;
                        t.distance = getDistanceFromLatLonInKm($scope.lat, $scope.lng, t.latitude, t.longitude);
                        temp.push(t);
                    }
                    var sing = temp[0].distance;
                    var st = '';
                    for (var j = 0; j < temp.length; j++) {
                        if (sing > temp[j].distance) {
                            st = temp[j].T_SITE_CODE;
                        }
                    }
                    var p = temp.filter(function (ar) {
                        return ar.T_SITE_CODE == st;
                    });
                    if (dtJson != '') {
                        var d = $scope.places.filter(function (ak) {
                            return ak.T_SITE_CODE == dtJson;
                        });
                        $scope.testLat = { latitude: d[0].latitude, longitude: d[0].longitude }
                        $scope.obj.k.selected = { T_LANG2_NAME: d[0].T_LANG2_NAME, T_SITE_CODE: d[0].T_SITE_CODE }  
                    } else {
                        $scope.testLat = { latitude: p[0].latitude, longitude: p[0].longitude }
                        var l = $scope.places.filter(function (ak) {
                            return ak.T_SITE_CODE == st;
                        });
                        $scope.obj.k.selected = { T_LANG2_NAME: l[0].T_LANG2_NAME, T_SITE_CODE: l[0].T_SITE_CODE }        
                    }
                    $scope.$apply();

                        
                        
                      
                    
                   
                });
                //navigator.geolocation.getCurrentPosition(showPosition);

                $scope.onIconClick = function (e) {
                    var conf = false;
                    if (e=='ah') {
                        conf = confirm("Have you reached at Hospital?");
                    }
                    else if (e == 'dh') {
                        conf = confirm("Do you want to depart From hospital?");
                    }
                    else if (e == 'as') {
                        conf = confirm("Have you reached at Station?");
                    }
                    else if (e == 'rd') {
                        conf = confirm("Are you ready for Attending next Case?");
                    }
                    if (conf == true) {
                        $scope.obj.lt = 23.7582849;
                        $scope.obj.ln = 90.39022219999993;
                        if ($scope.obj.lt != null && $scope.obj.ln != null) {
                            var saveEvent = T74138Service.saveEvent(e, $scope.obj.T_REQUEST_ID, $scope.obj.lt, $scope.obj.ln);
                            saveEvent.then(function (re) {
                                var k = re.slice(1, 2);
                              
                                if (re ==='"ns"') {
                                    alert('Please discharge patient');
                                    return;
                                } else if (k=="1") {
                                    alert(re.split('^')[1]);
                                }
                                else {
                                var as = T74138Service.GetLoginUserLatlong();
                                as.then(function (data) {
                                    var dt = JSON.parse(data);
                                    if (dt.length>0) {
                                        $scope.obj.T_TEAM_ARVL_HOS = dt[0].T_TEAM_ARVL_HOS;
                                        $scope.obj.T_TEAM_DEPT_HOS = dt[0].T_TEAM_DEPT_HOS;
                                        $scope.obj.T_TEAM_ARVL_STN = dt[0].T_TEAM_ARVL_STN;
                                        $scope.obj.T_RDY_FOR_NXT_PAT = dt[0].T_RDY_FOR_NXT_PAT;
                                        $scope.obj.T_REQUEST_ID = dt[0].T_REQUEST_ID;
                                    }
                                 //   else {
                                        if (e == 'rd') {
                                            $window.location.reload();
                                        }   
                                  //  }
                                });
                              }

                            });
                        } else {
                            //alert('Location cannot be accesed. Please enable your location from browser');
                            alert($scope.getSingleMsg('S0038'));
                        }
                    }



                   


                }
                //function showPosition(position) {
                //    $scope.obj.lt = position.coords.latitude;
                //    $scope.obj.ln = position.coords.longitude;

                //}

               





               
            });

        }
        //$scope.onGoDestination();
        function deg2rad(deg) {
            return deg * (Math.PI / 180);
        }
        function getDistanceFromLatLonInKm(lat1, lon1, lat2, lon2) {
            var R = 6371; // Radius of the earth in km
            var dLat = deg2rad(parseFloat(lat2) - parseFloat(lat1));  // deg2rad below
            var dLon = deg2rad(parseFloat(lon2) - parseFloat(lon1));
            var a = Math.sin(dLat / 2) * Math.sin(dLat / 2) + Math.cos(deg2rad(lat1)) * Math.cos(deg2rad(lat2)) * Math.sin(dLon / 2) * Math.sin(dLon / 2);
            var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
            var d = R * c; // Distance in km
            return d;
        }
        //func getAmbulanceId(List < PairAmbuDist > ambulanceDistances)
        // {
        //     //System.out.println("########Size of List " + ambulanceDistances.size());
        //     double mn = ambulanceDistances.get(0).getT_AMBU_DIST();
        //     String ID = ambulanceDistances.get(0).getT_AMBU_ID();
        //     for(PairAmbuDist ambulance: ambulanceDistances) {
        //         if (mn > ambulance.getT_AMBU_DIST()) {
        //             ID = ambulance.getT_AMBU_ID();
        //             mn = ambulance.getT_AMBU_DIST();
        //         }
        //     }
        //     return ID;
        //return ambulanceDistances;
        //}

        //private double getDistanceFromLatLonInKm(double lat1, double lon1, double lat2, double lon2)
        //{
        //    double R = 6371; // Radius of the earth in km
        //    double dLat = deg2rad(lat2 - lat1);  // deg2rad below
        //    double dLon = deg2rad(lon2 - lon1);
        //    double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        //    double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        //    double d = R * c; // Distance in km
        //    return d;
        //}
        $scope.showCity = function (event, city) {
            $scope.testLat = city;
            $scope.obj.k.selected = { T_LANG2_NAME: city.T_LANG2_NAME };
            //$scope.obj.t74041.T_AMBU_REG_ID = city.T_AMBU_REG_ID;
            //var mapData = T74041Service.GetAllDataOnMap_T74041(city.T_AMBU_REG_ID);
            //mapData.then(function (data) {
            //    $scope.mapdata = JSON.parse(data);
            //});
            //var UserId = T74041Service.GetUserIDByAMBRegID(city.T_AMBU_REG_ID);
            //UserId.then(function (data) {
            //    $scope.obj.t74041.T_USER_ID = JSON.parse(data)[0].T_USER_ID;
            //    //sessionStorage.setItem("t74041_T_USER_ID", JSON.parse(data)[0].T_USER_ID);
            //});
            // $scope.map.showInfoWindow('myInfoWindow', this);

        };
        ////dropdown-----------------------map-------------------------------------------------start
        //$scope.latitude = undefined;
        //$scope.longitude = undefined;

        $scope.$on('gmPlacesAutocomplete::placeChanged', function () {
            var location = $scope.autocomplete.getPlace().geometry.location;
            $scope.lat = location.lat();
            $scope.lng = location.lng();
            $scope.$apply();

        });
        ////dropdown-----------------------map-------------------------------------------------end

        //var Gender = T74041Service.Gender();
        //Gender.then(function (data) {
        //    $scope.gender = data;
        //});
        //var EntryUser = T74041Service.EntryUser();
        //EntryUser.then(function (data) {
        //    $scope.obj.t74041.T_ENTRY_USER = data;
        //    $scope.obj.t74046.T_ENTRY_USER = data;
        //    $scope.obj.t74046.T_ENTRY_DATE = $filter('date')(new Date(), 'dd-MMM-yyyy');
        //});
        //var CompanyId = T74041Service.CompanyId();
        //CompanyId.then(function (data) {
        //    $scope.obj.t74041.T_COMPANY_ID = data;
        //});
        //$scope.PatInfo = function () {
        //    var url = '';
        //    var data = '';
        //    T74041PatFactory.getModal(url, data);
        //}
        //$scope.Save = function () {
        //    // $scope.t74041.T_USER_ID = sessionStorage.getItem("t74041_T_USER_ID");
        //    if ($scope.obj.t74041.T_PAT_ID == undefined) {
        //        var save_t74046 = T74041Service.Insert_t74046($scope.obj.t74046);
        //        save_t74046.then(function (data) {
        //            $scope.obj.t74041.T_PAT_ID = JSON.parse(data);
        //            var save_t74041 = T74041Service.Insert_t74041($scope.obj.t74041);
        //            save_t74041.then(function (data) {
        //                alert('Data saved sucessfully');
        //                $scope.Clear();
        //            });
        //        });
        //    } else {
        //        var save_t74041 = T74041Service.Insert_t74041($scope.obj.t74041);
        //        save_t74041.then(function (data) {
        //            alert('Data saved sucessfully');
        //            $scope.Clear();
        //        });
        //    }

        //}
        //$scope.Clear = function () {
        //    $scope.obj.t74041 = {};
        //    $scope.obj.t74046 = {};
        //    $scope.obj.G.selected = {};
        //    $scope.obj.t74041.T_PAT_ID = undefined;
        //}
        ////-------- Ruhul Amin -----for GPS start-------------
        //var error = '';
        //var lati = '';
        //var longi = '';
        //function getLocation() {
        //    if (navigator.geolocation) {
        //        navigator.geolocation.getCurrentPosition(showPosition);
        //    } else {
        //        error = "Geolocation is not supported by this browser.";
        //    }
        //}

        //function showPosition(position) {
        //    var latitude = position.coords.latitude;
        //    var longitude = position.coords.longitude;
        //    //    $scope.latitude_Last = latitude;
        //    var gps = T74041Service.GPS_Insert(latitude, longitude);
        //    gps.then(function (data) {

        //    });

        //}

        //$scope.$watch('myVar', function () {
        //    // getLocation();
        //    // alert(lati);
        //});

        ////setInterval(function () {
        ////   // getLocation();
        ////}, 60000); //60000 second == 1 minute

        ////-------- Ruhul Amin -----for GPS end-------------
        $scope.saveRequestHospital = function () {
            var a =$scope.obj.k.selected.T_SITE_CODE;
            var chkReqHos = T74138Service.chkReqHos($scope.obj.T_REQUEST_ID);
            chkReqHos.then(function (data) {
                var dt = JSON.parse(data);
                if (dt == '') {
                    var save = T74138Service.saveRequestHospitalbyTeam($scope.obj.T_REQUEST_ID, a);
                    save.then(function (data) {
                        alert(data);

                        //var a = $scope.getLatLong();
                        //var t74026 = {};
                        //t74026.T_REQUEST_ID = $scope.T_REQUEST_ID;
                        //t74026.T_EVENT_ID = 8;
                        //t74026.T_LATITUDE = a.lt;
                        //t74026.T_LONGITUDE = a.ln;
                        //T74131Service.save26(t74026);
                        ////itemDatalist($scope.obj.pInfo.T_AMBU_REG_ID);
                    });
                } else {
                    alert(dt);
                }

            });








        };
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
