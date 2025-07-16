# EShopMicroservices

Microservices with C# and Docker [Mehmet Ozkaya](https://github.com/mehmetozkaya)

## Repos

- [EShopMicroservices Repositorio original](https://github.com/mehmetozkaya/EShopMicroservices)
- [AspNetCore.Diagnostics.HealthChecks](https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks)

## Introduction

EShopMicroservices is a sample project demonstrating how to build microservices using ASP.NET Core and Docker. This project includes various microservices, each responsible for a specific domain, and demonstrates best practices for building, deploying, and managing microservices.

## Libraries

- [Carter](https://github.com/CarterCommunity/Carter) 
- [Mapster](https://github.com/MapsterMapper/Mapster)
- [MediatR](https://github.com/jbogard/MediatR)
- [Fluent Validation](https://docs.fluentvalidation.net/en/latest/aspnet.html)

## Prerequisites

- [Docker](https://www.docker.com/products/docker-desktop)
- [Docker Compose](https://docs.docker.com/compose/install/)
- [Visual Studio Code](https://code.visualstudio.com/)
- [Visual Studio Code Extension: C# for Visual Studio Code (powered by OmniSharp)](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)
- [Visual Studio Code Extension: Docker](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-docker)

## Installation

1. Clone the repository:
    ```bash
    git clone https://github.com/yrrodriguezb/EShopMicroservices.git
    cd EShopMicroservices/src
    ```

2. Create cert https
    ```bash
    dotnet dev-certs https -ep "%APPDATA%\ASP.NET\Https\catalog.api.pfx" -p "123456"
    dotnet dev-certs https -ep "%APPDATA%\ASP.NET\Https\basket.api.pfx" -p "123456"
    dotnet dev-certs https -ep "%APPDATA%\ASP.NET\Https\discount.grpc.pfx" -p "123456"

    dotnet dev-certs https --trust
    ```

2. Build and run the project with Docker Compose:
    ```bash
    docker-compose up
    # or
    docker-compose -f docker-compose-yml -f docker-compose.override.yml up -d
    ```

3. To run the project in the background:
    ```bash
    docker-compose up -d
    ```

4. To stop the project:
    ```bash
    docker-compose down
    ```

## Usage

### Docker 

1. Verify the status of the containers
    ```bash
    docker ps
    ```

2. Connect to the container
    ```bash
    docker exec -it catalogdb bash
    ```

3. Connect to the database
    ```bash
    psql -U postgres
    ```

4. Create a database
    ```bash
    CREATE DATABASE catalog;
    ```

5. List the databases
    ```bash
    \l
    ```

6. Show the tables
    ```bash
    \dt
    ```

7. Connect to the database
    ```bash
    \c CatalogDb
    ```

8.  Create a table
    ```bash
    CREATE TABLE items (id SERIAL PRIMARY KEY, name VARCHAR(100), price NUMERIC(10, 2));
    ```

### Request API 
1. Get Catalog
    ```bash
    curl -X GET https://localhost:5050/products
    ```

2. Get Catalog by Id
    ```bash
    curl -X GET https://localhost:5050/products/5334c996-8457-4cf0-815c-ed2b77c4ff61
    ```

3. Get Catalog by Category
    ```bash
    curl -X GET https://localhost:5050/products/category/Camera
    ```

4. PUT Catalog
    ```bash
    curl -X PUT https://localhost:5050/products/ -H "Content-Type: application/json" -d '{"id":"5334c996-8457-4cf0-815c-ed2b77c4ff61","name":"IPhone X","description":"This phone is the companys biggest change to its flagship smartphone in years. It includes a borderless.","price":1000,"category":["Smart Phone","Phone","Tecnology"],"imageFile":"product-1.png"}'
    ```
5. Delete Catalog
    ```bash
    curl -X DELETE https://localhost:5050/products/0192d5ed-9b5b-4785-8cf6-cb1e3c5b853f
    ```

6. Post Catalog
    ```bash
    curl -X POST https://localhost:5050/products -H "Content-Type: application/json" -d '{"Name":"Product B","Category":["C1"],"Description":"Description product A","ImageFile":"IMG","Price":134}'
    ```

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Topics

### Catalog Microservice

1. Introduction to Microservices
2. Building Microservices with ASP.NET Core
3. Building Microservices with Docker
4. Building blocks of Microservices
5. Fluent Validation
6. CQRS Pattern
7. Pipelines Behaviors, how to work with MediatR
8. Health Checks