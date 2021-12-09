app.service("T74049Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
    var dataSvc = {
        saveData: saveData
        
    };
    return dataSvc;

    function saveData(R) {
        debugger;
        $http({
            method: 'Post',
            url: vrtlDirr +'/T74049/AddReligion',
            data: JSON.stringify(R)
        });
    }

    }
]);