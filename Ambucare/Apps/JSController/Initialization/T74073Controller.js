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
            // Let the output be the input untouched
            out = items;
        }

        return out;
    };
});

app.controller("T74073Controller", ["$scope", "$filter", "$http", "$window", "LabelService", "T74073Service", "uiGridConstants", "DTOptionsBuilder", "DTColumnBuilder", "Data",
    function ($scope, $filter, $http, $window, LabelService, T74073Service, uiGridConstants, DTOptionsBuilder, DTColumnBuilder, Data) {
        //for instance
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.M = {};
        $scope.addCost = {};

        $scope.check = {
            active: '1'
        };
      
        //Modal Start
        $scope.openDialog = function (id, M) {
            document.getElementById(id).style.display = "block";
            $scope.addCost.T_LANG2_NAME = M.T_LANG2_NAME;
            $scope.addCost.T_LANG1_NAME = M.T_LANG1_NAME;
            $scope.addCost.T_COST_TYPE_ID = M.T_COST_TYPE_ID;
        }
        $scope.closeDialog = function (id) {
            document.getElementById(id).style.display = "none";
            $scope.addCost = {};
            $scope.obj.M = {};
        }
        $scope.addCostType = function () {
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
            if ($scope.addCost.T_LANG2_NAME != null) {
                if ($scope.addCost.T_COST_TYPE_ID != undefined) {
                    var UpdateCostType = T74073Service.insert_t74072($scope.addCost);
                    UpdateCostType.then(function (data) {
                        //alert("Data Updaated Succesfully");
                        alert($scope.getSingleMsg('S0003'));
                        var CostTypeData = T74073Service.GetCostTypeData();
                        CostTypeData.then(function (data) {
                            $scope.CostType = data;
                            $scope.addCost = {};
                        });
                    });
                }
                else {
                    var saveCostType = T74073Service.insert_t74072($scope.addCost);
                    saveCostType.then(function (data) {
                        //alert("Data Save Succesfully");
                        alert($scope.getSingleMsg('S0012'));
                        var CostTypeData = T74073Service.GetCostTypeData();
                        CostTypeData.then(function (data) {
                            $scope.CostType = data;
                            $scope.addCost = {};
                        });
                    });
                }
                        
            } else {
                //alert('Engilsh Name is Required');
                alert($scope.getSingleMsg('M0003'));
                angular.element('#txtEnglishNamePopUp').focus();
            }
        }
        //Modal End

        //For Entry User
        var EntryUser = T74073Service.EntryUser();
        EntryUser.then(function (data) {
            $scope.obj.T_ENTRY_USER = data;
            $scope.obj.T_ENTRY_DATE = $filter('date')(new Date(), 'dd-MMM-yyyy');
        });
        //Label
        var Label = T74073Service.getLabel();
        Label.then(function (results) {
            $scope.LabelList = results;
        });

        // Form Label Data Service Start 
        var labelData = LabelService.getlabeldata('T74073');
        labelData.then(function (data) {
            $scope.entity = data;
        });
        // Form Label Data Service End 

        //For DropDown Start
        var CostTypeData = T74073Service.GetCostTypeData();
        CostTypeData.then(function (data) {
            $scope.CostType = data;
        });
        //For DropDown End
        
        $scope.Save_Click = function () {
            if ($scope.obj.T_LANG2_NAME != null) {
                if ($scope.obj.T_COST_TYPE_DTL_ID != undefined) {
                    var update = T74073Service.insert_t74073($scope.obj);
                    update.then(function (data) {
                        //alert("Data Update Succesfully");
                        alert($scope.getSingleMsg('S0012'));
                        $window.location.reload();

                    });
                } else {
                    var save = T74073Service.insert_t74073($scope.obj);
                    save.then(function (data) {
                            //alert("Data Save Succesfully");
                        alert($scope.getSingleMsg('S0003'));
                        $window.location.reload();
                    });
                }
           } else {
               $scope.closeDialog('CostTypediv');
                // $window.confirm('English Name is requered');
                alert($scope.getSingleMsg('M0003'));
                angular.element('#txtEnglishName').focus();
            }
        }
        $scope.Delete_Click = function () {
            if ($scope.obj.T_COST_TYPE_DTL_ID !== undefined) {
                if ($window.confirm('Are you sure ?')) {
                    var del = T74073Service.delete_t74073($scope.obj);
                    del.then(function (data) {
                       // alert("Data delete Succesfully");
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
            $scope.closeDialog('CostTypediv');
            $scope.obj.T_COST_TYPE_DTL_ID = '';
            $scope.obj.T_LANG1_NAME = '';
            $scope.obj.T_LANG2_NAME = '';
            $scope.obj.M = {};
            $scope.addCost.T_LANG1_NAME = '';
            $scope.addCost.T_LANG2_NAME = '';
            
            $scope.obj.T_COST_TYPE_DTL_ID = undefined;
        };
        //For Grid Function Start
        $scope.someClickHandler = someClickHandler;
        $scope.dtColumns = [
            //here We will add .withOption('name','column_name') for send column name to the server 
            DTColumnBuilder.newColumn("RowNumber", "Id").withOption('name', 'T_COST_TYPE_DTL_ID').notVisible(),
            DTColumnBuilder.newColumn("T_LANG2_NAME", "English Name").withOption('name', 'T_LANG2_NAME'),
            DTColumnBuilder.newColumn("T_LANG1_NAME", "Local Name").withOption('name', 'T_LANG1_NAME'),
            DTColumnBuilder.newColumn("CostType", "Price Type").withOption('name', 'CostType')
        ];

        $scope.dtOptions = DTOptionsBuilder.newOptions().withOption('ajax', {
                dataSrc: "data",
                url: "/T74073/GetLabelData",
                type: "POST"
            })
            .withOption('rowCallback', rowCallback)
            .withOption('processing', true) //for show progress bar
            .withOption('serverSide', true) // for server side processing
            .withPaginationType('full_numbers') // for get full pagination options // first / last / prev / next and page numbers
            .withDisplayLength(10) // Page size
            .withOption('aaSorting', [0, 'asc']) // for default sorting column // here 0 means first column

        function someClickHandler(info) {
            $scope.obj.T_COST_TYPE_DTL_ID = info.T_COST_TYPE_DTL_ID;
            $scope.obj.T_LANG2_NAME = info.T_LANG2_NAME;
            $scope.obj.T_LANG1_NAME = info.T_LANG1_NAME;
            $scope.obj.T_COST_TYPE_ID = info.T_COST_TYPE_ID;
            $scope.obj.CostType = info.CostType;
            $scope.obj.M.selected = { T_LANG2_NAME: info.CostType, T_COST_TYPE_ID: info.T_COST_TYPE_ID };
            $scope.check.active = info.T_ACTIVE;
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