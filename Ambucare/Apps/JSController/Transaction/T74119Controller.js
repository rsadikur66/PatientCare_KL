app.directive('productionQty', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attr, ngModelCtrl) {
            function fromUser(text) {
                var i = 0;
                var transformedInput = text.replace(/[^0-9]/g, function (match) {
                    return match === "." ? (i++ === 0 ? '.' : '') : '';
                });
                //console.log(transformedInput);
                if (transformedInput !== text) {
                    ngModelCtrl.$setViewValue(transformedInput);
                    ngModelCtrl.$render();
                }
                return transformedInput;
            }
            ngModelCtrl.$parsers.push(fromUser);

        }
    };
});
app.config(['$locationProvider', function ($locationProvider) {
    $locationProvider.html5Mode({ enabled: true }
    );
}]);

app.controller("T74119Controller", ["$scope", "$filter", "$http", "$window", "$location", "T74119Service", "uiGridConstants", "DTOptionsBuilder", "DTColumnBuilder", "Data", "LabelService", 
    function ($scope, $filter, $http, $window, $location, T74119Service, uiGridConstants, DTOptionsBuilder, DTColumnBuilder, Data, LabelService) {
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.T74036 = {};
        $scope.obj.T74048 = {};
        $scope.obj.T7427 = {};

        var url = $location.absUrl();
        var param = url.split("=").pop();
        $scope.obj.ProductList = [];
        var labelData = LabelService.getlabeldata('T74119');
        labelData.then(function (data) {
            $scope.entity = data;
        });
        $scope.SalesData=function () {
           var sale = T74119Service.getSalesData(param);
           sale.then(function (data) {
               if ($scope.obj.ProductList.length===0) {
                   $scope.obj.T_COMPANY_ID = data[0].T_COMPANY_ID;
                   $scope.obj.T_COMPANY_NAME = data[0].T_COMPANY_NAME;
                   $scope.obj.T_SUPPLIER_ID = data[0].T_SUPPLIER_ID;
                   $scope.obj.T_SUPPLIER_NAME = data[0].T_SUPPLIER_NAME;
                   $scope.obj.T74036.T_CURRENCY_ID = data[0].T_CURRENCY_ID;
                   $scope.obj.T_CURRENCY_NAME = data[0].T_CURRENCY_NAME;
                   $scope.obj.T74036.T_STORE_ID = data[0].T_STORE_ID_TO;
                   $scope.obj.T_STORE_TO_LANG2 = data[0].T_STORE_TO_LANG2;
                   $scope.obj.T7427.T_STORE_ID = data[0].T_STORE_ID_FROM;
                   $scope.obj.T_STORE_FROM_LANG2 = data[0].T_STORE_FROM_LANG2;
                   $scope.obj.T74036.T_ISSUE_DATE = $filter('date')(new Date(), 'dd-MMM-yyyy');
                   var reqDate = new Date(data[0].T_SL_REQ_DATE.match(/\d+/)[0] * 1);
                   $scope.obj.T_SL_REQ_DATE = $filter('date')(reqDate, "dd-MMM-yyyy");

                   $scope.obj.Check_STATUS = data[0].T_SL_REQ_STATUS;
                   

                  

                   for (var i = 0; i < data.length; i++) {
                       $scope.obj.demo = {};
                       $scope.obj.demo.T_ITEM_NAME = data[i].T_ITEM_NAME;
                       $scope.obj.demo.T_PRIORITY = data[i].T_PRIORITY;
                       $scope.obj.demo.T_STOCK_QUANTITY = JSON.parse(data[i].STOCK_QUANTITY);
                       $scope.obj.demo.T_SL_QTY = JSON.parse(data[i].T_SL_QTY);
                       $scope.obj.demo.T_ITEM_ID = JSON.parse(data[i].T_ITEM_ID);
                       $scope.obj.demo.T_ITEM_UM_ID = data[i].T_ITEM_UM_ID;
                       $scope.obj.demo.T_ITEM_UM_NAME = data[i].T_ITEM_UM_NAME;
                       $scope.obj.demo.T_RCV_QTY = data[i].T_RCV_QTY;
                       $scope.obj.demo.T_RATE = data[i].T_RATE;
                       $scope.obj.demo.T_SL_REQ_DTL_ID = data[i].T_SL_REQ_DTL_ID;
                       $scope.obj.demo.check = data[i].T_SL_STS_DTL === 'Reject' ? '1' : null;
                       $scope.obj.demo.T_SL_STS_DTL = data[i].T_SL_STS_DTL;
                       $scope.obj.ProductList.push($scope.obj.demo);
                   }
              }
               
           });
        }
           $scope.chk_Rcv_Qty = function (id) {
               if ($scope.obj.T74036.T_DISCOUNT != undefined || $scope.obj.T74036.T_VAT != undefined) {
               var StkQty = parseFloat(angular.element($("#spnStkQty" + id)).scope().obj.T_STOCK_QUANTITY);
               var ReqQty = parseFloat(angular.element($("#spnReqQty" + id)).scope().obj.T_SL_QTY);
               var IssQty = parseFloat(angular.element($("#spnIssQty" + id)).scope().obj.T_RCV_QTY);
               var RcvQty = parseFloat(angular.element($("#txtRcvQty" + id)).scope().obj.T_RCV_QTY_ISSUE);
              
               if (RcvQty > StkQty || RcvQty > ReqQty || RcvQty > ReqQty - IssQty ) {
                   //alert('Invalid Input');
                   alert($scope.getSingleMsg('S0018'));
                   $scope.obj.ProductList[id].T_RCV_QTY_ISSUE = null;
                   $scope.obj.ProductList[id].T_LINE_TOTAL = null;
               } else {
                   $scope.obj.ProductList[id].T_LINE_TOTAL = $scope.obj.ProductList[id].T_RATE * $scope.obj.ProductList[id].T_RCV_QTY_ISSUE;
                   $scope.obj.T74036.T_TOTAL = 0;
                   $scope.obj.T_DISCOUNT_AMOUNT = 0;
                   $scope.obj.T_GROSS_TOTAL = 0;
                   $scope.obj.T_VAT_AMOUNT = 0;
                   $scope.obj.T74036.T_GRAND_TOTAL = 0;
                   for (var i = 0; i < $scope.obj.ProductList.length; i++) {
                       if (!isNaN($scope.obj.ProductList[i].T_LINE_TOTAL)) {
                           $scope.obj.T74036.T_TOTAL += $scope.obj.ProductList[i].T_LINE_TOTAL;
                           $scope.obj.T_DISCOUNT_AMOUNT = $scope.obj.T74036.T_TOTAL *
                               ($scope.obj.T74036.T_DISCOUNT / 100);
                           $scope.obj.T_GROSS_TOTAL = $scope.obj.T74036.T_TOTAL - $scope.obj.T_DISCOUNT_AMOUNT;
                           $scope.obj.T_VAT_AMOUNT = $scope.obj.T_GROSS_TOTAL * ($scope.obj.T74036.T_VAT / 100);
                           $scope.obj.T74036.T_GRAND_TOTAL = $scope.obj.T_GROSS_TOTAL + $scope.obj.T_VAT_AMOUNT;

                       }
                   }
               }
               } else {
                   //alert('Discount and Vat can not be null');
                   alert($scope.getSingleMsg('S0024'));
                   $scope.obj.ProductList[id].T_RCV_QTY_ISSUE = null;
                   //return;
               }
           }
          $scope.CloseModal = function (id) {
              document.getElementById(id).style.display = "none";
          }
          $scope.OpenModal = function (id) {
              var empList = T74119Service.getEmpList($scope.obj.T74036.T_STORE_ID);
              empList.then(function (data) {
                  $scope.obj.EmployeeList = data;
              });
              document.getElementById(id).style.display = "block";
          }
          $scope.selectedItem = function (index, obj, id) {

              $scope.selectedRow = index;
              $scope.obj.EMP_NAME = obj.EMP_NAME;
              $scope.obj.T74036.T_ISSUED_BY = obj.T_EMP_ID;
              $scope.Search = "";
              document.getElementById(id).style.display = "none";
          }
         
          $scope.Reject = function (id) {
              if ($scope.obj.ProductList[id].check === '1') {
                  $scope.obj.ProductList[id].T_RCV_QTY_ISSUE = null;
                  $scope.obj.ProductList[id].T_LINE_TOTAL = null;
                  $scope.obj.T74036.T_TOTAL = 0;
                  $scope.obj.T_DISCOUNT_AMOUNT = 0;
                  $scope.obj.T_GROSS_TOTAL = 0;
                  $scope.obj.T_VAT_AMOUNT = 0;
                  $scope.obj.T74036.T_GRAND_TOTAL = 0;
                  for (var i = 0; i < $scope.obj.ProductList.length; i++) {
                      if (!isNaN($scope.obj.ProductList[i].T_LINE_TOTAL)) {
                          $scope.obj.T74036.T_TOTAL += $scope.obj.ProductList[i].T_LINE_TOTAL;
                          $scope.obj.T_DISCOUNT_AMOUNT = $scope.obj.T74036.T_TOTAL *
                              ($scope.obj.T74036.T_DISCOUNT / 100);
                          $scope.obj.T_GROSS_TOTAL = $scope.obj.T74036.T_TOTAL - $scope.obj.T_DISCOUNT_AMOUNT;
                          $scope.obj.T_VAT_AMOUNT = $scope.obj.T_GROSS_TOTAL * ($scope.obj.T74036.T_VAT / 100);
                          $scope.obj.T74036.T_GRAND_TOTAL = $scope.obj.T_GROSS_TOTAL + $scope.obj.T_VAT_AMOUNT;

                      }
                  }}
              
          }
          $scope.Save = function () {
              if ($scope.obj.Check_STATUS!=1) {
                  //T74049-----------------
                  $scope.obj.T74049 = [];
                  for (var i = 0; i < $scope.obj.ProductList.length; i++) {
                      if ($scope.obj.ProductList[i].T_RCV_QTY_ISSUE !== undefined) {
                          $scope.obj._t74049 = {};

                          $scope.obj._t74049.T_SL_REQ_DTL_ID = $scope.obj.ProductList[i].T_SL_REQ_DTL_ID;
                          $scope.obj._t74049.T_RCV_QTY = parseFloat($scope.obj.ProductList[i].T_RCV_QTY_ISSUE) + parseFloat($scope.obj.ProductList[i].T_RCV_QTY);
                          if ($scope.obj._t74049.T_RCV_QTY < parseFloat($scope.obj.ProductList[i].T_SL_QTY)) {
                              $scope.obj._t74049.T_SL_STS_DTL = 'Partial';
                          }
                          else if ($scope.obj._t74049.T_RCV_QTY === parseFloat($scope.obj.ProductList[i].T_SL_QTY)) { $scope.obj._t74049.T_SL_STS_DTL = 'Recieved'; }
                          else if ($scope.obj.ProductList[i].check === '1') {
                              $scope.obj._t74049.T_SL_STS_DTL = 'Reject';
                              $scope.obj._t74049.T_RCV_QTY = 0;
                          }

                          $scope.obj.T74049.push($scope.obj._t74049);
                      }
                  }
                  //T74048-----------------
                  //  var chk;
                  //for (var k = 0; k < $scope.obj.T74049.length; k++) {
                  //    if ($scope.obj.T74049[k].T_SL_STS_DTL === 'Partial' || $scope.obj.T74049[k].T_SL_STS_DTL == null) {
                  //        chk = 1;
                  //    }
                  //}
                  $scope.obj.T74048.T_SL_REQ_ID = param;
                  //$scope.obj.T74048.T_SL_REQ_STATUS = chk !== 1 ? 1 : null;
                  //T74036-----------------
                  $scope.obj.T74036.T_SL_REQ_ID = param;
                  //T74037-----------------
                  $scope.obj.T74037 = [];
                  for (var j = 0; j < $scope.obj.ProductList.length; j++) {
                      if ($scope.obj.ProductList[j].T_RCV_QTY_ISSUE !== undefined) {
                          if ($scope.obj.ProductList[j].check !== '1') {
                              $scope.obj._t74037 = {};
                              $scope.obj._t74037.T_ITEM_ID = $scope.obj.ProductList[j].T_ITEM_ID;
                              $scope.obj._t74037.T_UOM_ID = $scope.obj.ProductList[j].T_ITEM_UM_ID;
                              $scope.obj._t74037.T_SALE_PRICE = $scope.obj.ProductList[j].T_RATE;
                              $scope.obj._t74037.T_QUANTITY = $scope.obj.ProductList[j].T_RCV_QTY_ISSUE;
                              // $scope.obj._t74037.T_TOTAL_AMOUNT = $scope.obj.ProductList[j].T_LINE_TOTAL;
                              $scope.obj._t74037.T_SL_REQ_DTL_ID = $scope.obj.ProductList[j].T_SL_REQ_DTL_ID;
                              $scope.obj.T74037.push($scope.obj._t74037);
                          }
                      }
                  }
                  var t27 = $scope.obj.T7427;
                  var t48 = $scope.obj.T74048;
                  var t49 = $scope.obj.T74049;
                  var t36 = $scope.obj.T74036;
                  var t37 = $scope.obj.T74037;

                  var insert = T74119Service.Insert(t27, t36, t37, t48, t49);

                  insert.then(function (data) {
                      //alert('Saved Succesfully');
                      alert($scope.getSingleMsg('S0012'));
                      $scope.obj.ProductList = [];
                      $scope.SalesData();
                  }); 
              } else {
                  //alert("This requisition has been completed. You can not modify it");
                  alert($scope.getSingleMsg('S0025'));

              }
              
          }
    }]);





