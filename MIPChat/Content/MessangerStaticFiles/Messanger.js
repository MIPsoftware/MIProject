var chat_id = null;


$(document).ready(function () {

    $('#action_menu_btn').click(function () {
        $('.action_menu').toggle();
    });
});



$("#on_create_chat").click(() => {
    $.ajax({
        url: "Messanger/GetAllUsersToChat",
        type: "POST",
        success: (data) => { $('#users_list_row').html(data); },
        dataType: "html"
    });
});

$('#chat_field').ready(() => {
    let user_guid = document.cookie.split('UserGuid=')[1];

    $.ajax({
        url: "Messanger/GetAllChatsForUser",
        type: "POST",
        data: { userId: user_guid },
        success: (data) => { $('#chat_field').html(data); },
        dataType: "html"
    });
});





$("#CreateNewGroupButton").click(() => {
    var user_guid = document.cookie.split('UserGuid=')[1];

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

});


$('#msg_send_button').click(() => {
    let msg = $('#msg_input').val();
    let user_guid = document.cookie.split('UserGuid=')[1];
    console.log(msg);
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
            }
        });

    }

});