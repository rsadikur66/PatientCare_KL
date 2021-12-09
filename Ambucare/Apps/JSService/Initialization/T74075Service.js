app.service("T74075Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
        var dataSvc = {
            InsertOrUpdate: InsertOrUpdate,
            getLabel: getLabel,
            Delete: Delete,
            EntryUser: EntryUser
        };
        return dataSvc;
        //Entry User
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

        function InsertOrUpdate(_T74075) {

            try {
                var url = vrtlDirr + '/T74075/InsertOrUpdate';
                return $http({
                    url: url,
                    method: "POST",
                    data: JSON.stringify(_T74075)
                }).then(function (results) {

                    return results.data;
                }).catch(function (ex) {
                    throw ex;
                });
            } catch (ex) {
                throw ex;
            }
        }

        function getLabel() {

            try {
                var url = vrtlDirr + '/T74075/GetLabelData';
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

        function Delete(e) {

            try {
                var url = vrtlDirr + '/T74075/Delete';
                return $http({
                    url: url,
                    method: "POST",
                    data: { T_FREQUENCY_CODE: e }
                }).then(function (results) {

                    return results.data;
                }).catch(function (ex) {
                    throw ex;
                });
            } catch (ex) {
                throw ex;
            }
        }

    }
]);