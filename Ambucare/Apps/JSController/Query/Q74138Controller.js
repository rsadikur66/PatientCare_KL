app.controller("Q74138Controller", ["$scope", "$filter", "$http", "$window", "LabelService", "Q74138Service", "uiGridConstants", "DTOptionsBuilder", "DTColumnBuilder", "Data",
    function ($scope, $filter, $http, $window, LabelService, Q74138Service, uiGridConstants, DTOptionsBuilder, DTColumnBuilder, Data) {

        
        $scope.obj = {};
        $scope.obj.T_LATITUDE = '';
        $scope.obj.T_LONGITUDE = '';
        $scope.obj.Address = '';
        var labelData = LabelService.getlabeldata('Q74138');
        labelData.then(function (data) {
            $scope.entity = data;
        });
        getAllData();
        //setInterval(function () {
        //    getAllData();
        //}, 10000); //60000 milisecond == 1 minute
        function getAllData() {
            var info = Q74138Service.getMapload();
            info.then(function (data) {
                $scope.obj.getAllGridData = [];
                var dt = JSON.parse(data);
                $scope.obj.T_LATITUDE = dt[0].T_LATITUDE;
                $scope.obj.T_LONGITUDE = dt[0].T_LONGITUDE;
                for (var i = 0; i < dt.length; i++) {
                    var t = {};
                    t.T_LATITUDE = dt[i].T_LATITUDE;
                    t.T_LONGITUDE = dt[i].T_LONGITUDE;
                    t.T_USER_ID = dt[i].T_USER_ID;
                    t.T_ENTRY_TIME = dt[i].T_ENTRY_TIME;
                    t.Address = getAddress(t.T_LATITUDE, t.T_LONGITUDE);
                    $scope.obj.getAllGridData.push(t);
                }
                //$scope.obj.getAllGridData = JSON.parse(data);

                //var latlng = new google.maps.LatLng($scope.obj.T_LATITUDE, $scope.obj.T_LONGITUDE);
                //var geocoder = geocoder = new google.maps.Geocoder();
                //geocoder.geocode({ 'latLng': latlng }, function (results, status) {
                //    if (status == google.maps.GeocoderStatus.OK) {
                //        if (results[0]) {
                //            $scope.obj.Address = results[0].formatted_address;
                //            // $scope.obj.t74041.T_MAP_LOC = results[1].formatted_;
                //            // alert("Location: " + results[1].formatted_address);
                //        }
                //    }
                //});
                $scope.$apply();
            });

        }

        //var m = Q74138Service.getMapload();
        //m.then(function(d) {
        //    var dtt = JSON.parse(d);
        //    $scope.obj.T_LATITUDE = dtt[0].T_LATITUDE;
        //    $scope.obj.T_LONGITUDE = dtt[0].T_LONGITUDE;
        //});
      //  ------------------------------
        //For Grid Function Start 
        $scope.someClickHandler = someClickHandler;
        $scope.dtColumns = [
            //here We will add .withOption('name','column_name') for send column name to the server 
            DTColumnBuilder.newColumn("T_USER_ID", "User ID").withOption('name', 'T_USER_ID'),
           // DTColumnBuilder.newColumn("T_ENTRY_TIME", "Entry Date").withOption('name', 'T_ENTRY_TIME'),
            DTColumnBuilder.newColumn("T_ENTRY_TIME", "Entry Date").withOption('name', 'T_ENTRY_TIME').renderWith(function (data, type) {
                var dt = data.replace("/Date(", "").replace(")/", "");
                return $filter('date')(dt, 'dd/MM/yyyy');
            }), //date filter,
            //DTColumnBuilder.newColumn("T_ENTRY_TIME", "Entry Time").withOption('name', 'T_ENTRY_TIME'),
            DTColumnBuilder.newColumn("T_ENTRY_TIME", "Entry Date").withOption('name', 'T_ENTRY_TIME').renderWith(function (data, type) {
                var dt = data.replace("/Date(", "").replace(")/", "");
                return $filter('date')(dt, 'hh:mm:ss');
            }), //date filter,
            DTColumnBuilder.newColumn("T_LATITUDE", "Lat").withOption('name', 'T_LATITUDE').notVisible(),
            DTColumnBuilder.newColumn("T_LONGITUDE", "Lon").withOption('name', 'T_LONGITUDE').notVisible()
        ];
        $scope.dtOptions = DTOptionsBuilder.newOptions().withOption('ajax', {
                dataSrc: "data",
            url: "/Q74138/getAllGridData",
                type: "POST"
            })
            .withOption('rowCallback', rowCallback)
            .withOption('processing', true) //for show progress bar
            .withOption('serverSide', true) // for server side processing
            .withPaginationType('full_numbers') // for get full pagination options // first / last / prev / next and page numbers
            .withDisplayLength(5) // Page size
            .withOption('aaSorting', [0, 'asc']) // for default sorting column // here 0 means first column
        function someClickHandler(info) {
            $scope.obj.T_LATITUDE = info.T_LATITUDE;
            $scope.obj.T_LONGITUDE = info.T_LONGITUDE;
            
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

        function ddlText(e) {
            var a = $scope.cancelType.filter(function (a) { return a.CancelType == e; });
            return a[0].CancelText;
        }
        //For Grid Function End 


      //  ------------------------------






        function getAddress(lat, lng) {
            var a = '';
            var latlng = new google.maps.LatLng(lat, lng);
            var geocoder = new google.maps.Geocoder();
            geocoder.geocode({ 'latLng': latlng }, function (results, status) {
                if (status === google.maps.GeocoderStatus.OK) {
                    if (results[0]) {
                        $scope.obj.Address = results[0].formatted_address;
                        // $scope.obj.t74041.T_MAP_LOC = results[1].formatted_;
                        // alert("Location: " + results[1].formatted_address);
                    }
                }
            });
            return a;
        }

        $scope.showAddress = function () {
            var a = '';
            var latlng = new google.maps.LatLng($scope.obj.T_LATITUDE, $scope.obj.T_LONGITUDE);
            var geocoder = new google.maps.Geocoder();
            geocoder.geocode({ 'latLng': latlng }, function (results, status) {
                if (status === google.maps.GeocoderStatus.OK) {
                    if (results[0]) {
                        $scope.obj.Address = 'Address: ' + results[0].formatted_address;
                        
                        //alert(a);
                        // $scope.obj.t74041.T_MAP_LOC = results[1].formatted_;
                        // alert("Location: " + results[1].formatted_address);
                    }
                }
            });
            $scope.map.showInfoWindow('myInfoWindow', this);
        };

        $scope.hideAddress = function () {
            var a = '';
            var latlng = new google.maps.LatLng($scope.obj.T_LATITUDE, $scope.obj.T_LONGITUDE);
            var geocoder = new google.maps.Geocoder();
            geocoder.geocode({ 'latLng': latlng }, function (results, status) {
                if (status === google.maps.GeocoderStatus.OK) {
                    if (results[0]) {
                        $scope.obj.Address = results[0].formatted_address;

                        //alert(a);
                        // $scope.obj.t74041.T_MAP_LOC = results[1].formatted_;
                        // alert("Location: " + results[1].formatted_address);
                    }
                }
            });
            $scope.map.hideInfoWindow('myInfoWindow', this);
        };
        

        $scope.setClickedRow = function ($index, D) {
            var a = '';
            $scope.obj.T_LATITUDE = D.T_LATITUDE;
            $scope.obj.T_LONGITUDE = D.T_LONGITUDE;
            $scope.hideAddress();
            //var latlng = new google.maps.LatLng($scope.obj.T_LATITUDE, $scope.obj.T_LONGITUDE);
            //var geocoder = new google.maps.Geocoder();
            //geocoder.geocode({ 'latLng': latlng }, function (results, status) {
            //    if (status === google.maps.GeocoderStatus.OK) {
            //        if (results[0]) {
            //            $scope.obj.Address = results[0].formatted_address;
            //            a = $scope.obj.Address;
            //            //alert(a);
            //            // $scope.obj.t74041.T_MAP_LOC = results[1].formatted_;
            //            // alert("Location: " + results[1].formatted_address);
            //        }
            //    }
            //});
        };
    }]);