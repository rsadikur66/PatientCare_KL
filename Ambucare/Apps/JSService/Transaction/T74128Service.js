
app.service("T74128Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
    //For Instance Start 
    var dataSvc = {
        //getEmployee: getEmployee,
        getPrice: getPrice,
        getStock: getStock,
        getPackSize: getPackSize,
        GetTrade: GetTrade,
        getVat: getVat,
        getPatienRequest: getPatienRequest,
        saveData: saveData


    };
    return dataSvc;
    function GetTrade() {
        try {
            var url = vrtlDirr + '/T74128/GetTrade';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                 data: params
               // data: { empId: empId }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function getPackSize(cosTyDel, storeid) {
        try {
            var url = vrtlDirr + '/T74128/GetPackSize';
            var params = {};
            return $http({
                url: url,
                method: "POST",
              //  data: params
                data: { cosTyDel: cosTyDel, storeid: storeid }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }

    function getStock(item,store,umId) {
        try {
            var url = vrtlDirr + '/T74128/GetStock';
            var params = {};
            return $http({
                url: url,
                method: "POST",
               // data: params
                data: { item: item, store:store,umId: umId }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function getPrice(costype, umId) {
        try {
            var url = vrtlDirr + '/T74128/GetPrice';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                // data: params
                data: { costype: costype, umId: umId }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function getVat() {
        try {
            var url = vrtlDirr + '/T74128/GetVat';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                 data: params
               // data: { costype: costype, umId: umId }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function getPatienRequest() {
        try {
            var url = vrtlDirr + '/T74128/GetPatienRequest';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: params
                // data: { costype: costype, umId: umId }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function saveData(t36,t37) {
        try {
            var url = vrtlDirr + '/T74128/SaveData';
            var params = {};
            return $http({
                url: url,
                method: "POST",
               // data: params
                data: { t36: t36, t37: t37 }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
}])