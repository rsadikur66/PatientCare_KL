app.service("T74146Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
        var dataSvc = {
            getGridData: getGridData,
            Insert: Insert,
            Update: Update,
            formCodeList: formCodeList
        };
        return dataSvc;

        function getGridData() {
            try {
                var url = vrtlDirr + '/T74146/getGridData';
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

        function formCodeList() {
            try {
                var url = vrtlDirr + '/T74146/getFormCode';
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

        function Insert(T_MSG_CODE, T_LANG1_MSG, T_LANG2_MSG, T_FORM_CODE, T_ACTV_STTS) {

            try {
                var url = vrtlDirr + '/T74146/Insert';
                return $http({
                    url: url,
                    method: "POST",
                    data: { T_MSG_CODE: T_MSG_CODE, T_LANG1_MSG: T_LANG1_MSG, T_LANG2_MSG: T_LANG2_MSG, T_FORM_CODE: T_FORM_CODE, T_ACTV_STTS: T_ACTV_STTS}
                }).then(function (results) {

                    return results.data;
                }).catch(function (ex) {
                    throw ex;
                });
            } catch (ex) {
                throw ex;
            }
        }

        function Update(T_MSG_ID, T_MSG_CODE, T_LANG1_MSG, T_LANG2_MSG, T_FORM_CODE, T_ACTV_STTS) {

            try {
                var url = vrtlDirr + '/T74146/update';
                return $http({
                    url: url,
                    method: "POST",
                    data: { T_MSG_ID: T_MSG_ID, T_MSG_CODE: T_MSG_CODE, T_LANG1_MSG: T_LANG1_MSG, T_LANG2_MSG: T_LANG2_MSG, T_FORM_CODE: T_FORM_CODE, T_ACTV_STTS: T_ACTV_STTS}
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