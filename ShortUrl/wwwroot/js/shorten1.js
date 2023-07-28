var urlField = document.getElementById('url-field');
var urlError = document.getElementById('url-error');
var aliasField = document.getElementById('alias-field');
var aliasError = document.getElementById('alias-error');
var goToUrlButton = document.getElementById('gotourl');

function validateUrl()
{
    //if (!urlField.value.match(/(ftp|http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/)
    if (!urlField.value.match(/^http:\/\/|(www\.)?[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$/)
    ) {
        urlError.innerHTML = "Please enter a valid Url";
        return false;
    }
    else
    {
        urlError.innerHTML = "";
        return true;
    }
}

function shortenUrl()
{
    if (!aliasField.value.match(/^[a-zA-Z0-9-]+$/)) {
        aliasError.innerHTML = "Incorrect alias";
    }
    else if (aliasField.value.length < 5)
    {
        aliasError.innerHTML = "The Alias must be at least 5 characters.";
    }
    else if (validateUrl())
    {
        aliasError.innerHTML = "";

        $.ajax({
            type: 'POST',
            url: 'api/alias/checkalias',
            //url: 'api/collection/getstatus',
            data: { AliasValue: aliasField.value },
            //data: { privateTurboItemId: itemId },
            dataType: "json"
        })
            //$.ajax({
            //    url: '/collection/setstatus',
            //    data: { "privateTurboItemId": itemId, "statusId": statusId },
            //    dataType: "text"
            //})type: 'POST',
            //    //contentType: "application/json",
            //    //url: 'api/collection/getstatus',
            //    
            .done(function (result) {
                if (result) {
                    aliasError.innerHTML = "Duplicated alias";
                }
                else
                {
                    $.ajax({
                        type: 'POST',
                        url: 'api/alias/submitalias',
                        data: { AliasValue: aliasField.value, UrlValue: urlField.value }
                        //dataType: "json"
                    })
                        .done(function (result) {

                            aliasField.value = result;
                            aliasField.disabled = true;
                            aliasField.style.color = "darkgreen";
                            urlField.disabled = true;
                            urlField.style.color = "darkgreen";

                            //goToUrlButton

                        })
                }
            })
    }  
}

function goToUrl()
{
    window.open(aliasField.value, "_blank");
}