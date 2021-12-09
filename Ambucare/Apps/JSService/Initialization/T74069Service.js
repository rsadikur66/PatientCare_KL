app.service("T74069Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
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

        function SaveData(Blood) {
       
            try {
                var url = vrtlDirr + '/T74069/SaveBloodGroup';
                return $http({
                    url: url,
                    method: "POST",
                    data: JSON.stringify(Blood)
                }).then(function (results) {
                    return results.data;
                }).catch(function (ex) {
                    throw ex;
                });
            } catch (ex) {
                throw ex;
            }
        }
        //=========
        function getGridData() {
            try {
                var url = vrtlDirr + '/T74069/GetGridData';
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
    //=========

    function Delete(blood) {
        try {
            var url = vrtlDirr + '/T74069/Delete';
            return $http({
                url: url,
                method: "POST",
                data: { blood: blood }
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
