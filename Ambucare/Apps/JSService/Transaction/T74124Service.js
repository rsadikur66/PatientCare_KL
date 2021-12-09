
app.service("T74124Service", ["$http"
    , "vrtlDirr", function ($http, vrtlDirr) {
    //For Instance Start 
    var dataSvc = {
        getAmbulance: getAmbulance,
        getPatint: getPatint
        

    };
    return dataSvc;

    function getAmbulance() {
        try {
            var url = vrtlDirr + '/T74124/GetAmbulance';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                 data: params
                //data: { GEN_CODE: GEN_CODE }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function getPatint(ambId) {
        try {
            var url = vrtlDirr + '/T74124/GetPatint';
            var params = {};
            return $http({
                url: url,
                method: "POST",
               // data: params
                data: { ambId: ambId }
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