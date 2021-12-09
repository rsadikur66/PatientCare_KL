app.service("T74035Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
    var dataSvc = {
        deleteData: deleteData,
        InsertUpdate: InsertUpdate
    };
    return dataSvc;

    function InsertUpdate(_t74035) {
        try {
            var url = vrtlDirr + '/T74035/InsertUpdate';
            return $http({
                url: url,
                method: "POST",
                data: JSON.stringify(_t74035)
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }

    function deleteData(e) {

          try {
              var url = vrtlDirr + '/T74035/deleteData';
              return $http({
                  url: url,
                  method: "POST",
                  data: { T_DEPET_ID: e }
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