app.service("T74016Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {

    //For Instance Start
    var dataSvc = {
        EntryUser: EntryUser,
        getGriddata: getGriddata,
        Insert_T74016: Insert_T74016,
        Deleted_T74016: Deleted_T74016
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
            var url = vrtlDirr + '/T74016/GetGridData';//GetGridData from controller T74016Controller
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
    function Insert_T74016(_T74016) {
        try {
            var url = vrtlDirr + '/T74016/Insert_T74016';//AddItem from controller T74016Controller
            return $http.post(url, _T74016).then(function (results) {
                return results.data;
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }
    }
    //For Save and Update Function  End 
    //For Delete Function Start

    function Deleted_T74016(e) {

        try {
            var url = vrtlDirr + '/T74016/Deleted_T74016';
            return $http({
                url: url,
                method: "POST",
                data: { T_CURRENCY_ID: e }
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