app.controller("Q74001Controller", ["$scope", "$filter", "$http", "$window","Q74001Service", "Data","$interval",
    function (scope, $filter, $http, $window, Q74001Service, Data, $interval) {

        //For Instance
        scope.obj = {};
        scope.obj = Data;
        scope.currDate = new Date();
        $interval(function () { scope.currTime = Date.now(); }, 1000);

       
        var UserId = Q74001Service.GetUserIDByAMBRegID();
        UserId.then(function (data) {
            scope.img = JSON.parse(data);
            //scope.obj.q74001.T_USER_ID = JSON.parse(data)[0].T_USER_ID;
        });

        // For Active Ambulance Start
        var activeAmbu = Q74001Service.getActiveAmbulance();
        activeAmbu.then(function(data) {
            scope.obj.ActiveAbulanceCount = data.length;
        });

        scope.AmbulanceIn_Click = function(id) {
            var activeAmbu = Q74001Service.getActiveAmbulance();
            activeAmbu.then(function(data) {
                scope.Popup = "Active";
                scope.obj.Abulance = data;
            });
            document.getElementById(id).style.display = "block";
        };
        scope.CloseAmbulanceInPopup = function(id) {
            scope.Search = "";
            document.getElementById(id).style.display = "none";
        };
        // For Active Ambulance End

        // For Discharge Ambulance Start
        var dischargeAmbu = Q74001Service.getDischargeAmbulance();
        dischargeAmbu.then(function (data) {
         
            scope.obj.DischargeAbulanceCount = data.length;
        });
        scope.AmbulanceOut_Click = function(id) {
            var dischargeAmbu = Q74001Service.getDischargeAmbulance();
            dischargeAmbu.then(function(data) {
                scope.Popup = "Discharge";
                scope.obj.Abulance = data;
            });
            document.getElementById(id).style.display = "block";
        };

        scope.CloseAmbulanceOutPopup = function (id) {
            scope.Search = "";
            document.getElementById(id).style.display = "none";
        };

        // For Discharge Ambulance End

        // For All Patients Start
        var allPatients = Q74001Service.getallPatients();
        allPatients.then(function (data) {
            scope.obj.AllPatientsCount = data.length;
        });
        scope.AmbulanceInformation_Click = function(id) {
            var allPatients = Q74001Service.getallPatients();
            allPatients.then(function(data) {
                scope.Popup = "AllPatients";
                scope.obj.Abulance = data;
            });
            document.getElementById(id).style.display = "block";
        };
        scope.CloseAmbulanceTotalPopup = function (id) {
            scope.Search = "";
            document.getElementById(id).style.display = "none";
        };

        // For All Patients End

        // For Waitting Ambulance Start
        var waitingAmbulance = Q74001Service.getwaitingAmbulance();
        waitingAmbulance.then(function (data) {
            scope.obj.WaitingAmbulanceCount = data.length;
        });

        scope.WaittingAmbu_Click = function(id) {
            var waitingAmbulance = Q74001Service.getwaitingAmbulance();
            waitingAmbulance.then(function(data) {
                scope.obj.WaitingAmbulance = data;
            });
            document.getElementById(id).style.display = "block";
        };
        scope.CloseWaittingAmbuPopup = function(id) {

            document.getElementById(id).style.display = "none";
        };
        // For Waitting Ambulance End

        var maleAmbulance = Q74001Service.getMaleAmbulance();
        maleAmbulance.then(function (data) {
            scope.obj.MaleAmbulanceCount = data.length;
            scope.obj.MaleAmbulance = data;
        });
        var femalAmbulance = Q74001Service.getfemalAmbulance();
        femalAmbulance.then(function (data) {
            scope.obj.FemalAmbulanceCount = data.length;
            scope.obj.FemalAmbulance = data;
        });

        scope.Print_Click = function(popup) {
            // var con = "where T74041.T_DISCH_STATUS = 1";
            if (popup === "Discharge") {
                $window.open("../Q74001/R74001Report?T_FIRST_LANG2_NAME= " + "where T74041.T_DISCH_STATUS = 1",
                    "popup",
                    "width= 600, height = 600, left = 100, top = 50");
            } else if (popup === "Active") {
                $window.open("../Q74001/R74001Report?T_FIRST_LANG2_NAME= " + "where T74041.T_DISCH_STATUS IS NULL",
                    "popup",
                    "width= 600, height = 600, left = 100, top = 50");
            } else {
                $window.open("../Q74001/R74001Report? popup", "width= 600, height = 600, left = 100, top = 50");
            }
            //$window.open("../R74046/R74046Report?T_REQUEST_ID=" + requeId, "popup", "width=600,height=600,left=100,top=50");
        };

        scope.Print_WaiAmbu_Click = function() {
            $window.open("../Q74001/R74001ReportWaittingAmbulance? popup",
                "width= 600, height = 600, left = 100, top = 50");
        };
        scope.PatientDetails_Click = function(ambuId) {
            // alert(ambuId);
            $window.open("../Q74001/R74001ReportPatientDetails?T_AMBU_REG_ID=" + ambuId,
                " popup",
                "width= 600, height = 600, left = 100, top = 50");
        };
        //$http({
        //    method: 'POST',
        //    url: '/T04203/GetBed',
        //    data: JSON.stringify({ T_ER_LOCATION: $scope.ctrl.l.selected.T_LOC_CODE, T_BED_GENDER: $scope.GENDER })
        //})

    }
]);