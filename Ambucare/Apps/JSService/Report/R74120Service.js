
app.service("R74120Service", ["$http", "vrtlDirr", function ($http, vrtlDirr, $q) {

    var dataSvc = {
        GetStoreListTo: GetStoreListTo,
        GetGridData: GetGridData
    };
    return dataSvc;

    //Dropdown Store To Function Start
    function GetStoreListTo() {
        try {
            var url = vrtlDirr + "/R74120/GetStoreListTo";
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
    //For Grid data Function Start
    function GetGridData() {
        try {

            var url = vrtlDirr + '/R74120/GetGridData';//GetGridData from controller T74014Controller
            //alert("IUHIH");
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