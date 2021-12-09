app.service("T74024Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {

    var dataSvc = {
        insert_t74024: insert_t74024,
        delete_t74024: delete_t74024,
        EntryUser: EntryUser,
        getlabeldata: getlabeldata
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

    function getlabeldata() {

        try {
            var url = vrtlDirr + '/T74021/GetLabelData';
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

    function insert_t74024(_t74024) {
        try {
            var url = vrtlDirr + '/T74024/Add_t74024';
            return $http.post(url, _t74024).then(function (results) {
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }
    }

    function delete_t74024(_t74024) {
        try {
            var url = vrtlDirr + '/T74024/Delete_T74024';
            return $http.post(url, _t74024).then(function (results) {
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }
    }
}]);