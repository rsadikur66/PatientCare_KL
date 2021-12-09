app.service("Q74136Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
        var dataSvc = {
            GetNotificationData: GetNotificationData,
            GetCriterias: GetCriterias
        };
        return dataSvc;

        function GetNotificationData() {
            try {
                var url = vrtlDirr+ '/Q74136/GetNotificationData';
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
        function GetCriterias() {
            try {
                var url = vrtlDirr +'/Q74136/GetCriterias';
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

    }]);