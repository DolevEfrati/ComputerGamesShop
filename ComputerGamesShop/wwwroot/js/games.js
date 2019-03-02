$("#Price").change(function () {
    $("#max").text($(this).val() + '$');
});

$('.price').each(function (idx, p) {
    $(p).html(geoplugin_currencyConverter(p.innerText))
})

function showVal(newVal) {
    document.getElementById("max").innerHTML = newVal;
}

function addToCart(gameId) {
    var gameIdString = gameId.toString();
    $.ajax({
        type: 'POST',
        url: "/api/addToCart",
        data: { gameId: gameId },
        success: function () {
            document.getElementById('addToCartBtn_' + gameIdString).remove();
            $('#inCart_' + gameIdString).removeClass('invisible').addClass('visible');
        },
        error: function (err, status, reason) {
            alert(status);
        }
    });
}