## Description

**CachingWebApi** is a web API that provides a caching mechanism using **in-memory caching** and **Redis**. This API offers a simple, fast, and reliable caching system that can be used for storing and retrieving data with various cache management operations such as **adding**, **getting**, **removing**, and **checking existence** of cache entries. The API supports both **synchronous** and **asynchronous** caching operations, depending on your requirements.

### Key Features:
- **In-Memory Caching**: Simple and fast caching using the .NET in-memory cache.
- **Redis Caching**: Integration with Redis for distributed cache management.
- **Add, Get, Remove Cache**: Operations to manage cache entries.
- **Cache Existence Check**: Easy way to check if a key exists in the cache.
- **Async and Sync Support**: Both synchronous and asynchronous methods for caching operations.
## Technologies Used

- **.NET 8** for building the Web API.
- **ASP.NET Core** for the web framework.
- **Redis** for distributed caching.
- **MemoryCache** for in-memory caching.
- **Swagger UI** for interactive API documentation.

## Screenshots
- **Swagger UI**: 
  ![Swagger UI]( https://github.com/user-attachments/assets/c467d841-f868-40ed-a12a-d730821d087f)
)
## Installation

### Step-by-Step Installation:

1. Clone the repository:
   ```bash
   git clone https://github.com/3slam/CachingWebApi.git
   cd CachingWebApi
   ```

2. Restore the project dependencies:
   ```bash
   dotnet restore
   ```

3. Build the project:
   ```bash
   dotnet build
   ```

4. Run the application:
   ```bash
   dotnet run
   ```

5. The API will be available at `https://localhost:5001` (or the port specified in your `appsettings.json`).

> **Note**: If using Redis caching, ensure that Redis is running locally or provide the connection string in the `appsettings.json` file under `Redis:ConnectionString`.

## Setup

### Configuration:

In the `appsettings.json` file, configure your Redis connection string if you're using Redis as your caching provider:

```json
{
  "Redis": {
    "ConnectionString": "your-redis-connection-string"
  }
}
```

Make sure the **Redis server** is up and running, or if you're using **in-memory caching**, you don't need any external configuration.

## API Endpoints

### 1. **Add to Cache**

- **Endpoint**: `POST /api/cache/add`
- **Description**: Adds a key-value pair to the cache.
- **Request**:
  - **Query**: `key` (string)
  - **Body**: `value` (string)

- **Response**:
  - Success: `200 OK` with message `Value added successfully.`
  - Error: `400 BadRequest` with an error message.

### 2. **Get from Cache**

- **Endpoint**: `GET /api/cache/get/{key}`
- **Description**: Retrieves a value from the cache by its key.
- **Response**:
  - Success: `200 OK` with the cached value.
  - Not Found: `404 Not Found` if the key does not exist.

### 3. **Remove from Cache**

- **Endpoint**: `DELETE /api/cache/remove/{key}`
- **Description**: Removes a key-value pair from the cache.
- **Response**:
  - Success: `200 OK` with message `Value removed successfully.`
  - Error: `400 BadRequest` with an error message.

### 4. **Check Cache Existence**

- **Endpoint**: `GET /api/cache/exists/{key}`
- **Description**: Checks if a key exists in the cache.
- **Response**:
  - Success: `200 OK` with `KeyExists: true` or `false`.

## Contributing

We welcome contributions to improve the project! If you'd like to contribute:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature/your-feature`).
3. Make your changes and commit (`git commit -am 'Add new feature'`).
4. Push to your branch (`git push origin feature/your-feature`).
5. Create a pull request.




 
