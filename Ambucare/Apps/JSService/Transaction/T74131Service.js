app.service("T74131Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
    var dataSvc = {
        GetSite: GetSite,
        GetPatInfo: GetPatInfo,
        GetPreviousMedicine: GetPreviousMedicine,
        chatHistory: chatHistory,
        GetChatHistory: GetChatHistory,
        setDoc: setDoc,
        getItem: getItem,
        GetAllUserLatlong: GetAllUserLatlong,
        saveSuggestedMedicine: saveSuggestedMedicine,
        saveRequestHospital: saveRequestHospital,
        save26: save26,
        chatFileUpload: chatFileUpload,
        UserName: UserName,
        UserId: UserId,
        getlabeldata: getlabeldata,
        getConsoleApp: getConsoleApp
        //,saveTutorial: saveTutorial
        , reqListForTeam: reqListForTeam,
        reqListForDoc: reqListForDoc
        , acptReqofDoc: acptReqofDoc
        , onRcvPatReq: onRcvPatReq
        , closeChat: closeChat
        , conWthDoc: conWthDoc
        , chkConn: chkConn
        , getDocID: getDocID
        , chkReqHos: chkReqHos
        , sendREq: sendREq,
        getAllTeamData: getAllTeamData
        , setDoc_new: setDoc_new
        , setNewConv: setNewConv
        , emergenceyReq: emergenceyReq
        , getPatMsg: getPatMsg
      , chkPat: chkPat,
      getServiceData: getServiceData
        //, getPlaceHolder: getPlaceHolder

    };
    return dataSvc;

    function GetSite() {
        try {
            var url = vrtlDirr + '/T74131/GetSite';
            var params = {};
            return $http({
                url: url,
                method: "POST",
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
  
     
    function GetPatInfo(m) {
        try {
            var url = vrtlDirr + '/T74131/GetPatInfo';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: { P_USER_ID: m }
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function GetPreviousMedicine(m) {
        try {
            var url = vrtlDirr + '/T74131/GetPreviousMedicine';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: { requId: m }
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function chatHistory(m) {
        try {
            var url = vrtlDirr + '/T74131/chatHistory';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: { t74098: m }
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function GetChatHistory(req,s,r) {
        try {
            var url = vrtlDirr + '/T74131/GetChatHistory';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: { T_REQUEST_ID:req,T_U_ID: s, TYPE:r }
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function setDoc(s, r) {
        try {
            var url = vrtlDirr + '/T74131/setDoc';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: { }
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function getItem(ambu) {
        try {
            var url = vrtlDirr + '/T74134/GetItem';
            return $http({
                url: url,
                method: "POST",
                data: { ambu: ambu }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function GetAllUserLatlong() {
        try {
            var url = vrtlDirr + '/T74138/GetAllUserLatlong';
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

    
    function saveSuggestedMedicine(requestID,save40List) {
        try {
            var url = vrtlDirr + '/T74131/SaveSuggestedMedicine';
            //var params =
            return $http({
                url: url,
                method: "POST",
                data: { requestID: requestID, save40List: save40List }
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function saveRequestHospital(requestID, hosID) {
        try {
            var url = vrtlDirr + '/T74131/saveRequestHospital';
            //var params =
            return $http({
                url: url,
                method: "POST",
                data: { requestID: requestID, hosID: hosID }
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function save26(t74026) {
        try {
            var url = vrtlDirr + '/Menu/SaveLatLong';
            return $http({
                url: url,
                method: "POST",
                data: { t74026: t74026 }
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function chatFileUpload(e,f,ext) {
        try {
            var url = vrtlDirr + '/T74131/chatFileUpload';
            return $http({
                url: url,
                method: "POST",
                data: { t74098: e,b64:f,ext:ext }
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    //function saveTutorial(tutorial) {
    //    return akFileUploaderService.saveModel(tutorial, "/T74131/chatFileUpload");
    //};
    function UserName() {
        try {
            var url = vrtlDirr + '/Menu/UserName';
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
    function UserId() {
        try {
            var url = vrtlDirr + '/Menu/UserId';
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
    function getlabeldata(formcode) {
        try {
            var url = vrtlDirr + '/T74000/GetLabelTextData';
            //alert("IUHIH");
            var params = { T_FORM_CODE: formcode };
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
    function getConsoleApp() {
        try {
            var url = vrtlDirr + '/T74131/getConsoleApp';
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
    //---------------------------------------For Connectivity---------------------------
    function reqListForTeam() {
        try {
            var url = vrtlDirr + '/T74131/reqListForTeam';
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
    function reqListForDoc() {
        try {
            var url = vrtlDirr + '/T74131/reqListForDoc';
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
    function acptReqofDoc() {
        try {
            var url = vrtlDirr + '/T74131/acptReqofDoc';
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
    function onRcvPatReq(r) {
        try {
            var url = vrtlDirr + '/T74131/onRcvPatReq';
            var params = { req: r };
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
    function closeChat(r) {
        try {
            var url = vrtlDirr + '/T74131/closeChat';
            var params = { req: r };
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
    function conWthDoc(r) {
        try {
            var url = vrtlDirr + '/T74131/conWthDoc';
            var params = { req: r };
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
    function chkConn() {
        try {
            var url = vrtlDirr + '/T74131/chkConn';
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
    function getDocID() {
        try {
            var url = vrtlDirr + '/T74131/getDocID';
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
    function chkReqHos(r) {
        try {
            var url = vrtlDirr + '/T74131/chkReqHos';
            var params = { requestId:r};
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
    function sendREq(r) {
        try {
            var url = vrtlDirr + '/T74131/sendREq';
            var params = { T_REQUEST_ID:r};
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
    function getAllTeamData() {
        try {
            var url = vrtlDirr + '/Q74144/GetAllTeamData';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: params //JSON.stringify()
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function setDoc_new(req, user) {
        try {
            var url = vrtlDirr + '/T74131/setDoc_new';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: { req:req, user:user}
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function setNewConv(req) {
        try {
            var url = vrtlDirr + '/T74131/setNewConv';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: { req: req}
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function emergenceyReq(text) {
        try {
            var url = vrtlDirr + '/T74131/emergenceyReq';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: { text: text }
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function getPatMsg(e) {
        try {
            var url = vrtlDirr + '/T74131/getPatMsg';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: { req: e }
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function chkPat(e) {
        try {
            var url = vrtlDirr + '/T74131/chkPat';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: {req:e}
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
  }

    function getServiceData(requID) {
        try {
            var url = vrtlDirr + '/T74134/GetServiceData';
            return $http({
                url: url,
                method: "POST",
                data: { requID: requID }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    //function getPlaceHolder(e) {
    //    try {
    //        var url = vrtlDirr + '/T74131/getPlaceHolder';
    //        var params = { e: label};
    //        return $http({
    //            url: url,
    //            method: "POST",
    //            data: { req: e }
    //        }).then(function (results) {
    //            return results.data;
    //        }).catch(function (ex) {
    //            throw ex;
    //        });
    //    } catch (ex) {
    //        throw ex;
    //    }
    //}
}]);