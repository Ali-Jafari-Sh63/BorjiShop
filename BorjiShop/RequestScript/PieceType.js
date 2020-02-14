$(document).ready(function () {
    getPieceType(1, 0);
});

function showElement() {
    $('#hiddenElement').removeClass('hidden-element');
    $('#btnInsert').addClass('hidden-element');

}

function hideElement() {
    $("#txtEnglishName").val("");
    $("#txtPersianName").val("");
    $('#hiddenElement').addClass('hidden-element');
    $('#btnInsert').removeClass('hidden-element');
}

function getPieceType(pageNumber, editId) {
    var parameters = {};
    parameters["page"] = pageNumber;
    var counter = (pageNumber - 1) * 10;

    CallMethod("/Admin/PieceType/GetPieceTypes",
        JSON.stringify(parameters),
        function (response) {
            if (response["Item1"].length > 0) {
                $("#tableHead").empty();
                $("#tableHead").html(
                    $("#tableHead").html() +
                    "<tr><th>ردیف</th><th>نام لاتین</th><th>نام فارسی</th><th>عملیات</th></tr>"
                );
                $("#tableBody").empty();
                $.each(response["Item1"],
                    function () {
                        if (editId == this["ID"]) {
                            $("#tableBody").html(
                                $("#tableBody").html() +
                                "<tr valid='" +
                                this["ID"] +
                                "'><td>" +
                                ++counter +
                                "</td>" +
                                "<td><input id='txtEditEnglishName' type='text' class='form-control' value='" +
                                this["EnglishName"] +
                                "' /></td>" +
                                "<td><input id='txtEditPersianName' type='text' class='form-control' value='" +
                                this["PersianName"] +
                                "' /></td>" +
                                "<td>" +
                                "<a class='common-btn' onclick='updatePieceType(" +
                                this["ID"] +
                                ")'>" +
                                "<img data-toggle='tooltip' data-placement='top' title='ثبت' src='/Content/Images/Admin/tick.png' alt='ثبت' />" +
                                "</a >" +
                                "<a class='common-btn' onclick='location.reload()'>" +
                                "<img data-toggle='tooltip' data-placement='top' title='لغو' src='/Content/Images/Admin/cancel.png' alt='لغو' />" +
                                "</a >" +
                                "</td>" +
                                "</tr>");
                        } else {
                            $("#tableBody").html(
                                $("#tableBody").html() +
                                "<tr valid='" +
                                this["ID"] +
                                "'><td>" +
                                ++counter +
                                "</td>" +
                                "<td>" +
                                this["EnglishName"] +
                                "</td>" +
                                "<td>" +
                                this["PersianName"] +
                                "</td>" +
                                "<td>" +
                                "<a class='common-btn' onclick='getPieceType(" +
                                pageNumber +
                                ", " +
                                this["ID"] +
                                ")'>" +
                                "<img data-toggle='tooltip' data-placement='top' title='ویرایش' src='/Content/Images/Admin/edit.png' alt='ویرایش' />" +
                                "</a >" +
                                "<a class='common-btn' onclick='deletePieceType(" +
                                this["ID"] +
                                ")'>" +
                                "<img data-toggle='tooltip' data-placement='top' title='حذف' src='/Content/Images/Admin/delete.png' alt='حذف' />" +
                                "</a >" +
                                "</td>" +
                                "</tr>");
                        }
                    });
                $("#tableBody").html(
                    $("#tableBody").html() +
                    "<tr id='hiddenElement' class='hidden-element'>" +
                    "<td></td>" +
                    "<td><input id='txtEnglishName' type='text' class='form-control' /></td>" +
                    "<td><input id='txtPersianName' type='text' class='form-control' /></td>" +
                    "<td>" +
                    "<a class='common-btn' onclick='addPieceType()'>" +
                    "<img data-toggle='tooltip' data-placement='top' title='ثبت' src='/Content/Images/Admin/tick.png' alt='ثبت' />" +
                    "</a >" +
                    "<a class='common-btn' onclick='hideElement()'>" +
                    "<img data-toggle='tooltip' data-placement='top' title='لغو' src='/Content/Images/Admin/cancel.png' alt='لغو' />" +
                    "</a >" +
                    "</td>" +
                    "</tr>"
                );
                if (response["Item3"] != 1) {
                    $("#pagination").empty();
                    if (response["Item2"] != 1) {
                        $("#pagination").html(
                            $("#pagination").html() +
                            "<li>" +
                            "<a class='common-btn' aria-label='Previous' onclick='getPieceType(1,0)'>" +
                            "<span data-toggle='tooltip' data-placement='top' title='صفحه 1' aria-hidden='true' style='color: #5C6250;'>&lt;&lt;</span>" +
                            "</a></li>" +
                            "<li>" +
                            "<a class='common-btn' aria-label='Previous' onclick='getPieceType(" +
                            (response["Item2"] - 1) +
                            ",0)'>" +
                            "<span aria-hidden='true' style='color: #5C6250;'>&lt;</span>" +
                            "</a></li >");
                    }
                    $("#pagination").html(
                        $("#pagination").html() +
                        "<li><span id='currentPage' class='common-btn' style='color: #5C6250; cursor: text;'>" +
                        response["Item2"] +
                        "</span></li>"
                    );
                    if (response["Item2"] != response["Item3"]) {
                        $("#pagination").html(
                            $("#pagination").html() +
                            "<li>" +
                            "<a class='common-btn' aria-label='Next' onclick='getPieceType(" +
                            (response["Item2"] + 1) +
                            ",0)'>" +
                            "<span aria-hidden='true' style='color: #5C6250;'>&gt;</span>" +
                            "</a></li>" +
                            "<li>" +
                            "<a class='common-btn' aria-label='Next' onclick='getPieceType(" +
                            (response["Item3"]) +
                            ",0)'>" +
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
                $("#emptyTableHead").empty();
                $("#emptyTableHead").html(
                    $("#emptyTableHead").html() +
                    "<tr><th>ردیف</th><th>نام لاتین</th><th>نام فارسی</th><th>عملیات</th></tr>"
                );
                $("#emptyTableBody").empty();
                $("#emptyTableBody").html(
                    $("#emptyTableBody").html() +
                    "<tr>" +
                    "<td></td>" +
                    "<td><input id='txtEnglishName' type='text' class='form-control' /></td>" +
                    "<td><input id='txtPersianName' type='text' class='form-control' /></td>" +
                    "<td>" +
                    "<a class='common-btn' onclick='addPieceType()'>" +
                    "<img data-toggle='tooltip' data-placement='top' title='ثبت' src='/Content/Images/Admin/tick.png' alt='ثبت' />" +
                    "</a >" +
                    "<a class='common-btn' onclick='hideElement()'>" +
                    "<img data-toggle='tooltip' data-placement='top' title='لغو' src='/Content/Images/Admin/cancel.png' alt='لغو' />" +
                    "</a >" +
                    "</td>" +
                    "</tr>"
                );
            }
            $("#divBtn").html(
                "<button id='btnInsert' type='button' class='btn btn-success btn-insert' onclick='showElement()'>افزودن نوع قطعه جدید</button>"
            );
        },
        true);
};

function addPieceType() {
    var englishname = $("#txtEnglishName").val();
    var persianname = $("#txtPersianName").val();

    if (englishname == "") {
        LiteBoxShown("خطا", "لطفاً نام لاتین نوع قطعه را وارد نمایید.", "#F1A9A0", true);
        $("#LiteBoxButtom").click(function () {
            closeErrorMessage();
        });
        return;
    }

    if (persianname == "") {
        LiteBoxShown("خطا", "لطفاً نام فارسی نوع قطعه را وارد نمایید.", "#F1A9A0", true);
        $("#LiteBoxButtom").click(function () {
            closeErrorMessage();
        });
        return;
    }

    var parameters = {};
    var pieceType = {};

    pieceType["EnglishName"] = englishname;
    pieceType["PersianName"] = persianname;



    parameters["pieceType"] = pieceType;

    CallMethod("/Admin/PieceType/AddPieceType", JSON.stringify(parameters), function (response) {
        if (response["Item1"] == "Success") {
            LiteBoxShown("موفقیت", "نوع قطعه با موفقیت ثبت شد.", "#A2DED0", true);
            $("#LiteBoxButtom").click(function () {
                closeErrorMessage();
                hideElement();
                getPieceType(response["Item2"], 0);
            });
        } else if (response["Item1"] == "Error") {
            LiteBoxShown("خطا", "خطا در ثبت نوع قطعه.", "#F1A9A0", true);
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

function updatePieceType(ID) {

    var englishname = $("#txtEditEnglishName").val();
    var persianname = $("#txtEditPersianName").val();

    var page = $("#currentPage").text();

    if (englishname == "") {
        LiteBoxShown("خطا", "لطفاً نام لاتین نوع قطعه را وارد نمایید.", "#F1A9A0", true);
        $("#LiteBoxButtom").click(function () {
            closeErrorMessage();
        });
        return;
    }

    if (persianname == "") {
        LiteBoxShown("خطا", "لطفاً نام فارسی نوع قطعه را وارد نمایید.", "#F1A9A0", true);
        $("#LiteBoxButtom").click(function () {
            closeErrorMessage();
        });
        return;
    }

    var parameters = {};
    var brand = {};

    parameters["ID"] = ID;
    parameters["EnglishName"] = englishname;
    parameters["PersianName"] = persianname;
    parameters["page"] = page;


    CallMethod("/Admin/PieceType/UpdatePieceType", JSON.stringify(parameters), function (response) {
        if (response["Item1"] == "Success") {
            LiteBoxShown("موفقیت", "نوع قطعه با موفقیت ویرایش شد.", "#A2DED0", true);
            $("#LiteBoxButtom").click(function () {
                closeErrorMessage();
                getPieceType(response["Item2"], 0);
            });
        } else if (response["Item1"] == "Error") {
            LiteBoxShown("خطا", "خطا در ویرایش نوع قطعه.", "#F1A9A0", true);
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

function deletePieceType(ID) {
    var page = $("#currentPage").text();

    var parameters = {};

    parameters["ID"] = ID;
    LiteBoxConfirmShown("از حذف نوع قطعه اطمینان دارید ؟",
        "",
        function () {
            CallMethod("/Admin/PieceType/DeletePieceType",
                JSON.stringify(parameters),
                function (response) {
                    if (response["Item1"] == "Success") {
                        LiteBoxShown("موفقیت", "نوع قطعه با موفقیت حذف شد.", "#A2DED0", true);
                        $("#LiteBoxButtom").click(function () {
                            closeErrorMessage();
                            if (response["Item2"] == 1) {
                                getPieceType(1, 0);
                            } else {
                                getPieceType(page, 0);
                            }
                        });
                    } else if (response == "Error") {
                        LiteBoxShown("خطا", "خطا در حذف نوع قطعه.", "#F1A9A0", true);
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