app.service("T74136Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
    //For Instance Start 
    var dataSvc = {
        GetZone: GetZone,
        GetSite: GetSite,
        maxUserId: maxUserId,
        getEmpList: getEmpList,
        getRole: getRole,
        CheckUserId: CheckUserId,
        GetGridData: GetGridData,
        saveUser: saveUser,
        getMaxValue: getMaxValue
    };
    return dataSvc;

    function GetZone() {
        try {
            var url = vrtlDirr + '/T74136/getZone';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                // data: params
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
    function GetSite(e) {
        try {
            var url = vrtlDirr + '/T74136/GetSite';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: { P_REGION_CODE: e }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function maxUserId() {
        try {
            var url = vrtlDirr + '/T74136/maxUserId';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                // data: params
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
    function getEmpList() {
        try {
            var url = vrtlDirr + '/T74136/getEmpList';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                // data: params
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
    function getRole() {
        try {
            var url = vrtlDirr + '/T74136/getRole';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                // data: params
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

    function CheckUserId(userId) {
        try {
            var url = vrtlDirr + '/T74136/CheckUserId';
            var params = { userId: userId };
            return $http({
                url: url,
                method: "POST",
                // data: params
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
    function GetGridData() {
        try {
            var url = vrtlDirr + '/T74136/GetGridData';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                // data: params
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

    function saveUser(_T74057) {
        try {
            var url = vrtlDirr + '/T74136/saveUser';//AddItemBrand from controller T74002Controller
            return $http.post(url, _T74057).then(function (results) {
                return results.data;
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }
    }
    function getMaxValue(type) {
        try {
            var url = vrtlDirr + '/T74136/getMaxValue';
            return $http({
                url: url,
                method: "POST",
                // data: params
                data: { type: type}
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