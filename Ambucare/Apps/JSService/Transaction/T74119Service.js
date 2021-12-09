app.service("T74119Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
        var dataSvc = {
            getSalesData: getSalesData,
            getEmpList: getEmpList,
            Insert: Insert
            };
        return dataSvc;

        function getSalesData(e) {
            try {
                var url = vrtlDirr + '/T74119/getSalesData';
                return $http({
                    url: url,
                    method: "POST",
                    data: { T_SL_REQ_ID: e }
                }).then(function (results) {return results.data;
                }).catch(function (ex) {
                    throw ex;
                });
            } catch (ex) {
                throw ex;
            }
        }
        function getEmpList(e) {
            try {
                var url = vrtlDirr + '/T74119/getEmpList';
                return $http({
                    url: url,
                    method: "POST",
                    data: { T_STORE_ID: e }
                }).then(function (results) {return results.data;
                }).catch(function (ex) {
                    throw ex;
                });
            } catch (ex) {
                throw ex;
            }
        }
        function Insert(t74027,t74036, t74037List, t74048, t74049List) {
            try {
                var url = vrtlDirr + '/T74119/Insert';
                var params = {};
                return $http({
                    url: url,
                    method: "POST",
                    data: { t74027:t74027,t74036: t74036, t74037List: t74037List, t74048: t74048, t74049List: t74049List }
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