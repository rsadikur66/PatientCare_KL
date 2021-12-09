app.service("T74145Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
        var dataSvc = {
            getGridData: getGridData,
            Insert: Insert,
            Update: Update
        };
        return dataSvc;

        function getGridData() {
            try {
                var url = vrtlDirr + '/T74145/getGridData';
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
        function Insert(T_LANG1_NAME, T_LANG2_NAME) {

            try {
                var url = vrtlDirr + '/T74145/Insert';
                return $http({
                    url: url,
                    method: "POST",
                    data: {T_LANG1_NAME: T_LANG1_NAME, T_LANG2_NAME: T_LANG2_NAME}
                }).then(function (results) {

                    return results.data;
                }).catch(function (ex) {
                    throw ex;
                });
            } catch (ex) {
                throw ex;
            }
        }
        function Update(T_DISCH_ID, T_LANG1_NAME, T_LANG2_NAME) {

            try {
                var url = vrtlDirr + '/T74145/update';
                return $http({
                    url: url,
                    method: "POST",
                    data: { T_DISCH_ID: T_DISCH_ID, T_LANG1_NAME: T_LANG1_NAME, T_LANG2_NAME: T_LANG2_NAME}
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