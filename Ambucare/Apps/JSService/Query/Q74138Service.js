app.service("Q74138Service", ["$http", "vrtlDirr", function ($http, vrtlDirr, $q) {

    var dataSvc = {
        getMapload: getMapload
    };
    return dataSvc;

    function getMapload() {
        try {
            var url = vrtlDirr + '/Q74138/getMapload';
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
}]);