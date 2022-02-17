
$(document).ready(function () {
    var $user_delete_button = $("#user_delete_button");
    var $user_id = $("#user_id");
    var getUrl = window.location;
    var baseUrl = getUrl.protocol + "//" + getUrl.host;

    $('#dialog').dialog({
        resizable: false,
        height: "auto",
        autoOpen: false,
        width: 400,
        modal: true,
        buttons: [
            {
                text: "Cancel",
                "class": 'user_delete_discard_button',
                click: function () {
                    $('#dialog').dialog("close");
                }
            },
            {
                text: "Delete",
                "class": 'user_delete_confirm_button',
                click: function () {
                    delete_user();
                }
            }
        ],
        close: function () {
            // Close code here (incidentally, same as Cancel code)
        }
    });

    function delete_user() {
        console.log("user id to delete: " + $user_id.html());

        $.ajax({
            type: "get",
            url: baseUrl + "/json/users/del?id=" + $user_id.val(),
            contentType: "html",
            success: function (result) {
                location.href = baseUrl + "/users";

            }
        })
    }


    $user_delete_button.on("click", function () {
        $('#dialog').dialog('open');
    });

});