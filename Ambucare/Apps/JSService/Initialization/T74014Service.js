app.service("T74014Service", ["$http", "vrtlDirr", "akFileUploaderService", function ($http, vrtlDirr, akFileUploaderService) {
    //For Instance Start 
    var dataSvc = {
        EntryUser: EntryUser,
        getGriddata: getGriddata,
        Insert_T74014: Insert_T74014,
        Deleted_T74014: Deleted_T74014,
        GetColorId: GetColorId,
        GetAmbulanceId: GetAmbulanceId,
        GetBrandId: GetBrandId,
        GetEmployeeData: GetEmployeeData,
        GetImageData: GetImageData,
        saveTutorial: saveTutorial,
        getRegNo: getRegNo,
        getChesisNo: getChesisNo,
        getEngineNo: getEngineNo,
        getAmbulanceOwnerData: getAmbulanceOwnerData
    };
    return dataSvc;
    //For Instance End 
    function saveTutorial(tutorial) {
        return akFileUploaderService.saveModel(tutorial, vrtlDirr + "/T74014/Insert_T74014");
    };
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

            var url = vrtlDirr + '/T74014/GetGridData';//GetGridData from controller T74014Controller
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

    function GetImageData(T_BASE64_DATA) {
        try {

            var url = vrtlDirr + '/T74014/ImagetoBase64';//GetGridData from controller T74014Controller
            //alert("IUHIH");
            var params = { T_BASE64_DATA: T_BASE64_DATA };
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
    function Insert_T74014(_T74014) {
        try {
            var url = vrtlDirr + '/T74014/Insert_T74014';//AddItem from controller T74014Controller
            return $http.post(url, _T74014).then(function (results) {
                return results.data;
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }

    }
    //For Save and Update Function  End 
    //For Delete Function Start

    function Deleted_T74014(e) {

        try {
            var url = vrtlDirr + '/T74014/Deleted_T74014';
            return $http({
                url: url,
                method: "POST",
                data: { T_AMBU_REG_ID: e }
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

    //Dropdown Color Code Function Start
    function GetColorId() {
        try {
            var url = vrtlDirr + '/T74014/GetColorId';
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
    //Dropdown Color Code Function End

    //Dropdown Ambulance Type Id Function Start
    function GetAmbulanceId() {
        try {
            var url = vrtlDirr + '/T74014/GetAmbulanceId';
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
    //Dropdown Ambulance Type Id Function End

    //Dropdown Employee Type Id Function Start
    function GetBrandId() {
        try {
            var url = vrtlDirr + '/T74014/GetBrandId';
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
    //Dropdown Employee Type Id Function End
    //Employee Popup Data Function Start
    function GetEmployeeData() {
        try {
            var url = vrtlDirr + '/T74014/GetEmployeeData';
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
    //Employee Popup Data Function End
    function getRegNo(regNo) {
        try {
            var url = vrtlDirr + '/T74014/GetRegNo';
            //alert("IUHIH");
            var params = { regNo: regNo };
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
    function getChesisNo(chesisNo) {
        try {
            var url = vrtlDirr + '/T74014/GetChesisNo';
            //alert("IUHIH");
            var params = { chesisNo: chesisNo };
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
    function getEngineNo(engineNo) {
        try {
            var url = vrtlDirr + '/T74014/GetEngineNo';
            //alert("IUHIH");
            var params = { engineNo: engineNo };
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
    function getAmbulanceOwnerData() {
        try {
            var url = vrtlDirr + '/T74014/getAmbulanceOwnerData';
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
}]);