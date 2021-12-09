app.factory('T74114ItemTypeFactory', function ($document, $compile, $filter, $rootScope, $templateCache, $http, uiGridConstants, T74114Service) {
    var body = $document.find('body');

    return {
        getModal: function (url, data, index) {
            var scope = $rootScope.$new();
            angular.extend(scope, data);

            var dt = T74114Service.GetItemTypeList();
            dt.then(function (data) {
                scope.data = data;
            });
            scope.myAppScopeProvider = {
                showInfo: function (row) {
                    var scopeId = angular.element($("#txtTypeId" + index)).scope();
                    scopeId.obj.T_COST_TYPE_ID = row.entity.T_COST_TYPE_ID;
                    var scopeItm = angular.element($("#txtItemType"+index)).scope();
                    scopeItm.obj.T_LANG2_NAME = row.entity.T_LANG2_NAME;

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
                    { field: 'T_LANG2_NAME', displayName: 'Item Type' }
                ],
                filterOptions: { filterText: '<displayName>:<literal>', useExternalFilter: false },
                enableFiltering: true
            };
            var template = $http.get(url, { cache: $templateCache })
                .then(function (response) {
                    var modal = angular.element([
                        '<div class="modalDoc">',
                        '<div class="win"><span class ="headerText">Item Type List</span>',
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