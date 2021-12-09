app.service("LabelService", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
        var dataSvc = {
            getlabeldata: getlabeldata,
            LabelData: LabelData
        };
        return dataSvc;

//For Form Label Data Function Start
        function getlabeldata(formcode) {
            try {
                var url = vrtlDirr + '/T74000/GetLabelTextData';
                //alert("IUHIH");
                var params = { T_FORM_CODE: formcode };
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
        function LabelData(formCode, lblName) {
            try {
                var url = vrtlDirr + '/Menu/LabelData';
                var params = { T_FORM_CODE: formCode, T_LABEL_NAME: lblName };
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
       
    }
]);

//For Form Label Data Function End