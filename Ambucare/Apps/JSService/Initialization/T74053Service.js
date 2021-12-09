
app.service("T74053Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
    var dataSvc = {
        getZoneCode: getZoneCode,
        saveData: saveData,
        getHospitalData: getHospitalData,
        Delete: Delete,
        EntryUser: EntryUser
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
                    return results.data;
                }).catch(function (ex) {
                    throw ex;
                });
            } catch (ex) {
                throw ex;
            }
        }

    function getZoneCode() {
        
        try {
            var url = vrtlDirr + '/T74053/GetZoneCode';
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

    function getHospitalData() {
        
        try {
            var url = vrtlDirr + '/T74053/GetHosptalData';
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

    function saveData(Hospital) {
        
        try {
            var url = vrtlDirr + '/T74053/AddHospital';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: JSON.stringify(Hospital)
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }

        //$http({
        //    method: 'Post',
        //    url: '/T74053/AddHospital',
        //    data: JSON.stringify(Hospital)
        //});
            
    }

        function Delete(d) {

            try {
                var url = vrtlDirr + '/T74053/DeleteHosPital';
                return $http({
                    url: url,
                    method: "POST",
                    data: { T_SITE_NO: d }
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