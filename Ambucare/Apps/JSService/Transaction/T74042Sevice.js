
app.service("T74042Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
            var dataSvc = {
               
                getAllHospitalLatlong: getAllHospitalLatlong,
                getPatientInformation: getPatientInformation,
                updateByOperator: updateByOperator,
                save26: save26
                , chkReqHos: chkReqHos
            };
            return dataSvc;

            function getAllHospitalLatlong() {
                try {
                    var url = vrtlDirr +'/T74042/GetAllHospitalLatlong';
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
            function getPatientInformation(requestId) {
                try {
                    var url = vrtlDirr +'/T74042/GetPatientInformation';
                    var params = { requestId: requestId };
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
        function updateByOperator(requestId, doc) {
            try {
                var url = vrtlDirr +'/T74042/UpdateByOperator';
                var params = { requestId: requestId, doc: doc };
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
        function save26(t74026) {
            try {
                var url = vrtlDirr +'/Menu/SaveLatLong';
                return $http({
                    url: url,
                    method: "POST",
                    data: { t74026: t74026 }
                }).then(function (results) {
                    return results.data;
                }).catch(function (ex) {
                    throw ex;
                });
            } catch (ex) {
                throw ex;
            }
    }
        function chkReqHos(r) {
            try {
                var url = vrtlDirr + '/T74131/chkReqHos';
                var params = { requestId: r };
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