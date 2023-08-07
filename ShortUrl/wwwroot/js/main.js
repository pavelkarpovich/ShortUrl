var shortenButton = document.getElementById('shortenbutton');
var urlField = document.getElementById('url-field');
var urlError = document.getElementById('url-error');
var aliasField = document.getElementById('alias-field');
var aliasError = document.getElementById('alias-error');
var goToUrlButton = document.getElementById('goToUrlButton');
var copyUrlButton = document.getElementById('copyUrlButton');
var sideBar = document.getElementById('sidebar');
goToUrlButton.style.visibility = "hidden";
copyUrlButton.style.visibility = "hidden";
sideBar.style.visibility = "hidden";

function validateUrl()
{
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
    if (shortenButton.innerHTML == "Shorten another") {
        urlField.value = "";
        aliasField.value = "";
        aliasField.disabled = false;
        aliasField.style.color = "black";
        urlField.disabled = false;
        urlField.style.color = "black";
        goToUrlButton.style.visibility = "hidden";
        copyUrlButton.style.visibility = "hidden";
        shortenButton.innerHTML = "Shorten URL";
    }
    else {
        let errorUrl = false;
        let errorAlias = false;
        if (urlField.value.length == 0) {
            urlError.style.color = "red";
            urlError.innerHTML = "The URL field is required."
            urlField.style.borderColor = 'red';
            errorUrl = true;
        }
        if (urlField.value.length > 0 && !urlField.value.match(/^http:\/\/|(www\.)?[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$/)) {
            urlError.innerHTML = "Please enter a valid Url";
            urlError.style.color = "red";
            errorUrl = true;
        }
        if (aliasField.value.length > 0 && !aliasField.value.match(/^[a-zA-Z0-9-]+$/)) {
            aliasError.style.color = "red";
            aliasError.innerHTML = "The Alias format is invalid.";
            aliasField.style.borderColor = 'red';
            errorAlias = true;
        }
        if (aliasField.value.length > 0 && aliasField.value.length < 5) {
            aliasError.style.color = "red";
            aliasError.innerHTML = "The Alias must be at least 5 characters";
            aliasField.style.borderColor = 'red';
            errorAlias = true;
        }

        if (errorAlias == false && errorUrl == false) {

            if (!urlField.value.startsWith('https://') && !urlField.value.startsWith('http://')) {
                urlField.value = 'http://' + urlField.value;
            }
            urlError.style.color = "black";
            urlError.innerHTML = "";
            urlField.style.borderColor = "rgb(22, 22, 22)";
            if (aliasField.value.length > 0) {
                $.ajax({
                    type: 'POST',
                    url: 'api/alias/checkalias',
                    data: { AliasValue: aliasField.value },
                    dataType: "json"
                })
                    .done(function (result) {
                        if (result) {
                            aliasError.style.color = "red";
                            aliasError.innerHTML = "Duplicated alias";
                            aliasField.style.borderColor = 'red';
                            errorAlias = true;
                        }
                            else {
                                $.ajax({
                                    type: 'POST',
                                    url: 'api/alias/submitalias',
                                    data: { AliasValue: aliasField.value, UrlValue: urlField.value }
                                })
                                    .done(function (result) {

                                        aliasField.value = result;
                                        aliasField.disabled = true;
                                        aliasField.style.color = "darkgreen";
                                        urlField.disabled = true;
                                        urlField.style.color = "darkgreen";
                                        goToUrlButton.style.visibility = "visible";
                                        copyUrlButton.style.visibility = "visible";
                                        shortenButton.innerHTML = "Shorten another";
                                        aliasError.style.color = "black";
                                        aliasError.innerHTML = "";
                                        aliasField.style.borderColor = "rgb(22, 22, 22)";
                                    })
                            }
                    })
            }
            else {
                aliasField.value = Math.random().toString(36).slice(5);
                $.ajax({
                    type: 'POST',
                    url: 'api/alias/submitalias',
                    data: { AliasValue: aliasField.value, UrlValue: urlField.value }
                })
                    .done(function (result) {

                        aliasField.value = result;
                        aliasField.disabled = true;
                        aliasField.style.color = "darkgreen";
                        urlField.disabled = true;
                        urlField.style.color = "darkgreen";
                        goToUrlButton.style.visibility = "visible";
                        copyUrlButton.style.visibility = "visible";
                        shortenButton.innerHTML = "Shorten another";
                        aliasError.style.color = "black";
                        aliasError.innerHTML = "";
                        aliasField.style.borderColor = "rgb(22, 22, 22)";
                    })
            }
        }

        if (errorAlias == false && errorUrl == true) {
            aliasError.style.color = "black";
            aliasError.innerHTML = "";
            aliasField.style.borderColor = "rgb(22, 22, 22)";
        }
    }
}

function goToUrl()
{
    window.open(aliasField.value, "_blank");
}

function copyUrl()
{
    navigator.clipboard.writeText(aliasField.value);
}


