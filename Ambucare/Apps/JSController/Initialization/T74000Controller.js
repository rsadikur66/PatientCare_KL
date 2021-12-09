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

app.controller("T74000Controller", ["$scope", "$filter", "$http", "$window", "T74000Service", "LabelService", "uiGridConstants", "DTOptionsBuilder", "DTColumnBuilder", "Data","$rootScope",
    function ($scope, $filter, $http, $window, T74000Service, LabelService,uiGridConstants, DTOptionsBuilder, DTColumnBuilder, Data, $rootScope) {
        //For Gried Data
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.F = {};
        $scope.dtInstance = {};

        var Label = T74000Service.getLabel();
        Label.then(function (results) {
            $scope.LabelList = results;
        });
        //For Entry User
        var EntryUser = T74000Service.EntryUser();
        EntryUser.then(function (data) {
            $scope.obj.T_ENTRY_USER = data;
            $scope.obj.T_ENTRY_DATE = $filter('date')(new Date(), 'dd-MMM-yyyy');
        });

        // Form Label Data Service Start 
        var labelData = LabelService.getlabeldata('T74000');
        labelData.then(function (data) {
            $scope.entity = data;
        });
        // Form Label Data Service End 

        //For Label Brand DropDown Start
        var FromData = T74000Service.GetFormData();
        FromData.then(function (data) {
            $scope.FormCode = data;
        });
        //For Label Brand DropDown End
        var disable = $rootScope.$on('T74000Emit', function (event, data) {
            if (data == 'm') {
                Save_Click();
            } else if (data == 'Clear') {
                Clear();
            }
        });
        $scope.$on('$destroy', function () {
            disable();
        });
        //For Insert Update Start

        $scope.Save_Click = function () {
            Save_Click();
        }


        function Save_Click () {

            if ($scope.obj.T_FORM_CODE != null) {
                if ($scope.obj.T_LABEL_ID != undefined) {
                    var update = T74000Service.InsertOrUpdate($scope.obj);
                    update.then(function (data) {
                        //alert("Data Update Succesfully");
                        alert($scope.getSingleMsg('S0003'));
                        //$window.location = "";
                        Clear();
                        $scope.dtInstance.reloadData();
                        console.log($scope.dtInstance);
                    });
                } else {
                    var save = T74000Service.InsertOrUpdate($scope.obj);
                    save.then(function (data) {
                        //alert("Data Save Succesfully");
                        alert($scope.getSingleMsg('S0012'));
                        Clear();
                        $scope.dtInstance.reloadData();
                        //$window.location = "";
                    });
                }
            }
            else {
                //alert('From Code is Required');
                alert($scope.getSingleMsg('S0034'));
                angular.element('#txtFromCode').focus();

            }
        }

        //For Clear
        $scope.Clear = function () {
            Clear();
        }

        function Clear() {
            $scope.obj.T_LABEL_ID = '';
            $scope.obj.T_LABEL_NAME = '';
            $scope.obj.T_LANG1_TEXT = '';
            $scope.obj.T_LANG2_TEXT = '';
            $scope.obj.F = {};
            // $scope.FromCode();

            $scope.obj.T_LABEL_ID = undefined;
        }

        

        //For Delete
        $scope.Delete_Click = function () {
            if ($scope.obj.T_LABEL_ID != null) {
                if ($window.confirm('Are you sure to want delete?')) {
                    var dele = T74000Service.deleteData($scope.obj.T_LABEL_ID);
                    dele.then(function (data) {
                        alert($scope.getSingleMsg('S0007'));
                        //alert("Data Deleted Succesfully");
                        $window.location = "";
                    });
                }
            } else {
                alert($scope.getSingleMsg('S0011'));
                //alert("Select a data for delete.");
            }
        }

        //For Grid Function Start 

        //console.log($scope.dtInstance.reloadData());


        $scope.dtInstanceCallback = function(instance) {
            $scope.dtInstance = instance;
        }

        $scope.someClickHandler = someClickHandler;
        $scope.dtColumns = [
            //here We will add .withOption('name','column_name') for send column name to the server 
            DTColumnBuilder.newColumn("RowNumber", "Id").withOption('name', 'T_LABEL_ID').notVisible(),
            DTColumnBuilder.newColumn("T_LABEL_NAME", "Label Name").withOption('name', 'T_LABEL_NAME'),
            DTColumnBuilder.newColumn("T_FORM_CODE", "Form Code").withOption('name', 'T_FORM_CODE'),
            DTColumnBuilder.newColumn("T_LANG2_TEXT", "English Name").withOption('name', 'T_LANG2_TEXT'),
            DTColumnBuilder.newColumn("T_LANG1_TEXT", "Local Name").withOption('name', 'T_LANG1_TEXT')
        ];
        $scope.dtOptions = DTOptionsBuilder.newOptions().withOption('ajax', {
                dataSrc: "data",
          url: $scope.vrDir +"/T74000/GetLabelData",
                type: "POST"
            })
            .withOption('rowCallback', rowCallback)
            .withOption('processing', true) //for show progress bar
            .withOption('serverSide', true) // for server side processing
            .withPaginationType('full_numbers') // for get full pagination options // first / last / prev / next and page numbers
            .withDisplayLength(10) // Page size
            .withOption('aaSorting', [0, 'asc']) // for default sorting column // here 0 means first column
        function someClickHandler(info) {
            $scope.obj.T_LABEL_ID = info.T_LABEL_ID;
            $scope.obj.T_LABEL_NAME = info.T_LABEL_NAME;
            //$scope.obj.T_FORM_CODE = info.T_FORM_CODE;
            $scope.obj.T_LANG2_TEXT = info.T_LANG2_TEXT;
            $scope.obj.T_LANG1_TEXT = info.T_LANG1_TEXT;
            $scope.obj.F.selected = { T_FORM_CODE: info.T_FORM_CODE };
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