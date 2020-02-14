$(document).ready(function () {
    getbrand(1, 0);
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

function getbrand(pageNumber, editid) {
    var parameters = {};
    parameters["page"] = pageNumber;
    parameters["editID"] = editid;
    var counter = (pageNumber - 1) * 10;

    CallMethod("/Admin/Brand/GetBrands",
        JSON.stringify(parameters),
        function (response) {
            $("#emptyTableHead").empty();
            $("#emptyTableBody").empty();
            if (response["Item1"].length > 0) {
                $("#tablehead").empty();
                $("#tablehead").html(
                    $("#tablehead").html() +
                    "<tr><th>ردیف</th><th>نام لاتین</th><th>نام فارسی</th><th>عملیات</th></tr>"
                );
                $("#reqResults").empty();
                $.each(response["Item1"],
                    function () {
                        if (response["Item4"] == this["ID"]) {
                            $("#reqResults").html(
                                $("#reqResults").html() +
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
                                "<a class='common-btn' onclick='updateBrand(" +
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
                            $("#reqResults").html(
                                $("#reqResults").html() +
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
                                "<a class='common-btn' onclick='getbrand(" +
                                response["Item2"] +
                                "," +
                                this["ID"] +
                                ")'>" +
                                "<img data-toggle='tooltip' data-placement='top' title='ویرایش' src='/Content/Images/Admin/edit.png' alt='ویرایش' />" +
                                "</a >" +
                                "<a class='common-btn' onclick='deletebrand(" +
                                this["ID"] +
                                ")'>" +
                                "<img data-toggle='tooltip' data-placement='top' title='حذف' src='/Content/Images/Admin/delete.png' alt='حذف' />" +
                                "</a >" +
                                "</td>" +
                                "</tr>");
                        }
                    });
                $("#reqResults").html(
                    $("#reqResults").html() +
                    "<tr id='hiddenElement' class='hidden-element'>" +
                    "<td></td>" +
                    "<td><input id='txtEnglishName' type='text' class='form-control' /></td>" +
                    "<td><input id='txtPersianName' type='text' class='form-control' /></td>" +
                    "<td>" +
                    "<a class='common-btn' onclick='addBrand()'>" +
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
                            "<a class='common-btn' aria-label='Previous' onclick='getbrand(1,0)'>" +
                            "<span data-toggle='tooltip' data-placement='top' title='صفحه 1' aria-hidden='true' style='color: #5C6250;'>&lt;&lt;</span>" +
                            "</a></li>" +
                            "<li>" +
                            "<a class='common-btn' aria-label='Previous' onclick='getbrand(" +
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
                            "<a class='common-btn' aria-label='Next' onclick='getbrand(" +
                            (response["Item2"] + 1) +
                            ",0)'>" +
                            "<span aria-hidden='true' style='color: #5C6250;'>&gt;</span>" +
                            "</a></li>" +
                            "<li>" +
                            "<a class='common-btn' aria-label='Next' onclick='getbrand(" +
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
            }
            else {
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
                    "<a class='common-btn' onclick='addBrand()'>" +
                    "<img data-toggle='tooltip' data-placement='top' title='ثبت' src='/Content/Images/Admin/tick.png' alt='ثبت' />" +
                    "</a >" +
                    "<a class='common-btn' onclick='hideElement()'>" +
                    "<img data-toggle='tooltip' data-placement='top' title='لغو' src='/Content/Images/Admin/cancel.png' alt='لغو' />" +
                    "</a >" +
                    "</td>" +
                    "</tr>"
                );
                getbrand(pageNumber - 1, 0);
            }
            $("#div-btn").html(
                "<button id='btnInsert' type='button' class='btn btn-success btn-insert' onclick='showElement()'>افزودن برند جدید</button>"
            );
        },
        true);
};

function addBrand() {
    var englishname = $("#txtEnglishName").val();
    var persianname = $("#txtPersianName").val();
    var lastPage = $("#lastPage").text();

    if (englishname == "") {
        LiteBoxShown("خطا", "لطفاً نام انگلیسی برند را وارد نمایید.", "#F1A9A0", true);
        $("#LiteBoxButtom").click(function () {
            closeErrorMessage();
        });
        return;
    }

    if (persianname == "") {
        LiteBoxShown("خطا", "لطفاً نام فارسی برند را وارد نمایید.", "#F1A9A0", true);
        $("#LiteBoxButtom").click(function () {
            closeErrorMessage();
        });
        return;
    }

    var parameters = {};
    var brand = {};

    brand["EnglishName"] = englishname;
    brand["PersianName"] = persianname;



    parameters["brand"] = brand;

    CallMethod("/Admin/Brand/AddBrand", JSON.stringify(parameters), function (response) {
        if (response["Item1"] == "Success") {
            LiteBoxShown("موفقیت", "برند با موفقیت ثبت شد.", "#A2DED0", true);
            $("#LiteBoxButtom").click(function () {
                closeErrorMessage();
                getbrand(response["Item2"], 0);
            });
        } else if (response["Item1"] == "Error") {
            LiteBoxShown("خطا", "خطا در ثبت برند.", "#F1A9A0", true);
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

function updateBrand(ID) {
    var englishname = $("#txtEditEnglishName").val();
    var persianname = $("#txtEditPersianName").val();
    var page = $("#currentPage").text();

    if (englishname == "") {
        LiteBoxShown("خطا", "لطفاً نام انگلیسی برند را وارد نمایید.", "#F1A9A0", true);
        $("#LiteBoxButtom").click(function () {
            closeErrorMessage();
        });
        return;
    }

    if (persianname == "") {
        LiteBoxShown("خطا", "لطفاً نام فارسی برند را وارد نمایید.", "#F1A9A0", true);
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


    CallMethod("/Admin/Brand/updateBrand", JSON.stringify(parameters), function (response) {
        if (response["Item1"] == "Success") {
            LiteBoxShown("موفقیت", "برند با موفقیت ویرایش شد.", "#A2DED0", true);
            $("#LiteBoxButtom").click(function () {
                closeErrorMessage();
                //getbrand(response["Item2"], 0);
                getbrand(page, 0);
            });
        } else if (response["Item1"] == "Error") {
            LiteBoxShown("خطا", "خطا در ویرایش برند.", "#F1A9A0", true);
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

function deletebrand(ID) {
    var page = $("#currentPage").text();

    var parameters = {};

    parameters["ID"] = ID;
    LiteBoxConfirmShown("از حذف برند اطمینان دارید ؟",
        "",
        function () {

            CallMethod("/Admin/Brand/DeleteBrand",
                JSON.stringify(parameters),
                function (response) {
                    if (response["Item1"] == "Success") {
                        LiteBoxShown("موفقیت", "برند با موفقیت حذف شد.", "#A2DED0", true);
                        $("#LiteBoxButtom").click(function () {
                            closeErrorMessage();
                            if (response["Item2"] == 1) {
                                getbrand(1, 0);
                            } else {
                                getbrand(page, 0);
                            }
                        });
                    } else if (response == "Error") {
                        LiteBoxShown("خطا", "خطا در حذف برند.", "#F1A9A0", true);
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
