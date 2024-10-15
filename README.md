# Survivor

Creating a Web API application for the Survivor competition. This application will contain a relationship between contestants and categories and will contain API endpoints that perform CRUD (Create, Read, Update, Delete) operations related to these relationships.

Competitors Table:

![1](https://github.com/user-attachments/assets/ac568a89-2a3e-40a6-b400-bc463fa4e7b0)

Data is representative.

Category Table:

![2](https://github.com/user-attachments/assets/8f478b75-4123-4ed1-965f-d289276026e1)

Data is representative.

A Category can have more than one Competitor. This is a one-to-many relationship. Each Competitor will belong to only one Category.

A BaseEntity Class will be used to create the necessary Entities for the tables.

A CompetitorController and a CategoryController will be created. These controllers should be able to perform the following CRUD operations:

CompetitorController:

GET /api/competitors - List all contestants.

GET /api/competitors/{id} - Fetch a specific competitor.

GET /api/competitors/categories/{CategoryId} - Fetch competitors by Category Id.

POST /api/competitors - Create a new competitor.

PUT /api/competitors/{id} - Update a specific competitor.

DELETE /api/competitors/{id} - Delete a specific competitor.

CategoryController:

GET /api/categories - List all categories.

GET /api/categories/{id} - Fetch a specific category.

POST /api/categories - Create a new category.

PUT /api/categories/{id} - Update a specific category.

DELETE /api/categories/{id} - Delete a specific category

### Technologies used
- C#
- Asp.Net Core Api
- Entity Framework
