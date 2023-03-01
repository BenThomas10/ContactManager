# Contact Manager
This solution contains an easy to use and simple contact manager. It also works well on mobile devices!

## Technologies
- Visual Studio 2022
- SQL Server 2019
- .NET 7
- EntityFramework Core 7
- Bootstrap 4.4.1
- SignalR JS 2.4.1
- JQuery 3.3.1

## Local Enviroment Setup

1. You need to have the .NET 7 SDK installed. Be sure to download the latest version for Visual Studio 2022.

2. Use the latest version of Visual Studio 2022: https://visualstudio.microsoft.com/downloads/

3. Install SQL Server 2019 (or later version), Developer or Express edition: https://www.microsoft.com/en-us/sql-server/sql-server-downloads. By default this uses localdb; adjust the connection string in appsettings.json if needed. 


## Usage

* To get started, navigate to the **Contact** tab in the Navigation bar. You should see some existing contacts already. This was provided by the seed data we generated earlier.

* To create a new contact press the blue **New Contact** button and fill in the contact's First and Last name in the popup window.

* If you wish to add any email addresses, fill in the **Email** text input, define if it is a Personal email or a Business email, and press the **Add** button. You can use the red **X** button to delete an email from the list. Once you are finished, be sure to press the green **Save** button to save your changes.

* If you wish to add any addresses, fill in the **Address** text inputs, define if it is a Primary address or a Business address, and press the **Add** button. You can use the red **X** button to delete an address from the list. Once you are finished, be sure to press the green **Save** button to save your changes.

* If you wish to edit a contact, just double click on their name and you will be greeted by the Edit Contact popup window. Once you are finished, be sure to press the green **Save** button to save your changes. Use the **Cancel** button to discard any unwanted changes.

* To delete a contact just press the red **X** and confirm that you want to delete that contact.