app.factory('T74113TradeListFactory', function ($document, $compile, $filter, $rootScope, $templateCache, $http, uiGridConstants, T74113Service) {
    var body = $document.find('body');
    return {
        getModal: function (url, data, index,tradeList) {
            var $scope = $rootScope.$new();
            angular.extend($scope, data);
            
            var dt = T74113Service.GetMedicineByTrade();
            dt.then(function (data) {
               $scope.data = JSON.parse(data);
            });
            $scope.myAppScopeProvider = {
                showInfo: function (row) {
                    var chk = 0;
                    for (var i = 0; i < tradeList.length; i++) {
                        if (tradeList[i].T_TRADE_CODE === row.entity.TRADE_CODE) {
                        chk++;
                    }
                    }
                    if (chk===0) {
                    var scope1 = angular.element($("#txtItemCode" + index)).scope();
                    scope1.obj.T_ITEM_CODE = row.entity.ITEM_CODE;

                    var scope2 = angular.element($("#txtGenCode" + index)).scope();
                    scope2.obj.T_GEN_CODE = row.entity.GEN_CODE;

                    var scope3 = angular.element($("#txtTradeCode" + index)).scope();
                    scope3.obj.T_TRADE_CODE = row.entity.TRADE_CODE;

                    var scope4 = angular.element($("#ddlStrength" + index)).scope();
                    scope4.obj.S = {};
                    scope4.obj.S.selected = { STRENGTH: row.entity.STRENGTH };
                    var scope44 = angular.element($("#ddlStrengthList" + index)).scope();
                    scope44.obj.StrengthList = [];

                    var scope5 = angular.element($("#ddlForm" + index)).scope();
                    scope5.obj.Fr = {};
                    scope5.obj.Fr.selected = { FORM_CODE: row.entity.FORM_CODE, FORM_DESC: row.entity.FORM_DESC };
                    var scope55 = angular.element($("#ddlFormList" + index)).scope();
                    scope55.obj.FormList = [];

                    var scope6 = angular.element($("#ddlRoute" + index)).scope();
                    scope6.obj.R = {};
                    scope6.obj.R.selected = { ROUTE_CODE: row.entity.ROUTE_CODE, ROUTE_DESC: row.entity.ROUTE_DESC };
                    var scope66 = angular.element($("#ddlRouteList" + index)).scope();
                    scope66.obj.RouteList = [];

                    var scope7 = angular.element($("#txtMedicationName" + index)).scope();
                    scope7.obj.T_MEDICATION_NAME = row.entity.PRODUCT_DESC;
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
                    { field: 'PRODUCT_DESC', displayName: 'Medicine List'}
                ],
                filterOptions: { filterText: '<displayName>:<literal>', useExternalFilter: false },
                enableFiltering: true
                };
            var template = $http.get(url, { cache: $templateCache })
                .then(function (response) {
                    var modal = angular.element([
                        '<div class="modalDoc">',
                        '<div class="win"><span class ="headerText">Medicine List (Trade Name)</span>',
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