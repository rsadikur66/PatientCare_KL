
app.service("T74121Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
    //For Instance Start 
    var dataSvc = {
        GetProductList: GetProductList,
        getUomList: getUomList,
        genericData: genericData,
        getTypeData: getTypeData,
        getProTypeData: getProTypeData,
        saveData: saveData
        
    };
    return dataSvc;

   
    function GetProductList(GEN_CODE) {
            try {
                var url = vrtlDirr + '/T74121/GetProductList';
                var params = {};
                return $http({
                    url: url,
                    method: "POST",
                   // data: params
                    data: { GEN_CODE: GEN_CODE }
                }).then(function (results) {

                    return results.data;
                }).catch(function (ex) {
                    throw ex;
                });
            } catch (ex) {
                throw ex;
            }
        }
    
    function getUomList(TypeId, GEN_CODE, TRADE_CODE) {
        try {
            var url = vrtlDirr + '/T74121/GetUomtList';
            var params = {};
            return $http({
                url: url,
                method: "POST",
               // data: params
                data: { TypeId: TypeId, GEN_CODE: GEN_CODE, TRADE_CODE: TRADE_CODE }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function genericData() {
        try {
            var url = vrtlDirr + '/T74121/GenericData';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                 data: params
               // data: { Uom_ID: Uom }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function getTypeData() {
        try {
            var url = vrtlDirr + '/T74121/GetTypeData';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: params
                // data: { Uom_ID: Uom }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }

    function getProTypeData(id) {
        try {
            var url = vrtlDirr + '/T74121/GetProTypeData';
            var params = {};
            return $http({
                url: url,
                method: "POST",
               // data: params
                data: { Type_id: id }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function saveData(T74089) {
        try {
            var url = vrtlDirr + '/T74121/SaveData';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                // data: params
                data: { T74089}
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