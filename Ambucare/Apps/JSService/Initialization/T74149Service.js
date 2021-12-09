app.service("T74149Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
        var dataSvc = {
            Insert: Insert,
            getGridData: getGridData
        };
        return dataSvc;

    function Insert(_t06301) {
            try {
                var url = vrtlDirr + '/T74149/Insert';
                var params = { _t06301: _t06301 };
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
    function getGridData() {
            try {
                var url = vrtlDirr + '/T74149/getGridData';
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

    }
]);