app.controller("T74147Controller", ["$scope", "Data", "$rootScope",
    function ($scope, Data, $rootScope) {
        //For Instance
        $scope.obj = {};
        $scope.obj = Data;
        //----------Tab Toggling--------------Start
        $scope.tabs = [
          { title: 'Chief Complient', url: $scope.vrDir+'/PartPages/T74055.html', hdn: 'G' },
          { title: 'Problem Type', url: $scope.vrDir+'/PartPages/T74148.html', hdn: 'M' },
          { title: 'ICD-10', url: $scope.vrDir+'/PartPages/T74149.html', hdn: 'N' }
        ];
      $scope.currentTab = $scope.vrDir+'/PartPages/T74055.html';
        $scope.obj.hiddenField = 'G';
        $scope.onClickTab = function (tab) {

            $scope.obj.hiddenField = tab.hdn;
            $scope.currentTab = tab.url;


        };
        $scope.isActiveTab = function (tabUrl) { return tabUrl === $scope.currentTab; };
        $scope.Save_Click = function () {
            if ($scope.obj.hiddenField === 'G') {
                $rootScope.$emit('T74055Emit', 'g');
            } else if ($scope.obj.hiddenField === 'M') {
                $rootScope.$emit('T74148Emit', 'm');
            } else if ($scope.obj.hiddenField === 'N') {
                $rootScope.$emit('T74149Emit', 'n');
            } 
        };


        $scope.Clear = function() {
            if ($scope.obj.hiddenField === 'G') {
                $rootScope.$emit('ClearEmit', 'c');
            }
            else if ($scope.obj.hiddenField === 'M') {
                $rootScope.$emit('T74148ClearEmit', 'd');
            }
            else if ($scope.obj.hiddenField === 'N') {
                $rootScope.$emit('T74149ClearEmit', 'e');
            }
        }
       

        //----------Tab Toggling--------------End


    }]);





