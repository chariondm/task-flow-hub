<a name="design-doc-top"></a>

# Design Document: Task Flow Hub

## Summary

The Task Flow Hub is a web application that allows companies to manage their tasks. The application provides a secure authentication system, allowing users to register and log in to the system. Once logged in, users can create, edit, list, and view their tasks. The application also provides an admin view that allows administrators to list all registered users.

<details>
    <summary>Table of Contents</summary>
    <ul>
        <li><a href="#design-doc-top">Design Document: Task Flow Hub</a></li>
        <li><a href="#summary">Summary</a></li>
        <li><a href="#technology-stack">Technology Stack</a></li>
        <li><a href="#architecture">Architecture</a></li>
        <li><a href="#database-schema-and-data-dictionary">Database Schema and Data Dictionary</a></li>
        <li>
            <a href="#api-endpoints">API Endpoints</a>
            <ul>
                <li><a href="#register-user">Register User</a></li>
                <li><a href="#list-users">List Users</a></li>
                <li><a href="#login">Login</a></li>
                <li><a href="#create-task">Create Task</a></li>
                <li><a href="#edit-task">Edit Task</a></li>
                <li><a href="#list-tasks">List Tasks</a></li>
                <li><a href="#view-task">View Task</a></li>
            </ul>
        </li>
        <li><a href="#conclusion">Conclusion</a></li>
    </ul>
</details>


## Technology Stack

<p align="right">(<a href="#design-doc-top">back to top</a>)</p>

- **Backend:** .NET
- **Database:** MySQL
- **ORM:** Dapper
- **Authentication:** JWT

## Architecture

<p align="right">(<a href="#design-doc-top">back to top</a>)</p>

The application follows a hexagonal architecture, with the following layers:

- **Adapters:**
    - **Inbound:** Contains the API controllers that handle the HTTP requests.
    - **Outbound:** Contains the database repositories that handle the data access.
- **Core:**
    - **Application:** Contains the use cases that implement the application's business logic.
    - **Domain:** Contains the entities and value objects that represent the application's domain.
- **Infrastructure:**
    - **Persistence:** Contains the database context and migrations.

## Database Schema and Data Dictionary

<p align="right">(<a href="#design-doc-top">back to top</a>)</p>

The application uses a MySQL database with the following tables:

### Users Table

| Column Name | Data Type | Description                     |
|-------------|-----------|---------------------------------|
| UserId      | UUID      | Unique identifier for the user  |
| Username    | VARCHAR   | User's chosen name              |
| Email       | VARCHAR   | User's email address            |
| Password    | VARCHAR   | Hashed password                 |
| IsAdmin     | BOOLEAN   | Flag for admin                  |
| CreatedAt   | DATETIME  | User creation date              |
| UpdatedAt   | DATETIME  | User last update date           |

### Tasks Table

| Column Name | Data Type | Description                     |
|-------------|-----------|---------------------------------|
| TaskId      | UUID      | Unique identifier for the task  |
| UserId      | UUID      | User ID associated with the task |
| Title       | VARCHAR   | Task title                      |
| Description | TEXT      | Task description                |
| Status      | VARCHAR   | Task status pending, completed, canceled, in_progress |
| CreatedAt  | DATETIME  | Task creation date              |
| UpdatedAt   | DATETIME  | Task last update date           |

## API Endpoints

<p align="right">(<a href="#design-doc-top">back to top</a>)</p>

The application provides the following API endpoints:

### Register User

- **URL:** `/api/users`
- **Method:** `POST`
- **Request Body:**
  ```json
  {
    "username": "string",
    "email": "string",
    "password": "string"
  }
  ```

- **Response:**
    - **Status Code:** `201 Created`
    - **Body:**
        ```json
        {
            "success": true,
            "message": "User registered successfully",
            "data": {
                "id": "00000000-0000-0000-0000-000000000000"
            }
        }
        ```
    - **Status Code:** `400 Bad Request`
    - **Body:**
        ```json
        {
            "success": false,
            "details": "Invalid request body",
            "errors": [
                {
                    "field": "string",
                    "message": "string"
                }
            ]
        }
        ```
    - **Status Code:** `409 Conflict`
    - **Body:**
        ```json
        {
            "success": false,
            "details": "User already exists",
            "errors": []
        }
        ```
    - **Status Code:** `500 Internal Server Error`
    - **Body:**
        ```json
        {
            "success": false,
            "details": "Internal server error see logs for more details",
            "errors": []
        }
        ```

### List Users

- **URL:** `/api/users`
- **Method:** `GET`
- **Headers:**
    - `Authorization: Bearer {token}`
- **Response:**
    - **Status Code:** `200 OK`
    - **Body:**
        ```json
        {
            "success": true,
            "message": "Users listed successfully",
            "data": [
                {
                    "id": "00000000-0000-0000-0000-000000000000",
                    "username": "string",
                    "email": "string"
                }
            ]
        }
        ```
    - **Status Code:** `403 Forbidden`
    - **Body:**
        ```json
        {
            "success": false,
            "details": "Unauthorized access",
            "errors": []
        }
        ```
    - **Status Code:** `500 Internal Server Error`
    - **Body:**
        ```json
        {
            "success": false,
            "details": "Internal server error see logs for more details",
            "errors": []
        }
        ```

### Login

