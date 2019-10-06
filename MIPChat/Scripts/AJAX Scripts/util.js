$(function () {
    chat.client.onConnected = function (id, userName, allUsers) {

        alert(userName);
    });