# ShortUrl

ShortUrl is a web application used for creating short links to users' web resources.

## Description
By entering values of initial URL and alias and pressing button "Shorten URL" shortened link of type to the initial URL is created. Link [hostname]/[alias] redirects to the user's initial URL.
Button "Copy URL" copies created URL to clipboard, button "Go to URL" opens a new tab with created link.
Authentification based on login/password is impplemented. For authorized users links are saved and can be viewed by pressing "My URLs" button. There is a possibility to delete a link from the list.

## How to use
Running ShortUrl application on local machine requires .NET 7 to be installed.
Clone the repo or download zip file of the solution from https://github.com/pavelkarpovich/ShortUrl. Open file ShortUrl.sln in Visual Studio and start the app.

## Technologhies
The application is developed with ASP.NET Core.
MVC is used for main, sing up and sing in page as well as handling redirect.
Web Api is used for creating and deleting shortened links, returning a list of user's links.
Data is stored in database SQL Server. Entity Framework Core is used to access DB.
Unit tests are implemented using NUnit framework.
Serilog logging is added with saving logs in files.
