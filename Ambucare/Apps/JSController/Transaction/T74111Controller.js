//For Dropdown List
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

app.controller("T74111Controller",
    ["$scope", "$compile", "$filter", "$http", "$window", "T74111Service", "LabelService", "uiGridConstants", "DTOptionsBuilder", "DTColumnBuilder", "Data",
        function (scope, $compile, $filter, $http, $window, T74111Service, LabelService, uiGridConstants, DTOptionsBuilder, DTColumnBuilder, Data) {
            //for instance
            scope.obj = {};
            scope.obj = Data;
            scope.obj.A = {};   
            scope.message = '';
            scope.edit = edit;
            scope.obj.T_COST_TYPE_ID = {};
            
            //vm.delete = deleteRow;
            // Form Label Data Service Start
            var labelData = LabelService.getlabeldata('T74111');
            labelData.then(function (data) {
                scope.entity = data;
            });
            // Form Label Data Service End

            //Get Ambulance List Start
            var ambulanceId = T74111Service.getAmbulanceDropdownList();
            ambulanceId.then(function (data) {
                scope.ambulance = data;
            });
            //Get Ambulance List End
            
            //Get Ambulance Type Start
            var CostId = 0;

            scope.Ambu_click = function (CostTypeId) {
                CostId = CostTypeId;
                //For Grid Load Code when Select Dropdown Data
                scope.dtInstance.reloadData();
            }
            scope.Save = function () {
                var save = T74111Service.Insert_T74073(sessionStorage.getItem("Object"));
                save.then(function (mesg) {
                    CostId = 0;                    
                    //alert("Data Updated Successfully");
                    alert($scope.getSingleMsg('S0003'));

                    reloadData();
                });
            }
            //for checkbox 
            //scope.selected = {};
            scope.persons = {};
            //scope.selectAll = false;
            //scope.toggleAll = toggleAll;
            //scope.toggleOne = toggleOne;
            
           // var titleHtml = '<input type="checkbox" ng-model="selectAll" ng-click="toggleAll(selectAll, selected)">';
            scope.dtOptions = DTOptionsBuilder.newOptions().withOption('ajax',
                {
                    dataSrc: "data",
                    url: "/T74111/GetGridData",
                    type: "POST",
                    data: function (p) {
                        p.CompId = CostId;
                        // alert(Id);
                    }
                })
                .withDataProp('data')
                .withOption('processing', true) //for show progress bar
                .withOption('serverSide', true) // for server side processing
                .withPaginationType('full_numbers') // for get full pagination options // first / last / prev / next and page numbers
                .withOption('headerCallback', function (header) {
                    if (!scope.headerCompiled) {
                        // Use this headerCompiled field to only compile header once
                        scope.headerCompiled = true;
                        $compile(angular.element(header).contents())(scope);
                    }
                })
                .withOption('createdRow', function (row, data, dataIndex) {
                    // Recompiling so we can bind Angular directive to the DT
                    $compile(angular.element(row).contents())(scope);
                })
                .withDisplayLength(10) // Page size
                .withOption('aaSorting', [0, 'asc']); // for default sorting column // here 0 means first column

            scope.dtColumns = [
                //here We will add .withOption('name','column_name') for send column name to the server 
                DTColumnBuilder.newColumn("T_COST_TYPE_DTL_ID", "Cost Type Details ID").withOption('name', 'T_COST_TYPE_DTL_ID').notVisible(),
                DTColumnBuilder.newColumn("T_LANG2_NAME", "Description").withOption('name', 'T_LANG2_NAME'),
                DTColumnBuilder.newColumn('T_PRICE', 'Get Price')
                    .renderWith(function (data) {
                        return '<input type="text" readonly="readonly" id="' + data + '" value="' + data + '"/>'
                    }),
                DTColumnBuilder.newColumn(null).withTitle('Set Price').notSortable()
                    .renderWith(actionsHtml),
                DTColumnBuilder.newColumn("T_INCHARGABLE", "Inchargable").withOption('name', 'T_INCHARGABLE').notVisible()
                // DTColumnBuilder.newColumn("T_ACTIVE", "Active").notSortable().renderWith(activeHtml),
                //For Check box Control
                //DTColumnBuilder.newColumn(null).withTitle(titleHtml).notSortable()
                //    .renderWith(function (data, type, full, meta) {
                //        scope.selected[full.T_COST_TYPE_DTL_ID] = false;
                //        return '<input type="checkbox" ng-model="selected[' + data.T_COST_TYPE_DTL_ID + ']" ng-click="toggleOne(selected)">';
                //    })
            ];
            scope.reloadData = reloadData;
            scope.dtInstance = {};

            function reloadData() {
                scope.dtInstance.reloadData();
            }
            function createdRow(row, data, dataIndex) {
                // Recompiling so we can bind Angular directive to the DT
                $compile(angular.element(row).contents())(scope);
            }
            //For checkbox start
            function actionsHtml(data, type, full, meta) {
                scope.persons[data.T_COST_TYPE_DTL_ID] = data;
                return '<input type="text" id="' + data.T_COST_TYPE_DTL_ID + '"   ng-blur="edit(persons[' + data.T_COST_TYPE_DTL_ID + '])" class="btn btn-warning" />';
            }
            //function toggleAll(selectAll, selectedItems) {
            //    for (var id in selectedItems) {
            //        if (selectedItems.hasOwnProperty(id)) {
            //            selectedItems[id] = selectAll;
            //        }
            //    }

            //}
            var childObjectArray = [];
            //function toggleOne(selectedItems) {
            //    debugger;
            //    for (var id in selectedItems) {
            //        if (selectedItems.hasOwnProperty(id)) {
            //            if (!selectedItems[id]) {
            //                scope.selectAll = false;
            //                return;
            //            }
            //        }
            //    }

            //    scope.selectAll = true;
            //}
            //For checkbox end
            function edit(info) {
                var costtypeId = angular.element(document.getElementById(info.T_COST_TYPE_DTL_ID)).val();
                if (costtypeId != "") {
                    var newObject = {};
                    newObject.T_COST_TYPE_DTL_ID = info.T_COST_TYPE_DTL_ID;
                    newObject.T_PRICE = costtypeId;
                    childObjectArray.push(newObject);
                    sessionStorage.setItem("Object", JSON.stringify(childObjectArray)); 
                }
            }
            //For Print Button Function Start

            scope.Print = function (id) {
                $window.open("../T74111Report/GetData?id=" +scope.obj. T_COST_TYPE_ID, "popup","width=600,height=600,left=50,top=50");
            }
            //For Print Button Function End
           
        }]);

