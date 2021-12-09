app.service("T74015Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
    var dataSvc = {
        GetGridData: GetGridData,
        InsertOrUpdate: InsertOrUpdate,
        deleteData: deleteData,
        getemployeedata: getemployeedata,
        getGridEmployeeTypeData: getGridEmployeeTypeData,
        getAmbulanceDropdownList: getAmbulanceDropdownList,
        insert_T74015: insert_T74015,
        //insert_t74073: insert_t74073,
        getEmployeeTypeIdAndEmployeeIdByAmbulanceId: getEmployeeTypeIdAndEmployeeIdByAmbulanceId
    };
    return dataSvc;
    
    function GetGridData() {
        try {
            var url = vrtlDirr + '/T74015/GetGridData';
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
   
    function InsertOrUpdate(_t74015) {
        try {
            var url = vrtlDirr + '/T74015/InsertOrUpdate';
            return $http({
                url: url,
                method: "POST",
                data: JSON.stringify(_t74015)
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }

    function deleteData(e) {

        try {
            var url = vrtlDirr + '/T74015/deleteData';
            return $http({
                url: url,
                method: "POST",
                data: { T_AMBU_CAPA_TYPE_ID: e }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function getAmbulanceDropdownList() {
        try {
            var url = vrtlDirr + '/T74015/GetAmbulanceDropdownList';
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
    function getEmployeeTypeIdAndEmployeeIdByAmbulanceId(e) {
        try {
            var url = vrtlDirr + '/T74015/GetEmployeeTypeId';
            var params = { T_AMBU_REG_ID: e };
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
    function getemployeedata(T_EMP_TYP_ID) {
        try {
            var url = vrtlDirr +'/T74015/GetEmployeeData';
            var params = { T_EMP_TYP_ID: T_EMP_TYP_ID };
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
    function getGridEmployeeTypeData(e) {
        try {
            var url = vrtlDirr + '/T74015/getGridEmployeeTypeData';
            var params = { T_AMBU_REG_ID: e };
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

    function insert_T74015(_T74015) {
        try {
            var url = vrtlDirr + '/T74015/Add_T74015';//AddItemBrand from controller T74002Controller
            return $http.post(url, _T74015).then(function (results) {
                return results.data;
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }
    }
    //function insert_t74073(_t74073) {
    //    try {
    //        var url = vrtlDirr + '/T74015/Add_t74073';//AddItemBrand from controller T74002Controller
    //        return $http.post(url, _t74073).then(function (results) {
    //            return results.data;
    //        }).catch(function (e) { });
    //    } catch (ex) {
    //        throw ex;
    //    }
    //}
}]);