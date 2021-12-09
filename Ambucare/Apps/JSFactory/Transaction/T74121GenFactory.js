
app.factory('GenFactory', function ($document, $compile, $filter, $rootScope, $templateCache, $http, uiGridConstants, T74121Service) {
    var body = $document.find('body');
    return {
        getModal: function (url, data) {
            var scope = $rootScope.$new();
            angular.extend(scope, data);

            var generic = T74121Service.genericData();
            generic.then(function (data) {
              //  $scope.data = JSON.parse(data);
                scope.data = data;
            });
            scope.myAppScopeProvider = {
                showInfo: function (row) {

                    var scope1 = angular.element($("#txtGeneric")).scope();
                    scope1.obj.GEN_CODE = row.entity.GEN_CODE;

                    var scope2 = angular.element($("#txtGenCode")).scope();
                    scope2.obj.GEN_DESC = row.entity.GEN_DESC;

                   

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
                    { field: 'GEN_CODE', displayName: 'Gen Code' },
                    { field: 'GEN_DESC', displayName: 'Generic' }
                ],
                filterOptions: { filterText: '<displayName>:<literal>', useExternalFilter: false },
                enableFiltering: true
            };
            var template = $http.get(url, { cache: $templateCache })
                .then(function (response) {
                    var modal = angular.element([
                        '<div class="modalDoc">',
                        '<div class="win"><span class ="headerText">Generic List</span>',
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