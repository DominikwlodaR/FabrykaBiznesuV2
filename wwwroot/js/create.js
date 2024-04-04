$(document).ready(function () {
    $('#addUser').submit(function (e) {
        e.preventDefault();
       
        var formdata = {
            Name: $('#userName').val(),
            LastName: $('#LastName').val(),
            UserName: $('#UserName').val(),
            Email: $('#Email').val(),
            EmailConfirmed: true,
            TeamID: $('#TeamID').val(),
            PasswordHash: $('#pass').val()
        }
        console.log(formdata)
        $.ajax({
            type: 'POST',
            url: '/Home/CreateUser',
            data: formdata,
            success: function (data) {
                console.log(data);
               // location.reload();

            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.error("Request failed: " + textStatus, errorThrown);
                console.log('Status: ' + jqXHR.status);
                console.log('Status Text: ' + jqXHR.statusText);
                console.log('Response Text: ' + jqXHR.responseText);
            }
        })
    });


    $('#addTeam').submit(function (e) {
        e.preventDefault();

        var formdata = {
            TeamName: $('#teamName').val(),
            TeamLeaderId: $('#teamLeaderId').val(),
        }
        console.log(formdata)
        $.ajax({
            type: 'POST',
            url: '/Home/CreateTeam',
            data: formdata,
            success: function (data) {
                console.log(data);
                // location.reload();

            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.error("Request failed: " + textStatus, errorThrown);
                console.log('Status: ' + jqXHR.status);
                console.log('Status Text: ' + jqXHR.statusText);
                console.log('Response Text: ' + jqXHR.responseText);
            }
        })
    });
});