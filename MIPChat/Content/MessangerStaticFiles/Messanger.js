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



$("#selected_msg_chat").click(() => {

    $.ajax({
        url: "Messanger/FindChat",
        data: { ChatID: '40276596-f0df-479a-8e7e-25ecc714265d' },
        type: "POST",
        success: (data) => { $('#msg_field').html(data); },
        dataType: "html"
    });

});

$("#CreateNewGroupButton").click(() => {

    toAddList = new Array();

    $(".userinput").each((iter, item) => {
        if (item.checked) {
            toAddList.push((item.attributes[2].value));
        }
    });

    var ChatName = $('#GroupName1').val();


    if (toAddList.lenght !== 0 && ChatName !== null) {
        console.log(ChatName);
        toAddList.forEach((value, index) => {
            console.log("Челибос:" + value);
        });

        $.ajax({
            url: "Messanger/CreateMessageOrChat",
            data: { name: ChatName, usersGuids: toAddList },
            type: "POST",
            traditional: true,
            dataType: "text"
        });
    } else {
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