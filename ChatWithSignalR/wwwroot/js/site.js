// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript cod

//$(function () {
//    //limit hub
//    name = prompt("Enter Your Name")
//     prox = $.connection.chatHub;

//    //start connection
//    $.connection.hub.start();

//    // subscribe callback methode
//    prox.client.sendAsync = function (n, m) {
//        $("ul").append("<li>" + n + ": " + m + " </li>");
//    }
//})

//function Send() {
//    prox.server.sendMessage(name, $("#txt").val());
//}


var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").withAutomaticReconnect().build();  // limit hub


connection.onclose(async (error) => {
    console.error(`Connection closed due to error: ${error}. Trying to reconnect...`);
   // await start();
});


///when recieve message make li  and assign the data to it

//subscribe events

connection.on("ReceiveMessage", function (user, message) {     
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
  
    li.textContent = `${user} says ${message}`;
});

connection.on("ReceiveMessageee", function (senderName, message) {
    const msg = senderName + ": " + message;
    const li = document.createElement("li");
    li.textContent = msg;
    document.getElementById("messagesList").appendChild(li);
});


connection.on("NewMember", function (Gname, name) {
    var li2 = document.createElement("li");
    document.getElementById("messagesList").appendChild(li2);

    li2.textContent = `${name} join ${Gname}`;


})

connection.on("Private", function (id , message) {
    var li2 = document.createElement("li");
    document.getElementById("messagesList").appendChild(li2);

    li2.textContent = `${id} say ${message}`;


})


connection.on("SendGroup", function (n, m, g) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);

    li.textContent = `${n} send : ${m} to ${g}`;
})

//start connection then when click on button invoke the event that i handle in hub class and assign the value that event needed


// click


connection.start().then(function () {
  
    $("#sendButton").on("click", function () {
        var fromUsr = $("#userInput").val();
        var message = $("#messageInput").val();

        connection.invoke("SendMessage", fromUsr, message);

    })

}).catch(function (err) {
    return console.error(err.toString());
});

$(function () {
    $("#joinButton").on("click", function () {
        var gname = $("#groupName").val();
        var fromUser = $("#userInput").val();
        connection.invoke("JoinGroup", gname, fromUser);
    })
});


$(function () {
    $("#sendGroupButton").on("click", function () {
        var groupName = $("#groupName").val();
        var fromUser = $("#userInput").val();
        var message = $("#messageInput").val();

        connection.invoke("SendToGroup", groupName, fromUser, message);

    })
});


$(function () {
    $("#ndButton").on("click", function () {
        var senderId = parseInt($("#senderIdInput").val());
        var targetUserId = parseInt($("#targetUserIdInput").val()) ;
        var message = $("#messageInputt").val();

        connection.invoke("SendMessageToUser", senderId, targetUserId, message).catch(function (err) {
            return console.error(err.toString());
        });

    })
});





