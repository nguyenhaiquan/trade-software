
    $('#submit').click(function () {

        // Khai báo biến get dữ liệu từ form
        var name = $("#name").val();
        var password = $("#password").val();
        // Sử dụng phương thức jQuery để Post dữ liệu vào

        $.post("http://localhost:4031/Home/CheckLogin",
        { name: name, password: password },
        function (data, textStatus, jqXHR) {
            if (data.status) {                
                $("#CloseSignIn").click();
                $("#SignIn").css('display','none');
                $("#Successful").html('Xin chào bạn :  ' + name);
                $("#Successful").css('display', 'block');
            }
        }).fail(function (jqXHR, textStatus, errorThrown) {
            alert(textStatus);
        });
    });
    $("#Successful").click(function () {



    });