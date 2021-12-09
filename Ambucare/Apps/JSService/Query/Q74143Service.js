app.service("Q74143Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
        var dataSvc = {
            GetNotificationData: GetNotificationData,
            update_loggedOut57: update_loggedOut57,
            update_discharged41: update_discharged41
        };
        return dataSvc;

        function GetNotificationData() {
            try {
                var url = vrtlDirr + '/T74143/getAllGridData';
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

        function update_loggedOut57(T_USER_ID) {
            try {
                var url = vrtlDirr + '/T74143/update_loggedOut57';
                var params = { T_USER_ID: T_USER_ID };
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