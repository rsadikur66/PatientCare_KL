app.controller("T74015Controller", ["$scope", "$rootScope", "$filter", "$http", "$window", "T74015Service", "uiGridConstants", "DTOptionsBuilder", "DTColumnBuilder", "LabelService", "Data", "EmployeeFactory", "EmployeeTypeFactory",
    function ($scope, $rootScope, $filter, $http, $window, T74015Service, uiGridConstants, DTOptionsBuilder, DTColumnBuilder, LabelService, Data, EmployeeFactory, EmployeeTypeFactory) {







        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.T_EMP_ID = '';
        $scope.obj.T74015 = {};
        $scope.obj._T74015List = {};
        $scope.obj.employeeInfo = {};
        //document.getElementById('scheduleDiv').style.display = 'none';
        
        $scope.ViewSchedule = function (value) {
            alert(value);
        }
        var labelData = LabelService.getlabeldata('T74015');
        labelData.then(function (data) {
            $scope.entity = data;
        });

        var ambulanceId = T74015Service.getAmbulanceDropdownList();
        ambulanceId.then(function (data) {
            $scope.ambulance = data;
        });

        

        $scope.check = function () {
            var employeeDetails = T74015Service.getEmployeeTypeIdAndEmployeeIdByAmbulanceId($scope.obj.T_AMBU_REG_ID);
            employeeDetails.then(function (data) {
                $scope.obj.employeeInfo = data;
                console.log($scope.obj.employeeInfo);
            });

            $scope.obj.employeeInfo = [];
            $scope.obj._t74073 = [];

            $scope.obj._t74073 = {
                T_COST_TYPE_ID: 81,
                T_PRICE: 0,
                T_LANG2_NAME: $scope.obj.A.selected,
                T_ID: $scope.obj.T_AMBU_REG_ID
            };

            $scope.Addrow = function () {
                $scope.obj.employeeInfo.push({
                    T_AMBU_REG_ID: $scope.obj.T_AMBU_REG_ID,
                    T_EMP_ASSIGN_ID: '',
                    T_EMP_TYP_ID: '',
                    T_EMP_TYP_NAME: '',
                    T_EMP_ID: '',
                    FullName: '',
                    T_ACTIVE_STATUS: ''
                });
            }
        };

        $scope.obj.assignList = [];

        $scope.RemoveRow = function (index) {
            $scope.obj.assignList.push(
                {
                    T_EMP_ASSIGN_ID: $scope.obj.employeeInfo[index].T_EMP_ASSIGN_ID,
                    //T_EMP_ID: $scope.obj.employeeInfo[index].T_EMP_ID,
                    T_ACTIVE_STATUS: $scope.obj.employeeInfo[index].T_ACTIVE_STATUS = ''    
                }
            );
            var list = document.getElementById('rowId'+index);
            list.style.display = 'none';
            //$scope.obj.employeeInfo.slice(index, 1);
            //console.log($scope.obj.assignList);
            //console.log($scope.obj.employeeInfo);

        };
        
        $scope.Save_Click = function () {
            Save_Click();
        };

        function Save_Click() {
            if ($scope.obj.T_AMBU_REG_ID != null) {
                if ($scope.obj.T_EMP_ID != null) {
                    var checkstatus = 0;
                    for (var i = 0; i < $scope.obj.employeeInfo.length; i++) {
                        if ($scope.obj.employeeInfo[i].T_EMP_TYP_ID == '6') {
                            checkstatus++;
                        } else if ($scope.obj.employeeInfo[i].T_EMP_TYP_ID == '21') {
                            checkstatus++;
                        } else if ($scope.obj.employeeInfo[i].T_EMP_TYP_ID == '102') {
                            checkstatus++;
                        }
                    }
                    if (checkstatus == 3) {
                        //var insert_t74073 = T74015Service.insert_t74073($scope.obj._t74073);
                        $scope.obj._T74015List = $scope.obj.employeeInfo;
                        var insert15 = T74015Service.insert_T74015($scope.obj._T74015List);
                        insert15.then(function (msg) {
                            alert($scope.getSingleMsg('S0012'));
                            $scope.check($scope.obj.T_AMBU_REG_ID);
                            $scope.obj.assignList = [];
                        });
                    } else {
                        alert('You do not assign paramedix or driver or Team officer!!!');
                        var r = confirm("Are you want to save data???");
                        if (r == true) {
                            //var insert_t74073 = T74015Service.insert_t74073($scope.obj._t74073);
                            $scope.obj._T74015List = $scope.obj.employeeInfo;
                             insert15 = T74015Service.insert_T74015($scope.obj._T74015List);
                            insert15.then(function (msg) {
                                alert($scope.getSingleMsg('S0012'));
                                $scope.check($scope.obj.T_AMBU_REG_ID);
                                $scope.obj.assignList = [];
                            });
                        }
                    }
                    //if (checkstatus == 'yes') {
                    //    if ($scope.obj.assignList.length > 0) {
                    //        $scope.obj._T74015Del = $scope.obj.assignList;
                    //        T74015Service.Del_T74015($scope.obj._T74015Del);
                    //    }
                    //    var insert_t74073 = T74015Service.insert_t74073($scope.obj._t74073);
                    //    $scope.obj._T74015List = $scope.obj.employeeInfo;
                    //    var update = T74015Service.insert_T74015($scope.obj._T74015List);
                    //    update.then(function (msg) {
                    //        alert($scope.getSingleMsg('S0012'));
                    //        $scope.check($scope.obj.T_AMBU_REG_ID);
                    //        $scope.obj.assignList = [];
                    //    });

                    //} else {
                    //    alert('you must assign paramedix and Team Officer!!!');
                    //}

                }
                else {
                    //alert('Select Employee');
                    alert($scope.getSingleMsg('S0009'));
                }
            } else {
                //alert('Select Ambulance');
                alert($scope.getSingleMsg('S0010'));
            }
        };
        var disable = $rootScope.$on('T74015Emit', function (event, data) {
            if (data == 'o') {
                Save_Click();
            }
        });

        $scope.$on('$destroy', function () {
            disable();
        });

        $scope.Delete_Click = function () {

            if ($scope.obj.T_AMBU_CAPA_TYPE_ID != null) {
                if ($window.confirm('Are you sure to want delete ?')) {
                    var dele = T74015Service.deleteData($scope.obj.T_AMBU_CAPA_TYPE_ID);
                    dele.then(function (data) {
                        //alert("Data Deleted Succesfully");
                        alert($scope.getSingleMsg('S0007'));
                        $window.location = "";

                    });
                }
            } else {
                // alert("Select a data for delete.");
                alert($scope.getSingleMsg('S0011'));
            }
        };

        $scope.Print = function () {
            $window.open("../Report/T74015Report", "popup", "width=600,height=600,left=100,top=50");
        };

        $scope.emp = function (index) {
            var url = '';
            var data = '';
            EmployeeFactory.getModal(url, data, index, $scope.obj.employeeInfo[index].T_EMP_TYP_ID);
            //EmployeeFactory.getModal(url, data, index);
        };

        $scope.empType = function (index) {
            var url = '';
            var data = '';
            EmployeeTypeFactory.getModalEmployeeType(url, data, index, $scope.obj.T_AMBU_REG_ID);
        };
        
    }]);