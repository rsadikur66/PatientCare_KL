app.controller("T74014Controller",
    ["$scope", "$rootScope", "$filter", "$http", "$window", "T74014Service", "LabelService", "uiGridConstants", "DTOptionsBuilder",
        "DTColumnBuilder", "Data", "EmployeeFactory",
        function ($scope, $rootScope, $filter, $http, $window, T74014Service, LabelService, uiGridConstants, DTOptionsBuilder, DTColumnBuilder, Data, EmployeeFactory) {
            //For Instance 
            $scope.obj = {};
            $scope.obj = Data;
            $scope.obj.T_AMBU_REG_ID = '';
            $scope.obj.A = {};
            $scope.obj.B = {};
            $scope.obj.C = {};
            $scope.obj.D = {};
            $scope.dtInstance = {};


            //EntryUser Function Start
            var EntryUser = T74014Service.EntryUser();
            EntryUser.then(function (data) {
                $scope.obj.T_ENTRY_USER = data;
                $scope.obj.T_ENTRY_DATE = $filter('date')(new Date(), 'dd-MMM-yyyy');
            });

            //EntryUser Function End
            // Form Label Data Service Start 
            var labelData = LabelService.getlabeldata('T74014');
            labelData.then(function (data) {
                $scope.entity = data;
            });
            // Form Label Data Service End

            //Save and Update Function Start 
            $scope.Save_Click = function () {
                Save_Click();
            };

            function Save_Click() {
                if ($scope.obj.T_AMBU_REG_NUM == undefined || $scope.obj.T_AMBU_REG_NUM == '') {
                    //$window.confirm('Ambulance registration no required');
                    alert($scope.getSingleMsg('S0039'));
                    $scope.localName.show();
                    return;
                }
                if ($scope.obj.T_ENGINE_NO == undefined || $scope.obj.T_ENGINE_NO == '') {
                    // $window.confirm('Engine No is required');
                    alert($scope.getSingleMsg('S0045'));
                    scope.localName.show();
                    return;
                }
                if ($scope.obj.T_REG_IMAGE == "/Images/RegistrationImage/No_Image_Available.jpg") {
                    $scope.obj.T_REG_IMAGE = '';
                }
                var save = T74014Service.saveTutorial($scope.obj);
                save.then(function (mesg) {
                    if (mesg == "Data Save Successfully") {
                        alert($scope.getSingleMsg('S0012'));
                    } else if (mesg == "Data Update Successfully") {
                        alert($scope.getSingleMsg('S0003'));
                    }
                    //alert(mesg);
                    $scope.obj = {};
                    Clear();
                    $scope.dtInstance.reloadData();
                });
            };

            var disable = $rootScope.$on('T74014Emit', function (event, data) {
                if (data == 'r') {
                    Save_Click();
                    //Clear();
                }
                if (data == 'x') {
                    Clear();
                }
            });
            $scope.$on('$destroy', function () {
                disable();
                
            });
            //Save and Update Function End 

            //For Delete Function Start
            $scope.Delete = function () {
                if ($scope.obj.T_AMBU_REG_ID != null) {
                    if ($window.confirm('Are you sure ?')) {
                        var dele = T74014Service.Deleted_T74014($scope.obj.T_AMBU_REG_ID);
                        dele.then(function (data) {
                            //alert("Data Deleted Succesfully");
                            alert($scope.getSingleMsg('S0007'));
                            $scope.obj = {};
                            Clear();
                            $scope.dtInstance.reloadData();
                        });
                    }
                    return true;
                } else {
                    //alert("Select a data for delete.");
                    alert($scope.getSingleMsg('S0011'));
                }
            };
            //Delete Function End

            //Get Color Code Function Start
            var colorId = T74014Service.GetColorId();
            colorId.then(function (data) {
                $scope.color = data;
            });
            //Get Color Code Function End

            //Get Ambulance Type Function Start
            var ambuId = T74014Service.GetAmbulanceId();
            ambuId.then(function (data) {
                $scope.ambutype = data;
            });
            //Get Color Code Function End 

            //Get Brand Type Function Start
            var brandtype = T74014Service.GetBrandId();
            brandtype.then(function (data) {
                $scope.BrandId = data;
            });
            //Get Brand Type Function End

            //Employee Model Function Start
            //var empdata = T74014Service.GetEmployeeData();
            //empdata.then(function (data) {
            //    scope.emp = data;
            //});
            $scope.emp = function () {
                document.getElementById('divEmpList').style.display = 'Block';
                var ow = T74014Service.getAmbulanceOwnerData();
                ow.then(function (data) {
                    $scope.obj.OwnerList = data;
                });
                //var url = '';
                //var data = '';
                //EmployeeFactory.getModal(url, data, '', '22');
            };

            $scope.obj.CloseUserPopup = function()
            {
                document.getElementById('divEmpList').style.display = 'none';
            }

            $scope.obj.onUserSelect = function(ind, data) {
                $scope.obj.T_EMP_ID=data.T_EMP_ID;
                $scope.obj.T_FIRST_LANG2_NAME = data.T_NAME;
                document.getElementById('divEmpList').style.display = 'none';
            }
            //Employee Model Function End

            $scope.Clear = function () {
                Clear();
            };

            //Clear Function Start
            function Clear() {
                $scope.obj = {};
                $scope.obj.T_EMP_ID = '';
                $scope.obj.T_AMBU_REG_ID = '';
                $scope.obj.T_AMBU_REG_NUM = '';
                $scope.obj.T_CHASIS_NO = '';
                $scope.obj.T_ENGINE_NO = '';
                $scope.obj.T_WEIGHT = '';
                $scope.obj.T_REG_IMAGE = '';
                $scope.obj.T_BARCODE_ID = '';
                $scope.obj.T_YEAR_MODEL = '';
                $scope.obj.T_COLOR_ID = '';
                $scope.obj.T_AMB_TYPE_ID = '';
                $scope.obj.T_BRAND_ID = '';
                $scope.obj.T_REG_DATE = '';
                $scope.obj.A = {};
                $scope.obj.B = {};
                $scope.obj.C = {};
                $scope.obj.FullName = '';
                $scope.obj.T_SERIES = '';
                angular.forEach(
                    angular.element("input[type='file']"),
                    function (inputElem) {
                        angular.element(inputElem).val(null);
                    });
                $scope.obj.T_AMBU_REG_ID = undefined;
                //$scope.stepsModel = [];
                $scope.step = {};
            }
            //Clear Function End
            //For Grid Function Start 

            $scope.dtInstanceCallback = function (instance) {
                $scope.dtInstance = instance;
            }

            $scope.someClickHandler = someClickHandler;
            $scope.dtColumns = [
                //here We will add .withOption('name','column_name') for send column name to the server
                DTColumnBuilder.newColumn("EmpId", "Employee ID").withOption('name', 'EmpId'),
                DTColumnBuilder.newColumn("EmpName", "Name").withOption('name', 'EmpName').withOption('width', '200px'),
                DTColumnBuilder.newColumn("AmbuRegId", "Reg ID").withOption('name', 'AmbuRegId').notVisible(),
                DTColumnBuilder.newColumn("AmbuRegNo", "Reg Number").withOption('name', 'AmbuRegNo'),
                DTColumnBuilder.newColumn("RegDate", "RegDate").renderWith(function (data, type) {
                    var myDate = new Date(data.match(/\d+/)[0] * 1);
                    return $filter('date')(myDate, "dd/MM/yyyy");
                }),
                DTColumnBuilder.newColumn("ChasisNo", "Chasis").withOption('name', 'ChasisNo'),
                DTColumnBuilder.newColumn("EngineNo", "Engine").withOption('name', 'EngineNo'),
                DTColumnBuilder.newColumn("Weight", "Weight").withOption('name', 'Weight'),
                DTColumnBuilder.newColumn("RegImage", "RegImage").withOption('name', 'RegImage').notVisible(),
                DTColumnBuilder.newColumn("Series", "Series").withOption('name', 'Series').notVisible(),
                DTColumnBuilder.newColumn("BarcodeId", "Barcode").withOption('name', 'BarcodeId').notVisible(),
                DTColumnBuilder.newColumn("YearModel", "Model").withOption('name', 'YearModel').notVisible(),
                DTColumnBuilder.newColumn("ColorId", "Color Id").withOption('name', 'ColorId').notVisible(),
                DTColumnBuilder.newColumn("ColorName", "Color").withOption('name', 'ColorName').notVisible(),
                DTColumnBuilder.newColumn("AmbuId", "Ambu Type ID").withOption('name', 'AmbuId').notVisible(),
                DTColumnBuilder.newColumn("AmbuName", "Ambulance").withOption('name', 'AmbuName'),
                DTColumnBuilder.newColumn("BrandId", "Brand").withOption('name', 'BrandId').notVisible(),
                DTColumnBuilder.newColumn("BrandName", "Brand").withOption('name', 'BrandName')

            ];
            function addressHtml(data, type, full, meta) {
                return data.addr1 + ' - ' + data.addr2;
            }
            //T_REG_DATE
            $scope.dtOptions = DTOptionsBuilder.newOptions().withOption('ajax',
                    {
                        dataSrc: "data",
                        url: $scope.vrDir + "/T74014/GetGridData",
                        type: "POST"
                    })
                .withOption('rowCallback', rowCallback)
                .withOption('processing', true) //for show progress bar
                .withOption('serverSide', true) // for server side processing
                .withPaginationType(
                    'full_numbers') // for get full pagination options // first / last / prev / next and page numbers
                .withDisplayLength(10) // Page size
                .withOption('aaSorting', [0, 'asc']) // for default sorting column // here 0 means first column

            function someClickHandler(info) {
                $scope.imgDiv = true;
                $scope.obj.T_EMP_ID = info.EmpId;
                $scope.obj.T_FIRST_LANG2_NAME = info.EmpName;
                $scope.obj.T_AMBU_REG_ID = info.AmbuRegId;
                $scope.obj.T_AMBU_REG_NUM = info.AmbuRegNo;
                $scope.obj.T_CHASIS_NO = info.ChasisNo;
                $scope.obj.T_ENGINE_NO = info.EngineNo;
                $scope.obj.T_WEIGHT = info.Weight;
                $scope.obj.T_SERIES = info.Series;
                $scope.obj.T_BARCODE_ID = info.BarcodeId;
                $scope.obj.T_YEAR_MODEL = info.YearModel;
                $scope.obj.T_COLOR_ID = info.ColorId;
                $scope.obj.T_LANG2_NAME = info.ColorName;
                $scope.obj.T_AMBU_TYPE_ID = info.AmbuId;
                $scope.obj.T_LANG2_NAME = info.AmbuName;
                $scope.obj.T_ITEM_BRA_ID = info.BrandId;
                $scope.obj.T_LANG2_NAME = info.BrandName;
                var myDate = new Date(info.RegDate.match(/\d+/)[0] * 1);
                $scope.obj.T_REG_DATE = $filter('date')(myDate, "yyyy-MM-dd");
                $scope.obj.A.selected = { T_LANG2_NAME: info.AmbuName, T_AMBU_TYPE_ID: info.AmbuId };
                $scope.obj.B.selected = { T_LANG2_NAME: info.BrandName, T_ITEM_BRA_ID: info.BrandId };
                $scope.obj.C.selected = { T_LANG2_NAME: info.ColorName, T_ITEM_COLOR_ID: info.ColorId };
                if (info.RegImage != null) {
                    $scope.imgDiv = false;
                    $scope.stepsModel = [];
                    $scope.obj.T_REG_IMAGE = "/Images/RegistrationImage/" + info.RegImage;
                    $scope.step = "/Images/RegistrationImage/" + info.RegImage;
                } else {
                    $scope.obj.T_REG_IMAGE = "/Images/RegistrationImage/No_Image_Available.jpg";
                    $scope.step = "/Images/RegistrationImage/No_Image_Available.jpg";

                }
            }
            function rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                // Unbind first in order to avoid any duplicate handler (see https://github.com/l-lin/angular-datatables/issues/87)
                $('td', nRow).unbind('click');
                $('td', nRow).bind('click', function () {
                    $scope.$apply(function () {
                        $scope.someClickHandler(aData);
                    });
                });
                return nRow;
            }
            $scope.imgDiv = false;
            //For Grid Function End 
            //Image Upload Function Start
            $scope.stepsModel = [];
            $scope.imageUpload = function (event) {
                $scope.obj.T_REG_IMAGE = '';
                $scope.imgDiv = false;
                var files = event.target.files; //FileList object         
                for (var i = 0; i < files.length; i++) {
                    var file = files[0];
                    var reader = new FileReader();
                    reader.onload = $scope.imageIsLoaded;
                    reader.readAsDataURL(file);

                    //var imageServer = T74014Service.GetImageData(reader.result.substr(reader.result.indexOf(',') + 1));
                    //imageServer.then(function (data) {
                    //    setInterval(function () {

                    //    }, 5000);
                    //    //alert(data);
                    //});                    
                }
                //var base64result = reader.result.substr(reader.result.indexOf(',') + 1);
                //if (base64result !== "") {


                // }                
            };
            $scope.imageIsLoaded = function (e) {
                $scope.$apply(function () {
                    //$scope.stepsModel.push(e.target.result);
                    $scope.step = e.target.result;
                    //$scope.img = e.target.result;
                });
            };

            $scope.imageClick = function () {
               // alert('work');
                // Get the modal
                var modal = document.getElementById("myModal");
                var modalImg = document.getElementById("img01");
                modal.style.display = "block";
                modalImg.src = $scope.step;
                //captionText.innerHTML = this.alt;
            };
            var modal = document.getElementById("myModal");
            myModal.onclick = function () {
                modal.style.display = "none";
            };
            $scope.checkAmbuRegNo = function (e) {
                var getRegNo = T74014Service.getRegNo(e);
                getRegNo.then(function (data) {
                    var RegNoCount = data;
                    if (RegNoCount != 0) {
                        alert("Registration no is already exist!!!");
                        return;
                    } else {
                        document.getElementById('txtChasisNo').focus();
                    }

                });

            };
            
            $scope.checkAmbuChesisNo = function (e) {
                var getChesisNo = T74014Service.getChesisNo(e);
                getChesisNo.then(function (data) {
                    var ChesisNoCount = data;
                    if (ChesisNoCount != 0) {
                        alert("Chesis no is already exist!!!");
                        return;
                    } else {
                        document.getElementById('txtYearModel').focus();

                    }

                });

            };

            $scope.checkAmbuEngineNo = function (e) {
                var getEngineNo = T74014Service.getEngineNo(e);
                getEngineNo.then(function (data) {
                    var EngineNoCount = data;
                    if (EngineNoCount != 0) {
                        alert("Engine no is already exist!!!");
                        return;
                    } else {
                        document.getElementById('txtWeight').focus();

                    }

                });

            };
            //Image Upload Function End
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
    //
});
