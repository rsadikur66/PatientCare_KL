app.factory('AmbulanceFactory', function ($document, $compile, $filter, $rootScope, $templateCache, $http, uiGridConstants) {
    var body = $document.find('body');
    return {
        getModal: function (url, data, index) {
            // A new scope for the modal using the passed data
            var scope = $rootScope.$new();
            angular.extend(scope, data);
            function getemployeedata() {
                try {
                    var url = '/T74014/GetEmployeeData';
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
            var dtemployee = getemployeedata();
            dtemployee.then(function (data) {
                var newDataJSON = JSON.parse(data);
                scope.data = newDataJSON;
            });
            scope.myAppScopeProvider = {
                showInfo: function (row) {
                    debugger;
                    var rodata = row.entity;
                    var scope1 = angular.element($("#txtEmployeeName" + index)).scope();
                    scope1.T_FIRST_LANG2_NAME = rodata.T_FIRST_LANG2_NAME + " " + rodata.T_FATHER_LANG2_NAME + " " + rodata.T_GFATHER_LANG2_NAME + " " + rodata.T_FAMILY_LANG2_NAME;
                    var scope2 = angular.element($("#txtEmployeeId" + index)).scope();
                    scope2.T_EMP_ID = rodata.T_EMP_ID;
                    scope.close();
                }
            }
            scope.gridOptions =
            {
                onRegisterApi: function (gridApi) {
                    scope.gridApi = gridApi;
                },
                appScopeProvider: scope.myAppScopeProvider,
                rowTemplate: "<div ng-dblclick=\"grid.appScope.showInfo(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>",
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
                    { field: 'T_FIRST_LANG2_NAME', displayName: 'Employee Name' },
                    { field: 'T_EMP_ID', displayName: 'Employee No' }
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
                        '<div class="win"><span class ="headerText">Employee List</span>',
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

