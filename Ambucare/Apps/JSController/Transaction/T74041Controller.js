app.controller("T74041Controller", ["$scope", "$window", "$location", "T74041Service", "NgMap", "LabelService", "Data", "$filter", "T74041PatFactory", "$rootScope",
    function ($scope, $window, $location, T74041Service, NgMap, LabelService, Data, $filter, T74041PatFactory, $rootScope) {
        //-------------------------------------Sorted Code----------------Start----------
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.G = {};
        $scope.obj.R = {};
        $scope.obj.k = {};
        $scope.obj.G.selected = {};
        $scope.obj.R.selected = {};
        $scope.obj.k.selected = {};
        $scope.obj.t74041 = {};
        $scope.obj.t74046 = {};
        $scope.obj.t74120 = {};
        $scope.obj.t74026 = {};
        $scope.latitude = undefined;
        $scope.longitude = undefined;
        $scope.obj.selectValue = "1";
        $scope.kkk = false;
        $scope.obj.DispDir = true;
        $scope.obj.DispMrkr = true;
        $scope.MrkrImg = 'PatientLoc';
        $scope.obj.type = "";
        $scope.activeLen = 0;
        $scope.pendingLen = 0;
        $scope.CancelCount = 0;
        $scope.PreviousPendingLen = 0;
        document.getElementById('gLocation').focus();

        getActivePatientInfo();
        getPendingPatInfo();
        getcancelData();
        getlabel();
        getgender();
        getcallingReason();
        getEntryUser();
        getCompanyId();
        getcanReason();
        getAllUserLatLong();
        removeSessionReq();

        $scope.ddd = function (p) {
            focus(p);
        };
        $scope.ckAge = function (age) {
            if (age >= 120) {
                alert("Please Check Your Age");
            }
        }
        var places = T74041Service.GetAllUserLatlong(null, null);
        places.then(function (data) {
            $scope.places = JSON.parse(data);
            //$scope.MrkrImg = 'PatientLoc';
        });
        $scope.unknown = function () {
            try {
                //-----------------Clear------------------
                $scope.obj.t74041 = {};
                $scope.obj.t74046 = {};
                $scope.obj.G.selected = {};
                $scope.obj.t74041.T_PAT_ID = undefined;
                $scope.obj.autocomplete = '';
                $scope.obj.selectValue = "1";
                $scope.obj.k.selected = { AMB_CAPASITY: '', T_AMBU_REG_ID: '' };
                var places = T74041Service.GetAllUserLatlong(null, null);
                places.then(function (data) {
                    $scope.places = JSON.parse(data);
                    $scope.MrkrImg = 'PatientLoc';
                });
                $scope.loader(false);
                //-----------------Clear------------------
                $scope.obj.t74046.T_FIRST_LANG2_NAME = 'Unknown';
                $scope.obj.t74046.T_FATHER_LANG2_NAME = 'Unknown';
                $scope.obj.t74046.T_GFATHER_LANG2_NAME = 'Unknown';
                document.getElementById("btnSave").disabled = false;
                var span = document.getElementById("spnClass");
                span.parentNode.removeChild(span);
                var contact = document.getElementById("txtContactNo");
                contact.required = false;
                contact.classList.remove("ng-invalid");

            } catch (e) {
                $scope.setError($scope.controllerName, 'unknown', e.message);
            }
        }
        $scope.Save = function () {
            $scope.loader(true);
            if ($scope.obj.selectValue == '1') {
                var a = $scope.getLatLong();
                $scope.obj.t74026.T_REQUEST_ID = $scope.obj.t74041.T_REQUEST_ID;
                $scope.obj.t74026.T_LATITUDE = a.lt;
                $scope.obj.t74026.T_LONGITUDE = a.ln;
                $scope.obj.t74041.T_CALL_REASON_ID = $scope.obj.R.selected.T_CALL_REASON_ID;

                var t41 = $scope.obj.t74041;
                var t46 = $scope.obj.t74046;
                var t20 = $scope.obj.t74120;
                var t26 = $scope.obj.t74026;

                var saveReq = T74041Service.saveReq(t41, t46, t20, t26);
                saveReq.then(function (data) {
                    var dt = JSON.parse(data);
                    alert(dt);
                   $scope.Clear();
                   
                });
            } else {
                alert('Please select "All available ambulance"');
            }
        };
        $scope.Clear = function () {
                var reqid = $scope.obj.t74041.T_REQUEST_ID;
                T74041Service.AsignPatientFromPendinglist(reqid, 'Cancel');
                $scope.obj.type = "";
                $scope.obj.lat = null;
                $scope.obj.lng = null;
                //$scope.cx.latitude = null;
                //$scope.cx.longitude = null;

                $scope.obj.t74041 = {};
                $scope.obj.t74046 = {};
                $scope.obj.t74120 = {};
                $scope.obj.t74207 = {};
                $scope.obj.G.selected = {};
                $scope.obj.t74041.T_PAT_ID = undefined;
                $scope.obj.DispDir = false;
                $scope.obj.DispMrkr = false;
                $scope.obj.autocomplete = '';
                $scope.obj.selectValue = "1";
                $scope.obj.R.selected = '';
                getCompanyId();
                $scope.obj.k.selected = { AMB_CAPASITY: '', T_AMBU_REG_ID: '' };
                var places = T74041Service.GetAllUserLatlong(null, null);
                places.then(function (data) {
                    $scope.places = JSON.parse(data);
                    $scope.MrkrImg = 'PatientLoc';
                });
                $scope.loader(false);
        }
        $scope.ActivePatientInfo = function () {
            document.getElementById("ShowPendingPopup").style.display = "none";
            document.getElementById("ShowCancelPopup").style.display = "none";
            var actvPatList = T74041Service.GetActivePatientsData();
            actvPatList.then(function (data) {
                var dt = JSON.parse(data);
                $scope.obj.ActivePatiens = dt;
                document.getElementById("ShowActivePatientPopup").style.display = "block";
            });

        }
        $scope.SaveRemarks = function (req, rem) {
            var SaveRemarks = T74041Service.SaveRemarks(req, rem);
            SaveRemarks.then(function (data) {
                var dt = JSON.parse(data);
                alert(dt);
            });
        }
        $scope.onAmbSearch = function (lat, long, ambulanceId) {
            //------- ruhul------
            $scope.obj.DispDir = true;
            $scope.obj.DispMrkr = true;
            //--------------------------
            $scope.testLat = { latitude: lat, longitude: long };
            $scope.obj.t74041.T_AMBU_REG_ID = ambulanceId;
            $scope.obj.t74041.T_USER_ID = $scope.obj.k.selected.AMB_CAPASITY;
            //var UserId = T74041Service.GetUserIDByAMBRegID($scope.obj.t74041.T_AMBU_REG_ID);
            //UserId.then(function (data) {
            //    $scope.obj.t74041.T_USER_ID = JSON.parse(data)[0].T_USER_ID;
            //});
            $scope.$apply();

            var city = { latitude: lat, longitude: long }
            $scope.new = city;
            $scope.cx.latitude = city.latitude;
            $scope.cx.longitude = city.longitude;

            document.getElementById("txtContactNo").focus();
        };
        $scope.showCity = function (event, city, index, type, allObj) {
            //------- ruhul------
            $scope.obj.DispDir = true;
            $scope.obj.DispMrkr = true;
            //--------------------------
            $scope.testLat = city;
            $scope.obj.t74041.T_AMBU_REG_ID = city.T_AMBU_REG_ID;
            $scope.obj.k.selected = { AMB_CAPASITY: city.AMB_CAPASITY };
            var mapData = T74041Service.GetAllDataOnMap_T74041(city.T_AMBU_REG_ID);
            mapData.then(function (data) {
                $scope.mapdata = JSON.parse(data);
                var t = $scope.mapdata.filter(function (i) { return (i.T_EMP_TYP_ID == '101010'); });
                var indx = $scope.mapdata.findIndex(x => x.T_EMP_TYP_ID == "101010");
                if (t.lenght > 0) {
                    if (t[0].MOBILE == null) {
                        var latlng = new google.maps.LatLng(t[0].LATITUDE, t[0].LONGITUDE);
                        var geocoder = new google.maps.Geocoder();
                        geocoder.geocode({ 'latLng': latlng },
                            function (results, status) {
                                if (status === google.maps.GeocoderStatus.OK) {
                                    if (results[0]) {
                                        t[0].MOBILE = results[0].formatted_address;
                                        $scope.mapdata.slice(0)[indx] = t;
                                    }
                                }
                            });
                    }
                }
                if ($scope.obj.selectValue == '2' || $scope.obj.selectValue == '3') {
                    $scope.obj.lat = t[0].HOS_LAT;
                    $scope.obj.lng = t[0].HOS_LONG;
                    $scope.MrkrImg = 'HospitalLoc';
                    $scope.obj.DispDir = true;
                    $scope.obj.DispMrkr = true;
                    if (t[0].HOS_LAT == null) {
                        $scope.obj.DispDir = false;
                        $scope.obj.DispMrkr = false;
                    }
                } else {
                    $scope.MrkrImg = 'PatientLoc';
                }

            });
            $scope.obj.t74041.T_USER_ID = $scope.obj.k.selected.AMB_CAPASITY;
            //var UserId = T74041Service.GetUserIDByAMBRegID(city.T_AMBU_REG_ID);
            //UserId.then(function (data) {
            //    $scope.obj.t74041.T_USER_ID = JSON.parse(data)[0].T_USER_ID;
            //});
            if (type == 2) {
                $scope.map.showInfoWindow('myInfoWindow', 'custom-marker-' + index);
            } else {
                for (var j = 0; j < allObj.length; j++) {
                    $scope.map.hideInfoWindow('myInfoWindow', 'custom-marker-' + j);
                }

            }


        };
        $scope.$on('gmPlacesAutocomplete::placeChanged', function () {
            var location = $scope.obj.autocomplete.getPlace().geometry.location;
            $scope.obj.lat = location.lat();
            $scope.obj.lng = location.lng();
            $scope.cx.latitude = location.lat();
            $scope.cx.longitude = location.lng();
            $scope.obj.DispDir = false;
            $scope.obj.DispMrkr = true;
            ambulatlong($scope.obj.lat, $scope.obj.lng);
            $scope.$apply();

            //-----------------Ruhul Amin-----------------
            //  function GetAddress() {
            //var lat = $scope.lat;
            // var lng =  $scope.lng;
            var latlng = new google.maps.LatLng($scope.obj.lat, $scope.obj.lng);
            var geocoder = geocoder = new google.maps.Geocoder();
            geocoder.geocode({ 'latLng': latlng }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    if (results[0]) {
                        $scope.obj.t74041.T_MAP_LOC = results[0].formatted_address;
                        // $scope.obj.t74041.T_MAP_LOC = results[1].formatted_;
                        // alert("Location: " + results[1].formatted_address);
                    }
                }
            });
            // }
            //-----------------------------------

        });
        $scope.getPos = function (event) {
            $scope.obj.lat = event.latLng.lat();
            $scope.obj.lng = event.latLng.lng();
            $scope.cx.latitude = event.latLng.lat();
            $scope.cx.longitude = event.latLng.lng();
            $scope.obj.DispDir = true;
            $scope.obj.DispMrkr = true;
            ambulatlong();
            $scope.$apply();

            //-----------------Ruhul Amin-----------------
            //  function GetAddress() {
            //var lat = $scope.lat;
            // var lng =  $scope.lng;
            var latlng = new google.maps.LatLng($scope.obj.lat, $scope.obj.lng);
            var geocoder = geocoder = new google.maps.Geocoder();
            geocoder.geocode({ 'latLng': latlng }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    if (results[0]) {
                        //$scope.obj.t74041.T_MAP_LOC = results[0].formatted_address;
                        $scope.obj.autocomplete = results[0].formatted_address;
                        // alert("Location: " + results[1].formatted_address);
                    }
                }
            });
        }
        $scope.PatInfo = function () {
            holdignOfPendingClear();
            document.getElementById("ShowActivePatientPopup").style.display = "none";
            document.getElementById("ShowCancelPopup").style.display = "none";
            var pendingReq = T74041Service.getPendingRequestData();
            pendingReq.then(function (data) {
                var jsonData = JSON.parse(data);
                $scope.pendingLen = jsonData.length;
                $scope.obj.PendingRequestList = jsonData;
                document.getElementById("ShowPendingPopup").style.display = "block";
            });
        }
        $scope.ClosePopup = function (id) {
            closePopup(id);
        };
        $scope.CloseActivePopup = function (id) {
            closePopup(id);
        };
        $scope.ShortDistance = function () {
            shortDistance();

        };
        $scope.Assing_Click = function (ind, data) {
            document.getElementById("ShowPendingPopup").style.display = "none";
            if (data.T_ACTV_OPER !== null) {
                alert($scope.getSingleMsg('S0077') + data.T_ACTV_OPER);
                // alert("Request is under processing by " + data.T_ACTV_OPER);
                return;
            }
            //------------Ruhul-------------
            //$scope.obj.DispDir = true;
            //$scope.obj.DispMrkr = true;
            //------------------------------
            $scope.obj.type = "u";
            sessionStorage.setItem('T_REQUEST_ID', data.T_REQUEST_ID);
            $scope.obj.t74041.T_REQUEST_ID = data.T_REQUEST_ID;
            $scope.obj.t74046.T_FIRST_LANG2_NAME = data.T_FIRST_LANG2_NAME;
            $scope.obj.t74046.T_FATHER_LANG2_NAME = data.T_FATHER_LANG2_NAME;
            $scope.obj.t74046.T_GFATHER_LANG2_NAME = data.T_GFATHER_LANG2_NAME;
            $scope.obj.t74046.T_MOBILE_NO = data.T_MOBILE_NO;
            $scope.obj.t74046.T_NATIONAL_ID = data.T_NATIONAL_ID;
            $scope.obj.t74046.T_ALT_MOBILE_NO = data.T_ALT_MOBILE_NO;
            $scope.obj.t74041.T_AGE = data.T_AGE;
            $scope.obj.t74041.T_PROBLEM = data.T_PROBLEM;
            $scope.obj.t74041.T_PROBLEM_DURATION = data.T_PROBLEM_DURATION;
            $scope.obj.autocomplete = data.T_MAP_LOC;

            $scope.obj.G.selected = { T_LANG2_NAME: data.GENDER, T_SEX_CODE: data.T_SEX_CODE };
            // $scope.obj.G.selected = { T_SEX_CODE: $scope.obj.PendingRequestList[ind].T_SEX_CODE };
            $scope.cx.latitude = data.T_LATITUDE;
            $scope.cx.longitude = data.T_LONGITUDE;
            $scope.obj.lat = data.T_LATITUDE;
            $scope.obj.lng = data.T_LONGITUDE;


            //caller info


            $scope.obj.t74120.T_FIRST_LANG2_NAME = data.CALLER_FIRST_NAME;
            $scope.obj.t74120.T_LAST_LANG2_NAME = data.CALLER_LAST_NAME;
            $scope.obj.t74120.T_MOBILE_NO = data.CALLER_MOBILE_NO;
            $scope.obj.t74120.T_ADDRESS = data.CALLER_ADDRESS;
            $scope.obj.R.selected = { T_CALL_REASON_ID: data.T_CALL_REASON_ID, T_REASON_NAME: data.CALL_REASON_NAME }
            $scope.obj.t74041.T_CALLER_ID = data.T_CALLER_ID;


            //$scope.obj.t74207.T_TELEFON_NO = data.T_TELEFON_NO;
            //$scope.obj.t74207.T_CAGRI_YAPAN_KISI = data.T_CAGRI_YAPAN_KISI;
            //$scope.obj.t74207.T_CAGRI_NEDENI = data.T_CAGRI_NEDENI;
            //$scope.obj.t74207.T_BULUSMA_NOKTASI = data.T_BULUSMA_NOKTASI;
            //$scope.obj.t74207.T_IL = data.T_IL;
            //$scope.obj.t74207.T_ILCE = data.T_ILCE;
            //$scope.obj.t74207.T_MAHALLE = data.T_MAHALLE;
            //$scope.obj.t74207.T_SOKAK_ADI = data.T_SOKAK_ADI;
            //$scope.obj.t74207.T_CAGRI_YAPAN_TIPI = data.T_CAGRI_YAPAN_TIPI;
            //$scope.obj.t74207.T_ADRES = data.T_ADRES;
            //$scope.obj.t74207.T_VAKA_ADRESI = data.T_VAKA_ADRESI;
            //$scope.obj.t74207.T_ARAYANIN_ADI_SOYADI = data.T_ARAYANIN_ADI_SOYADI;

            //case info
            //$scope.obj.t74207.T_OLAY_NO = data.T_OLAY_NO;
            //$scope.obj.t74207.T_KAYIT_PROTOKOL = data.T_KAYIT_PROTOKOL;
            //$scope.obj.t74207.T_PROTOCAL_NO = data.T_PROTOCAL_NO;
            //$scope.obj.t74207.T_KAYIT_TARIH_ZAMAN = $filter('date')(new Date(data.T_KAYIT_TARIH_ZAMAN), 'dd-MMM-yy');
            //$scope.obj.t74207.T_KAYIT_TARIH_ZAMAN_TIME = $filter('date')(new Date(data.T_KAYIT_TARIH_ZAMAN), 'h:mm a');

            if (data.T_FIRST_LANG2_NAME == 'Unknown' || data.T_FATHER_LANG2_NAME == 'Unknown' || data.T_GFATHER_LANG2_NAME == 'Unknown') {
                document.getElementById("btnSave").disabled = false;
                var span = document.getElementById("spnClass");
                span.parentNode.removeChild(span);
                var contact = document.getElementById("txtContactNo");
                contact.required = false;
                contact.classList.remove("ng-invalid");
            }


            //---------------------------------Newly Added
            if (data.T_LATITUDE != null && data.T_LONGITUDE != null) {
                var latlng = new google.maps.LatLng(data.T_LATITUDE, data.T_LONGITUDE);
                var geocoder = new google.maps.Geocoder();
                geocoder.geocode({ 'latLng': latlng }, function (results, status) {
                    if (status === google.maps.GeocoderStatus.OK) {
                        if (results[0]) {
                            //scope.obj.ApprxLocation = results[0].formatted_address;
                            $scope.obj.autocomplete = results[0].formatted_address;
                            //alert(results[0].formatted_address);
                        }
                    }
                });
            }
            //--------------Save Asign operator star-------------------
            var A = T74041Service.AsignPatientFromPendinglist(data.T_REQUEST_ID, 'Asign');
            A.then(function (data) {

            });

            //------------------Save Asign operator end---------------



            //$scope.$apply();
        }
        $scope.CancelPatientInfo = function () {
            document.getElementById("ShowPendingPopup").style.display = "none";
            document.getElementById("ShowActivePatientPopup").style.display = "none";
            getcancelData();
            document.getElementById("ShowCancelPopup").style.display = "block";
        };
        $scope.Accept_Cancel = function (ind, data) {
            document.getElementById("ShowCancelPopup").style.display = "none";
            var con = confirm($scope.getSingleMsg('S0132')); //Do you want to Approve ?


            if (con == true) {
                if (data.T_REQUEST_ID == null) {
                    var cncl = T74041Service.saveCancelConfirmData('Cancel_Confirm', data.T_REQUEST_ID, data.T_CANCEL_REASON, data.T_ID);
                    cncl.then(function (ok) {
                        alert('Request has been Approved');
                    });
                };

                var places = T74041Service.GetAllUserLatlong(null, null);
                places.then(function (am) {
                    $scope.places = JSON.parse(am);
                    if ($scope.places.length > 0) {
                        $scope.obj.type = "GeneratCancelRequest";
                        $scope.obj.t74041.T_REQUEST_ID = data.T_REQUEST_ID;
                        $scope.obj.t74041.T_PAT_ID = data.T_PAT_ID;
                        $scope.obj.t74041.T_COMPANY_ID = data.T_COMPANY_ID;
                        $scope.obj.t74046.T_FIRST_LANG2_NAME = data.T_FIRST_LANG2_NAME;
                        $scope.obj.t74046.T_FATHER_LANG2_NAME = data.T_FATHER_LANG2_NAME;
                        $scope.obj.t74046.T_GFATHER_LANG2_NAME = data.T_GFATHER_LANG2_NAME;
                        $scope.obj.t74046.T_MOBILE_NO = data.T_MOBILE_NO;
                        $scope.obj.t74046.T_NATIONAL_ID = data.T_NATIONAL_ID;
                        $scope.obj.t74046.T_ALT_MOBILE_NO = data.T_ALT_MOBILE_NO;
                        $scope.obj.t74041.T_AGE = data.T_AGE;
                        $scope.obj.t74041.T_PROBLEM = data.T_PROBLEM;
                        $scope.obj.t74041.T_PROBLEM_DURATION = data.T_PROBLEM_DURATION;
                        $scope.obj.autocomplete = data.T_MAP_LOC;

                        $scope.obj.G.selected = { T_LANG2_NAME: data.GENDER, T_SEX_CODE: data.T_SEX_CODE };
                        // $scope.obj.G.selected = { T_SEX_CODE: $scope.obj.PendingRequestList[ind].T_SEX_CODE };
                        $scope.cx.latitude = data.T_LATITUDE;
                        $scope.cx.longitude = data.T_LONGITUDE;
                        $scope.obj.lat = data.T_LATITUDE;
                        $scope.obj.lng = data.T_LONGITUDE;


                        //caller info
                        $scope.obj.t74207.T_TELEFON_NO = data.T_TELEFON_NO;
                        $scope.obj.t74207.T_CAGRI_YAPAN_KISI = data.T_CAGRI_YAPAN_KISI;
                        $scope.obj.t74207.T_CAGRI_NEDENI = data.T_CAGRI_NEDENI;
                        $scope.obj.t74207.T_BULUSMA_NOKTASI = data.T_BULUSMA_NOKTASI;
                        $scope.obj.t74207.T_IL = data.T_IL;
                        $scope.obj.t74207.T_ILCE = data.T_ILCE;
                        $scope.obj.t74207.T_MAHALLE = data.T_MAHALLE;
                        $scope.obj.t74207.T_SOKAK_ADI = data.T_SOKAK_ADI;
                        $scope.obj.t74207.T_CAGRI_YAPAN_TIPI = data.T_CAGRI_YAPAN_TIPI;
                        $scope.obj.t74207.T_ADRES = data.T_ADRES;
                        $scope.obj.t74207.T_VAKA_ADRESI = data.T_VAKA_ADRESI;
                        $scope.obj.t74207.T_ARAYANIN_ADI_SOYADI = data.T_ARAYANIN_ADI_SOYADI;

                        // it is for Table T74117, for passing data
                        $scope.obj.t74207.T_ID_17 = data.T_ID;
                        $scope.obj.t74207.T_TYPE = data.T_CANCEL_REASON;

                        //case info
                        $scope.obj.t74207.T_OLAY_NO = data.T_OLAY_NO;
                        $scope.obj.t74207.T_KAYIT_PROTOKOL = data.T_KAYIT_PROTOKOL;
                        $scope.obj.t74207.T_PROTOCAL_NO = data.T_PROTOCAL_NO;
                        $scope.obj.t74207.T_KAYIT_TARIH_ZAMAN = $filter('date')(new Date(data.T_KAYIT_TARIH_ZAMAN), 'dd-MMM-yy');
                        $scope.obj.t74207.T_KAYIT_TARIH_ZAMAN_TIME = $filter('date')(new Date(data.T_KAYIT_TARIH_ZAMAN), 'h:mm a');

                        if (data.T_FIRST_LANG2_NAME == 'Unknown' || data.T_FATHER_LANG2_NAME == 'Unknown' || data.T_GFATHER_LANG2_NAME == 'Unknown') {
                            document.getElementById("btnSave").disabled = false;
                            var span = document.getElementById("spnClass");
                            span.parentNode.removeChild(span);
                            var contact = document.getElementById("txtContactNo");
                            contact.required = false;
                            contact.classList.remove("ng-invalid");
                        }


                        //---------------------------------Newly Added
                        if (data.T_LATITUDE != null && data.T_LONGITUDE != null) {
                            var latlng = new google.maps.LatLng(data.T_LATITUDE, data.T_LONGITUDE);
                            var geocoder = new google.maps.Geocoder();
                            geocoder.geocode({ 'latLng': latlng }, function (results, status) {
                                if (status === google.maps.GeocoderStatus.OK) {
                                    if (results[0]) {
                                        //scope.obj.ApprxLocation = results[0].formatted_address;
                                        $scope.obj.autocomplete = results[0].formatted_address;
                                        //alert(results[0].formatted_address);
                                    }
                                }
                            });
                        }
                    } else {
                        var m = confirm('Team is not Available, Do you want to send into Pending list ?');
                        if (m == true) {
                            var cncl = T74041Service.saveCancelConfirmData('Send_to_PendingList', data.T_REQUEST_ID, data.T_CANCEL_REASON, data.T_ID);
                            cncl.then(function (data) {
                                alert($scope.getSingleMsg('S0134'));
                            });
                        }
                    }
                });
                // });


            } else {
                //var cncl = T74041Service.saveCancelConfirmData('Cancel_Not_Confirm',data.T_REQUEST_ID);
                //cncl.then(function (data) {

                //}); 
            }
        };
        $scope.Declined_Cancel = function (ind, data) {
            document.getElementById("ShowCancelPopup").style.display = "none";
            var conf = confirm($scope.getSingleMsg('S0133'));//Do you want to Decline ?
            if (conf == true) {
                var cncl = T74041Service.saveCancelConfirmData('Cancel_Not_Confirm', data.T_REQUEST_ID, data.T_CANCEL_REASON, data.T_ID);
                cncl.then(function (d) {
                    if (d == '1') {
                        // alert('Team is not permited for need to clean');
                    } else {
                        alert($scope.getSingleMsg('S0135'));
                    }

                    // alert('Reassign into same Team ');
                    document.getElementById("ShowCancelPopup").style.display = "none";
                });
            }
        }
        $scope.CancelRequest_Click = function (i) {
            if ($scope.obj.PendingRequestList[i].T_CANCEL_REASON !== undefined) {
                var cancelCode = $scope.obj.PendingRequestList[i].T_CANCEL_REASON;
                var request = $scope.obj.PendingRequestList[i].T_REQUEST_ID;
                var cancel = T74041Service.cancelReuest(request, cancelCode);
                cancel.then(function (data) {
                    alert(data);
                    //document.getElementById("ShowPendingPopup").style.display = "block";
                    var a = $scope.getLatLong();
                    var t74026 = {};
                    t74026.T_REQUEST_ID = request;
                    t74026.T_EVENT_ID = 3;
                    t74026.T_LATITUDE = a.lt;
                    t74026.T_LONGITUDE = a.ln;
                    T74041Service.save26(t74026);


                    //Reload Pending List without popup close
                    $scope.PatInfo();



                });
            } else {
                //alert('You select Cancel Reason');
                alert($scope.getSingleMsg('S0016'));
            }

        };
        $scope.chk01Click = function (e) {
            var UserId = T74041Service.GetCriteriasData();
            UserId.then(function (data) {
                $scope.obj.t74041.T_USER_ID = JSON.parse(data)[0].T_USER_ID;
                //sessionStorage.setItem("t74041_T_USER_ID", JSON.parse(data)[0].T_USER_ID);
            });
        };
        $scope.LoadDropDownList = function (e, t) {
            $scope.loader(true);
            if (e == 1) {
                var places = T74041Service.GetAllUserLatlong($scope.obj.t74041.T_LATITUDE, $scope.obj.t74041.T_LONGITUDE);
                places.then(function (data) {
                    $scope.places = JSON.parse(data);
                    $scope.places.sort(function (a, b) {
                        return parseFloat(a.distance) - parseFloat(b.distance);
                    });
                    $scope.loader(false);
                    $scope.MrkrImg = 'PatientLoc';
                    $scope.desLatLong = $scope.places;
                    chkChngd(t);
                });
            } else if (e == 2) {
                var ambWithPat = T74041Service.getAmbulanceList();
                ambWithPat.then(function (data) {
                    $scope.places = JSON.parse(data);
                    $scope.loader(false);
                    $scope.MrkrImg = 'HospitalLoc';
                    chkChngd(t);
                });
            } else if (e == 3) {
                var ambneedClean = T74041Service.getCleanNeedAmbulanceList();
                ambneedClean.then(function (data) {
                    $scope.places = JSON.parse(data);
                    $scope.loader(false);
                    $scope.MrkrImg = 'HospitalLoc';
                    chkChngd(t);
                });
            } else {
                var amblogOut = T74041Service.getLoggedOutAmbulanceList();
                amblogOut.then(function (data) {
                    $scope.places = JSON.parse(data);
                    $scope.loader(false);
                    $scope.MrkrImg = 'PatientLoc';

                    chkChngd(t);
                });
            }



        };
        setInterval(function () {
            if ($scope.obj.selectValue === "1") {
                var places = T74041Service.GetAllUserLatlong($scope.obj.t74041.T_LATITUDE, $scope.obj.t74041.T_LONGITUDE);
                places.then(function (data) {
                    //$scope.places = JSON.parse(data);
                    var dt = JSON.parse(data);
                    $scope.ddlList = JSON.parse(data);
                    $scope.kkk = false;
                    dt.sort(function (a, b) {
                        return parseFloat(a.distance) - parseFloat(b.distance);
                    });
                    $scope.places = dt;
                    //$scope.$apply();
                });
            }

            $scope.LoadDropDownList($scope.obj.selectValue, 0);

            getActivePatientInfo();
            getLastestPendingPatInfo();
            getcancelData();
            //alert($scope.activeLen);
            //alert($scope.pendingLen);



        }, 10000);
        //------------------Functions---------------------
        function focus(p) {
            document.getElementById(p).focus();
        }
        function getActivePatientInfo() {
            var actvPatList = T74041Service.GetActivePatientsData();
            actvPatList.then(function (data) {
                var dt = JSON.parse(data);
                $scope.activeLen = dt.length;

            });
        }
        function getPendingPatInfo() {
            var pendingReq = T74041Service.getPendingRequestData();
            pendingReq.then(function (data) {
                var jsonData = JSON.parse(data);
                $scope.pendingLen = jsonData.length;
                $scope.obj.PendingRequestList = jsonData;

                var temp = jsonData.length;

                $scope.pendingLen = temp;
                $scope.PreviousPendingLen = temp;
            });
        }
        function getcancelData() {
            var cn = T74041Service.getCancelPatData();
            cn.then(function (data) {
                var count = JSON.parse(data);
                $scope.CancelCount = count.length;
                $scope.obj.CancelRequestList = JSON.parse(data);
            });
        }
        function ambulatlong() {
            var getAllAmb = T74041Service.getAllAmb($scope.obj.lat, $scope.obj.lng);
            getAllAmb.then(function (data) {
                var dt = JSON.parse(data);
                dt.sort(function (a, b) {
                    return parseFloat(a.distance) - parseFloat(b.distance);
                });
                $scope.places = dt;
            });
        }
        function getlabel() {
            var labelData = LabelService.getlabeldata('T74041');
            labelData.then(function (data) {
                $scope.entity = data;
            });
        }
        function getgender() {
            var Gender = T74041Service.Gender();
            Gender.then(function (data) {
                $scope.gender = data;
            });
        }
        function getcallingReason() {
            var callReason = T74041Service.getCallReason();
            callReason.then(function (data) {
                $scope.callReasonList = data;
            });
        }
        function getEntryUser() {
            var EntryUser = T74041Service.EntryUser();
            EntryUser.then(function (data) {
                $scope.obj.t74041.T_ENTRY_USER = data;
                $scope.obj.t74046.T_ENTRY_USER = data;
                $scope.obj.t74046.T_ENTRY_DATE = $filter('date')(new Date(), 'dd-MMM-yyyy');
            });
        }
        function getCompanyId() {
            var CompanyId = T74041Service.CompanyId();
            CompanyId.then(function (data) {
                $scope.obj.t74041.T_COMPANY_ID = data;
            });
        }
        function getcanReason() {
            var canReason = T74041Service.getCancelReasonData();
            canReason.then(function (data) {
                $scope.CancelReasonList = data;
            });
        }
        function getAllUserLatLong() {
            var cx = T74041Service.GetLoginUserLatlong();
            cx.then(function (data) {
                $scope.cx = { latitude: JSON.parse(data)[0].latitude, longitude: JSON.parse(data)[0].longitude };
            });
        }
        function holdignOfPendingClear() {
            var B = T74041Service.AsignPatientFromPendinglist(0, 'ClearAll');
            B.then(function (data) {
            });
        }
        function removeSessionReq() {
            if (sessionStorage.getItem('T_REQUEST_ID') !== null) {
                var B = T74041Service.AsignPatientFromPendinglist(sessionStorage.getItem('T_REQUEST_ID'), 'Clear');
                B.then(function (data) {
                    sessionStorage.removeItem('T_REQUEST_ID');
                });
            }
        }
        function closePopup(id) {
            document.getElementById(id).style.display = "none";
        }
        function getLastestPendingPatInfo() {
            var pendingReq = T74041Service.getPendingRequestData();
            pendingReq.then(function (data) {
                var jsonData = JSON.parse(data);

                $scope.obj.PendingRequestList = jsonData;
                var temp = jsonData.length;

                $scope.pendingLen = temp;
                if ($scope.PreviousPendingLen > $scope.pendingLen) {
                    $scope.PreviousPendingLen = $scope.pendingLen;
                }

            });
        }
      function shortDistance() {
          $scope.obj.DispDir = true;
            var temp = [];
            for (var i = 0; i < $scope.places.length; i++) {
                var t = {};
                t.T_AMBU_REG_ID = $scope.places[i].T_AMBU_REG_ID;
                t.AMB_CAPASITY = $scope.places[i].AMB_CAPASITY;
                t.latitude = $scope.places[i].latitude;
                t.longitude = $scope.places[i].longitude;
                t.distance = getDistanceFromLatLonInKm($scope.obj.lat, $scope.obj.lng, t.latitude, t.longitude);

                temp.push(t);
            }

            var sing = temp[0].distance;
            var st = temp[0].T_AMBU_REG_ID;
            for (var j = 0; j < temp.length; j++) {
                if (sing > temp[j].distance) {
                    st = temp[j].T_AMBU_REG_ID;
                }
            }
            var p = temp.filter(function (ar) {
                return ar.T_AMBU_REG_ID == st;
            });
            $scope.testLat = { latitude: p[0].latitude, longitude: p[0].longitude }

            $scope.obj.t74041.T_AMBU_REG_ID = p[0].T_AMBU_REG_ID;
            $scope.obj.k.selected = { AMB_CAPASITY: p[0].AMB_CAPASITY };
            $scope.obj.t74041.T_USER_ID = $scope.obj.k.selected.AMB_CAPASITY;
            //var UserId = T74041Service.GetUserIDByAMBRegID(p[0].T_AMBU_REG_ID);
            //UserId.then(function (data) {
            //    $scope.obj.t74041.T_USER_ID = JSON.parse(data)[0].T_USER_ID;
            //    //sessionStorage.setItem("t74041_T_USER_ID", JSON.parse(data)[0].T_USER_ID);
            //});
            //$scope.$apply();

        }
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
        function chkChngd(t) {
            if (t == 1) {
                $scope.obj.t74041.T_USER_ID = null;
                $scope.obj.t74041.T_AMBU_REG_ID = null;
                $scope.obj.k.selected = { AMB_CAPASITY: '', T_AMBU_REG_ID: '' };
                $scope.obj.DispDir = false;
                $scope.obj.DispMrkr = false;
                $scope.obj.autocomplete = '';
                $scope.loader(false);
            }
        }
        //-------------------------------------Sorted Code----------------End------------



























        //var vm = this;

        //NgMap.getMap().then(function (map) {
        //    vm.map = map;
        //    $scope.map = map;
        //});


        //---------------------------------Previous Save without MOH DATA------Start

        //if ($scope.obj.t74041.T_PAT_ID == undefined) {
        //    var save_t74046 = T74041Service.Insert_t74046($scope.obj.t74046);
        //    save_t74046.then(function (data) {
        //        $scope.obj.t74041.T_PAT_ID = JSON.parse(data);
        //        var save_t74041 = T74041Service.Insert_t74041($scope.obj.t74041);
        //        save_t74041.then(function (data) {
        //            //alert('Data saved sucessfully');
        //            alert($scope.getSingleMsg('S0002'));
        //            $scope.Clear();

        //            var a = $scope.getLatLong();
        //            var t74026 = {};
        //            t74026.T_REQUEST_ID = $scope.obj.t74041.T_REQUEST_ID;
        //            t74026.T_EVENT_ID = 1;
        //            t74026.T_LATITUDE = a.lt;
        //            t74026.T_LONGITUDE = a.ln;
        //            var dd = T74041Service.save26(t74026);
        //            dd.then(function (data) {
        //                var t74026A = {};
        //                t74026A.T_REQUEST_ID = $scope.obj.t74041.T_REQUEST_ID;
        //                t74026A.T_EVENT_ID = 2;
        //                t74026A.T_LATITUDE = a.lt;
        //                t74026A.T_LONGITUDE = a.ln;
        //                var kk = T74041Service.save26(t74026A);
        //                kk.then(function (d) {
        //                    //$window.location.reload();
        //                });

        //            });

        //            // var a = $scope.getLatLong();


        //        });
        //    });
        //} else {
        //    var save_t74041 = T74041Service.Insert_t74041($scope.obj.t74041);
        //    save_t74041.then(function (data) {
        //        //alert('Data saved sucessfully');
        //        alert($scope.getSingleMsg('S0012'));
        //        $scope.Clear();
        //        var a = $scope.getLatLong();
        //        var t74026 = {};
        //        t74026.T_REQUEST_ID = $scope.obj.t74041.T_REQUEST_ID;
        //        t74026.T_EVENT_ID = 2;
        //        t74026.T_LATITUDE = a.lt;
        //        t74026.T_LONGITUDE = a.ln;
        //        var jj = T74041Service.save26(t74026);
        //        jj.then(function (data) {
        //            //$window.location.reload();
        //        });
        //    });
        //}





        //---------------------------------Previous Save without MOH DATA------End 
        //---------------------------------Latest Save with MOH DATA------Start
        //$scope.Save = function () {
        //    $scope.loader(true);
        //    if ($scope.obj.selectValue == '1') {
        //        var savet74207 = T74041Service.Insert_T74207($scope.obj.t74207, $scope.obj.t74041, $scope.obj.t74046,$scope.obj.type);
        //        savet74207.then(function (data) {
        //            var dt = JSON.parse(data);
        //            alert(dt);
        //            $scope.Clear();
        //            protocolNo();
        //        });
        //    } else {
        //        alert('Please select "All available ambulance"');
        //    }




        //};
        //---------------------------------Latest Save with MOH DATA------End
        //------------------------for closing tab start ---------------------------
        //var inFormOrLink;
        // $('a').live('click', function () { inFormOrLink = true; });
        // $('form').bind('submit', function () { inFormOrLink = true; });

        //$(window).bind('beforeunload', function (eventObject) {
        //    var B = T74041Service.AsignPatientFromPendinglist(0, 'Clear');
        //    B.then(function (data) {
        //        if (data==='1') {
        //            var returnValue = undefined;
        //            if (!inFormOrLink) {
        //                returnValue = "Do you really want to close?";
        //            }
        //            eventObject.returnValue = returnValue;
        //            return returnValue;
        //        }

        //    });


        //}); 



        //window.addEventListener("beforeunload", function (e) {

        //    var confirmationMessage = "\o/";
        //    (e || window.event).returnValue = confirmationMessage; //Gecko + IE
        //    return confirmationMessage; 

        //                              Webkit, Safari, Chrome
        //});

        //------------------------for closing tab end ---------------------------

        // this function has been implemented in menucontroller also


        //var marker = new google.maps.Marker({
        //    position: new google.maps.LatLng($scope.lat, $scope.lon),
        //    icon: '',
        //    label: { color: '#00aaff', fontWeight: 'bold', fontSize: '14px', text: 'Your text here' },
        //    optimized: false,
        //    visible: true
        //});
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





        //-----------------------------Code end---------------------------





        //-------------For Active PatList show & save-----------end-------------
        // ----- Ruhul Amin start---------------

        ////for getting Id from query sting start

        //var url = $location.absUrl();
        //var reqid = url.split("=").pop();//NaN
        //$scope.requestId = parseInt(reqid);
        //if (reqid !==url) {
        //    $scope.requestId = parseInt(reqid);

        //    var places = T74041Service.getAllHospitalLatlong();
        //    places.then(function (data) {
        //        $scope.HospitalList = data; //JSON.parse(data);
        //        patientInformation();

        //    });

        //} else {
        //    $scope.isAmbulance = true;
        //}
        //$scope.onHospitalSearch = function (lat, long, sitecode) {
        //    $scope.Hospital = { latitude: lat, longitude: long };
        //    $scope.latHos = lat;
        //    $scope.lngHos = long;
        //    $scope.$apply();
        //};

        //function patientInformation() {
        //    var getPatIn = T74041Service.getPatientInformation($scope.requestId);
        //    getPatIn.then(function(data) {
        //        $scope.obj.t74041.T_REQUEST_ID = data[0].T_REQUEST_ID;
        //        $scope.obj.t74046.T_FIRST_LANG2_NAME = data[0].T_FIRST_LANG2_NAME;
        //        $scope.obj.t74046.T_FATHER_LANG2_NAME = data[0].T_FATHER_LANG2_NAME;
        //        $scope.obj.t74046.T_GFATHER_LANG2_NAME = data[0].T_GFATHER_LANG2_NAME;
        //        $scope.obj.t74046.T_MOBILE_NO = data[0].T_MOBILE_NO;
        //        $scope.obj.t74046.T_NATIONAL_ID = data[0].T_NATIONAL_ID;
        //        $scope.obj.t74046.T_ALT_MOBILE_NO = data[0].T_ALT_MOBILE_NO;
        //        $scope.obj.t74041.T_AGE = data[0].T_AGE;
        //        $scope.obj.t74041.T_PROBLEM = data[0].T_PROBLEM;
        //        $scope.obj.t74041.T_PROBLEM_DURATION = data[0].T_PROBLEM_DURATION;


        //        $scope.obj.G.selected = { T_LANG2_NAME: data[0].GENDER, T_SEX_CODE: data[0].T_SEX_CODE };

        //        $scope.latAmbu = data[0].latitude;
        //        $scope.lngAmbu = data[0].longitude;
        //        $scope.cx.latitudeHos = $scope.latAmbu;
        //        $scope.cx.longitudeHos = $scope.lngAmbu;
        //        $scope.lat = data[0].latitude;
        //        $scope.lng = data[0].longitude;

        //        var latlng = new google.maps.LatLng(data[0].latitude, data[0].longitude);
        //        var geocoder = geocoder = new google.maps.Geocoder();
        //         geocoder.geocode({ 'latLng': latlng }, function (results, status) {
        //         if (status == google.maps.GeocoderStatus.OK) {
        //         if (results[0]) {
        //             $scope.autocomplete = results[0].formatted_address;
        //             $scope.lat = data[0].latitude;
        //             $scope.lng = data[0].longitude;

        //                  }
        //              }
        //          });


        //    });

        //}
        // ----- Ruhul Amin end ---------------


        //$scope.onExit = function () {
        //    alert('sddd');
        //}
        //$scope.obj.time = 10000;
        //document.onkeypress = function () {
        //    //key_dtl = key_dtl || window.event;
        //    //var uni_code = key_dtl.keyCode || key_dtl.which;
        //    //var key_name = String.fromCharCode(uni_code);
        //    //alert(key_name);
        //    $scope.obj.time = 10000;
        //}
        //setInterval(function () {
        //    var places = T74041Service.GetAllUserLatlong();
        //    places.then(function (data) {
        //        $scope.places = JSON.parse(data);
        //    }); 
        //}, 5000);


        //testplace();
        //function testplace() {
        //    var places = T74041Service.GetAllUserLatlong();
        //    places.then(function(data) {
        //        $scope.places = JSON.parse(data);
        //        $scope.$apply();
        //    });
        //}

        //$scope.hideMarker = function () {
        //    //alert(id);
        //    //this.hide();
        //    //document.getElementById(id).style.display.hide();
        //    //visibility: hidden
        //    //document.getElementById('temp_Marker').style.display = "none";
        //    //document.getElementById('txtsearchAmb').style.display = "none";
        //}


        //setInterval(function () {
        //   // getLocation();
        //}, 60000); //60000 second == 1 minute

        //-------- Ruhul Amin -----for GPS end-------------
        //$scope.ShortDistanceOnEnter = function(event) {
        //    if (event.keyCode == 13) {
        //        shortDistance();
        //    }
        //}


        //----------------For Search-------------------------


        //var ambneedClean = T74041Service.getCleanNeedAmbulanceList();
        //ambneedClean.then(function(data) {
        //    var dt = JSON.parse(data);
        //});
        //function initmap() {

        //var directionsDisplay = new google.maps.DirectionsRenderer;
        //var map = new google.maps.Map(document.getElementById('gmap'),
        //    {
        //        zoom: 13,
        //        center: { lat: $scope.cx.latitude, lng: $scope.cx.longitude }
        //    });
        //directionsDisplay.setMap(map);

        //$scope.cx.resetMap = function () {

        //}
        //NgMap.getMap({ id: "gmap" }).
        //    then(function (map) {
        //        NgMap.initMap(map.id);
        //    });

        //}


        //$scope.ondisableClick = function() {
        //    if ($scope.kkk) {
        //        alert('Please select "All available ambulance"');
        //    }
        //}

        //NgMap.getMap({ id: "gmap" }).
        //    then(function (map) {
        //        NgMap.initMap(map.id);
        //    });
        //$scope.obj.countRequest = 0;
        //setInterval(function () {

        //    if ($scope.obj.countRequest == 0) {
        //            var pendingReq = T74041Service.getPendingRequestData();
        //            pendingReq.then(function (data) {
        //                var jsonData = JSON.parse(data);
        //                $scope.obj.countRequest = jsonData.length;
        //                $scope.$apply();
        //            });
        //        }
        //    },
        //    5000);
       
    }]);
     
