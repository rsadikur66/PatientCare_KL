app.service("T74111Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
    //For Instance Start 
    var dataSvc = {
        getAmbulanceDropdownList: getAmbulanceDropdownList,
        GetGridData: GetGridData,
        EntryUser: EntryUser,
        Insert_T74073: Insert_T74073
    };
    return dataSvc;
    //For Instance End 

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
    
    //For Save and Update Function  Start 
    function Insert_T74073(_T74073) {
        try {
            var url = vrtlDirr + '/T74111/Insert_T74073';//AddItem from controller T74073Controller
            return $http.post(url, _T74073).then(function (results) {
                return results.data;
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }
    }
    //For Save and Update Function  End 

    //Dropdown Ambulance Code Function Start
    function getAmbulanceDropdownList() {
        try {
            var url = vrtlDirr + '/T74111/GetAmbulanceDropdownList';
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
    //Dropdown Ambulance Code Function End

    //Ambulance type Data Function start
    function GetGridData(e) {
        try {
            var url = vrtlDirr + '/T74111/GetGridData';
            return $http({
                url: url,
                method: "POST",
                data: { T_COST_TYPE_ID: e }
             }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    //Ambulance type Data Function End

   

}]);