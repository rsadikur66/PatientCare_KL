
app.service("T74005Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
    var dataSvc = {
        //getZoneCode: getZoneCode,
        SaveData: SaveData,
        getGridData: getGridData,
        Delete: Delete,
        EntryUser: EntryUser
       
    };
    return dataSvc;

        //Entry User

        function EntryUser() {
            try {
                var url = vrtlDirr +'/Accounts/EntryUser';
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

    function SaveData(Emp) {
        
        try {
            var url = vrtlDirr +'/T74005/SaveEmployee';
            return $http({
                url: url,
                method: "POST",
                data: JSON.stringify(Emp)
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
        }

    function getGridData() {
        try {
            var url = vrtlDirr +'/T74005/GetGridData';
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

        function Delete(d) {
            
            try {
                var url = vrtlDirr +'/T74005/DeleteEmType';
                return $http({
                    url: url,
                    method: "POST",
                    data: { T_EMP_TYP_ID: d }
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