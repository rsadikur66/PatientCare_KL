app.controller("T74052Controller", ["$scope", "$filter", "$http", "$window", "T74049Service",
    function ($scope, $filter, $http, $window, T74049Service) {
        //scope.religion = {};
        //var Religion = getReligion();

        //Religion.then(function (results) {
        //    scope.ReligionList = results;
        //});

        $scope.Save_Click = function () {

            var R = {
                T_RLGN_CODE: $scope.religion.ReligionCode,
                T_LANG1_NAME: $scope.religion.EnglishName,
                T_LANG2_NAME: $scope.religion.LocalName

            }
            T740549Service.saveData(R).then(function (result) {
                alert(result.data);
                //getReligion();
            });

        }
    }
]);