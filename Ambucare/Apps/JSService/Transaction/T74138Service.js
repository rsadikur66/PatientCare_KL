

app.service("T74138Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
    var dataSvc = {
        //  GetUserIDByAMBRegID: GetUserIDByAMBRegID,
        //  Gender: Gender,
        GetAllUserLatlong: GetAllUserLatlong,
        GetLoginUserLatlong: GetLoginUserLatlong,
        //  GetAllDataOnMap_T74041: GetAllDataOnMap_T74041,
        //  EntryUser: EntryUser,
        //  CompanyId: CompanyId,
        //  Insert_t74046: Insert_t74046,
        // Insert_t74041: Insert_t74041,
        // GPS_Insert: GPS_Insert
        getDestination: getDestination,
        getDocSite: getDocSite,
        saveEvent: saveEvent
        , chkReqHos: chkReqHos
        , saveRequestHospitalbyTeam: saveRequestHospitalbyTeam
    };
    return dataSvc;
    //function Gender() {
    //    try {
    //        var url = vrtlDirr + '/T74041/Gender';
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
    function GetAllUserLatlong() {
        try {
            var url = vrtlDirr + '/T74138/GetAllUserLatlong';
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
    function GetLoginUserLatlong() {
        try {
            var url = vrtlDirr + '/T74138/GetLoginUserLatlong';
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
    //function GetAllDataOnMap_T74041(e) {
    //    try {
    //        var url = vrtlDirr + '/T74041/GetAllDataOnMap_T74041';
    //        var params = {};
    //        return $http({
    //            url: url,
    //            method: "POST",
    //            data: { P_AMBU_REG_ID: e }
    //        }).then(function (results) {
    //            return results.data;
    //        }).catch(function (ex) {
    //            throw ex;
    //        });
    //    } catch (ex) {
    //        throw ex;
    //    }
    //}
    //function GetUserIDByAMBRegID(e) {
    //    try {
    //        var url = vrtlDirr + '/T74041/GetUserIDByAMBRegID';
    //        var params = {};
    //        return $http({
    //            url: url,
    //            method: "POST",
    //            data: { T_AMBU_REG_ID: e }
    //        }).then(function (results) {
    //            return results.data;
    //        }).catch(function (ex) {
    //            throw ex;
    //        });
    //    } catch (ex) {
    //        throw ex;
    //    }
    //}
    //function EntryUser() {
    //    try {
    //        var url = vrtlDirr + '/Accounts/EntryUser';
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
    //function CompanyId() {
    //    try {
    //        var url = vrtlDirr + '/Accounts/CompanyId';
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
    //function Insert_t74046(e) {
    //    try {
    //        var url = vrtlDirr + '/T74041/Insert_t74046';
    //        var params = { _t74046: e };
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
    //function Insert_t74041(e) {
    //    try {
    //        var url = vrtlDirr + '/T74041/Insert_t74041';
    //        var params = { _t74041: e };
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
    //function GPS_Insert(latitude, longitude) {
    //    try {
    //        var url = vrtlDirr + '/T74041/GPS_Insert';
    //        var params = { latitude: latitude, longitude: longitude };
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
    function getDestination(e) {
        try {
            var url = vrtlDirr + '/T74138/getDestination';
            var params = { e: e };
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
    function getDocSite(e) {
        try {
            var url = vrtlDirr + '/T74138/getDocSite';
            var params = { e: e };
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
    function saveEvent(e, r, lt, ln) {
        try {
            var url = vrtlDirr + '/T74138/saveEvent';
            var params = { evt: e, req: r, lat: lt, lng: ln };
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
    function saveRequestHospitalbyTeam(requestID, hosID) {
        try {
            var url = vrtlDirr + '/T74138/saveRequestHospitalbyTeam';
            //var params =
            return $http({
                url: url,
                method: "POST",
                data: { requestID: requestID, hosID: hosID }
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