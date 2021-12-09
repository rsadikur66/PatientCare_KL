app.controller("T74131Controller",
    function ($scope, $rootScope, signalR, Flash, $compile, Data, T74131Service, $location) {

        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.t74098 = {};
        $scope.obj.k = {};
        $scope.obj.k.selected = {};
        $scope.checkCondition = 'MedicineList';
        $scope.patListClick = '1';

        var labelData = T74131Service.getlabeldata('T74131');
        labelData.then(function (data) {
            $scope.entity = data;
            $scope.PlaceHolder = $scope.entity.filter(function (a) { return a.T_LABEL_NAME == 'lblChatPlace'; })[0].T_LANG2_TEXT;
        });
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
        //-----Changes for reconnectivity--------------start---------------
        //---------------------------new  one--------start----------------------
        $scope.obj.chtReqNo = 0;
        $scope.obj.HospWithDoc = [];
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
                        //New-------Maybe needed to be removed-------------------
                        $scope.PrivateMessages = [];
                        //$scope.obj.HospWithDoc = [];
                        $scope.UserInPrivateChat = [];
                        $scope.chatuser = [];
                    });




                });
            }
            //else {
            //    var req = $scope.obj.HospWithDoc[0].docInfo[0].T_REQUEST_ID;
            //    if (req!=null) {
            //        var chkPat = T74131Service.chkPat(req);
            //        chkPat.then(function (data) {
            //            var dt = JSON.parse(data);
            //            if (dt > 0) {
            //                document.getElementById('divChtBox').style.display = 'none';
            //                //$scope.PrivateMessages = [];
            //                //$scope.obj.HospWithDoc = [];
            //                //$scope.UserInPrivateChat = [];
            //                //$scope.chatuser = [];
            //            } else {
            //                document.getElementById('divChtBox').style.display = 'block';    
            //            }        
            //        });
            //    }
                
            //}

            $scope.obj.reqList = [];
            var reqListForTeam = T74131Service.reqListForDoc();
            reqListForTeam.then(function (data) {
                var dt = JSON.parse(data);
                $scope.obj.chtReqNo = dt.length;
                if (dt.length > 0) {
                    $scope.obj.reqList = dt;
                }
            });

            //---------------------
            getAllTeam();

        }, 5000);
        $scope.onRcvPatReq = function (r, t) {
            if ($scope.obj.HospWithDoc.length > 0) {
                //alert('Please hold chat with current patient in order to communicate with another patient');
                alert($scope.getSingleMsg('S0054'));
            } else {
                if (r != undefined || r != null) {
                   // var con = confirm('Do you want to communicate with ' + t);
                    var con = confirm($scope.getSingleMsg('S0056') + t);
                    if (con) {
                        var onRcvPatReq = T74131Service.onRcvPatReq(r);
                        onRcvPatReq.then(function (data) {
                            var dt = JSON.parse(data);
                            alert(dt);
                            $scope.PrivateMessages = [];
                            $scope.obj.HospWithDoc = [];
                            $scope.UserInPrivateChat = [];
                            $scope.chatuser = [];

                        });
                    }
                }

            }



        }
        $scope.onRcvPatLst = function () {
            if ($scope.obj.reqList.length > 0) {
                document.getElementById('teamList').style.display = "block";
            } else {
                document.getElementById('teamList').style.display = "none";
            }


        }
        $scope.onRmvPatLst = function () {
            document.getElementById('teamList').style.display = "none";
        }

        $scope.obj.onChatClose = function () {
           // var con = confirm("Do you want to hold this patient?");
            var con = confirm($scope.getSingleMsg('S0057'));
            if (con) {
                var closeChat = T74131Service.closeChat($scope.obj.HospWithDoc[0].docInfo[0].T_REQUEST_ID);
                closeChat.then(function (data) {
                    var dt = JSON.parse(data);
                    alert(dt);
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
                });
            }
        }

        //---------------------------new  one--------End----------------------
        //-----Changes for reconnectivity--------------End---------------

        // T74131Service.setDoc();
        //var docList = function () {
        //var data = T74131Service.GetSite();
        //data.then(function (dt) {
        //    $scope.obj.Hos_List = dt.data;
        //    function UniqueArray(collection, keyname) {
        //        var output = [],
        //            keys = [];

        //        angular.forEach(collection,
        //            function (item) {
        //                var key = item[keyname];
        //                if (keys.indexOf(key) === -1) {
        //                    keys.push(key);
        //                    output.push(item);
        //                }
        //            });
        //        return output;
        //    };
        //    $scope.obj.HosList = UniqueArray($scope.obj.Hos_List, "T_SITE_CODE");
        //    $scope.obj.HospWithDoc = [];
        //    for (var i = 0; i < $scope.obj.HosList.length; i++) {
        //        $scope.obj.dummy = {};
        //        $scope.obj.dummy.HOSPITAL_NAME = $scope.obj.HosList[i].HOSPITAL_NAME;
        //        $scope.obj.dummy.T_SITE_CODE = $scope.obj.HosList[i].T_SITE_CODE;
        //        $scope.obj.dummy.DOC_NO = 0;
        //        $scope.obj.dummy.docInfo = [];
        //        for (var j = 0; j < $scope.obj.Hos_List.length; j++) {
        //            if ($scope.obj.dummy.T_SITE_CODE === $scope.obj.Hos_List[j].T_SITE_CODE) {
        //                $scope.obj.dummy2 = {};
        //                $scope.obj.dummy2.T_EMP_NO = $scope.obj.Hos_List[j].T_EMP_NO;
        //                $scope.obj.dummy2.T_EMP_CODE = $scope.obj.Hos_List[j].T_EMP_CODE;
        //                $scope.obj.dummy2.HDM_DOC_CODE = $scope.obj.Hos_List[j].HDM_DOC_CODE;
        //                $scope.obj.dummy2.DOCTOR = $scope.obj.Hos_List[j].DOCTOR;
        //                $scope.obj.dummy2.HDM_SPCLTY_CODE = $scope.obj.Hos_List[j].HDM_SPCLTY_CODE;
        //                $scope.obj.dummy2.HDM_SPCLTY_DSCRPTN = $scope.obj.Hos_List[j].HDM_SPCLTY_DSCRPTN;
        //                $scope.obj.dummy.docInfo.push($scope.obj.dummy2);
        //            }
        //        }
        //        $scope.obj.HospWithDoc.push($scope.obj.dummy);

        //    }
        //});
        //} 
        //if ($location.absUrl().split('/')[5] === undefined) {
        //    //-----------------------Doctor List-When Ambulance Login-----------
        //    docList();
        //}
        $scope.checkTimer = 0;
        setInterval(function () {
            if ($scope.UserInPrivateChat != null || $scope.UserInPrivateChat != undefined) {
                if ($scope.UserInPrivateChat.TYPE == 'Pat') {
                    for (var i = 0; i < $scope.obj.ItemList.length; i++) {
                        if ($scope.obj.ItemList[i].ISSU_QTY !== 0) {
                            $scope.checkTimer = 1;
                        } 
                    }
                    if ($scope.checkTimer === 0) {
                        OpenDiv();
                        //alert('oDDk');
                    }



                    $scope.$apply();
                }
            }
            //else {
            //    alert($scope.obj.HospWithDoc.length);
            //}


        }, 5000);
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
                        var nm_from = dt[i].T_SENDER_TYPE == 'Pat' ? user.DOCTOR : $scope.UserName;
                        // $scope.PrivateMessages.push({ to: 0, from: nm_from, message: dt[i].T_TEXT, ConnectionId: 0 });
                        $scope.PrivateMessages.push({ T_TIME: dt[i].T_TIME, U_NAME: dt[i].U_NAME, T_USER_ID: dt[i].T_USER_ID,to: 0, from: nm_from, message: dt[i].T_TEXT, ConnectionId: 0, T_SENDER_TYPE: dt[i].T_SENDER_TYPE, T_FILE_LOC: dt[i].T_FILE_LOC, ext: (dt[i].T_FILE_LOC != null ? dt[i].T_FILE_LOC.split('.').pop() : null) });

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
                    //document.getElementById("divHosName" + i).className = "head-border";
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
        var uploadedCount = 0;

        $scope.files = [];
        $scope.file = [];
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

        //--------------------------------Header-----------------------------------
        $rootScope.RootPrivateMessages = [];
        $scope.groups = [];
        $rootScope.updateMessage = function () {
            $scope.groups = _.groupBy($rootScope.RootPrivateMessages, "from");
            $scope.messageCount = $rootScope.RootPrivateMessages.length;
        }
        //--------------------------------Body--------------------------------

        $scope.rooms = [];
        signalR.startHub();
        $scope.activeRoom = '';
        $scope.chatHistory = [];
        $scope.Users = [];
        $scope.RoomsLoggedId = [];
        $scope.typemsgdisable = true;
        //-----------------------------------------------Comment Start---------
        //    signalR.UserEntered(function (room, user, cid) {
        //       if ($scope.activeRoom == room && user != '') {
        //            var result = $.grep($scope.users, function (e) { return e.name == user; })
        //            console.log("----------");
        //            console.log(result);
        //            if (result != undefined || result != null) {
        //                $scope.users.push({ name: user, ConnectionId: cid });
        //                $scope.$apply();
        //            }
        //        }
        //    });
        //    signalR.UserLoggedOut(function (room, user) {

        //        if ($scope.activeRoom == room && user != '') {
        //            $scope.users = $scope.users.filter(function (themObjects) {
        //                return themObjects.name != user;
        //            });
        //            $scope.$apply();

        //        }
        //});
        //-----------------------------------------------Comment End---------

        //  Flash.add('success', message, 'custom-class')

        //signalR.Login($location.absUrl().split('/')[5],
        //    JSON.parse(sessionStorage.getItem("UserName")),
        //    JSON.parse(sessionStorage.getItem("UserId")));

        ///////////////// server


        $scope.UsersCount = 0;
        $scope.bubblesCount = [];
        $scope.maxBubbles = 10;
        //-----------------------------------------------Comment Start---------
        //    $scope.ClosePrivateWindow = function () {
        //        $scope.ShowPrivateWindow = false;
        //        $scope.UserInPrivateChat = null;

        //        // close_popup();
        //}
        //-----------------------------------------------Comment End---------
        // $("#PrivateChatArea").css("display", "block");
        $scope.UserInPrivateChat = null;
        $scope.ShowPrivateWindow = false;
        $scope.PrivateMessages = [];
        $scope.currentprivatemessages = {};
        $scope.pvtmessage = '';
        $scope.OpenPrivatewindow = function (index) {
            var user = $scope.users[index];
            //  var conId = '#' + user.ConnectionId;
            $scope.ShowPrivateWindow = true;
            $scope.UserInPrivateChat = user;
            console.log("CID :" + $scope.UserInPrivateChat.Connectionid);
            //$scope.$apply();
            $scope.$evalAsync();
            // $scope.createPrivateChatWindow($scope.$parent.UserName, conId, user.name)
        }

        $scope.SendPrivateMessage = function (e, u, id, name) {
            if (e.keyCode == 13) {
                if (id == null || id == undefined) {
                    $scope.obj.t74098.T_RECIEVER_ID = u.T_U_ID;
                    $scope.obj.t74098.T_REQUEST_ID = u.T_REQUEST_ID;
                    $scope.obj.t74098.T_SENDER_TYPE = u.TYPE == 'Pat' ? 'Doc' : 'Pat';
                    $scope.obj.t74098.T_TEXT = $scope.chatText.trim();
                    T74131Service.chatHistory($scope.obj.t74098);
                    $scope.chatText = '';
                } else {
                    //  if ($scope.UserLeft == false) { 
                    var txt = $("#txt" + id)
                    if (txt.val().trim() != '') {
                        //$("#txt392331de-393d-427d-a631-3f52caf06dd1").val()
                        var txt = $("#txt" + id)
                        console.log(txt);
                        signalR.SendPrivateMessage(id, txt.val().trim(), name);
                        //  $scope.pvtmessage = '';
                        $scope.obj.t74098.T_RECIEVER_ID = u.T_U_ID;
                        $scope.obj.t74098.T_REQUEST_ID = u.T_REQUEST_ID;
                        $scope.obj.t74098.T_SENDER_TYPE = u.TYPE == 'Pat' ? 'Doc' : 'Pat';
                        $scope.obj.t74098.T_TEXT = $scope.chatText.trim();
                        T74131Service.chatHistory($scope.obj.t74098);
                        txt.val('');
                    }



                }


                // }
                //else {
                //    Flash.create('danger', 'User is offline');
                //}
            }
        }
        $scope.OnlineUsers = [];
        //$scope.obj.HospWithDoc = [];
        signalR.GetOnlineUsers(function (onlineUsers, user) {
            var a = JSON.parse(onlineUsers);
            //$scope.OnlineUsers = $.parseJSON(onlineUsers);
            //$scope.obj.HospWithDoc = $scope.OnlineUsers[0].HospWithDoc;

            //$scope.OnlineUsers = onlineUsers;
            //$scope.obj.HospWithDoc = $scope.OnlineUsers.HospWithDoc; 
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
                        //if (user.U_TYPE=='p') {
                        //    if (user.HospWithDoc[i].T_U_ID == a[l].id) {
                        //        tc.src = "/Images/Chat_img/Circle_green.png";
                        //    }   
                        //} else {
                        //    if (user.HospWithDoc[i].docInfo[k].T_U_ID == a[l].id) {
                        //        tc.src = "/Images/Chat_img/Circle_green.png";
                        //    }       
                        //}
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
                    //tc.src = user.HospWithDoc[i].docInfo[k].HDM_SPCLTY_DSCRPTN;
                }

                t.docInfo = x;
                s.push(t);


                //for (var j = 0; j < a.length; j++) {
                //    if (user.HospWithDoc[i].T_U_ID == a[j].id) {
                //        document.getElementById("imgDoc" + i + 0).src = "/Images/Chat_img/Circle_green.png";
                //    }
                //}
            }
            $scope.obj.HospWithDoc = s;
            //$scope.obj.HospWithDoc = a[0].HospWithDoc; 


            //if ($scope.obj.HospWithDoc==undefined) {
            //    docList();
            //}
            //for (var i = 0; i < $scope.OnlineUsers.length; i++) {
            //    for (var j = 0; j < $scope.obj.HospWithDoc.length; j++) {
            //        for (var k = 0; k < $scope.obj.HospWithDoc[j].docInfo.length; k++) {
            //            if ($scope.obj.HospWithDoc[j].docInfo[k].T_EMP_CODE == $scope.OnlineUsers[i].id) {
            //                $scope.obj.HospWithDoc[j].DOC_NO++;
            //                document.getElementById("imgDoc" + j + k).src = "/Images/Chat_img/Circle_green.png";
            //            }
            //        }
            //    }
            //}


            console.log($scope.OnlineUsers);
            $scope.$apply();


            $scope.$evalAsync();
        });
        //$scope.ChangeStatus = function (status) {

        //    signalR.UpdateStatus(status);
        //}
        signalR.NewOnlineUser(function (user) {

            for (var i = 0; i < $scope.obj.HospWithDoc.length; i++) {
                for (var k = 0; k < $scope.obj.HospWithDoc[i].docInfo.length; k++) {
                    //if ($scope.obj.HospWithDoc[i].docInfo[k].TYPE == 'Pat') {
                    //    if ($scope.obj.HospWithDoc[i].docInfo[k].T_U_ID == user.id) {
                    //        $scope.obj.HospWithDoc[i].DOC_NO++;
                    //        document.getElementById("imgDoc" + i + k).src = "/Images/Chat_img/Circle_green.png";
                    //    } 
                    //}
                    //else if ($scope.obj.HospWithDoc[i].docInfo[k].TYPE == 'Doc') {
                    //    if ($scope.obj.HospWithDoc[i].docInfo[k].T_EMP_CODE == user.id) {
                    //        $scope.obj.HospWithDoc[i].DOC_NO++;
                    //        document.getElementById("imgDoc" + i + k).src = "/Images/Chat_img/Circle_green.png";
                    //    } 
                    //}
                    if ($scope.obj.HospWithDoc[i].docInfo[k].T_U_ID == user.id) {
                        $scope.obj.HospWithDoc[i].DOC_NO++;

                        $scope.obj.HospWithDoc[i].docInfo[k].ConnectionId = user.ConnectionId;
                        $scope.obj.HospWithDoc[i].docInfo[k].id = user.id;
                        $scope.obj.HospWithDoc[i].docInfo[k].name = user.name;

                        document.getElementById("imgDoc" + i + k).src = "/Images/Chat_img/Circle_green.png";
                    }
                }
            }
            //$scope.OnlineUsers.push(user);
            $scope.$apply();
            //var message = '<strong> !!</strong>' + user.name + ' in online';
            //Flash.create('success', message, 'custom-class');
        });
        signalR.UpdateConnectionId(function (oid, nid) {
            debugger;
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
        $scope.UserLeft = false;
        //----------------------------------Comment-------------------------
        //$scope.SkeyPress = function (e, id) {
        //    //
        //    if (e.which == 13) {
        //        $scope.SendPrivateMessage(id, name);
        //        $scope.usertyping = ''
        //    }
        //    else if (e.which == 46 || e.which == 8) {
        //        signalR.UserTyping(id, 'Deleting..');
        //        window.setTimeout(function () {
        //            $scope.usertyping = '';
        //        }, 500);
        //    }
        //    else {
        //        //$scope.UserInPrivateChat.ConnectionId
        //        signalR.UserTyping(id, 'Typing..');
        //        window.setTimeout(function () {
        //            $scope.usertyping = '';
        //        }, 500);
        //    }
        //}
        //----------------------------------Comment-------------------------


        // PrivateMessage($index)
        $scope.openPvtChat = function (pi, index) {
            $scope.obj.pi = pi;
            $scope.obj.i = index;

            console.log(index);
            //var user = $scope.OnlineUsers[index];
            var user = $scope.obj.HospWithDoc[pi].docInfo[index];
            $scope.UserInPrivateChat = user;
            var sendREq = T74131Service.sendREq(user.T_REQUEST_ID);
            sendREq.then(function (data) {
                OpenDiv();
                hosName();
            });
            //$scope.register_popup(user.ConnectionId, user.name);
        }
        //$scope.closePvtChat = function (id) {

        //    close_popup(id);
        //}
        //-----------------------------Comment-----------------------------------------
        //$scope.PrivateMessage = function (index) {

        //    var user = $scope.OnlineUsers[index];
        //    // $scope.ShowPrivateWindow = true;
        //    $scope.UserInPrivateChat = user;
        //    // console.log($scope.OnlineUsers);    
        //    //$scope.$apply();
        //    $scope.$evalAsync();
        //};
        //-----------------------------Comment-----------------------------------------


        //-----------------------------Comment-----------------------------------------
        //$scope.usertyping = '';
        //signalR.IsTyping(function (connectionid, msg) {

        //    if ($scope.UserInPrivateChat != undefined)
        //        if ($scope.UserInPrivateChat.ConnectionId == connectionid)
        //            $scope.usertyping = msg;
        //        else
        //            $scope.usertyping = '';
        //    // $scope.$apply();
        //    $scope.$evalAsync();
        //    window.setTimeout(function () {
        //        $scope.usertyping = '';
        //        //  $scope.$apply();
        //        $scope.$evalAsync();
        //    }, 500);
        //});
        //-----------------------------Comment-----------------------------------------


        //-----------------------------Comment-----------------------------------------
        //signalR.StatusChanged(function (connectionId, status) {

        //    $.each($scope.OnlineUsers, function (i) {
        //        if ($scope.OnlineUsers[i].ConnectionId === connectionId) {
        //            $scope.OnlineUsers[i].status = status;
        //        }
        //    });
        //    // $scope.OnlineUsers.push(user);
        //    // $scope.$apply();
        //    $scope.$evalAsync();

        //});

        //-----------------------------Comment-----------------------------------------


        $rootScope.RootPrivateMessages = [];
        signalR.RecievingPrivateMessage(function (toname, fromname, msg, connectionId) {
            // if ($scope.ShowPrivateWindow == false) {
            //     $scope.ShowPrivateWindow = true;
            //}

            $scope.openchats = [];
            // var msgBdy = { room: r, msgx: { message: msg.message, sender: msg.sender, css: msg.css } };
            //$scope.chatHistory.push(msgBdy);
            // var user = $scope.OnlineUsers[connectionId];
            // console.log(user);
            $scope.PrivateMessages.push({ to: toname, from: fromname, message: msg, ConnectionId: connectionId });

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
            //if (fromname!=$rootScope.)
            //$scope.register_popup(connectionId, fromname);
            $scope.$evalAsync();
            // $scope.$evalAsync();
            $rootScope.$evalAsync();
            $rootScope.updateMessage(); //$evalAsync();
            /// To Scroll the message window.
            if ($("#PrivateChatArea div.panel-body").length == 1) {
                var $container = $("#PrivateChatArea div.panel-body");
                $container[0].scrollTop = $container[0].scrollHeight;
                $container.animate({ scrollTop: $container[0].scrollHeight }, "fast");
            }
            // $scope.AddMessageToRoom(msgBdy);
        });
        //$("#PrivateChatArea").css("display", "block");

        ///////////////////////////////New Code For Multiple Chat Windows////////////////////////
        //// @$scope.popupsId=[];
        ////$scope.$parent.UserName
        //$scope.register_popup = function (id, name) {
        //    for (var iii = 0; iii < popups.length; iii++) {
        //        //already registered. Bring it to front.
        //        if (id == popups[iii]) {
        //            Array.remove(popups, iii);
        //            popups.unshift(id);
        //            calculate_popups();
        //            return;
        //        }
        //    }

        //    var chatBoxElemet = '<div class="panel-body" style="overflow-y:scroll; min-height:200px; max-height:200px;">'
        //    chatBoxElemet += '<ul class="chat">'
        //    chatBoxElemet += '<li ng-class="(msg.from == ' + name + ')? \'left\' : \'right\'" class="clearfix" ng-repeat="msg in PrivateMessages | filter:({ConnectionId: \'' + id + '\'})">'
        //    chatBoxElemet += '<div class="chat-body clearfix">'
        //    chatBoxElemet += '<div class="header">'
        //    chatBoxElemet += '<i ng-class="(msg.from != ' + name + ' )? \'sender\' : \'reciver\'" class="fa fa-user"></i>'
        //    chatBoxElemet += '<strong class="primary-font">{{msg.from}}</strong>';
        //    chatBoxElemet += '<small ng-class="(msg.from == ' + name + ' )? \'pull-left\' : \'pull-right\'"';
        //    chatBoxElemet += 'class="text-muted"></small>';
        //    chatBoxElemet += '</div>';
        //    chatBoxElemet += '<div class="chat-message">';
        //    chatBoxElemet += '{{msg.message}}';
        //    chatBoxElemet += '</div>';
        //    chatBoxElemet += '</div>';
        //    chatBoxElemet += '</li>';
        //    chatBoxElemet += '</ul>';
        //    chatBoxElemet += '</div>';
        //    chatBoxElemet += '<div class="panel-footer">';
        //    chatBoxElemet += '<div style="min-height:9px; font-size:7px;" id="div' + id + '">';
        //    chatBoxElemet += '{{usertyping}}';
        //    chatBoxElemet += '</div>';
        //    chatBoxElemet += '<div class="input-group">';
        //    //ng-model="pvtmessage"
        //    chatBoxElemet += '<input id="txt' + id + '" type="text" class="form-control input-sm"  placeholder="Type your message here..." ng-keypress="SkeyPress($event,\'' + id + '\',\'' + name + '\')" />';
        //    chatBoxElemet += '<span class="input-group-btn">';
        //    chatBoxElemet += '<button class="btn btn-warning btn-sm" id="btn-chat" ng-click="SendPrivateMessage(\'' + id + '\',\'' + name + '\')">';
        //    chatBoxElemet += 'Send';
        //    chatBoxElemet += '</button>';
        //    chatBoxElemet += '</span>';
        //    console.log(id);
        //    var elementE = '<div class="popup-box chat-popup" id="' + id + '">';
        //    elementE = elementE + '<div class="popup-head">';
        //    elementE = elementE + '<div class="popup-head-left">' + name + '</div>';
        //    elementE = elementE + '<div class="popup-head-right"><a  data-ng-click="closePvtChat(\'' + id + '\')">&#10005;</a></div>';
        //    elementE = elementE + '<div style="clear: both"></div></div><div class="popup-messages">';
        //    elementE = elementE + chatBoxElemet;
        //    elementE = elementE + '</div></div>';
        //    angular.element(document.getElementById('ChatRoomsContainer')).append($compile(elementE)($scope));
        //    popups.unshift(id);
        //    calculate_popups();
        //}
        //function calculate_popups() {
        //    var width = window.innerWidth;
        //    if (width < 540) {
        //        total_popups = 0;
        //    }
        //    else {
        //        width = width - 200;
        //        //320 is width of a single popup box
        //        total_popups = parseInt(width / 320);
        //    }
        //    display_popups();
        //}
        //Array.remove = function (array, from, to) {
        //    var rest = array.slice((to || from) + 1 || array.length);
        //    array.length = from < 0 ? array.length + from : from;
        //    return array.push.apply(array, rest);
        //};
        ////this variable represents the total number of popups can be displayed according to the viewport width
        //var total_popups = 0;
        ////arrays of popups ids
        //var popups = [];
        ////this is used to close a popup
        //function close_popup(id) {
        //    for (var iii = 0; iii < popups.length; iii++) {
        //        if (id == popups[iii]) {
        //            Array.remove(popups, iii);
        //            document.getElementById(id).style.display = "none";
        //            calculate_popups();
        //            return;
        //        }
        //    }
        //}
        ////displays the popups. Displays based on the maximum number of popups that can be displayed on the current viewport width
        //function display_popups() {
        //    var right = 200;
        //    var iii = 0;
        //    for (iii; iii < total_popups; iii++) {
        //        if (popups[iii] != undefined) {
        //            var element = document.getElementById(popups[iii]);
        //            element.style.right = right + "px";
        //            right = right + 300;
        //            element.style.display = "block";
        //        }
        //    }
        //    for (var jjj = iii; jjj < popups.length; jjj++) {
        //        var element = document.getElementById(popups[jjj]);
        //        element.style.display = "none";
        //    }
        //}


        //---------------------------------------------Imran---------------------------------------------------
        $scope.obj.pInfo = {};

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
                        if ($scope.checkCondition === 'MedicineList') {
                            $scope.Medicine = true;
                           // $scope.ActivePatientList = false;
                           // $scope.checkCondition = 'ActPatList';
                        } else {
                            $scope.Medicine = false;
                           // $scope.ActivePatientList = true;
                           // $scope.checkCondition = 'MedicineList';
                        }
                        

                        $scope.obj.pInfo.T_REQUEST_ID = dt[0].T_REQUEST_ID;
                        $scope.obj.pInfo.T_REF_DOC_CODE = dt[0].T_REF_DOC_CODE;
                        $scope.obj.pInfo.REFER_HOS = dt[0].REFER_HOS;
                        $scope.obj.pInfo.HOSNAME = dt[0].HOSNAME;
                        $scope.obj.pInfo.HOS_ID = dt[0].HOS_ID;
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
                            ambulanceservice();
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
        //var his = T74131Service.GetChatHistory('4921', 'Doc');
        //his.then(function (data) {
        //    var dt = JSON.parse(data);
        //    $scope.obj.ChatHistoryList = dt;
        //    document.getElementById('chatHistory').style.display = "block";
        //});
        $scope.getChatHistory = function () {
            var pi = $scope.obj.pi;
            var index = $scope.obj.i;
            var user = $scope.obj.HospWithDoc[pi].docInfo[index];
            var his = T74131Service.GetChatHistory(user.T_REQUEST_ID, user.T_U_ID, user.TYPE);
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




                //$scope.obj.ChatHistoryList = dt;
                document.getElementById('chatHistory').style.display = "block";
            });
        }
        ambulanceservice();
        function ambulanceservice() {
          var serv = T74131Service.getServiceData($scope.obj.pInfo.T_REQUEST_ID);
            serv.then(function (data) {
                $scope.obj.ServiceList = JSON.parse(data);
                $scope.obj.T74074.totalPrice = 0;
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
                    $scope.obj.ItemList[i].CHECK = 'NO';
                    $scope.obj.ItemList[i].ISSU_QTY = 0;
                }
            }

            $scope.T_REQUEST_ID = $scope.obj.pInfo.T_REQUEST_ID;
            var save = T74131Service.saveSuggestedMedicine($scope.T_REQUEST_ID, $scope.Save_40List);
            save.then(function (data) {
                $scope.checkTimer = 0;
                alert(data);
                itemDatalist($scope.obj.pInfo.T_AMBU_REG_ID);
                
            });

        };
        $scope.RequestForHospital = function () {
          var a = $scope.obj.k.selected.T_SITE_CODE;
         
          if (a !== null && a !== undefined && a !== "" && a !== $scope.obj.pInfo.HOS_ID) {
            $scope.T_REQUEST_ID = $scope.obj.pInfo.T_REQUEST_ID;
            var chkReqHos=T74131Service.chkReqHos($scope.T_REQUEST_ID);
            chkReqHos.then(function (data) {
                var dt = JSON.parse(data);
                if (dt=='') {
                    var save = T74131Service.saveRequestHospital($scope.T_REQUEST_ID, a);
                    save.then(function (data) {
                        alert(data);

                        var a = $scope.getLatLong();
                        var t74026 = {};
                        t74026.T_REQUEST_ID = $scope.T_REQUEST_ID;
                        t74026.T_EVENT_ID = 8;
                        t74026.T_LATITUDE = a.lt;
                        t74026.T_LONGITUDE = a.ln;
                        T74131Service.save26(t74026);
                        //itemDatalist($scope.obj.pInfo.T_AMBU_REG_ID);
                    });
                } else {
                    alert(dt);
                }
                
            });
            } else {
               // $scope.getSingleMsg('S0139')
            if (a === $scope.obj.pInfo.HOS_ID) {
              alert($scope.getSingleMsg('S0140'));
              } else {
                alert($scope.getSingleMsg('S0139'));
            }
                
            }

            }

        $scope.onImageClick = function (fLocation) {
            window.open(fLocation);
        };
        $scope.onConsoleClick = function () {
            //window.open(fLocation);
            var console = T74131Service.getConsoleApp();
            console.then(function (data) {
                if (data) {
                    window.open(data);
                }

            });
        };
        
        $scope.getPatList = function () {
            //---- comment Ruhul start

            //var a = $location.host();
            //var k = '';
            //if ($scope.vrDir!="") {
            //    k +=  $scope.vrDir;
            //} else {
            //    //http://localhost:5871/Transaction/T74131
            //    k += ':5871';
            //}
            //var s = a + k + '/Queries/Q74144';
            //window.open('http://' + s);

            //---- comment Ruhul end
            
            if ($scope.checkCondition === 'MedicineList' && $scope.patListClick === '2') {
                if ($scope.obj.HospWithDoc.length > 0) {
                    $scope.openPvtChat(0, 0);
                    $scope.Medicine = true;
                    $scope.ActivePatientList = false;
                    $scope.checkCondition = 'MedicineList';
                    $scope.patListClick = '1';
                    
                }
            } else {
               
                if ($scope.patListClick === '1') {
                    $scope.Medicine = false;
                    $scope.ActivePatientList = true;
                    $scope.checkCondition = 'ActPatList';
                    $scope.patListClick = '2';
                } else {
                    if ($scope.obj.HospWithDoc.length > 0) {
                        $scope.openPvtChat(0, 0);
                        $scope.Medicine = true;
                        $scope.ActivePatientList = false;
                        $scope.checkCondition = 'MedicineList';
                        $scope.patListClick = '1'; 
                    }
                   
                }
                
            }
            
        };
        $scope.onChatStart = function (e, req, user, f) {
            if ($scope.obj.HospWithDoc.length > 0) {
                //alert('Please hold chat with current patient in order to communicate with another patient');
                alert($scope.getSingleMsg('S0054'));
            } else {
                if (e == null) {
                    //var con = confirm('Do you want to communicate with ' + user);
                    var con = confirm($scope.getSingleMsg('S0056') + user);
                    if (con) {
                        var doc = T74131Service.setDoc_new(req, user);
                        doc.then(function (datas) {
                            var dts = JSON.parse(datas);
                            if (dts != '') {
                                if (dts != 'Ok') {
                                    $scope.PrivateMessages = [];
                                    $scope.obj.HospWithDoc = [];
                                    $scope.UserInPrivateChat = [];
                                    $scope.chatuser = [];
                                    getAllTeam();
                                } else {
                                    alert(dts);
                                }   
                            }
                            
                        });
                        //var onRcvPatReq = T74131Service.onRcvPatReq(req);
                        //onRcvPatReq.then(function (data) {
                        //    var dt = JSON.parse(data);
                        //    alert(dt);
                            
                        //});
                    }
                    
                } else {
                    if ($scope.UserId == e) {
                        if (f != null) {
                           // var con1 = confirm('Do you want to communicate with ' + user);
                            var con1 = confirm($scope.getSingleMsg('S0056') + user);
                            if (con1) {
                                var doc_New = T74131Service.setNewConv(req);
                                doc_New.then(function (data) {
                                    var dt = JSON.parse(data);
                                    if (dt != '') {
                                        $scope.PrivateMessages = [];
                                        $scope.obj.HospWithDoc = [];
                                        $scope.UserInPrivateChat = [];
                                        $scope.chatuser = [];
                                        getAllTeam();
                                    }
                                });
                                //var onRcvPatReq = T74131Service.onRcvPatReq(req);
                                //onRcvPatReq.then(function (data) {
                                //    var dt = JSON.parse(data);
                                //    alert(dt);

                                //});
                            }
                        }
                    } else {
                       // alert('This Team is busy with ' + e);
                        alert($scope.getSingleMsg('S0055')+e);
                    }

                }    
            }
            
        }
        getAllTeam();
        function getAllTeam() {
            var allData = T74131Service.getAllTeamData();
            allData.then(function (data) {
                $scope.obj.AllTeamData = JSON.parse(data);
                
                if ($scope.obj.HospWithDoc.length>0) {
                    var reqId = $scope.obj.HospWithDoc[0].docInfo[0].T_REQUEST_ID;
                    if (reqId!=null) {
                        var req = $scope.obj.AllTeamData.filter(function (k) { return (k.T_REQUEST_ID == reqId); }).length;
                        if (req==0) {
                            
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
                                    //New-------Maybe needed to be removed-------------------
                                    $scope.PrivateMessages = [];
                                    //$scope.obj.HospWithDoc = [];
                                    $scope.UserInPrivateChat = [];
                                    $scope.chatuser = [];
                                    $scope.Medicine = false;
                                });




                            });
                        }
                    }
                }






            });
        }
        $scope.getPatMsg = function (e) {
            $scope.obj.patMsg = [];
            var patMsg = T74131Service.getPatMsg(e);
            patMsg.then(function(data) {
                var dt = JSON.parse(data);
                if (dt.length>0) {
                  $scope.obj.patMsg = dt;
                 document.getElementById("ShowPatientMsgPopup").style.display = "block";
                }
               
            });
        }

        $scope.CloseActivePopup = function () {
            $scope.obj.patMsg = [];
            document.getElementById("ShowPatientMsgPopup").style.display = "none";
        }

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

