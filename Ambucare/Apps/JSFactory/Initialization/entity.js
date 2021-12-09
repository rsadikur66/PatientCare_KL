"use strict";
(function () {
   app.factory("entityService", ["akFileUploaderService", function (akFileUploaderService) {
               var saveTutorial = function (tutorial) {
                   return akFileUploaderService.saveModel(tutorial, "/home/saveTutorial");
               };
               return {
                   saveTutorial: saveTutorial
               };
           }]);
})();