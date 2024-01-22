# Bank

### Task:
Develop a .Net API for a financial management application

### Objective:
- The API will allow users to perform CRUD (Create, Read, Update, Delete) operations on financial transactions and account information. This includes creating new transactions, viewing transaction history, updating or deleting existing transactions, and managing account information such as balances and account details.
- The API should handle a large amount of data and provide efficient responses to user requests. This means the API should be able to handle a high volume of requests and quickly retrieve and return the necessary data to the user.
- The API should also provide advanced reporting capabilities, such as the ability to generate custom reports that involve multiple tables and a large amount of data. This includes generating reports that involve complex data relationships, such as joining multiple tables and filtering data based on specific criteria. (Optional) The ability to export reports in various formats (e.g. PDF, Excel).
- (Optional) The API should also implement secure authentication and authorization so that only authorized users can access the financial data.

### Reports:
1. Transactions by location: This report would provide a breakdown of all transactions that have occurred within a specific geographic location. The report would involve joining the transactions table with the location table in order to retrieve the location information for each transaction.
2. Transactions by bank: This report would provide a breakdown of all transactions that have occurred across different banks. The report would involve joining the transactions table with the bank table in order to retrieve the bank information for each transaction.
3. Transactions by person: This report would provide a breakdown of all transactions that have occurred for a specific person. The report would involve joining the transactions table with the person table in order to retrieve the person information for each transaction.
4. Transactions history: This report would provide a historical view of all transactions that have occurred over time. The report would involve querying the transactions table and filtering the data based on a specific date range.
5. Transactions by category: This report would provide a breakdown of all transactions that have occurred across different categories. The report would involve joining the transactions table with the category table in order to retrieve the category information for each transaction.
6. Transactions by type: This report would provide a breakdown of all transactions that have occurred across different types (e.g. deposit, withdrawal, transfer, etc.). The report would involve querying the transactions table and filtering the data based on a specific type.
(Optional)
1. Monthly transaction summary: This report would provide a summary of all transactions that have occurred within a specific month, grouped by category and bank. The report would involve joining the transactions table with the category and bank tables in order to retrieve the relevant information for each transaction. Additionally, the report would involve performing calculations to compute totals for each category and bank, such as the total number of transactions and the total amount of money involved.
2. Transaction prediction: This report would provide a prediction of future transactions based on historical transaction data. The report would involve joining multiple tables, such as transactions, people, locations and bank table. The report would also involve analyzing the historical data to identify patterns and trends, and then using that information to make predictions about future transactions.
3. Transactions by account and category: This report would provide a breakdown of all transactions that have occurred for a specific account across different categories. The report would involve joining the transactions table with the account and category table in order to retrieve the account and category information for each transaction. Additionally, the report would involve performing calculations to compute totals for each category, such as the total number of transactions and the total amount of money involved.

### Requirements:
- Use C# and the .Net framework
- Utilize Entity Framework to interact with the database
- Implement endpoints for handling transactions, account information, and user authentication
- Implement database migrations as necessary
- Optimize database queries to ensure efficient performance
- Document the codebase and API endpoints
- (Optional) Write thorough unit tests to ensure proper functionality

### Deliverables:
- The completed API, including all source code and documentation
- A detailed report outlining the design and implementation of the API, including any
challenges encountered and solutions implemented
- A test plan and results demonstrating the API's functionality and performance

### Database schema:

Transactions table:
- transaction_id (int, primary key)
- transaction_date (date)
- transaction_amount (float)
- transaction_type (varchar)
- bank_id (int, foreign key)
- location_id (int, foreign key)
- person_id (int, foreign key)
- category_id (int, foreign key)
  
Banks table:
- bank_id (int, primary key)
- bank_name (varchar)
- bank_address (varchar)
- bank_phone (varchar)
  
Locations table:
- location_id (int, primary key)
- location_name (varchar)
- location_address (varchar)
- location_latitude (float)
- location_longitude (float)
  
People table:
- person_id (int, primary key)
- first_name (varchar)
- last_name (varchar)
- phone (varchar)
- address (varchar)
- email (varchar)
- account_number (varchar)
  
Categories table:
- category_id (int, primary key)
- category_name (varchar)
- category_description (varchar)

## Solution
This is a object-oriented rest api server based on the: ASP.NET Core and MVC pattern. All mandatory registers and reports are implemented + export to pdf/excel. A login system using Identity Server has been introduced

### Normalized database schema

Transactions table:
- transaction_id (int, primary key)
- transaction_date (date)
- transaction_amount (float)
- transaction_type_id (foreign key to TransactionsTypes table)
- bank_id (int, foreign key to Banks table)
- location_id (int, foreign key to Locations table)
- account_id (guid, foreign key to Accounts table)
- category_id (int, foreign key to Categories table)

TransactionsTypes table:
- id (primary key)
- type (varchar)

Accounts table:
- account_number (guid, primary key)
- account_name (varchar)
- person_id (int, foreign key to Persons table)

Banks table:
- bank_id (int, primary key)
- bank_name (varchar)
- bank_address (varchar)
- phone_number (varchar)

Locations table:
- location_id (int, primary key)
- location_name (varchar)
- address_id (foreign key to Addresses table)
- location_latitude (float)
- location_longitude (float)

Persons table:
- person_id (int, primary key)
- first_name (varchar)
- last_name (varchar)
- phone_number (varchar)
- address_id (foreign key to Addresses table)
- email (varchar)

Addresses table:
- address_id (int, primary key)
- Description (varchar)

Categories table:
- category_id (int, primary key)
- category_name (varchar)
- category_description (varchar)

## Getting Started
**Application is supposed to be run using an Visual Studio 2022**
* In the Visual Studio, clone repository using the link ```https://github.com/RadoslavPanteleev/Bank.git```
* Open Tools -> Command Line -> Developer PowerShell and type ```dotnet restore```, to restore the required NuGet libraries.
* You need Micorsoft SQL Server Installed
* In appsettings.json, change WebApiDatabase -> Data Source to your database server instance, user and password. (You don't need to manually create database)
* In appsettings.json, set "TempDirectory" for pdf/excel export files.
* Open Tools -> Command Line -> Developer PowerShell and type ```Update-Database```, to apply all migrations to database.
* The default user for login into the API is ```admin``` with password ```Test1234.```
* All users pasword is ```Test1234.```
* For tests you can use for example user: ```renee_fay77``` with pasword: ```Test1234.```

## Generate fake test data
Fake test data was generated using Bogus library in Test Data Generator tool. All generated data was applied to migrations.

## Used technologies
* .NET 6.0
* ASP.NET Web API

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE) file for details
