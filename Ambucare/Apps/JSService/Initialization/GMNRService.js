app.service("GMNRService", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
    //For Instance Start 
    var dataSvc = {
        InsertOrUpdateT74050: InsertOrUpdateT74050,
        InsertOrUpdateT74051: InsertOrUpdateT74051,
        InsertOrUpdateT74058: InsertOrUpdateT74058,
        InsertOrUpdateT74059: InsertOrUpdateT74059,
        getLabelT74050: getLabelT74050,
        deleteData: deleteData,
        deleteDataMarital: deleteDataMarital,
        deleteDataNationality: deleteDataNationality,
        deleteDataReligion: deleteDataReligion
};
    return dataSvc;

    //Insert and Update Function start
    function InsertOrUpdateT74050(_t74050) {

        try {
            var url = vrtlDirr +'/GMNR/InsertOrUpdateT74050';
            return $http({
                url: url,
                method: "POST",
                data: JSON.stringify(_t74050)
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }

    //t74051
    function InsertOrUpdateT74051(T74051) {

        try {
            var url = vrtlDirr +'/GMNR/InsertOrUpdateT74051';
            return $http({
                url: url,
                method: "POST",
                data: JSON.stringify(T74051)
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    //t74058
    function InsertOrUpdateT74058(_t74058) {

        try {
            var url = vrtlDirr +'/GMNR/InsertOrUpdateT74058';
            return $http({
                url: url,
                method: "POST",
                data: JSON.stringify(_t74058)
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    } 

    //t74059
    function InsertOrUpdateT74059(_t74059) {

        try {
            var url = vrtlDirr +'/GMNR/InsertOrUpdateT74059';
            return $http({
                url: url,
                method: "POST",
                data: JSON.stringify(_t74059)
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    

    //Insert and Update Function End

    //for delete start
    function deleteData(e) {
        debugger;
        try {
            var url = vrtlDirr +'/GMNR/deleteData';
            return $http({
                url: url,
                method: "POST",
                data: {T_SEX_CODE: e }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    } 

    function deleteDataMarital(e) {
        debugger;
        try {
            var url = vrtlDirr +'/GMNR/deleteDataMarital';
            return $http({
                url: url,
                method: "POST",
                data: { T_MRTL_STATUS_CODE: e }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }

    function deleteDataNationality(e) {
        debugger;
        try {
            var url = vrtlDirr +'/GMNR/deleteDataNationality';
            return $http({
                url: url,
                method: "POST",
                data: { T_NTNLTY_ID: e }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }


    function deleteDataReligion(e) {
        debugger;
        try {
            var url = vrtlDirr +'/GMNR/deleteDataReligion';
            return $http({
                url: url,
                method: "POST",
                data: { T_RLGN_CODE: e }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    //for delete end



    //for label start
    function getLabelT74050() {

        try {
            var url = vrtlDirr +'/GMNR/GetLabelDataT74050';
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
    //for label end
    
}]);


