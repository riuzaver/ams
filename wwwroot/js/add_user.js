$(document).ready(function () {

    var $submit_button = $("#submit");
    var $input_username = $("#add_user_name");
    var $input_email = $("#add_user_email");
    var $input_location = $("#add_user_location");
    var $input_status = $("#add_user_active");
    var $output = $("#results");

    var active = true;

    var getUrl = window.location;
    var baseUrl = getUrl.protocol + "//" + getUrl.host;



    function adduser() {
        $submit_button.toggle();
        $output.html("");
        $input_username = $("#add_user_name");
        $input_email = $("#add_user_email");
        $input_location = $("#add_user_location");
        $input_status = $("#add_user_active");

        if ($input_status.val() == "True") {
            active = true;
        }
        else {
            active = false;
        }

        console.log(active);

        $.ajax({
            type: "post",
            url: baseUrl + "/json/users/add",
            data: JSON.stringify({ "Name": $input_username.val(), "Email": $input_email.val(), "Location": $input_location.val(), "isActive": active }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                var postdata = "";
                postdata += '<div class="add_user_result_block_content">' +
                    '<h3>Results</h3>' +
                    '<p>User has been successfully created!</p>' +
                    '<p><b>ID:</b>' + data.id + ', <b>Name:</b>' + data.name + ', <b>E-Mail:</b>' + data.email + ', <b>Location:</b>' + data.location + ', <b>Active Status:</b>' + data.isActive + '</p>' +
                    '<a href="' + baseUrl + '/users/' + data.id + '"><button class="btn btn-primary btn-sm">Go to user page</button></a>' +
                    '</div>';
                $submit_button.toggle();
                console.log(postdata);
                $output.append(postdata);
            },
            error: function (errMsg) {
                alert(errMsg);
                $submit_button.toggle();
            }
        })

        
    }

    $submit_button.on("click", adduser);

});