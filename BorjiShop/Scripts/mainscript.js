$(document).ready(function () {
    $('ul.dropdown-menu [data-toggle=dropdown]').on('click', function (event) {
        event.preventDefault();
        event.stopPropagation();
        $(this).parent().siblings().removeClass('open');
        $(this).parent().toggleClass('open');
    });

    $('#login').on('click', function (event) {
        event.preventDefault();
        event.stopPropagation();
        $(this).parent().siblings().removeClass('open');
        $(this).parent().toggleClass('open');
    });
});

// slider
$("#slider").responsiveSlides({
    auto: true,
    pager: false,
    nav: true,
    speed: 500,
    timeout: 4000,
    pause: true,
    namespace: "callbacks",
    before: function () {
        $('.events').append("<li>before event fired.</li>");
    },
    after: function () {
        $('.events').append("<li>after event fired.</li>");
    }
});
// end of slider

$('.collapse').on('show.bs.collapse', function () {
    $(this).parent().find('.fa-plus').removeClass('fa-plus').addClass('fa-minus');
}).on('hidden.bs.collapse', function () {
    $(this).parent().find('.fa-minus').removeClass('fa-minus').addClass('fa-plus');
});

// messagebox
function SecondLiteBoxShown(title, content, color, mode, btnOK, functionToCall) {
    LiteBoxShown(title, content, color, mode, btnOK, functionToCall, true);
}

function LiteBoxShown(title, content, color, mode, btnOK, functionToCall, isSecond) {
    var prefix = "";
    if (isSecond) {
        prefix = "second";
    }

    var correctDocument = window.document;
    if ($("#" + prefix + "LiteBox").length === 0) {
        //note: when use iframe
        correctDocument = window.parent.document;
    }

    if (mode === true) {
        $(correctDocument.getElementById(prefix + "LiteBoxTitle")).html(title);
        $(correctDocument.getElementById(prefix + "LiteBoxTitle")).css("background", color);
        $(correctDocument.getElementById(prefix + "LiteBoxButtom")).css("background", color);
        $(correctDocument.getElementById(prefix + "LiteBoxContent")).html(content);
        $(correctDocument.getElementById(prefix + "AllLiteBox")).fadeIn(500);
        $(correctDocument.getElementById(prefix + "LiteBox")).animate({ marginTop: "30px" }, 500);
        $(correctDocument.getElementById(prefix + "LiteBoxCloseButtom")).css("background", color);
    } else {
        $(correctDocument.getElementById(prefix + "AllLiteBox")).fadeOut(500);
        $(correctDocument.getElementById(prefix + "LiteBox")).animate({ marginTop: "-1000px" }, 500);
    }

    //نمایش دکمه تایید
    if (btnOK == undefined || btnOK)
        $(correctDocument.getElementById(prefix + "LiteBoxButtom")).show();

    if (title == "ورود به سیستم") {
        $(correctDocument.getElementById(prefix + "LiteBoxButtom")).attr('onclick', "refresh()");
        return;
    }

    if (functionToCall == "" || functionToCall == null || functionToCall == undefined)
        functionToCall = "";

    if (functionToCall != "")
        $(correctDocument.getElementById(prefix + "LiteBoxButtom")).attr('onclick', functionToCall);
}

function showSuccess(message) {
    if (!message) {
        message = "عملیات با موفقیت انجام گردید";
    }
    SecondLiteBoxShown("موفقیت", message, "#A2DED0", true);
}
function showError(message) {
    if (!message) {
        message = "خطایی در انجام عملیات رخ داده است";
    }
    SecondLiteBoxShown("خطا", message, "#F1A9A0", true);
}


function LiteBoxCloseButtomClick(isSecond) {
    var prefix = "";
    if (isSecond) {
        prefix = "second";
    }

    $("#" + prefix + "AllLiteBox").fadeOut(500), $("#" + prefix + "LiteBox").animate({
        marginTop: "-1000px"
    }, 500)
}

