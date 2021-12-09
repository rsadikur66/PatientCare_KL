app.service("T74054Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {

    var dataSvc = {
        insert_t74054: insert_t74054,
        delete_t74054 : delete_t74054
};
    return dataSvc;
    function insert_t74054(_t74054) {
        try {
            var url = vrtlDirr + '/T74054/AddItemBrand';//AddItemBrand from controller T74054Controller
            return $http.post(url, _t74054).then(function (results) {
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }
    }

    function delete_t74054(_T74054) {
        try {
            var url = vrtlDirr + '/T74054/Delete_T74054';//DeleteMedicalData from controller T74009Controller
            return $http.post(url, _T74054).then(function (results) {
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }
    }
}]);