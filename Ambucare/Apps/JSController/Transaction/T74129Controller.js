
app.controller("T74129Controller",["$scope", "$location", "$compile", "$filter", "$http", "$window", "T74129Service", "Data",
        function(scope, $location, $compile, $filter, $http, $window, T74129Service, Data) {
            //for instance
            scope.obj = {};
            scope.obj = Data;
            scope.obj.T74095 = {};
            
            var getData = T74129Service.getGridData();
            getData.then(function (data) {
                var cou = 0;
                scope.newDataList = [];
                for (var i = 0; i < data.length; i++) {
                    scope.newObject = {};
                    scope.newObject.T_FIRST_LANG2_NAME = data[i].T_FIRST_LANG2_NAME;
                    scope.newObject.T_PROB_DET_ID = data[i].T_PROB_DET_ID;
                    scope.newObject.T_PROB_DELT_LANG2 = data[i].T_PROB_DELT_LANG2;
                    scope.newObject.T_REPAIR_INST_DELT = data[i].T_REPAIR_INST_DELT;
                    var apDd = new Date(data[i].T_APR_DELV_DATE.match(/\d+/)[0] * 1);
                    scope.newObject.T_APR_DELV_DATE = $filter('date')(apDd, "dd-MM-yyyy");

                    var entD = new Date(data[i].T_ENTRY_DATE.match(/\d+/)[0] * 1);
                    scope.newObject.T_ENTRY_DATE = $filter('date')(entD, "dd-MM-yyyy");
                    scope.newObject.T_REPAIR_STATUS = data[i].T_REPAIR_STATUS;
                    if (data[i].T_REPAIR_STATUS!=='Done') {
                        cou = cou + 1;
                    }
                    scope.newDataList.push(scope.newObject);
                }
                scope.obj.DataList = scope.newDataList;
                scope.obj.Count = cou;
            });

            scope.setClickedRow = function (ind, obj) {
                if (obj.T_REPAIR_STATUS !=='Done') {
                    scope.obj.T74095.T_PROB_DET_ID = obj.T_PROB_DET_ID;
                    scope.obj.T74095.T_PROB_DELT_LANG2 = obj.T_PROB_DELT_LANG2;
                    scope.obj.T74095.T_REPAIR_INST_DELT = obj.T_REPAIR_INST_DELT;
                    scope.obj.T74095.T_REPAIR_DATE = $filter('date')(new Date(), "dd-MM-yyyy");;
                    scope.Edit_Text = true;
                } else {
                    //alert('It has been repaired !!!');
                    alert(scope.getSingleMsg('S0032'));
                    scope.Edit_Text = false;
                }
                
                    
            }

            scope.Save_Click = function() {
                var save = T74129Service.saveData(scope.obj.T74095);
                save.then(function(data) {
                    //alert('Save successfully');
                    alert(scope.getSingleMsg('S0012'));
                    //----------------------------------
                    var getData = T74129Service.getGridData();
                    getData.then(function (data) {
                        var cou = 0;
                        scope.newDataList = [];
                        for (var i = 0; i < data.length; i++) {
                            scope.newObject = {};
                            scope.newObject.T_FIRST_LANG2_NAME = data[i].T_FIRST_LANG2_NAME;
                            scope.newObject.T_PROB_DET_ID = data[i].T_PROB_DET_ID;
                            scope.newObject.T_PROB_DELT_LANG2 = data[i].T_PROB_DELT_LANG2;
                            scope.newObject.T_REPAIR_INST_DELT = data[i].T_REPAIR_INST_DELT;
                            var apDd = new Date(data[i].T_APR_DELV_DATE.match(/\d+/)[0] * 1);
                            scope.newObject.T_APR_DELV_DATE = $filter('date')(apDd, "dd-MM-yyyy");

                            var entD = new Date(data[i].T_ENTRY_DATE.match(/\d+/)[0] * 1);
                            scope.newObject.T_ENTRY_DATE = $filter('date')(entD, "dd-MM-yyyy");
                            scope.newObject.T_REPAIR_STATUS = data[i].T_REPAIR_STATUS;
                            if (data[i].T_REPAIR_STATUS !== 'Done') {
                                cou = cou + 1;
                            }
                            scope.newDataList.push(scope.newObject);
                        }
                        scope.obj.DataList = scope.newDataList;
                        scope.obj.Count = cou;
                    });
                    //-------------------------------------
                    
                });
            }

            scope.Select = function (sect) {
                scope.List = [];
                var search = T74129Service.getSearchData(sect);
                search.then(function (data) {
                  
                    //for (var i = 0; i < data.length; i++) {
                    //    scope.newlist = {};
                    //    scope.newlist.T_PROB_DELT_LANG2 = data[i].T_PROB_DELT_LANG2;
                    //    scope.List.push(scope.newlist);
                    //}
                    scope.dfsafds = data;
                    
                });
            }
            // ----------------- for getting current location start-------------
            setInterval(function () {
              //  window.location.reload();
              //  alert('ok');
                getLocation();
            }, 5000); 
           // alert('ok');
            var x = document.getElementById("demo");
            

            function getLocation () {
                if (navigator.geolocation) {
                    navigator.geolocation.getCurrentPosition(showPosition);
                    
                } else {
                    x.innerHTML = "Geolocation is not supported by this browser.";
                }
               
            }

            function showPosition(position) {
                //x.innerHTML = "Latitude: " + position.coords.latitude +
                //    "<br>Longitude: " + position.coords.longitude;
                //var latitude = position.coords.latitude;
                //var longitude = position.coords.longitude;
                scope.lang = position.coords.latitude;
                scope.long = position.coords.longitude;
               // alert('ok');
            }
            // ----------------- for getting current location end -------------

        }
]);
