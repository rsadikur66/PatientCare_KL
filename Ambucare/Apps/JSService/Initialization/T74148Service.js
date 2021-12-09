app.service("T74148Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
        var dataSvc = {
            getGridData: getGridData,
            InsertOrUpdate: InsertOrUpdate
        };
        return dataSvc;

        function getGridData() {
            try {
                var url = vrtlDirr + '/T74148/getGridAllData';
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

        function InsertOrUpdate(_t02040) {

            try {
                var url = vrtlDirr + '/T74148/InsertOrUpdate';
                return $http({
                    url: url,
                    method: "POST",
                    data: JSON.stringify(_t02040)
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