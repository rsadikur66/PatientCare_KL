app.filter('ageFilter', function () {
    function calculateAge(birthday) {
        var ageDifMs = Date.now() - birthday.getTime();
        var ageDate = new Date(ageDifMs);
        return Math.abs(ageDate.getUTCFullYear() - 1970);
    }

    function monthDiff(d1, d2) {
        if (d1 < d2) {
            var months = d2.getMonth() - d1.getMonth();
            return months <= 0 ? 0 : months;
        }
        return 0;
    }


    return function (birthdate) {
        var age;
        if (monthDiff(birthdate, new Date()) === 0) {
            age = calculateAge(birthdate) + ' Yrs';
        } else {
            age = calculateAge(birthdate) + ' Yrs ' + monthDiff(birthdate, new Date()) + ' Mos';
        }
        return age;
    };
});
app.factory('T74130PatFactory', function ($document, $compile, $filter, $rootScope, $templateCache, $http, uiGridConstants) {
    var body = $document.find('body');
    return {
        getModal: function (url, data) {
            var scope = $rootScope.$new();
            angular.extend(scope, data);
            function GetPatInfo() {
                try {
                    var url = '/T74041/GetPatInfo';
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
            var dt = GetPatInfo();
            dt.then(function (data) {
                scope.data = data;
            });
            scope.myAppScopeProvider = {
                showInfo: function (row) {
                    var rodata = row.entity;
                    var scope1 = angular.element($("#txtPatientCode")).scope();
                    scope1.obj.T_PAT_ID = rodata.T_PAT_ID;
                    var scope2 = angular.element($("#txtName")).scope();
                    var T_FIRST_LANG2_NAME = rodata.T_FIRST_LANG2_NAME;
                    var T_FATHER_LANG2_NAME= rodata.T_FATHER_LANG2_NAME;
                    var T_GFATHER_LANG2_NAME = rodata.T_GFATHER_LANG2_NAME;
                    var T_FAMILY_LANG2_NAME = rodata.T_FAMILY_LANG2_NAME;
                    scope2.obj.FullName = T_FIRST_LANG2_NAME + " " + T_FATHER_LANG2_NAME + " " + T_GFATHER_LANG2_NAME + " " + T_FAMILY_LANG2_NAME;
                    var scope8 = angular.element($("#txtMobile")).scope();
                    scope8.obj.T_MOBILE_NO = rodata.T_MOBILE_NO;
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
                    { field: 'T_PAT_ID', displayName: 'Patient ID', visible: false },
                    { field: 'T_FIRST_LANG2_NAME', displayName: 'Name' },
                    { field: 'Gender', displayName: 'Gender' },
                    { field: 'T_MOBILE_NO', displayName: 'Contact No' },
                    { field: 'T_ALT_MOBILE_NO', displayName: 'Alt Contact No' }
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
                        '<div class="win"><span class ="headerText">Request List</span>',
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