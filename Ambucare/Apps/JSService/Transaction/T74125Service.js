
app.service("T74125Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
    //For Instance Start 
    var dataSvc = {
        getPatientDetails: getPatientDetails,
        getBill: getBill,
        getBillDetails: getBillDetails,
        getIssueDetails: getIssueDetails,
        getIssueSumary: getIssueSumary,
        GetZone: GetZone,
        GetSite: GetSite,
        PatData: PatData,
        SavePatInfo: SavePatInfo,
        UpdateT74046: UpdateT74046,
        update_T74041: update_T74041,
        getDischargeReason: getDischargeReason,
        save26: save26
        , verifySummeryReport: verifySummeryReport


    };
    return dataSvc;

    function getPatientDetails(patId) {
        try {
            var url = vrtlDirr +'/T74125/GetPatientDetails';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                // data: params
                data: { patId: patId }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function getBill(patId) {
        try {
            var url = vrtlDirr +'/T74125/GetBill';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                // data: params
                data: { patId: patId }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function getBillDetails(request) {
        try {
            var url = vrtlDirr +'/T74125/GetBillDetails';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                // data: params
                data: { request: request }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }

    function getIssueDetails(request) {
        try {
            var url = vrtlDirr +'/T74125/GetIssueDetails';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                // data: params
                data: { request: request }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function getIssueSumary(request) {
        try {
            var url = vrtlDirr +'/T74125/GetIssueSumary';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                // data: params
                data: { request: request }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function GetZone() {
        try {
            var url = vrtlDirr +'/T74125/GetZone';
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
    function GetSite(e) {
        try {
            var url = vrtlDirr +'/T74125/GetSite';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                // data: params
                data: { P_REGION_CODE: e}
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function PatData(e) {
        try {
            var url = vrtlDirr +'/T74125/PatData';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: { T_PAT_ID: e}
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function SavePatInfo(t74046, t74041, t74043, t02003, T_SITE_CODE) {
        try {
            var url = vrtlDirr +'/T74125/SavePatInfo';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: { t74046: t74046, t74041: t74041, t74043: t74043, t02003: t02003, T_SITE_CODE: T_SITE_CODE}
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function UpdateT74046(T_PAT_ID, T_DISCH_DEST) {
        try {
            var url = vrtlDirr +'/T74125/UpdateT74046';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: { T_PAT_ID: T_PAT_ID, T_DISCH_DEST: T_DISCH_DEST}
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }

    function update_T74041(t74041) {
        try {
            var url = vrtlDirr +'/T74125/UpdateT74041';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                // data: params
                data: { t74041: t74041 }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function getDischargeReason() {
        try {
            var url = vrtlDirr +'/T74125/GetDischargeReason';
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
    function verifySummeryReport(requestId) {
        try {
            var url = vrtlDirr +'/T74125/verifySummeryReport';
            return $http({
                url: url,
                method: "POST",
                data: { requestId: requestId }
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