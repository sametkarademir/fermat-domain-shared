# Fermat.Domain.Shared

A comprehensive .NET 8.0 shared domain library that provides foundational components for building enterprise applications with auditing, authorization, and data transfer object patterns.

## 🚀 Features

### Core Domain Entities
- **Base Entity Classes**: Generic and non-generic base classes for domain entities
- **Auditing Support**: Full auditing capabilities with creation, modification, and deletion tracking
- **Soft Delete**: Built-in soft delete functionality
- **Concurrency Control**: Support for optimistic concurrency with concurrency stamps

### Data Transfer Objects (DTOs)
- **Base DTO Classes**: Generic and non-generic base classes for data transfer objects
- **Audited DTOs**: DTOs with full auditing information
- **Type Safety**: Strongly typed DTOs with generic key support

### Authorization & Security
- **Controller Authorization Conventions**: Dynamic authorization configuration for controllers
- **Endpoint-Level Security**: Granular control over endpoint access
- **Policy-Based Authorization**: Support for custom authorization policies
- **Role-Based Access Control**: Built-in role-based authorization

### API Management
- **API Disabling**: Ability to disable specific APIs or controllers
- **Processing Filters**: Custom filters for request processing
- **Controller Conventions**: Flexible controller configuration patterns

## 📦 Project Structure

```
src/Fermat.Domain.Shared/
├── Auditing/                    # Auditing base classes
│   ├── AuditedEntity.cs
│   ├── CreationAuditedEntity.cs
│   ├── Entity.cs
│   └── FullAuditedEntity.cs
├── Conventions/                 # Controller conventions
│   ├── ControllerAuthorizationConvention.cs
│   ├── ControllerDisablingConvention.cs
│   └── ControllerRemovalConvention.cs
├── DTOs/                       # Data Transfer Objects
│   ├── AuditedEntityDto.cs
│   ├── CreationAuditedEntityDto.cs
│   ├── EntityDto.cs
│   └── FullAuditedEntityDto.cs
├── Filters/                    # Custom filters
│   ├── DisableApiFilter.cs
│   └── ExcludeFromProcessingAttribute.cs
└── Interfaces/                 # Domain interfaces
    ├── IAuditedObject.cs
    ├── ICreationAuditedObject.cs
    ├── IDeletionAuditedObject.cs
    ├── IEntity.cs
    ├── IEntityCorrelationId.cs
    ├── IEntityDto.cs
    ├── IEntitySessionId.cs
    ├── IEntitySnapshotId.cs
    ├── IFullAuditedObject.cs
    ├── IHasConcurrencyStamp.cs
    ├── IHasCreationTime.cs
    ├── IHasDeletionTime.cs
    ├── IHasModificationTime.cs
    ├── IMayHaveCreator.cs
    ├── IModificationAuditedObject.cs
    └── ISoftDelete.cs
```

## 🛠️ Installation

### NuGet Package
```bash
  dotnet add package Fermat.Domain.Shared
```

### From Source
```bash
  git clone <repository-url>
  cd Fermat.Domain.Shared
  dotnet build
```

## 📖 Usage Examples

### Basic Entity Implementation
```csharp
public class Product : Entity<Guid>
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
```

### Audited Entity Implementation
```csharp
public class User : FullAuditedEntity<Guid>
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
```

### DTO Implementation
```csharp
public class ProductDto : EntityDto<Guid>
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
```

### Controller Authorization Convention
```csharp
var convention = new ControllerAuthorizationConvention(
    typeof(ProductController),
    "api/products",
    new AuthorizationOptions
    {
        RequireAuthentication = true,
        Roles = new List<string> { "Admin", "Manager" }
    },
    new List<EndpointOptions>
    {
        new EndpointOptions
        {
            ActionName = "Get",
            RequireAuthentication = false
        }
    }
);
```

### Disabling API Endpoints
```csharp
[DisableApiFilter]
public class DeprecatedController : ControllerBase
{
    // This controller will return 404 for all requests
}
```

## 🏗️ Architecture

### Domain-Driven Design
This library follows Domain-Driven Design principles:
- **Entities**: Core business objects with identity
- **Value Objects**: Immutable objects without identity
- **Aggregates**: Clusters of related entities
- **Repositories**: Data access abstractions

### Auditing Pattern
Comprehensive auditing support with:
- **Creation Auditing**: Tracks who created and when
- **Modification Auditing**: Tracks who modified and when
- **Deletion Auditing**: Tracks who deleted and when (soft delete)
- **Full Auditing**: Combines all audit capabilities

### Authorization Pattern
Flexible authorization system with:
- **Global Policies**: Controller-level authorization
- **Endpoint Policies**: Action-level authorization
- **Role-Based Access**: Traditional role-based security
- **Policy-Based Access**: Custom authorization policies

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'feat: Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 🆘 Support

For support and questions:
- Create an issue in the repository
- Contact the development team
- Check the documentation
