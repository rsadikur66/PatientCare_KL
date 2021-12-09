app.service("T74009Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {

    var dataSvc = {
        insert_t74009: insert_t74009,
        delete_t74009: delete_t74009
    };
    return dataSvc;
    function insert_t74009(_T74009) {
        try {
            var url = vrtlDirr + '/T74009/AddMedicalData';//AddMedicalData from controller T74009Controller
            return $http.post(url, _T74009).then(function (results) {
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }
    }

    function delete_t74009(_T74009) {
        try {
            var url = vrtlDirr + '/T74009/Delete_T74009';//DeleteMedicalData from controller T74009Controller
            return $http.post(url, _T74009).then(function (results) {
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }
    }
}]);