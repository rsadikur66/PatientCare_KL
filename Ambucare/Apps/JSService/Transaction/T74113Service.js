app.service("T74113Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
    var dataSvc = {
        GetPatInfo: GetPatInfo,
        GetDocInfoByReq: GetDocInfoByReq,
        GetDocDept: GetDocDept,
        DiagonosisList: DiagonosisList,
        GetFrequency: GetFrequency,
        GetDuration: GetDuration,
        GetMedicineByTrade: GetMedicineByTrade,
        GetGen: GetGen,
        GetStrengthByGen: GetStrengthByGen,
        GetRouteByGen: GetRouteByGen,
        GetFormByGen: GetFormByGen,
        Insert: Insert,
        EntryUser: EntryUser,
        GetAdviceList: GetAdviceList,
        GetDiagonosisByPres: GetDiagonosisByPres,
        GetMedListByPres: GetMedListByPres

};
    return dataSvc;
    
    function GetPatInfo() {
        try {
            var url = vrtlDirr + '/T74113/GetPatInfo';
            var params = {};
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
    function GetDocInfoByReq(e) {
        try {
            var url = vrtlDirr + '/T74113/GetDocInfoByReq';
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
    function GetDocDept(e) {
        try {
            var url = vrtlDirr + '/T74113/GetDocDept';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: { T_EMP_ID: e }
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function DiagonosisList() {
        try {
            var url = vrtlDirr + '/T74113/DiagonosisList';
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
    function GetFrequency() {
        try {
            var url = vrtlDirr + '/T74113/GetFrequency';
            var params = {};
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
    function GetDuration(Frequency_Code) {
        try {
            var url = vrtlDirr + '/T74113/GetDuration';
            var params = { Frequency_Code: Frequency_Code};
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
    function GetStrengthByGen(e) {
        try {
            var url = vrtlDirr + '/T74113/GetStrengthByGen';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: { T_GEN_CODE:e}
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function GetMedicineByTrade() {
        try {
            var url = vrtlDirr + '/T74113/GetMedicineByTrade';
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
    function GetGen() {
        try {
            var url = vrtlDirr + '/T74113/GetGen';
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
    function GetRouteByGen(e) {
        try {
            var url = vrtlDirr + '/T74113/GetRouteByGen';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: { T_GEN_CODE:e}
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function GetFormByGen(e) {
        try {
            var url = vrtlDirr + '/T74113/GetFormByGen';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: { T_GEN_CODE:e}
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function Insert(t74039, t74040List, t74078List) {
        try {
            var url = vrtlDirr + '/T74113/Insert';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: { t74039: t74039, t74040List: t74040List, t74078List: t74078List}
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

    function GetAdviceList(e) {
        try {
            var url = vrtlDirr + '/T74113/GetAdviceList';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: { T_LANG2_NAME: e }
            }).then(function(results) {
                return results.data;
            }).catch(function(ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function GetDiagonosisByPres(e) {
        try {
            var url = vrtlDirr + '/T74113/GetDiagonosisByPres';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: { Pres: e }
            }).then(function(results) {
                return results.data;
            }).catch(function(ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function GetMedListByPres(e) {
        try {
            var url = vrtlDirr + '/T74113/GetMedListByPres';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: { Pres: e }
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