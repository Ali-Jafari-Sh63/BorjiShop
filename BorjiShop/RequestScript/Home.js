function searchPiece(page) {
    var pieceTypeId = $("#drppieceTypeId").val();
    var brandId = $("#drpbrandId").val();
    var deviceId = $("#drpdeviceId").val();

    var parameters = {};

    parameters["pieceTypeId"] = pieceTypeId;
    parameters["brandId"] = brandId;
    parameters["deviceId"] = deviceId;
    parameters["page"] = page;

    CallMethod("/Home/SearchPiece",
        JSON.stringify(parameters),
        function (response) {
            if (response["Item1"].length > 0) {
                $("#sliderSection").empty();
                $("#mainCaption").empty();
                $("#mainCaption").html(
                    $("#mainCaption").html() +
                    "نتیجه جستجوی شما"
                );
                $("#mainPiece").empty();
                $(response["Item1"]).each(function () {
                    $("#mainPiece").html(
                        $("#mainPiece").html() +
                        "<div class='thumbnail col-xs-12 col-md-4'>" +
                        "<a href='#'>" +
                        "<img src='/PieceImage/" +
                        this["FileName"] +
                        "' class='img-responsive img-rounded img-custom hvr-grow' style='height: 225px;' />" +
                        "<p class='caption-custom'>" +
                        this["PieceTypeName"] +
                        " " +
                        this["BrandName"] +
                        " " +
                        this["DeviceName"] +
                        "</p>" +
                        "</a>" +
                        "<p class='price'>" +
                        this["Price"] +
                        " تومان</p>" +
                        "<div class='text-center'>" +
                        "<a href='#' role='button' class='btn btn-success text-center' onclick='addToCart("+ this["ID"] +")'>افزودن به سبد خرید</a>" +
                        "</div>" +
                        "</div>"
                    );
                });
                if (response["Item3"] !== 1) {
                    $("#pagination").empty();
                    if (response["Item2"] !== 1) {
                        $("#pagination").html(
                            $("#pagination").html() +
                            "<li>" +
                            "<a class='common-btn' aria-label='Previous' onclick='searchPiece(1)'>" +
                            "<span data-toggle='tooltip' data-placement='top' title='صفحه 1' aria-hidden='true' style='color: #5C6250;'>&lt;&lt;</span>" +
                            "</a></li>" +
                            "<li>" +
                            "<a class='common-btn' aria-label='Previous' onclick='searchPiece(" +
                            (response["Item2"] - 1) +
                            ")'>" +
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
                            "<a class='common-btn' aria-label='Next' onclick='searchPiece(" +
                            (response["Item2"] + 1) +
                            ")'>" +
                            "<span aria-hidden='true' style='color: #5C6250;'>&gt;</span>" +
                            "</a></li>" +
                            "<li>" +
                            "<a class='common-btn' aria-label='Next' onclick='searchPiece(" +
                            (response["Item3"]) +
                            ")'>" +
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
            } else {
                $("#mainCaption").empty();
                $("#mainCaption").html(
                    $("#mainCaption").html() +
                    "جستجوی شما نتیجه ای در بر نداشت "
                );
                $("#mainPiece").empty();
                $("#pagination").empty();
            }
        },
        true);
}

function searchPieceByBrand(brandID, page) {
    var pieceTypeId = 0;
    var brandId = brandID;
    var deviceId = 0;

    var parameters = {};

    parameters["pieceTypeId"] = pieceTypeId;
    parameters["brandId"] = brandId;
    parameters["deviceId"] = deviceId;
    parameters["page"] = page;

    CallMethod("/Home/SearchPiece",
        JSON.stringify(parameters),
        function (response) {
            if (response["Item1"].length > 0) {
                console.log(response);
                $("#sliderSection").empty();
                $("#mainCaption").empty();
                $("#mainCaption").html(
                    $("#mainCaption").html() +
                    "قطعات اصلی " + response["Item7"]
                );
                $("#mainPiece").empty();
                $(response["Item1"]).each(function () {
                    $("#mainPiece").html(
                        $("#mainPiece").html() +
                        "<div class='thumbnail col-xs-12 col-md-4'>" +
                        "<a href='#'>" +
                        "<img src='/PieceImage/" +
                        this["FileName"] +
                        "' class='img-responsive img-rounded img-custom hvr-grow' style='height: 225px;' />" +
                        "<p class='caption-custom'>" +
                        this["PieceTypeName"] +
                        " " +
                        this["BrandName"] +
                        " " +
                        this["DeviceName"] +
                        "</p>" +
                        "</a>" +
                        "<p class='price'>" +
                        this["Price"] +
                        " تومان</p>" +
                        "</div>"
                    );
                });
                if (response["Item3"] !== 1) {
                    $("#pagination").empty();
                    if (response["Item2"] !== 1) {
                        $("#pagination").html(
                            $("#pagination").html() +
                            "<li>" +
                            "<a class='common-btn' aria-label='Previous' onclick='searchPiece(1)'>" +
                            "<span data-toggle='tooltip' data-placement='top' title='صفحه 1' aria-hidden='true' style='color: #5C6250;'>&lt;&lt;</span>" +
                            "</a></li>" +
                            "<li>" +
                            "<a class='common-btn' aria-label='Previous' onclick='searchPiece(" +
                            (response["Item2"] - 1) +
                            ")'>" +
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
                            "<a class='common-btn' aria-label='Next' onclick='searchPiece(" +
                            (response["Item2"] + 1) +
                            ")'>" +
                            "<span aria-hidden='true' style='color: #5C6250;'>&gt;</span>" +
                            "</a></li>" +
                            "<li>" +
                            "<a class='common-btn' aria-label='Next' onclick='searchPiece(" +
                            (response["Item3"]) +
                            ")'>" +
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
            } else {
                $("#mainCaption").empty();
                $("#mainCaption").html(
                    $("#mainCaption").html() +
                    "جستجوی شما نتیجه ای در بر نداشت "
                );
                $("#mainPiece").empty();
                $("#pagination").empty();
            }
        },
        true);
}

