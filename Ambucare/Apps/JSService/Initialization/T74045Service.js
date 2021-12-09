app.service("T74045Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {

    //For Instance Start
    var dataSvc = {
        EntryUser: EntryUser,
        getGriddata: getGriddata,
        Insert_T74045: Insert_T74045,
        Deleted_T74045: Deleted_T74045
    };
    return dataSvc;
    //For Instance End

    //Entry User Function Start
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
    //Entry User Function End

    //For Grid View Data Get Function  Start 
    function getGriddata() {
        try {
            var url = vrtlDirr + '/T74045/GetGridData';//GetGridData from controller T74045Controller
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
    //For Grid View Data Get Function  End
    //For Save and Update Function  Start 
    function Insert_T74045(_T74045) {
        try {
            var url = vrtlDirr + '/T74045/Insert_T74045';//AddItem from controller T74045Controller
            return $http.post(url, _T74045).then(function (results) {
                return results.data;
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }
    }
    //For Save and Update Function  End 
    //For Delete Function Start

    function Deleted_T74045(e) {

        try {
            var url = vrtlDirr + '/T74045/Deleted_T74045';
            return $http({
                url: url,
                method: "POST",
                data: { T_SUPPLIER_ID: e }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    //For Delete Function End

}]);