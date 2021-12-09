app.controller("T74150Controller",
    ["$rootScope","$scope", "$compile", "$filter", "$http", "$window", "LabelService", "T74150Service", "uiGridConstants", "DTOptionsBuilder", "DTColumnBuilder", "Data",
        function ($rootScope,scope, $compile, $filter, $http, $window, LabelService, T74150Service, uiGridConstants, DTOptionsBuilder, DTColumnBuilder, Data) {
            //For Instance 
            scope.obj = {};
            scope.obj = Data;
            scope.obj.K = {};
            scope.obj.D = {};
            scope.obj.E = {};
            scope.obj.K.selected = {};
            scope.obj.D.selected = {};
            scope.obj.E.selected = {};
            scope.dtInstance = {};
            
            scope.tabs = [
                //{ title: 'Message', url: scope.vrDir + '/Views/Transaction/T74121.cshtml', hdn: 'P' },
                { title: 'Product', url: 'tabProduct.tpl.html', hdn: 'G' },
                //{ title: 'Price', url: scope.vrDir + '/PartPages/T74151.html', hdn: 'P' },
                { title: 'Stock', url: scope.vrDir + '/PartPages/T74152.html', hdn: 'S' }
                //{ title: 'Discharge Reason', url: scope.vrDir + '/PartPages/T74145.html', hdn: 'R' },
            ];
            scope.currentTab = 'tabProduct.tpl.html'; //scope.vrDir + '/Views/Transaction/T74121.cshtml';
            scope.obj.hiddenField = 'G';
            scope.onClickTab = function (tab) {
                scope.obj.hiddenField = tab.hdn;
                scope.currentTab = tab.url;
            };
            scope.isActiveTab = function (tabUrl) { return tabUrl === scope.currentTab; };

            //document.getElementById('Div_Generic').style.display = "none";
            //document.getElementById('divformInfo').style.display = "none";
            var labelData = LabelService.getlabeldata('T74150');
            labelData.then(function (data) {
                scope.entity = data;
            });

            //var gridData = T74150Service.allGridData();
            //gridData.then(function (data) {
            //    scope.getAllGridData = JSON.parse(data);
            //});

            //Get itemType list To Start
            var itemType = T74150Service.GetItemTypeList();
            itemType.then(function (data) {
                scope.itemTypeList = JSON.parse(data);
            });

            var medList = T74150Service.GetFormList();
            medList.then(function (data) {
                scope.formList = JSON.parse(data);
            });

            //scope.GenOnselect = function () {
            //    var medList = T74150Service.GetFormList();
            //    medList.then(function (data) {
            //        scope.formList = JSON.parse(data);
            //    });
            //};


            var generics = T74150Service.GetGenList();
            generics.then(function (data) {
                scope.genericList = JSON.parse(data);
            });

            var itemtype = 23;
            scope.itemTypeOnselect = function (itemtypeid) {
                if (itemtypeid == 23) {
                    clear();
                    document.getElementById('Div_Generic').style.display = "Block";
                    itemtype = itemtypeid;
                    scope.dtColumns[5].bVisible = true;
                    scope.dtColumns[7].bVisible = true;
                    scope.dtInstance.reloadData();
                    
                    //document.getElementById('divformInfo').style.display = "Block";
                } else if (itemtypeid == 121) {
                    clear();
                    document.getElementById('Div_Generic').style.display = "none";
                    ////document.getElementById('divformInfo').style.display = "none";
                    ////scope.obj.D.selected = '';
                    itemtype = itemtypeid;
                    //console.log(scope.dtColumns.bVisible = false);
                    scope.dtColumns[5].bVisible = false;
                    scope.dtColumns[7].bVisible = false;
                    scope.dtInstance.reloadData();
                    console.log(scope.dtInstance);

                }
            };


            scope.Save_Click = function () {
                if (scope.obj.hiddenField === 'G') {
                    //$rootScope.$emit('T74146Emit', 'g');
                    if (scope.obj.T_COST_TYPE_ID != null) {
                        var update = T74150Service.insert(scope.obj.T_ID, scope.obj.T_COST_TYPE_DTL_ID,
                            scope.obj.T_COST_TYPE_ID,
                            scope.obj.T_GEN_CODE,
                            scope.obj.T_FORM_CODE,
                            scope.obj.T_LANG2_NAME,
                            scope.obj.T_LANG1_NAME);
                        update.then(function (data) {
                            alert(data);
                            //swal(data,"success");
                            scope.dtInstance.reloadData();
                            clear();
                            $window.location.reload();
                            
                        });
                        
                    }
                } else if (scope.obj.hiddenField === 'P') {
                    $rootScope.$emit('T74121Emit', 'p');
                }else if (scope.obj.hiddenField === 'S') {
                    $rootScope.$emit('T74027Emit', 's');
                }
                //else if ($scope.obj.hiddenField === 'R') {
                //    $rootScope.$emit('T74145Emit', 'r');
                //}
            };
            
            scope.Clear = function () {
                scope.obj.K.selected = '';
                clear();
            };
            function clear() {
                scope.obj.D.selected = '';
                scope.obj.E.selected = '';
                scope.obj.T_LANG2_NAME = '';
                scope.obj.T_LANG1_NAME = '';
            }


            //For Grid Function Start 
            scope.someClickHandler = someClickHandler;
            
            scope.dtOptions = DTOptionsBuilder.newOptions().withOption('ajax', {
                dataSrc: "data",
                url: scope.vrDir + "/T74150/allGridData",
                type: "POST",
                data: function(p) {
                    p.typeid = itemtype;
                    // alert(Id);
                }
            })
                //.withDataProp('data')
                .withOption('rowCallback', rowCallback)
                .withOption('processing', true) //for show progress bar
                .withOption('serverSide', true) // for server side processing
                .withPaginationType('full_numbers') // for get full pagination options // first / last / prev / next and page numbers
                .withDisplayLength(10) // Page size
                .withOption('aaSorting', [0, 'asc'])// for default sorting column // here 0 means first column

            scope.dtColumns = [
                //here We will add .withOption('name','column_name') for send column name to the server 
                DTColumnBuilder.newColumn("T_COST_TYPE_DTL_ID", "Cost Type Dtl").withOption('name', 'T_COST_TYPE_DTL_ID').notVisible(),
                DTColumnBuilder.newColumn("T_ITEM_CODE", "Item Code").withOption('name', 'T_ITEM_CODE'),
                DTColumnBuilder.newColumn("ITEM_TYPE_CODE", "Item Type Code").withOption('name', 'ITEM_TYPE_CODE').notVisible(),
                DTColumnBuilder.newColumn("ITEM_TYPE", "Item Type").withOption('name', 'ITEM_TYPE'),
                DTColumnBuilder.newColumn("T_GEN_CODE", "Generic Code").withOption('name', 'T_GEN_CODE').notVisible(),
                DTColumnBuilder.newColumn("GenericName", "Generic Name").withOption('name', 'GenericName'),
                DTColumnBuilder.newColumn("T_FORM_CODE", "Form Code").withOption('name', 'T_FORM_CODE').notVisible(),
                DTColumnBuilder.newColumn("FORM_TYPE", "Form Type").withOption('name', 'FORM_TYPE'),
                DTColumnBuilder.newColumn("T_LANG1_NAME", "Local Name").withOption('name', 'T_LANG1_NAME'),
                DTColumnBuilder.newColumn("T_LANG2_NAME", "English Name").withOption('name', 'T_LANG2_NAME')

            ];
            function someClickHandler(info) {
                scope.obj.T_COST_TYPE_DTL_ID = info.T_CANCEL_REASON_ID;
                scope.obj.T_LANG2_NAME = info.T_LANG2_NAME;
                scope.obj.T_LANG1_NAME = info.T_LANG1_NAME;
                scope.obj.T_ID = info.T_ITEM_CODE;
                scope.obj.T_COST_TYPE_DTL_ID = info.T_COST_TYPE_DTL_ID;
                
                scope.obj.K.selected ={T_LANG2_NAME: info.ITEM_TYPE, T_COST_TYPE_ID: info.ITEM_TYPE_CODE};
                scope.obj.E.selected = { T_LANG2_NAME: info.GenericName, T_GEN_CODE: info.T_GEN_CODE};
                scope.obj.D.selected = { T_LANG2_NAME: info.FORM_TYPE, T_FORM_CODE: info.T_FORM_CODE };
                if (info.ITEM_TYPE_CODE == 121) {
                    document.getElementById('Div_Generic').style.display = "none";
                    //document.getElementById('divformInfo').style.display = "none";
                }
                
            }
            //scope.reloadData = reloadData;
            //scope.dtInstance = {};

            //function reloadData() {
            //    scope.dtInstance.reloadData();
            //}

           
            
            scope.dtIntanceCallback = function(instance) {
                scope.dtInstance = instance;
            };
            function reloadData() {
                scope.dtInstance.reloadData();
            }

            

            function rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                // Unbind first in order to avoid any duplicate handler (see https://github.com/l-lin/angular-datatables/issues/87)
                $('td', nRow).unbind('click');
                $('td', nRow).bind('click', function () {
                    scope.$apply(function () {
                        scope.someClickHandler(aData);
                    });
                });
                return nRow;
            }

            //function ddlText(e) {
            //    var a = scope.cancelType.filter(function (a) { return a.CancelType == e; });
            //    return a[0].CancelText;
            //}
            //For Grid Function End 

        }]);



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
    //
});

