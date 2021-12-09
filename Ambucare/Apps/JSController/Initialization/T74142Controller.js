app.controller("T74142Controller", ["$scope", "Data", "$rootScope",
    function ($scope, Data, $rootScope) {
        //For Instance
        $scope.obj = {};
        $scope.obj = Data;
        //----------Tab Toggling--------------Start
        $scope.tabs = [
          { title: 'Message', url: $scope.vrDir+'/PartPages/T74146.html', hdn: 'G' },
          { title: 'Label', url: $scope.vrDir+'/PartPages/T74000.html', hdn: 'M' },
          { title: 'Cancel Reason', url: $scope.vrDir+'/PartPages/T74144.html', hdn: 'N' },
          { title: 'Discharge Reason', url: $scope.vrDir+'/PartPages/T74145.html', hdn: 'R' },
        ];
      $scope.currentTab = $scope.vrDir+'/PartPages/T74146.html';
        $scope.obj.hiddenField = 'G';
        $scope.onClickTab = function(tab) {

            $scope.obj.hiddenField = tab.hdn;
            $scope.currentTab = tab.url;

            
        };
        $scope.isActiveTab = function (tabUrl) { return tabUrl === $scope.currentTab; };
        $scope.Save_Click = function () {
            if ($scope.obj.hiddenField === 'G') {
                $rootScope.$emit('T74146Emit', 'g');
            } else if ($scope.obj.hiddenField === 'M') {
                $rootScope.$emit('T74000Emit', 'm');
            } else if ($scope.obj.hiddenField === 'N') {
                $rootScope.$emit('T74144Emit', 'n');
            } else if ($scope.obj.hiddenField === 'R') {
                $rootScope.$emit('T74145Emit', 'r');
            }
        };

        //----------Tab Toggling--------------End
        $scope.Clear = function() {
            if ($scope.obj.hiddenField === 'G') {
                $rootScope.$emit('T74146Emit', 'Clear');
            } else if ($scope.obj.hiddenField === 'M') {
                $rootScope.$emit('T74000Emit', 'Clear');
            } else if ($scope.obj.hiddenField === 'N') {
                $rootScope.$emit('T74144Emit', 'Clear');
            } else if ($scope.obj.hiddenField === 'R') {
                $rootScope.$emit('T74145Emit', 'Clear');
            }
        }

    }]);





