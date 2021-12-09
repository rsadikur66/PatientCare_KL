app.factory('T74113DiagonosisListFactory', function ($document, $compile, $filter, $rootScope, $templateCache, $http, uiGridConstants, T74113Service) {
    var body = $document.find('body');
    return {
        getModal: function (url, data, index,req_Id,diagonosisList) {
            var $scope = $rootScope.$new();
            angular.extend($scope, data);
            
            var dt = T74113Service.DiagonosisList();
            dt.then(function (data) {
                //var newDataJSON = JSON.parse(data);
                //$scope.data = newDataJSON;
                $scope.data = data;
            });
            $scope.myAppScopeProvider = {
                showInfo: function (row) {
                    var chk = 0;
                    for (var i = 0; i < diagonosisList.length; i++) {
                        if (diagonosisList[i].T_COST_TYPE_DTL_ID === row.entity.Id) {
                        chk++;
                    }
                    }
                    if (chk===0) {
                    var scope0 = angular.element($("#txtRequestId" + index)).scope();
                    scope0.obj.T_REQUEST_ID = req_Id;
                    var scope1 = angular.element($("#txtCostTypeDtlId" + index)).scope();
                    scope1.obj.T_COST_TYPE_DTL_ID = row.entity.Id;
                    var scope2 = angular.element($("#txtCostName" + index)).scope();
                    scope2.obj.T_LANG2_NAME = row.entity.Name;
                    var scope3 = angular.element($("#txtPrice" + index)).scope();
                    scope3.obj.T_PRICE = row.entity.Price;
                    var scope4 = angular.element($("#txtTotalPrice")).scope();
                    scope4.obj.TotalPrice += row.entity.Price;
                    var scope5 = angular.element($("#txtDiagonosis")).scope();
                    scope5.obj.T_DIAGONOSIS = index + 1 + " Tests" +" , Total Ammount "+ scope4.obj.TotalPrice;
                     }   
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
                    { field: 'Name', displayName: 'Name' },
                    { field: 'Price', displayName: 'Price' }
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
                        '<div class="win"><span class ="headerText">Diagonosis List</span>',
                        '<a href="#" class="close" ng-click="close()"></a>',
                        '<div class="gridStyle small-font" ui-grid-selection ui-grid="gridOptions" ui-grid-pagination style="width:auto;"></div>',
                        '</div>',
                        '</div>'
                    ].join(''));
                    // The important part
                    $compile(modal)($scope);
                    // Adding the modal to the body
                    body.append(modal);
                    // A close method
                    $scope.close = function () {
                        modal.remove();
                        $scope.$destroy();
                    };

                });
        }
    };
});