
app.service("T74037Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
    //For Instance Start 
    var dataSvc = {
        GetCompanyData: GetCompanyData,
        GetEmployeeData: GetEmployeeData
    };
    return dataSvc;

    function GetCompanyData() {
        try {
            var url = vrtlDirr +'/T74037/GetCompanyData';
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
   
    function GetEmployeeData() {
        try {
            var url = vrtlDirr +'/T74037/GetEmployeeData';
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
}]);