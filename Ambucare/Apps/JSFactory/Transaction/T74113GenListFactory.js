app.factory('T74113GenListFactory', function ($document, $compile, $filter, $rootScope, $templateCache, $http, uiGridConstants, T74113Service) {
    var body = $document.find('body');

    return {
        getModal: function(url, data, index) {
            var $scope = $rootScope.$new();
            angular.extend($scope, data);

            var dt = T74113Service.GetGen();
            dt.then(function(data) {
                $scope.data = JSON.parse(data);
            });
            $scope.myAppScopeProvider = {
                showInfo: function(row) {

                    var scope1 = angular.element($("#txtMedicationName" + index)).scope();
                    scope1.obj.T_MEDICATION_NAME = row.entity.GEN_DESC;
                    var scope2 = angular.element($("#txtGenCode" + index)).scope();
                    scope2.obj.T_GEN_CODE = row.entity.GEN_CODE;
                    var strength = T74113Service.GetStrengthByGen(row.entity.GEN_CODE);
                    strength.then(function(dt) {
                        var scope = angular.element($("#ddlStrengthList" + index)).scope();
                        scope.obj.StrengthList = JSON.parse(dt);

                        var scope2 = angular.element($("#ddlStrength" + index)).scope();
                        scope2.obj.S = {};
                        scope2.obj.S.selected = { STRENGTH: 'Select' };
                    });
                    var form = T74113Service.GetFormByGen(row.entity.GEN_CODE);
                    form.then(function(dt) {
                        var scope = angular.element($("#ddlFormList" + index)).scope();
                        scope.obj.FormList = JSON.parse(dt);

                        var scope2 = angular.element($("#ddlForm" + index)).scope();
                        scope2.obj.Fr = {};
                        scope2.obj.Fr.selected = { FORM_CODE: '', FORM_DESC: 'Select'  };
                    });
                    var route = T74113Service.GetRouteByGen(row.entity.GEN_CODE);
                    route.then(function(dt) {
                        var scope = angular.element($("#ddlRouteList" + index)).scope();
                        scope.obj.RouteList = JSON.parse(dt);

                        var scope2 = angular.element($("#ddlRoute" + index)).scope();
                        scope2.obj.R = {};
                        scope2.obj.R.selected = { ROUTE_CODE: '', ROUTE_DESC: 'Select' };
                    });


                    $scope.close();
                }

            }
            $scope.gridOptions =
            {
                onRegisterApi: function(gridApi) {
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
                    { field: 'GEN_DESC', displayName: 'Generic Name' }
                ],
                filterOptions: { filterText: '<displayName>:<literal>', useExternalFilter: false },
                enableFiltering: true
            };
            var template = $http.get(url, { cache: $templateCache })
                .then(function(response) {
                    var modal = angular.element([
                        '<div class="modalDoc">',
                        '<div class="win"><span class ="headerText">Medicine List (Generic Name)</span>',
                        '<a href="#" class="close" ng-click="close()"></a>',
                        '<div class="gridStyle small-font" ui-grid-selection ui-grid="gridOptions" ui-grid-pagination style="width:auto;"></div>',
                        '</div>',
                        '</div>'
                    ].join(''));
                    $compile(modal)($scope);
                    body.append(modal);
                    $scope.close = function() {
                        modal.remove();
                        $scope.$destroy();
                    };

                });
        }
};
});