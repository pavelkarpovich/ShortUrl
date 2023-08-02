//var img = document.getElementById("partialContainer");

if ($('#partialContainer').length > 0) {
    //alert('exist')

    $.ajax({
        type: 'GET',
        url: '/account/getusername',
        //dataType: "text"
    })
        .done(function (result) {

            //let x = result;

            document.getElementById("partialContainer").innerHTML = result;

        });
}

$('html').click(function () {
    var id = event.target.getAttribute("id");
    //if (id != 'myUrlsButton' && id != 'sidebar')
    if (!(event.target.id == "sidebar" || $(event.target).parents("#sidebar").length || event.target.id == "myUrlsButton"))
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

                //let x = result;

                for (var i = 0; i < result.length; i++) {
                    var url = result[i].urlValue;
                    var alias = result[i].aliasValue;

                    document.getElementById('sidebar-nav').innerHTML = '';
                    const div = document.createElement('div');

                    div.className = 'row';

                    div.innerHTML = `
                        <div class="my-horizontal-item">
                            <div class="sidebar-url">` + result[i].urlValue +`</div>
                            <div class="sidebar-alias">` + result[i].aliasValue +`</div>
                            <div class="buttons">
                                <button class="sidebar-gotourl-button" alias="` + result[i].aliasValue + `" onclick="sidebarGoToUrl(this)">Go to Url</button>
                                <button class="sidebar-delete-button" alias="` + result[i].aliasValue + `">Delete</button>

                            </div>
                        </div>
  `;

                    document.getElementById('sidebar-nav').appendChild(div);
                }
        

            });
    }
        
    else
        sideBar.style.visibility = "hidden";
});


function sidebarGoToUrl(element) {
    var x = element.getAttribute("alias");
    x = x;
}

