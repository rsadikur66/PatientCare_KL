app.service("T74028Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {

    var dataSvc = {
        insert_t74028: insert_t74028,
        delete_t74028: delete_t74028,
        GetEmployeeData: GetEmployeeData
    };
    return dataSvc;
    function insert_t74028(_t74028) {
        try {
            var url = vrtlDirr +'/T74028/Add_T74028';//AddItemBrand from controller T74028Controller
            return $http.post(url, _t74028).then(function (results) {
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }
    }

    function delete_t74028(_T74028) {
        try {
            var url = vrtlDirr +'/T74028/Delete_T74028';//DeleteMedicalData from controller T74009Controller
            return $http.post(url, _T74028).then(function (results) {
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }
    }

     //Employee Popup Data Function Start
    function GetEmployeeData() {
        try {
            var url = vrtlDirr +'/T74014/GetEmployeeData';
            //alert("IUHIH");
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
    //Employee Popup Data Function End
}]);