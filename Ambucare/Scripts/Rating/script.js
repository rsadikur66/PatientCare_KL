// Code goes here

angular.module('myApp', ['ngMaterial', 'jkAngularRatingStars'])

.controller('MyCtrl', function($scope) {
    $scope.firstRate = 0;
    $scope.secondRate = 3;
    $scope.readOnly = true;
    $scope.onItemRating = function(rating){
      alert('On Rating: ' + rating);
    };
});