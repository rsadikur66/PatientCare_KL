app.service("T74001Service", ["$http", "vrtlDirr", function ($http, vrtlDirr,$q) {

    var dataSvc = {
        insert_t74001: insert_t74001,
        delete_t74001: delete_t74001,
        getLabel: getLabel,
        getItemBrandCode: getItemBrandCode,
        getItemUMCode: getItemUMCode,
        getTypeCode: getTypeCode,
        EntryUser: EntryUser,
        insert_t74073: insert_t74073
    };
    return dataSvc;

    function insert_t74001(_t74001) {

        try {
            var url = vrtlDirr +'/T74001/AddItemBrand';
            return $http.post(url, _t74001).then(function (results) {
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }
    }

    function insert_t74073(_t74073) {

        try {
            var url = '/T74001/AddItemBrand73';
            return $http.post(url, _t74073).then(function (results) {
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }
    }

    //Entry User
        function EntryUser() {
            try {
                var url = vrtlDirr +'/Accounts/EntryUser';
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

    //DropDown Start
    function getItemBrandCode() {

        try {
            var url = vrtlDirr +'/T74001/GetItemBrandCode';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data:params
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }

    }

    function getItemUMCode(Type) {

        try {
            var url = vrtlDirr +'/T74001/GetItemUMCode';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: { T_TYPE_ID: Type }
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }

    }

    function getTypeCode() {

        try {
            var url = vrtlDirr +'/T74001/GetTypeCode';
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
    //DropDown End

    function getLabel() {
        try {
            var url = vrtlDirr +'/T74001/GetLabelData';
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

    function delete_t74001(_T74001) {
        try {
            var url = vrtlDirr +'/T74001/Delete_T74001';
            return $http.post(url, _T74001).then(function (results) {
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }
    }
}]);