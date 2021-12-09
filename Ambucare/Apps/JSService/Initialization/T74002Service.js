app.service("T74002Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {

    var dataSvc = {
        insert_t74002: insert_t74002,
        delete_t74002: delete_t74002,
        print_t74002: print_t74002
    };
    return dataSvc;
    function insert_t74002(_t74002) {
        try {
            var url = vrtlDirr +'/T74002/AddItemBrand';//AddItemBrand from controller T74002Controller
            return $http.post(url, _t74002).then(function (results) {
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }
    }
    function print_t74002() {
        try {
            var url = vrtlDirr +'/T74002/RaporGoster';//PrintItemBrand from controller T74002Controller
            return $http.post(url).then(function (results) {
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }
    }
    function delete_t74002(_T74002) {
        try {
            var url = vrtlDirr +'/T74002/Delete_T74002';//DeleteMedicalData from controller T74009Controller
            return $http.post(url, _T74002).then(function (results) {
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }
    }
}]);