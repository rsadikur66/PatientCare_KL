app.controller("T74004Controller", ["$scope", "$filter", "$http", "$window", "T74004Service", "Data", "DTOptionsBuilder", "DTColumnBuilder", "LabelService", "$rootScope",
    function (scope, $filter, $http, $window, T74004Service, Data, DTOptionsBuilder, DTColumnBuilder, LabelService, $rootScope) {

        scope.obj = {};
        scope.obj = Data;
        // scope.MaritalList = {};
        scope.obj.R = {};
        scope.obj.B = {};
        scope.obj.G = {};
        scope.obj.M = {};
        scope.obj.N = {};
        scope.Chk = {};
        scope.obj.EM = {};

        var maritalLoad = T74004Service.MaritalData();
        maritalLoad.then(function (data) {
            scope.MaritalList = data;
        });

        var bloodGroupLoad = T74004Service.BloodGroupData();
        bloodGroupLoad.then(function (data) {

            scope.BloodGruopList = data;
        });

        var genderLoad = T74004Service.GenderData();
        genderLoad.then(function (data) {

            scope.Gender = data;
        });

        var religionLoad = T74004Service.ReligionData();

        religionLoad.then(function (data) {

            scope.ReligionList = data;
        });

        var EmployeeType = T74004Service.EmployeeTypeData();

        EmployeeType.then(function (data) {

            scope.EmployeeTypeList = data;
        });
        var Nationality = T74004Service.NationalityData();

        Nationality.then(function (data) {

            scope.NationalityList = data;
        });

        //For Entry User
        var EntryUser = T74004Service.EntryUser();
        EntryUser.then(function (data) {

            scope.obj.T_ENTRY_USER = data;
            scope.obj.T_ENTRY_DATE = $filter('date')(new Date(), 'dd-MMM-yyyy');
        });

        // Form Label Data Service Start 
        var labelData = LabelService.getlabeldata('T74004');
        labelData.then(function (data) {
            scope.entity = data;
        });
        // Form Label Data Service End 

        scope.Clear_Click = function () {
            scope.obj.T_EMP_ID = "";
            scope.obj.T_EMP_NO = "";
            scope.obj.T_FIRST_LANG1_NAME = "";
            scope.obj.T_FIRST_LANG2_NAME = "";
            scope.obj.T_FATHER_LANG1_NAME = "";
            scope.obj.T_FATHER_LANG2_NAME = "";

            scope.obj.T_MOTHER_LANG1_NAME = "";
            scope.obj.T_MOTHER_LANG2_NAME = "";
            scope.obj.T_GFATHER_LANG1_NAME = "";
            scope.obj.T_GFATHER_LANG2_NAME = "";
            scope.Chk.Inactive = "";

            scope.obj.T_ADDRESS1 = "";
            scope.obj.T_ADDRESS2 = "";
            scope.obj.T_NTNLTY_ID = "";
            scope.obj.T_PASSPORT_NO = "";

            scope.obj.T_PHONE_HOME = "";
            scope.obj.T_PHONE_WORK = "";
            scope.obj.T_MOBILE_NO = "";
            scope.obj.T_POSTAL_CODE = "";
            scope.Chk.Smoker = "";

            scope.obj.T_EMP_DEATH = "";
            scope.obj.T_IP_EPISODES = "";
            scope.obj.T_BALANCE_AMOUNT = "";
            scope.obj.T_EMAIL_ID = "";
            scope.obj.T_EMP_LANG = "";

            scope.obj.R.selected = "";
            scope.obj.G.selected = "";
            scope.obj.B.selected = "";
            scope.obj.M.selected = "";
            scope.obj.N.selected = "";
            scope.obj.EM.selected = "";
            scope.Chk.Accept = "";
        };

        function Save_Click() {
            var save = T74004Service.SaveData(scope.obj);
            save.then(function (msg) {
                if (msg == "1") {
                    alert(scope.getSingleMsg('S0012'));
                } else if (msg == "2") {
                    alert(scope.getSingleMsg('S0003'));
                } else if (msg == "3") {
                    alert("This employee already assigned!!! Couldn't update.");
                }
                //alert(msg);
                $window.location = "";
                //for grid Load
                //var gridLoad = T74004Service.getGridData();
                //gridLoad.then(function (data) {
                //    scope.EmployeeList = data;
                //});
            });
        }
        //scope.Save_Click = function () {
        //    Save_Click();

        //}
        function Clear() {
            scope.obj.T_PASSPORT_NO = '';
            scope.obj.T_FIRST_LANG2_NAME = '';
            scope.obj.T_EMP_NO = '';
            scope.obj.T_MOBILE_NO = '';
            scope.obj.R = {};
            scope.obj.G = {};
            scope.obj.T_RLGN_CODE = '';
            scope.obj.T_SEX_CODE = '';
            scope.obj.T_FIRST_LANG1_NAME = '';
            scope.obj.T_MOTHER_LANG2_NAME = '';
            scope.obj.T_FATHER_LANG2_NAME = '';
            scope.obj.T_ADDRESS2 = '';
            scope.obj.M = {};
            scope.obj.N = {};
            scope.obj.T_NTNLTY_ID = '';
            scope.obj.T_MRTL_STATUS_CODE = '';
            scope.obj.T_FATHER_LANG1_NAME = '';
            scope.obj.T_INACTIVE_FLAG = '2';

            scope.obj.T_GFATHER_LANG1_NAME = '';
            scope.obj.T_GFATHER_LANG2_NAME = '';
            scope.obj.T_EMAIL_ID = '';
            scope.obj.EM = {};
            scope.obj.B = {};
            scope.obj.T_BLD_GROUP_ID = '';
            scope.obj.T_EMP_TYP_ID = '';
        }

        var disable = $rootScope.$on('T74004Emit', function (event, data) {
            if (data == 'reg') {
                Save_Click();
            } else if (data == 'Clear') {
                Clear();
            }
        });
        scope.$on('$destroy', function () {
            disable();
        });
        scope.Chk = {
            Inactive: '1',
            Smoker: '1',
            Accept: '1'
        };

        //For Grid Function Start
        scope.someClickHandler = someClickHandler;
        scope.dtColumns = [
            //here We will add .withOption('name','column_name') for send column name to the server 
            DTColumnBuilder.newColumn("T_EMP_ID", "Emp No").withOption('name', 'T_EMP_ID'),
            DTColumnBuilder.newColumn("T_PASSPORT_NO", "Passport No").withOption('name', 'T_PASSPORT_NO'),
            DTColumnBuilder.newColumn("T_EMP_NO", "Emp No").withOption('name', 'T_EMP_NO').withOption('width', '55px').notVisible(),
            DTColumnBuilder.newColumn("T_NAME", "Emp Name").withOption('name', 'T_NAME').withOption('width', '110px'),
            DTColumnBuilder.newColumn("T_FIRST_LANG1_NAME", "FNm Loc").withOption('name', 'T_FIRST_LANG1_NAME').notVisible(),
            DTColumnBuilder.newColumn("T_FIRST_LANG2_NAME", "FNm Eng").withOption('name', 'T_FIRST_LANG2_NAME').notVisible(),
            DTColumnBuilder.newColumn("T_FATHER_LANG1_NAME", "FaNm Loc").withOption('name', 'T_FATHER_LANG1_NAME').notVisible(),
            DTColumnBuilder.newColumn("T_FATHER_LANG2_NAME", "FaNm Eng").withOption('name', 'T_FATHER_LANG2_NAME').notVisible(),

            DTColumnBuilder.newColumn("T_MOTHER_LANG1_NAME", "MoNm Loc").withOption('name', 'T_MOTHER_LANG1_NAME').notVisible(),
            DTColumnBuilder.newColumn("T_MOTHER_LANG2_NAME", "MoNm Eng").withOption('name', 'T_MOTHER_LANG2_NAME').notVisible(),
            DTColumnBuilder.newColumn("T_GFATHER_LANG1_NAME", "GFNm Loc").withOption('name', 'T_GFATHER_LANG1_NAME').notVisible(),
            DTColumnBuilder.newColumn("T_GFATHER_LANG2_NAME", "GFNm Eng").withOption('name', 'T_GFATHER_LANG2_NAME').notVisible(),
            DTColumnBuilder.newColumn("T_ADDRESS2", "Address").withOption('name', 'T_ADDRESS2').withOption('align', 'center'),
            DTColumnBuilder.newColumn("T_ADDRESS1", "Adrs Loc").withOption('name', 'T_ADDRESS1').notVisible(),
            DTColumnBuilder.newColumn("T_ADDRESS2", "Address").withOption('name', 'T_ADDRESS2').notVisible(),
            DTColumnBuilder.newColumn("T_EMP_LANG", "Emp Lang").withOption('name', 'T_EMP_LANG').notVisible(),
            DTColumnBuilder.newColumn("T_NTNLTY_ID", "Nat ID").withOption('name', 'T_NTNLTY_ID').notVisible(),
            //DTColumnBuilder.newColumn("T_PASSPORT_NO", "Pas No").withOption('name', 'T_PASSPORT_NO').notVisible(),

            DTColumnBuilder.newColumn("T_PHONE_HOME", "Phone Hom").withOption('name', 'T_PHONE_HOME').notVisible(),
            DTColumnBuilder.newColumn("T_PHONE_WORK", "Phone Wor").withOption('name', 'T_PHONE_WORK').notVisible(),

            DTColumnBuilder.newColumn("T_POSTAL_CODE", "Post Code").withOption('name', 'T_POSTAL_CODE').notVisible(),
            DTColumnBuilder.newColumn("T_SMOKER_FLAG", "Smoker").withOption('name', 'T_SMOKER_FLAG').notVisible(),

            DTColumnBuilder.newColumn("T_EMP_DEATH", "Death").withOption('name', 'T_EMP_DEATH').notVisible(),
            DTColumnBuilder.newColumn("T_IP_EPISODES", "Episode").withOption('name', 'T_IP_EPISODES').notVisible(),
            DTColumnBuilder.newColumn("T_BALANCE_AMOUNT", "Balance").withOption('name', 'T_BALANCE_AMOUNT').notVisible(),
            DTColumnBuilder.newColumn("T_EMAIL_ID", "Email").withOption('name', 'T_EMAIL_ID'),
            DTColumnBuilder.newColumn("T_MOBILE_NO", "Mobile No").withOption('name', 'T_MOBILE_NO'),


            DTColumnBuilder.newColumn("T_RLGN_CODE", "Religion").withOption('name', 'T_RLGN_CODE').notVisible(),
            DTColumnBuilder.newColumn("T_EMP_TYP_ID", "Emp Type").withOption('name', 'T_EMP_TYP_ID').notVisible(),
            DTColumnBuilder.newColumn("T_SEX_CODE", "Gender").withOption('name', 'T_SEX_CODE').notVisible(),
            DTColumnBuilder.newColumn("T_BLD_GROUP_ID", "BL Group").withOption('name', 'T_BLD_GROUP_ID').notVisible(),
            DTColumnBuilder.newColumn("T_MRTL_STATUS_CODE", "MR Status").withOption('name', 'T_MRTL_STATUS_CODE').notVisible(),
            DTColumnBuilder.newColumn("T_ACCEPTED", "Accepted").withOption('name', 'T_ACCEPTED').notVisible(),

            DTColumnBuilder.newColumn("Re_T_LANG2_NAME", "Religion").withOption('name', 'Re_T_LANG2_NAME').notVisible(),
            DTColumnBuilder.newColumn("GE_T_LANG2_NAME", "Gender").withOption('name', 'GE_T_LANG2_NAME'),
            DTColumnBuilder.newColumn("Bl_T_BLOOD_GORUP", "BG").withOption('name', 'Bl_T_BLOOD_GORUP').notVisible(),
            DTColumnBuilder.newColumn("Ma_T_LANG2_NAME", "MR Status").withOption('name', 'Ma_T_LANG2_NAME').notVisible(),
            DTColumnBuilder.newColumn("Na_T_LANG2_NAME", "Nationality").withOption('name', 'Na_T_LANG2_NAME'),
            DTColumnBuilder.newColumn("EmTy_T_LANG2_NAME", "Emp Type").withOption('name', 'EmTy_T_LANG2_NAME'),
            DTColumnBuilder.newColumn("T_INACTIVE_FLAG", "Emp Status").withOption('name', 'T_INACTIVE_FLAG').renderWith(function (T_INACTIVE_FLAG, row) {
                if (T_INACTIVE_FLAG == 1) {

                    return 'Active';
                }
                return 'Inactive';
            })
        ];

        scope.dtOptions = DTOptionsBuilder.newOptions().withOption('ajax',
                {
                    dataSrc: "data",
                    url: scope.vrDir + "/T74004/GetEmployeeData",
                    type: "POST"
                })
            .withOption('rowCallback', rowCallback)
            .withOption('processing', true) //for show progress bar
            .withOption('serverSide', true) // for server side processing
            .withPaginationType(
                'full_numbers') // for get full pagination options // first / last / prev / next and page numbers
            .withDisplayLength(10) // Page size
            .withOption('aaSorting', [0, 'asc']); // for default sorting column // here 0 means first column


        function someClickHandler(info) {
            scope.obj.T_EMP_ID = info.T_EMP_ID;
            scope.obj.T_EMP_NO = info.T_EMP_NO;
            scope.obj.T_NAME = info.T_FIRST_LANG2_NAME + " " + info.T_FATHER_LANG2_NAME + " " + info.T_GFATHER_LANG2_NAME + " " + info.T_MOTHER_LANG2_NAME;
            scope.obj.T_FIRST_LANG1_NAME = info.T_FIRST_LANG1_NAME;
            scope.obj.T_FIRST_LANG2_NAME = info.T_FIRST_LANG2_NAME;
            scope.obj.T_FATHER_LANG1_NAME = info.T_FATHER_LANG1_NAME;
            scope.obj.T_FATHER_LANG2_NAME = info.T_FATHER_LANG2_NAME;

            scope.obj.T_MOTHER_LANG1_NAME = info.T_MOTHER_LANG1_NAME;
            scope.obj.T_MOTHER_LANG2_NAME = info.T_MOTHER_LANG2_NAME;
            scope.obj.T_GFATHER_LANG1_NAME = info.T_GFATHER_LANG1_NAME;
            scope.obj.T_GFATHER_LANG2_NAME = info.T_GFATHER_LANG2_NAME;
            scope.Chk.Inactive = info.T_INACTIVE_FLAG;

            scope.obj.T_ADDRESS1 = info.T_ADDRESS1;
            scope.obj.T_ADDRESS2 = info.T_ADDRESS2;
            // scope.obj.T_NTNLTY_CODE = info.T_NTNLTY_CODE;
            scope.obj.T_NTNLTY_ID = info.T_NTNLTY_ID;
            scope.obj.T_PASSPORT_NO = info.T_PASSPORT_NO;

            scope.obj.T_PHONE_HOME = info.T_PHONE_HOME;
            scope.obj.T_PHONE_WORK = info.T_PHONE_WORK;
            scope.obj.T_MOBILE_NO = info.T_MOBILE_NO;
            scope.obj.T_POSTAL_CODE = info.T_POSTAL_CODE;
            scope.Chk.Smoker = info.T_SMOKER_FLAG;

            scope.obj.T_EMP_DEATH = info.T_EMP_DEATH;
            scope.obj.T_IP_EPISODES = info.T_IP_EPISODES;
            scope.obj.T_BALANCE_AMOUNT = info.T_BALANCE_AMOUNT;
            scope.obj.T_EMAIL_ID = info.T_EMAIL_ID;
            scope.obj.T_EMP_LANG = info.T_EMP_LANG;
            scope.obj.T_EMP_TYP_ID = info.T_EMP_TYP_ID;
            scope.obj.R.selected = { T_LANG2_NAME: info.Re_T_LANG2_NAME, T_RLGN_CODE: info.T_RLGN_CODE };
            scope.obj.G.selected = { T_LANG2_NAME: info.GE_T_LANG2_NAME, T_SEX_CODE: info.T_SEX_CODE };
            scope.obj.B.selected = { T_BLOOD_GORUP: info.Bl_T_BLOOD_GORUP, T_BLD_GROUP_ID: info.T_BLD_GROUP_ID };
            scope.obj.M.selected = { T_LANG2_NAME: info.Ma_T_LANG2_NAME, T_MRTL_STATUS_CODE: info.T_MRTL_STATUS_CODE };
            scope.obj.N.selected = { T_LANG2_NAME: info.Na_T_LANG2_NAME, T_NTNLTY_ID: info.T_NTNLTY_ID };
            scope.obj.EM.selected = { T_LANG2_NAME: info.EmTy_T_LANG2_NAME, T_EMP_TYP_ID: info.T_EMP_TYP_ID };
            scope.Chk.Accept = info.T_ACCEPTED;

        }
        function rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
            // Unbind first in order to avoid any duplicate handler (see https://github.com/l-lin/angular-datatables/issues/87)
            $('td', nRow).unbind('click');
            $('td', nRow).bind('click', function () {
                scope.$apply(function () {
                    scope.someClickHandler(aData);
                });
            });
            return nRow;
        }

        scope.SearchPassport = function () {
            var checkPassportNo = T74004Service.getPassportNo(scope.obj);
            checkPassportNo.then(function (data) {
                scope.storePassport = data;
                if (scope.storePassport.length !== 0) {
                    alert("passport number already exist!!!!");
                    fillupForm(scope.storePassport);
                } else {
                    alert("passport no not exist");
                    return;
                }
            });
        };
        function fillupForm(d) {
            scope.obj.R.selected = {};
            scope.obj.G.selected = {};
            scope.obj.M.selected = {};
            scope.obj.N.selected = {};
            scope.obj.B.selected = {};
            scope.obj.EM.selected = {};
            scope.obj.T_EMP_ID = d[0].T_EMP_ID;
            scope.obj.T_EMP_NO = d[0].T_EMP_NO;
            scope.obj.T_FIRST_LANG1_NAME = d[0].T_FIRST_LANG1_NAME;
            scope.obj.T_FIRST_LANG2_NAME = d[0].T_FIRST_LANG2_NAME;
            scope.obj.T_FATHER_LANG1_NAME = d[0].T_FATHER_LANG1_NAME;
            scope.obj.T_FATHER_LANG2_NAME = d[0].T_FATHER_LANG2_NAME;

            scope.obj.T_MOTHER_LANG1_NAME = d[0].T_MOTHER_LANG1_NAME;
            scope.obj.T_MOTHER_LANG2_NAME = d[0].T_MOTHER_LANG2_NAME;
            scope.obj.T_GFATHER_LANG1_NAME = d[0].T_GFATHER_LANG1_NAME;
            scope.obj.T_GFATHER_LANG2_NAME = d[0].T_GFATHER_LANG2_NAME;
            scope.Chk.Inactive = d[0].T_INACTIVE_FLAG;

            scope.obj.T_ADDRESS1 = d[0].T_ADDRESS1;
            scope.obj.T_ADDRESS2 = d[0].T_ADDRESS2;
            scope.obj.T_NTNLTY_ID = "";
            scope.obj.T_PASSPORT_NO = d[0].T_PASSPORT_NO;

            scope.obj.T_PHONE_HOME = "";
            scope.obj.T_PHONE_WORK = "";
            scope.obj.T_MOBILE_NO = d[0].T_MOBILE_NO;
            scope.obj.T_POSTAL_CODE = "";
            scope.Chk.Smoker = "";

            scope.obj.T_EMP_DEATH = "";
            scope.obj.T_IP_EPISODES = "";
            scope.obj.T_BALANCE_AMOUNT = "";
            scope.obj.T_EMAIL_ID = d[0].T_EMAIL_ID;
            scope.obj.T_EMP_LANG = "";

            scope.obj.R.selected.T_LANG2_NAME = d[0].Re_T_LANG2_NAME;
            scope.obj.G.selected.T_LANG2_NAME = d[0].GE_T_LANG2_NAME;
            scope.obj.B.selected.T_BLOOD_GORUP = d[0].Bl_T_BLOOD_GORUP;
            scope.obj.M.selected.T_LANG2_NAME = d[0].Ma_T_LANG2_NAME;
            scope.obj.N.selected.T_LANG2_NAME = d[0].Na_T_LANG2_NAME;
            scope.obj.EM.selected.T_LANG2_NAME = d[0].EmTy_T_LANG2_NAME;
            scope.Chk.Accept = "";

        }
        //var maritalLoad = T74004Service.MaritalData();
        //maritalLoad.then(function (data) {
        //    scope.MaritalList = data;
        //});

    }
]);

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