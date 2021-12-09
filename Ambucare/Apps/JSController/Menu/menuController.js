app.controller("menuController", ["$scope", "$window", "$location", "MenuService", "$filter", "vrtlDirr",
    function ($scope, $window, $location, MenuService, $filter, vrtlDirr) {
        $scope.UserName = '';
        $scope.chkBack = JSON.parse(sessionStorage.getItem("FCode"));
        $scope.stl_layout = $scope.chkBack == 'T74046' ? '84vh' : '78vh';
        $scope.vrDir = vrtlDirr;
        var apiLink = MenuService.getAPILink();
        apiLink.then(function (data) {
            $scope.apiLink = JSON.parse(data);

        });

        $scope.menusI = [];
        $scope.menusT = [];
        $scope.menusR = [];
        $scope.menusQ = [];



        var setup2 = MenuService.GetAllMenuData();
        setup2.then(function (data) {
            var dt = JSON.parse(data);
            for (var d = 0; d < dt.length; d++) {
                if (dt[d].T_FORM_TYPE_ID === 1) {
                    $scope.menusI.push(dt[d]);

                    //for (var i = 0; i < $scope.Menuentity.length; i++) {
                    //    if ($scope.Menuentity[i].T_LABEL_NAME == 'lblInitialization') {
                    //        $scope.Labelmsg = $scope.Menuentity[i].T_LANG2_TEXT;
                    //    }
                    //}
                    ////$scope.Labelmsg = "Initialization";
                    //$scope.menusI = columnize(dt, 15);
                } else if (dt[d].T_FORM_TYPE_ID === 2) {
                    $scope.menusT.push(dt[d]);
                    //for (var i = 0; i < $scope.Menuentity.length; i++) {
                    //    if ($scope.Menuentity[i].T_LABEL_NAME == 'lblTransaction') {
                    //        $scope.Labelmsg = $scope.Menuentity[i].T_LANG2_TEXT;
                    //    }
                    //}
                    ////$scope.Labelmsg = "Transaction";
                    //$scope.menusT = columnize(dt, 50);
                } else if (dt[d].T_FORM_TYPE_ID === 3) {
                    $scope.menusR.push(dt[d]);
                    //for (var i = 0; i < $scope.Menuentity.length; i++) {
                    //    if ($scope.Menuentity[i].T_LABEL_NAME == 'lblReports') {
                    //        $scope.Labelmsg = $scope.Menuentity[i].T_LANG2_TEXT;
                    //    }
                    //}
                    ////$scope.Labelmsg = "Report";
                    //$scope.menusR = columnize(dt, 15);
                } else if (dt[d].T_FORM_TYPE_ID === 4) {
                    $scope.menusQ.push(dt[d]);
                    //for (var i = 0; i < $scope.Menuentity.length; i++) {
                    //    if ($scope.Menuentity[i].T_LABEL_NAME == 'lblQuery') {
                    //        $scope.Labelmsg = $scope.Menuentity[i].T_LANG2_TEXT;
                    //    }
                    //}
                    ////$scope.Labelmsg = "Query";
                    //$scope.menusQ = columnize(dt, 15);
                }
            }

        });

        function columnize(input, cols) {
            var arr = [];
            for (i = 0; i < input.length; i++) {
                var colIdx = i % cols;
                arr[colIdx] = arr[colIdx] || [];
                arr[colIdx].push(input[i]);
            }
            return arr;
        }

        switch_style();

        function switch_style() {
            var i, linkTag;
            for (i = 0, linkTag = document.getElementsByTagName("script"); i < linkTag.length; i++) {
                if (linkTag[i].title == 'map') {
                    linkTag[i].disabled = true;
                }
            }
        }

        $scope.onLangClick = function (e) {
            var MenuLabel = MenuService.MenuLabel('Menu', e);
            MenuLabel.then(function (data) {
                $scope.Menuentity = data;
                sessionStorage.setItem("e", e);
                $scope.Menu($scope.par);
            });
        }
        $scope.onLangClick_n = function (e) {
            $scope.onLangClick(e);
            var k = $location.absUrl();
            var urlLength = k.split("/").length - 1;
            var a = urlLength > 4 ? k.split('/')[5] : k.split('/')[4];
            MenuService.getFormName(a).then(function (data) {
                var dt = JSON.parse(data);
                sessionStorage.setItem("FCode", JSON.stringify(a));
                sessionStorage.setItem("FName", JSON.stringify(dt[0].NAME));
                $window.location = "";


            });

            //var getFormName = MenuService.getFormName(e);
            //getFormName.then(function (data) {
            //    var dt = JSON.parse(data);
            //    return dt[0].NAME;

            //});


        }
        var e = sessionStorage.getItem("e");
        var a = e == undefined || e == null ? 2 : e;
        $scope.par = e == undefined || e == null ? 2 : $scope.par;
        $scope.onLangClick(a);

        $scope.Menu = function (e) {
            $scope.par = e;
            var Setup = MenuService.MenuData(e);
            Setup.then(function (data) {
                var dt = JSON.parse(data);
                if (e === 1) {
                    for (var i = 0; i < $scope.Menuentity.length; i++) {
                        if ($scope.Menuentity[i].T_LABEL_NAME == 'lblInitialization') {
                            $scope.Labelmsg = $scope.Menuentity[i].T_LANG2_TEXT;
                        }
                    }
                    //$scope.Labelmsg = "Initialization";
                    $scope.menus = columnize(dt, 15);
                } else if (e === 2) {
                    for (var i = 0; i < $scope.Menuentity.length; i++) {
                        if ($scope.Menuentity[i].T_LABEL_NAME == 'lblTransaction') {
                            $scope.Labelmsg = $scope.Menuentity[i].T_LANG2_TEXT;
                        }
                    }
                    //$scope.Labelmsg = "Transaction";
                    $scope.menus = columnize(dt, 50);
                } else if (e === 3) {
                    for (var i = 0; i < $scope.Menuentity.length; i++) {
                        if ($scope.Menuentity[i].T_LABEL_NAME == 'lblReports') {
                            $scope.Labelmsg = $scope.Menuentity[i].T_LANG2_TEXT;
                        }
                    }
                    //$scope.Labelmsg = "Report";
                    $scope.menus = columnize(dt, 15);
                } else if (e === 4) {
                    for (var i = 0; i < $scope.Menuentity.length; i++) {
                        if ($scope.Menuentity[i].T_LABEL_NAME == 'lblQuery') {
                            $scope.Labelmsg = $scope.Menuentity[i].T_LANG2_TEXT;
                        }
                    }
                    //$scope.Labelmsg = "Query";
                    $scope.menus = columnize(dt, 15);
                }
            });
        }

        var url = JSON.parse(sessionStorage.getItem("url"));
        $scope.BackURL = url === null || url === undefined ? "Transaction" : url;
        $scope.BackInit = function (e) {
            if (e === "Transaction") {
                var transaction = $scope.Menu(2);
                return transaction;
            } else if (e === "Report") {
                var reports = $scope.Menu(3);
                return reports;
            } else if (e === "Query") {
                var query = $scope.Menu(4);
                return query;
            } else {
                var setup = $scope.Menu(1);
                return setup;
            }
        }
        var UserName = MenuService.UserName();
        UserName.then(function (data) {
            $scope.UserName = JSON.parse(data);
            sessionStorage.setItem("UserName", JSON.stringify($scope.UserName));
        });
        var UserId = MenuService.UserId();
        UserId.then(function (data) {
            $scope.UserId = JSON.parse(data);
            sessionStorage.setItem("UserId", JSON.stringify($scope.UserId));
        });

        $scope.Logout = function () {

            if (sessionStorage.getItem('T_REQUEST_ID') !== null) {
                var B = MenuService.AsignPatientFromPendinglist(sessionStorage.getItem('T_REQUEST_ID'), 'Clear');
                B.then(function (data) {
                    sessionStorage.removeItem('T_REQUEST_ID');
                });
            }



            var u = sessionStorage.getItem("orUrl");
            var msg = $scope.confLatLong();
            if (msg == 'ok') {
                //var b = $scope.getLatLong();
                var b = { lt: 37.092231, ln: 36.249527 };
                //var b = {lt:'',ln:''};
                var a = $location.absUrl().split('/')[2].split('?')[0];
                var logout = MenuService.UserLogout(b.lt, b.ln);
                logout.then(function (data) {
                    sessionStorage.clear();
                    //$window.location = "http://" + a;
                    $window.location = u;
                });
            } else {
                a = $location.absUrl().split('/')[2].split('?')[0];
                logout = MenuService.UserLogout('', '');
                logout.then(function (data) {
                    sessionStorage.clear();
                    // $window.location = "http://" + a;
                    $window.location = u;
                });
            }



            //var a = $location.absUrl().split('/')[2].split('?')[0];
            //var logout = MenuService.UserLogout();
            //logout.then(function(data) {
            //    sessionStorage.clear();
            //    $window.location = "http://" + a;
            //});
        };
        $scope.Back = function () {
            var k = $location.absUrl();
            //var k = 'http://localhost:5871/Transaction/T74041';
            var urlLength = k.split("/").length - 1;
            var a = '';
            var url = '';
            var vrtl = '';
            if (urlLength > 4) {
                vrtl = k.split('/')[3];
                a = k.split('/')[5].split('?')[0];
                if (a === "T74116" || a === "T74119") {
                    $window.location = "/" + vrtl + "/Transaction/T74115";
                } else {
                    url = k.split('/')[4];
                    sessionStorage.setItem("url", JSON.stringify(url));
                    $window.location = "/" + vrtl + "/Menu";
                }

            }

            else {
                a = k.split('/')[4].split('?')[0];
                if (a === "T74116" || a === "T74119") {
                    $window.location = "/Transaction/T74115";
                } else {
                    url = k.split('/')[3];
                    sessionStorage.setItem("url", JSON.stringify(url));
                    $window.location = "/Menu";
                }
            }

        }

        $scope.FormName = function (data) {
            sessionStorage.setItem("FCode", JSON.stringify(data.T_FORM_CODE));
            sessionStorage.setItem("FName", JSON.stringify(data.T_LANG2_NAME));
            if (sessionStorage.getItem('T_REQUEST_ID') !== null) {
                var B = MenuService.AsignPatientFromPendinglist(sessionStorage.getItem('T_REQUEST_ID'), 'Clear');
                B.then(function (data) {
                    sessionStorage.removeItem('T_REQUEST_ID');
                });
            }
            var a = $location.$$absUrl;
            var k = a.search('5871') < 0 ? a.split('/').splice(0, 4).join('/') : a.split('/').splice(0, 3).join('/');
            var msg = MenuService.updateForm(data.T_FORM_CODE);
            msg.then(function (dt) {
                chkForm = JSON.parse(dt) == 'OK' ? false : true;
                window.location.href = k + "/" + data.Menu_Name + "/" + data.T_FORM_CODE;
            }).catch(function (ex) {
                window.location.href = k + "/" + data.Menu_Name + "/" + data.T_FORM_CODE;
            });

        };

        $scope.FCode = JSON.parse(sessionStorage.getItem("FCode"));
        $scope.FName = JSON.parse(sessionStorage.getItem("FName"));

        $scope.loader = function (p) {
            $scope.loading = p === undefined ? false : p;
            return $scope.loading;
        };
        //-------- Ruhul Amin -----for GPS start-------------
        var error = '';
        var lati = '';
        var longi = '';

        function getLocation() {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(showPosition);
            } else {
                error = "Geolocation is not supported by this browser.";
            }
        }

        function showPosition(position) {
            var latitude = position.coords.latitude;
            var longitude = position.coords.longitude;
            // $scope.latitude_Last = latitude;
            // if ($scope.latitude_Last !== latitude) {
            var gps = MenuService.GPS_Insert(latitude, longitude);
            gps.then(function (data) {
                latitude = '';
                longitude = '';
            });
            // }

        }

        $scope.$watch('myVar',
            function () {
                // getLocation();
                // alert(lati);
            });

        setInterval(function () {
            // getLocation();
        },
            60000); //60000 milisecond == 1 minute

        //-------- Ruhul Amin -----for GPS end-------------

        //navigator.geolocation.getCurrentPosition(showPosition2);
        //function showPosition2(position) {
        //    $scope.lt = position.coords.latitude;
        //    $scope.ln = position.coords.longitude;

        //}

        $scope.Panicked = function () {


            $scope.lt = 23.7582849;
            $scope.ln = 90.39022219999993;

            MenuService.Save_t74041($scope.UserId, $scope.lt, $scope.ln, showAddress($scope.lt, $scope.ln));
            //if ($scope.lt != null && $scope.ln != null) {
            //    navigator.geolocation.getCurrentPosition(function (p) {
            //        $scope.$evalAsync(function () {
            //            $scope.lt = p.coords.latitude;
            //            $scope.ln = p.coords.longitude;
            //        });
            //    });
            //     MenuService.Save_t74041($scope.UserId, $scope.lt, $scope.ln, showAddress($scope.lt, $scope.ln));
            //}
            //else {
            //    alert('Location cannot be accesed. Please enable your location from browser');
            //}











            //var getLatLong = MenuService.GetLatLong($scope.UserId);
            //getLatLong.then(function (data) {
            //    var LatLongData = JSON.parse(data);
            //    var lat = LatLongData[0].T_LATITUDE;
            //    var long = LatLongData[0].T_LONGITUDE;
            //    var address = showAddress(lat, long);
            //    var savePanic_t74041 = MenuService.Save_t74041($scope.UserId, lat, long,address);
            //});


        }
        $scope.confLatLong = function () {
            //return ($scope.lt != null && $scope.ln != null) ? 'ok' : 'Location cannot be accesed.Please enable your location from browser';
            return 'ok';
        }
        $scope.getLatLong = function () {
            //navigator.geolocation.getCurrentPosition(function (p) {
            //    $scope.$evalAsync(function () {
            //        $scope.lt = p.coords.latitude;
            //        $scope.ln = p.coords.longitude;
            //    });
            //});
            //return { lt: $scope.lt, ln: $scope.ln };


            //----------------------------------Fake It will bechanged----------
            $scope.lt = 23.7582849;
            $scope.ln = 90.39022219999993;
            return { lt: $scope.lt, ln: $scope.ln };
        }
        $scope.getLatLong();

        //$scope.confLatLong = function () {
        //    return ($scope.lt != null && $scope.ln != null)? 'ok': 'Location cannot be accesed.Please enable your location from browser';
        //}
        //$scope.getLatLong = function () {
        //    navigator.geolocation.getCurrentPosition(function (p) {
        //        $scope.$evalAsync(function () {
        //            $scope.lt = p.coords.latitude;
        //            $scope.ln = p.coords.longitude;
        //        });
        //    });
        //    return {lt: $scope.lt, ln: $scope.ln};
        //}
        //$scope.showAddress =
        function showAddress(lat, lon) {
            var a = '';
            var latlng = new google.maps.LatLng(lat, lon);
            var geocoder = new google.maps.Geocoder();
            geocoder.geocode({ 'latLng': latlng }, function (results, status) {
                if (status === google.maps.GeocoderStatus.OK) {
                    if (results[0]) {
                        a = results[0].formatted_address;

                        //alert(a);
                        // $scope.obj.t74041.T_MAP_LOC = results[1].formatted_;
                        // alert("Location: " + results[1].formatted_address);
                    }
                }
            });
            //$scope.map.showInfoWindow('myInfoWindow', this);
            return a;
        };
        //var chk_session = JSON.parse(sessionStorage.getItem("msg"));
        var chk_session = JSON.parse(sessionStorage.getItem("FCode"));
        if (chk_session != null) {
            if ($scope.FCode != undefined || $scope.FCode != '') {
                var msg = MenuService.GetAllUserMsg($scope.FCode);
                msg.then(function (data) {
                    var sdfdsf = JSON.stringify(JSON.parse(data));
                    sessionStorage.setItem("msg", JSON.stringify(JSON.parse(data)));
                });
            }
        }
        $scope.getSingleMsg = function (e) {
            var obj = $filter('filter')(JSON.parse(sessionStorage.getItem("msg")), { T_MSG_CODE: e }, true)[0];
            return obj.MSG;
        }
        $(function () {
            $('.omf-modal').draggable({
                //handle: ".omf-modal-header"
                handle: ".omf-modal-body"
            });
            //$('omf-modal').resizable();

        });
        $scope.custom = true;
        $scope.onLogout = function (i) {
            $scope.custom = $scope.custom === false ? true : false;
            document.getElementById('divLogout').style.display = 'inline-block';

        }


        //-----------------set user form---------
        $scope.chkForm = true;
        function updateForm(form) {
            if ($scope.chkForm) {
                var a = $location.$$absUrl;
                var fc = a.search('5871') < 0 ? a.split('/')[5] : a.split('/')[4];
                var formCode = fc == undefined || fc == '' ? a.search('5871') < 0 ? a.split('/')[4] : a.split('/')[3]:fc;
                var param = form != null ? form : formCode;
                var msg = MenuService.updateForm(param);
                msg.then(function (data) {
                    $scope.chkForm = JSON.parse(data) == 'OK' ? false : true;
                    return true;
                }).catch(function (ex) {
                    return true;
                });
            }


        }

        $(window).focus(function () {
            updateForm(null);
        });
        $(document).on('visibilitychange', function () {

            if (document.visibilityState == 'hidden') {
                // page is hidden
            } else {
                // page is visible
                updateForm(null);
            }
        });
        $(document).on('click', function () {
            if ($scope.chkForm) {
                updateForm(null);
                $scope.chkForm = false;
            }

        });

        $scope.setError = function (controller, action, message) {
            MenuService.setClientErrorLog(controller, action, message);
        }

    }]);
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
app.directive('numbersOnly', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attr, ngModelCtrl) {
            function fromUser(text) {
                if (text) {
                    var transformedInput = text.replace(/[^0-9]/g, '');

                    if (transformedInput !== text) {
                        ngModelCtrl.$setViewValue(transformedInput);
                        ngModelCtrl.$render();
                    }
                    return transformedInput;
                }
                return undefined;
            }
            ngModelCtrl.$parsers.push(fromUser);
        }
    };
});
app.directive('nextFocus', function () {
    return {
        restrict: 'A',
        link: function ($scope, elem, attrs) {
            elem.bind('keydown', function (e) {
                var code = e.keyCode || e.which;
                if (code === 13) {
                    try {
                        if (attrs.tabindex != undefined) {
                            var currentTabIndex = attrs.tabindex;
                            var nextTabIndex = parseInt(attrs.tabindex) + 1;
                            $("[tabindex=" + nextTabIndex + "]").focus();
                        }
                    } catch (e) {

                    }
                }
            });
        }
    }
});
app.directive('clickCapture',function () {
        return {
            link: function (scope, elm, attrs) {
                elm.bind('click',
                    function () {
                        //Clicks on the body close all drop downs.
                        $("body").removeClass("page-quick-sidebar-open");
                        //$scope.custom = $scope.custom === false ? true : false;
                        $("#divLogout").css("display", "none");
                    });

                elm.bind('keydown',
                    function (e) {
                        if (e.keyCode === 27) {
                            $("body").removeClass("page-quick-sidebar-open");
                        }

                    });
            }
        }
    });
