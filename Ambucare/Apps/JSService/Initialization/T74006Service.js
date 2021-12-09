

app.service("T74006Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
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


        function SaveData(EmpDes) {
            
            try {
                var url = vrtlDirr +'/T74006/SaveEmpDesignation';
                return $http({
                    url: url,
                    method: "POST",
                    data: JSON.stringify(EmpDes)
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
                var url = vrtlDirr +'/T74006/DeleteEmp';
                return $http({
                    url: url,
                    method: "POST",
                    data: { T_EMP_DESI_ID: d }
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
                var url = vrtlDirr +'/T74006/GetGridData';
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
    }
]);