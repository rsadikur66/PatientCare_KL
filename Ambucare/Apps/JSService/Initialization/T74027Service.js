
app.service("T74027Service", ["$http", "vrtlDirr", function ($http, vrtlDirr, $q) {

    var dataSvc = {
        GetCompanysData: GetCompanyData,
        GetProductItem: GetProductItem,
        GetStoresData: GetStoresData,
        GetSupplierData: GetSupplierData,
        GetProductTypeData: GetProductTypeData,
        GetUom: GetUom,
        GetGridAllData: GetGridAllData,
        insert_T74027: insert_T74027,
        Del_t74027: Del_t74027,
        EntryUser: EntryUser,
        GetStockQuantity: GetStockQuantity
        
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

    //DropDown Start
    function GetCompanyData() {

        try {
            var url = vrtlDirr + '/T74027/GetCompanysData';
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

    function GetProductItem(e) {
        try {
            var url = vrtlDirr + '/T74027/GetProductItem';
            return $http({
                url: url,
                method: "POST",
                data: { T_COST_TYPE_ID: e }
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }

    function GetStoresData() {
        try {
            var url = vrtlDirr + '/T74027/GetStoresData';
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

    function GetSupplierData() {
        try {
            var url = vrtlDirr + '/T74027/GetSupplierData';
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

    function GetProductTypeData() {
        try {
            var url = vrtlDirr + '/T74027/GetProductTypeData';
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

    function GetUom(T_TYPE_ID) {
        try {
            
            var url = vrtlDirr + '/T74027/GetUom';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: { T_TYPE_ID: T_TYPE_ID}
            }).then(function(results) {
                return results.data;
            }).catch(function(ex) {
                 throw ex;
            });
        } catch (ex) {
            throw ex;
        } 
    }

    function GetGridAllData(T_STORE_ID) {
        debugger;
        try {
            var url = vrtlDirr + '/T74027/GetGridAllData';
            return $http({
                url: url,
                method: "POST",
                data: {
                    T_STORE_ID: T_STORE_ID
                    //,
                    //T_COMPANY_ID: T_COMPANY_ID
                }
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }

    //Save data start

    function insert_T74027(_t74027) {
        try {
            var url = vrtlDirr + '/T74027/insert_T74027';//AddItemBrand from controller T74002Controller
            return $http.post(url, _t74027)
               .then(function (results) {
            }).catch(function (e) { });
        } catch (ex) {
            throw ex;
        }
    }
    //Save data end
    
    function Del_t74027(e) {

        try {
            var url = vrtlDirr + '/T74027/Del_t74027';
            return $http({
                url: url,
                method: "POST",
                data: { _t74027Del: e }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }


    function GetStockQuantity(ITEM_ID) {
        debugger;
        try {
            var url = vrtlDirr + '/T74027/GetStockQuantity';
            return $http({
                url: url,
                method: "POST",
                data: {
                    ITEM_ID: ITEM_ID
                }
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