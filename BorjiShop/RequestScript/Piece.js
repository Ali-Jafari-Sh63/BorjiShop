var deviceList = new Array();

function getDeviceByBrandId() {
    var pieceId = $("#elementId").val();
    var brandId = $("#brandSelect").val();
    var parameters = {};
    parameters["brandId"] = brandId;
    parameters["pieceId"] = pieceId;

    CallMethod("/Admin/Device/GetDeviceByBrandId",
        JSON.stringify(parameters),
        function (response) {
            deviceList = "";
            if (response["Item1"].length > 0) {
                $.each(response["Item1"],
                    function () {
                        if (this["ID"] === response["Item2"]) {
                            deviceList += "<option value='" + this["ID"] + "' selected>" + this["EnglishName"] + "</option>";
                        } else {
                            deviceList += "<option value='" + this["ID"] + "'>" + this["EnglishName"] + "</option>";
                        }
                    });
            };
            $("#device").empty();
            $("#device").html(
                $("#device").html() +
                "<select class='form-control no-top-padding' id='deviceSelect' name='deviceSelect'>" +
                deviceList +
                "</select>"
            );
        },
        true);
}

function showElement() {
    $('#hiddenElement').removeClass('hidden-element');
    $('#btnInsert').addClass('hidden-element');

}

function hideElement() {
    $("#txtPrice").val("");
    $("#txtEnglishName").val("");
    $("#txtPersianName").val("");
    $('#hiddenElement').addClass('hidden-element');
    $('#btnInsert').removeClass('hidden-element');
}

function getPiece(pageNumber) {
    var parameters = {};
    parameters["page"] = pageNumber;
    var counter = (pageNumber - 1) * 10;

    CallMethod("/Admin/Piece/GetPieces",
        JSON.stringify(parameters),
        function (response) {
            if (response["Item1"].length > 0) {
                $("#tableHead").empty();
                $("#tableHead").html(
                    $("#tableHead").html() +
                    "<tr><th>ردیف</th><th>نوع قطعه</th><th>برند دستگاه</th><th>دستگاه</th><th>قیمت</th><th>قطعه ویژه</th><th>تصویر قطعه</th><th>تاریخ</th><th>عملیات</th></tr>"
                );
                $("#tableBody").empty();
                $.each(response["Item1"],
                    function () {
                        console.log(this["IsSlider"]);
                        var check;
                        if (this["IsSlider"]) {
                            check = "<input type='checkbox' checked disabled/>";
                        } else {
                            check = "<input type='checkbox' disabled/>";
                        }
                        $("#tableBody").html(
                            $("#tableBody").html() +
                            "<tr valid='" +
                            this["ID"] +
                            "'><td>" +
                            ++counter +
                            "</td>" +
                            "<td>" +
                            this["PieceTypeName"] +
                            "</td>" +
                            "<td>" +
                            this["BrandName"] +
                            "</td>" +
                            "<td>" +
                            this["DeviceName"] +
                            "</td>" +
                            "<td>" +
                            this["Price"] +
                            "</td>" +
                            "<td>" +
                            check +
                            "</td>" +
                            "<td>" +
                            "<img src='/PieceImage/" +
                            this["FileName"] +
                            "'" +
                            " class='img-rounded' style='width: 75px; height: 75px;'/>" +
                            "</td>" +
                            "<td>" +
                            this["Date"] +
                            "</td>" +
                            "<td>" +
                            "<a class='common-btn' href='/Admin/Piece/Modify/" +
                            this["ID"] +
                            "?type=1&page=" + response["Item2"] + "'>" +
                            "<img data-toggle='tooltip' data-placement='top' title='ویرایش' src='/Content/Images/Admin/edit.png' alt='ویرایش' />" +
                            "</a >" +
                            "<a class='common-btn' onclick='deletePiece(" +
                            this["ID"] +
                            ")'>" +
                            "<img data-toggle='tooltip' data-placement='top' title='حذف' src='/Content/Images/Admin/delete.png' alt='حذف' />" +
                            "</a >" +
                            "</td>" +
                            "</tr>"
                        );
                    });
                if (response["Item3"] !== 1) {
                    $("#pagination").empty();
                    if (response["Item2"] !== 1) {
                        $("#pagination").html(
                            $("#pagination").html() +
                            "<li>" +
                            "<a class='common-btn' aria-label='Previous' href='/Admin/Piece?page=1'>" +
                            "<span data-toggle='tooltip' data-placement='top' title='صفحه 1' aria-hidden='true' style='color: #5C6250;'>&lt;&lt;</span>" +
                            "</a></li>" +
                            "<li>" +
                            "<a class='common-btn' aria-label='Previous' href='/Admin/Piece?page=" +
                            (response["Item2"] - 1) +
                            "'>" +
                            "<span aria-hidden='true' style='color: #5C6250;'>&lt;</span>" +
                            "</a></li >");
                    }
                    $("#pagination").html(
                        $("#pagination").html() +
                        "<li><span id='currentPage' class='common-btn' style='color: #5C6250; cursor: text;'>" +
                        response["Item2"] +
                        "</span></li>"
                    );
                    if (response["Item2"] !== response["Item3"]) {
                        $("#pagination").html(
                            $("#pagination").html() +
                            "<li>" +
                            "<a class='common-btn' aria-label='Next' href='/Admin/Piece?page=" +
                            (response["Item2"] + 1) +
                            "'>" +
                            "<span aria-hidden='true' style='color: #5C6250;'>&gt;</span>" +
                            "</a></li>" +
                            "<li>" +
                            "<a class='common-btn' aria-label='Next' href='/Admin/Piece?page=" +
                            (response["Item3"]) +
                            "'>" +
                            "<span data-toggle='tooltip' data-placement='top' title='صفحه " +
                            response["Item3"] +
                            "' aria-hidden='true' style='color: #5C6250;'>&gt;&gt;</span>" +
                            "</a></li>"
                        );
                    }
                } else {
                    $("#pagination").empty();
                    $("#pagination").html(
                        $("#pagination").html() +
                        "<li><span id='currentPage' class='common-btn hidden-element' style='color: #5C6250; cursor: text;'>" +
                        response["Item2"] +
                        "</span></li>"
                    );
                }
                $("#lastPage").html(
                    response["Item3"]
                );
            } else {
                getPiece(pageNumber - 1);
            }
        },
        true);
}

