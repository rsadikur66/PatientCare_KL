
app.service("T74150Service", ["$http", "vrtlDirr", function ($http, vrtlDirr, $q) {

    var dataSvc = {
        GetItemTypeList: GetItemTypeList,
        GetItemsList: GetItemsList,
        GetGenList: GetGenList,
        GetFormList: GetFormList,
        insert: insert,
        //allGridData: allGridData,
        //allGridDatabytype: allGridDatabytype
    };
    return dataSvc;

    //Dropdown Store To Function Start
    function GetItemTypeList() {
        try {
            var url = vrtlDirr + "/T74150/GetItemTypeList";
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
    //For Grid data Function Start
    function GetItemsList(itemtype) {
        try {

            var url = vrtlDirr + '/T74150/GetItemsList';
            var params = { itemtype: itemtype };
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

    function GetGenList() {
        try {

            var url = vrtlDirr + '/T74150/GetGenList';
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

    function GetFormList() {
        try {

            var url = vrtlDirr + '/T74150/GetFormList';
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

    function allGridDatabytype(itemtype) {
        try {

            var url = vrtlDirr + '/T74150/allGridDatabytype';
            var params = { itemtype: itemtype};
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

    function insert(itemid,costdtlid, itemtype, gencode, formcode, itemnameeng, itemnameloc) {
        try {

            var url = vrtlDirr + '/T74150/insert';
            var params = { itemid: itemid, costdtlid: costdtlid, itemtype: itemtype, gencode: gencode, formcode: formcode, itemnameeng: itemnameeng, itemnameloc: itemnameloc };
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


}]);