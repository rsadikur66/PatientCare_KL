
app.service("Q74144Service", ["$http", "vrtlDirr", function($http, vrtlDirr) {
    var dataSvc = {
        getAllTeamData: getAllTeamData
        , getPatMsg: getPatMsg,
        getlabeldata: getlabeldata
    
    };
    return dataSvc;

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
}])