function LiteBoxConfirmShown(title, content, yesFunction) {
    var color = "#2e323e";
    $("#LiteBoxTitleConfirm").html(title);
    $("#LiteBoxTitleConfirm").css("background", color);
    $("#LiteBoxButtomConfirmYes").css("background", "#66BB6A");
    $("#LiteBoxButtomConfirmNo").css("background", "#ef5350");
    $("#LiteBoxContentConfirm").html(content);
    $("#AllLiteBoxConfirm").fadeIn(500);
    $("#LiteBoxConfirm").animate({ marginTop: "30px" }, 500);
    $('#LiteBoxButtomConfirmYes').off('click');
    $('#LiteBoxButtomConfirmNo').off('click');
    $("#LiteBoxButtomConfirmYes").click(function () {
        yesFunction();
        $("#AllLiteBoxConfirm").fadeOut(500);
        $("#LiteBoxConfirm").animate({ marginTop: "-1000px" }, 500);
    });
    $("#LiteBoxButtomConfirmNo").click(function () {
        $("#AllLiteBoxConfirm").fadeOut(500);
        $("#LiteBoxConfirm").animate({ marginTop: "-1000px" }, 500);
    });
}

function showSuccessMessage(title, content, onOkFunc) {
    LiteBoxShown(title, content, "#A2DED0", true, true, onOkFunc);
}

function closeErrorMessage() {
    //$("#AllLiteBox").fadeOut(500);
    //$("#LiteBox").animate({ marginTop: "-1000px" }, 500);
    LiteBoxCloseButtomClick();
}
// end of messagebox

//callMethod
function CallMethod(e, o, t, i, ajaxType) {
    if (!ajaxType) {
        ajaxType = "POST";
    }

    //1 == i && setTimeout(showLoader, 3000), $.ajax({
    1 == i, $.ajax({
        type: ajaxType,
        url: e,
        data: o,
        contentType: "application/json; charset=utf-8",
        //dataType: "json",
        timeout: 999999,
        success: function (e) {
            setTimeout(hideLoader, 1000),
                //$("#Wating").fadeOut(100),
                "Error" != e ? 1 != e.hasOwnProperty("LogMessage") ? t(e)
                    : LiteBoxShown("خطای کاربری", e.LogMessage, "#F1A9A0", !0)
                    : LiteBoxShown("خطای سیستمی", "خطایی در سیستم به وجود آمده", "#F1A9A0", !0)
        },
        failure: function (e) {
            LiteBoxShown("خطای سیستمی", "خطا در اتصال به سرویس دهنده", "#F1A9A0", !0)
        },
        error: function (e) {
            LiteBoxShown("خطای سیستمی", "خطا در اتصال به سرویس دهنده", "#F1A9A0", !0)
        }
    })
}

function CallMethodAsyncLess(e, o, t, i) {
    1 == i && $("#Wating").fadeIn(100), $.ajax({
        type: "POST",
        url: e,
        async: !1,
        data: o,
        contentType: "application/json; charset=utf-8",
        //dataType: "json",
        timeout: 999999,
        success: function (e) {
            $("#Wating").fadeOut(100), "Error" != e ? 1 != e.hasOwnProperty("LogMessage") ? t(e) : LiteBoxShown("خطای کاربری", e.LogMessage, "#F1A9A0", !0) : LiteBoxShown("خطای سیستمی", "خطایی در سیستم به وجود آمده", "#F1A9A0", !0)
        },
        failure: function (e) {
            LiteBoxShown("خطای سیستمی", "خطا در اتصال به سرویس دهنده", "#F1A9A0", !0)
        },
        error: function (e) {
            LiteBoxShown("خطای سیستمی", "خطا در اتصال به سرویس دهنده", "#F1A9A0", !0)
        }
    })
}

//end of callMethod

//preloader
function hideLoader() {
    document.getElementById('preloader').style.display = "none";
}

function showLoader() {
    document.getElementById('preloader').style.display = "block";
}
//end of preloader

