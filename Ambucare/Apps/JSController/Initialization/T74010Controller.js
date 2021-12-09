﻿
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

app.controller("T74010Controller", ["$scope", "$filter", "$http", "$window", "LabelService", "uiGridConstants", "DTOptionsBuilder", "DTColumnBuilder", "Data","T74010Service",
    function ($scope, $filter, $http, $window, LabelService, uiGridConstants, DTOptionsBuilder, DTColumnBuilder, Data, T74010Service) {

        //For Gried Data
        $scope.obj = {};
        $scope.obj = Data;

        var Label = T74010Service.getLabel();
        Label.then(function (results) {
            $scope.LabelList = results;
        });
         //For Entry User
        var EntryUser = T74010Service.EntryUser();
        EntryUser.then(function (data) {
            $scope.obj.T_ENTRY_USER = data;
            $scope.obj.T_ENTRY_DATE = $filter('date')(new Date(), 'dd-MMM-yyyy');
        });

        // Form Label Data Service Start 
        var labelData = LabelService.getlabeldata('T74010');
        labelData.then(function (data) {
            $scope.entity = data;
        });
        // Form Label Data Service End 

        //For Insert Update Start
        $scope.Save_Click = function() {
            if ($scope.obj.T_LANG1_NAME == undefined || $scope.obj.T_LANG1_NAME == '') {
                //$window.confirm('Local Name is requered');
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
            if ($scope.obj.T_LANG2_NAME !== undefined) {
                if ($scope.obj.T_SUPPLI_ID != undefined) {
                    var update = T74010Service.InsertOrUpdate($scope.obj);
                    update.then(function(data) {
                        // alert("Data Update Succesfully");
                        alert($scope.getSingleMsg('S0003'));
                        $window.location = "";
                    });
                } else {
                    var save = T74010Service.InsertOrUpdate($scope.obj);

                    save.then(function(data) {
                        // alert("Data Save Succesfully");
                        alert($scope.getSingleMsg('S0012'));
                        $window.location = "";

                    });
                }
            } else {
                //alert('Engilsh Name is Required');
                alert($scope.getSingleMsg('M0003'));
                angular.element('#txtEnglishName').focus();
            }
        };

        //For Clear
        $scope.Clear = function() {
            $scope.obj.T_SUPPLI_ID = '';
            $scope.obj.T_LANG1_NAME = '';
            $scope.obj.T_LANG2_NAME = undefined;

            $scope.obj.T_SUPPLI_ID = undefined;
        };

        //For Delete
        $scope.Delete_Click = function() {

            if ($scope.obj.T_SUPPLI_ID != null) {
                if ($window.confirm('Are you sure to want delete ?')) {
                    var dele = T74010Service.deleteData($scope.obj.T_SUPPLI_ID);
                    dele.then(function(data) {
                        // alert("Data Deleted Succesfully");
                        alert($scope.getSingleMsg('S0007'));
                        $window.location = "";

                    });
                }
            } else {
                //alert("Select a data for delete.");
                alert($scope.getSingleMsg('S0011'));
            }
        };
        
        //For Grid Function Start 
        $scope.someClickHandler = someClickHandler;
        $scope.dtColumns = [
            //here We will add .withOption('name','column_name') for send column name to the server 
            DTColumnBuilder.newColumn("RowNumber", "Supply Id").withOption('name', 'T_SUPPLI_ID').notVisible(),
            DTColumnBuilder.newColumn("T_LANG2_NAME", "English Name").withOption('name', 'T_LANG2_NAME'),
            DTColumnBuilder.newColumn("T_LANG1_NAME", "Local Name").withOption('name', 'T_LANG1_NAME')
        ];
        $scope.dtOptions = DTOptionsBuilder.newOptions().withOption('ajax', {
                dataSrc: "data",
                url: "/T74010/GetLabelData",
                type: "POST"
            })
            .withOption('rowCallback', rowCallback)
            .withOption('processing', true) //for show progress bar
            .withOption('serverSide', true) // for server side processing
            .withPaginationType('full_numbers') // for get full pagination options // first / last / prev / next and page numbers
            .withDisplayLength(10) // Page size
            .withOption('aaSorting', [0, 'asc']) // for default sorting column // here 0 means first column
        function someClickHandler(info) {
            $scope.obj.T_SUPPLI_ID = info.T_SUPPLI_ID;
            $scope.obj.T_LANG2_NAME = info.T_LANG2_NAME;
            $scope.obj.T_LANG1_NAME = info.T_LANG1_NAME;
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
    }
]);