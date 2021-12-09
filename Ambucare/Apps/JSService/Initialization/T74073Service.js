app.service("T74073Service", ["$http", "vrtlDirr", function ($http, vrtlDirr, $q) {

    var dataSvc = {
        insert_t74073: insert_t74073,
        delete_t74073: delete_t74073,
        getLabel: getLabel,
        GetCostTypeData: GetCostTypeData,
        EntryUser: EntryUser,
        insert_t74072: insert_t74072
    };
    return dataSvc;

    function insert_t74073(_t74073) {

        try {
            var url = vrtlDirr + '/T74073/insert_t74073';
            return $http.post(url, _t74073).then(function (results) {
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }
    }

    function insert_t74072(_t74072) {

        try {
            var url = vrtlDirr + '/T74073/insert_t74072';
            return $http.post(url, _t74072).then(function (results) {
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }
    }

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

    //DropDown Start
    function GetCostTypeData() {

        try {
            var url = vrtlDirr + '/T74073/GetCostTypeData';
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
            var url = vrtlDirr + '/T74073/GetLabelData';
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

    function delete_t74073(_T74073) {
        try {
            var url = vrtlDirr + '/T74073/Delete_T74073';
            return $http.post(url, _T74073).then(function (results) {
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }
    }
    
}]);