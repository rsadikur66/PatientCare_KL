
app.controller("Q74144Controller", ["$scope", "$filter", "$http", "$window", "Q74144Service", "Data", "$interval",
    function (scope, $filter, $http, $window, Q74144Service, Data, $interval) {

        //For Instance
        scope.obj = {};
        scope.obj = Data;
        var allData = Q74144Service.getAllTeamData();
        allData.then(function (data) {
            scope.obj.AllTeamData = JSON.parse(data);
        });
        //----------------for label start -------------
        var labelData = Q74144Service.getlabeldata('Q74144');
        labelData.then(function (data) {
            scope.entity = data;
        });
        //----------------for label end -------------
        setInterval(function () {
            
            var allData = Q74144Service.getAllTeamData();
            allData.then(function (data) {
                scope.obj.AllTeamData = JSON.parse(data);
            });
        }, 5000);

        scope.getPatMsg = function (e) {
            scope.obj.patMsg = [];
            var patMsg = Q74144Service.getPatMsg(e);
            patMsg.then(function (data) {
                var dt = JSON.parse(data);
                if (dt.length > 0) {
                    scope.obj.patMsg = dt;
                    document.getElementById("ShowPatientMsgPopup").style.display = "block";
                }
                
            });
        }
        scope.CloseActivePopup = function () {
            scope.obj.patMsg = [];
            document.getElementById("ShowPatientMsgPopup").style.display = "none";
        }
        
           

    }])