app.controller("T74028Controller", ["$scope", "$filter", "$http", "$window", "T74028Service", "LabelService", "uiGridConstants", "DTOptionsBuilder", "DTColumnBuilder", "Data", "AmbulanceFactory",
    function (scope, $filter, $http, $window, T74028Service, LabelService, uiGridConstants, DTOptionsBuilder, DTColumnBuilder, Data, AmbulanceFactory) {
        //for instance
        scope.obj = {};
        
        scope.obj = Data;
        //scope.items = {};
        // Form Label Data Service Start 
        var labelData = LabelService.getlabeldata('T74028');
        labelData.then(function (data) {
            scope.entity = data;
        });
        scope.stest = function (data) {
            alert(JSON.stringify(data.target.id + JSON.parse(sessionStorage.getItem("FCode"))));
        }
        scope.Save = function () {
            if (scope.obj.T_PUR_REQ_ID === undefined) {
                var save = T74028Service.insert_t74028(scope.obj);
                save.then(function (data) {
                    //alert("Data saved Succesfully");
                    alert(scope.getSingleMsg('S0002'));
                    $window.location = "/Initialization/T74028";

                });
            }
            else {
                var save = T74028Service.insert_t74028(scope.obj);
                save.then(function (data) {
                    //alert("Data updated Succesfully");
                    alert(scope.getSingleMsg('S0003'));
                    $window.location = "/Initialization/T74028";
                });
            }
        }
        scope.Delete = function () {
            if (scope.obj.T_PUR_REQ_ID !== undefined) {
                if ($window.confirm('Are you sure ?')) {
                    var del = T74028Service.delete_t74028(scope.obj);
                    del.then(function (data) {
                        //alert("Data delete Succesfully");
                        alert(scope.getSingleMsg('S0007'));
                        $window.location = "/Initialization/T74028";
                    });
                }
                return false;
            }
            else {
                //alert('Please select Item Brand');
                alert(scope.getSingleMsg('S0015'));
                return false;
            }
        };
        scope.Clear = function () {
            scope.obj.T_PUR_REQ_ID = '';
            scope.obj.T_COMPANY_ID = '';
            scope.obj.T_PURCHASE_NO = '';

            scope.obj.T_PUR_REQ_ID = undefined;
        };

        //Button Add and remove
        scope.gvw = [{
            WsCode: '',
            WsName: '',
            AnalysisNo: '',
            AnalysisName: '',
            Diagnosis: '',
            pretest: '',
            SpecimenCode: ''
        }];
        scope.Addrow = function () {

            //var gvw1 = {};
            //gvw1.WsCode = scope.WsCode;
            //gvw1.WsName = scope.WsName;
            //scope.gvw.push(gvw1);
            scope.gvw.indexOf();
            scope.gvw.push({
                WsCode: '',
                WsName: '',
                AnalysisNo: '',
                AnalysisName: '',
                Diagnosis: '',
                pretest: '',
                SpecimenCode: ''
            });
           
            //scope.selected.items = scope.gvw[index];
        }
        scope.RemoveRow = function (index) {
            scope.gvw.splice(index, 1);
        };
            //Button Add and remove end

        //comment

        scope.ambu = function (index) {
            var url = '';
            var data = '';
           // alert(index);
            AmbulanceFactory.getModal(url, data, index);
        };
      
        //Get Ambulance Type Function Start
        var ambuId = T74028Service.GetEmployeeData();
        ambuId.then(function (data) {
            scope.ambuId = JSON.parse(data);
        });
        //Get Ambulance Type Function End


        
        
    }]);
