<a name="readme-top"></a>

<detalis>
    <summary>Table of Contents</summary>
    <ul>
        <li><a href="#task-flow-hub">Task Flow Hub</a></li>
        <li><a href="#hexagonal-architecture-fundamentals">Hexagonal Architecture Fundamentals</a></li>
        <li><a href="#project-structure">Project Structure</a></li>
        <li><a href="#getting-started">Getting Started</a></li>
        <li><a href="#configuration">Configuration</a></li>
    </ul>
</detalis>

# Task Flow Hub

<p align="right">(<a href="#readme-top">back to top</a>)</p>

`Task Flow Hub` is a comprehensive system developed using .NET 8 and C# designed to manage the task flow of a company. It leverages the principles of Hexagonal and Clean Architecture to provide a robust and scalable solution for task management. This system includes functionalities such as creating new users, managing tasks, and handling user registrations and task assignments. The project is organized into different modules, including Core, Adapters, and Infrastructure, to ensure a clear separation of concerns and facilitate the development and maintenance of the system.

## Hexagonal Architecture Fundamentals

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<details>
    <summary>Click to expand!</summary>
The essence of Hexagonal Architecture lies in the clear separation between the core application logic and the interaction points with the external world. This separation is achieved through the use of:

- **Adapters**: Concrete implementations of the ports, adapted to specific means of communication or technologies. For example, an adapter might be responsible for receiving HTTP requests and converting them into calls to the application logic, or for publishing messages in a Kafka topic based on events within the application.

    - **Inbound Adapters**: Handle the communication between the external world and the application. They are responsible for receiving requests from external clients and translating them into calls to the application logic.

    - **Outbound Adapters**: Handle the communication between the application and the external world. They are responsible for executing external actions requested by the application logic, such as writing to a database or sending messages to a message broker.

- **Core**: Contains the essential business logic and domain models. This is the part of the application that encapsulates fundamental rules and operations, such as creating users, managing tasks, and handling task assignments.

    - **Domain Models**: Represent the core concepts of the application domain. They encapsulate the business rules and behaviors that govern the application's behavior.

    - **Application**: Contains the `use cases` and business logic of the application. It orchestrates the interactions between the domain models and the adapters, ensuring that the application's business rules are enforced.

- **Infrastructure**: Provides the necessary infrastructure for the application to run, including database configurations, such as migrations and entity mappings, and other infrastructure-related concerns.

    - **Database**: Contains the database configurations, such as migrations and entity mappings, needed to persist the application's data.

    - **Security**: Contains the security-related configurations, such as authentication and authorization mechanisms, needed to secure the application.

By organizing the application into these distinct modules, Hexagonal Architecture promotes a clear separation of concerns and a modular, flexible structure that facilitates the development and maintenance of the system.

### Benefits of Hexagonal Architecture

- **Modularity**: The clear separation of concerns between the core application logic and the adapters makes it easier to understand and maintain the system.

- **Flexibility**: The modular structure allows for easy integration of new adapters or changes to existing ones without affecting the core application logic.

- **Testability**: The separation of concerns makes it easier to write unit tests for the core application logic and the adapters, ensuring that the system behaves as expected.

- **Scalability**: The modular structure allows for the system to scale by adding new adapters or scaling the core application logic independently.

- **Maintainability**: The clear separation of concerns and modular structure make it easier to maintain and extend the system over time.

</details>

## Project Structure

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<details>
    <summary>Click to expand!</summary>

The project is organized into different modules, each serving a specific purpose within the system. The main modules are:

```
├── docker-setup
├── docs
│   └── design-docs
├── src
│   ├── Adapters
│   │   ├── Inbounds
│   │   │   └── TaskFlowHubHttpApi
│   │   └── Outbounds
│   │       └── MySqlDbAdapter
│   ├── Core
│   │   ├── Application
│   │   └── Domain
│   └── Infrastructure
│       ├── Database
│       │   └── MySqlDb
│       └── Security
└── test
    ├── IntegrationTests
    │   └── MySqlDbAdapterTests
    └── UnitTests
```

