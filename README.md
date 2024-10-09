The architecture consists of several key layers:

Core Layer: This layer contains the domain entities and Data Transfer Objects (DTOs). It represents the fundamental business logic and data structures used throughout the application.

Persistence Layer: This layer is responsible for managing the connection to the database and includes a generic repository pattern. It abstracts data access and provides methods for querying and manipulating the database.

Service Layer: This layer contains the implementation of business services. It interacts with both the Core and Persistence layers, providing a set of operations that can be called by the API layer. The service layer encapsulates the business logic and coordinates data operations, ensuring that all data handling occurs through the generic repository.

API Layer: This layer serves as the entry point for clients to interact with the application. It exposes endpoints that call the services defined in the service layer. The API layer is responsible for handling incoming requests and returning appropriate responses.

Data Handling
Within the service layer, the generic repository handles data operations, including soft deletion. When a record is deleted from the database, instead of being permanently removed, a flag called IsDeleted is set. This flag determines whether the record should be included in the results of GET API calls, allowing for a logical deletion without losing data.

Design Principles
The project adheres to SOLID principles, ensuring a well-structured and maintainable codebase. Each layer has a distinct responsibility, promoting separation of concerns and making the system more modular and testable.
