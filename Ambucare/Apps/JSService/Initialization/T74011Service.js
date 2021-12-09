app.service("T74011Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
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


        function InsertOrUpdate(ItemColor) {

            try {
                var url = vrtlDirr + '/T74011/InsertOrUpdate';
                return $http({
                    url: url,
                    method: "POST",
                    data: JSON.stringify(ItemColor)
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
                var url = vrtlDirr + '/T74011/GetLabelData';
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
                var url = vrtlDirr + '/T74011/deleteData';
                return $http({
                    url: url,
                    method: "POST",
                    data: { T_ITEM_COLOR_ID: e }
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