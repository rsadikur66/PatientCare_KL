app.service("T74010Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
        var dataSvc = {
            InsertOrUpdate: InsertOrUpdate,
            getLabel: getLabel,
            deleteData: deleteData,
            EntryUser: EntryUser
        };
        return dataSvc;
     //Entry User
        function EntryUser() {
            try {
                var url = vrtlDirr +'/Accounts/EntryUser';
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

        function InsertOrUpdate(Supplier) {

            try {
                var url = vrtlDirr +'/T74010/InsertOrUpdate';
                return $http({
                    url: url,
                    method: "POST",
                    data: JSON.stringify(Supplier)
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
                var url = vrtlDirr +'/T74010/GetLabelData';
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

        function deleteData(e) {

            try {
                var url = vrtlDirr +'/T74010/deleteData';
                return $http({
                    url: url,
                    method: "POST",
                    data: { T_SUPPLI_ID: e }
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