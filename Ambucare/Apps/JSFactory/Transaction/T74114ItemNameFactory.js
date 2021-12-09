app.factory('T74114ItemNameFactory', function ($document, $compile, $filter, $rootScope, $templateCache, $http, uiGridConstants, T74114Service) {
    var body = $document.find('body');

    return {
        getModal: function (url, data, index,id) {
            var scope = $rootScope.$new();
            angular.extend(scope, data);

            var dt = T74114Service.GetItemNameList(id);
            dt.then(function (data) {
                scope.data = data;
            });

                scope.myAppScopeProvider = {
                    showInfo: function (row) {
                        var scope1 = angular.element($("#txtItemId" + index)).scope();
                        scope1.obj.T_ITEM_ID = row.entity.T_ID;
                        var scope2 = angular.element($("#ItemName" + index)).scope();
                        scope2.obj.CostType = row.entity.CostType;

                        scope.close();
                    }
                }
             
            
            scope.gridOptions =
            {
                onRegisterApi: function (gridApi) {
                    scope.gridApi = gridApi;
                },
                appScopeProvider: scope.myAppScopeProvider,
                rowTemplate:
                    "<div ng-click=\"grid.appScope.showInfo(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>",
                enableRowSelection: true,
                multiSelect: false,
                enableSelectAll: false,
                enableRowHeaderSelection: false,
                enableGridMenu: true,
                noUnselect: true,
                data: 'data',
                paginationPageSizes: [50, 100, 150],
                paginationPageSize: 50,
                columnDefs: [
                    { field: 'CostType', displayName: 'Item Name' }
                ],
                filterOptions: { filterText: '<displayName>:<literal>', useExternalFilter: false },
                enableFiltering: true
            };
            var template = $http.get(url, { cache: $templateCache })
                .then(function (response) {
                    var modal = angular.element([
                        '<div class="modalDoc">',
                        '<div class="win"><span class ="headerText">Item List</span>',
                        '<a href="#" class="close" ng-click="close()"></a>',
                        '<div class="gridStyle small-font" ui-grid-selection ui-grid="gridOptions" ui-grid-pagination style="width:auto;"></div>',
                        '</div>',
                        '</div>'
                    ].join(''));
                    $compile(modal)(scope);
                    body.append(modal);
                    scope.close = function () {
                        modal.remove();
                        scope.$destroy();
                    };

                });
        }
    };
});