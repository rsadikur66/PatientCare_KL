
app.service("Q74146Service", ["$http", "vrtlDirr", function ($http, vrtlDirr, $q) {

    var dataSvc = {
        GetAmbulanceList: GetAmbulanceList,
        GetStockItem: GetStockItem,
        getStorId: getStorId
    };
    return dataSvc;

    //Dropdown Store To Function Start
    function GetAmbulanceList() {
        try {
            var url = vrtlDirr + "/Q74146/GetAmbulanceList";
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
    function GetStockItem(ambuId) {
        try {

            var url = vrtlDirr + '/Q74146/GetStockItem';//GetGridData from controller T74014Controller
            //alert("IUHIH");
            var params = { ambuId: ambuId};
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
    function getStorId(ambuId) {
        try {

            var url = vrtlDirr + '/Q74146/GetStorId';//GetGridData from controller T74014Controller
            //alert("IUHIH");
            var params = { ambuId: ambuId };
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