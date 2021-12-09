
app.service("T74123Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
    //For Instance Start 
    var dataSvc = {
        getPatients: getPatients,
        patientDetalisData: patientDetalisData,
        priscriptionData: priscriptionData,
        bodyMeasurementData: bodyMeasurementData

    };
    return dataSvc;
    function getPatients() {
        try {
            var url = vrtlDirr + '/T74123/GetPatients';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: params
                // data: { Uom_ID: Uom }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function patientDetalisData(patId) {
        try {
            var url = vrtlDirr + '/T74123/PatientDetalisData';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                //data: params
                data: { Id: patId }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function priscriptionData(patId) {
        try {
            var url = vrtlDirr + '/T74123/PriscriptionData';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                //data: params
                data: { Id: patId }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }

    function bodyMeasurementData(patId) {
        try {
            var url = vrtlDirr + '/T74123/BodyMeasurementData';
            var params = {};
            return $http({
                url: url,
                method: "POST",
               //data: params
                data: { Id: patId }
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