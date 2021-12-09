
app.controller("T74124Controller", ["$scope", "$compile", "$filter", "$http", "$window", "T74124Service", "Data",
    function (scope, $compile, $filter, $http, $window, T74124Service, Data) {
        //for instance
        scope.obj = {};
        scope.obj = Data;
        scope.obj.T = {};
        var ambulance = T74124Service.getAmbulance();
        ambulance.then(function(data) {
            scope.obj.AmbulList = data;
            scope.obj.T.selected = { T_STORE_ID: data[0].T_STORE_ID, T_LANG2_NAME: data[0].T_LANG2_NAME, T_AMBU_REG_ID: data[0].T_AMBU_REG_ID };
            if (scope.obj.AmbulList.length == '1') {
                var ambId = data[0].T_AMBU_REG_ID;
                Ambulance_Click(ambId);
                
            }
            
        });


        function Ambulance_Click(ambId) {
            var patient = T74124Service.getPatint(ambId);
            patient.then(function (data) {
                if (data.length !== 0) {
                    scope.obj.PatientList = data;
                } else {
                    scope.obj.PatientList = [];
                    //alert('Patient is not available  !!!!');
                    return;
                }

            });
        }
       // scope.Ambulance_Click = function (ambId) {
       //     debugger;
       //     var patient = T74124Service.getPatint(ambId);
       //     patient.then(function (data) {
       //         if (data.length !== 0) {
       //             scope.obj.PatientList = data;
       //         } else {
       //             scope.obj.PatientList = [];
       //             alert('Patient is not available  !!!!');
       //         }

       //     });
       //}

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