function UploadPicture(fileUploadElement, uploadUrl, acceptExtentions, isRequred, fileName, id, maxLenght, successFunction) {

    if (fileUploadElement.get(0).files.length == 0 && isRequred == true) {
        LiteBoxShown("خطا", "شما فایل انتخاب نکرده اید.", "#F1A9A0", true);
        $("#LiteBoxButtom").click(function () {
            closeErrorMessage();
        });
        return;
        //LiteBoxShown("خطای کاربری", "شما فایل انتخاب نکرده اید", "#F1A9A0", true);
        //return true;
    }
    if (fileUploadElement.get(0).files.length == 0) {
        successFunction("", "");
        return;
    }
    var extension = fileUploadElement.val().replace(/^.*\./, '');

    var isValidExtention = false;
    $(acceptExtentions).each(function () {
        if (extension.toLowerCase() == this.toLowerCase())
            isValidExtention = true;
    });
    if (isValidExtention == false) {
        LiteBoxShown("خطا", "پسوند فایل صحیح نیست.", "#F1A9A0", true);
        $("#LiteBoxButtom").click(function () {
            closeErrorMessage();
        });
        return;
        //LiteBoxShown("خطای کاربری", "پسوند فایل صحیح نیست", "#F1A9A0", true);
        //return;
    }

    var data = new FormData();
    data.append("FileName", fileName);
    var files = fileUploadElement.get(0).files;

    if (((files[0].size / 1024) / 1024) > maxLenght) {
        LiteBoxShown("خطا", "حجم فایل باید از " + maxLenght + " مگابایت کمتر باشد.", "#F1A9A0", true);
        $("#LiteBoxButtom").click(function () {
            closeErrorMessage();
        });
        return;
        //LiteBoxShown("خطای کاربری", "حجم فایل باید از " + maxLenght + " مگابایت کمتر باشد", "#F1A9A0", true);
        //return;
    }

    if (files.length > 0) {
        data.append("UploadedImage", files[0]);
    }

    data.append("ID", id);

    var ajaxRequest = $.ajax({
        type: "POST",
        url: uploadUrl,
        contentType: false,
        processData: false,
        data: data
    });

    ajaxRequest.done(function (responseData, textStatus) {
        console.log("text status: " + textStatus);
        if (textStatus == 'success') {
            if (responseData == "Error")
                LiteBoxShown("خطای کاربری", "خطایی در سیستم به وجود آمده", "#F1A9A0", true);
            else
                successFunction(responseData, textStatus);
        } else {
            LiteBoxShown("خطای کاربری", responseData, "#F1A9A0", true);
        }
    });
}

function checkEmail(email) {
    var filter = /^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$/;
    if (!filter.test(email)) {
        return false;
    }
}

function logIn() {
    var email = $("#txtEmail").val();
    var password = $("#txtPassword").val();
    var rememberMe;
    if ($("#chkRememberMe").is(':checked')) {
        rememberMe = true;
    } else {
        rememberMe = false;
    }


    if (email === "") {
        LiteBoxShown("خطا", "لطفاً پست الکترونیک خود را وارد نمایید.", "#F1A9A0", true);
        $("#LiteBoxButtom").click(function () {
            closeErrorMessage();
        });
        return;
    }

    if (password === "") {
        LiteBoxShown("خطا", "لطفاً رمز عبور خود را وارد نمایید.", "#F1A9A0", true);
        $("#LiteBoxButtom").click(function () {
            closeErrorMessage();
        });
        return;
    }

    if (checkEmail(email) === false) {
        LiteBoxShown("خطا", "لطفاً پست الکترونیک معتبر وارد نمایید.", "#F1A9A0", true);
        $("#LiteBoxButtom").click(function () {
            closeErrorMessage();
        });
        return;
    }

    var parameters = {};

    parameters["email"] = email;
    parameters["password"] = password;
    parameters["rememberMe"] = rememberMe;

    CallMethod("/Users/LoginUser",
        JSON.stringify(parameters),
        function (response) {
            if (response === "Success") {
                LiteBoxShown("موفقیت","خوش آمدید.", "#A2DED0", true);
                $("#LiteBoxButtom").click(function () {
                    location.reload();
                });
                
            } else {
                LiteBoxShown("خطا", response, "#F1A9A0", true);
                $("#LiteBoxButtom").click(function () {
                    closeErrorMessage();
                });
            }
        },
        true);
}

function logOut() {
    var parameters = {};
    CallMethod("/Users/LogOutUser",
        JSON.stringify(parameters),
        function () {
            LiteBoxShown("موفقیت", "امیدواریم باز هم شاهد حضور شما در فروشگاه باشیم، خدا نگهدار.", "#A2DED0", true);
            $("#LiteBoxButtom").click(function () {
                window.location = "/";
            });
        },
        true);
}

function searchCookie() {
    var parameters = {};
    CallMethod("/Users/SearchCookie",
        JSON.stringify(parameters),
        function (response) {
            if (response === "Success") {
                location.reload();
            }
        },true);
}
