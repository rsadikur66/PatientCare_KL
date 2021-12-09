app.controller("T74024Controller", ["$scope", "$filter", "$http", "$window", "T74024Service","LabelService", "uiGridConstants", "DTOptionsBuilder", "DTColumnBuilder", "Data",
    function ($scope, $filter, $http, $window, T74024Service, LabelService, uiGridConstants, DTOptionsBuilder, DTColumnBuilder, Data) {
        //for instance
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.T_LANG2_NAME = '';
        $scope.obj.T_RESULT_TYP_ID = '';

        //For Entry User
        var EntryUser = T74024Service.EntryUser();
        EntryUser.then(function (data) {
            $scope.obj.T_ENTRY_USER = data;
            $scope.obj.T_ENTRY_DATE = $filter('date')(new Date(), 'dd-MMM-yyyy');
        });

        // Form Label Data Service Start 
       
        var labelData = LabelService.getlabeldata('T74024');
        labelData.then(function (data) {
            $scope.entity = data;
        });
        // Form Label Data Service End 
        $scope.Save = function () {
            if ($scope.obj.T_LANG1_NAME == undefined || $scope.obj.T_LANG1_NAME == '') {
               // $window.confirm('Local Name is requered');
                alert($scope.getSingleMsg('M0002'));
                $scope.localName.show();
                return;
            }
            if ($scope.obj.T_LANG2_NAME == undefined || $scope.obj.T_LANG2_NAME == '') {
               // $window.confirm('English Name is requered');
                alert($scope.getSingleMsg('M0003'));
                $scope.localName.show();
                return;
            }
            if ($scope.obj.T_LANG2_NAME !== "") {
                var save = T74024Service.insert_t74024($scope.obj);
                save.then(function(data) {
                    if ($scope.obj.T_RESULT_TYP_ID == '') {
                        //alert("Data save Succesfully");
                        alert($scope.getSingleMsg('S0012'));
                    } else {
                        //alert("Data update Succesfully");
                        alert($scope.getSingleMsg('S0003'));
                    }
                    $scope.obj = {};
                    $window.location.reload();
                });
            } else {
                //alert('Please fill up all fields');
                alert($scope.getSingleMsg('S0048'));
            }
        };

        $scope.Delete = function() {
            if ($scope.obj.T_RESULT_TYP_ID !== undefined) {
                if ($window.confirm('Are you shure ?')) {
                    var save = T74024Service.delete_t74024($scope.obj);
                    save.then(function(data) {
                        $scope.obj = {};
                        $window.location.reload();
                        //alert("Data deleted Succesfully");
                        alert($scope.getSingleMsg('S0007'));
                    });
                }
                return false;
            } else {
                //alert('Please select Result Type Id');
                alert($scope.getSingleMsg('S0049'));
            }
        };
        $scope.Clear = function () {
            $scope.obj.T_RESULT_TYP_ID = '';
            $scope.obj.T_LANG1_NAME = '';
            $scope.obj.T_LANG2_NAME = '';
            $scope.obj.T_RESULT_TYP_ID = undefined;
        };
        //For Grid Function Start 
        $scope.someClickHandler = someClickHandler;
        $scope.dtColumns = [
            //here We will add .withOption('name','column_name') for send column name to the server 
            DTColumnBuilder.newColumn("T_RESULT_TYP_ID", "Result Type").withOption('name', 'T_RESULT_TYP_ID').notVisible(),
            DTColumnBuilder.newColumn("T_LANG2_NAME", "English Name").withOption('name', 'T_LANG2_NAME'),
            DTColumnBuilder.newColumn("T_LANG1_NAME", "Local Name").withOption('name', 'T_LANG1_NAME')
        ];

        $scope.dtOptions = DTOptionsBuilder.newOptions().withOption('ajax', {
            dataSrc: "data",
            url: "/T74024/Get_t74024",
            type: "POST"
        })
            .withOption('rowCallback', rowCallback)
            .withOption('processing', true) //for show progress bar
            .withOption('serverSide', true) // for server side processing
            .withPaginationType('full_numbers') // for get full pagination options // first / last / prev / next and page numbers
            .withDisplayLength(10) // Page size
            .withOption('aaSorting', [0, 'asc']) // for default sorting column // here 0 means first column

        function someClickHandler(info) {
            $scope.obj.T_RESULT_TYP_ID = info.T_RESULT_TYP_ID;
            $scope.obj.T_LANG2_NAME = info.T_LANG2_NAME;
            $scope.obj.T_LANG1_NAME = info.T_LANG1_NAME;
            //var prevUrl = history.length > 1 ? history.splice(-2)[0] : "/";
            //$location.path(prevUrl);
            //window.location.href = "/Menus/T04201?Pat_ID =" + info.CustomerID;
        }
        function rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
            // Unbind first in order to avoid any duplicate handler (see https://github.com/l-lin/angular-datatables/issues/87)
            $('td', nRow).unbind('click');
            $('td', nRow).bind('click', function () {
                $scope.$apply(function () {
                    $scope.someClickHandler(aData);
                });
            });
            return nRow;
        }

        //For Grid Function End 
    }]);