app.directive('soundButton', function ($interval) {
    return {
        restrict: 'E',
        link: function ($scope, element, attrs) {
            var audioSrc = attrs.origem,
                audio = angular.element('<audio/>'),
                inner = angular.element('<source/>');

            inner.attr('src', audioSrc);
            audio.attr('autoplay', true);
            audio.attr('control', false);
            audio.attr('loop', true);
            element.append(audio);
            audio.append(inner);

            audio[0].pause();


            element.css({
                display: 'none'

            });

            $interval(function () {
                //$scope.PreviousPendingLen = $scope.pendingLen;

                if ($scope.pendingLen > $scope.PreviousPendingLen) {
                    $("#blk").addClass("blinkSpn");
                    audio[0].play();
                    element.css({
                        borderRadius: '50%',
                        width: '44px',
                        height: '44px',
                        display: 'inline-block',
                        position: 'absolute',
                        right: '282px',
                        top: '263px',
                        textAlign: 'center'
                    });


                }
                //else {
                //    $("#blk").removeClass("blinkSpn");
                //    audio[0].pause();
                //    element.css({
                //        display: 'none'

                //    });
                //}

            }, 100);


            $interval(function () {
                //$scope.PreviousPendingLen = $scope.pendingLen;

                $("#blk").removeClass("blinkSpn");
                audio[0].pause();
                element.css({
                    display: 'none'

                });

                $scope.PreviousPendingLen = $scope.pendingLen;

            }, 10000);

            $scope.play = function () {
                audio[0].pause();

                $scope.PreviousPendingLen = $scope.pendingLen;
                $("#blk").removeClass("blinkSpn");
                //if (audio[0].paused) {
                //    audio[0].play();
                //} else {
                //    audio[0].pause();
                //}

                element.css({
                    display: 'none'

                });
            };



        }
    }
});
