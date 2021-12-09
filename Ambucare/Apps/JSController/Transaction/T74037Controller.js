
app.controller("T74037Controller", ["$scope", "$filter", "$http", "$window", "uiGridConstants", "DTOptionsBuilder", "DTColumnBuilder", "Data","T74037Service",
    function (scope, $filter, $http, $window, uiGridConstants, DTOptionsBuilder, DTColumnBuilder, Data, T74037Service) {
        scope.Div_Person = false;
        scope.IssueInfo = [{
            ItemCode: '',
            Price: '',
            Stock: '',
            Quantity: ''
           
        }];

        scope.Addrow = function (items) {

            scope.IssueInfo.push({
                ItemCode: '',
                Price: '',
                Stock: '',
                Quantity: ''
            });
          //  var index = $scope.employeeInfo.indexOf(items);
            //$scope.selected.items = $scope.gvw[index];
        }
        var company = T74037Service.GetCompanyData();

        company.then(function (data) {
          
            scope.CompanyList = data;
        });

        var employeeType = T74037Service.GetEmployeeData();

        employeeType.then(function (data) {
            
            scope.EmployeeList = data;
        });

       

        scope.RemoveRow = function (index) {
            scope.IssueInfo.splice(index, 1);
        };

        scope.ChekPersonal_Click = function () {
            if (scope.Chk.Person==1) {
                scope.Chk.Ambulace = '2';
                scope.Div_Ambulace = false;
                scope.Div_Person = true; 
            } else {
                scope.Div_Person = false;
            }
            

        }

        scope.ChekAmbulace_Click = function () {
            if (scope.Chk.Ambulace==1) {
                scope.Chk.Person = '2';
                scope.Div_Person = false;

                scope.Div_Ambulace = true;
            } else {
                scope.Div_Ambulace = false;
            }
           
        }

    }]);

app.filter('propsFilter', function () {
    return function (items, props) {
        var out = [];

        if (angular.isArray(items)) {
            var keys = Object.keys(props);

            items.forEach(function (item) {
                var itemMatches = false;

                for (var i = 0; i < keys.length; i++) {
                    var prop = keys[i];
                    var text = props[prop].toLowerCase();
                    if (item[prop].toString().toLowerCase().indexOf(text) !== -1) {
                        itemMatches = true;
                        break;
                    }
                }

                if (itemMatches) {
                    out.push(item);
                }
            });
        } else {
            // Let the output be the input untouched
            out = items;
        }

        return out;
    };
});