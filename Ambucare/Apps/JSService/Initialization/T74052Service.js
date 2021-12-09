
app.service("T74052Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
        var dataSvc = {
            saveData: saveData,
            getZone: getZone,
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

        function saveData(Z) {
       
            try {
                var url = vrtlDirr + '/T74052/AddZone';
                return $http({
                    url: url,
                    method: "POST",
                    data: JSON.stringify(Z)
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
                var url = vrtlDirr + '/T74052/DeleteZone';
                return $http({
                    url: url,
                    method: "POST",
                    data: { T_ZONE_CODE: d }
                }).then(function (results) {

                    return results.data;
                }).catch(function (ex) {
                    throw ex;
                });
            } catch (ex) {
                throw ex;
            }
        }

        function getZone() {
           
            try {
                var url = vrtlDirr + '/T74052/GetZoneData';
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

        //this.addTeacher = function (Teacher) {
        //    var Re = $http({
        //        method: 'Post',
        //        url: 'AddTeacher',
        //        data: JSON.stringify(Teacher)
        //    })
        //    return Re;
        //}
    }
]);