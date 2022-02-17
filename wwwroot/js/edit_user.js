$(document).ready(function () {
    var $user_edit_button = $("#user_edit_button");
    var $user_info_edit_status = $("#user_info_edit_status");
    var $user_active = $("#user_active");
    var $user_edit_cancel_button = $("#user_edit_cancel_button");
    var $user_input_id = $("#user_id");
    var $user_input_name = $("#user_name");
    var $user_input_email = $("#user_email");
    var $user_input_location = $("#user_location");
    var $buttons_html = '<div class="user_edit_submit_link"><a href="#" id="user_edit_submit_button">Submit</a></div>' +
        '<div class="user_delete_link"><a href="#" id="user_edit_cancel_button">Cancel</a></div>';
    var $user_active_change_button = $("#user_active_change_button");
    var getUrl = window.location;
    var baseUrl = getUrl.protocol + "//" + getUrl.host;

    //old info values
    window.old_name = $user_input_name.val();
    window.old_email = $user_input_email.val();
    window.old_location = $user_input_location.val();


    var $user_info_block_buttons = $("#user_info_block_buttons");

    function change_active() {
        var active = true;
        if ($user_active.html() == "Active") active = false;
        if ($user_active.html() == "Inactive") active = true;

        update_user($user_input_id.val(), $user_input_email.val(), $user_input_location.val(), $user_input_name.val(), active, false, true);
    }

    function result_display(type, title, text) {
        $user_info_edit_status.html('<div class="' + type + '">' +
            '<p class="message_title">' + title + '</p>' +
            '<p class="message_text">' + text + '</p>' +
            '</div > ');
    }

    function update_user(id, email, location, name, active,log = true, reloadonsuccess = false) {
        $.ajax({
            type: "put",
            url: baseUrl + "/json/users/edit?id=" + id,
            data: JSON.stringify({ "Name": name, "Email": email, "Location": location, "isActive": active }),
            contentType: "application/json; charset=utf-8",
            
            success: function (result) {
                console.log("Success: " + result);
                if (log == true) result_display("message_type_success", "Success", "User details has been successfully modified.");
                window.old_name = $user_input_name.val();
                window.old_email = $user_input_email.val();
                window.old_location = $user_input_location.val();
                if (reloadonsuccess == true) {
                    window.location.reload();
                }
            },
            error: function (result) {
                console.log("Error: " + result);
                if (log == true) result_display("message_type_success", "Success", "User details has been successfully modified.");
            }
        })
    }

    function result_hide() {
        $user_info_edit_status.html('');
    }

    function inputs_toggle(toggle) {
        if (toggle == "hide") {
            $user_input_name.prop('readonly', true);
            $user_input_email.prop('readonly', true);
            $user_input_location.prop('readonly', true);
        }
        if (toggle == "show") {
            $user_input_name.prop('readonly', false);
            $user_input_email.prop('readonly', false);
            $user_input_location.prop('readonly', false);
        }
    }

    function edit_cancel(backold = true) {
        //Put user info back
        if (backold) {
            console.log("OLD NAME: " + window.old_name);
            $user_input_name.val(window.old_name);
            $user_input_email.val(window.old_email);
            $user_input_location.val(window.old_location);
        }
        else {
            
        }

        //Disable inputs
        inputs_toggle("hide");

        //Hide submit button
        $user_info_block_buttons.html('');

        //hide error
        result_hide();
    }

    function edit_submit() {
        $user_input_name = $("#user_name");
        $user_input_email = $("#user_email");
        $user_input_location = $("#user_location");
        console.log("submit");
        //check if inputs are empty
        var empty_fields = false
        if ($user_input_name.val() == "") empty_fields = true
        if ($user_input_email.val() == "") empty_fields = true
        if ($user_input_location.val() == "") empty_fields = true

        //check if nothing is changed
        var nothing_changed = false;
        if ($user_input_name.val() == window.old_name && $user_input_email.val() == window.old_email && $user_input_location.val() == window.old_location) {
            nothing_changed = true;
        }
        

        if (empty_fields == false) {
            if (nothing_changed == false) {
                console.log("Inputs are not empty! Updating user...");
                edit_cancel(false);
                var active = true
                if ($user_active.html() == "Active") active = true;
                if ($user_active.html() == "Inactive") active = false;
                console.log("Updates: " + "ID = " + $user_input_id.val() + ", Active = " + active + ", Email = " + $user_input_email.val() + ", Name = " + $user_input_name.val() + ", Location = " + $user_input_location.val());
                //initializing update
                update_user($user_input_id.val(), $user_input_email.val(), $user_input_location.val(), $user_input_name.val(), active);
            }
            else {
                console.log("NOTHING IS CHANGED");
                edit_cancel();
            }
        }
        else {
            console.log("Some input is empty");
            result_display("message_type_error", "Error", "One of the required input fields is empty.")
        }
    }

    function edit() {
        $user_edit_button.disabled = true;
        
        
        //enable inputs
        inputs_toggle("show");

        //Append SUBMIT button
        $user_info_block_buttons.html($buttons_html);
        $user_edit_cancel_button = $("#user_edit_cancel_button");
        $("#user_edit_cancel_button").on("click", function () {
            edit_cancel();
        });

        $user_edit_submit_button = $("#user_edit_submit_button");
        $("#user_edit_submit_button").on("click", function () {
            edit_submit();
        });

        
    }

    $user_active_change_button.on("click", function () {
        change_active();
    });

    $user_edit_button.on("click", function () {
        edit();
    });

    $("#user_edit_cancel_button").on("click", function () {
        edit_cancel();
    });
});