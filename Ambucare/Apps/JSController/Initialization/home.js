"use strict";
(function () {
    app.controller("homeCtrl", ["$scope", "entityService",
               function ($scope, entityService) {
                   $scope.saveTutorial = function (tutorial) {
                       entityService.saveTutorial(tutorial)
                                    .then(function (data) {
                                        console.log(data);
                                    });
                   };
                   $scope.stepsModel = [];
                   $scope.imageUpload = function (event) {
                       var files = event.target.files; //FileList object         
                       for (var i = 0; i < files.length; i++) {
                           var file = files[0];
                           var reader = new FileReader();
                           reader.onload = $scope.imageIsLoaded;
                           reader.readAsDataURL(file);
                       };
                   }
                   $scope.imageIsLoaded = function (e) {
                       $scope.$apply(function () {
                           $scope.stepsModel.push(e.target.result);
                       });
                   }
               }]);
})();