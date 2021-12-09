
app.service("T74116Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
        //For Instance Start 
        var dataSvc = {
            getPursData: getPursData,
            receiveBy: receiveBy,
            purProduct: purProduct,
            EntryUser: EntryUser,
            SaveData: SaveData,
            GetUom: GetUom

        };
        return dataSvc;

        function getPursData(pursid) {
            try {
                var url = vrtlDirr + '/T74116/GetPursData';
                var params = {};
                return $http({
                    url: url,
                    method: "POST",
                    //data: params
                    data: { Pur_ID: pursid}
                }).then(function (results) {

                    return results.data;
                }).catch(function (ex) {
                    throw ex;
                });
            } catch (ex) {
                throw ex;
            }
        }
        function receiveBy(StorID) {
            try {
                var url = vrtlDirr + '/T74116/GetreceiveBy';
                var params = {};
                return $http({
                    url: url,
                    method: "POST",
                  //  data: params
                    data: { store_ID: StorID }
                }).then(function (results) {

                    return results.data;
                }).catch(function (ex) {
                    throw ex;
                });
            } catch (ex) {
                throw ex;
            }
        }
        function purProduct(pursid) {
            try {
                var url = vrtlDirr + '/T74116/GetpurProduct';
                var params = {};
                return $http({
                    url: url,
                    method: "POST",
                   // data: params
                    data: { Pur_ID: pursid }
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
        //Item UOM Function Start
        function GetUom(pursid) {
            try {
                var url = vrtlDirr + '/T74116/GetUom';
                var params = {};
                return $http({
                    url: url,
                    method: "POST",
                    data: { T_PUR_ID: pursid }
                }).then(function (results) {
                    return results.data;
                }).catch(function (ex) {
                    throw ex;
                });

            } catch (ex) {
                throw ex;
            }
        }
        //Item UOM Function End

        function SaveData(T74060, T74061, T74031, T74030, T74027) {
            try {
                var url = vrtlDirr + '/T74116/SaveData';
                var params = {};
                return $http({
                    url: url,
                    method: "POST",
                    // data: params
                    data: { T74060, T74061, T74031, T74030, T74027 }
                }).then(function (results) {

                    return results.data;
                }).catch(function (ex) {
                    throw ex;
                });
            } catch (ex) {
                throw ex;
            }
        }
    }
]);