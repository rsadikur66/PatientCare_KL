app.factory('T74113PrscrptnByDocFactory', function ($document, $compile, $filter, $rootScope, $templateCache, $http, uiGridConstants, T74113Service) {
    var body = $document.find('body');

    return {
        getModal: function (url, data, T_DOC_ID, T_PAT_ID) {
            var $scope = $rootScope.$new();
            angular.extend($scope, data);

            var dt = T74113Service.GetPrscrptnByDoc(T_DOC_ID, T_PAT_ID);
            dt.then(function (data) {
                $scope.data = data;
            });
            $scope.myAppScopeProvider = {
                showInfo: function (row) {
                    var scope1 = angular.element($("#txtSlip")).scope();
                    scope1.obj.T_PRESCRIPTION_ID = row.entity.T_PRESCRIPTION_ID;
                    var scope2 = angular.element($("#txtDate")).scope();
                    scope2.obj.T_DATE = row.entity.T_ENTRY_DATE;
                    var scope3 = angular.element($("#txtRemarks")).scope();
                    scope3.obj.t74039.T_REMARKS = row.entity.T_REMARKS;

                    $scope.close();
                }

            }
            $scope.gridOptions =
            {
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                },
                appScopeProvider: $scope.myAppScopeProvider,
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
                    { field: 'T_ENTRY_DATE', displayName: 'Date' },
                    { field: 'T_PRESCRIPTION_ID', displayName: 'Slip No' }
                ],
                filterOptions: { filterText: '<displayName>:<literal>', useExternalFilter: false },
                enableFiltering: true
            };
            var template = $http.get(url, { cache: $templateCache })
                .then(function (response) {
                    var modal = angular.element([
                        '<div class="modalDoc">',
                        '<div class="win"><span class ="headerText">List of Prescription</span>',
                        '<a href="#" class="close" ng-click="close()"></a>',
                        '<div class="gridStyle small-font" ui-grid-selection ui-grid="gridOptions" ui-grid-pagination style="width:auto;"></div>',
                        '</div>',
                        '</div>'
                    ].join(''));
                    $compile(modal)($scope);
                    body.append(modal);
                    $scope.close = function () {
                        modal.remove();
                        $scope.$destroy();
                    };

                });
        }
    };
});