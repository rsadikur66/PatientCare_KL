
app.controller("T74056Controller", ["$scope", "$filter", "$http", "$window", "uiGridConstants", "DTOptionsBuilder", "DTColumnBuilder", "Data", "T74056Service", "LabelService",
    function ($scope, $filter, $http, $window, uiGridConstants, DTOptionsBuilder, DTColumnBuilder, Data, T74056Service, LabelService) {
        $scope.obj = {};
        $scope.obj = Data;
        //$scope.Label = function (e) {
        //    var lbl = LabelService.LabelData(JSON.parse(sessionStorage.getItem("FCode")), e);
        //    lbl.then(function (data) {
        //       // $scope.label = data[0].T_LANG2_TEXT;
        //    });
        //}

        var EntryUser = T74056Service.EntryUser();
        EntryUser.then(function (data) {
            $scope.obj.T_ENTRY_USER = data;
            $scope.obj.T_ENTRY_DATE = $filter('date')(new Date(), 'dd-MMM-yyyy');
        });
        $scope.Save = function () {
            if (scope.obj.T_LANG1_NAME == undefined || scope.obj.T_LANG1_NAME == '') {
                // $window.confirm('Local Name is requered');
                alert($scope.getSingleMsg('M0002'));
                scope.localName.show();
                return;
            }
            if (scope.obj.T_LANG2_NAME == undefined || scope.obj.T_LANG2_NAME == '') {
                // $window.confirm('English Name is requered');
                alert($scope.getSingleMsg('M0003'));
                scope.localName.show();
                return;
            }
            if ($scope.obj.T_TRIAGE_SEQ === undefined) {
                var save = T74056Service.Insert($scope.obj);
                save.then(function (data) {
                    //alert("Data saved Succesfully");
                     alert($scope.getSingleMsg('S0012'));
                    $window.location.reload();

                });
            }
            else {
                var updat = T74056Service.Insert($scope.obj);
                updat.then(function (data) {
                    //alert("Data updated Succesfully");
                   alert($scope.getSingleMsg('S0003'));
                    $window.location.reload();
                });
            }
        }
        $scope.Delete = function () {
            if ($scope.obj.T_TRIAGE_SEQ !== undefined) {
                if ($window.confirm('Are you sure ?')) {
                    var del = T74056Service.Delete($scope.obj.T_TRIAGE_SEQ);
                    del.then(function (data) {
                        //alert("Data deleted Succesfully");
                        alert($scope.getSingleMsg('S0007'));
                        $window.location.reload();
                    });
                }
                return false;
            }
            else {
                //alert("Select a data for delete.");
                alert($scope.getSingleMsg('S0011'));
                return false;
            }
        };
        $scope.Clear = function () {
            $scope.obj.T_TRIAGE_SEQ = '';
            $scope.obj.T_LANG1_NAME = '';
            $scope.obj.T_LANG2_NAME = '';

            $scope.obj.T_TRIAGE_SEQ = undefined;
        };
        $scope.someClickHandler = someClickHandler;
        $scope.dtColumns = [
            DTColumnBuilder.newColumn("T_LANG2_NAME", "English Name").withOption('name', 'T_LANG2_NAME'),
            DTColumnBuilder.newColumn("T_LANG1_NAME", "Local Name").withOption('name', 'T_LANG1_NAME')
        ];
        $scope.dtOptions = DTOptionsBuilder.newOptions().withOption('ajax',
                {
                    dataSrc: "data",
                    url: "/T74056/GetGridData",
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
            $scope.obj.T_TRIAGE_SEQ = info.T_TRIAGE_SEQ;
            $scope.obj.T_LANG2_NAME = info.T_LANG2_NAME;
            $scope.obj.T_LANG1_NAME = info.T_LANG1_NAME;
        }
        function rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
            $('td', nRow).unbind('click');
            $('td', nRow).bind('click', function () {
                $scope.$apply(function () {
                    $scope.someClickHandler(aData);
                });
            });
            return nRow;
        }
    }]);