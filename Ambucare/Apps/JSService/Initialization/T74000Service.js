app.service("T74000Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
        var dataSvc = {
            InsertOrUpdate: InsertOrUpdate,
            getLabel: getLabel,
            deleteData: deleteData,
            GetFormData: GetFormData,
            EntryUser: EntryUser
            };
        return dataSvc;
        //DropDown Start
        function GetFormData() {

            try {
                var url = vrtlDirr + '/T74000/GetFormData';
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

        //Insert Or Update
        function InsertOrUpdate(labelField) {
            
            try {
                var url = vrtlDirr +'/T74000/InsertOrUpdate';
                return $http({
                    url: url,
                    method: "POST",
                    data: { labelField: labelField}
                }).then(function (results) {

                    return results.data;
                }).catch(function (ex) {
                    throw ex;
                });
            } catch (ex) {
                throw ex;
            }
        }

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

        //Label Data
        function getLabel() {
            try {
                var url = vrtlDirr +'/T74000/GetLabelData';
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

        //Delete
        function deleteData(e) {
            
            try {
                var url = vrtlDirr+'/T74000/deleteData';
                return $http({
                    url: url,
                    method: "POST",
                    data: { T_LABEL_ID: e }
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