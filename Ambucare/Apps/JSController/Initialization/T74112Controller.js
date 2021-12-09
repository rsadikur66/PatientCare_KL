﻿app.controller("T74112Controller", ["$scope", "$filter", "$http", "$window", "T74112Service", "LabelService",  "uiGridConstants",
    "DTOptionsBuilder", "Data", "DTColumnBuilder",
    function (scope, $filter, $http, $window, T74112Service, LabelService, uiGridConstants, DTOptionsBuilder, Data, DTColumnBuilder) {
        //For Instance
        scope.obj = {};
        scope.T74050 = {};
        scope.T02003 = {};
        scope.T74059 = {};

        scope.T74050 = Data;
        scope.obj = Data;

        scope.T74051 = Data;
        scope.T02003 = Data;
        scope.T74059 = Data;
        //scope.obj = Data;
    
        var labelData = LabelService.getlabeldata('T74112');
        labelData.then(function (data) {
            scope.entity = data;
        });
        scope.stest = function(data) {
            alert(JSON.stringify(data.target.id + JSON.parse(sessionStorage.getItem("FCode"))));
        };
        
        //----------Tab Toggling--------------Start
        scope.tabs = [
            { title: 'Gender', url: 'tabGender.tpl.html', hdn: 'G' },
            { title: 'Marital Status', url: 'tabMaritalStatus.tpl.html', hdn: 'M' },
            { title: 'Nationality', url: 'tabNationality.tpl.html', hdn: 'N' },
            { title: 'Religion', url: 'tabReligion.tpl.html', hdn: 'R' }
        ];
        scope.currentTab = 'tabGender.tpl.html';
        scope.obj.hiddenField = 'G';
        scope.onClickTab = function (tab) {
            scope.obj.hiddenField = tab.hdn;
            scope.currentTab = tab.url;

                if (scope.obj.hiddenField === 'G') {
                    
                    scope.T74050.T_SEX_CODE = '';
                    scope.T74050.T_LANG2_NAME = '';
                    scope.T74050.T_LANG1_NAME = '';
                    scope.T74050.T_SHORT_GNDR_NAME = '';
                    scope.T74050 = Data;

                } else if (scope.obj.hiddenField === 'M') {
                    scope.T74051.T_MRTL_STATUS_CODE = '';
                   
                    scope.T74051.T_LANG2_NAME = '';
                    scope.T74051.T_LANG1_NAME = '';
                    scope.T74051 = Data;
                } else if (scope.obj.hiddenField === 'N') {
                    scope.T02003.T_NTNLTY_ID = '';
                    scope.T02003.T_LANG2_NAME = '';
                    scope.T02003.T_LANG1_NAME = '';
                    scope.T02003.T_PRIM_LANG = '';
                    scope.T02003.T_SECOND_LANG = '';
                    scope.T02003 = Data;
                } else if (scope.obj.hiddenField === 'R') {
                    scope.T74059.T_RLGN_CODE = '';
                    scope.T74059.T_LANG2_NAME = '';
                    scope.T74059.T_LANG1_NAME = '';
                    scope.obj = Data;
                }
            
        
        }
        scope.isActiveTab = function (tabUrl) {
            
            return tabUrl === scope.currentTab;
            
        }
        //----------Tab Toggling--------------End


        //For Insert Update Start
        scope.Save_Click = function () {
            if (scope.obj.hiddenField === 'G') {
                if (scope.T74050.T_SEX_CODE === undefined) {
                    var save = T74112Service.InsertOrUpdateT74050(scope.T74050);
                    save.then(function (data) {
                        //alert("Data Save Succesfully");
                        alert(scope.getSingleMsg('S0012'));
                        $window.location.reload();
                    });
                   // $window.location = "";
                   
                } else {
                    var upd = T74112Service.InsertOrUpdateT74050(scope.T74050);
                    upd.then(function (data) {
                       // alert("Data updated Succesfully");
                        alert(scope.getSingleMsg('S0003'));
                        $window.location.reload();
                    });
                   // $window.location = "";
                   
                }
            }
            else if (scope.obj.hiddenField === 'M') {
                if (scope.T74051.T_MRTL_STATUS_CODE === "") {
                    var save = T74112Service.InsertOrUpdateT74051(scope.T74051);
                    save.then(function (data) {
                        //alert("Data Save Succesfully");
                        alert(scope.getSingleMsg('S0012'));
                        $window.location.reload();
                    });
                   // $window.location = "";
                   
                    //scope.T74051 = "";

                } else {
                    var upd = T74112Service.InsertOrUpdateT74051(scope.T74051);
                    upd.then(function (data) {
                        //alert("Data updated Succesfully");
                        alert(scope.getSingleMsg('S0003'));
                        $window.location.reload();
                    });
                    scope.T74051 = "";
                    //$window.location = "";
                   
                }
            }
            else if (scope.obj.hiddenField === 'N') {
                if (scope.T02003.T_NTNLTY_ID === "") {
                    var save = T74112Service.InsertOrUpdateT02003(scope.T02003);
                    save.then(function (data) {
                        //alert("Data Save Succesfully");
                        alert(scope.getSingleMsg('S0012'));
                        $window.location.reload();
                    });
                   // $window.location = "";
                  

                } else {
                    var upd = T74112Service.InsertOrUpdateT02003(scope.T02003);
                    upd.then(function (data) {
                        //alert("Data updated Succesfully");
                        alert(scope.getSingleMsg('S0003'));
                        $window.location.reload();
                    });
                   // $window.location = "";
                   
                }
            }
            else if (scope.obj.hiddenField === 'R') { 
                if (scope.T74059.T_RLGN_CODE === "") {
                    var save = T74112Service.InsertOrUpdateT74059(scope.T74059);
                    save.then(function (data) {
                        //alert("Data Save Succesfully");
                        alert(scope.getSingleMsg('S0012'));
                        $window.location.reload();
                    });
                   // $window.location = "";
                   

                } else {
                    var upd = T74112Service.InsertOrUpdateT74059(scope.T74059);
                    upd.then(function (data) {
                        //alert("Data updated Succesfully");
                        alert(scope.getSingleMsg('S0003'));
                        $window.location.reload();
                    });
                   // $window.location = "";
                  
                }
            }
            else {
                //alert('Data not Saved!');
                alert(scope.getSingleMsg('S0042'));
            }

        }
        //For Insert Update End

        //For Clear Start
        scope.Clear = function () {
            if (scope.obj.hiddenField === 'G') {

                scope.T74050.T_SEX_CODE = '';
                scope.T74050.T_LANG2_NAME = '';
                scope.T74050.T_LANG1_NAME = '';
                scope.T74050.T_SHORT_GNDR_NAME = '';
                scope.T74050 = Data;

            } else if (scope.obj.hiddenField === 'M') {
                scope.T74051.T_MRTL_STATUS_CODE = '';

                scope.T74051.T_LANG2_NAME = '';
                scope.T74051.T_LANG1_NAME = '';
                scope.T74051 = Data;
            } else if (scope.obj.hiddenField === 'N') {
                scope.T02003.T_NTNLTY_ID = '';
                scope.T02003.T_LANG2_NAME = '';
                scope.T02003.T_LANG1_NAME = '';
                scope.T02003.T_PRIM_LANG = '';
                scope.T02003.T_SECOND_LANG = '';
                scope.T02003 = Data;
            } else if (scope.obj.hiddenField === 'R') {
                scope.T74059.T_RLGN_CODE = '';
                scope.T74059.T_LANG2_NAME = '';
                scope.T74059.T_LANG1_NAME = '';
                scope.T74059 = Data;
            }
        }
        //For Clear End

        //For Delete Start
        scope.Delete_Click = function () {
            if (scope.obj.hiddenField === 'G') {
                if (scope.T74050.T_SEX_CODE != "") {
                    var delet = $window.confirm("Are you sure to delete it?");
                    if (delet) {
                        var dele = T74112Service.deleteData(scope.T74050.T_SEX_CODE);
                        dele.then(function (data) {
                            //alert("Data Deleted Succesfully");
                            alert(scope.getSingleMsg('S0007'));
                            //$window.location = "";
                            $window.location.reload();
                        });
                    }

                } else {
                    //alert("Select a data for delete.");
                    alert(scope.getSingleMsg('S0011'));
                }
            }
            else if (scope.obj.hiddenField === 'M') {
                if (scope.T74051.T_MRTL_STATUS_CODE != "") {
                    var deletes = $window.confirm("Are you sure to delete it?");
                    if (deletes) {
                        var dele = T74112Service.deleteDataMarital(scope.T74051.T_MRTL_STATUS_CODE);
                        dele.then(function (data) {
                            //alert("Data Deleted Succesfully");
                            alert(scope.getSingleMsg('S0007'));
                            //$window.location = "";
                            $window.location.reload();
                        });
                    }
                } else {
                    //alert("Select a data for delete.");
                    alert(scope.getSingleMsg('S0011'));
                }
            }
            else if (scope.obj.hiddenField === 'N') {
                if (scope.T02003.T_NTNLTY_ID != "") {
                    var de = $window.confirm("Are you sure to delete it?");
                    if (de) {
                        var dele = T74112Service.deleteDataNationality(scope.T02003.T_NTNLTY_ID);
                        dele.then(function (data) {
                            //alert("Data Deleted Succesfully");
                            alert(scope.getSingleMsg('S0007'));
                            //$window.location = "";
                            $window.location.reload();
                        });
                    }
                } else {
                    //alert("Select a data for delete.");
                    alert(scope.getSingleMsg('S0011'));
                }
            }
            else if (scope.obj.hiddenField === 'R') {
                if (scope.T74059.T_RLGN_CODE != "") {
                    var de = $window.confirm("Are you sure to delete it?");
                    if (de) {
                        var dele = T74112Service.deleteDataReligion(scope.T74059.T_RLGN_CODE);
                        dele.then(function (data) {
                            //alert("Data Deleted Succesfully");
                            alert(scope.getSingleMsg('S0007'));
                            //$window.location = "";
                            $window.location.reload();
                        });
                    }
                } else {
                    //alert("Select a data for delete.");
                    alert(scope.getSingleMsg('S0011'));
                }
            }
        }
        //For Delete End

        //For Gender Grid Function Start 
        scope.someClickHandler = someClickHandler;

        scope.dtColumns = [
            //here We will add .withOption('name','column_name') for send column name to the server 
            DTColumnBuilder.newColumn("T_SEX_CODE", "Gender Code").withOption('name', 'T_SEX_CODE').notVisible(),
            DTColumnBuilder.newColumn("T_LANG2_NAME", "English Name").withOption('name', 'T_LANG2_NAME'),
            DTColumnBuilder.newColumn("T_LANG1_NAME", "Local Name").withOption('name', 'T_LANG1_NAME'),
            DTColumnBuilder.newColumn("T_SHORT_GNDR_NAME", "Short Name").withOption('name', 'T_SHORT_GNDR_NAME')
        ];
        scope.dtOptions = DTOptionsBuilder.newOptions().withOption('ajax',
            {
                dataSrc: "data",
              url: scope.vrDir+"/T74112/GetGridData",
                type: "POST"
            })
            .withOption('rowCallback', rowCallback)
            .withOption('processing', true) //for show progress bar
            .withOption('serverSide', true) // for server side processing
            .withPaginationType(
            'full_numbers') // for get full pagination options // first / last / prev / next and page numbers
            .withDisplayLength(10) // Page size
            .withOption('aaSorting', [0, 'asc']); // for default sorting column // here 0 means first column
        function someClickHandler(info) {
            scope.T74050.T_SEX_CODE = info.T_SEX_CODE;
            scope.T74050.T_LANG2_NAME = info.T_LANG2_NAME;
            scope.T74050.T_LANG1_NAME = info.T_LANG1_NAME;
            scope.T74050.T_SHORT_GNDR_NAME = info.T_SHORT_GNDR_NAME;
            //var prevUrl = history.length > 1 ? history.splice(-2)[0] : "/";
            //$location.path(prevUrl);
            //window.location.href = "/Menus/T04201?Pat_ID =" + info.CustomerID;
        }
        //For Gender Grid Function End
        var reloadData = function () {
            scope.dtOptions.reloadData();
        };

        ////For Marital Grid Function Start debugger;
        scope.clickHandlerMarital = clickHandlerMarital;
        scope.dtColumnsMarital = [
            //here We will add .withOption('name','column_name') for send column name to the server 
            DTColumnBuilder.newColumn("T_MRTL_STATUS_CODE", "Marital Status Code").withOption('name','T_MRTL_STATUS_CODE').notVisible(),
            DTColumnBuilder.newColumn("T_LANG2_NAME", "English Name").withOption('name', 'T_LANG2_NAME'),
            DTColumnBuilder.newColumn("T_LANG1_NAME", "Local Name").withOption('name', 'T_LANG1_NAME')

        ];
        scope.dtOptionsMarital = DTOptionsBuilder.newOptions().withOption('ajax',
            {
                dataSrc: "data",
              url: scope.vrDir + "/T74112/GetMaritalGrid",// Controller/ActionResult
                type: "POST"
            })
            .withOption('rowCallback', rowCallback)
            .withOption('processing', true) //for show progress bar
            .withOption('serverSide', true) // for server side processing
            .withPaginationType(
            'full_numbers') // for get full pagination options // first / last / prev / next and page numbers
            .withDisplayLength(10) // Page size
            .withOption('aaSorting', [0, 'asc']); // for default sorting column // here 0 means first column
        function clickHandlerMarital(v) {
            scope.T74051.T_MRTL_STATUS_CODE = v.T_MRTL_STATUS_CODE;
            scope.T74051.T_LANG2_NAME = v.T_LANG2_NAME;
            scope.T74051.T_LANG1_NAME = v.T_LANG1_NAME;
            //var prevUrl = history.length > 1 ? history.splice(-2)[0] : "/";
            //$location.path(prevUrl);
            //window.location.href = "/Menus/T04201?Pat_ID =" + info.CustomerID;
        }

        ////For Marital Grid Function End



        //For Nationality Grid Function Start

        scope.clickHandlerNationality = clickHandlerNationality;
        scope.dtColumnsNationality = [
            //here We will add .withOption('name','column_name') for send column name to the server 
            DTColumnBuilder.newColumn("T_NTNLTY_ID", "Nationality Code").withOption('name', 'T_NTNLTY_ID').notVisible(),
            DTColumnBuilder.newColumn("T_LANG2_NAME", "English Name").withOption('name', 'T_LANG2_NAME'),
            DTColumnBuilder.newColumn("T_LANG1_NAME", "Local Name").withOption('name', 'T_LANG1_NAME'),
          //  DTColumnBuilder.newColumn("T_PRIM_LANG", "Primary Language").withOption('name', 'T_PRIM_LANG'),
           // DTColumnBuilder.newColumn("T_SECOND_LANG", "Local Language").withOption('name', 'T_SECOND_LANG')

        ];
        scope.dtOptionsNationality = DTOptionsBuilder.newOptions().withOption('ajax',
            {
                dataSrc: "data",
              url: scope.vrDir + "/T74112/GetNationalityGrid", // Controller/ActionResult
                type: "POST"
            })
            .withOption('rowCallback', rowCallback)
            .withOption('processing', true) //for show progress bar
            .withOption('serverSide', true) // for server side processing
            .withPaginationType(
            'full_numbers') // for get full pagination options // first / last / prev / next and page numbers
            .withDisplayLength(10) // Page size
            .withOption('aaSorting', [0, 'asc']); // for default sorting column // here 0 means first column
        function clickHandlerNationality(d) {
            scope.T02003.T_NTNLTY_ID = d.T_NTNLTY_ID;
            scope.T02003.T_LANG2_NAME = d.T_LANG2_NAME;
            scope.T02003.T_LANG1_NAME = d.T_LANG1_NAME;
            scope.T02003.T_PRIM_LANG = d.T_PRIM_LANG;//primary
            scope.T02003.T_SECOND_LANG = d.T_SECOND_LANG;//second
            //var prevUrl = history.length > 1 ? history.splice(-2)[0] : "/";
            //$location.path(prevUrl);
            //window.location.href = "/Menus/T04201?Pat_ID =" + info.CustomerID;
        }

        //For Nationality Grid Function End

        //For Religion Grid Function Start
        scope.clickHandlerReligion = clickHandlerReligion;
        scope.dtColumnsReligion = [
            //here We will add .withOption('name','column_name') for send column name to the server 
            DTColumnBuilder.newColumn("T_RLGN_CODE", "Religion Code").withOption('name', 'T_RLGN_CODE').notVisible(),
            DTColumnBuilder.newColumn("T_LANG2_NAME", "English Name").withOption('name', 'T_LANG2_NAME'),
            DTColumnBuilder.newColumn("T_LANG1_NAME", "Local Name").withOption('name', 'T_LANG1_NAME')

        ];

        scope.dtOptionsReligion = DTOptionsBuilder.newOptions().withOption('ajax',
            {
                dataSrc: "data",
              url: scope.vrDir + "/T74112/GetReligionGrid",// Controller/ActionResult
                type: "POST"
            })
            .withOption('rowCallback', rowCallback)
            .withOption('processing', true) //for show progress bar
            .withOption('serverSide', true) // for server side processing
            .withPaginationType(
            'full_numbers') // for get full pagination options // first / last / prev / next and page numbers
            .withDisplayLength(10) // Page size
            .withOption('aaSorting', [0, 'asc']); // for default sorting column // here 0 means first column
        function clickHandlerReligion(a) {
            scope.T74059.T_RLGN_CODE = a.T_RLGN_CODE;
            scope.T74059.T_LANG2_NAME = a.T_LANG2_NAME;
            scope.T74059.T_LANG1_NAME = a.T_LANG1_NAME;

            //var prevUrl = history.length > 1 ? history.splice(-2)[0] : "/";
            //$location.path(prevUrl);
            //window.location.href = "/Menus/T04201?Pat_ID =" + info.CustomerID;
        }
        //For Religion Grid Function End

        //function test () {
        //    if()
        //}

        function rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
            // Unbind first in order to avoid any duplicate handler (see https://github.com/l-lin/angular-datatables/issues/87)
            $('td', nRow).unbind('click');
            $('td', nRow).bind('click', function () {
                scope.$apply(function () {
                    if (scope.obj.hiddenField === 'G') {
                        scope.someClickHandler(aData);
                        
                    } else if (scope.obj.hiddenField === 'M') {
                        scope.clickHandlerMarital(aData);
                       
                    } else if (scope.obj.hiddenField === 'N') {
                        scope.clickHandlerNationality(aData);
                       
                    } else if (scope.obj.hiddenField === 'R') {
                        scope.clickHandlerReligion(aData);
                       
                    }
                   // scope.clickHandlerNationality(aData);
                    //scope.clickHandlerReligion(aData);
                });
            });
            return nRow;
        }




    }]);





