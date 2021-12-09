app.service("T74151Service", ["$http", "vrtlDirr", function ($http, vrtlDirr, $q)
{
    var dataSvc = {
        getRespData: getRespData,
        InsertResp:insertResp
        
    };

    return dataSvc;

    function getRespData() {
        try {
            var url = vrtlDirr + '/T74151/GetRespData';
            var params = {};
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

    function insertResp(t74116) {
        try {
            var url = vrtlDirr + '/T74151/InsertResp';
            var params = { t74116: t74116};
            return $http({
                url: url,
                method: "POST",
                data: params
            }).then(function(restuls) {
                return restuls.data;
            }).catch(function(e) {
                throw e;
            });
        } catch (e) {
            throw e;
        } 
    }

}]);