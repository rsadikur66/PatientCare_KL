app.service("LoginService", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
        var dataSvc = {
            UserLogin: UserLogin,
            //LabelData: LabelData
            getFormName: getFormName
            , updateForm: updateForm
            , setClientErrorLog: setClientErrorLog
        };
        return dataSvc;

        //For Form Label Data Function Start
    function UserLogin(userId, password,lt,ln) {
            try {
                var url = vrtlDirr+'/Accounts/UserLogin';
                //alert("IUHIH");
                var params = { userId: userId, Password: password, lt: lt, ln: ln};
                return $http({
                    url: url,
                    method: "POST",
                    data: params
                }).then(function (results) {
                    return results.data;
                }).catch(function (ex) {
                    throw ex;
                });
            } catch (ex) {
                throw ex;
            }
        }
        function LabelData(formCode, lblName) {
            try {
                var url = vrtlDirr+'/Menu/LabelData';
                var params = { T_FORM_CODE: formCode, T_LABEL_NAME: lblName };
                return $http({
                    url: url,
                    method: "POST",
                    data: params
                }).then(function (results) {
                    return results.data;
                }).catch(function (ex) {
                    throw ex;
                });
            } catch (ex) {
                throw ex;
            }
    }
        function getFormName(e) {
            try {
                var url = vrtlDirr + '/Menu/getFormName';
                return $http({
                    url: url,
                    method: "POST",
                    data: { code: e }
                }).then(function (results) {
                    return results.data;
                }).catch(function (ex) {
                    throw ex;
                });
            } catch (ex) {
                throw ex;
            }
        }
    function updateForm(form) {
        try {
            var url = vrtlDirr + '/Menu/updateForm';
            var params = { form: form };
            return $http({
                url: url,
                method: "POST",
                data: params
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function setClientErrorLog(controller, action, message) {
        try {
            var url = vrtlDirr + '/Accounts/setClientErrorLog';
            var params = { controller: controller, action: action, message: message };
            return $http({
                url: url,
                method: "POST",
                data: params
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    }
]);

//For Form Label Data Function End