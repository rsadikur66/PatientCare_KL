app.service("T74018Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
    var dataSvc = {
        GetLabelData: GetLabelData,
        EntryUser: EntryUser,
        Insert: Insert,
        Delete: Delete,
        GetCostTypeData: GetCostTypeData
    };
    return dataSvc;
   
    //DropDown Start
    function GetCostTypeData() {

        try {
            var url = vrtlDirr + '/T74018/GetCostTypeData';
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

    function GetLabelData() {
        try {
            var url = vrtlDirr + '/T74018/GetLabelData';
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

    function Insert(e) {
        try {
            var url = vrtlDirr + '/T74018/Insert';//AddItem from controller T74013Controller
            return $http.post(url, e).then(function (results) {
                return results.data;
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }
    }
    function Delete(e) {
        try {
            var url = vrtlDirr + '/T74018/Delete';
            return $http({
                url: url,
                method: "POST",
                data: { T_ID: e }
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