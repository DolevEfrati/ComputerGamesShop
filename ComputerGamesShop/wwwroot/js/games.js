$("#Price").change(function () {
    $("#max").text($(this).val() + '$');
});

$('.price').each(function (idx, p) {
    $(p).html(geoplugin_currencyConverter(p.innerText))
})

function showVal(newVal) {
    document.getElementById("max").innerHTML = newVal;
}