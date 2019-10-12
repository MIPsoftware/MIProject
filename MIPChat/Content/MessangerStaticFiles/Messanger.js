$(document).ready(function () {
    $('#action_menu_btn').click(function () {
        $('.action_menu').toggle();
    });
});

$("#CreateNewGroupButton").click(function () {
    let UserNames = $("input[name='UserName[]']:checked")
        .map(function () {
            return $(this).val();
        }).get();

    let data = {
        GroupName: $("#GroupName").val(),
        UserNames: UserNames
    };

    $.ajax({
        type: "POST",
        url: "/api/group",
        data: JSON.stringify(data),
        success: (data) => {
            $('#CreateNewGroup').modal('hide');
        },
        dataType: 'json',
        contentType: 'application/json'
    });

});

$("#SendMessage").click(function () {
    $.ajax({
        type: "POST",
        url: "/api/message",
        data: JSON.stringify({
            AddedBy: $("#UserName").val(),
            GroupId: $("#currentGroup").val(),
            message: $("#Message").val(),
            socketId: pusher.connection.socket_id
        }),
        success: (data) => {
            $(".chat_body").append(`<div class="row chat_message float-right"><b>`
                + data.data.addedBy + `: </b>` + $("#Message").val() + `</div>`
            );

            $("#Message").val('');
        },
        dataType: 'json',
        contentType: 'application/json'
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
            let position = (data.addedBy == $("#UserName").val()) ? " float-right" : "";

            message += `<div class="row chat_message` + position + `">
                             <b>` + data.addedBy + `: </b>` + data.message +
                `</div>`;
        });

        $(".chat_body").html(message);
    });

});