
app.controller("T74123Controller", ["$scope", "$compile", "$filter", "$http", "$window","$location", "T74123Service", "Data", 
    function (scope, $compile, $filter, $http, $window, $location, T74123Service, Data ) {
        //for instance
        scope.obj = {};
        scope.obj = Data;

        scope.NewObject = [];
        var patientList = T74123Service.getPatients();
        patientList.then(function (data) {
            scope.obj.Count = data.length;
            for (var i = 0; i < data.length; i++) {
                scope.obj.List = {};
                scope.obj.List.Name = data[i].Name;
                scope.obj.List.Mobile = data[i].Mobile;
                scope.obj.List.Problem = data[i].Problem;
                scope.obj.List.PatentId = data[i].PatentId;
                scope.obj.List.RequestId = data[i].RequestId;
                var toDay = $filter('date')(new Date(), 'yyyy,MM,dd');
               // var bD = data[i].BirthDate;
                var bD = new Date(data[i].BirthDate.match(/\d+/)[0] * 1);
                var birthDate = $filter('date')(bD, 'yyyy,MM,dd');
                scope.obj.List.Age = ageCalculet(new Date(birthDate), new Date(toDay)); // 'ageCalculet' is a function
                scope.NewObject.push(scope.obj.List);

            }

            scope.obj.PatList = scope.NewObject;
        });
        //------------------------------
        function ageCalculet(toDay, warEndDate) {
            //convert to UTC
            var date2_UTC = new Date(Date.UTC(warEndDate.getUTCFullYear(), warEndDate.getUTCMonth(), warEndDate.getUTCDate()));
            var date1_UTC = new Date(Date.UTC(toDay.getUTCFullYear(), toDay.getUTCMonth(), toDay.getUTCDate()));


            var yAppendix, mAppendix, dAppendix;


            //--------------------------------------------------------------
            var days = date2_UTC.getDate() - date1_UTC.getDate();
            if (days < 0) {

                date2_UTC.setMonth(date2_UTC.getMonth() - 1);
                days += DaysInMonth(date2_UTC);
            }
            //--------------------------------------------------------------
            var months = date2_UTC.getMonth() - date1_UTC.getMonth();
            if (months < 0) {
                date2_UTC.setFullYear(date2_UTC.getFullYear() - 1);
                months += 12;
            }
            //--------------------------------------------------------------
            var years = date2_UTC.getFullYear() - date1_UTC.getFullYear();


            if (years > 1) yAppendix = " Y";
            else yAppendix = " Y";
            if (months > 1) mAppendix = " M";
            else mAppendix = " M";
            if (days > 1) dAppendix = " D";
            else dAppendix = " D";


            return years + yAppendix + ", " + months + mAppendix + ", " + days + dAppendix;
        }
        function DaysInMonth(date2_UTC) {
            var monthStart = new Date(date2_UTC.getFullYear(), date2_UTC.getMonth(), 1);
            var monthEnd = new Date(date2_UTC.getFullYear(), date2_UTC.getMonth() + 1, 1);
            var monthLength = (monthEnd - monthStart) / (1000 * 60 * 60 * 24);
            return monthLength;
        }
        //-------------------------------
        scope.setClickedRow = function (pindex, chin, ob) {
                    //for (var j = 0; j < scope.obj.UomList.length; j++) {
                    //        scope.obj.newObj = {};
                    //        scope.obj.newObj.SALE_PRICE = scope.obj.UomList[j].SALE_PRICE;
                    //}
                    //scope.obj.UomList.push(scope.obj.newObj);
                
           // document.getElementById(id).style.display = "none";
        }
        scope.Report_Click = function (id, patienId) {

            var getDetails = T74123Service.patientDetalisData(patienId);
            getDetails.then(function (data) {
                scope.obj.Name = data[0].Name;
                scope.obj.Mobile = data[0].Mobile;
                scope.obj.Problem = data[0].Problem;
                scope.obj.PatentId = data[0].PatentId;
                scope.obj.Ambulance = data[0].Ambulance;
               // scope.obj.RequestId = data[0].RequestId;
                
                var toDay = $filter('date')(new Date(), 'yyyy,MM,dd');
                // var bD = data[i].BirthDate;
                var bD = new Date(data[0].BirthDate.match(/\d+/)[0] * 1);
                var birthDate = $filter('date')(bD, 'yyyy,MM,dd');
                scope.obj.Age = ageCalculet(new Date(birthDate), new Date(toDay)); // 'ageCalculet' is a function
               
                
                
            document.getElementById(id).style.display = "block";
            });
            var priscription = T74123Service.priscriptionData(patienId);
            priscription.then(function (data) {
                scope.obj.PriscriptionList = [];
                scope.obj.PriscriptionList = data;
            });
            scope.obj.newBodyMeasurmentObject = [];
            var bodyMeasurement = T74123Service.bodyMeasurementData(patienId);
            bodyMeasurement.then(function (data) {
                for (var i = 0; i < data.length; i++) {
                    scope.NewObject = {};
                    scope.NewObject.T_TEMP = data[i].T_TEMP;
                    scope.NewObject.T_PULS = data[i].T_PULS;
                    scope.NewObject.T_WEIGHT = data[i].T_WEIGHT;
                    scope.NewObject.T_HIGHT = data[i].T_HIGHT;
                    scope.NewObject.T_BP_SYS = data[i].T_BP_SYS;
                    scope.NewObject.T_BP_DIA = data[i].T_BP_DIA;
                    scope.NewObject.T_BSUGAR_F = data[i].T_BSUGAR_F;
                   // var da = data[i].T_ENTRY_DATE;
                    var bD = new Date(data[i].T_ENTRY_DATE.match(/\d+/)[0] * 1);
                    var dat = $filter('date')(bD, 'dd-MMM-yyyy');
                    scope.NewObject.T_ENTRY_DATE = dat;
                    scope.obj.newBodyMeasurmentObject.push(scope.NewObject );
                }
                scope.obj.MeasurmentList = [];
                scope.obj.MeasurmentList = scope.obj.newBodyMeasurmentObject;
            });
        }
        scope.CloseReceivePopup = function(id) {
            document.getElementById(id).style.display = "none";
        }

        scope.$watch('selectedRow', function () {
            console.log('Do Some processing'); //runs the block whenever selectedRow is changed. 
        });
        //----------search --------
        //this.startsWithW = function (item) {
        //    return /w/i.test(item.name.substring(0, 1));
        //};
        scope.ChatRoom = function (reqid) {
            sessionStorage.setItem("FCode", JSON.stringify('T74131'));
            sessionStorage.setItem("FName", JSON.stringify('Chat Room'));
            $window.location = "/Transaction/T74131";

        }

        scope.Print_Click = function (requeId) {
            $window.open("../R74123/R74123Report?T_REQUEST_ID=" + requeId, "popup", "width= 600, height = 600, left = 100, top = 50");
            //$window.open("../R74046/R74046Report?T_REQUEST_ID=" + requeId, "popup", "width=600,height=600,left=100,top=50");
        }

    }]);

