app.controller("T74137Controller", ["$scope", "$rootScope", "$filter", "$http", "$window", "T74137Service", "uiGridConstants",
    "DTOptionsBuilder", "Data", "DTColumnBuilder",
    function ($scope, $rootScope, $filter, $http, $window, T74137Service, uiGridConstants, DTOptionsBuilder, Data, DTColumnBuilder) {
        //For Instance

        $scope.obj = {};
        $scope.obj = Data;

        

        //----------Tab Toggling--------------Start
        $scope.tabs = [
          //{ title: 'Item Color', url: $scope.vrDir+'/PartPages/T74011.html', hdn: 'G' },
          //{ title: 'Item Brand', url: $scope.vrDir+ '/PartPages/T74002.html', hdn: 'M' },
          //{ title: 'Ambulance Type', url: $scope.vrDir+'/PartPages/T74047.html', hdn: 'N' },
          { title: 'Register Ambulance', url: $scope.vrDir+'/PartPages/T74014.html', hdn: 'R' },
          { title: 'Employee Assign', url: $scope.vrDir+'/PartPages/T74015.html', hdn: 'O' },
          { title: 'Store Setup', url: $scope.vrDir+'/PartPages/T74044.html', hdn: 'A' }
        ];

        $scope.currentTab = $scope.vrDir +'/PartPages/T74014.html';
        $scope.obj.hiddenField = 'A';
        $scope.onClickTab = function (tab) {

            $scope.obj.hiddenField = tab.hdn;
            $scope.currentTab = tab.url;


        };
        $scope.isActiveTab = function (tabUrl) { return tabUrl === $scope.currentTab; };
        $scope.Save_Click = function () {
            if ($scope.obj.hiddenField === 'G') {
                $rootScope.$emit('T74011Emit', 'g');
            } else if ($scope.obj.hiddenField === 'M') {
                $rootScope.$emit('T74002Emit', 'm');
            } else if ($scope.obj.hiddenField === 'N') {
                $rootScope.$emit('T74047Emit', 'n');
            } else if ($scope.obj.hiddenField === 'R') {
                $rootScope.$emit('T74014Emit', 'r');
            } else if ($scope.obj.hiddenField === 'O') {
                $rootScope.$emit('T74015Emit', 'o');
            } else if ($scope.obj.hiddenField === 'A') {
                $rootScope.$emit('T74044Emit', 'a');
            }
        };
        $scope.Clear = function () {
            if ($scope.obj.hiddenField === 'G') {
                $rootScope.$emit('T74011Emit', 'g');
            } else if ($scope.obj.hiddenField === 'M') {
                $rootScope.$emit('T74002Emit', 'm');
            } else if ($scope.obj.hiddenField === 'N') {
                $rootScope.$emit('T74047Emit', 'n');
            } else if ($scope.obj.hiddenField === 'R') {
                $rootScope.$emit('T74014Emit', 'x');
            } else if ($scope.obj.hiddenField === 'O') {
                $rootScope.$emit('T74015Emit', 'o');
            } else if ($scope.obj.hiddenField === 'A') {
                $rootScope.$emit('T74044Emit', 'a');
            }
        }
    }]);





