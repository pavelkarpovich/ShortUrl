if ($('#partialContainer').length > 0) {
    $.ajax({
        type: 'GET',
        url: '/account/getusername',
    })
        .done(function (result) {
            document.getElementById("partialContainer").innerHTML = result;
        });
}

$('html').click(function () {
    if (!(event.target.id == "sidebar" || $(event.target).parents("#sidebar").length || event.target.id == "myUrlsButton" || event.target.outerText == 'Delete'))
     document.getElementById('sidebar').style.visibility = "hidden";
});


$('#myUrlsButton').click(function (event) {
    if (sideBar.style.visibility == "hidden") {
        sideBar.style.visibility = "visible";
        $.ajax({
            type: 'GET',
            url: '/api/alias/getmyurls',
            dataType: "json"
        })
            .done(function (result) {
                let text = "";
                for (var i = 0; i < result.length; i++) {
                    document.getElementById('sidebar-nav').innerHTML = '';
                    let div = document.createElement('div');
                    div.className = 'row';
                    text = text + 
                    `<div class="sidebar-horizontal-item">
                            <div class="sidebar-url">` + result[i].urlValue +`</div>
                            <div class="sidebar-alias">` + result[i].aliasValue +`</div>
                            <div class="buttons">
                                <button class="sidebar-gotourl-button" alias="` + result[i].aliasValue + `" onclick="sidebarGoToUrl(this)">Go to Url</button>
                                <button class="sidebar-delete-button" alias="` + result[i].aliasValue + `" onclick="sidebarDelete(this)">Delete</button>
                            </div>
                        </div>`;
                    div.innerHTML = text;
                    document.getElementById('sidebar-nav').innerHTML = document.getElementById('sidebar-nav').innerHTML + div.innerHTML;
                }
            });
    }
    else
        sideBar.style.visibility = "hidden";
});


function sidebarGoToUrl(element) {
    var alias = element.getAttribute("alias");
    window.open(alias, "_blank");
}

function sidebarDelete(element) {
    var aliasFull = element.getAttribute("alias");
    var index = aliasFull.lastIndexOf('/');
    var alias = aliasFull.substring(index + 1, aliasFull.length);

    $.ajax({
        type: 'DELETE',
        url: 'api/alias/deletealias',
        data: { AliasValue: alias },
    })
        .done(function () {
            var parent = element.closest("#sidebar-nav");
            var child = element.closest(".sidebar-horizontal-item");
            parent.removeChild(child);
        })
}

