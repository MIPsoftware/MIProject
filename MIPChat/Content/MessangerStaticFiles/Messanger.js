var chat_id = null;
var user_guid = document.cookie.split('UserGuid=')[1];



$(document).ready(function () {
    $('#action_menu_btn').click(function () {
        $('.action_menu').toggle();
    });
});



$("#on_create_chat").click(() => {
    $.ajax({
        url: "Messanger/GetAllUsersToChat",
        type: "POST",
        data: { correntUser: user_guid },
        success: (data) => { $('#users_list_row').html(data); },
        dataType: "html"
    });
});

$('#chat_field').ready(() => {
    $.ajax({
        url: "Messanger/GetAllChatsForUser",
        type: "POST",
        data: { userId: user_guid },
        success: (data) => { $('#chat_field').html(data); },
        dataType: "html"
    });
});





$("#CreateNewGroupButton").click(() => {
    toAddList = new Array();


    $(".userinput").each((iter, item) => {
        if (item.checked) {
            toAddList.push(item.attributes[2].value);
        }
    });
    toAddList.push(user_guid);


    let ChatName = $('#GroupName1').val();

    if (ChatName !== null) {


        $.ajax({
            url: "Messanger/CreateMessageOrChat",
            data: { name: ChatName, usersGuids: toAddList },
            type: "POST",
            traditional: true,
            dataType: "text",
            success: () => {

                $.ajax({
                    url: "Messanger/GetAllChatsForUser",
                    type: "POST",
                    data: { userId: user_guid },
                    success: (data) => { $('#chat_field').html(data); },
                    dataType: "html"
                });
            }
        });

    }
    else {
        // НЕ УДАЛОСЬ СОЗДАТЬ ЧАТИК
        return;
    }

});



$("#groups").on("click", ".group", function () {
    chat_id = $(this).attr("data-group_id");

    $('.group').css({ "background-color": "rgba(0,0,0,0)", cursor: "pointer" });


    $(this).css({ "background-color": "rgba(0,0,0,0.3)", cursor: "default" });

    $("#currentGroup").val(chat_id);
    currentGroupId = chat_id;

    $.ajax({
        url: "Messanger/FindChat",
        type: "POST",
        data: { ChatID: chat_id },
        success: (data) => { $('#msg_field').html(data); },
        dataType: "html"
    });

    $(function () {
        var messageHub = $.connection.messagesHub;

        messageHub.client.onChatUpdate = (chatId) => {
            if (chatId === chat_id) {
                console.log(3213123123123213123);
                $.ajax({
                    url: "Messanger/FindChat",
                    type: "POST",
                    data: { ChatID: chat_id },
                    success: (data) => { $('#msg_field').html(data); },
                    dataType: "html"
                });
            }

        };
        //messageHub.client.SelectGroup = (chatId) => {
        //    console.log(321);
        //};
        //messageHub.client.LeaveGroup = (chatId) => {
        //    console.log(31121);
        //};


        $.connection.hub.start().done(() => {

            //if (null !== chat_id) {
            //    messageHub.server.LeaveGroup(chat_id);
            //    messageHub.server.SelectGroup(chat_id);
            //}
            //else if (chat_id === null) {
            //    SelectGroup(chat_id);
            //}

            $('#msg_send_button').click(() => {
                let msg = $('#msg_input').val();
                if (msg !== null && chat_id !== null) {
                    setTimeout(500, messageHub.server.onChatUpdate(chat_id));
                }
            });
        });
    });
});

// TODO Fix only one update on msg send!
$('#msg_send_button').click(() => {
    let msg = $('#msg_input').val();


    if (msg !== null && chat_id !== null) {
        $.ajax({
            url: "Messanger/SendMessage",
            type: "POST",
            data: { message: msg, chatId: chat_id, UserSenderId: user_guid },
            success: () => {
                let msg = document.getElementById('msg_input').value = '';
                $.ajax({
                    url: "Messanger/FindChat",
                    type: "POST",
                    data: { ChatID: chat_id },
                    success: (data) => { $('#msg_field').html(data); },
                    dataType: "html"
                });
                msg = "";
                //setTimeout(() => {
                //    $(function () {
                //        var chat = $.connection.messagesHub;
                //        chat.server.onChatUpdate(chat_id);
                //    });
                //}, 500);
            }
        });

    }


});