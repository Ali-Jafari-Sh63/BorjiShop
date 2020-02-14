function craeteUser() {
    var error = "";
    var FirstName = $("#txtFirstName").val();
    var LastName = $("#txtLastName").val();
    var Email = $("#txtEmail").val();
    var Phone = $("#txtPhone").val();
    var Password = $("#txtPassword").val();
    var ConfirmPassword = $("#txtConfirmPassword").val();

    if (FirstName === "") {
        error += "لطفا نام خود را وارد نمایید.";
        error = error + "</br>";
    }
    if (LastName === "") {
        error += "لطفا نام خانوادگی خود را وارد نمایید.</ br>";
        error = error + "</br>";
    }
    if (checkEmail(Email) === false) {
        error += "لطفاً پست الکترونیک معتبر وارد نمایید.";
        error = error + "</br>";
    }
    if (Phone === "") {
        error += "لطفا شماره تلفن خود را وارد نمایید.";
        error = error + "</br>";
    }
    if (Password === "") {
        error += "لطفا رمز عبور خود را وارد نمایید.";
        error = error + "</br>";
    }
    if (Password !== ConfirmPassword) {
        error += "لطفا رمز عبور خود را درست تکرار نمایید.";
        error = error + "</br>";
    }

    if (error !== "") {
        LiteBoxShown("خطا", error, "#F1A9A0", true);
        $("#LiteBoxButtom").click(function () {
            closeErrorMessage();
        });
        return;
    }


    var parameters = {};
    var user = {};

    user["FirstName"] = FirstName;
    user["LastName"] = LastName;
    user["Email"] = Email;
    user["Phone"] = Phone;
    user["Password"] = Password;

    parameters["user"] = user;

    CallMethod("/Users/CreateUser",
        JSON.stringify(parameters),
        function (response) {
            if (response === "Success") {
                LiteBoxShown("موفقیت", "شما با موفقیت ثبت نام کردید. ایمیلی حاوی لینک فعال سازی برای شما ارسال شد.", "#A2DED0", true);
                $("#LiteBoxButtom").click(function () {
                    window.location = "/";
                });
            } else {
                LiteBoxShown("خطا", response, "#F1A9A0", true);
                $("#LiteBoxButtom").click(function () {
                    closeErrorMessage();
                });
                return;
            }
        },
        true);
}

function showProfElement()
{
    $(".prof-input").removeClass('hidden-element');
    $(".prof-lbl").addClass('hidden-element');
}

function hideProfElement() {
    $(".prof-lbl").removeClass('hidden-element');
    $(".prof-input").addClass('hidden-element');
}

function cancelCP() {
    $("#txtOldPassword").val("");
    $("#txtPassword").val("");
    $("#txtConfPass").val("");
}

