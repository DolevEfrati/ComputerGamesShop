function filterPublishers(condition) {
    var input, filter, ul, li, h4, i, txtValue;
    input = document.getElementById("publisherNameSearch");
    filter = input.value.toUpperCase();
    ul = document.getElementById("publishers-list");
    li = ul.getElementsByTagName("li");
    for (i = 0; i < li.length; i++) {
        h4 = li[i].getElementsByClassName("name-string")[0];
        txtValue = h4.textContent || h4.innerText;
        if (txtValue.toUpperCase().indexOf(filter) > -1) {
            li[i].style.display = "";
        } else {
            li[i].style.display = "none";
        }
    }
}