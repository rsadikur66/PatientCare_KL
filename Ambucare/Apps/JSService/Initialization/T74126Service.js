
app.service("T74126Service", ["$http", "vrtlDirr", function ($http, vrtlDirr, $q) {

        var dataSvc = {
            insert_T74126: insert_T74126,
            getEmployee: getEmployee,
            GetEmProData: GetEmProData
    };
        return dataSvc;

        function insert_T74126(t74126, Itemlist) {
            debugger;
        try {
            var url = vrtlDirr + "/T74126/insert_T74126";
            return $http({
                url: url,
                method: "POST",
                data: { T74004: t74126, T74093: Itemlist}
            }).then(function(results) {
                return results.data;
            }).catch(function(ex) {
                return ex;
            });
        } catch (ex) {
            throw ex;
        } 
    }

        function getEmployee() {
            try {
                var url = vrtlDirr + '/T74126/GetEmployee';
                var params = {};
                return $http({
                    url: url,
                    method: "POST",
                    data: params
                    // data: { GEN_CODE: GEN_CODE }
                }).then(function (results) {

                    return results.data;
                }).catch(function (ex) {
                    throw ex;
                });
            } catch (ex) {
                throw ex;
            }
        }

        function GetEmProData(T_EMP_ID) {
            
            try {
                var url = vrtlDirr + '/T74126/GetEmProData';
                return $http({
                    url: url,
                    method: "POST",
                    data: {
                        T_EMP_ID: T_EMP_ID
                    }
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