- **docker-setup**: Contains the Docker Compose configuration files for setting up the development environment.

- **docs**: Contains the documentation for the project, including design documents and other relevant information.

    - **design-docs**: Contains the design documents for the project, detailing the architecture, design decisions, and other relevant information.

- **src**: Contains the source code for the project, organized into different modules.

    - **Adapters**: Contains the concrete implementations of the ports, adapted to specific means of communication or technologies.

        - **Inbounds**: Contains the inbound adapters responsible for handling the communication between the external world and the application.

            - **TaskFlowHubHttpApi**: Handles the HTTP communication between the external clients and the application.

        - **Outbounds**: Contains the outbound adapters responsible for handling the communication between the application and the external world.

            - **MySqlDbAdapter**: Handles the communication with the MySQL database.

    - **Core**: Contains the essential business logic and domain models of the application.

        - **Application**: Contains the use cases and business logic of the application.

        - **Domain**: Contains the domain models that represent the core concepts of the application domain.

    - **Infrastructure**: Contains the infrastructure-related configurations and implementations.

        - **Database**:

            - **MySqlDb**: Contains the database configurations, such as migrations and entity mappings, needed to persist the application's data.

        - **Security**: Contains the security-related configurations, such as authentication and authorization mechanisms, needed to secure the application.

- **test**: Contains the test code for the project, organized into different test suites.
    
    - **IntegrationTests**: Contains the integration tests for the project.
    
        - **MySqlDbAdapterTests**: Contains the integration tests for the MySQL database adapter.
    
    - **UnitTests**: Contains the unit tests for the project.

</details>

## Getting Started

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<details>
    <summary>Click to expand!</summary>

To get started with the project, follow these steps:

1. **Clone the Repository**:

    ```bash
    git clone https://github.com/chariondm/task-flow-hub.git
    ```

2. **Navigate to the Project Directory**:

    ```bash
    cd task-flow-hub
    ```

3. **Set Up the Development Environment**:

    ```bash
    docker compose -f docker-setup/docker-compose.services.yml up -d
    ```

4. **Build the Project**:
    
    ```bash
    dotnet build
    ```

    :warning: **Note**: You may need to install the required .NET SDK version specified in the [global.json](global.json) file.

5. **Run the Project**:

    ```bash
    dotnet run --project src/Adapters/Inbounds/TaskFlowHubHttpApi/TaskFlowHubHttpApi.csproj
    ```

    :warning: **Note**: You may need to set the environment variables required by the application. Refer to the [Configuration](#configuration) section for more information.

6. **Access the Application**:

    - The application will be accessible at http://localhost:5126/swagger/index.html.

7. **Run the Tests**:

    ```bash
    dotnet test
    ```

    :warning: **Note**: You may need to set up the `Set Up the Development Environment` step before running the tests.

</details>

## Configuration

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<details>
    <summary>Click to expand!</summary>

The application can be configured using environment variables. The following environment variables are used by the application:

- **ConnectionStrings__DefaultConnection**: The connection string for the MySQL database.

- **JwtTokenSettings__Audience**: The audience for the JWT token.

- **JwtTokenSettings__Issuer**: The issuer for the JWT token.

- **JwtTokenSettings__Secret**: The secret key for the JWT token.

    - **Note**: The encryption algorithm used for the JWT token is `HS256` and the secret key requires a key size of at least `128` bits.

- **JwtTokenSettings__ExpirationInMinutes**: The expiration time for the JWT token in minutes.

### Example Configuration

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=3306;Database=task_flow_hub_db;User Id=task_flow_hub_user;password=changeme_user;"
  },
  "JwtTokenSettings": {
    "Audience": "https://localhost:5126",
    "Issuer": "https://localhost:5126",
    "Secret": "D92BC749BE52BDA162BBB28A7677D3A3",
    "ExpiryInMinutes": 60
  },
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

</details>
