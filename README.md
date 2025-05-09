# Storied

1.- In order to Run the application you need to replace the connection string in appsettings.json to point to your local environment.

Example :

"ConnectionStrings": { "Sql": "Server=.;Database=Storied;Trusted_Connection=True;TrustServerCertificate=True" }

2.- Once you have youur connection string in place you need to update the database using the migrations.

The application is configred  to Esure the Database is created when you run it for first  time,  in case the Db is not created you can Excecute:

 update-database command from Package Manager Console

 ![image](https://github.com/user-attachments/assets/2492160e-7858-4313-9944-aa65ef1540d6)

 You can check in Yor SQL Server Management Studio when the DB is Created:

 ![image](https://github.com/user-attachments/assets/903642ea-6b49-4216-865c-da7ae2f470e5)

 When the application is running yo will se the Swagger Window in which you can test all the Endpoints.

 ![image](https://github.com/user-attachments/assets/48c4fd7e-9042-4700-900d-86fcf9e3d740)


 I Have Impleented CQRS Pattern as requested, combined with Generic Repository pattern, UUnit of Work, Mediator Pattern.
 All the validations are using Fluent Validation  and the logging is using a middleware.

 The Exceptions are handled globally.

 All the  application is on Clean Architecture and is followinr SOLID principles.

 Had I had more time, I would have included additional unit tests, and also Integration Tests and implemented caching as well. I aimed to deliver the work as professionally as possible.

 Rafael C.


 




