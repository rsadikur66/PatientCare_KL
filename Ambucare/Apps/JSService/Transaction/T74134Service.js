
app.service("T74134Service",["$http", "vrtlDirr", function ($http, vrtlDirr) {
            var dataSvc = {
                getPatInfo: getPatInfo,
                getVitalSign: getVitalSign,
                getItem: getItem,
                getStockData: getStockData,
                saveData: saveData,
                getPreviousMedicine: getPreviousMedicine,
                getServiceData: getServiceData,
                saveServiceData: saveServiceData
                
            };
            return dataSvc;
            function getPatInfo() {
                try {
                    var url = vrtlDirr + '/T74134/GetPatInfo';
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
            function getVitalSign(reqId) {
                try {
                    var url = vrtlDirr + '/T74134/GetVitalSign';
                    return $http({
                        url: url,
                        method: "POST",
                        data: { reqId: reqId}
                    }).then(function (results) {

                        return results.data;
                    }).catch(function (ex) {
                        throw ex;
                    });
                } catch (ex) {
                    throw ex;
                }
            }
            function getItem(ambu) {
                try {
                    var url = vrtlDirr + '/T74134/GetItem';
                    return $http({
                        url: url,
                        method: "POST",
                        data: { ambu: ambu }
                    }).then(function (results) {

                        return results.data;
                    }).catch(function (ex) {
                        throw ex;
                    });
                } catch (ex) {
                    throw ex;
                }
            }
            function getStockData(ambu,item) {
                try {
                    var url = vrtlDirr + '/T74134/GetStockData';
                    return $http({
                        url: url,
                        method: "POST",
                        data: { ambu: ambu, item: item }
                    }).then(function (results) {

                        return results.data;
                    }).catch(function (ex) {
                        throw ex;
                    });
                } catch (ex) {
                    throw ex;
                }
            }
            function saveData(t74036, t74037) {
                try {
                    var url = vrtlDirr + '/T74134/SaveData';
                    return $http({
                        url: url,
                        method: "POST",
                        data: { t74036: t74036, t74037: t74037}
                    }).then(function (results) {

                        return results.data;
                    }).catch(function (ex) {
                        throw ex;
                    });
                } catch (ex) {
                    throw ex;
                }
            }
            function getPreviousMedicine(requID) {
                try {
                    var url = vrtlDirr + '/T74134/GetPreviousMedicine';
                    return $http({
                        url: url,
                        method: "POST",
                        data: { requID: requID }
                    }).then(function (results) {

                        return results.data;
                    }).catch(function (ex) {
                        throw ex;
                    });
                } catch (ex) {
                    throw ex;
                }
            }

            function getServiceData(requID) {
                try {
                    var url = vrtlDirr + '/T74134/GetServiceData';
                    return $http({
                        url: url,
                        method: "POST",
                        data: { requID: requID }
                    }).then(function (results) {

                        return results.data;
                    }).catch(function (ex) {
                        throw ex;
                    });
                } catch (ex) {
                    throw ex;
                }
            }
            function saveServiceData(t74074, t74079) {
                try {
                    var url = vrtlDirr + '/T74134/SaveServiceData';
                    return $http({
                        url: url,
                        method: "POST",
                        data: { t74074: t74074, t74079: t74079 }
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