app.factory('EmployeeTypeFactory', ["$document", "$compile", "$filter", "$rootScope", "$templateCache", "$http", "uiGridConstants", "T74015Service", function ($document, $compile, $filter, $rootScope, $templateCache, $http, uiGridConstants, T74015Service) {
    var body = $document.find('body');
    return {
        getModalEmployeeType: function (url, data, index, T_AMBU_REG_ID) {
            // A new scope for the modal using the passed data
            var scope = $rootScope.$new();
            angular.extend(scope, data);

            //function getemployeedata() {
            //    try {
            //        var url = '/T74015/getGridEmployeeTypeData';
            //        var params = { T_AMBU_REG_ID: T_AMBU_REG_ID};
            //        return $http({
            //            url: url,
            //            method: "POST",
            //            data: params
            //        }).then(function (results) {
            //            return results.data;
            //        }).catch(function (ex) {
            //            throw ex;
            //        });
            //    } catch (ex) {
            //        throw ex;
            //    }
            //}

            var dtemployee = T74015Service.getGridEmployeeTypeData(T_AMBU_REG_ID);
            dtemployee.then(function (data) {
                var newDataJSON = JSON.parse(data);
                scope.data = newDataJSON;
            });
            scope.myAppScopeProvider = {
                showInfo: function (row) {
                    var rodata = row.entity;
                    var scope1 = angular.element($("#txtEmployeeName" + index)).scope();
                    scope1.obj.T_EMP_TYP_ID = rodata.T_EMP_TYP_ID;
                    var scope2 = angular.element($("#txtEmployeeId" + index)).scope();
                    scope2.obj.T_EMP_TYP_NAME = rodata.T_LANG2_NAME;
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
                    "<div ng-dblclick=\"grid.appScope.showInfo(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>",
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
                    { field: 'T_LANG2_NAME', displayName: 'Employee Type Name' },
                    { field: 'T_EMP_TYP_ID', displayName: 'Employee Type Id' }
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
                        '<div class="win"><span class ="headerText">Employee Type List</span>',
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
}]);