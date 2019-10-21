$(document).ready(function () {
    $('#action_menu_btn').click(function () {
        $('.action_menu').toggle();
    });
});



$("#on_create_chat").click(() => {
    $.ajax({
        url: "Messanger/GetAllUsersToChat",
        type: "POST",
        success: (data) => { $('#users_list_row').html(data);},
        dataType: "html"
    });

});



$("#groups").on("click", ".group", function () {
    let group_id = $(this).attr("data-group_id");

    $('.group').css({ "background-color": "rgba(0,0,0,0)", cursor: "pointer" });
    $(this).css({ "background-color": "rgba(0,0,0,0.3)", cursor: "default" });

    $("#currentGroup").val(group_id);
    currentGroupId = group_id;

    $.get("/api/message/" + group_id, function (data) {
        let message = "";

        data.forEach(function (data) {
            let position = (data.addedBy === $("#UserName").val()) ? " float-right" : "";

            message += `<div class="row chat_message` + position + `">
                             <b>` + data.addedBy + `: </b>` + data.message +
                `</div>`;
        });

        $(".chat_body").html(message);
    });

});