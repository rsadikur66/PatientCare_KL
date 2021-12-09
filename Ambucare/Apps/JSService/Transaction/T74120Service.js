
app.service("T74120Service", ["$http", "vrtlDirr", function ($http, vrtlDirr, $q) {

    var dataSvc = {
        EntryUser: EntryUser,
        Insert_T74120: Insert_T74120,
        GetSupplierList: GetSupplierList,
        GetStoreListFrom: GetStoreListFrom,
        GetStoreListTo: GetStoreListTo,
        GetCurrencyList: GetCurrencyList,
        GetPersonType: GetPersonType,
        GetPersonName: GetPersonName,
        GetUom: GetUom,
        GetStock: GetStock,
        GetPurPrice: GetPurPrice
    };
    return dataSvc;

    //Entry User
    function EntryUser() {
        try {
            var url = vrtlDirr + "/Accounts/EntryUser";
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

    //Save data start
    function Insert_T74120(t74080, t74081List) {
        try {
            var url = vrtlDirr + '/T74120/Insert_T74120';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: { t74080: t74080, t74081List: t74081List }
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
            var url = vrtlDirr + "/T74120/GetSupplierList";
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

    //Dropdown Store From Function Start
    function GetStoreListFrom() {
        try {
            var url = vrtlDirr + "/T74120/GetStoreListFrom";
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

    //Dropdown Store To Function Start
    function GetStoreListTo() {
        try {
            var url = vrtlDirr + "/T74120/GetStoreListTo";
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
    
    //Dropdown Currency List Function Start
    function GetCurrencyList() {
        try {
            var url = vrtlDirr + "/T74120/GetCurrencyList";
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

    //Popup Person Type List Function Start
    function GetPersonType() {
        try {
            var url = vrtlDirr + "/T74120/GetPersonType";
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
            var url = vrtlDirr + "/T74120/GetPersonName";
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

    //Get UM Id Function Start
    function GetUom(T_COST_TYPE_ID) {
        try {
            var url = vrtlDirr + "/T74120/GetUom";
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: { T_COST_TYPE_ID: T_COST_TYPE_ID }
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });

        } catch (ex) {
            throw ex;
        }
    }

    //Get Stock Function Start
    function GetStock(ITEM_ID, UM_ID, STORE_ID) {
        try {
            var url = vrtlDirr + "/T74120/GetStock";
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: { ITEM_ID: ITEM_ID, UM_ID: UM_ID, STORE_ID: STORE_ID }
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });

        } catch (ex) {
            throw ex;
        }
    }

    //Get Purchase Price Function Start
    function GetPurPrice(ITEM_ID, UM_ID) {
        try {
            var url = vrtlDirr + "/T74120/GetPurPrice";
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: { ITEM_ID: ITEM_ID, UM_ID: UM_ID}
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