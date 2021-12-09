app.service("T74044Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {

    var dataSvc = {
        GetAmbReg: GetAmbReg,
        GetMohTeam: GetMohTeam,
        GetMohStation: GetMohStation,
        GetStoreData: GetStoreData,
        AddStore: AddStore
    };
    return dataSvc;

    function GetAmbReg() {
        try {
            var url = vrtlDirr + '/T74044/GetAmbReg';
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
    function GetMohTeam() {
        try {
            var url = vrtlDirr + '/T74044/GetMohTeam';
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
    function GetMohStation() {
        try {
            var url = vrtlDirr + '/T74044/GetMohStation';
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
    function GetStoreData() {
        try {
            var url = vrtlDirr + '/T74044/GetStoreData';
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
    function AddStore(_t74044) {
        try {
            var url = vrtlDirr + '/T74044/AddStore';
            return $http.post(url, _t74044).then(function (results) {
                return results.data;
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }
    }
}]);