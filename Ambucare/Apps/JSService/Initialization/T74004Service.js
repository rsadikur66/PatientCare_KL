
app.service("T74004Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
        var dataSvc = {
            MaritalData: MaritalData,
            BloodGroupData: BloodGroupData,
            GenderData: GenderData,
            ReligionData: ReligionData,
            EmployeeTypeData: EmployeeTypeData,
            SaveData: SaveData,
            EntryUser: EntryUser,
            NationalityData: NationalityData,
            getPassportNo: getPassportNo
            //getGridData: getGridData,
            // Delete: Delete

        };
        return dataSvc;

        //Entry User
        function getPassportNo(_t74004) {
            try {
                var url = vrtlDirr + '/T74004/CheckPassport';
                var params = { _t74004: _t74004 };
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


        function MaritalData() {
            try {
                var url = vrtlDirr + '/T74004/GetMaritalData';
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

        function BloodGroupData() {
            try {
                var url = vrtlDirr + '/T74004/GetBloodGroupData';
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

        function GenderData() {
            try {
                var url = vrtlDirr + '/T74004/GetGenderData';
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

        function ReligionData() {
            try {
                var url = vrtlDirr + '/T74004/GetReligionData';
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

        function EmployeeTypeData() {
            try {
                var url = '/T74004/EmployeeTypeData';
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

        function NationalityData() {
            try {
                var url = vrtlDirr + '/T74004/GetNationality';
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

        function SaveData(_t74004) {

            try {
                var url = vrtlDirr + '/T74004/SaveEmployeeInfo';
                return $http({
                    url: url,
                    method: "POST",
                    // data: JSON.stringify(_t74004)
                    data: { _t74004: _t74004 }
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