app.service("T74003Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
    //For Instance Start 
    var dataSvc = {
        EntryUser: EntryUser,
        getGriddata: getGriddata,
        Insert_T74003: Insert_T74003,
        Deleted_T74003: Deleted_T74003,
        GetTypeDataCode: GetTypeDataCode
       };
    return dataSvc;
    //For Instance End

    function GetTypeDataCode() {

        try {
            var url = vrtlDirr +'/T74003/GetTypeDataCode';
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
    //Entry User Function Start
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
    //Entry User Function End
    //For Grid View Data Get Function  Start 
    function getGriddata() {
        try {
            var url = vrtlDirr +'/T74003/GetGridData';//GetGridData from controller T74003Controller
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
    function Insert_T74003(_T74003) {
        try {
            var url = vrtlDirr +'/T74003/Insert_T74003';//AddItem from controller T74003Controller
            return $http.post(url, _T74003).then(function (results) {
                return results.data;
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }
    }
    //For Save and Update Function  End 
    //For Delete Function Start

    function Deleted_T74003(e) {

        try {
            var url = vrtlDirr +'/T74003/Deleted_T74003';
            return $http({
                url: url,
                method: "POST",
                data: { T_ITEM_UM_ID: e }
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