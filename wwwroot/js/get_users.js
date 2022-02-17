$(document).ready(function () {
    var $search_id = $("#search_user_id");
    var $search_location = $("#search_user_location");
    var $search_email = $("#search_user_email");
    var $search_name = $("#search_user_name");
    var $users_table = $("#users_table");
    var $user_active = $("#search_user_active");
    var $user_active_real = "";
    window.loading = false;
    var $smth_changed = false;
    var $loading_div = '<div class="loading"><p>Loading...</p></div>';
    var $nodata_div = '<div class="no-data"><p>No data</p></div>';

    function try_find() {
        if (window.loading == false) {
            find();
        }
        else {
            console.log("Error: Loading=" + window.loading);
        }
    }

    function find() {
            console.log("START OF FUNC (Changed=" + $smth_changed + ", loading=" + window.loading + ")");
            window.loading = true;
            $search_id = $("#search_user_id");
            $search_name = $("#search_user_name");
            $users_table = $("#users_table");
            $user_active = $("#search_user_active");
            $search_email = $("#search_user_email");
            $search_location = $("#search_user_location");
            $user_active_real = "";


            if ($user_active.val() == "All") {
                $user_active_real == ""
            }
            else {
                $user_active_real = $user_active.val();
            }

            console.log("Active: " + $user_active.val());

        $users_table.html($loading_div);
            $.ajax({
                type: "get",
                url: "json/users?id=" + $search_id.val() + "&name=" + $search_name.val() + "&active=" + $user_active_real + "&email=" + $search_email.val() + "&location=" + $search_location.val(),
                contentType: "html",
                success: function (result) {
                    if (result.length == 0) {
                        $users_table.html($nodata_div)
                        console.log("MIDDLE OF FUNCTION: NO DATA!")
                        window.loading = false;
                    }
                    else {
                        var $data = "";
                        var $data_head = '<table class="table_users" cellpadding="4" cellspacing="0">' +
                            '<tr>' +
                            '<td align="center">ID</td>' +
                            '<td align="center">Active?</td>' +
                            '<td align="center">Name</td>' +
                            '<td align="center">E-mail</td>' +
                            '<td align="center">Location</td>' +
                            '<td align="center">Action</td>' +
                            '</tr>';




                        $.each(result, function (index, value) {
                            var isActive = true;
                            var active_class = "";
                            if (value.isActive == true) {
                                active_class = "userIsActiveTrue"
                            }
                            else {
                                active_class = "userIsActiveFalse"
                            }

                            $data += '<tr class="table_item">' +
                                '<td align="center">' + value.id + '</td>' +
                                '<td align="center"><div class="' + active_class + '"> ' + value.isActive + '</div></td>' +
                                '<td align="center">' + value.name + '</td>' +
                                '<td align="center">' + value.email + '</td>' +
                                '<td align="center">' + value.location + '</td>' +
                                '<td align="center"><div class="user_info_link"><a href="/users/' + value.id + '/">View</a> </div></td>' +
                                '</tr>';



                        });
                        var $data_end = '</table>';
                        $users_table.html(" ");
                        $users_table.append($data_head + $data + $data_end);
                        window.loading = false;
                        console.log("End OF FUNC (Changed=" + $smth_changed + ", loading=" + window.loading + ")");
                    }
                }
            })
        }
    $search_name.on("input", try_find);
    $search_location.on("input", try_find);
    $search_email.on("input", try_find);
    $search_id.on("input", try_find);
    $user_active.on("change", try_find);
});