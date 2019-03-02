$(document).ready(function () {
    if (document.getElementById("games-list").getElementsByTagName("li").length == 0) {
        document.getElementById("confirmBtn").disabled = true;
    }
});

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
            alert('done');
            location.href = '/';
        },
        error: function (xhr, status) {
            alert(status);
        }
    });
}