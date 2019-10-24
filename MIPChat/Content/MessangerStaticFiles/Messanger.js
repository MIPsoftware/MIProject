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
    }, () => { console.log('ready'); });
});


$('.selected_msg_chat').each((index, element) => {
    element.ready(() => console.log(3213));
});


//$("#chat_field").click(() => {
//    $('.selected_msg_chat').each((index, element) => {
//        element.click(() => console.log(312));
//    });


//});


//element.click(() => {
//    console.log(123213);
//    $.ajax({
//        url: "Messanger/FindChat",
//        data: { ChatID: '40276596-f0df-479a-8e7e-25ecc714265d' },
//        type: "POST",
//        success: (data) => { $('#msg_field').html(data); },
//        dataType: "html"
//    });
//});


$("#CreateNewGroupButton").click(() => {

    toAddList = new Array();

    $(".userinput").each((iter, item) => {
        if (item.checked) {
            toAddList.push((item.attributes[2].value));
        }
    });

    let ChatName = $('#GroupName1').val();


    if (toAddList.lenght > 2 && ChatName !== null) {


        $.ajax({
            url: "Messanger/CreateMessageOrChat",
            data: { name: ChatName, usersGuids: toAddList },
            type: "POST",
            traditional: true,
            dataType: "text"
        });
    }
    else {
        // НЕ УДАЛОСЬ СОЗДАТЬ ЧАТИК
        return;
    }

});



$("#groups").on("click", ".group", function () {
    let group_id = $(this).attr("data-group_id");

    $('.group').css({ "background-color": "rgba(0,0,0,0)", cursor: "pointer" });
    $(this).css({ "background-color": "rgba(0,0,0,0.3)", cursor: "default" });

    $("#currentGroup").val(group_id);
    currentGroupId = group_id;

    $.get("/api/message/" + group_id, function (data) {
        let message = "";


        $(".chat_body").html(message);
    });

});