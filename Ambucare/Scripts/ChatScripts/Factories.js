(function () {
    'use strict'
    //if (app != undefined) {
        app.factory("signalR", function ($rootScope) {
            var $hub = $.connection.chat;
            var connection = null;
            var signalR = {
                startHub: function () {
                    debugger;
                    console.log("started now");
                    connection = $.connection.hub.start();
                },
                //////////////////// SERVER METHODS/////////////////
                Login: function (username) {
                    debugger;
                    console.log("Login");
                    connection.done(function () {
                        $hub.server.login(username);
                    });
                },
                SendPrivateMessage: function (touser, message, name) {
                    debugger;
                    console.log("SendPrivateMessage");
                    connection.done(function () {
                        $hub.server.sendPrivateMessage(touser, message, name);
                    });
                },
                UpdateStatus: function (status) {
                    debugger;
                    console.log("UpdateStatus");
                    connection.done(function () {
                        $hub.server.updateStatus(status);
                    });
                },
                UserTyping: function (connectionid, msg) {
                    debugger;
                    console.log("UserTyping");
                    connection.done(function () {
                        $hub.server.userTyping(connectionid, msg);
                    });
                },
                ////////////////////// CLIENT METHODS////////////////////            
                joinroom: function (callback) {
                    debugger;
                    console.log("joinroom");
                    $hub.client.joinroom = callback;
                },
                UserEntered: function (callback) {
                    debugger;
                    console.log("UserEntered");
                    $hub.client.userEntered = callback;
                },
                UserLoggedOut: function (callback) {
                    debugger;
                    console.log("UserLoggedOut");
                    $hub.client.userLoggedOut = callback;
                },
                RecievingPrivateMessage: function (callback) {
                    debugger;
                    console.log("RecievingPrivateMessage");
                    $hub.client.sendPrivateMessage = callback;
                },
                GetOnlineUsers: function (callback) {
                    console.log("GetOnlineUsers");
                    $hub.client.getOnlineUsers = callback;
                },
                NewOnlineUser: function (callback) {
                    debugger;
                    console.log("NewOnlineUser");
                    $hub.client.newOnlineUser = callback;
                },
                NewOfflineUser: function (callback) {
                    debugger;
                    console.log("NewOfflineUser");
                    $hub.client.newOfflineUser = callback;
                },
                StatusChanged: function (callback) {
                    debugger;
                    console.log("StatusChanged");
                    $hub.client.statusChanged = callback;
                },
                IsTyping: function (callback) {
                    debugger;
                    console.log("IsTyping");
                    $hub.client.isTyping = callback;
                },
                UpdateConnectionId: function (callback) {
                    debugger;
                    console.log("UpdateConnectionId");
                    $hub.client.updateConnectionId = callback;
                }


            }
            return signalR;
        });
    //}
})();