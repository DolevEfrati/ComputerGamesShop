$("#Price").change(function () {
    $("#max").text($(this).val() + '$');
});

function showVal(newVal) {
    document.getElementById("max").innerHTML = newVal;
}