app.directive('arrowSelector', ['$document', function ($document) {
    return {
        restrict: 'A',
        link: function (scope, elem, attrs, ctrl) {
            var elemFocus = false;
            elem.on('mouseenter', function () {
                elemFocus = true;
            });
            elem.on('mouseleave', function () {
                elemFocus = false;
            });
            $document.bind('keydown', function (e) {
                if (elemFocus) {
                    if (e.keyCode == 38) {
                        console.log(scope.selectedRow);
                        if (scope.selectedRow == 0) {
                            return;
                        }
                        scope.selectedRow--;
                        scope.$apply();
                        e.preventDefault();
                    }
                    if (e.keyCode == 40) {
                        if (scope.selectedRow == scope.foodItems.length - 1) {
                            return;
                        }
                        scope.selectedRow++;
                        scope.$apply();
                        e.preventDefault();
                    }
                }
            });
        }
    };
}]);
//------for search start ----------


app.filter('cartypefilter', function () {
    return function (items, search) {
        if (!search) {
            return items;
        }
        var filtered = [];
        var letterMatch = new RegExp(search, 'i');


        for (var i = 0; i < items.length; i++) {
            var item = items[i];
            if (letterMatch.test(item.Name.substring(0, 5)) || letterMatch.test(item.Mobile.substring(0, 5))) {
               // return items;
                filtered.push(item);
            }
        }
        return filtered;
        

    };
});