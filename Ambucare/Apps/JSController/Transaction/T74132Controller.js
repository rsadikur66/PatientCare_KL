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
            // Let the output be the input untouched
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
            if (letterMatch.test(item.CODE) || letterMatch.test(item.NAME)) {
                filtered.push(item);
            }
        }
        return filtered;


    };
});
app.controller("T74132Controller", ["$scope", "$window", "T74132Service", "Data","$rootScope",
    function($scope, $window, T74132Service, Data, $rootScope) {
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.t74132 = {};
        $scope.obj.R = {};
        $scope.obj.U = {};
        $scope.obj.F = {};
        $scope.obj.S = {};
        $scope.obj.S.selected = {};
        $scope.obj.R.selected = { CODE: null, NAME: 'Select' };
        $scope.obj.U.selected = { CODE: null, NAME: 'Select' };
        $scope.obj.F.selected = { CODE: null, NAME: 'Select' };
        $scope.obj.S.selected = { CODE: null, NAME: 'Select' };
        $scope.obj.t74132.T_TYPE = 'w';
        clear();
        roleList(null);
        userTypeList($scope.obj.t74132.T_TYPE);
        //userTypeList(61);
       // $scope.obj.selectedValue = 'Disable';

        $scope.onRadiobuttonChange = function (val) {
            //var p = val === 'm' ? 0 : 61;
            $scope.obj.t74132.T_TYPE = val === 'm' ? 'm' : 'w';
            userTypeList($scope.obj.t74132.T_TYPE);
            //userTypeList(p);
            roleList(null);
            clear();
        }
        $scope.onRoleSelect = function (userType, fType) {
           
            document.getElementById('chkUpdateAllForm').checked = false;
           
            var formType = fType === null ? 1 : fType;
            $scope.fmCode = formType; //Ruhul
            $scope.usrType = userType;//Ruhul
            var code = $scope.obj.R.selected.CODE;
            var empType = $scope.obj.R.selected.TYP_ID;
           $scope.obj.T_INPUT_STATUS = 'Ins';
            formTypeList($scope.obj.t74132.T_ROLE_CODE, null);
           // getUserList(userType, empType, $scope.obj.T_INPUT_STATUS, formType); // block Ruhul
         
        }

        $scope.onFormTypeSelect = function (f, r) {
            if (r != null) {
             // formList(f, r, null);
                formTypeList(r, null);
            }
        }

        $scope.onUserTypeSelect = function (userType, empType, formType) {
            $scope.obj.T_INPUT_STATUS = 'Ins';
            document.getElementById("chkUpdateAllUser").checked = false;
            document.getElementById("chkUpdateOrInsert").checked = false;
            if (formType != null) {
                getUserList(userType, empType, $scope.obj.T_INPUT_STATUS, formType);
            }
            //else {
            //    alert('Please Select Form Type');
            //    $scope.obj.U.selected = { CODE: null, NAME: 'Select' };
            //    document.getElementById("divUserList").style.display = "none";
            //}
        }

      $scope.onSiteSelect = function (siteCode) {
        $scope.SiteCode = siteCode;
          var empType = $scope.obj.R.selected.TYP_ID; //Ruhul
            getUserList($scope.usrType, empType, $scope.obj.T_INPUT_STATUS, $scope.fmCode); //Ruhul
      };
      
      function siteLoad() {
        var st=  $scope.obj.S.selected.CODE;
        if (st == undefined) {
            var site = T74132Service.getSiteData();
            site.then(function(data) {
                $scope.SiteList = data;
                $scope.obj.S.selected = { CODE: data[0].CODE, NAME: data[0].NAME };
                $scope.SiteCode = data[0].CODE;
                var empType = $scope.obj.R.selected.TYP_ID; //Ruhul
                getUserList($scope.usrType, empType, $scope.obj.T_INPUT_STATUS, $scope.fmCode); //Ruhul
            });
        } else {
            var empType = $scope.obj.R.selected.TYP_ID; //Ruhul
            getUserList($scope.usrType, empType, $scope.obj.T_INPUT_STATUS, $scope.fmCode); //Ruhul  
        }

      }
        //----------------------------------------User List-----------------------------------
        $scope.onUpdateOrInsertUser = function (userType, empType1, formType) {
            $scope.obj.T_INPUT_STATUS = document.getElementById('chkUpdateOrInsert').checked === true ? 'Upd' : 'Ins';
            document.getElementById('chkUpdateAllUser').checked = false;
            var empType = $scope.obj.R.selected.TYP_ID;
            getUserList(userType, empType, $scope.obj.T_INPUT_STATUS, formType);
        }
      $scope.onUserSelect = function (obj, i) {
          // $scope.loader(true);
        if ($scope.obj.T_INPUT_STATUS === 'Upd') {
          $scope.loader(true);
          
           // $scope.obj.userList[i].T_CHCK = 'T';
           // $scope.obj.userList[i].T_CHK_SINGLE_USER = '1';
                $scope.userList = [];
                for (var j = 0; j < $scope.obj.userList.length; j++) {
                    $scope.obj.newObject = {};
                   // $scope.obj.newObject.T_CHK_SINGLE_USER = $scope.obj.userList[j].SLNO === obj.SLNO ? '1' : '2';
                    $scope.obj.newObject.SLNO = $scope.obj.userList[j].SLNO;
                    $scope.obj.newObject.CODE = $scope.obj.userList[j].CODE;
                  $scope.obj.newObject.NAME = $scope.obj.userList[j].NAME;
                  if (obj.CODE == $scope.obj.userList[j].CODE) {
                      $scope.obj.newObject.T_CHCK = 'T';
                      $scope.obj.newObject.T_CHK_SINGLE_USER = '1';
                  } else {
                    $scope.obj.newObject.T_CHCK = 'F';
                      $scope.obj.newObject.T_CHK_SINGLE_USER = '';
                  }
                    $scope.userList.push($scope.obj.newObject);
                }
                $scope.obj.userList = $scope.userList;
               // roleList(obj.CODE);
              loadUserData(obj.CODE);
            }
            else if ($scope.obj.T_INPUT_STATUS === 'Ins') {
              if ($scope.obj.userList[i].T_INACTIVE_FLAG === '1') {
                 // $scope.loader(false);
                  return;
              } else {
                var d = $scope.obj.userList[i].T_CHK_SINGLE_USER;
                if (d == '1') {
                 
                  $scope.obj.userList[i].T_CHK_SINGLE_USER = $scope.obj.userList[i].T_CHK_SINGLE_USER === '1' ? '2' : '1';
                  $scope.obj.userList[i].T_CHCK = 'F';
                } else {
                  $scope.obj.userList[i].T_CHK_SINGLE_USER = $scope.obj.userList[i].T_CHK_SINGLE_USER === '1' ? '2' : '1';
                  $scope.obj.userList[i].T_CHCK = 'T';
                }
               
                  // $scope.loader(false);
              }
               
            }

      }

      function loadUserData(userCode) {
          $scope.fmCode = $scope.obj.t74132.T_FORM_CODE;//Ruhul
          var user = T74132Service.getFdataByUser($scope.fmCode, $scope.obj.t74132.T_ROLE_CODE,userCode);
          user.then(function (ur) {
            $scope.obj.formList = ur;
              $scope.loader(false);
          });
        }
        $scope.onFormSelect = function (i) {
          var scope1 = angular.element($("#chkUpdateForm" + i)).scope();
          if (scope1.obj.T_INACTIVE_FLAG==='1') {
              scope1.obj.UPDATED = 'T'; //scope1.obj.UPDATED === 'B' ? 'A' : 'B';
          } else {
              scope1.obj.UPDATED = 'F';
          }
            
        }
        //----------------------------------------Common-----------------------------------
        $scope.onAllCheckBoxClick = function (type) {
            if (type === 'F') {
                var a = $scope.obj.formList.length;
                $scope.formList = [];
                for (var i = 0; i < a; i++) {
                    $scope.obj.newObject = {};
                    $scope.obj.newObject.UPDATED =
                        document.getElementById('chkUpdateAllForm').checked === true ? 'A' : 'B';
                    $scope.obj.newObject.CODE = $scope.obj.formList[i].CODE;
                    $scope.obj.newObject.NAME = $scope.obj.formList[i].NAME;
                    $scope.formList.push($scope.obj.newObject);

                }
                $scope.obj.formList = $scope.formList;
            }
            else if (type === 'U' && $scope.obj.T_INPUT_STATUS === 'Ins') {
                var b = $scope.obj.userList.length;
                $scope.userList = [];
                for (var j = 0; j < b; j++) {
                    $scope.obj.newObject = {};
                    $scope.obj.newObject.T_CHK_SINGLE_USER =
                        document.getElementById('chkUpdateAllUser').checked === true ? '1' : '2';
                    $scope.obj.newObject.CODE = $scope.obj.userList[j].CODE;
                    $scope.obj.newObject.NAME = $scope.obj.userList[j].NAME;
                    $scope.userList.push($scope.obj.newObject);

                }
                $scope.obj.userList = $scope.userList;
            }


        }
        function save() {

          $scope.obj.t74066 = [];
        
          $scope.filterUsrerList = $scope.obj.userList.filter((x) => x.T_CHCK == 'T');
          for (var i = 0; i < $scope.filterUsrerList.length; i++) {
            if ($scope.filterUsrerList[i].T_CHK_SINGLE_USER === '1') {
                $scope.filterformList = $scope.obj.formList.filter((x) => x.UPDATED !== '');
                 for (var j = 0; j < $scope.filterformList.length; j++) {
                        $scope.obj.new = {};
                   $scope.obj.new.T_FORM_CODE = $scope.filterformList[j].CODE;
                   $scope.obj.new.T_LANG1_NAME = $scope.filterformList[j].T_LANG1_NAME;
                   $scope.obj.new.T_LANG2_NAME = $scope.filterformList[j].NAME;
                   $scope.obj.new.T_INACTIVE_FLAG = $scope.filterformList[j].UPDATED === 'T' ? 'T' : 'F';
                   $scope.obj.new.T_PAGE_TYPE_ID = $scope.filterformList[j].T_PAGE_TYPE_ID;
                   $scope.obj.new.T_ORDER = $scope.filterformList[j].T_ORDER;
                   $scope.obj.new.T_USER_ID = $scope.filterUsrerList[i].CODE;
                        $scope.obj.new.T_TYPE = $scope.obj.t74132.T_TYPE;
                        $scope.obj.new.T_FORM_TYPE_ID = $scope.obj.t74132.T_FORM_CODE;
                        $scope.obj.new.T_ROLE_CODE = $scope.obj.t74132.T_ROLE_CODE;
                        $scope.obj.t74066.push($scope.obj.new);
                    }
                }
            }
            if ($scope.obj.t74066.length > 0) {

                var Save = T74132Service.Insert($scope.obj.t74066, $scope.obj.T_INPUT_STATUS);
                Save.then(function(dt) {
                  alert($scope.getSingleMsg('S0012'));
                 // clear();
                  $scope.obj.srchUserList = '';
                  $scope.obj.srchFormList = '';
                });
            }

        }
        $scope.Save = function () {

            save();

        }
        $rootScope.$on('T74132Emit', function (event, data) {
            if (data == 'for') {
                save();
            }
        });
        $scope.Clear = function () { clear(); }
        //------------------------------------Functions-----------------------------
        function roleList(e) {
            var RoleList = T74132Service.GetRole(e);
            RoleList.then(function (data) {
                $scope.roleList = data;
                if (e == null) {
                    $scope.obj.R.selected = { CODE: null, NAME: 'Select' };
                }
                else if (e != null) {
                    $scope.obj.R.selected = { CODE: $scope.roleList[0].CODE, NAME: $scope.roleList[0].NAME };
                    formTypeList($scope.roleList[0].CODE, e);
                }
            });
        }
        function userTypeList(e) {
            var UserType = T74132Service.GetUserType(e);
            UserType.then(function (data) {
                $scope.userType = data;
            });
        }
        function getUserList(userType, empType, inputStatus, frmType) {
          var UserList = T74132Service.GetUserList(userType, empType, inputStatus, frmType, $scope.SiteCode);
            UserList.then(function (dtU) {
                if (dtU.length > 0) {
                  $scope.obj.userList = dtU;
                  formList($scope.fmCode, $scope.obj.t74132.T_ROLE_CODE, null); //Ruhul
                    document.getElementById("divUserList").style.display = "inline-block";
                    document.getElementById("divFormList").style.display = "inline-block";
                }
                else {
                    //alert('No data found');
                    alert($scope.getSingleMsg('R0001'));
                    $scope.obj.T_INPUT_STATUS = 'Ins';
                  document.getElementById('chkUpdateOrInsert').checked = false;
                  $scope.obj.userList = [];//Ruhul
                  $scope.obj.formList = [];//Ruhul
                    // $scope.obj.U.selected = { CODE: null, NAME: 'Select' };
                    //document.getElementById("divUserList").style.display = "none";
                    //document.getElementById("divFormList").style.display = "none";
                }

            });
        }
        function formTypeList(r, u) {
            var FormType = T74132Service.GetFormType(r, u);
            FormType.then(function (data) {
              $scope.formType = data;
              $scope.fmCode = $scope.obj.t74132.T_FORM_CODE;//Ruhul
              if ($scope.fmCode == null) {
                $scope.fmCode = $scope.formType[0].CODE;//Ruhul
                $scope.obj.F.selected = { CODE: $scope.formType[0].CODE, NAME: $scope.formType[0].NAME };
                }
              siteLoad();
               // formList($scope.formType[0].CODE, r, u); block Ruhul

              //var empType = $scope.obj.R.selected.TYP_ID; //Ruhul
             // getUserList($scope.usrType, empType, $scope.obj.T_INPUT_STATUS, $scope.fmCode); //Ruhul
            });
        }
        function formList(f, r, u) {
            var redio = document.getElementsByName('rdbPatType');
            var user = redio[1].checked == true ? redio[1].value : redio[0].value;
            var FormList = T74132Service.GetFormList(f, r, u, user);
            FormList.then(function (dtF) {
                $scope.obj.formList = dtF;
            });
        }
        
        function clear() {
            $scope.obj.R.selected = { CODE: null, NAME: 'Select' };
            $scope.obj.F.selected = { CODE: null, NAME: 'Select' };
            $scope.obj.U.selected = { CODE: null, NAME: 'Select' };
            $scope.obj.S.selected = { CODE: null, NAME: 'Select' };
            document.getElementById("divFormList").style.display = "none";
            document.getElementById("divUserList").style.display = "none";
            $scope.obj.T_INPUT_STATUS = 'Ins';
        }


    }]);