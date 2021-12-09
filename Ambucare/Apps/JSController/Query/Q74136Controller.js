app.controller("Q74136Controller", ["$scope", "$filter", "$http", "$window", "Q74136Service", "LabelService", "Data", "$interval",
  function ($scope, $filter, $http, $window, Q74136Service, LabelService, Data, $interval) {

        GetNotificationData();
        $scope.obj = {};
      $scope.obj.k = {};
      $scope.obj.NotificationDashboard = [];

        $scope.obj.k.selected = { T_EVENT_ID: 0, T_LANG2_NAME: 'Select' }
        // Form Label Data Service Start 
        var labelData = LabelService.getlabeldata('Q74136');
        labelData.then(function (data) {
            $scope.entity = data;
      });

      
        $scope.show = false;
            setInterval(function () {
                GetNotificationData();
                $scope.show = !$scope.show;
                $scope.obj.k.selected = { T_EVENT_ID: 0, T_LANG2_NAME: 'Select' };
                
                
                //showHideTable();
            }, 10000); //60000 milisecond == 1 minute


        //$scope.showMe = true;
        //$scope.hideHandover = true;
        //$scope.HADS = true;



        function GetNotificationData() {
            var info = Q74136Service.GetNotificationData();
            info.then(function (data) {
                $scope.obj.NotificationDashboard = JSON.parse(data);
                
                //$scope.$apply();
            });

        }

        $scope.Hospital_Click = function (ind) {
            var requst = $scope.obj.NotificationDashboard[ind].T_REQUEST_ID;
            var discharge = $scope.obj.NotificationDashboard[ind].HANDOVER_DATE;
            var cancelDate = $scope.obj.NotificationDashboard[ind].T_CAN_DATE;
            var panic = $scope.obj.NotificationDashboard[ind].T_PANIC_ID;
            if (discharge === null && cancelDate === null && panic===null) {
              $window.location.href = $scope.vrDir+'/Transaction/T74042?requst =' + requst;
            } else {
                if (discharge !== null) {
                    //alert('Patient already has been discharged');
                    alert($scope.getSingleMsg('S0041'));
                } else if (cancelDate !== null){
                    alert('Patient already has been Canceled');
                } 
                

            }


        };

        function showHideTable() {
            if ($scope.HADS == true) {
                $scope.hideHandover = false;
                $scope.HADS = false;
            } else if ($scope.hideHandover == false) {
                $scope.HADS = true;
                $scope.hideHandover = true;
            }
        }




        var criteriaList = Q74136Service.GetCriterias();
        criteriaList.then(function (data) {
            $scope.criterias = JSON.parse(data);
        });
        $scope.onEventSelect = function (e) {
            //alert(e);
            var arr = [];
            if (e == 1) { arr = $scope.obj.NotificationDashboard.filter(function (a) { return a.T_REQUEST_DATE != null; }); }
            else if (e == 2) { arr = $scope.obj.NotificationDashboard.filter(function (a) { return a.ASIGN_DATE != null; }); }
            else if (e == 3) { arr = $scope.obj.NotificationDashboard.filter(function (a) { return a.T_OPER_CNCL_FLAG!=null; }); }
            else if (e == 4) { arr = $scope.obj.NotificationDashboard.filter(function (a) { return a.ACCEPT_DATE != null; }); }
            else if (e == 5) { arr = $scope.obj.NotificationDashboard.filter(function (a) { return a.T_CANCEL_STATUS == 1 && a.T_OPER_CNCL_FLAG == null; }); }
            else if (e == 6) { arr = $scope.obj.NotificationDashboard.filter(function (a) { return a.CASE_RECEIVED_DATE != null; }); }
            else if (e == 7) { arr = $scope.obj.NotificationDashboard.filter(function (a) { return a.DIST_START_DATE != null; }); }
            else if (e == 8) { arr = $scope.obj.NotificationDashboard.filter(function (a) { return a.T_REF_DOC_CODE != null; }); }
            else if (e == 9) { arr = $scope.obj.NotificationDashboard.filter(function (a) { return a.T_DOC_REQ_ACPT_DATE !=null; }); }
            else if (e == 10) { arr = $scope.obj.NotificationDashboard.filter(function (a) { return a.T_OPER_DES_CODE != null; }); }
            else if (e == 11) { arr = $scope.obj.NotificationDashboard.filter(function (a) { return a.T_OPER_ACPT_DATE !=null; }); }
            else if (e == 12) { arr = $scope.obj.NotificationDashboard.filter(function (a) { return a.HOSPITAL_ARRIVAL != null; }); }
            else if (e == 13) { arr = $scope.obj.NotificationDashboard.filter(function (a) { return a.T_DISCH_STATUS == 1; }); }
            else if (e == 14) { arr = $scope.obj.NotificationDashboard.filter(function (a) { return a.HOSPITAL_DEPARTURE != null; }); }
            else if (e == 15) { arr = $scope.obj.NotificationDashboard.filter(function (a) { return a.HOSPITAL_STATION != null; }); }
            else if (e == 16) { arr = $scope.obj.NotificationDashboard.filter(function (a) { return a.T_PANIC_ID !=null; }); }
            //else { arr = $scope.obj.NotificationDashboard; }
            $scope.obj.NotificationDashboard = arr;

        }

      function check() {
          elements = document.getElementsByClassName('green');
          for (var i = 0; i < elements.length; i++) {
              elements[i].style.backgroundColor = "blue";
          }
      }


      $scope.swap_color = function() {
          swap();
      }
    }
]);