- **URL:** `/api/auth/login`
- **Method:** `POST`
- **Request Body:**
  ```json
  {
    "email": "string",
    "password": "string"
  }
  ```

- **Response:**
    - **Status Code:** `200 OK`
    - **Body:**
        ```json
        {
            "success": true,
            "message": "User authenticated successfully",
            "data": {
                "token": "string"
            }
        }
        ```
    - **Status Code:** `400 Bad Request`
    - **Body:**
        ```json
        {
            "success": false,
            "details": "Invalid request body",
            "errors": [
                {
                    "field": "string",
                    "message": "string"
                }
            ]
        }
        ```
    - **Status Code:** `401 Unauthorized`
    - **Body:**
        ```json
        {
            "success": false,
            "details": "Invalid credentials",
            "errors": []
        }
        ```
    - **Status Code:** `500 Internal Server Error`
    - **Body:**
        ```json
        {
            "success": false,
            "details": "Internal server error see logs for more details",
            "errors": []
        }
        ```

### Create Task

- **URL:** `/api/tasks`
- **Method:** `POST`
- **Headers:**
    - `Authorization: Bearer {token}`
- **Request Body:**
  ```json
  {
    "title": "string",
    "description": "string",
    "status": "string"
  }
  ```
- **Response:**
    - **Status Code:** `201 Created`
    - **Body:**
        ```json
        {
            "success": true,
            "message": "Task created successfully",
            "data": {
                "id": "00000000-0000-0000-0000-000000000000"
            }
        }
        ```
    - **Status Code:** `400 Bad Request`
    - **Body:**
        ```json
        {
            "success": false,
            "details": "Invalid request body",
            "errors": [
                {
                    "field": "string",
                    "message": "string"
                }
            ]
        }
        ```
    - **Status Code:** `401 Unauthorized`
    - **Body:**
        ```json
        {
            "success": false,
            "details": "Unauthorized access",
            "errors": []
        }
        ```
    - **Status Code:** `500 Internal Server Error`
    - **Body:**
        ```json
        {
            "success": false,
            "details": "Internal server error see logs for more details",
            "errors": []
        }
        ```

### Edit Task

- **URL:** `/api/tasks/{id}`
    - **URL Parameters:**
        - `id` (uuid): The task's ID
- **Method:** `PUT`
- **Request Body:**
  ```json
  {
    "title": "string",
    "description": "string",
    "status": "string",
  }
  ```
- **Response:**
    - **Status Code:** `200 OK`
    - **Body:**
        ```json
        {
            "success": true,
            "message": "Task updated successfully",
            "data": {
                "id": "00000000-0000-0000-0000-000000000000"
            }
        }
        ```
    - **Status Code:** `400 Bad Request`
    - **Body:**
        ```json
        {
            "success": false,
            "details": "Invalid request body",
            "errors": [
                {
                    "field": "string",
                    "message": "string"
                }
            ]
        }
        ```
    - **Status Code:** `404 Not Found`
    - **Body:**
        ```json
        {
            "success": false,
            "details": "Task not found",
            "errors": []
        }
        ```
    - **Status Code:** `500 Internal Server Error`
    - **Body:**
        ```json
        {
            "success": false,
            "details": "Internal server error see logs for more details",
            "errors": []
        }
        ```

### List Tasks

- **URL:** `/api/tasks`
- **Method:** `GET`
- **Response:**
    - **Status Code:** `200 OK`
    - **Body:**
        ```json
        {
            "success": true,
            "message": "Tasks listed successfully",
            "data": [
                {
                    "id": "00000000-0000-0000-0000-000000000000",
                    "title": "string",
                    "description": "string",
                    "status": "string",
                    "creationDate": "string"
                }
            ]
        }
        ```
    - **Status Code:** `500 Internal Server Error`
    - **Body:**
        ```json
        {
            "success": false,
            "details": "Internal server error see logs for more details",
            "errors": []
        }
        ```

### View Task

- **URL:** `/api/tasks/{id}`
    - **URL Parameters:**
        - `id` (uuid): The task's ID
- **Method:** `GET`
- **Response:**
    - **Status Code:** `200 OK`
    - **Body:**
        ```json
        {
            "success": true,
            "message": "Task retrieved successfully",
            "data": {
                "id": "00000000-0000-0000-0000-000000000000",
                "title": "string",
                "description": "string",
                "status": "string",
                "creationDate": "string"
            }
        }
        ```
    - **Status Code:** `404 Not Found`
    - **Body:**
        ```json
        {
            "success": false,
            "details": "Task not found",
            "errors": []
        }
        ```
    - **Status Code:** `500 Internal Server Error`
    - **Body:**
        ```json
        {
            "success": false,
            "details": "Internal server error see logs for more details",
            "errors": []
        }
        ```

## Conclusion

<p align="right">(<a href="#design-doc-top">back to top</a>)</p>

The Task Flow Hub is a web application that provides companies with a secure and efficient way to manage their tasks. The application's API endpoints allow users to register, log in, create, edit, list, and view tasks. The application also provides an admin view that allows administrators to list all registered users. The application follows a hexagonal architecture, with separate layers for adapters, core, and infrastructure. The database schema includes tables for users and tasks, with the necessary columns to store the required data. The API endpoints provide detailed information on the request and response bodies, status codes, and possible error messages. The Task Flow Hub is a robust and user-friendly application that meets the needs of companies looking to streamline their task management processes.
