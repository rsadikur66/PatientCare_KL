app.controller("T74035Controller",
    ["$scope", "$filter", "$http", "$window", "uiGridConstants", "DTOptionsBuilder", "DTColumnBuilder", "T74035Service","LabelService","Data",
        function (scope, $filter, $http, $window, uiGridConstants, DTOptionsBuilder, DTColumnBuilder, T74035Service,LabelService,Data) {
            scope.obj = {};
            scope.obj = Data;


            // Form Label Data Service Start 
            var labelData = LabelService.getlabeldata('T74035');
            labelData.then(function (data) {
                scope.entity = data;
            });
            // Form Label Data Service End 


            //For Insert Update Start
            scope.Save_Click = function () {
                if (scope.obj.T_LANG1_NAME == undefined || scope.obj.T_LANG1_NAME == '') {
                    //$window.confirm('Local Name is requered');
                    alert(scope.getSingleMsg('M0002'));
                    scope.localName.show();
                    return;
                }
                if (scope.obj.T_LANG2_NAME == undefined || scope.obj.T_LANG2_NAME == '') {
                    //$window.confirm('English Name is requered');
                    alert(scope.getSingleMsg('M0003'));
                    scope.localName.show();
                    return;
                }
                if (scope.obj.T_LANG2_NAME != null) {
                    if (scope.obj.T_DEPET_ID != null) {

                        var update = T74035Service.InsertUpdate(scope.obj);
                        update.then(function (data) {
                            //alert("Data Update Succesfully");
                            alert(scope.getSingleMsg('S0003'));
                            $window.location.reload();

                        });
                    } else {
                        var save = T74035Service.InsertUpdate(scope.obj);

                        save.then(function (data) {
                            //alert("Data Save Succesfully");
                            alert(scope.getSingleMsg('S0012'));
                            $window.location.reload();

                        });
                    }
                } else {
                    //alert('Engilsh Name is Required');
                    alert(scope.getSingleMsg('M0003'));
                }
            }
            //For Clear
            scope.Clear = function () {
                scope.obj.T_DEPET_ID = '';
                scope.obj.T_LANG1_NAME = '';
                scope.obj.T_LANG2_NAME = '';
            }
            //For Delete
            scope.Delete_Click = function () {
                if (scope.obj.T_DEPET_ID != null) {
                    if ($window.confirm('Are you sure to want delete ?')) {
                        var dele = T74035Service.deleteData(scope.obj.T_DEPET_ID);
                        dele.then(function (data) {
                            //alert("Data Deleted Succesfully");
                            alert(scope.getSingleMsg('S0007'));
                            $window.location.reload();

                        });
                    }
                }
                else {
                    //alert("Select a data for delete.");
                    alert(scope.getSingleMsg('S0011'));
                }
            }

            //For Gridview
            scope.someClickHandler = someClickHandler;
            scope.dtColumns = [
                DTColumnBuilder.newColumn("T_DEPET_ID", "Department Id").withOption('name', 'T_DEPET_ID'),
                DTColumnBuilder.newColumn("T_LANG2_NAME", "English Name").withOption('name', 'T_LANG2_NAME'),
                DTColumnBuilder.newColumn("T_LANG1_NAME", "Local Name").withOption('name', 'T_LANG1_NAME')
            ];
            scope.dtOptions = DTOptionsBuilder.newOptions().withOption('ajax',
                    {
                        dataSrc: "data",
                        url: "/T74035/GetGridDataT74035",
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
                scope.obj.T_DEPET_ID = info.T_DEPET_ID;
                scope.obj.T_LANG2_NAME = info.T_LANG2_NAME;
                scope.obj.T_LANG1_NAME = info.T_LANG1_NAME;
            }
            function rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                $('td', nRow).unbind('click');
                $('td', nRow).bind('click', function () {
                    scope.$apply(function () {
                        scope.someClickHandler(aData);
                    });
                });
                return nRow;
            }

        }]);