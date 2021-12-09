app.controller("T74135Controller", ["$scope", "Data","$rootScope",
    function ($scope, Data, $rootScope) {
        //For Instance
        $scope.obj = {};
        $scope.obj = Data;

        $scope.tabs = [
          { title: 'User Registration', url: $scope.vrDir+'/PartPages/T74004.html', hin: 'REG' },
          { title: 'User Authentication', url: $scope.vrDir+ '/PartPages/T74136.html', hin: 'AUT' }
            //{ title: 'Role Creation', url: '/PartPages/T74066.html', hin: 'ROL' },
            //{ title: 'Page Distribution', url: '/PartPages/T74132.html', hin: 'FOR' }

        ];
      $scope.currentTab = $scope.vrDir+'/PartPages/T74004.html';
        $scope.obj.Hidenfield = 'REG';
        $scope.onClickTab = function(tab) {

            $scope.obj.Hidenfield = tab.hin;
            $scope.currentTab = tab.url;
            //if (tab.hin == 'BILL') {


            //} else if (tab.hin === 'DIA') {

            //}

            
        };
        $scope.isActiveTab = function(tabUrl) { return tabUrl === $scope.currentTab; };
        $scope.onSave = function() {
            if ($scope.obj.Hidenfield === 'REG') {
                $rootScope.$emit('T74004Emit', 'reg');
            } else if ($scope.obj.Hidenfield === 'ROL') {
                $rootScope.$emit('T74066Emit', 'rol');
            } else if ($scope.obj.Hidenfield === 'FOR') {
                $rootScope.$emit('T74132Emit', 'for');
            } else if ($scope.obj.Hidenfield === 'AUT') {
                $rootScope.$emit('T74136Emit', 'aut');
            }
        };
      
       
    }]);







