
app.controller("Q74137Controller", ["$scope", "$filter", "$http", "$window", "Q74137Service","LabelService",  "Data", "$interval",
    function ($scope, $filter, $http, $window, Q74137Service, LabelService, Data, $interval) {

           // GetNotificationData();
            $scope.obj = {};
            getPendingData();


            setInterval(function () {
                getPendingData();
            }, 5000); //60000 second == 1 minute

            function getPendingData() {
                var pendingReq = Q74137Service.getPendingRequestData();
                pendingReq.then(function (data) {
                    var jsonData = JSON.parse(data);
                    $scope.obj.PendingRequestList = jsonData;
                });
            }

            var labelData = LabelService.getlabeldata('Q74137');
            labelData.then(function (data) {
                $scope.entity = data;
            });
            

        }
    ]);