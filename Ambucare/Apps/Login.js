document.getElementById("txtEmpID").focus();
var focused = 'UName';

//$("#btnLogin").click(function () {
//    login();
//});
//$("#btnLogout").click(function () {
//    $.post("/Accounts/UserLogout").done(function () {
//        $("#txtEmpID").val("");
//        $("#txtPassword").val("");
//        window.top.close();
//    });
//});
$('#txtPassword').keypress(function (e) {
    if (e.which === 13) {
        login();
    }
});
$('#txtEmpID').keypress(function (e) {
    if (e.which === 13) {
        document.getElementById("txtPassword").focus();
        focused = 'Pwd';
    }
});

//Number add function start
function addNumber(num) {
    if (focused === "UName") {
        document.getElementById("txtEmpID").value += num;
        document.getElementById("txtEmpID").focus();
    }
    if (focused === "Pwd") {
        document.getElementById("txtPassword").value += num;
        document.getElementById("txtPassword").focus();
    }
};

function Cancel() {
    if (focused === "UName") {
        document.getElementById("txtEmpID").value = '';
        document.getElementById("txtEmpID").focus();
    }
    if (focused === "Pwd") {
        document.getElementById("txtPassword").value = '';
        document.getElementById("txtPassword").focus();
    }
}

function BackNumber() {
    if (focused === "UName") {
        document.getElementById("txtEmpID").value = document.getElementById("txtEmpID").value.substring(0, document.getElementById("txtEmpID").value.length - 1);
        document.getElementById("txtEmpID").focus();
    }
    if (focused === "Pwd") {
        document.getElementById("txtPassword").value = document.getElementById("txtPassword").value.substring(0, document.getElementById("txtPassword").value.length - 1);
        document.getElementById("txtPassword").focus();
    }
}


function Enter() {
    if (focused == 'UName') {
        document.getElementById("txtPassword").focus();
        focused = 'Pwd';
    } else {
        login();
    }
    //if (focused = 'Pwd') {

    //}


}
//Number add function end
function login() {
    var userId = $("#txtEmpID").val();
    var password = $("#txtPassword").val();
    $.post("/Accounts/UserLogin", { userId: userId, Password: password })
        .done(function (data) {
            if (data === "Invalid Id or Password....") {
                alert(data);
                document.getElementById("txtEmpID").focus();
                focused = "UName";
            } else {
               // $.post("/Accounts/UpdateLogin", { userId: userId });
                window.location.href = data !== "" ? "../Menu" : "";

                //if (data.length >4) {
                //    $.post("/Accounts/checkAmbulance", { userId: userId }).done(function (data1) {

                //        if (data1.length===0) {
                //            $.post("/Accounts/UpdateLogin", { userId: userId });
                //            window.location.href = data !== "" ? "../Menu" : "";
                //        }
                //        if (data1[0].T_PWD  === "" || data1[0].T_PWD  === "Done") {
                //            $.post("/Accounts/UpdateLogin", { userId: userId });
                //            window.location.href = data !== "" ? "../Menu" : "";
                //        }
                //        else {
                //            alert("Ambulance in Service/Maintanance");
                //        }
                //    }).fail(function () {
                //        alert(data);
                //        });    

                //    //$.ajax(
                //    //    {
                //    //        type: "POST",
                //    //        contentType: "application/json; charset=utf-8",
                //    //        url: "/Accounts/checkAmbulance",
                //    //        data: "{ userId: '" + userId + "'}",
                //    //        success: function (result) {
                //    //            debugger;
                //    //            $.post("/Accounts/UpdateLogin", { userId: userId });
                //    //            window.location.href = data !== "" ? "../Menu" : "";
                //    //        }
                //    //    });
                //}
                //else {
                //    window.location.href = "../Transaction/T74131/" + data;

                //}
            }

        }).fail(function () {
            alert(data);
        });
}