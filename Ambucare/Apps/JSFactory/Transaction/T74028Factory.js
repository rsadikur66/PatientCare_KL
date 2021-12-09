
app.factory('WorkStationFactory', function ($document, $compile, $filter, $rootScope, $templateCache, $http, uiGridConstants) {
    var obj = {};
    $rootScope.items = {};
    var body = $document.find('body');
    return {
        getModal: function (url, data) {
            //scope.households = [];			
            // A new scope for the modal using the passed data
            var scope = $rootScope.$new();
            angular.extend(scope, data);
            function getWorkstationdata() {
                try {
                    var url = '/T74035/GetWorkStationData';
                    debugger;
                    var params = {};
                    return $http({
                        url: url,
                        method: "POST",
                        data: params
                    }).then(function (results) {
                        return results.data;
                    }).catch(function (ex) {
                        throw ex;
                    });
                } catch (ex) {
                    throw ex;
                }
            }
            var dtDisStas = getWorkstationdata();
            dtDisStas.then(function (data) {
                debugger;
                var newDataJSON = JSON.parse(data);
                scope.households = newDataJSON;
                //var data = newDataJSON;
            });
            scope.myAppScopeProvider = {
                showInfo: function (row) {
                    debugger;
                    var rodata = JSON.stringify(row.entity);
                    //var WS_CODE = row.entity.T_WS_CODE;
                    //var scope1 = angular.element($("#txtWsCode")).scope();
                    //scope1.gvw.WS_CODE = WS_CODE;
                    //var WS_NAME = row.entity.WS_NAME;
                    //var scope2 = angular.element($("#txtWsName")).scope();
                    //scope2.gvw.WS_NAME = WS_NAME;


                    //
                    var WS_CODE = row.entity.T_WS_CODE;
                    var scope1 = angular.element($("#txtWsCode")).scope();
                    scope1.items.WS_CODE = WS_CODE;
                    var WS_NAME = row.entity.WS_NAME;
                    var scope2 = angular.element($("#txtWsName")).scope();
                    scope2.items.WS_NAME = WS_NAME;

                    scope.close();
                }
            }

            scope.gridOptions =
            {
                onRegisterApi: function (gridApi) {
                    scope.gridApi = gridApi;
                },
                appScopeProvider: scope.myAppScopeProvider,
                rowTemplate: "<div ng-dblclick=\"grid.appScope.showInfo(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by $index\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>",
                enableRowSelection: true,
                multiSelect: false,
                enableSelectAll: false,
                enableRowHeaderSelection: false,
                enableGridMenu: true,
                noUnselect: true,
                data: 'households',
                paginationPageSizes: [50, 100, 150],
                paginationPageSize: 50,
                columnDefs: [
                    { field: 'WS_NAME', displayName: 'WS Name' },
                    { field: 'T_WS_CODE', displayName: 'WS Code' }
                ],
                filterOptions: { filterText: '<displayName>:<literal>', useExternalFilter: false },
                enableFiltering: true
            };
            // Caching the template for future calls
            var template = $http.get(url, { cache: $templateCache })
                .then(function (response) {

                    // Wrapping the template with some extra markup
                    var modal = angular.element([
                        '<div class="modalDoc">',
                        '<div class="win"><span class ="headerText">Location List</span>',
                        '<a href="#" class="close" ng-click="close()"></a>',
                        //'<div>' + response.data + '</div>',
                        '<div class="gridStyle small-font" ui-grid-selection ui-grid="gridOptions" ui-grid-pagination style="width:auto;"></div>',
                        '</div>',
                        '</div>'
                    ].join(''));

                    // The important part
                    $compile(modal)(scope);
                    // Adding the modal to the body
                    body.append(modal);

                    // A close method
                    scope.close = function () {

                        modal.remove();
                        scope.$destroy();
                    };

                });
        },
        name1: "ok",
    }
})