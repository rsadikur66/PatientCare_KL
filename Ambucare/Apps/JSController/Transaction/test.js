
app.controller("test", ["$scope", "$filter", "$http", "$window","Data", 
    function ($scope, $filter, $http, $window, Data) {
        $scope.obj = {};
        $scope.obj = Data;
        $scope.OpenDiv = function (id) {
              document.getElementById(id).style.display = "block";
        }
        $scope.CloseDiv = function (id) {
            document.getElementById(id).style.display = "none";
        }
     }]);