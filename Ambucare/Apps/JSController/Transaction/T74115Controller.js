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
app.controller("T74115Controller",["$scope", "$filter", "$http", "$window", "T74115Service", "uiGridConstants", "DTOptionsBuilder", "DTColumnBuilder","Data", 
    function (scope, $filter, $http, $window, T74115Service, uiGridConstants, DTOptionsBuilder, DTColumnBuilder, Data) {
        scope.obj = {};
        scope.obj = Data;

        scope.TypeList = [
            { TYPE_ID: "SR", TYPE_NAME: "Sales Requisition" },
            { TYPE_ID: "PR", TYPE_NAME: "Purchase Requisition" },
            { TYPE_ID: "TI", TYPE_NAME: "Transfer Issue" },
            { TYPE_ID: "SRT", TYPE_NAME: "Sales Return" }
        ];
        var TypeID = "";

        scope.Type = function (e) {
            TypeID = e;
            scope.dtInstance.reloadData();
        }

            scope.clickHandler = clickHandler;
            scope.dtColumns = [
                //here We will add .withOption('name','column_name') for send column name to the server 
                DTColumnBuilder.newColumn("CMN_ID", "CMN ID").withOption('name', 'CMN_ID').notVisible(),
                DTColumnBuilder.newColumn("CMN_STORE_ID", "CMN_STORE ID").withOption('name', 'CMN_STORE_ID').notVisible(),
                DTColumnBuilder.newColumn("CMN_NO", "SL. No").withOption('name', 'CMN_NO'),
                DTColumnBuilder.newColumn("CMN_LANG2_NAME", "Store Name").withOption('name', 'CMN_LANG2_NAME'),
                DTColumnBuilder.newColumn("CMN_DATE", "Date").withOption('name', 'CMN_DATE').renderWith(function (data, type) {
                    var myDate = new Date(data.match(/\d+/)[0] * 1);
                    return $filter('date')(myDate, "dd/MM/yyyy");
                }),
                DTColumnBuilder.newColumn("CMN_DLVRY_DATE", "Exp. Delivery Date").withOption('name', 'CMN_DLVRY_DATE').renderWith(function (data, type) {
                    var myDate = new Date(data.match(/\d+/)[0] * 1);
                    return $filter('date')(myDate, "dd/MM/yyyy");
                }),
                DTColumnBuilder.newColumn("CMN_ID", "").renderWith(function (data) {
                    var tag = '';
                    if (TypeID === "PR") {
                        tag = '<a id="btnActive" href="/Transaction/T74116?P_PUR_ID=' + data + '" style="width: 100px; color:#c12d2d">Receive</a>';
                       
                    }
                    else if (TypeID === "SR") {
                        tag = '<a id="btnActive" href="/Transaction/T74119?P_SL_REQ_ID=' + data + '" style="width: 100px; color:#c12d2d">Receive</a>';
                        
                    }
                    else if (TypeID === "TI") {
                        tag = '<a id="btnActive" href="/Transaction/T74122?T_TRANSFER_REQ_ID=' + data + '" style="width: 100px; color:#c12d2d">Receive</a>';

                    }
                    return tag;
                })
            ];
        scope.reloadData = reloadData;
        scope.dtInstance = {};

        function reloadData() {
            scope.dtInstance.reloadData();
        }
            
            scope.dtOptions = DTOptionsBuilder.newOptions().withOption('ajax',
                {
                    
                    dataSrc: "data",
                    url: "/T74115/GridData",// Controller/ActionResult
                    type: "POST",
                    data: function (p) {
                        p.TypeID = TypeID;
                       
                    }
                })
                .withOption('rowCallback', rowCallback)
                .withOption('processing', true) //for show progress bar
                .withOption('serverSide', true) // for server side processing
                .withPaginationType(
                'full_numbers') // for get full pagination options // first / last / prev / next and page numbers
                .withDisplayLength(10) // Page size
                .withOption('aaSorting', [0, 'asc']); // for default sorting column // here 0 means first column
            function clickHandler(a) {
               // scope.T74059.T_RLGN_CODE = a.T_RLGN_CODE;
               // scope.T74059.T_Eng = a.T_Eng;
               // scope.T74059.T_Loc = a.T_Loc;
                
            }
            //For  Grid Function End

            function rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                // Unbind first in order to avoid any duplicate handler (see https://github.com/l-lin/angular-datatables/issues/87)
                $('td', nRow).unbind('click');
                $('td', nRow).bind('click', function () {
                    scope.$apply(function () {
                        
                        scope.clickHandler(aData);
                       
                    });
                });
                return nRow;
            }

        }]);