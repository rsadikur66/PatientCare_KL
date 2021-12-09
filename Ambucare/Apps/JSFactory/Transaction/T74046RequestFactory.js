
app.factory('RequestFactory', function ($document, $compile, $filter, $rootScope, $templateCache, $http, uiGridConstants, T74046Service) {
    var body = $document.find('body');
   
    return {
        getModal: function (url, data) {
           
            // A new scope for the modal using the passed data
            var scope = $rootScope.$new();
            angular.extend(scope, data);
            function getemployeedata() {
                try {
                    var url = '/T74046/GetRequestData';
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
                
                //var newDataJSON = JSON.parse(data);
                scope.data = data;
            });
            scope.myAppScopeProvider = {
                showInfo: function (row) {
                    var rodata = row.entity;
                    var scope1 = angular.element($("#txtRequest")).scope();
                    scope1.obj.T74046.T_PAT_ID = rodata.T_PAT_ID;
                    var scope2 = angular.element($("#txtPatient")).scope();
                    scope2.obj.T74046.T_REQUEST_ID = rodata.T_REQUEST_ID;
                    var scope1 = angular.element($("#txtPat_FirstNameEng")).scope();
                    scope1.obj.T74046.T_FIRST_LANG2_NAME = rodata.T_FIRST_LANG2_NAME;
                    var scope2 = angular.element($("#txtPat_FirstNameLocal")).scope();
                    scope2.obj.T74046.T_FIRST_LANG1_NAME = rodata.T_FIRST_LANG1_NAME;
                    var scope1 = angular.element($("#txtPat_FaNameEng")).scope();
                    scope1.obj.T74046.T_FATHER_LANG2_NAME = rodata.T_FATHER_LANG2_NAME;
                    var scope2 = angular.element($("#txtPat_FaNameLocal")).scope();
                    scope2.obj.T74046.T_FATHER_LANG1_NAME = rodata.T_FATHER_LANG1_NAME;

                    var scope1 = angular.element($("#txtPat_MoNameEng")).scope();
                    scope1.obj.T74046.T_MOTHER_LANG2_NAME = rodata.T_MOTHER_LANG2_NAME;
                    var scope2 = angular.element($("#txtPat_MoNameLocal")).scope();
                    scope2.obj.T74046.T_MOTHER_LANG1_NAME = rodata.T_MOTHER_LANG1_NAME;
                    var scope1 = angular.element($("#txtPat_GrFaNameEng")).scope();
                    scope1.obj.T74046.T_GFATHER_LANG2_NAME = rodata.T_GFATHER_LANG2_NAME;
                    var scope2 = angular.element($("#txtPat_GrFaNameLocal")).scope();
                    scope2.obj.T74046.T_GFATHER_LANG1_NAME = rodata.T_GFATHER_LANG1_NAME;

                    var scope8 = angular.element($("#txtPat_FamFaNameEng")).scope();
                    scope8.obj.T74046.T_FAMILY_LANG2_NAME = rodata.T_FAMILY_LANG2_NAME;
                    var scope9 = angular.element($("#txtPat_FamFaNameLocal")).scope();
                    scope9.obj.T74046.T_FAMILY_LANG1_NAME = rodata.T_FAMILY_LANG1_NAME;

                    var scope2 = angular.element($("#txtPat_MobileNo")).scope();
                    scope2.obj.T74046.T_MOBILE_NO = rodata.T_MOBILE_NO;
                    var scope2 = angular.element($("#txtAmbulance")).scope();
                    scope2.obj.T74046.T_AMBU_REG_ID = rodata.T_AMBU_REG_ID;

                    var scope3 = angular.element($("#ddlPat_Religion")).scope();
                    scope3.obj.R.selected = { T_LANG2_NAME: rodata.Re_T_LANG2_NAME, T_RLGN_CODE: rodata.T_RLGN_CODE };
                    var scope4 = angular.element($("#ddlPat_MRIL")).scope();
                    scope4.obj.M.selected = { T_LANG2_NAME: rodata.MRTL_T_LANG2_NAME, T_MRTL_STATUS_CODE: rodata.T_MRTL_STATUS };

                    var scope5 = angular.element($("#ddlPat_Gender")).scope();
                    scope5.obj.G.selected = { T_LANG2_NAME: rodata.Ge_T_LANG2_NAME, T_SEX_CODE: rodata.T_SEX_CODE };

                    var scope6 = angular.element($("#txtProblem")).scope();
                    scope6.obj.T_PROBLEM = rodata.T_PROBLEM;
                    var scope7 = angular.element($("#txtDuration")).scope();
                    scope7.obj.T_PROBLEM_DURATION = rodata.T_PROBLEM_DURATION;

                    sessionStorage.setItem("RequestId", JSON.stringify(scope2.obj.T74046.T_REQUEST_ID));

                    var dt = T74046Service.GetDocId(scope2.obj.T74046.T_REQUEST_ID);
                    dt.then(function (data) {
                        var param = {};
                        param.T_REQUEST_ID = scope2.obj.T74046.T_REQUEST_ID;
                        param.T_Doc_ID = JSON.parse(data);
                        sessionStorage.setItem("param", JSON.stringify(param));
                    });


                    

                    //var scope6 = angular.element($("#txtAmbulance")).scope();
                    //scope6.obj.T74046.T_AMBU_REG_ID = { T_LANG2_NAME: rodata.Re_T_LANG2_NAME, T_RLGN_CODE: rodata.T_RLGN_CODE };

                   // angular.element($("#txtPatient")).trigger('ChangeID_Click');
                    //$http({
                    //    method: 'POST',
                    //    url: '/T74046/GetPatient',
                    //    data: JSON.stringify({ T_PAT_ID: row.entity.T_PAT_ID })
                    //}).success(function (data) {
                    //    scope.pt.newDataJSON = data;
                    //    });

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
                        { field: 'T_PAT_ID', displayName: 'Patient ID', visible: false },
                        { field: 'T_REQUEST_ID', displayName: 'Request ID', visible: false},
                        { field: 'T_PAT_NO', displayName: 'Patient No', visible: false },
                        { field: 'T_AMBU_REG_ID', displayName: 'Amb ID' },
                        { field: 'T_FIRST_LANG2_NAME', displayName: 'Name English' },
                        { field: 'T_FIRST_LANG1_NAME', displayName: 'Name Local', visible: false },
                        { field: 'T_FIRST_LANG1_NAME', displayName: 'Name Local' },
                        { field: 'T_FATHER_LANG2_NAME', displayName: 'FaNme English', visible: false },

                        { field: 'T_MOTHER_LANG2_NAME', displayName: 'MName Eng', visible: false},
                        { field: 'T_MOTHER_LANG1_NAME', displayName: 'MName Local', visible: false },
                        { field: 'T_GFATHER_LANG2_NAME', displayName: 'GFName Eng', visible: false},
                        { field: 'T_GFATHER_LANG1_NAME', displayName: 'GFName Local', visible: false },

                        { field: 'T_FAMILY_LANG2_NAME', displayName: 'FamName Eng', visible: false },
                        { field: 'T_FAMILY_LANG1_NAME', displayName: 'FamName Local', visible: false },

                        { field: 'T_MOBILE_NO', displayName: 'Mobile No' },

                        { field: 'T_SEX_CODE', displayName: 'GenderCode', visible: false },
                        { field: 'T_MRTL_STATUS', displayName: 'MarritalCode', visible: false },
                        { field: 'T_RLGN_CODE', displayName: 'ReligionCode', visible: false },
                        { field: 'Re_T_LANG2_NAME', displayName: 'Religion', visible: false },
                        { field: 'MRTL_T_LANG2_NAME', displayName: 'Marrital', visible: false },
                        { field: 'Ge_T_LANG2_NAME', displayName: 'Gender', visible: false },

                        { field: 'T_PROBLEM', displayName: 'Problem', visible: false },
                        { field: 'T_PROBLEM_DURATION', displayName: 'PrDuration', visible: false }
                     
                         
                    
                    
                    
                    
                    


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