function addPiece() {
    var pieceTypeId = $("#pieceTypeSelect").val();
    var brandId = $("#brandSelect").val();
    var deviceId = $("#deviceSelect").val();
    var price = $("#txtPrice").val();
    var keys = $("#txtKeys").val();
    var isslider;
    if ($("#chkIsSlider").is(':checked')) {
        isslider = true;
    } else {
        isslider = false;
    }

    if (price == "") {
        LiteBoxShown("خطا", "لطفاً قیمت قطعه را وارد نمایید.", "#F1A9A0", true);
        $("#LiteBoxButtom").click(function () {
            closeErrorMessage();
        });
        return;
    }

    if (keys == "") {
        LiteBoxShown("خطا", "لطفاً کلمات کلیدی را وارد نمایید.", "#F1A9A0", true);
        $("#LiteBoxButtom").click(function () {
            closeErrorMessage();
        });
        return;
    }

    if ($("#uploadPic").get(0).files.length == 0) {
        LiteBoxShown("خطا", "شما تصویر را انتخاب نکرده اید.", "#F1A9A0", true);
        $("#LiteBoxButtom").click(function () {
            closeErrorMessage();
        });
        return;
    }

    var extension = $("#uploadPic").val().replace(/^.*\./, '');

    var isValidExtention = false;
    $.each(["png", "jpg", "jpeg"],
        function () {
            if (extension.toLowerCase() == this.toLowerCase())
                isValidExtention = true;
        });
    if (isValidExtention == false) {
        LiteBoxShown("خطا", "فرمت تصویر صحیح نیست.", "#F1A9A0", true);
        $("#LiteBoxButtom").click(function () {
            closeErrorMessage();
        });
        return;
    }

    var files = $("#uploadPic").get(0).files;
    if (((files[0].size / 1024) / 1024) > 0.5) {
        LiteBoxShown("خطا", "حجم تصویر باید از 500 کیلوبایت کمتر باشد.", "#F1A9A0", true);
        $("#LiteBoxButtom").click(function () {
            closeErrorMessage();
        });
        return;
    }

    var parameters = {};
    var piece = {};

    piece["BrandId"] = brandId;
    piece["DeviceId"] = deviceId;
    piece["PieceTypeId"] = pieceTypeId;
    piece["Price"] = price;
    piece["IsSlider"] = isslider;

   
    parameters["piece"] = piece;
    parameters["pieceKeys"] = keys;
    parameters["type"] = $("#actionMode").val();
    parameters["id"] = $("#elementId").val();

    CallMethod("/Admin/Piece/ModifyPiece", JSON.stringify(parameters), function (response) {
        if (response["Item1"] === "Success") {
            UploadPicture($("#uploadPic"),
                "/Admin/Piece/UploadPicture",
                ["png", "jpg", "jpeg"],
                true,
                response["Item3"],
                response["Item2"],
                0.5,
                function (xxx,
                    yyy) {
                    LiteBoxShown("موفقیت", "قطعه با موفقیت ثبت شد.", "#A2DED0", true);
                    $("#LiteBoxButtom").click(function () {
                        closeErrorMessage();
                        window.location = "/Admin/Piece?page=" + response["Item4"] + "";
                    });
                });
        } else if (response["Item1"] === "Error") {
            LiteBoxShown("خطا", "خطا در ثبت قطعه.", "#F1A9A0", true);
            $("#LiteBoxButtom").click(function () {
                closeErrorMessage();
            });
        } else {
            LiteBoxShown("خطا", response["Item1"], "#F1A9A0", true);
            $("#LiteBoxButtom").click(function () {
                closeErrorMessage();
            });
        }
    }, true);
}

