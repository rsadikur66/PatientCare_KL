app.service("T74056Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {

    var dataSvc = {
        Insert: Insert,
        Delete: Delete,
        EntryUser: EntryUser
    };
    return dataSvc;
    function Insert(obj) {
        try {
            var url = vrtlDirr + '/T74056/Insert';
            return $http.post(url, obj).then(function (results) {
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }
    }


    function Delete(e) {
        try {
            var url = vrtlDirr + '/T74056/Delete';
            var params = { T_TRIAGE_SEQ: e };
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
}]);