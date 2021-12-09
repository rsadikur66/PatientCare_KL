app.filter('propsFilter', function () {
    return function (items, props) {
        var out = [];
        if (angular.isArray(items)) {
            var keys = Object.keys(props);
            items.forEach(function (item) {
                var itemMatches = false;
                for (var i = 0; i < keys.length; i++) {
                    var prop = keys[i];
                    var text = props[prop].toLowerCase();
                    if (item[prop].toString().toLowerCase().indexOf(text) !== -1) {
                        itemMatches = true;
                        break;
                    }
                }
                if (itemMatches) {
                    out.push(item);
                }
            });
        } else {
            out = items;
        }
        return out;
    };
});
app.filter('cartypefilter', function () {
    return function (items, search) {
        if (!search) {
            return items;
        }
        var filtered = [];
        var letterMatch = new RegExp(search, 'i');
        for (var i = 0; i < items.length; i++) {
            var item = items[i];
            if (letterMatch.test(item.ROLE_NAME) || letterMatch.test(item.FORM_TYPE_NAME) || letterMatch.test(item.PAGE_TYPE_NAME) || letterMatch.test(item.FORM_CODE) || letterMatch.test(item.FORM_NAME2) || letterMatch.test(item.FORM_NAME1) || letterMatch.test(item.FORM_STATUS)) {
                filtered.push(item);
            }
        }
        return filtered;


    };
});
app.controller("T74066Controller", ["$scope", "$filter", "$http", "$window", "T74066Service", "LabelService", "uiGridConstants", "DTOptionsBuilder", "DTColumnBuilder", "Data","$rootScope",
    function ($scope, $filter, $http, $window, T74066Service, LabelService, uiGridConstants, DTOptionsBuilder, DTColumnBuilder, Data, $rootScope) {
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.t74066 = {};
        $scope.obj.R = {};
        $scope.obj.F = {};
        $scope.obj.P = {};
        $scope.obj.S = {};
        $scope.obj.S.selected = { CODE: '1', NAME: 'Active' };
        getGridData();
        $scope.obj.formStatusList=[
            {CODE:'1',NAME:"Active"},{CODE:'i',NAME:"Internal"},{CODE:'0',NAME:"Inactive"}];
        $scope.obj.ins_or_upd = 'Ins';

        
        var labelData = LabelService.getlabeldata('T74066');
        labelData.then(function (dt) { $scope.entity = dt; }); 

        var EntryUser = T74066Service.EntryUser();
        EntryUser.then(function (dt) { $scope.obj.t74066.T_ENTRY_USER = dt; $scope.obj.t74066.T_ENTRY_DATE = $filter('date')(new Date(), 'dd-MMM-yyyy');});

        var RoleList = T74066Service.GetRoleList();
        RoleList.then(function (dt) { $scope.obj.roleList = dt; });

        var FormType = T74066Service.GetFormTypeList();
        FormType.then(function (dt) { $scope.obj.formTypeList = dt; });


        var PageType = T74066Service.GetPageTypeList();
        PageType.then(function (dt) { $scope.obj.pageTypeList = dt; });

        $scope.onFormCodeSearch = function (e) {
            var FormList = T74066Service.GetFormList(e);
            FormList.then(function (dt) {
                if (dt.length !== 0) {
                    $scope.obj.formCodeList = dt;
                    $scope.showFormCodeListTable = true;
                } else { $scope.showFormCodeListTable = false; }
            });
        }
        $scope.onPageTypeSelect = function() {
            if ($scope.obj.ins_or_upd === 'Ins') {
                var MaxOrderNo = T74066Service.GetMaxOrderNo($scope.obj.t74066.T_ROLE_CODE,
                    $scope.obj.t74066.T_FORM_TYPE_ID,
                    $scope.obj.t74066.T_PAGE_TYPE_ID);
                MaxOrderNo.then(function(dt) { $scope.obj.t74066.T_ORDER = dt; });
            }
        };
        $scope.onFormCodeSelect = function(e) {
            $scope.obj.t74066.T_FORM_CODE = e.FORM_CODE;
            $scope.showFormCodeListTable = false;
        };

        $scope.Save_Click = function () {
            if (scope.obj.T_LANG1_NAME == undefined || scope.obj.T_LANG1_NAME == '') {
                // $window.confirm('Local Name is requered');
                alert($scope.getSingleMsg('M0002'));
                $scope.localName.show();
                return;
            }
            if (scope.obj.T_LANG2_NAME == undefined || scope.obj.T_LANG2_NAME == '') {
                // $window.confirm('English Name is requered');
                alert($scope.getSingleMsg('M0003'));
                $scope.localName.show();
                return;
            }
            Save_Click();
        };

        function Save_Click() {
            var msg = $scope.obj.ins_or_upd === 'Ins' ? "Data Saved Succesfully" : "Data Updated Succesfully";
            var save = T74066Service.Insert($scope.obj.t74066);
            save.then(function (data) {
                if (mesg == "Data Save Successfully") {
                    alert($scope.getSingleMsg('S0012'));
                } else if (mesg == "Data Update Successfully") {
                    alert($scope.getSingleMsg('S0003'));
                }
                //alert(msg);
                clear();
            });
        }

        $scope.Save = function() {
            Save_Click();
        };

        $rootScope.$on('T74066Emit', function (event, data) {
            if (data == 'rol') {
                Save_Click();
            }
        });
        $scope.Clear = function () {
            clear();
        };
        $scope.onMenuSelect = function (o) {
            $scope.obj.ins_or_upd = 'Upd';
            $scope.obj.R.selected = { CODE: o.ROLE_CODE, NAME: o.ROLE_NAME };
            $scope.obj.F.selected = { CODE: o.FORM_TYPE_CODE, NAME: o.FORM_TYPE_NAME };
            $scope.obj.P.selected = { CODE: o.PAGE_TYPE_CODE, NAME: o.PAGE_TYPE_NAME };
            $scope.obj.S.selected = { CODE: o.T_INACTIVE_FLAG, NAME: o.FORM_STATUS };
            $scope.obj.t74066.T_FORM_CODE_ID = o.T_FORM_CODE_ID;
            $scope.obj.t74066.T_FORM_CODE = o.FORM_CODE;
            $scope.obj.t74066.T_ORDER = o.T_ORDER;
            $scope.obj.t74066.T_LANG2_NAME = o.FORM_NAME2;
            $scope.obj.t74066.T_LANG1_NAME = o.FORM_NAME1;
            document.getElementById("txtOrder").readOnly = false;
        }
        //---------------------------------------------------------------------------
        function clear() {
            $scope.obj.ins_or_upd = 'Ins';
            $scope.obj.R.selected = { CODE: null, NAME: 'Select' };
            $scope.obj.F.selected = { CODE: null, NAME: 'Select' };
            $scope.obj.P.selected = { CODE: null, NAME: 'Select' };
            $scope.obj.S.selected = { CODE: '1', NAME: 'Active' };
            $scope.obj.t74066.T_FORM_CODE_ID = undefined;
            $scope.obj.t74066.T_FORM_CODE = "";
            $scope.obj.t74066.T_ORDER = "";
            $scope.obj.t74066.T_LANG2_NAME = "";
            $scope.obj.t74066.T_LANG1_NAME = "";
            document.getElementById("txtOrder").readOnly = true;
            $scope.obj.srchMenuList = '';
            getGridData();
        }

        function getGridData() {
            var GridData = T74066Service.GetGridData();
            GridData.then(function (dt) {
                $scope.obj.newObj = [];
                for (var i = 0; i < dt.length; i++) {
                    $scope.obj.newObject = {};
                    $scope.obj.newObject.T_FORM_CODE_ID = dt[i].T_FORM_CODE_ID;
                    $scope.obj.newObject.FORM_CODE = dt[i].FORM_CODE;
                    $scope.obj.newObject.FORM_NAME1 = dt[i].FORM_NAME1;
                    $scope.obj.newObject.FORM_NAME2 = dt[i].FORM_NAME2;
                    $scope.obj.newObject.FORM_TYPE_CODE = dt[i].FORM_TYPE_CODE;
                    $scope.obj.newObject.FORM_TYPE_NAME = dt[i].FORM_TYPE_NAME;
                    $scope.obj.newObject.PAGE_TYPE_CODE = dt[i].PAGE_TYPE_CODE;
                    $scope.obj.newObject.PAGE_TYPE_NAME = dt[i].PAGE_TYPE_NAME;
                    $scope.obj.newObject.ROLE_CODE = dt[i].ROLE_CODE;
                    $scope.obj.newObject.ROLE_NAME = dt[i].ROLE_NAME;
                    $scope.obj.newObject.T_ORDER = dt[i].T_ORDER;
                    $scope.obj.newObject.T_INACTIVE_FLAG = dt[i].T_INACTIVE_FLAG;  
                    if (dt[i].T_INACTIVE_FLAG === '1') { $scope.obj.newObject.FORM_STATUS = 'Active'; }
                    else if (dt[i].T_INACTIVE_FLAG === 'i') { $scope.obj.newObject.FORM_STATUS = 'Internal'; }
                    else if (dt[i].T_INACTIVE_FLAG === '0') { $scope.obj.newObject.FORM_STATUS = 'Inactive'; }
                    $scope.obj.newObj.push($scope.obj.newObject);
                }
                $scope.obj.menuList = $scope.obj.newObj;
            });
        }
        
    }]);


