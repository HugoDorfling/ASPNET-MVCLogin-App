# ASPNET-MVCLogin-App
## ASP.NET MVC C#  - Login and Manage Info application. 

Authenticate against a database and display Info page for successful authentication. 
Display and allow editing of user information on info page based on two tables: 
Person (Id, Name, Surname, Password, LastLogin) and Info (PersonId, TelNo, CellNo, AddressLine1, AddressLine2, AddressLine3, AddressCode, PostalAddress1, PostalAddress2, PostalCode) tables. 

### Test From Recruitment Agency and project specifications:
#### Prerequisites

Create a 2-page website using the following technologies and techniques:
MVC, C#, Visual Studio, MS SQL, any styling framework (Bootstrap, DevExtreme, etc)

Test
Create a database based on the structure specified below.
Create a Login page, which uses a Username and Password authentication against a database to authenticate a user.
Display a message if login failed.
Redirect to Info page if login successful.
Create Info page, which displays and allows editing of user information, including password (except for name and surname). The user information should be updated without using post-back.

Send screenshots of the completed and styled site, as well as all the code in an easily deployable format for assessment – this includes a DB backup.

The database structure:
Person
Id
Name
Surname
Password
LastLogin
Info
PersonId
TelNo
CellNo
AddressLine1
AddressLine2
AddressLine3
AddressCode
PostalAddress1
PostalAddress2
PostalCode
