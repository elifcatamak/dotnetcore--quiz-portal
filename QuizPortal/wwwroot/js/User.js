function Register(){
    var myData = $("#registerForm").serialize();

    $.ajax({
        url: "/user/register",
        type: "POST",
        contentType: "application/x-www-form-urlencoded; charset=utf-8",
        data: myData,
        success: function(data){
            if (data.success) {
                toastr.success(data.message,"",
                    {
                        timeOut: 250,
                        fadeOut: 250,
                        onHidden: function(){
                            window.location = data.url;
                        }
                    }
                );
            }
            else {
                toastr.error(data.message);
            }
        }
    });
}

function Login() {
    var myData = $("#loginForm").serialize();

    $.ajax({
        url: "/user/login",
        type: "POST",
        contentType: "application/x-www-form-urlencoded; charset=utf-8",
        data: myData,
        success: function (data) {
            if (data.success) {
                toastr.success(data.message, "",
                    {
                        timeOut: 1000,
                        fadeOut: 1000,
                        onHidden: function () {
                            window.location = data.url;
                        }
                    }
                );
            }
            else {
                toastr.error(data.message);
            }
        }
    });
}