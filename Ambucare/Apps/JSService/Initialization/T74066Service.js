app.service("T74066Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
    var dataSvc = {
        EntryUser: EntryUser,
        GetRoleList: GetRoleList,
        GetFormTypeList: GetFormTypeList,
        GetPageTypeList: GetPageTypeList,
        GetFormList: GetFormList,
        GetMaxOrderNo: GetMaxOrderNo,
        Insert: Insert,
        GetGridData: GetGridData
    };
    return dataSvc;
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
    function GetRoleList() {

        try {
            var url = vrtlDirr + '/T74066/GetRoleList';
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
    function GetFormTypeList() {

        try {
            var url = vrtlDirr + '/T74066/GetFormTypeList';
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
    function GetPageTypeList() {

        try {
            var url = vrtlDirr + '/T74066/GetPageTypeList';
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
    function GetFormList(e) {

        try {
            var url = vrtlDirr + '/T74066/GetFormList';
            var params = { P_FORM_CODE:e};
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
    function GetMaxOrderNo(rc,ft,pt) {
        try {
            var url = vrtlDirr + '/T74066/GetMaxOrderNo';
            var params = { P_ROLE_CODE:rc,P_FORM_TYPE_ID:ft,P_PAGE_TYPE_ID:pt};
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
    function Insert(e) {
        try {
            var url = vrtlDirr + '/T74066/Insert';
            return $http.post(url, e).then(function (results) {
                return results.data;
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }
    }
    function GetGridData() {

        try {
            var url = vrtlDirr + '/T74066/GetGridData';
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