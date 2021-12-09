app.service("Q74001Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
        var dataSvc = {
            GetUserIDByAMBRegID: GetUserIDByAMBRegID,
            getActiveAmbulance: getActiveAmbulance,
            getDischargeAmbulance: getDischargeAmbulance,
            getallPatients: getallPatients,
            getwaitingAmbulance: getwaitingAmbulance,
            getMaleAmbulance: getMaleAmbulance,
            getfemalAmbulance: getfemalAmbulance

        };
        return dataSvc;

        function GetUserIDByAMBRegID(e) {
            try {
                var url = vrtlDirr + '/Q74001/GetUserIDByAMBRegID';
                var params = {};
                return $http({
                    url: url,
                    method: "POST",
                    data: JSON.stringify()
                }).then(function(results) {
                    return results.data;
                }).catch(function(ex) {
                    throw ex;
                });
            } catch (ex) {
                throw ex;
            }
        }

        function getActiveAmbulance() {
            try {
                var url = vrtlDirr + '/Q74001/GetActiveAmbulance';
                var params = {};
                return $http({
                    url: url,
                    method: "POST",
                    data: params
                   // data: { Id: patId }
                }).then(function (results) {

                    return results.data;
                }).catch(function (ex) {
                    throw ex;
                });
            } catch (ex) {
                throw ex;
            }
        }
        function getDischargeAmbulance() {
            try {
                var url = vrtlDirr + '/Q74001/GetDischargeAmbulance';
                var params = {};
                return $http({
                    url: url,
                    method: "POST",
                    data: params
                    // data: { Id: patId }
                }).then(function (results) {

                    return results.data;
                }).catch(function (ex) {
                    throw ex;
                });
            } catch (ex) {
                throw ex;
            }
        }
        function getallPatients() {
            try {
                var url = vrtlDirr + '/Q74001/GetallPatients';
                var params = {};
                return $http({
                    url: url,
                    method: "POST",
                    data: params
                    // data: { Id: patId }
                }).then(function (results) {

                    return results.data;
                }).catch(function (ex) {
                    throw ex;
                });
            } catch (ex) {
                throw ex;
            }
        }
        function getwaitingAmbulance() {
            try {
                var url = vrtlDirr + '/Q74001/WetwaitingAmbulance';
                var params = {};
                return $http({
                    url: url,
                    method: "POST",
                    data: params
                    // data: { Id: patId }
                }).then(function (results) {

                    return results.data;
                }).catch(function (ex) {
                    throw ex;
                });
            } catch (ex) {
                throw ex;
            }
        }
        function getMaleAmbulance() {
            try {
                var url = vrtlDirr + '/Q74001/GetMaleAmbulance';
                var params = {};
                return $http({
                    url: url,
                    method: "POST",
                    data: params
                    // data: { Id: patId }
                }).then(function (results) {

                    return results.data;
                }).catch(function (ex) {
                    throw ex;
                });
            } catch (ex) {
                throw ex;
            }
        }
        function getfemalAmbulance() {
            try {
                var url = vrtlDirr + '/Q74001/GetfemalAmbulance';
                var params = {};
                return $http({
                    url: url,
                    method: "POST",
                    data: params
                    // data: { Id: patId }
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