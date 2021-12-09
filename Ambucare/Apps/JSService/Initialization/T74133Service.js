app.service("T74133Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {

    var dataSvc = {
        GetZone: GetZone,
        GetStoreListTo: GetStoreListTo,
        GetGridAllData: GetGridAllData,
        Insert_T74133: Insert_T74133
};
    return dataSvc;

    function GetZone() {
        try {
            var url = vrtlDirr + '/T74133/GetZone';
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

    function GetStoreListTo() {
        try {
            var url = vrtlDirr + "/T74133/GetStoreListTo";
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

    function GetGridAllData(ambuId,zone) {
        try {
            var url = vrtlDirr + '/T74133/GetGridAllData';
            var params = { ambuId: ambuId,zone: zone };
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

    function Insert_T74133(t96) {
        try {
            var url = vrtlDirr + '/T74133/Insert_T74133';
            var params = { T74096: t96 };
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