$(document).ready(function () {

    var $search_id = $("#user_id");
    var $table_accesses = $("#users_table");
    var getUrl = window.location;
    var baseUrl = getUrl.protocol + "//" + getUrl.host;

    console.log("ID: " + $search_id.val());

    $table_accesses.html("");
    $.ajax({
        type: "get",
        url: baseUrl + "/json/accesses?id=" + $search_id.val(),
        contentType: "html",
        success: function (result) {
            if (result.length == 0) {
                $table_accesses.append('<p>No data</p>')
            }
            else {
                var $data = '';
                var $data_head = '<table class="table_users" cellpadding="4" cellspacing="0">' +
                    '<tr>' +
                    '<td align="center">ID</td>' +
                    '<td align="center">Service</td>' +
                    '<td align="center">User Login</td>' +
                    '</tr>';

                $.each(result, function (index, value) {
                    
                    $data += '<tr class="table_item">' +
                        '<td align="center">' + value.id + '</td>' +
                        '<td align="center">' + value.serviceName + '</td>' +
                        '<td align="center">' + value.userServiceLogin + '</td>' +
                        '</tr>';

                });

                var $data_end = '</table>';
                console.log($data_head + $data + $data_end);
                
                $table_accesses.append('' + $data_head + $data + $data_end);
                console.log($table_accesses.html());


            }
        }
    })

});