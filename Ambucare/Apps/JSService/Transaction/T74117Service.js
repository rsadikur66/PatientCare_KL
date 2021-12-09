app.service("T74117Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
    //For Instance Start 
    var dataSvc = {
        GetSupplierList: GetSupplierList,
        GetStoreListFrom: GetStoreListFrom,
        GetStoreListTo: GetStoreListTo,
        GetCurrencyList: GetCurrencyList,
        GetPersonalType: GetPersonalType,
        EntryUser: EntryUser,
        GetUom: GetUom,
        insert_T74117: insert_T74117,
        GetPersonName: GetPersonName
    };
    return dataSvc;
    //For Instance End 

    ////Entry User Function Start

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

    ////Entry User Function End

    //Save data start
    function insert_T74117(t74048, t74049List) {
        try {
            var url = vrtlDirr + '/T74117/insert_T74117';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: { t74048: t74048, t74049List: t74049List }
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }

    //Save data end

    //Dropdown Supplier List Function Start

    function GetSupplierList() {
        try {
            var url = vrtlDirr + '/T74117/GetSupplierList';
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
    //Dropdown Supplier List Function End

    //Item UOM Function Start
    function GetUom(T_TYPE_ID) {
        try {
            var url = vrtlDirr + '/T74117/GetUom';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: { T_TYPE_ID: T_TYPE_ID }
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });

        } catch (ex) {
            throw ex;
        }
    }
    //Item UOM Function End

    //Dropdown Store From Function Start
    function GetStoreListFrom() {
        try {
            var url = vrtlDirr + '/T74117/GetStoreListFrom';
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
    //Dropdown Store From Function End

    //Dropdown Store From Function Start
    function GetStoreListTo() {
        try {
            var url = vrtlDirr + '/T74117/GetStoreListTo';
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
    //Dropdown Store From Function End

    //Dropdown Currency List Function Start
    function GetCurrencyList() {
        try {
            var url = vrtlDirr + '/T74117/GetCurrencyList';
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
    //Dropdown Currency List Function End

    //Popup Person Type List Function Start
    function GetPersonalType() {
        try {
            var url = vrtlDirr + '/T74117/GetPersonalType';
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

    //Popup Person Name List Function Start
    function GetPersonName(T_EMP_TYP_ID) {
        try {
            var url = vrtlDirr + '/T74117/GetPersonName';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: { T_EMP_TYP_ID: T_EMP_TYP_ID }
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