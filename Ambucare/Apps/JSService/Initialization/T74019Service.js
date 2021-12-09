app.service("T74019Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
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

    //Insert and Update Function start
        function InsertOrUpdate(Degree) {

            try {
                var url = vrtlDirr + '/T74019/InsertOrUpdate';
                return $http({
                    url: url,
                    method: "POST",
                    data: JSON.stringify(Degree)
                }).then(function (results) {

                    return results.data;
                }).catch(function (ex) {
                    throw ex;
                });
            } catch (ex) {
                throw ex;
            }
        }
        //Insert and Update Function End

    //for label start
        function getLabel() {

            try {
                var url = vrtlDirr + '/T74019/GetLabelData';
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
        //for label end

    //for delete start
        function deleteData(e) {

            try {
                var url = vrtlDirr + '/T74019/deleteData';
                return $http({
                    url: url,
                    method: "POST",
                    data: { T_JOB_TYPE_ID: e }
                }).then(function (results) {

                    return results.data;
                }).catch(function (ex) {
                    throw ex;
                });
            } catch (ex) {
                throw ex;
            }
        }
        //for delete end
    }
]);