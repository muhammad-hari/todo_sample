# What is Todoist ? 
This project is a simple application to create a Todo List Application, demonstrating a layered application architecture with DDD best practices. Implements for some NLayer **Hexagonal architecture** (Application, Infrastructure and Presentation Layers) and **Domain Driven Design** (Entities, Repositories, Domain/Application Services and so on) 
and aimed to be a **Clean Architecture**, with applying **SOLID principles**. 

## Note:
It should be noted that what I am implementing here is only a demonstration that does not reach all business logic, I only provide a few simple implementations and I am very happy if later when joining I can provide the experience I have for actual cases.

## Layered Architecture

![DDD_png_pure]([https://user-images.githubusercontent.com/1147445/54773098-e1efe700-4c19-11e9-9150-74f7e770de42.png](https://jasontaylor.dev/wp-content/uploads/2020/01/Figure-01-2.png))

### Structure of Project
Repository include layers divided by **4 project**;
* API
  * The API layer is the part of an Application that is responsible for Programming everything it needs to send and receive data via an Interface (APIs).
* Domain
  * It’s responsible for implementing the application’s business logic and contains the entities and use cases. The Domain Layer is independent of the presentation and infrastructure layers and should only contain business logic that’s specific to the application.  
* Application
  * The Application Layer is responsible for implementing the application’s use cases. It acts as an intermediary between the presentation and domain layers and is responsible for executing the use cases by calling the appropriate domain layer methods.
* Infrastructure
  * The Infrastructure Layer is responsible for implementing the application’s infrastructure, such as databases, external APIs, and file systems. It’s the layer that interacts with external systems and provides a way for the application to persist data.
* Presentation Layer
  * The Presentation Layer is responsible for handling the user interface and presentation logic. It’s the layer that interacts with the user and provides a way for them to interact with the application.


## Technologies
* NET Core 6.0 (BackEnd)
* Angular (FrontEnd)
* Entity Framework Core (ORM) 
* Message Brocker (RabbitMQ)
* etc

## Architecture
* Clean Architecture
* CQRS
* SOLID and Clean Code
* Domain Driven Design (Layers and Domain Model Pattern)




