app.service("T74114Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
    //For Instance Start 
    var dataSvc = {
        
        GetSupplierList: GetSupplierList,
        GetStoreList: GetStoreList,
        GetCurrencyList: GetCurrencyList,
        GetItemTypeList: GetItemTypeList,
        GetItemNameList: GetItemNameList,
        EntryUser: EntryUser,
        GetUom:GetUom,
        insert_T74114: insert_T74114
    };
    return dataSvc;
    //For Instance End 

    //Entry User Function Start
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

    //Entry User Function End

    //Save data start
    function insert_T74114(t74030, t74031List) {
        try {
            var url = vrtlDirr + '/T74114/insert_T74114';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: { t74030: t74030, t74031List: t74031List}
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
            var url = vrtlDirr + '/T74114/GetSupplierList';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: params
            }).then(function(results) {
                return results.data;
            }).catch(function(ex) {
                throw ex;
            });

        } catch (ex) {
            throw ex;
        }
    }
    //Dropdown Supplier List Function End

    //Dropdown Store List Function Start
    function GetStoreList() {
        try {
            var url = vrtlDirr + '/T74114/GetStoreList';
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
    //Dropdown Store List Function End

    //Dropdown Currency List Function Start
    function GetCurrencyList() {
        try {
            var url = vrtlDirr + '/T74114/GetCurrencyList';
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

    //Popup Item Type List Function Start
    function GetItemTypeList() {
        try {
            var url = vrtlDirr + '/T74114/GetItemTypeList';
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
    //Popup Item Type List Function End

    //Popup Item Name List Function Start
    function GetItemNameList(Itemid) {
        try {
            var url = vrtlDirr + '/T74114/GetItemNameList';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: { id: Itemid}
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
            var url = vrtlDirr + '/T74114/GetUom';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: { T_TYPE_ID: T_TYPE_ID}
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });

        } catch (ex) {
            throw ex;
        }
    }
    //Popup Item Name List Function End

     
}]);