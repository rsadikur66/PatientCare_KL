
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

app.controller("T74001Controller", ["$scope", "$filter", "$http", "$window","LabelService", "T74001Service", "uiGridConstants", "DTOptionsBuilder", "DTColumnBuilder", "Data",
    function ($scope, $filter, $http, $window, LabelService, T74001Service, uiGridConstants, DTOptionsBuilder, DTColumnBuilder, Data) {
        //for instance
         
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.M = {};
        $scope.obj.N = {};
        $scope.obj.l = {};
        

        //For Cascading
        $scope.Type_Click = function () {
            debugger;
            var type = T74001Service.getItemUMCode($scope.obj.M.selected.T_TYPE_ID);
            type.then(function (results) {
                $scope.TypeList = results;
            }); 
        }
        //Cascading End

        //For Entry User
        var EntryUser = T74001Service.EntryUser();
        EntryUser.then(function (data) {
            $scope.obj.T_ENTRY_USER = data;
            $scope.obj.T_ENTRY_DATE = $filter('date')(new Date(), 'dd-MMM-yyyy');
        });
       //Label
        var Label = T74001Service.getLabel();
        Label.then(function (results) {
            $scope.LabelList = results;
        });

        // Form Label Data Service Start 
        var labelData = LabelService.getlabeldata('T74001');
        labelData.then(function (data) {
            $scope.entity = data;
        });
        // Form Label Data Service End 

        //For Item Brand DropDown Start
        var ItemBrandData = T74001Service.getItemBrandCode();
        ItemBrandData.then(function (data) {
            $scope.ItemBrand = data;
        });
        //For Item Brand DropDown End

        //For Type DropDown Start
        var TypeData = T74001Service.getTypeCode();
        TypeData.then(function (data) {
            $scope.TypeData = data;
            
        });
        //For Type DropDown End
       
        $scope.Save_Click = function () {
           if ($scope.obj.T_LANG2_NAME != null) {
                if ($scope.obj.T_ITEM_ID != undefined) {
                    var update = T74001Service.insert_t74001($scope.obj);
                    update.then(function (data) {
                        alert($scope.getSingleMsg('S0003'));
                        //alert("Data Update Succesfully");
                        $window.location = "";
                        });
                } else {
                    var save = T74001Service.insert_t74001($scope.obj);
                    save.then(function (data) {
                        alert($scope.getSingleMsg('S0012'));
                        //alert("Data Save Succesfully");
                           $window.location = "";
                    });
                }
            } else {
                //alert('Engilsh Name is Required');
               alert($scope.getSingleMsg('S0039'));
                angular.element('#txtEnglishName').focus();
            }
        }
        $scope.Delete_Click = function () {
            if ($scope.obj.T_ITEM_ID !== undefined) {
                if ($window.confirm('Are you sure ?')) {
                    var del = T74001Service.delete_t74001($scope.obj);
                    del.then(function (data) {
                        alert($scope.getSingleMsg('S0007'));
                        //alert("Data delete Succesfully");
                        $window.location = "";
                    });
                }
                return false;
            }
            else {
                alert($scope.getSingleMsg('S0040'));
                //alert('Please select Item Brand');
                return false;
            }
        };
        $scope.Clear = function () {
            $scope.obj.T_ITEM_ID = '';
            $scope.obj.T_ITEM_BRA_ID = '';
            $scope.obj.T_TYPE_ID = '';
            $scope.obj.T_ITEM_UM_ID = '';
            $scope.obj.T_LANG1_NAME = '';
            $scope.obj.T_LANG2_NAME = '';
            $scope.obj.M = {};
            $scope.obj.N = {};
            $scope.obj.l = {};
            $scope.obj.T_ITEM_ID = undefined;
        };
        //For Grid Function Start
        $scope.someClickHandler = someClickHandler;
        $scope.dtColumns = [
            //here We will add .withOption('name','column_name') for send column name to the server 
           // DTColumnBuilder.newColumn("RowNumber", "Id").withOption('name', 'T_ITEM_ID'),
            DTColumnBuilder.newColumn("T_LANG2_NAME", "English Name").withOption('name', 'T_LANG2_NAME'),
            DTColumnBuilder.newColumn("T_LANG1_NAME", "Local Name").withOption('name', 'T_LANG1_NAME'),
            DTColumnBuilder.newColumn("BrandName", "Item Brand").withOption('name', 'BrandName'),
            DTColumnBuilder.newColumn("TypeName", "Product Type").withOption('name', 'TypeName'),
            DTColumnBuilder.newColumn("ItemUmName", "Item UM").withOption('name', 'ItemUmName')
        ];

        $scope.dtOptions = DTOptionsBuilder.newOptions().withOption('ajax', {
                dataSrc: "data",
                url: "/T74001/GetLabelData",
                type: "POST"
            })
            .withOption('rowCallback', rowCallback)
            .withOption('processing', true) //for show progress bar
            .withOption('serverSide', true) // for server side processing
            .withPaginationType('full_numbers') // for get full pagination options // first / last / prev / next and page numbers
            .withDisplayLength(10) // Page size
            .withOption('aaSorting', [0, 'asc']) // for default sorting column // here 0 means first column

        function someClickHandler(info) {
            $scope.obj.T_ITEM_ID = info.T_ITEM_ID;
            $scope.obj.T_LANG2_NAME = info.T_LANG2_NAME;
            $scope.obj.T_LANG1_NAME = info.T_LANG1_NAME;
            $scope.obj.T_ITEM_UM_ID = info.T_ITEM_UM_ID;
            $scope.obj.ItemUmName = info.ItemUmName;
            $scope.obj.N.selected = { T_LANG2_NAME: info.ItemUmName, T_ITEM_UM_ID: info.T_ITEM_UM_ID };
            $scope.obj.T_TYPE_ID = info.T_TYPE_ID;
            $scope.obj.TypeName = info.TypeName;
            $scope.obj.M.selected = { T_LANG2_NAME: info.TypeName, T_TYPE_ID: info.T_TYPE_ID }; 
            $scope.obj.T_TYPE_ID = info.T_TYPE_ID;
            $scope.obj.T_ITEM_BRA_ID = info.T_ITEM_BRA_ID;
            $scope.obj.l.selected = { T_LANG2_NAME: info.BrandName, T_ITEM_BRA_ID: info.T_ITEM_BRA_ID };
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