function updatePiece() {
    var page = $("#page").val();
    var pieceTypeId = $("#pieceTypeSelect").val();
    var brandId = $("#brandSelect").val();
    var deviceId = $("#deviceSelect").val();
    var price = $("#txtPrice").val();
    var keys = $("#txtKeys").val();
    var isslider;
    if ($("#chkIsSlider").is(':checked')) {
        isslider = true;
    } else {
        isslider = false;
    }

    if (price == "") {
        LiteBoxShown("خطا", "لطفاً قیمت قطعه را وارد نمایید.", "#F1A9A0", true);
        $("#LiteBoxButtom").click(function () {
            closeErrorMessage();
        });
        return;
    }

    if (keys == "") {
        LiteBoxShown("خطا", "لطفاً کلمات کلیدی را وارد نمایید.", "#F1A9A0", true);
        $("#LiteBoxButtom").click(function () {
            closeErrorMessage();
        });
        return;
    }

    if ($("#uploadPic").get(0).files.length !== 0) {
        var extension = $("#uploadPic").val().replace(/^.*\./, '');

        var isValidExtention = false;
        $.each(["png", "jpg", "jpeg"],
            function () {
                if (extension.toLowerCase() == this.toLowerCase())
                    isValidExtention = true;
            });
        if (isValidExtention == false) {
            LiteBoxShown("خطا", "فرمت تصویر صحیح نیست.", "#F1A9A0", true);
            $("#LiteBoxButtom").click(function () {
                closeErrorMessage();
            });
            return;
        }

        var files = $("#uploadPic").get(0).files;
        if (((files[0].size / 1024) / 1024) > 0.5) {
            LiteBoxShown("خطا", "حجم تصویر باید از 500 کیلوبایت کمتر باشد.", "#F1A9A0", true);
            $("#LiteBoxButtom").click(function () {
                closeErrorMessage();
            });
            return;
        }


    }



    var parameters = {};
    var piece = {};

    piece["BrandId"] = brandId;
    piece["DeviceId"] = deviceId;
    piece["PieceTypeId"] = pieceTypeId;
    piece["Price"] = price;
    piece["IsSlider"] = isslider;

    parameters["piece"] = piece;
    parameters["pieceKeys"] = keys;
    parameters["type"] = $("#actionMode").val();
    parameters["id"] = $("#elementId").val();

    CallMethod("/Admin/Piece/ModifyPiece", JSON.stringify(parameters), function (response) {
        if (response["Item1"] === "Success") {
            if ($("#uploadPic").get(0).files.length !== 0) {
                UploadPicture($("#uploadPic"),
                    "/Admin/Piece/UploadPicture",
                    ["png", "jpg", "jpeg"],
                    true,
                    response["Item3"],
                    response["Item2"],
                    0.5,
                    function (xxx,
                        yyy) { });
            }
            LiteBoxShown("موفقیت", "قطعه با موفقیت ثبت شد.", "#A2DED0", true);
            $("#LiteBoxButtom").click(function () {
                closeErrorMessage();
                //window.location = "/Admin/Piece?page=" + page + "";
                //window.location = history.back();
                window.location = "/Admin/Piece?page=" + page + "";
            });
        } else if (response["Item1"] === "Error") {
            LiteBoxShown("خطا", "خطا در ویرایش قطعه.", "#F1A9A0", true);
            $("#LiteBoxButtom").click(function () {
                closeErrorMessage();
            });
        } else {
            LiteBoxShown("خطا", response["Item1"], "#F1A9A0", true);
            $("#LiteBoxButtom").click(function () {
                closeErrorMessage();
            });
        }
    }, true);
}

function deletePiece(ID) {
    var page = $("#currentPage").text();

    var parameters = {};

    parameters["ID"] = ID;
    LiteBoxConfirmShown("از حذف قطعه اطمینان دارید ؟",
        "",
        function () {
            CallMethod("/Admin/Piece/DeletePiece",
                JSON.stringify(parameters),
                function (response) {
                    if (response["Item1"] == "Success") {
                        LiteBoxShown("موفقیت", "قطعه با موفقیت حذف شد.", "#A2DED0", true);
                        $("#LiteBoxButtom").click(function () {
                            closeErrorMessage();
                            if (response["Item2"] === 1) {
                                window.location = "/Admin/Piece?page=1";

                            } else {
                                getPiece(page);

                            }
                        });
                    } else if (response == "Error") {
                        LiteBoxShown("خطا", "خطا در حذف قطعه.", "#F1A9A0", true);
                        $("#LiteBoxButtom").click(function () {
                            closeErrorMessage();
                        });
                    } else {
                        LiteBoxShown("خطا", response, "#F1A9A0", true);
                        $("#LiteBoxButtom").click(function () {
                            closeErrorMessage();
                        });
                    }
                },
                true);
        });
}