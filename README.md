# Orion

## 1. Architecture

For the **Manager** app architecture I'm using a **Clean Architecture** model, splitting the solution into: **Core**, **Infra**, **Service (Ports)** and **Shared Kernel** layers.

![alt text](src/Images/CleanArchitecture.jpg "Clean Architecture by Uncle Bob")
<br>
<br>

### Core
The **Core** layer is responsible for the **Domain Entities**, **Value Objects**, **Domain Services**, **Domain Events** and **Use Cases**.

The organization is based upon **Screaming Architecture** concept by Uncle Bob that purposes that you application architecture should scream what the system do. So I've prefer a organization by context rather than by component type.

Other principle that I've followed was **CQRS/CQS**, so the **Use Cases** are divided into **Write** or **Read** operations. Also any **Use Cases** has his owns **Command**, **Query**, **Result** and **Handler**, this last only for **orchestration concerns**.

The main focus is to keep the Domain isolated from external details.
<br>

![alt text](src/Images/ScreamingArchitecture.png "Screaming Architecture by Uncle Bob")
<br>
<br>

### Infra
The Infra Project is responsible for **Repository Concerns** (**Mappings**, **Migrations**, **Repository Implementations**, **Database Contexts** and **Unit of Work**), **Dependency Injection** and **External Providers** for integration or services consumption.

### Services
This are projects that consumes the **Core Use Cases**. Can be **APIs**, **gRPC Services**, **Serverless Functions**, **Console Applications** or **MVCs**. As the domain does not depends upon, the consumption can be anything.

### Shared Kernel
This are common resources that needs to be used between application layers, like **Application Settings**. This project is use to avoid circular dependencies between the other projects.

## 2. Dependencies Structure

![alt text](src/Images/Dependencies.png "Dependencies Structure")

## 3. Tests Structure

![alt text](src/Images/TestDependencies.png "Tests Dependencies Structure")

## 4. Serverless
The **Messenger** app was created to be an **Microservice** to send SMS, E-mail, and Push Notifications. The project is an **Azure Function**, triggered by an **RabbitMQ** queue/topic or a **gRPC** call.

## 5. Tests 
The Manager tests are focused on Domain. There are **Unit Tests** on **Entities** and **Value Objects** and **Integration Tests** on **Use Cases**. Note that are no network calls need on this tests, they are integration based on concept there they are testing the Use Case. 

The folder structure are similar to the structured used on Core.

![alt text](src/Images/TestStructureFolder.png "Tests Folder Structure")

## 6. Packages
There is some packages at SharedKernel to provide common solutions that can be packaged using nuget and installed at projects.

- _Asp.net_ - For **Endpoints** pattern implementation
- _Core_ - For **Domain Events**, **Value Object** and **Aggregate** patterns
- _DataContracts_ - For some common **Command**, **Query** and **Results**
- _DomainValidation_ - To use **Domain Validation** instead of throwing exception for non exceptional cases
- _OperationResult_ - To implement **Railway Oriented Programing** into methods using signature honesty
- _Repository_ - To implement **Specification**, **Unit of Work** and other repositories concepts
- _Resilience_ - To HTTP calls and other resources that may need **Retrying**, **Circuit Breaker** and **Timeout** policies

## 7. Used Patterns and Principles
- [Clean Architecture](https://blog.cleancoder.com/)
- [CQRS/CQRS](https://martinfowler.com/bliki/CQRS.html)
- [SOLID](https://blog.cleancoder.com/uncle-bob/2020/10/18/Solid-Relevance.html)
- [Unit of work](https://medium.com/@martinstm/unit-of-work-net-core-652f9b6cf894)
- [Endpoints Pattern](https://ardalis.com/mvc-controllers-are-dinosaurs-embrace-api-endpoints/)
- [Domain Validation](https://martinfowler.com/articles/replaceThrowWithNotification.html)
- [Domain Events](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/domain-events-design-implementation)
- [Aggregates](https://martinfowler.com/bliki/DDD_Aggregate.html)
- [Value Objects](https://martinfowler.com/bliki/ValueObject.html)
- [Fail-Fast Principle](https://enterprisecraftsmanship.com/posts/fail-fast-principle/)
- [Rich Domain Modeling](https://blog.codecentric.de/en/2019/10/ddd-vs-anemic-domain-models/)
- [Mediator](https://refactoring.guru/design-patterns/mediator)
- [Repository Pattern](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design)
- [AAA Test Pattern](https://medium.com/@pjbgf/title-testing-code-ocd-and-the-aaa-pattern-df453975ab80)

## 8. Next Steps
1. Containerization with **Docker**, **Docker Compose** and **Kubernetes**
2. More **Microservices** to integrations exploration
3. **Open Tracing** with **Jaeger**
4. **GraphQL** service example
5. **CI/CD** pipeline with **Github Actions**
6. **API Management Gateway** with **Kong**
7. **Authentication**/**Authorization** Service
8. Use more design patterns like **Rules Builder**, **Decorator** and more
9. Add an app implemented using **Vertical Slices Architecture** to the environment
10. K6 to Load/Stress Tests