$(document).ready(function () {
    getDevice(1, 0);
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

function getDevice(pageNumber, editId) {
    var parameters = {};
    parameters["page"] = pageNumber;
    var counter = (pageNumber - 1) * 10;

    CallMethod("/Admin/Device/GetDevices",
        JSON.stringify(parameters),
        function (response) {
            if (response["Item1"].length > 0) {
                $("#tableHead").empty();
                $("#tableHead").html(
                    $("#tableHead").html() +
                    "<tr><th>ردیف</th><th>برند</th><th>نام لاتین</th><th>نام فارسی</th><th>عملیات</th></tr>"
                );
                $("#tableBody").empty();
                $.each(response["Item1"],
                    function () {
                        var edit = this.BrandId;
                        if (editId === this["ID"]) {
                            var arrOption = new Array();
                            $.each(response["Item4"],
                                function () {
                                    if (edit === this["ID"]) {
                                        arrOption += "<option value='" +
                                            this["ID"] +
                                            "'selected>" +
                                            this["EnglishName"] +
                                            "</option>";
                                    } else {
                                        arrOption += "<option value='" +
                                            this["ID"] +
                                            "'>" +
                                            this["EnglishName"] +
                                            "</option>";
                                    }
                                });
                            $("#tableBody").html(
                                $("#tableBody").html() +
                                "<tr valid='" +
                                this["ID"] +
                                "'><td>" +
                                ++counter +
                                "</td>" +
                                "<td>" +
                                "<select id='brandSelect' name='brandSelect' class='form-control' style='padding-top: 0; width: 140px;'>" +
                                arrOption +
                                "<select>" +
                                "</td>" +
                                "<td><input id='txtEditEnglishName' type='text' class='form-control' value='" +
                                this["EnglishName"] +
                                "' /></td>" +
                                "<td><input id='txtEditPersianName' type='text' class='form-control' value='" +
                                this["PersianName"] +
                                "' /></td>" +
                                "<td>" +
                                "<a class='common-btn' onclick='updateDevice(" +
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
                                this["BrandName"] +
                                "</td>" +
                                "<td>" +
                                this["EnglishName"] +
                                "</td>" +
                                "<td>" +
                                this["PersianName"] +
                                "</td>" +
                                "<td>" +
                                "<a class='common-btn' onclick='getDevice(" +
                                pageNumber +
                                ", " +
                                this["ID"] +
                                ")'>" +
                                "<img data-toggle='tooltip' data-placement='top' title='ویرایش' src='/Content/Images/Admin/edit.png' alt='ویرایش' />" +
                                "</a >" +
                                "<a class='common-btn' onclick='deleteDevice(" +
                                this["ID"] +
                                ")'>" +
                                "<img data-toggle='tooltip' data-placement='top' title='حذف' src='/Content/Images/Admin/delete.png' alt='حذف' />" +
                                "</a >" +
                                "</td>" +
                                "</tr>");
                        }
                    });
                var arrOption2 = new Array();
                $.each(response["Item4"],
                    function () {
                        arrOption2 += "<option value='" + this["ID"] + "'>" + this["EnglishName"] + "</option>";
                    });
                $("#tableBody").html(
                    $("#tableBody").html() +
                    "<tr id='hiddenElement' class='hidden-element'>" +
                    "<td></td>" +
                    "<td>" +
                    "<select id='drpBrand' name='drpBrand' class='form-control' style='padding-top: 0; width: 140px;'>" +
                    arrOption2 +
                    "<select>" +
                    "</td>" +
                    "<td><input id='txtEnglishName' type='text' class='form-control' /></td>" +
                    "<td><input id='txtPersianName' type='text' class='form-control' /></td>" +
                    "<td>" +
                    "<a class='common-btn' onclick='addDevice()'>" +
                    "<img data-toggle='tooltip' data-placement='top' title='ثبت' src='/Content/Images/Admin/tick.png' alt='ثبت' />" +
                    "</a >" +
                    "<a class='common-btn' onclick='hideElement()'>" +
                    "<img data-toggle='tooltip' data-placement='top' title='لغو' src='/Content/Images/Admin/cancel.png' alt='لغو' />" +
                    "</a >" +
                    "</td>" +
                    "</tr>"
                );
                if (response["Item3"] !== 1) {
                    $("#pagination").empty();
                    if (response["Item2"] !== 1) {
                        $("#pagination").html(
                            $("#pagination").html() +
                            "<li>" +
                            "<a class='common-btn' aria-label='Previous' onclick='getDevice(1,0)'>" +
                            "<span data-toggle='tooltip' data-placement='top' title='صفحه 1' aria-hidden='true' style='color: #5C6250;'>&lt;&lt;</span>" +
                            "</a></li>" +
                            "<li>" +
                            "<a class='common-btn' aria-label='Previous' onclick='getDevice(" +
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
                            "<a class='common-btn' aria-label='Next' onclick='getDevice(" +
                            (response["Item2"] + 1) +
                            ",0)'>" +
                            "<span aria-hidden='true' style='color: #5C6250;'>&gt;</span>" +
                            "</a></li>" +
                            "<li>" +
                            "<a class='common-btn' aria-label='Next' onclick='getDevice(" +
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
                var arrOption3 = new Array();
                $.each(response["Item4"],
                    function () {
                        arrOption3 += "<option value='" + this["ID"] + "'>" + this["EnglishName"] + "</option>";
                    });
                $("#emptyTableHead").empty();
                $("#emptyTableHead").html(
                    $("#emptyTableHead").html() +
                    "<tr><th>ردیف</th><th>برند</th><th>نام لاتین</th><th>نام فارسی</th><th>عملیات</th></tr>"
                );
                $("#emptyTableBody").empty();
                $("#emptyTableBody").html(
                    $("#emptyTableBody").html() +
                    "<tr>" +
                    "<td></td>" +
                    "<td>" +
                    "<select id='drpBrand' name='drpBrand' class='form-control' style='padding-top: 0; width: 140px;'>" +
                    arrOption3 +
                    "<select>" +
                    "</td>" +
                    "<td><input id='txtEnglishName' type='text' class='form-control' /></td>" +
                    "<td><input id='txtPersianName' type='text' class='form-control' /></td>" +
                    "<td>" +
                    "<a class='common-btn' onclick='addDevice()'>" +
                    "<img data-toggle='tooltip' data-placement='top' title='ثبت' src='/Content/Images/Admin/tick.png' alt='ثبت' />" +
                    "</a >" +
                    "<a class='common-btn' onclick='hideElement()'>" +
                    "<img data-toggle='tooltip' data-placement='top' title='لغو' src='/Content/Images/Admin/cancel.png' alt='لغو' />" +
                    "</a >" +
                    "</td>" +
                    "</tr>"
                );
                getDevice(pageNumber - 1, 0);
            }
            $("#divBtn").html(
                "<button id='btnInsert' type='button' class='btn btn-success btn-insert' onclick='showElement()'>افزودن دستگاه جدید</button>"
            );
        },
        true);
};

function addDevice() {
    var englishname = $("#txtEnglishName").val();
    var persianname = $("#txtPersianName").val();
    var brandId = $("#drpBrand").val();

    if (englishname === "") {
        LiteBoxShown("خطا", "لطفاً نام لاتین دستگاه را وارد نمایید.", "#F1A9A0", true);
        $("#LiteBoxButtom").click(function () {
            closeErrorMessage();
        });
        return;
    }

    if (persianname === "") {
        LiteBoxShown("خطا", "لطفاً نام فارسی دستگاه را وارد نمایید.", "#F1A9A0", true);
        $("#LiteBoxButtom").click(function () {
            closeErrorMessage();
        });
        return;
    }

    var parameters = {};
    var device = {};

    device["EnglishName"] = englishname;
    device["PersianName"] = persianname;
    device["BrandId"] = brandId;



    parameters["device"] = device;

    CallMethod("/Admin/Device/AddDevice", JSON.stringify(parameters), function (response) {
        if (response["Item1"] == "Success") {
            LiteBoxShown("موفقیت", "برند با موفقیت ثبت شد.", "#A2DED0", true);
            $("#LiteBoxButtom").click(function () {
                closeErrorMessage();
                hideElement();
                getDevice(response["Item2"], 0);
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

function updateDevice(ID) {
    var brandId = $("#brandSelect").val();
    var englishname = $("#txtEditEnglishName").val();
    var persianname = $("#txtEditPersianName").val();

    var page = $("#currentPage").text();

    if (englishname == "") {
        LiteBoxShown("خطا", "لطفاً نام لاتین دستگاه را وارد نمایید.", "#F1A9A0", true);
        $("#LiteBoxButtom").click(function () {
            closeErrorMessage();
        });
        return;
    }

    if (persianname == "") {
        LiteBoxShown("خطا", "لطفاً نام فارسی دستگاه را وارد نمایید.", "#F1A9A0", true);
        $("#LiteBoxButtom").click(function () {
            closeErrorMessage();
        });
        return;
    }

    var parameters = {};
    var brand = {};

    parameters["ID"] = ID;
    parameters["BrandId"] = brandId;
    parameters["EnglishName"] = englishname;
    parameters["PersianName"] = persianname;
    parameters["page"] = page;


    CallMethod("/Admin/Device/UpdateDevice", JSON.stringify(parameters), function (response) {
        if (response["Item1"] == "Success") {
            LiteBoxShown("موفقیت", "دستگاه با موفقیت ویرایش شد.", "#A2DED0", true);
            $("#LiteBoxButtom").click(function () {
                closeErrorMessage();
                getDevice(response["Item2"], 0);
            });
        } else if (response["Item1"] == "Error") {
            LiteBoxShown("خطا", "خطا در ویرایش دستگاه.", "#F1A9A0", true);
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

function deleteDevice(ID) {
    var page = $("#currentPage").text();

    var parameters = {};

    parameters["ID"] = ID;
    LiteBoxConfirmShown("از حذف دستگاه اطمینان دارید ؟",
        "",
        function () {
            CallMethod("/Admin/Device/DeleteDevice",
                JSON.stringify(parameters),
                function (response) {
                    if (response["Item1"] == "Success") {
                        LiteBoxShown("موفقیت", "دستگاه با موفقیت حذف شد.", "#A2DED0", true);
                        $("#LiteBoxButtom").click(function () {
                            closeErrorMessage();
                            if (response["Item2"] == 1) {
                                getDevice(1, 0);
                            } else {
                                getDevice(page, 0); 
                            }
                        });
                    } else if (response == "Error") {
                        LiteBoxShown("خطا", "خطا در حذف دستگاه.", "#F1A9A0", true);
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