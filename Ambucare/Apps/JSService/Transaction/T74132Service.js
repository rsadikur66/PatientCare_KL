app.service("T74132Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
    var dataSvc = {
        GetRole: GetRole,
        GetUserType: GetUserType,
        GetUserList: GetUserList,
        GetFormType: GetFormType,
        GetFormList: GetFormList,
        Insert: Insert,
      getFdataByUser: getFdataByUser,
      getSiteData: getSiteData
};
    return dataSvc;
    function GetRole(e) {
        try {
            var url = vrtlDirr + '/T74132/GetRole';
            var params = { P_USER_ID:e};
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
    function GetUserType(e) {
        try {
            var url = vrtlDirr + '/T74132/GetUserType';
            var params = {P_TYPE:e};
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
    function GetUserList(userType, empType, inpStatus, frmType,siteCode) {
        try {
            var url = vrtlDirr + '/T74132/GetUserList';
          var params = {
            P_USER_TYPE: userType, P_EMP_TYP_ID: empType,
            P_USER_STATUS: inpStatus, P_FORM_TYPE_ID: frmType,
            siteCode: siteCode
          };
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
    function GetFormType(r,u) {
        try {
            var url = vrtlDirr + '/T74132/GetFormType';
            var params = {P_ROLE_CODE:r, P_USER_ID:u};
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
    function GetFormList(e, r, u, user) {
        try {
            var url = vrtlDirr + '/T74132/GetFormList';
            var params = { P_FORM_TYPE_ID: e, P_ROLE_CODE: r, P_USER_ID: u, user:user};
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
    function Insert(e, stts) {
        try {
            var url = vrtlDirr + '/T74132/Insert';
            var params = { t74066: e, status: stts};
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
  function getFdataByUser(fmCode,rCode,uData) {
      try {
        var url = vrtlDirr + '/T74132/GetFdataByUser';
        var params = { fmCode: fmCode, rCode: rCode, uData: uData };
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
  function getSiteData() {
        try {
          var url = vrtlDirr + '/T74132/GetSiteData';
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
}]);