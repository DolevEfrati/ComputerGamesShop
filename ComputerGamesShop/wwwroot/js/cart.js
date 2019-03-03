$(document).ready(function () {
    if (document.getElementById("games-list").getElementsByTagName("li").length == 0) {
        document.getElementById("confirmBtn").disabled = true;
    }

    var sum = 0
    $('.price').each(function (idx, p) {
        sum += parseFloat(p.innerText.split('$')[0])
    })
    $('#sum').html(sum + '$')
    $('#currency-btn').html($('#currency-btn').text() + ' ' + geoplugin_currencySymbol())
});

$('#currency-btn').click(function () {
    $('#sum').html(geoplugin_currencyConverter($('#sum').text().split('$')[0]))
    $('#currency-btn').hide()
})

function filterGames(condition) {
    var input, filter, ul, li, h4, i, txtValue;
    input = document.getElementById("gameNameSearch");
    filter = input.value.toUpperCase();
    ul = document.getElementById("games-list");
    li = ul.getElementsByTagName("li");
    for (i = 0; i < li.length; i++) {
        h4 = li[i].getElementsByClassName("title-string")[0];
        txtValue = h4.textContent || h4.innerText;
        if (txtValue.toUpperCase().indexOf(filter) > -1) {
            li[i].style.display = "";
        } else {
            li[i].style.display = "none";
        }
    }
}

function deleteGamefromCart(gameId) {
    var gameIdString = gameId.toString();
    $.ajax({
        type: 'POST',
        url: "/api/deletefromCart",
        data: { gameId: gameId },
        success: function () {
            location.reload();
        },
        error: function (err, status, reason) {
            alert(status);
        }
    });
}

function saveOrder() {
    var elem = document.getElementById("storesList");
    var storeId = elem.options[elem.selectedIndex].value;

    $.ajax({
        type: 'POST',
        url: "/api/saveOrder",
        data: { storeId: storeId },
        success: function () {
            Swal.fire({
                type: 'success',
                title: 'Your order has been saved',
                showConfirmButton: false,
                timer: 2500,
                onClose: () => {
                    location.href = '/';
                }
            })
        },
        error: function (xhr, status) {
            Swal.fire({
                type: 'error',
                title: 'Oops...',
                text: 'Something went wrong! status:' + status,
            })
        }
    });
}