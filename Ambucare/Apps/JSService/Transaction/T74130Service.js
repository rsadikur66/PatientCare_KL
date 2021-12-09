app.service("T74130Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {

    var dataSvc = {
        insert_T74130: insert_T74130,
        delete_T74130: delete_T74130,
        print_T74130: print_T74130,
        GetZone: GetZone,
        GetSite: GetSite,
        getAmbPatList_t74130: getAmbPatList_t74130,
        Insert_T74130 : Insert_T74130
    };
    return dataSvc;
    function insert_T74130(_T74130) {
        try {
            var url = vrtlDirr + '/T74130/AddItemBrand';//AddItemBrand from controller T74130Controller
            return $http.post(url, _T74130).then(function (results) {
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }
    }
    function print_T74130() {
        try {
            var url = vrtlDirr + '/T74130/RaporGoster';//PrintItemBrand from controller T74130Controller
            return $http.post(url).then(function (results) {
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }
    }
    function delete_T74130(_T74130) {
        try {
            var url = vrtlDirr + '/T74130/Delete_T74130';//DeleteMedicalData from controller T74009Controller
            return $http.post(url, _T74130).then(function (results) {
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }
    }

    function GetZone() {
        try {
            var url = vrtlDirr + '/T74130/GetZone';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                // data: params
                data: {}
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function GetSite(zone) {
        try {
            var url = vrtlDirr + '/T74130/GetSite';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                // data: params
                data: { zone: zone }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }

    //Dropdown Ambulance Code Function Start
    function getAmbPatList_t74130() {
        try {
            var url = vrtlDirr + '/T74130/getAmbPatList_t74130';
            var params = { };
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
    //Dropdown Ambulance Code Function End


    function Insert_T74130(t96) {
        try {
            var url = vrtlDirr + '/T74130/Insert_T74130';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: { T74130: t96 }
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