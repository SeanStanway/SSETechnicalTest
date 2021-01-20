# SSETechnicalTest
Technical Test for MMT

# Project Layout
1. SSETechnicalTest - C# NET Core Console application consuming API
2. ProductApi - C# ASP NET Core API retrieving data from SQL Library
3. SqlLibrary - C# NET Core library to bridge gap between API and SQL database, running stored procedures to retrieve data
4. UnitTesting - C# NET Core unit test library
5. SQL Files - Contains SQL files for creating database tables and stored procedure

### SSETechnicalTest
A C# NET Core console application. Has simple console interface for user to read and then perform desired actions.

When data is retrieved from the API it is written to a text file that relates to the function selected by the user
(e.g. featuredProducts-[current time].txt, availableCategories-[current time].txt, productsByCategoryName-[current time].txt).

A user is notified on the console if there is a failed API call, with the response message written to the console for
the user to see.

### ProductApi
A C# ASP NET Core web API application. Has three calls that can be consumed by an outside application:

- [GET] GetFeaturedProducts
- [GET] GetAvailableCategories
- [GET] GetProductsByCategoryName(string categoryName) 
	- This call will return a Bad Request if the category name is not in the available categories

Each call uses the SQL library to get data from the SQL database.

### SqlLibrary
C# Net Core library. Prime functionality is to send a command to the SQL database and execute stored procedures.
Heavy exception catching implemented, Serilogging enabled to log these if they occur.

### Unit Testing
C# Net Core Unit Test project. Testing based solely around the SSETechnicalTest console application, utilising Moq
and MSTest to allow testing of class that performs main functionality. No testing around API due to slip ups with 
Unity configuration in NET Core. 

### SQL Files
SQL files used for creating the database schemas and stored procedures. Files contain the following:

##### CreateTables_schema.sql 
  - Creates three tables; Categories, Products, and FeaturedProductCategories. 
  - Categories: Id and Name. 
  - Products: SKU, CategoryId, Name, Description, and Price.
  - FeaturedProductCategories: CategoryId (This table is to store the featured product IDs. Potential for writing a POST API call that allows a user to clear that table and write new IDs into it)
##### GetAvailableCategories_procedure.sql
  - Creates a procedure that gets a list of all categories in the Categories table.
##### GetFeaturedProducts_procedure.sql
  - Creates a procedure that gets a list of products based on if the product's CategoryId is within the FeaturedProductCategories table.
##### GetProductsByCategory_procedure.sql
  - Creates a procedure that gets a list of products. Uses the 'categoryName' parameter to determine the CategoryId to get products by.
##### PopulateCategories_procedure.sql
  - Procedure to seed Categories table
##### PopulateProducts_procedure.sql
  - Procedure to see Products table
