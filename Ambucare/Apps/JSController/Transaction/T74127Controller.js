
app.controller("T74127Controller", ["$scope", "$location", "$compile", "$filter", "$http", "$window", "T74127Service", "Data",
    function (scope, $location, $compile, $filter, $http, $window, T74127Service, Data) {
        //for instance
        scope.obj = {};
        scope.obj = Data;
        scope.obj.T74094 = {};
     

        scope.VeiwProduct_Click = function() {
            scope.UploadProduct = false;
            scope.EditProduct = false;
            scope.VeiwProduct = true;
        }
        scope.UploadProduct_Click = function() {
            scope.EditProduct = false;
            scope.VeiwProduct = false;
            scope.UploadProduct = true;
        }
        scope.EditProduct_Click = function () {
            scope.UploadProduct = false;
            scope.VeiwProduct = false;
            scope.EditProduct = true;
        }
        scope.InputItem_Click = function() {
            scope.InputItem = true;
        }

        scope.Problem_Click = function (em, en, reg, ind) {

            var previousProblem = T74127Service.getPreviousProblem(em, en, reg);
            previousProblem.then(function(data) {
                scope.obj.PreviousProblemlist = data;

              var  value = sessionStorage.getItem("ind");
                if (value != null) {
                    document.getElementById('Problem' + value).style.display = 'none';
                }
                document.getElementById('Problem' + ind).style.display = 'block';
                sessionStorage.setItem("ind", ind);
            });
           
            //var value = sessionStorage.getItem("ind");
        }
        scope.InputProblem = function (em, en, reg,ind) {
            scope.newArre = [];
           
            scope.obj.T74094.T_EMP_ID = em;
            scope.obj.T74094.T_ENGIN_NO = en;
            scope.obj.T74094.T_REGI_NO = reg;
            scope.InputItem = true;

           
            scope.newObject = {}
            scope.newObject.T_EMP_ID = em;
            scope.newObject.T_ENGIN_NO = en;
            scope.newObject.T_REGI_NO = reg;
            scope.newObject.T_ENTRY_DATE = $filter('date')(new Date(), 'dd-MMM-yyyy');
            scope.newArre.push(scope.newObject);
            scope.obj.ProblemList = scope.newArre;

            scope.NewObjectItemList = [];
            for (var i = 0; i <scope.obj.Itemlist.length; i++) {
                scope.Objectlist = {};
                if (scope.obj.Itemlist[i].T_ENGIN_NO == en && scope.obj.Itemlist[i].T_REGI_NO == reg ) {
                    scope.Objectlist.chkRowItem = '1';
                    scope.Objectlist.T_EMP_ID = scope.obj.Itemlist[i].T_EMP_ID;
                    scope.Objectlist.T_DESC = scope.obj.Itemlist[i].T_DESC;
                    scope.Objectlist.T_CHESES_NO = scope.obj.Itemlist[i].T_CHESES_NO;
                    scope.Objectlist.T_MODEL_NO = scope.obj.Itemlist[i].T_MODEL_NO;
                    scope.Objectlist.T_BARCODE = scope.obj.Itemlist[i].T_BARCODE;
                    scope.Objectlist.T_ENGIN_NO = scope.obj.Itemlist[i].T_ENGIN_NO;
                    scope.Objectlist.T_REGI_NO = scope.obj.Itemlist[i].T_REGI_NO;
                    scope.NewObjectItemList.push(scope.Objectlist);
                } else {
                    scope.Objectlist.chkRowItem = '2';
                    scope.Objectlist.T_EMP_ID = scope.obj.Itemlist[i].T_EMP_ID;
                    scope.Objectlist.T_DESC = scope.obj.Itemlist[i].T_DESC;
                    scope.Objectlist.T_CHESES_NO = scope.obj.Itemlist[i].T_CHESES_NO;
                    scope.Objectlist.T_MODEL_NO = scope.obj.Itemlist[i].T_MODEL_NO;
                    scope.Objectlist.T_BARCODE = scope.obj.Itemlist[i].T_BARCODE;
                    scope.Objectlist.T_ENGIN_NO = scope.obj.Itemlist[i].T_ENGIN_NO;
                    scope.Objectlist.T_REGI_NO = scope.obj.Itemlist[i].T_REGI_NO;
                    scope.NewObjectItemList.push(scope.Objectlist);
                }
            }
            scope.obj.Itemlist = scope.NewObjectItemList; 
        }
        scope.ClosePopup = function (ind) {
            document.getElementById('InputProblem' + ind).style.display = 'none';
        }
        scope.Save_Click = function () {
           
            scope.T74095 = [];

            for (var i = 0; i < scope.obj.ProblemList.length; i++) {
                
                scope.saveItemObject = {};
                //scope.saveItemObject.T_EMP_ID = scope.obj.ProblemList[i].T_EMP_ID;
                //scope.saveItemObject.T_REGI_NO = scope.obj.ProblemList[i].T_REGI_NO;
                //scope.saveItemObject.T_ENGIN_NO = scope.obj.ProblemList[i].T_ENGIN_NO;
                scope.saveItemObject.T_PROB_DELT_LANG2 = scope.obj.ProblemList[i].T_PROB_DELT_LANG2;
                scope.saveItemObject.T_APR_DELV_DATE = scope.obj.ProblemList[i].T_APR_DELV_DATE;

                scope.T74095.push(scope.saveItemObject);

            }

            var save = T74127Service.saveData(scope.obj.T74094, scope.T74095);
            save.then(function (data) {
                //alert('Save Successfully');
                alert(scope.getSingleMsg('S0012'));
            }); 

        }
        scope.AddRow_Click = function (enNo, ind) {
            scope.obj.ProblemList.push({
                T_EMP_ID: scope.obj.ProblemList.T_EMP_ID,
                T_ENGIN_NO: scope.obj.ProblemList.T_ENGIN_NO,
                T_REGI_NO: scope.obj.ProblemList.T_REGI_NO,
                T_ENTRY_DATE : $filter('date')(new Date(), 'dd-MMM-yyyy')
            });
        }

        var emp = T74127Service.getEmployee();
        emp.then(function (data) {
            scope.EmployeeList = data;
        });
       
        scope.Employee_Click = function(empId) {
            var empDetails = T74127Service.getEmployeeDetails(empId);
            empDetails.then(function(data) {
                scope.obj.T_FIRST_LANG2_NAME = data[0].T_FIRST_LANG2_NAME;
                scope.obj.T_FATHER_LANG2_NAME = data[0].T_FATHER_LANG2_NAME;
                scope.obj.T_MOTHER_LANG2_NAME = data[0].T_MOTHER_LANG2_NAME;
                scope.obj.T_MOBILE_NO = data[0].T_MOBILE_NO;
                scope.obj.T_PHONE_WORK = data[0].T_PHONE_WORK;
                scope.obj.T_ADDRESS1 = data[0].T_ADDRESS1;
                scope.obj.T_ADDRESS2 = data[0].T_ADDRESS2;

                scope.employeeDetails = true;

                var itemsList = T74127Service.getItem(empId);
                itemsList.then(function (itmdata) {
                  //  scope.objectItemList = {};
                    scope.objectItemList = [];
                    for (var i = 0; i < itmdata.length; i++) {
                        scope.object = {};
                        scope.object.T_EMP_ID = itmdata[i].T_EMP_ID;
                        scope.object.T_DESC = itmdata[i].T_DESC;
                        scope.object.T_CHESES_NO = itmdata[i].T_CHESES_NO;
                        scope.object.T_MODEL_NO = itmdata[i].T_MODEL_NO;
                        scope.object.T_BARCODE = itmdata[i].T_BARCODE;
                        scope.object.T_ENGIN_NO = itmdata[i].T_ENGIN_NO;
                        scope.object.T_REGI_NO = itmdata[i].T_REGI_NO;
                        scope.object.ProblemList = [];
                       
                            for (var j = 0; j < itmdata.length; j++) {
                                if (scope.object.ProblemList.length === 0) {
                                scope.Newproblem = {};
                                scope.Newproblem.T_EMP_ID = itmdata[i].T_EMP_ID;
                                scope.Newproblem.T_ENGIN_NO = itmdata[i].T_ENGIN_NO;
                                scope.Newproblem.T_ENTRY_DATE = $filter('date')(new Date(), 'dd-MMM-yyyy');
                                scope.object.ProblemList.push(scope.Newproblem);
                            }
                        }                       
                        scope.objectItemList.push(scope.object);                      
                    }                    
                    scope.obj.Itemlist = scope.objectItemList;
                });
            });

        }
        scope.NewItemList = [];
        scope.SaveItem_Click = function (item) {
           // scope.NewObject = {};
            if (scope.obj.Itemlist.length !== 0) {
                for (var i = 0; i < scope.obj.Itemlist.length; i++) {
                    scope.NewObject = {};
                    scope.I = {};
                    scope.NewObject.T_DESC = scope.obj.Itemlist[i].T_DESC;
                    scope.NewObject.T_CHESES_NO = scope.obj.Itemlist[i].T_CHESES_NO;
                    scope.NewObject.T_MODEL_NO = scope.obj.Itemlist[i].T_MODEL_NO;
                    scope.NewObject.T_BARCODE = scope.obj.Itemlist[i].T_BARCODE;
                    scope.NewObject.T_ENGIN_NO = scope.obj.Itemlist[i].T_ENGIN_NO;
                    //scope.NewObject.LicenceNo1 = scope.obj.Itemlist[i].LicenceNo1;

                    scope.NewItemList.push(scope.NewObject);
                    //------------------------------------

                    
                }
                scope.NewObject1 = {};
                scope.NewObject1.T_DESC = item.T_DESC1;
                scope.NewObject1.T_CHESES_NO = item.T_CHESES_NO1;
                scope.NewObject1.T_MODEL_NO = item.T_MODEL_NO1;
                scope.NewObject1.T_BARCODE = item.T_BARCODE1;
                scope.NewObject1.T_ENGIN_NO = item.T_ENGIN_NO1;
                //scope.NewObject1.LicenceNo1 = item.LicenceNo;

                scope.NewItemList.push(scope.NewObject1);
           // scope.Item = true;
               
            } else {
                scope.NewObject = {};
                scope.NewObject.T_DESC = item.T_DESC1;
                scope.NewObject.T_CHESES_NO = item.T_CHESES_NO1;
                scope.NewObject.T_MODEL_NO = item.T_MODEL_NO1;
                scope.NewObject.T_BARCODE = item.T_BARCODE1;
                scope.NewObject.T_ENGIN_NO = item.T_ENGIN_NO1;
                //scope.NewObject.LicenceNo1 = item.LicenceNo;

                scope.NewItemList.push(scope.NewObject);
            }
          
            scope.obj.Itemlist = scope.NewItemList;
            scope.NewItemList = [];
            scope.InputItem = false;
        }

    }]);
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