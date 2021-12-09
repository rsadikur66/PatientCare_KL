app.controller("R74125Controller",
    ["$scope", "$compile", "$filter", "$http", "$window", "LabelService", "uiGridConstants", "DTOptionsBuilder", "DTColumnBuilder", "Data",
        function (scope, $compile, $filter, $http, $window, LabelService, uiGridConstants, DTOptionsBuilder, DTColumnBuilder, Data) {
            //For Instance 
            scope.obj = {};
            scope.obj = Data;
            
            
            // Form Label Data Service Start 
            var labelData = LabelService.getlabeldata('R74125');
            labelData.then(function (data) {
                scope.entity = data;
            });
            
            scope.Print = function () {
                $window.open("../R74125/GetAllReportData?T_REQUEST_ID=" + scope.obj.REQUEST_ID, "popup", "width=600,height=600,left=50,top=50");
            }
        }]);