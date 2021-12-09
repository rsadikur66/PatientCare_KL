
app.service("T74129Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
            //For Instance Start 
            var dataSvc = {
                //getEmployee: getEmployee,
                //getPrice: getPrice,
                //getStock: getStock,
                saveData: saveData,
                getGridData: getGridData,
                getSearchData: getSearchData
                


            };
            return dataSvc;
            function getGridData() {
            try {
                var url = vrtlDirr + '/T74129/GetGridData';
                var params = {};
                return $http({
                    url: url,
                    method: "POST",
                     data: params
                   // data: { item: item, store: store, umId: umId }
                }).then(function (results) {

                    return results.data;
                }).catch(function (ex) {
                    throw ex;
                });
            } catch (ex) {
                throw ex;
            }
            }
            function saveData(t95) {
            try {
                var url = vrtlDirr + '/T74129/SaveData';
                var params = {};
                return $http({
                    url: url,
                    method: "POST",
                   // data: params
                    data: { t95: t95 }
                }).then(function (results) {

                    return results.data;
                }).catch(function (ex) {
                    throw ex;
                });
            } catch (ex) {
                throw ex;
            }
        }
    function getSearchData(stl) {
            try {
                var url = vrtlDirr + '/T74129/GetSearchData';
                var params = {};
                return $http({
                    url: url,
                    method: "POST",
                    // data: params
                    data: { stl: stl }
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