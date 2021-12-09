app.controller("T74141Controller",
    function ($scope, $rootScope, signalR, Flash, $compile, Data, T74131Service, $location, $window) {
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.t74098 = {};
        $scope.obj.k = {};
        $scope.obj.k.selected = {};
        $scope.OnlineUsers = [];
        $scope.obj.HospWithDoc = [];
        $scope.UserInPrivateChat = {};
        $scope.rooms = [];
        signalR.startHub();
        $scope.activeRoom = '';
        $scope.chatHistory = [];
        $scope.Users = [];
        $scope.RoomsLoggedId = [];
        $scope.typemsgdisable = true;
        $scope.UsersCount = 0;
        $scope.bubblesCount = [];
        $scope.maxBubbles = 10;
        $scope.ShowPrivateWindow = false;
        $scope.PrivateMessages = [];
        $scope.currentprivatemessages = {};
        $scope.pvtmessage = '';
        var uploadedCount = 0;
        $scope.groups = [];
        $scope.UserLeft = false;
        $scope.files = [];
        $scope.file = [];
        $rootScope.RootPrivateMessages = [];
        $rootScope.RootPrivateMessages = [];
        $scope.obj.pInfo = {};
        $scope.obj.chtDoc = '';
        var a = $location.absUrl().split('=')[1];
        if (a != undefined) {
            document.getElementById('divbody').style.display = "none";
        }

        var UserName = T74131Service.UserName();
        UserName.then(function (data) {
            $scope.UserName = JSON.parse(data);
            sessionStorage.setItem("UserName", JSON.stringify($scope.UserName));
            var UserId = T74131Service.UserId();
            UserId.then(function (dtUser) {
                $scope.UserId = JSON.parse(dtUser);
                sessionStorage.setItem("UserId", JSON.stringify($scope.UserId));


                signalR.Login($location.absUrl().split('/')[5],
                    JSON.parse(sessionStorage.getItem("UserName")),
                    JSON.parse(sessionStorage.getItem("UserId")));
            });




        });

        $scope.obj.onDocCall = function () {

            if ($scope.obj.HospWithDoc.length > 0) {
                alert('You are connected with doc');
            } else {
                var con = confirm('Do you want to reconnect with this Doctor?');
                if (con) {
                    var conWthDoc = T74131Service.conWthDoc();
                    conWthDoc.then(function (data) {
                        var dt = JSON.parse(data);
                        alert(dt);
                    });
                }
            }
           
         
            if ($scope.obj.HospWithDoc.length == 0) {
                var UserName = T74131Service.UserName();
                UserName.then(function (data) {
                    $scope.UserName = JSON.parse(data);
                    sessionStorage.setItem("UserName", JSON.stringify($scope.UserName));
                    var UserId = T74131Service.UserId();
                    UserId.then(function (dtUser) {
                        $scope.UserId = JSON.parse(dtUser);
                        sessionStorage.setItem("UserId", JSON.stringify($scope.UserId));


                        signalR.Login($location.absUrl().split('/')[5],
                            JSON.parse(sessionStorage.getItem("UserName")),
                            JSON.parse(sessionStorage.getItem("UserId")));
                    });




                });
            }
        }
        $scope.obj.chtReqNo = 0;
        setInterval(function () {
            if ($scope.obj.HospWithDoc.length == 0) {
                var UserName = T74131Service.UserName();
                UserName.then(function (data) {
                    $scope.UserName = JSON.parse(data);
                    sessionStorage.setItem("UserName", JSON.stringify($scope.UserName));
                    var UserId = T74131Service.UserId();
                    UserId.then(function (dtUser) {
                        $scope.UserId = JSON.parse(dtUser);
                        sessionStorage.setItem("UserId", JSON.stringify($scope.UserId));
                        signalR.Login($location.absUrl().split('/')[5],
                            JSON.parse(sessionStorage.getItem("UserName")),
                            JSON.parse(sessionStorage.getItem("UserId")));
                    });
                });
            }
            var reqListForTeam = T74131Service.reqListForTeam();
            reqListForTeam.then(function (data) {
                var dt = JSON.parse(data);
                if (dt.length > 0) {
                    $scope.obj.chtReqNo = dt.length;

                } else {
                    $scope.obj.chtReqNo = 0;
                }
            });


            var chkConn = T74131Service.chkConn();
            chkConn.then(function (data) {
                var dt = JSON.parse(data);
                if (dt == 0) {
                    var UserName = T74131Service.UserName();
                    UserName.then(function (data) {
                        $scope.UserName = JSON.parse(data);
                        sessionStorage.setItem("UserName", JSON.stringify($scope.UserName));
                        var UserId = T74131Service.UserId();
                        UserId.then(function (dtUser) {
                            $scope.UserId = JSON.parse(dtUser);
                            sessionStorage.setItem("UserId", JSON.stringify($scope.UserId));


                            signalR.Login($location.absUrl().split('/')[5],
                                JSON.parse(sessionStorage.getItem("UserName")),
                                JSON.parse(sessionStorage.getItem("UserId")));

                            $scope.PrivateMessages = [];
                            $scope.obj.HospWithDoc = [];
                            $scope.UserInPrivateChat = [];
                            $scope.chatuser = [];
                        });




                    });
                }
            });

             
            
            


        }, 5000);
        setInterval(function() {
            if ($scope.obj.chtDoc == '' || $scope.obj.chtDoc == null) {
                var getDocID = T74131Service.getDocID();
                getDocID.then(function (data) {
                    var dt = JSON.parse(data);
                    if (dt.length>0) {
                        $scope.obj.chtDoc = dt[0].T_CHAT_DOC_ID;    
                    }
                    
                });

            }
        },5000);
        $scope.onRcvDocReq = function () {
            if ($scope.obj.HospWithDoc.length > 0) {
                alert('You are connected with doc');
            } else {
                var acptReqofDoc = T74131Service.acptReqofDoc();
                acptReqofDoc.then(function (data) {
                    var dt = JSON.parse(data);
                    alert(dt);
                });
            }

        }
        setInterval(function () {
            $scope.obj.pi = 0;
            $scope.obj.i = 0;
            if ($scope.obj.HospWithDoc.length > 0) {
                $scope.chatuser = $scope.obj.HospWithDoc[0].docInfo[0];
                $scope.UserInPrivateChat = $scope.obj.HospWithDoc[0].docInfo[0];
                $scope.UserInPrivateChat.name = $scope.obj.HospWithDoc[0].docInfo[0].DOCTOR;
                $scope.UserInPrivateChat.ConnectionId = 0;
                var user = $scope.obj.HospWithDoc[0].docInfo[0];
                var his = T74131Service.GetChatHistory(user.T_REQUEST_ID, user.T_U_ID, user.TYPE);
                his.then(function (data) {
                    $scope.PrivateMessages = [];
                    var dt = JSON.parse(data);
                    for (var i = 0; i < dt.length; i++) {
                        var nm_from = dt[i].T_SENDER_TYPE == 'Doc' ? user.DOCTOR : $scope.UserName;
                        $scope.PrivateMessages.push({ T_TIME: dt[i].T_TIME,U_NAME: dt[i].U_NAME,T_USER_ID: dt[i].T_USER_ID,to: 0, from: nm_from, message: dt[i].T_TEXT, ConnectionId: 0, T_SENDER_TYPE: dt[i].T_SENDER_TYPE, T_FILE_LOC: dt[i].T_FILE_LOC, ext : (dt[i].T_FILE_LOC != null ? dt[i].T_FILE_LOC.split('.').pop() : null)});
                       
                    }
                   
                   
                    //$scope.$apply();
                });
                
            }
        }, 1000);
       
        $scope.onHosNameClick = function (id, name) {
            $scope.obj.HospitalName = name;
            if (document.getElementById("divDocList" + id).style.display === "none") {
                for (var i = 0; i < $scope.obj.HospWithDoc.length; i++) {
                    document.getElementById("divDocList" + i).style.display = "none";
                    document.getElementById("imgHosName" + i).src = "../Images/Chat_img/plus_white.png";
                    document.getElementById("divHosName" + i).style.background = "#D23F45";
                }
                document.getElementById("divDocList" + id).style.display = "block";
                document.getElementById("imgHosName" + id).src = "../Images/Chat_img/minus_white.png";
                document.getElementById("divHosName" + id).style.background = "#41484d";
            } else {
                document.getElementById("divDocList" + id).style.display = "none";
                document.getElementById("imgHosName" + id).src = "../Images/Chat_img/plus_white.png";
                //document.getElementById("divHosName" + id).style.background = "#D23F45";
                document.getElementById("divHosName" + id).style.backgroundImage = "url('../Images/title_background.png')";
            }


        }
      
        document.getElementById('fileUpload').addEventListener('click', openDialog);

        function openDialog() {
            document.getElementById('flUpld').click();
            $scope.file = null;
        }

        $scope.onFileLoad = function () {
            $scope.loader(true);
        }

        setInterval(function () {
            if ($scope.file == null) {
                $scope.loader(true);
            } else {
                $scope.loader(false);
            }

        }, 1000);

        $scope.fileSent = function () {
            var u = $scope.UserInPrivateChat;
            $scope.obj.t74098.T_RECIEVER_ID = u.T_U_ID;
            $scope.obj.t74098.T_REQUEST_ID = u.T_REQUEST_ID;
            $scope.obj.t74098.T_SENDER_TYPE = u.TYPE == 'Pat' ? 'Doc' : 'Pat';

            var fileName = $scope.file.filename.split('.').pop();
            var save = T74131Service.chatFileUpload($scope.obj.t74098, $scope.file.base64, fileName);
            save.then(function (mesg) {
                alert('File Sent');
                //$scope.obj = {};
                //$window.location = ""; //For Page load After Save and Update
            });




            //$scope.obj.t74098.T_TEXT = txt.val().trim();
            //T74131Service.chatHistory($scope.obj.t74098);






            //var save = T74131Service.saveTutorial($scope.obj);
            //save.then(function (mesg) {
            //    //alert(mesg);
            //    //$scope.obj = {};
            //    //$window.location = ""; //For Page load After Save and Update
            //});
        }

        $rootScope.updateMessage = function () {
            $scope.groups = _.groupBy($rootScope.RootPrivateMessages, "from");
            $scope.messageCount = $rootScope.RootPrivateMessages.length;
        }

        $scope.OpenPrivatewindow = function (index) {
            var user = $scope.users[index];
            $scope.ShowPrivateWindow = true;
            $scope.UserInPrivateChat = user;
            console.log("CID :" + $scope.UserInPrivateChat.Connectionid);
            //$scope.$apply();
            $scope.$evalAsync();
            // $scope.createPrivateChatWindow($scope.$parent.UserName, conId, user.name)
        }

        $scope.SendPrivateMessage = function (e, u, id, name) {
            
            if (e.keyCode == 13) {
                if ($scope.obj.HospWithDoc.length > 0) {
                    if (id == null || id == undefined) {
                        $scope.obj.t74098.T_RECIEVER_ID = u.T_U_ID;
                        $scope.obj.t74098.T_REQUEST_ID = u.T_REQUEST_ID;
                        $scope.obj.t74098.T_SENDER_TYPE = u.TYPE == 'Pat' ? 'Doc' : 'Pat';
                        $scope.obj.t74098.T_TEXT = $scope.chatText.trim();
                        T74131Service.chatHistory($scope.obj.t74098);
                        //$scope.PrivateMessages.push({ to: u.DOCTOR, from: $scope.UserName, message: $scope.chatText });
                        $scope.chatText = '';
                    } else {

                        //  if ($scope.UserLeft == false) { 
                        var txt = $("#txt" + id)
                        if (txt.val().trim() != '') {
                            //$("#txt392331de-393d-427d-a631-3f52caf06dd1").val()
                            var txt = $("#txt" + id);
                            console.log(txt);
                            signalR.SendPrivateMessage(id, txt.val().trim(), name);
                            //  $scope.pvtmessage = '';
                            $scope.obj.t74098.T_RECIEVER_ID = u.T_U_ID;
                            $scope.obj.t74098.T_REQUEST_ID = u.T_REQUEST_ID;
                            $scope.obj.t74098.T_SENDER_TYPE = u.TYPE == 'Pat' ? 'Doc' : 'Pat';
                            //$scope.obj.t74098.T_TEXT = txt.val().trim();
                            $scope.obj.t74098.T_TEXT = $scope.chatText.trim();
                            T74131Service.chatHistory($scope.obj.t74098);
                            txt.val('');
                            $scope.chatText = '';
                        }

                    }
                }
                else {
                    var text = '';
                    text = $scope.chatText.trim();
                    if (text!='') {
                        var emergenceyReq = T74131Service.emergenceyReq(text);
                        emergenceyReq.then(function(data) {
                            var dt = JSON.parse(data);
                            alert(dt);
                            $scope.chatText = '';

                        });
                    }
                    
                }
               





            }
        }

        signalR.GetOnlineUsers(function (onlineUsers, user) {
            var a = JSON.parse(onlineUsers);
            $scope.OnlineUsers = a;

            $scope.obj.HospWithDoc = user.HospWithDoc;
            var s = [];
            for (var i = 0; i < user.HospWithDoc.length; i++) {
                var t = {};
                t.HOSPITAL_NAME = user.HospWithDoc[i].HOSPITAL_NAME;
                t.DOC_NO = 0;
                var x = [];
                for (var k = 0; k < user.HospWithDoc[i].docInfo.length; k++) {
                    var tc = {};
                    $scope.HDM_DOC_CODE = user.HospWithDoc[i].docInfo[k].HDM_DOC_CODE;
                    tc.HDM_DOC_CODE = user.HospWithDoc[i].docInfo[k].HDM_DOC_CODE;
                    tc.HDM_SPCLTY_CODE = user.HospWithDoc[i].docInfo[k].HDM_SPCLTY_CODE;
                    tc.DOCTOR = user.HospWithDoc[i].docInfo[k].DOCTOR;
                    tc.HDM_SPCLTY_DSCRPTN = user.HospWithDoc[i].docInfo[k].HDM_SPCLTY_DSCRPTN;
                    tc.T_U_ID = user.HospWithDoc[i].docInfo[k].T_U_ID;
                    tc.T_REQUEST_ID = user.HospWithDoc[i].docInfo[k].T_REQUEST_ID;
                    tc.TYPE = user.HospWithDoc[i].docInfo[k].TYPE;
                    //-----------------------------New ---------------------------------
                    tc.T_USER_ID = user.HospWithDoc[i].docInfo[k].T_USER_ID;

                    for (var l = 0; l < a.length; l++) {
                     
                        if (user.HospWithDoc[i].docInfo[k].T_U_ID == a[l].id) {
                            tc.src = "../Images/Chat_img/Circle_green.png";
                            tc.ConnectionId = a[l].ConnectionId;
                            tc.id = a[l].id;
                            tc.name = a[l].name;

                        }

                    }
                    tc.pi = i;
                    tc.i = k;
                    x.push(tc);
                }

                t.docInfo = x;
                s.push(t);

            }
            $scope.obj.HospWithDoc = s;
           
            //console.log($scope.OnlineUsers);
            $scope.$apply();


            $scope.$evalAsync();
        });
       
        signalR.NewOnlineUser(function (user) {

            for (var i = 0; i < $scope.obj.HospWithDoc.length; i++) {
                for (var k = 0; k < $scope.obj.HospWithDoc[i].docInfo.length; k++) {
                  
                    if ($scope.obj.HospWithDoc[i].docInfo[k].T_U_ID == user.id) {
                        $scope.obj.HospWithDoc[i].DOC_NO++;

                        $scope.obj.HospWithDoc[i].docInfo[k].ConnectionId = user.ConnectionId;
                        $scope.obj.HospWithDoc[i].docInfo[k].id = user.id;
                        $scope.obj.HospWithDoc[i].docInfo[k].name = user.name;

                        document.getElementById("imgDoc" + i + k).src = "../Images/Chat_img/Circle_green.png";
                    }
                }
            }
            //$scope.OnlineUsers.push(user);
            $scope.$apply();
           
        });

        signalR.UpdateConnectionId(function (oid, nid) {
             
            if ($scope.OnlineUsers[i].ConnectionId == oid)
                $scope.OnlineUsers[i].ConnectionId = nid;
            $scope.$evalAsync();
        });

        signalR.NewOfflineUser(function (user) {

            for (var i = 0; i < $scope.obj.HospWithDoc.length; i++) {
                for (var k = 0; k < $scope.obj.HospWithDoc[i].docInfo.length; k++) {
                    if ($scope.obj.HospWithDoc[i].docInfo[k].T_U_ID == user.id) {
                        $scope.obj.HospWithDoc[i].DOC_NO--;
                        document.getElementById("imgDoc" + i + k).src = "";
                    }
                }
            }
            $.each($scope.OnlineUsers,
                function (i) {
                    if ($scope.OnlineUsers[i] != undefined)
                        if ($scope.OnlineUsers[i].name === user.name &&
                            $scope.OnlineUsers[i].ConnectionId == user.ConnectionId) {
                            $scope.OnlineUsers.splice(i, 1);
                            var message = '<strong> !! ' + user.name + '</strong> left the chat ';
                            Flash.create('danger', message);
                        }
                });
            if ($scope.UserInPrivateChat != undefined) {

                if ($scope.UserInPrivateChat.ConnectionId == user.ConnectionId) {
                    $scope.UserLeft = true;
                } else {
                    $scope.UserLeft = false;
                }
            }
            // $scope.OnlineUsers.push(user);
            $scope.$apply();
        });
        
        $scope.openPvtChat = function (pi, index) {
            $scope.obj.pi = pi;
            $scope.obj.i = index;

            console.log(index);
            //var user = $scope.OnlineUsers[index];
            var user = $scope.obj.HospWithDoc[pi].docInfo[index];
            //$scope.UserInPrivateChat = user;
            OpenDiv();
            hosName();
            //$scope.register_popup(user.ConnectionId, user.name);
        }
       
        signalR.RecievingPrivateMessage(function (toname, fromname, msg, connectionId) {
           
            $scope.openchats = [];
           
            $scope.PrivateMessages.push({ to: toname, from: fromname, message: msg, ConnectionId: 0 });

            if ($rootScope.RootPrivateMessages != undefined)
                $rootScope.RootPrivateMessages.push({
                    to: toname,
                    from: fromname,
                    message: msg,
                    ConnectionId: connectionId
                });
            if ($scope.$parent.UserName != fromname) // otheruser's pm
            {
                if ($scope.UserInPrivateChat == null) {
                    $scope.UserInPrivateChat = { name: fromname, ConnectionId: toname };
                }
            }
            console.log($scope.PrivateMessages);
         
            $scope.$evalAsync();
         
            $rootScope.$evalAsync();
            $rootScope.updateMessage(); //$evalAsync();
        
            if ($("#PrivateChatArea div.panel-body").length == 1) {
                var $container = $("#PrivateChatArea div.panel-body");
                $container[0].scrollTop = $container[0].scrollHeight;
                $container.animate({ scrollTop: $container[0].scrollHeight }, "fast");
            }
          
        });

        $scope.OpenDiv = function () {
            OpenDiv();
            hosName();
        }

        function OpenDiv() {

            if ($scope.UserInPrivateChat.TYPE == 'Pat') {

                var info = T74131Service.GetPatInfo($scope.UserInPrivateChat.T_U_ID);

                info.then(function (data) {
                    var dt = JSON.parse(data);
                    if (dt.length > 0) {
                        $scope.Medicine = true;

                        $scope.obj.pInfo.T_REQUEST_ID = dt[0].T_REQUEST_ID;
                        $scope.obj.pInfo.T_REF_DOC_CODE = dt[0].T_REF_DOC_CODE;
                        $scope.obj.pInfo.REFER_HOS = dt[0].REFER_HOS;
                        $scope.obj.pInfo.HOSNAME = dt[0].HOSNAME;
                        $scope.obj.pInfo.T_AMBU_REG_ID = dt[0].T_AMBU_REG_ID;
                        $scope.obj.pInfo.PATNAME = dt[0].PATNAME;
                        $scope.obj.pInfo.AGE = dt[0].AGE;
                        $scope.obj.pInfo.MARITAL_STATUS = dt[0].MARITAL_STATUS;
                        $scope.obj.pInfo.NATIONALITY = dt[0].NATIONALITY;
                        $scope.obj.pInfo.GENDER = dt[0].GENDER;
                        $scope.obj.pInfo.RELIGION = dt[0].RELIGION;
                        $scope.obj.pInfo.T_PROBLEM = dt[0].T_PROBLEM;
                        $scope.obj.pInfo.T_PROBLEM_DURATION = dt[0].T_PROBLEM_DURATION;
                        $scope.obj.pInfo.T_PROB_DETAILS = dt[0].T_PROB_DETAILS;
                        $scope.obj.pInfo.PROBLEM_TYPE = dt[0].PROBLEM_TYPE;
                        $scope.obj.pInfo.CHIEF_COMPLIENT = dt[0].CHIEF_COMPLIENT;
                        $scope.obj.pInfo.ICD10 = dt[0].ICD10;
                        $scope.obj.PatHistory = dt;







                        var med = T74131Service.GetPreviousMedicine($scope.obj.pInfo.T_REQUEST_ID);
                        med.then(function (data) {
                            var dt = JSON.parse(data);
                            $scope.obj.PreviousMedicineList = dt;
                            itemDatalist($scope.obj.pInfo.T_AMBU_REG_ID);
                            //document.getElementById('DocInfo').style.display = "block";
                            //document.getElementById('ShowPreviusMedication').style.display = "block";
                        });


                    }
                });
            }

        }

        function hosName() {
            var places = T74131Service.GetAllUserLatlong();
            places.then(function (data) {
                if ($scope.obj.pInfo.T_REF_DOC_CODE == null) {
                    $scope.obj.k.selected = { T_SITE_CODE: '', T_LANG2_NAME: 'Select' };

                } else {
                    $scope.obj.k.selected = { T_SITE_CODE: $scope.obj.pInfo.T_REF_DOC_CODE, T_LANG2_NAME: $scope.obj.pInfo.REFER_HOS };
                }
                $scope.places = JSON.parse(data);
            });
        }

        $scope.showMedication = function () {
            var med = T74131Service.GetPreviousMedicine($scope.obj.pInfo.T_REQUEST_ID);
            med.then(function (data) {
                var dt = JSON.parse(data);
                $scope.obj.PreviousMedicineList = dt;
                document.getElementById('ShowPreviusMedication').style.display = "block";
            });

        }

        $scope.CloseDiv = function (id) {
            document.getElementById(id).style.display = "none";
        }
      
        $scope.getChatHistory = function () {
            var pi = $scope.obj.pi;
            var index = $scope.obj.i;
            $scope.chatuser = $scope.obj.HospWithDoc[pi].docInfo[index];
            var his = T74131Service.GetChatHistory($scope.chatuser.T_REQUEST_ID, $scope.chatuser.T_U_ID, $scope.chatuser.TYPE);
            his.then(function (data) {
                $scope.obj.ChatHistoryList = [];
                var dt = JSON.parse(data);
                for (var i = 0; i < dt.length; i++) {
                    var t = {};
                    t.T_AUTO_ID = dt[i].T_AUTO_ID;
                    t.T_SENDER_ID = dt[i].T_SENDER_ID;
                    t.T_RECIEVER_ID = dt[i].T_RECIEVER_ID;
                    t.T_SENDER_TYPE = dt[i].T_SENDER_TYPE;
                    t.T_TEXT = dt[i].T_TEXT;
                    t.T_TIME = dt[i].T_TIME;
                    t.T_REF_ID = dt[i].T_REF_ID;
                    t.T_FILE_LOC = dt[i].T_FILE_LOC;
                    t.T_REQUEST_ID = dt[i].T_REQUEST_ID;
                    t.ext = dt[i].T_FILE_LOC != null ? dt[i].T_FILE_LOC.split('.').pop() : null;
                    t.name = dt[i].T_FILE_LOC != null ? dt[i].T_FILE_LOC.split('/').pop() : null;
                    $scope.obj.ChatHistoryList.push(t);

                }


                //var dtF = 'some data here...',
                //    blob = new Blob([dtF], { type: 'text/plain' }),
                //    url = $window.URL || $window.webkitURL;
                //$scope.fileUrl = url.createObjectURL(blob);

                //$scope.obj.ChatHistoryList = dt;
                document.getElementById('chatHistory').style.display = "block";
            });
        }

        function itemDatalist(e) {
            var item = T74131Service.getItem(e);  //$scope.obj.t74134.T_AMBU_REG_ID
            item.then(function (i) {
                $scope.dataList = [];
                var jsonItem = JSON.parse(i);
                //  $scope.obj.ItemList = jsonItem;
                for (var j = 0; j < jsonItem.length; j++) {
                    $scope.masterObj = {};
                    $scope.masterObj.T_STORE_ID = jsonItem[j].T_STORE_ID;
                    $scope.masterObj.T_ITEM_ID = jsonItem[j].T_ITEM_ID;
                    $scope.masterObj.ISSED_ITEM = jsonItem[j].ISSED_ITEM;
                    $scope.masterObj.T_DOSE_DURATION_CODE = jsonItem[j].T_DOSE_DURATION_CODE;
                    $scope.masterObj.ADMINISTERED_QUTY = jsonItem[j].ADMINISTERED_QUTY;
                    $scope.masterObj.T_LANG2_NAME = jsonItem[j].T_LANG2_NAME;
                    $scope.masterObj.STOCK = (jsonItem[j].STOCK).toFixed();
                    $scope.masterObj.ISSU_QTY = 0;
                    $scope.masterObj.CHECK = 'NO';
                    $scope.dataList.push($scope.masterObj);
                }
                $scope.obj.ItemList = $scope.dataList;

            });
        };

        $scope.CheckForQuanty = function (ind) {
            if ($scope.obj.ItemList[ind].CHECK === 'YES') {
                $scope.obj.ItemList[ind].ISSU_QTY += 1;
            } else {
                $scope.obj.ItemList[ind].ISSU_QTY = 0;
            }

        };

        $scope.Number_Click = function (btn, i) {
            if (btn === 'plus') {
                $scope.obj.ItemList[i].ISSU_QTY += 1;
                $scope.obj.ItemList[i].CHECK = 'YES';
            } else {
                if ($scope.obj.ItemList[i].ISSU_QTY > 0) {
                    $scope.obj.ItemList[i].ISSU_QTY -= 1;
                    if ($scope.obj.ItemList[i].ISSU_QTY === 0) {
                        $scope.obj.ItemList[i].CHECK = 'NO';
                    }
                } else {
                    $scope.obj.ItemList[i].CHECK = 'NO';
                }

            }

        };

        $scope.PutQty_Click = function (i) {
            $scope.obj.ItemList[i].CHECK = 'YES';

            //$scope.NewQtyData = {};
            //$scope.NewQtyData = [];
            //for (var i = 0; i < $scope.obj.ItemList.length; i++) {
            //    if (ind === i) {
            //        $scope.NewObj = {};
            //        $scope.NewObj.T_STORE_ID = $scope.obj.ItemList[i].T_STORE_ID;
            //        $scope.NewObj.T_ITEM_ID = $scope.obj.ItemList[i].T_ITEM_ID;
            //        $scope.NewObj.T_LANG2_NAME = $scope.obj.ItemList[i].T_LANG2_NAME;
            //        $scope.NewObj.STOCK = $scope.obj.ItemList[i].STOCK;
            //        $scope.NewObj.ISSU_QTY = $scope.obj.ItemList[i].ISSU_QTY;
            //        $scope.NewObj.CHECK = 'YES';
            //        $scope.NewQtyData.push($scope.NewObj);

            //    } else {
            //        $scope.NewObj = {};
            //        $scope.NewObj.T_STORE_ID = $scope.obj.ItemList[i].T_STORE_ID;
            //        $scope.NewObj.T_ITEM_ID = $scope.obj.ItemList[i].T_ITEM_ID;
            //        $scope.NewObj.T_LANG2_NAME = $scope.obj.ItemList[i].T_LANG2_NAME;
            //        $scope.NewObj.STOCK = $scope.obj.ItemList[i].STOCK;
            //        $scope.NewObj.ISSU_QTY = $scope.obj.ItemList[i].ISSU_QTY;
            //        $scope.NewObj.CHECK = $scope.obj.ItemList[i].CHECK;
            //        $scope.NewQtyData.push($scope.NewObj);
            //    }
            //}
            //$scope.obj.ItemList = $scope.NewQtyData;
            //$scope.NewQtyData = [];
        }

        $scope.SaveItem_Click = function () {
            $scope.Save_40List = [];
            for (var i = 0; i < $scope.obj.ItemList.length; i++) {
                if ($scope.obj.ItemList[i].CHECK === 'YES') {
                    $scope.saveObj = {};
                    $scope.saveObj.T_ITEM_CODE = $scope.obj.ItemList[i].T_ITEM_ID;
                    $scope.saveObj.T_DOSE_DURATION_CODE = $scope.obj.ItemList[i].ISSU_QTY;
                    $scope.Save_40List.push($scope.saveObj);
                }
            }

            $scope.T_REQUEST_ID = $scope.obj.pInfo.T_REQUEST_ID;
            var save = T74131Service.saveSuggestedMedicine($scope.T_REQUEST_ID, $scope.Save_40List);
            save.then(function (data) {
                alert(data);
                itemDatalist($scope.obj.pInfo.T_AMBU_REG_ID);
                $scope.checkTimer = 0;
            });

        };

        $scope.RequestForHospital = function () {
            var a = $scope.obj.k.selected.T_SITE_CODE;
            $scope.T_REQUEST_ID = $scope.obj.pInfo.T_REQUEST_ID;
            var save = T74131Service.saveRequestHospital($scope.T_REQUEST_ID, a);
            save.then(function (data) {
                alert(data);
                //itemDatalist($scope.obj.pInfo.T_AMBU_REG_ID);
            });
        }

        $scope.onImageClick = function (fLocation) {
            window.open(fLocation);
        };
    });
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


