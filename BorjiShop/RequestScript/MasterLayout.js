//$(document).ready(function () {
//    var parameters = {};
//    CallMethod("/Shop/Index", JSON.stringify(parameters), function(response) {
//        $("#countShopCart").html(response);
//    },false);
//});

function getDeviceByBrandId() {
    var brandId = $("#drpbrandId").val();
    var parameters = {};
    parameters["brandId"] = brandId;
    parameters["pieceId"] = 0;

    CallMethod("/Admin/Device/GetDeviceByBrandId",
        JSON.stringify(parameters),
        function (response) {
            var deviceList = "<option value='0'>انتخاب دستگاه</option>";
            if (response["Item1"].length > 0) {
                $.each(response["Item1"],
                    function () {
                        deviceList += "<option value='" + this["ID"] + "'>" + this["EnglishName"] + "</option>";
                    });
            };
            $("#drpdeviceId").empty();
            $("#drpdeviceId").html(
                $("#drpdeviceId").html() +
                deviceList
            );
        },
        true);
}