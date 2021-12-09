
app.service("Q74137Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
            var dataSvc = {
                getPendingRequestData: getPendingRequestData
            };
            return dataSvc;

            function getPendingRequestData() {
                try {
                    var url = vrtlDirr + '/T74041/GetPendingRequestData';
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