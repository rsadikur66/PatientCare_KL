app.service("MenuService", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
    //for instance
    var dataSvc = {
        MenuData: MenuData,
        UserName: UserName,
        UserId:UserId,
        UserLogout: UserLogout,
        getlabeldata: getlabeldata,
        MenuLabel: MenuLabel,
        GPS_Insert: GPS_Insert,
        GetLatLong: GetLatLong,
        Save_t74041: Save_t74041,
        GetAllUserMsg: GetAllUserMsg,
        GetAllMenuData: GetAllMenuData
        , getFormName: getFormName
        , getAPILink: getAPILink
        , AsignPatientFromPendinglist: AsignPatientFromPendinglist
        , updateForm: updateForm
        , setClientErrorLog: setClientErrorLog
    };
    return dataSvc;
    function MenuData(e) {
        try {
            var url = vrtlDirr+'/Menu/MenuData';
            var params = {T_FORM_TYPE_ID:e};
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

    function GetAllMenuData() {
        try {
            var url = vrtlDirr + '/Menu/GetAllMenuData';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: params
            }).then(function(results) {
                return results.data;
            }).catch(function(ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }

    function UserName() {
        try {
            var url = vrtlDirr +'/Menu/UserName';
            var params = {};
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
    function UserId() {
        try {
            var url = vrtlDirr +'/Menu/UserId';
            var params = {};
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
    function UserLogout(lt,ln) {
        try {
            var url = vrtlDirr +'/Accounts/UserLogout';
            var params = {lt:lt,ln:ln};
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
    function getlabeldata(formcode) {
        try {
            var url = vrtlDirr +'/T74000/GetLabelTextData';
            //alert("IUHIH");
            var params = { T_FORM_CODE: formcode };
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
    function MenuLabel(formcode,e) {
        try {
            var url = vrtlDirr +'/T74000/MenuLabel';
            //alert("IUHIH");
            var params = { T_FORM_CODE: formcode,e:e };
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
     function GPS_Insert(latitude, longitude) {
        try {
            var url = vrtlDirr +'/Menu/GPS_Insert';
            var params = { latitude: latitude, longitude: longitude};
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
    function GetLatLong(T_USER_ID) {
        try {
            var url = vrtlDirr +'/Menu/GetLatLong';
            //alert("IUHIH");
            var params = { T_USER_ID: T_USER_ID };
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
    function Save_t74041(userid,lat,lon,address) {
        try {
            var url = vrtlDirr +'/Menu/Save_t74041';
            var params = { userid: userid, lat: lat, lon: lon, address: address};
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

    function GetAllUserMsg(e) {
        try {
            var url = vrtlDirr +'/Menu/GetAllUserMsg';
            return $http({
                url: url,
                method: "POST",
                data: { T_FORM_CODE : e}
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
            var url = vrtlDirr +'/Menu/getFormName';
            return $http({
                url: url,
                method: "POST",
                data: { code : e}
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function getAPILink() {
        try {
            var url = vrtlDirr + '/Menu/getAPILink';
            return $http({
                url: url,
                method: "POST",
                data: {}
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function AsignPatientFromPendinglist(reqId, operation) {
        try {
            var url = vrtlDirr + '/T74041/AsignPatientFromPendinglist';
            var params = { reqId: reqId, operation: operation };
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
            var params = { controller: controller, action: action, message: message, source: ''};
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
    
}]);