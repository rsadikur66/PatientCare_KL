app.filter('propsFilter', function () {
    return function (items, props) {
        var out = [];
        if (angular.isArray(items)) {
            var keys = Object.keys(props);
            items.forEach(function (item) {
                var itemMatches = false;
                for (var i = 0; i < keys.length; i++) {
                    var prop = keys[i];
                    var text = props[prop].toLowerCase();
                    if (item[prop].toString().toLowerCase().indexOf(text) !== -1) {
                        itemMatches = true;
                        break;
                    }
                }
                if (itemMatches) {
                    out.push(item);
                }
            });
        } else {
            out = items;
        }
        return out;
    };
});
app.controller("T74018Controller", ["$scope", "$filter", "$http", "$window", "T74018Service", "LabelService",  "uiGridConstants", "DTOptionsBuilder", "DTColumnBuilder", "Data",
    function ($scope, $filter, $http, $window, T74018Service, LabelService,  uiGridConstants, DTOptionsBuilder, DTColumnBuilder, Data) {
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.l = {};
       
        $scope.check = {
            active: 1
        };

        //For  DropDown Start
        var CostTypeData = T74018Service.GetCostTypeData();
        CostTypeData.then(function (data) {
            $scope.CostTypeData = data;
        });
        
        //For Entry User
        var EntryUser = T74018Service.EntryUser();
        EntryUser.then(function (data) {
            $scope.obj.T_ENTRY_USER = data;
            $scope.obj.T_ENTRY_DATE = $filter('date')(new Date(), 'dd-MMM-yyyy');
        });
        // Form Label Data Service Start 
        var labelData = LabelService.getlabeldata('T74018');
        labelData.then(function (data) {
            $scope.entity = data;
        });
        // Form Label Data Service End 

        $scope.Save_Click = function() {
            if ($scope.obj.T_AMOUNT != null) {
                if ($scope.obj.T_ID != undefined) {

                    var update = T74018Service.Insert($scope.obj);
                    update.then(function(data) {
                        //alert("Data Update Succesfully");
                        alert($scope.getSingleMsg('S0003'));
                        $window.location.reload();

                    });
                } else {
                    var save = T74018Service.Insert($scope.obj);
                    save.then(function(data) {
                        //alert("Data Save Succesfully");
                        alert($scope.getSingleMsg('S0012'));
                        $window.location.reload();

                    });
                }
            } else {
                //alert('Amount is Required');
                alert($scope.getSingleMsg('S0046'));
                angular.element('#txtAmount').focus();
            }
        };
        $scope.Clear = function () {
            $scope.obj.T_AMOUNT = "";
            $scope.obj.T_DESCRIPTION = "";
            $scope.obj.l = {};
            $scope.obj.T_ID = undefined;
            };
        $scope.Delete_Click = function () {
            if ($scope.obj.T_ID != null) {
                if ($window.confirm('Are you sure to want delete ?')) {
                    var dele = T74018Service.Delete($scope.obj.T_ID);
                    dele.then(function (data) {
                        //alert("Data Deleted Succesfully");
                        alert($scope.getSingleMsg('S0007'));
                        $window.location.reload();

                    });
                }
            } else {
                //alert("Select a data for delete.");
                alert($scope.getSingleMsg('S0011'));
            }
        }
        $scope.someClickHandler = someClickHandler;
        $scope.dtColumns = [
            DTColumnBuilder.newColumn("CostType", "Cost Type").withOption('name', 'T_LANG2_NAME'),
            DTColumnBuilder.newColumn("T_COST_TYPE_ID", "Cost Type").withOption('name', 'T_COST_TYPE_ID').notVisible(),
            DTColumnBuilder.newColumn("T_DESCRIPTION", "Description").withOption('name', 'T_DESCRIPTION'),
            DTColumnBuilder.newColumn("T_AMOUNT", "Amount").withOption('name', 'T_AMOUNT'),
            DTColumnBuilder.newColumn("T_ACTIVE", "Flag").withOption('name', 'T_ACTIVE').renderWith(function (T_ACTIVE, row) {
                if (T_ACTIVE == 1) {

                    return 'Active';
                }
                return 'Inactive';
            })
        ];
        $scope.dtOptions = DTOptionsBuilder.newOptions().withOption('ajax',
                {
                    dataSrc: "data",
                    url: "/T74018/GetLabelData",
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
            $scope.obj.T_ID = info.T_ID;
            $scope.obj.T_DESCRIPTION = info.T_DESCRIPTION;
            $scope.obj.T_AMOUNT = info.T_AMOUNT;
            $scope.obj.T_COST_TYPE_ID = info.T_COST_TYPE_ID;
            $scope.obj.l.selected = { T_LANG2_NAME: info.CostType, T_COST_TYPE_ID: info.T_COST_TYPE_ID };
            $scope.check.active = info.T_ACTIVE;
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