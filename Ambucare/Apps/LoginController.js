app.controller("LoginController", ["$scope", "LoginService", "$location", "vrtlDirr",
    function ($scope, LoginService, $location, vrtlDirr) { //$location,
        $scope.obj = {};
        $scope.vrtl = vrtlDirr;
        $scope.userName = 'nam';
        $scope.obj.T_PWD = "";
        $scope.obj.T_LOGIN_NAME = "";
        document.getElementById("txtEmpID").focus();
        $scope.loader = function (p) {
            $scope.loading = p === undefined ? false : p;
            return $scope.loading;
        };
        $scope.login = function () {
            try {
            var a = { lt: 23.7582849, ln: 90.39022219999993 };
                login($scope.obj.T_LOGIN_NAME, $scope.obj.T_PWD, a.lt, a.ln);
            } catch (e) {
                $scope.setError($scope.controllerName, 'login', e.message);
            }
        }
        $scope.enterKey = function (event) {
            if (event.keyCode === 13) {
                if ($scope.obj.T_PWD !== undefined && $scope.obj.T_PWD !== "") {
                    var a = { lt: 23.7582849, ln: 90.39022219999993 };
                    login($scope.obj.T_LOGIN_NAME, $scope.obj.T_PWD, a.lt, a.ln);
                }
                else {
                    document.getElementById("txtPassword").focus();
                }
            }
            else if (event.keyCode === 9) {
                document.getElementById("txtPassword").focus();
            }

        };        
        $scope.addNumber = function (num) {
            if ($scope.userName === 'nam') {
                $scope.obj.T_LOGIN_NAME += num;
            }
            else if ($scope.userName === 'pass') {
                $scope.obj.T_PWD += num;
                document.getElementById("txtPassword").focus();
            }
        };
        $scope.Cancel = function (ac) {
            $scope.obj.T_LOGIN_NAME = "";
            $scope.obj.T_PWD = "";
        };
        $scope.BackNumber = function () {
            if ($scope.userName === 'nam') {
                var tnum = $scope.obj.T_LOGIN_NAME.length;
                $scope.obj.T_LOGIN_NAME = $scope.obj.T_LOGIN_NAME.substring(0, tnum - 1);
            }
            else if ($scope.userName === 'pass') {
                var pnum = $scope.obj.T_PWD.length;
                $scope.obj.T_PWD = $scope.obj.T_PWD.substring(0, pnum - 1);
            }
        };
        $scope.EnterButton = function (l, p) {
            if ($scope.userName === 'pass') {
                login(l, p);
            } else {
                document.getElementById("txtPassword").focus();
                $scope.userName = 'pass';
            }
        }
        $scope.Logout = function () {
            $scope.obj.T_LOGIN_NAME = "";
            $scope.obj.T_PWD = "";
            document.getElementById("txtEmpID").focus();
        }
        function login(l, p, lt, ln) {
            $scope.loader(true);
            var loginData = LoginService.UserLogin(l, p, lt, ln);
            loginData.then(function (data) {
                var dt = JSON.parse(data);
                $scope.loader(false);
                
                if (dt[0].msg === "fls") {
                    alert('Invalid Id or Password....');
                    document.getElementById("txtEmpID").focus();
                }
                else {
                    var k = '';
                    var lastChar = $location.$$absUrl[$location.$$absUrl.length - 1];
                    if (lastChar == '/') {
                        k = $location.$$absUrl.slice(0, -1);
                    } else {
                        if ($location.$$absUrl.includes("http://localhost:5871") &&
                            $location.$$absUrl.includes("ReturnUrl=%2F")) {
                            k = $location.$$absUrl.slice(0, 21);
                        } else {
                            var appName = $location.$$absUrl.split('/')[3];
                            k = $location.$$protocol + "://" + $location.$$host + "/" + appName;
                        }

                    }
                    if (dt[0].role == 24) {
                        LoginService.getFormName('T74046').then(function (data) {
                            var dt = JSON.parse(data);
                            sessionStorage.setItem("FCode", JSON.stringify('T74046'));
                            sessionStorage.setItem("FName", JSON.stringify(dt[0].NAME));
                            setForm('/Transaction', 'T74046');

                        });
                    }
                    else if (dt[0].role == 81) {
                        LoginService.getFormName('T74131').then(function (data) {
                            var dt = JSON.parse(data);
                            sessionStorage.setItem("FCode", JSON.stringify('T74131'));
                            sessionStorage.setItem("FName", JSON.stringify(dt[0].NAME));
                            setForm('/Transaction', 'T74131');
                        });
                    }
                    else if (dt[0].role == 86) {
                        LoginService.getFormName('T74041').then(function (data) {
                            var dt = JSON.parse(data);
                            sessionStorage.setItem("FCode", JSON.stringify('T74041'));
                            sessionStorage.setItem("FName", JSON.stringify(dt[0].NAME));
                            setForm('/Transaction', 'T74041');
                        });

                    }
                    else if (dt[0].role == 111) {
                        LoginService.getFormName('Q74136').then(function (data) {
                            var dt = JSON.parse(data);
                            sessionStorage.setItem("FCode", JSON.stringify('Q74136'));
                            sessionStorage.setItem("FName", JSON.stringify(dt[0].NAME));
                            setForm('/Queries', 'Q74136');
                        });
                    }
                    else {
                        setForm('', 'Menu');
                    }
                }
            });
        }
        function setForm(type, code) {
            var a = $location.$$absUrl;
            var k = a.search('5871') < 0 ? a.split('/').splice(0, 4).join('/') : a.split('/').splice(0, 3).join('/');
            var msg = LoginService.updateForm(code);
            msg.then(function (dt) {
                window.location.href = k + type + '/' + code;
            }).catch(function (ex) {
                window.location.href = k + type + '/' + code;
            });
        }
        $scope.setError = function (controller, action, message) {
            LoginService.setClientErrorLog(controller, action, message);
        }
        }]);
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
                        alert(e);
                    }
                }
            });
        }
    }
});

