app.service("T74015Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
    var dataSvc = {
        GetGridData: GetGridData,
        insert_T74015: insert_T74015,
        insert_t74073: insert_t74073,
        Del_T74015: Del_T74015,
        getAmbulanceDropdownList: getAmbulanceDropdownList,
      getEmployeeTypeIdAndEmployeeIdByAmbulanceId: getEmployeeTypeIdAndEmployeeIdByAmbulanceId,
        getemployeedata: getemployeedata,
        getGridEmployeeTypeData: getGridEmployeeTypeData
    };
    return dataSvc;
    function getGridEmployeeTypeData(e) {
        try {
            var url = vrtlDirr+ '/T74015/getGridEmployeeTypeData';
            var params = { T_AMBU_REG_ID: e};
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
    function getemployeedata() {
        try {
          var url = vrtlDirr+'/T74014/GetEmployeeData';
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
    function GetGridData() {
        try {
            var url = vrtlDirr +'/T74015/GetGridData';
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
   
    function insert_T74015(_T74015) {
        try {
            var url = vrtlDirr +'/T74015/Add_T74015';//AddItemBrand from controller T74002Controller
            return $http.post(url, _T74015).then(function (results) {
                return results.data;
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }
    }
    function insert_t74073(_t74073) {
        try {
            var url = vrtlDirr +'/T74015/Add_t74073';//AddItemBrand from controller T74002Controller
            return $http.post(url, _t74073).then(function (results) {
                return results.data;
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }
    }

    function Del_T74015(e) {

        try {
            var url = vrtlDirr +'/T74015/Del_T74015';
            return $http({
                url: url,
                method: "POST",
                data: { _T74015Del: e }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }

    //Dropdown Ambulance Code Function Start
    function getAmbulanceDropdownList() {
        try {
            var url = vrtlDirr +'/T74044/GetAmbulanceDropdownList';
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
    //Dropdown Ambulance Code Function End


    //Get EmployeeType and Employee Information by AmbulanceId
    function getEmployeeTypeIdAndEmployeeIdByAmbulanceId(e) {
        try {
            var url = vrtlDirr +'/T74015/GetEmployeeTypeId';
            var params = { T_AMBU_REG_ID : e};
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