app.service("T74025Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {

    var dataSvc = {
        insert_t74025: insert_t74025,
        delete_t74025: delete_t74025
    };
    return dataSvc;
    function insert_t74025(_t74025) {
        try {
            var url = vrtlDirr + '/T74025/Insert_T74025';
            return $http.post(url, _t74025).then(function (results) {
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }
    }

    function delete_t74025(_t74025) {
        try {
            var url = vrtlDirr + '/T74025/Delete_T74025';
            return $http.post(url, _t74025).then(function (results) {
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }
    }
}]);