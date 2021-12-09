
app.service("T74122Service", ["$http", "vrtlDirr", function ($http, vrtlDirr, $q) {

    var dataSvc = {
        EntryUser: EntryUser,
        GetTransferData: GetTransferData,
        ReceiveBy: ReceiveBy,
        SaveData: SaveData,
        GetTransferList_85: GetTransferList_85
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
                return results.dt;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }

    //Get Transfer Data

    function GetTransferData(T_ReqId) {
        try {
            var url = vrtlDirr + '/T74122/GetTransferData';
            return $http({
                url: url,
                method: "POST",
                data: { T_TRANSFER_REQ_ID: T_ReqId }
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }

    function ReceiveBy(T_RCV_TO_STR_ID) {
        try {
            var url = vrtlDirr + '/T74122/GetReceiveBy';
            //var params = {};
            return $http({
                url: url,
                method: "POST",
                //  data: params
                data: { T_RCV_TO_STR_ID: T_RCV_TO_STR_ID }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }

    function SaveData(T74080,T74084, T74085, T74027) {
        try {
            var url = vrtlDirr + '/T74122/SaveData';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                // data: params
                data: { t74080: T74080,t74084: T74084, t74085: T74085, T74027: T74027 }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }

    function GetTransferList_85(f) {
        try {
            var url = vrtlDirr + '/T74122/GetTransferList_85';
            return $http({
                url: url,
                method: "POST",
                data: { T_TRANSFER_REQ_ID:f }
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