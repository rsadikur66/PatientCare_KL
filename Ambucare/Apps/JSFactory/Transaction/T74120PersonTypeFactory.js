app.factory('T74120PersonTypeFactory', function ($document, $compile, $filter, $rootScope, $templateCache, $http, uiGridConstants, T74120Service) {
    $rootScope.obj = {};
    var body = $document.find('body');
    return {
        getModal: function (url, data) {
            // A new scope for the modal using the passed data
            var scope = $rootScope.$new();
            angular.extend(scope, data);
            var dt = T74120Service.GetPersonType();
            dt.then(function (data) {
                scope.data = data;
            });

            scope.myAppScopeProvider = {
                showInfo: function (row) {
                    var rodata = row.entity;
                    var scope1 = angular.element($("#txtPersonType")).scope();
                    scope1.obj.t74080.T_LANG2_NAME = rodata.T_LANG2_NAME;
                    var scope2 = angular.element($("#txtTypeId")).scope();
                    scope2.obj.t74080.T_EMP_TYP_ID = rodata.T_EMP_TYP_ID;
                    scope.close();
                }

            }
            scope.gridOptions =
            {
                onRegisterApi: function (gridApi) {
                    scope.gridApi = gridApi;
                },
                appScopeProvider: scope.myAppScopeProvider,
                rowTemplate: "<div ng-click=\"grid.appScope.showInfo(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>",
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
                    { field: 'T_LANG2_NAME', displayName: 'Person Type' }
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
                        '<div class="win"><span class ="headerText">Person Type List</span>',
                        '<a href="#" class="close" ng-click="close()"></a>',
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
        }
    };
});

