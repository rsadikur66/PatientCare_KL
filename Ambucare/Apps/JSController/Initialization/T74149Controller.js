app.controller("T74149Controller", ["$scope", "$rootScope", "$filter", "$http", "$window", "LabelService", "uiGridConstants", "DTOptionsBuilder", "DTColumnBuilder", "Data", "T74149Service",
    function ($scope, $rootScope, $filter, $http, $window, LabelService, uiGridConstants, DTOptionsBuilder, DTColumnBuilder, Data, T74149Service) {
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.T06301 = {};
        $scope.dtInstance = {};
        
        var labelData = LabelService.getlabeldata('T74147');
        labelData.then(function (data) {
            $scope.entity = data;
        });

        $scope.Save_Click = function () {
            Save_Click();
        };

        function Save_Click() {
            var save = T74149Service.Insert($scope.obj.T06301);
            save.then(function (data) {
               //alert("Data saved Succesfully");
                alert($scope.getSingleMsg('S0012'));
                Clear();
                $scope.dtInstance.reloadData();
                //$window.location.reload();

            });
        }
        //function Save_Click() {
        //    if ($scope.obj.T_CH_COMP == undefined) {
        //        var save = T74055Service.Insert($scope.obj);
        //        save.then(function (data) {
        //            alert("Data saved Succesfully");
        //            $window.location = "/Initialization/T74055";

        //        });
        //    }
        //    else {
        //        var save = T74055Service.Insert($scope.obj);
        //        save.then(function (data) {
        //            alert("Data updated Succesfully");
        //            $window.location = "/Initialization/T74055";
        //        });
        //    }
        //}

        var disable = $rootScope.$on('T74149Emit', function (event, data) {
            if (data == 'n') {
                Save_Click();
            }
        });

        var clearDisable = $rootScope.$on('T74149ClearEmit',
            function (event, data) {
                if (data === 'e') {
                    Clear();
                }
            });

        $scope.$on('$destroy', function () {
            disable();
            clearDisable();
        });

        $scope.dtInstanceCallback = function(instance) {
            $scope.dtInstance = instance;
        }

        $scope.someClickHandler = someClickHandler;
        $scope.dtColumns = [
            DTColumnBuilder.newColumn("T_ICD10_MAIN_CODE", "Main Code").withOption('name', 'T_ICD10_MAIN_CODE'),
            DTColumnBuilder.newColumn("T_ICD10_SUB_CODE", "Sub Code").withOption('name', 'T_ICD10_SUB_CODE'),
            DTColumnBuilder.newColumn("T_ICD10_LONG_DESC1", "Long Des 1").withOption('name', 'T_ICD10_LONG_DESC1'),
            DTColumnBuilder.newColumn("T_ICD10_LONG_DESC2", "Long Des 2").withOption('name', 'T_ICD10_LONG_DESC2'),
          DTColumnBuilder.newColumn("T_ICD10_SHORT_DESC2", "Short Des 1").withOption('name', 'T_ICD10_SHORT_DESC2').notVisible(),
          DTColumnBuilder.newColumn("T_ICD10_SHORT_DESC1", "Short Des 2").withOption('name', 'T_ICD10_SHORT_DESC1').notVisible(),
            DTColumnBuilder.newColumn("T_ICD10_TYPE", "Type").withOption('name', 'T_ICD10_TYPE'),
            DTColumnBuilder.newColumn("T_ICD10_CODE", "ICD 10").withOption('name', 'T_ICD10_CODE')
        ];
        $scope.dtOptions = DTOptionsBuilder.newOptions().withOption('ajax',
                {
                    dataSrc: "data",
                  url: $scope.vrDir+"/T74149/GetGridData",
                    type: "POST"
                })
            .withOption('rowCallback', rowCallback)
            .withOption('processing', true) //for show progress bar
            .withOption('serverSide', true) // for server side processing
            .withPaginationType(
                'full_numbers') // for get full pagination options // first / last / prev / next and page numbers
            .withDisplayLength(10) // Page size
            .withOption('aaSorting', [0, 'asc']); // for default sorting column // here 0 means first column

        function someClickHandler(info) {
            $scope.obj.T06301.T_ICD10_MAIN_CODE = info.T_ICD10_MAIN_CODE;
            $scope.obj.T06301.T_ICD10_SUB_CODE = info.T_ICD10_SUB_CODE;
            $scope.obj.T06301.T_ICD10_LONG_DESC2 = info.T_ICD10_LONG_DESC2;
            $scope.obj.T06301.T_ICD10_LONG_DESC1 = info.T_ICD10_LONG_DESC1;
            $scope.obj.T06301.T_ICD10_SHORT_DESC2 = info.T_ICD10_SHORT_DESC2;
            $scope.obj.T06301.T_ICD10_SHORT_DESC1 = info.T_ICD10_SHORT_DESC1;
            $scope.obj.T06301.T_ICD10_TYPE = info.T_ICD10_TYPE;
            $scope.obj.T06301.T_ICD10_CODE = info.T_ICD10_CODE;
        }

        $scope.Clear = function() {
            Clear();
        }

        function Clear() {
            $scope.obj.T06301.T_ICD10_MAIN_CODE = '';
            $scope.obj.T06301.T_ICD10_SUB_CODE = '';
            $scope.obj.T06301.T_ICD10_LONG_DESC2 = '';
            $scope.obj.T06301.T_ICD10_LONG_DESC1 = '';
            $scope.obj.T06301.T_ICD10_SHORT_DESC2 = '';
            $scope.obj.T06301.T_ICD10_SHORT_DESC1 = '';
            $scope.obj.T06301.T_ICD10_TYPE = '';
            $scope.obj.T06301.T_ICD10_CODE = undefined;

        };

        function rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
            $('td', nRow).unbind('click');
            $('td', nRow).bind('click', function () {
                $scope.$apply(function () {
                    $scope.someClickHandler(aData);
                });
            });
            return nRow;
        }




    }

]);