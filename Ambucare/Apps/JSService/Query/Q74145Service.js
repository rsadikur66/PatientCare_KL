app.service("Q74145Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
    var dataSvc = {

        GetAllAcceptRequest: GetAllAcceptRequest
        //update_discharged41: update_discharged41
    };
    return dataSvc;
    function GetAllAcceptRequest(from_date, to_date) {
        try {
            var url = vrtlDirr + '/Q74145/GetAllAcceptRequest';
            var params = { from_date: from_date, to_date: to_date };
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
    function update_discharged41(T_REQUEST_ID) {
        try {
            var url = vrtlDirr + '/T74143/update_discharged41';
            var params = { T_REQUEST_ID: T_REQUEST_ID };
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