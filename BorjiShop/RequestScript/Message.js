$(document).ready(function() {
    hideLoader();
});

function reply(id) {
    var answer = $("#txtAnswer").val();

    if (answer === "") {
        LiteBoxShown("خطا", "لطفاً ابتدا پاسخ را وارد نمایید.", "#F1A9A0", true);
        $("#LiteBoxButtom").click(function () {
            closeErrorMessage();
        });
        return;
    }

    var parameters = {};
    var message = {};

    message["ID"] = id;
    message["Answer"] = answer;



    parameters["message"] = message;

    CallMethod("/Admin/Message/ReplyMessage", JSON.stringify(parameters), function (response) {
        if (response === "Success") {
            LiteBoxShown("موفقیت", "پاسخ شما با موفقیت ارسال شد.", "#A2DED0", true);
            $("#LiteBoxButtom").click(function () {
                closeErrorMessage();
            });
        } else if (response === "Error") {
            LiteBoxShown("خطا", "خطایی در ارسال پاسخ روی داده، لطفاً دوباره تلاش نمایید.", "#F1A9A0", true);
            $("#LiteBoxButtom").click(function () {
                closeErrorMessage();
            });
        } else {
            LiteBoxShown("خطا", response, "#F1A9A0", true);
            $("#LiteBoxButtom").click(function () {
                closeErrorMessage();
            });
        }
    }, true);
}