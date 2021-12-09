app.service("T74096Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {

    var dataSvc = {
        insert_T74096: insert_T74096,
        delete_T74096: delete_T74096,
        print_T74096: print_T74096,
        GetZone: GetZone,
        GetSite: GetSite,
        getAmbPatList: getAmbPatList,
        Insert_T74096: Insert_T74096
    };
    return dataSvc;
    function insert_T74096(_T74096) {
        try {
            var url = vrtlDirr + '/T74096/AddItemBrand';//AddItemBrand from controller T74096Controller
            return $http.post(url, _T74096).then(function (results) {
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }
    }
    function print_T74096() {
        try {
            var url = vrtlDirr + '/T74096/RaporGoster';//PrintItemBrand from controller T74096Controller
            return $http.post(url).then(function (results) {
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }
    }
    function delete_T74096(_T74096) {
        try {
            var url = vrtlDirr + '/T74096/Delete_T74096';//DeleteMedicalData from controller T74009Controller
            return $http.post(url, _T74096).then(function (results) {
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }
    }

    function GetZone() {
        try {
            var url = vrtlDirr + '/T74096/GetZone';
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
            var url = vrtlDirr + '/T74096/GetSite';
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
    function getAmbPatList(siteCode) {
        try {
            var url = vrtlDirr + '/T74096/GetAmbPatList';
            var params = { T_SITE_CODE:siteCode};
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

    function Insert_T74096(t96) {
        try {
            var url = vrtlDirr + '/T74096/Insert_T74096';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: { T74096: t96 }
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