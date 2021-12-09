
app.factory('UomFactory', function ($document, $compile, $filter, $rootScope, $templateCache, $http, uiGridConstants) {
    var body = $document.find('body');

    return {
        getModal: function (url, data, umId, ind) {

            // A new scope for the modal using the passed data
            var scope = $rootScope.$new();
            angular.extend(scope, data);
            function getemployeedata() {
                try {
                    var url = '/T74116/GetUom';
                    var params = {};
                    return $http({
                        url: url,
                        method: "POST",
                        data: { T_ITEM_ID: umId }
                       // data: params
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

                //var newDataJSON = JSON.parse(data);
                scope.data = data;
            });
           // scope.obj.purProductList = [];
            scope.myAppScopeProvider = {
                showInfo: function (row) {
                  //  scope.obj.Um_Type = {};
                    var rodata = row.entity;

                    var scope1 = angular.element($("#txtUM" + ind)).scope();
                    scope1.obj.UM_T_LANG2_NAME = rodata.T_LANG2_NAME;
                    var scope2 = angular.element($("#txtUM_Id" + ind)).scope();
                    scope2.obj.T_ITEM_UM_ID = rodata.T_ITEM_UM_ID;
                  

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
                    { field: 'T_LANG2_NAME', displayName: 'UOM' },
                    { field: 'T_ITEM_UM_ID', displayName: 'UOM Id' }
                    //{ field: 'T_PAT_NO', displayName: 'Patient No', visible: false }
                    //{ field: 'T_AMBU_REG_ID', displayName: 'Amb ID' },
                    //{ field: 'T_FIRST_LANG2_NAME', displayName: 'Name English' },
                    //{ field: 'T_FIRST_LANG1_NAME', displayName: 'Name Local', visible: false },
                    //{ field: 'T_FIRST_LANG1_NAME', displayName: 'Name Local' },
                    //{ field: 'T_FATHER_LANG2_NAME', displayName: 'FaNme English', visible: false },

                    //{ field: 'T_MOTHER_LANG2_NAME', displayName: 'MName Eng', visible: false },
                    //{ field: 'T_MOTHER_LANG1_NAME', displayName: 'MName Local', visible: false },
                    //{ field: 'T_GFATHER_LANG2_NAME', displayName: 'GFName Eng', visible: false },
                    //{ field: 'T_GFATHER_LANG1_NAME', displayName: 'GFName Local', visible: false },
                    //{ field: 'T_MOBILE_NO', displayName: 'Mobile No' },

                    //{ field: 'T_SEX_CODE', displayName: 'GenderCode', visible: false },
                    //{ field: 'T_MRTL_STATUS', displayName: 'MarritalCode', visible: false },
                    //{ field: 'T_RLGN_CODE', displayName: 'ReligionCode', visible: false },
                    //{ field: 'Re_T_LANG2_NAME', displayName: 'Religion', visible: false },
                    //{ field: 'MRTL_T_LANG2_NAME', displayName: 'Marrital', visible: false },
                    //{ field: 'Ge_T_LANG2_NAME', displayName: 'Gender', visible: false },

                    //{ field: 'T_PROBLEM', displayName: 'Problem', visible: false },
                    //{ field: 'T_PROBLEM_DURATION', displayName: 'PrDuration', visible: false }









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