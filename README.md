# Bank-API
Bank API

This is a simple ASP.NET Core Web API application for managing banks and bank accounts. The application is built using .NET 8 and Entity Framework Core.

#Features

Manage banks (Create, Read, Update, Delete)

Manage bank accounts (Create, Read, Update, Delete)

Swagger UI for API documentation and testing

CORS support

#Getting Started

-Prerequisites

.NET 8 SDK

SQL Server

Installation

#Clone the repository:

git clone [https://github.com/yourusername/BankAPI.git](https://github.com/Collins0399/Bank-API.git)

#Usage

-The API provides the following endpoints:

1.Bank Endpoints

GET /api/Bank - Get all banks

GET /api/Bank/{id} - Get a bank by ID

POST /api/Bank - Create a new bank

PUT /api/Bank/{id} - Update a bank

DELETE /api/Bank/{id} - Delete a bank

2.Bank Account Endpoints

GET /api/BankAccount - Get all bank accounts

GET /api/BankAccount/{id} - Get a bank account by ID

POST /api/BankAccount - Create a new bank account

PUT /api/BankAccount/{id} - Update a bank account

DELETE /api/BankAccount/{id} - Delete a bank account

#Swagger UI

You can access the Swagger UI for testing the API endpoints at https://localhost:7187/swagger.

#API Testing with Postman

To test the API endpoints using Postman, follow these steps:

a.Set Up Postman:

b.Download and install Postman from here.

c.Open Postman and create a new collection for your API tests.

d.Add Requests to the Collection:

For each endpoint, create a new request in Postman.

(i).Bank Endpoints

Get All Banks

Method: GET

URL: https://localhost:7187/api/Bank

Description: Retrieves a list of all banks.

Get Bank by ID

Method: GET

URL: https://localhost:7187/api/Bank/{id}

Description: Retrieves a bank by its ID.

Parameters:

id (path parameter): The ID of the bank to retrieve.

Create a New Bank

Method: POST

URL: https://localhost:7187/api/Bank

Description: Creates a new bank.

Body (JSON):

(ii).Bank Account Endpoints

Get All Bank Accounts

Method: GET

URL: https://localhost:7187/api/BankAccount

Description: Retrieves a list of all bank accounts.

Get Bank Account by ID

Method: GET

URL: https://localhost:7187/api/BankAccount/{id}

Description: Retrieves a bank account by its ID.

Parameters:

id (path parameter): The ID of the bank account to retrieve.

Create a New Bank Account

Method: POST

URL: https://localhost:7187/api/BankAccount

Description: Creates a new bank account.

Body (JSON):

Delete a Bank Account

Method: DELETE

URL: https://localhost:7187/api/BankAccount/{id}

Description: Deletes a bank account by its ID.

Parameters:

id (path parameter): The ID of the bank account to delete.

#Project Structure

Controllers - Contains the API controllers for handling HTTP requests.

Models - Contains the data models and the Entity Framework Core DB context.

Program.cs - The entry point of the application.

#Configuration

appsettings.json - Contains the configuration settings for the application.

appsettings.Development.json - Contains the development-specific configuration settings.

#Contributing

Contributions are welcome! Here’s how you can contribute:

Open an Issue: Report bugs, suggest features, or ask questions via GitHub Issues.

Submit a Pull Request: Fork the repository, make improvements, and submit a pull request for review.

