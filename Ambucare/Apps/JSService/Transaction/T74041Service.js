app.service("T74041Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
    var dataSvc = {
        GetUserIDByAMBRegID: GetUserIDByAMBRegID,
        Gender: Gender,
        GetAllUserLatlong: GetAllUserLatlong,
        GetLoginUserLatlong: GetLoginUserLatlong,
        GetAllDataOnMap_T74041: GetAllDataOnMap_T74041,
        EntryUser: EntryUser,
        CompanyId: CompanyId,
        Insert_t74046: Insert_t74046,
        Insert_t74041: Insert_t74041,
        Insert_T74207: Insert_T74207,
        GPS_Insert: GPS_Insert,
        getPendingRequestData: getPendingRequestData,
        cancelReuest: cancelReuest,
        getCancelReasonData: getCancelReasonData,
        save26: save26,
        Result: Result,
        GetCriteriasData: GetCriteriasData
        // getAllHospitalLatlong: getAllHospitalLatlong,
        // getPatientInformation: getPatientInformation,
        // updateByOperator: updateByOperator
        , GetActivePatientsData: GetActivePatientsData
        , SaveRemarks: SaveRemarks
        , getAmbulanceList: getAmbulanceList
        , getLoggedOutAmbulanceList: getLoggedOutAmbulanceList
        , getCleanNeedAmbulanceList: getCleanNeedAmbulanceList
        , getAllHospitalLatlong: getAllHospitalLatlong
        , getMaxProtocol: getMaxProtocol

        , getWebSer: getWebSer
        , AsignPatientFromPendinglist: AsignPatientFromPendinglist,
        getCancelPatData: getCancelPatData,
        saveCancelConfirmData: saveCancelConfirmData
        , getCallReason: getCallReason
        , getAllAmb: getAllAmb
        , saveReq: saveReq
    };
    return dataSvc;
    function Gender() {
        try {
            var url = vrtlDirr + '/T74041/Gender';
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
    function GetAllUserLatlong(patLat, patLon) {
        try {
            var url = vrtlDirr + '/T74041/GetAllUserLatlong';
            var params = { patLat: patLat, patLon: patLon};
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
    function GetLoginUserLatlong() {
        try {
            var url = vrtlDirr + '/T74041/GetLoginUserLatlong';
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
    function GetAllDataOnMap_T74041(e) {
        try {
            var url = vrtlDirr + '/T74041/GetAllDataOnMap_T74041';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: { P_AMBU_REG_ID: e }
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function GetUserIDByAMBRegID(e) {
        try {
            var url = vrtlDirr + '/T74041/GetUserIDByAMBRegID';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: { T_AMBU_REG_ID: e }
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function EntryUser() {
        try {
            var url = vrtlDirr + '/Accounts/EntryUser';
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
    function CompanyId() {
        try {
            var url = vrtlDirr + '/Accounts/CompanyId';
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
    function Insert_t74046(e) {
        try {
            var url = vrtlDirr + '/T74041/Insert_t74046';
            var params = { _t74046: e };
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
    function Insert_t74041(e) {
        try {
            var url = vrtlDirr + '/T74041/Insert_t74041';
            var params = { _t74041: e };
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

    function Insert_T74207(e,f,g,t) {
        try {
            var url = vrtlDirr + '/T74041/Insert_T74207';
            var params = { t74207: e, t74041:f,t74046:g,type:t};
            return $http({
                url: url,
                method: "POST",
                data: params
            }
            ).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (e) {
            throw e;
        }
    }

    function GPS_Insert(latitude, longitude) {
        try {
            var url = vrtlDirr + '/T74041/GPS_Insert';
            var params = { latitude: latitude, longitude: longitude };
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
    function getPendingRequestData(latitude, longitude) {
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
    function cancelReuest(request, canCode) {
        try {
            var url = vrtlDirr + '/T74041/CancelReuest';
            var params = { request: request, canCode: canCode };
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
    function getCancelReasonData() {
        try {
            var url = vrtlDirr + '/T74041/GetCancelReasonData';
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
    function save26(t74026) {
        try {
            var url = vrtlDirr + '/Menu/SaveLatLong';
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
    function Result() {
        try {
            var url = vrtlDirr + '/T74041/Result';
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
    //function getAllHospitalLatlong() {
    //    try {
    //        var url = '/T74041/GetAllHospitalLatlong';
    //        var params = {};
    //        return $http({
    //            url: url,
    //            method: "POST",
    //            data: params
    //        }).then(function (results) {
    //            return results.data;
    //        }).catch(function (ex) {
    //            throw ex;
    //        });
    //    } catch (ex) {
    //        throw ex;
    //    }
    //}
    //function getPatientInformation(requestId) {
    //    try {
    //        var url = '/T74041/GetPatientInformation';
    //        var params = { requestId: requestId};
    //        return $http({
    //            url: url,
    //            method: "POST",
    //            data: params
    //        }).then(function (results) {
    //            return results.data;
    //        }).catch(function (ex) {
    //            throw ex;
    //        });
    //    } catch (ex) {
    //        throw ex;
    //    }
    //}
    //function updateByOperator(requestId,doc) {
    //    try {
    //        var url = '/T74041/UpdateByOperator';
    //        var params = { requestId: requestId,doc:doc };
    //        return $http({
    //            url: url,
    //            method: "POST",
    //            data: params
    //        }).then(function (results) {
    //            return results.data;
    //        }).catch(function (ex) {
    //            throw ex;
    //        });
    //    } catch (ex) {
    //        throw ex;
    //    }
    //}


    function GetCriteriasData() {
        try {
            var url = vrtlDirr + '/T74041/GetCriteriasData';
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
    function GetActivePatientsData() {
        try {
            var url = vrtlDirr + '/T74041/GetActivePatientsData';
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
    function SaveRemarks(req, rem) {
        try {
            var url = vrtlDirr + '/T74041/SaveRemarks';
            var params = { req: req, rem: rem };
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

    function getAmbulanceList() {
        try {
            var url = vrtlDirr + '/T74041/getAmbulanceList';
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
    function getLoggedOutAmbulanceList() {
        try {
            var url = vrtlDirr + '/T74041/getLoggedOutAmbulanceList';
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
    function getCleanNeedAmbulanceList() {
        try {
            var url = vrtlDirr + '/T74041/getCleanNeedAmbulanceList';
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
    //-----------------------------------------------------------------------
    function getAllHospitalLatlong() {
        try {
            var url = vrtlDirr + '/T74042/GetAllHospitalLatlong';
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
    function getMaxProtocol() {
        try {
            var url = vrtlDirr + '/T74041/getMaxProtocol';
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
    function getWebSer() {
        try {
            var url = vrtlDirr + '/T74041/getWebSer';
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
    function getAllAmb(patLat, patLon) {
        try {
            var url = vrtlDirr + '/T74041/getAllAmb';
            var params = { patLat: patLat, patLon: patLon};
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
    function AsignPatientFromPendinglist(reqId,operation) {
        try {
            var url = vrtlDirr + '/T74041/AsignPatientFromPendinglist';
            var params = { reqId: reqId, operation: operation};
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
    function getCancelPatData() {
        try {
            var url = vrtlDirr + '/T74041/GetCancelPatData';
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
    function saveCancelConfirmData(cnfm,reqId,cnlRsn,Tid) {
        try {
            var url = vrtlDirr + '/T74041/SaveCancelConfirmData';
            var params = { cnfm: cnfm, reqId: reqId, cnlRsn: cnlRsn, Tid: Tid};
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
    //getCancelPatData
    function getCallReason() {
        try {
            var url = vrtlDirr + '/T74041/getCallReason';
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
    function saveReq(t41, t46, t20, t26) {
        try {
            var url = vrtlDirr + '/T74041/saveReq';
            var params = { t41:t41, t46:t46, t20:t20, t26:t26};
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