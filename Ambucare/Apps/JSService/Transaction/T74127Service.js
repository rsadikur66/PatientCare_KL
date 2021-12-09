
app.service("T74127Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
    //For Instance Start 
    var dataSvc = {
        getEmployee: getEmployee,
        getEmployeeDetails: getEmployeeDetails,
        getItem: getItem,
        saveData: saveData,
        getPreviousProblem: getPreviousProblem


    };
    return dataSvc;

    function getEmployee() {
        try {
            var url = vrtlDirr +'/T74127/GetEmployee';
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

    function getEmployeeDetails(empId) {
        try {
            var url = vrtlDirr +'/T74127/GetEmployeeDetails';
            var params = {};
            return $http({
                url: url,
                method: "POST",
               // data: params
                data: { empId: empId }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function getItem(empId) {
        try {
            var url = vrtlDirr +'/T74127/GetItem';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                // data: params
                data: { empId: empId }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }

    function saveData(T74094, T74095) {
        try {
            var url = vrtlDirr +'/T74127/SaveData';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                // data: params
                data: { T74094, T74095}
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }

    function getPreviousProblem(empId, en, reg) {
        try {
            var url = vrtlDirr +'/T74127/GetPreviousProblem';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                // data: params
                data: { empId: empId, en: en, reg: reg}
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