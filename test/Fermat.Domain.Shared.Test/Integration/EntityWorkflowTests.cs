using Fermat.Domain.Shared.Auditing;
using Fermat.Domain.Shared.DTOs;
using Fermat.Domain.Shared.Interfaces;

namespace Fermat.Domain.Shared.Test.Integration;

public class EntityWorkflowTests
{
    [Fact]
    public void EntityToDto_Mapping_WorksCorrectly()
    {
        // Arrange
        var entity = new TestProduct
        {
            Id = Guid.NewGuid(),
            Name = "Test Product",
            Price = 99.99m,
            CreationTime = DateTime.UtcNow,
            CreatorId = Guid.NewGuid(),
            LastModificationTime = DateTime.UtcNow.AddHours(1),
            LastModifierId = Guid.NewGuid()
        };

        // Act
        var dto = new TestProductDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Price = entity.Price,
            CreationTime = entity.CreationTime,
            CreatorId = entity.CreatorId,
            LastModificationTime = entity.LastModificationTime,
            LastModifierId = entity.LastModifierId
        };

        // Assert
        Assert.Equal(entity.Id, dto.Id);
        Assert.Equal(entity.Name, dto.Name);
        Assert.Equal(entity.Price, dto.Price);
        Assert.Equal(entity.CreationTime, dto.CreationTime);
        Assert.Equal(entity.CreatorId, dto.CreatorId);
        Assert.Equal(entity.LastModificationTime, dto.LastModificationTime);
        Assert.Equal(entity.LastModifierId, dto.LastModifierId);
    }

    [Fact]
    public void FullAuditedEntity_CompleteLifecycle_WorksCorrectly()
    {
        // Arrange
        var creatorId = Guid.NewGuid();
        var modifierId = Guid.NewGuid();
        var deleterId = Guid.NewGuid();
        var creationTime = DateTime.UtcNow;
        var modificationTime = creationTime.AddHours(1);
        var deletionTime = creationTime.AddHours(2);

        // Act - Create
        var entity = new TestFullAuditedProduct
        {
            Id = Guid.NewGuid(),
            Name = "Test Product",
            CreationTime = creationTime,
            CreatorId = creatorId
        };

        // Assert - Creation
        Assert.Equal(creatorId, entity.CreatorId);
        Assert.Equal(creationTime, entity.CreationTime);
        Assert.False(entity.IsDeleted);

        // Act - Modify
        entity.Name = "Modified Product";
        entity.LastModificationTime = modificationTime;
        entity.LastModifierId = modifierId;

        // Assert - Modification
        Assert.Equal("Modified Product", entity.Name);
        Assert.Equal(modificationTime, entity.LastModificationTime);
        Assert.Equal(modifierId, entity.LastModifierId);

        // Act - Soft Delete
        entity.IsDeleted = true;
        entity.DeletionTime = deletionTime;
        entity.DeleterId = deleterId;

        // Assert - Deletion
        Assert.True(entity.IsDeleted);
        Assert.Equal(deletionTime, entity.DeletionTime);
        Assert.Equal(deleterId, entity.DeleterId);
    }

    [Fact]
    public void EntityWithDifferentKeyTypes_WorkCorrectly()
    {
        // Arrange & Act
        var guidEntity = new TestEntityWithGuidKey(Guid.NewGuid());
        var intEntity = new TestEntityWithIntKey(42);
        var stringEntity = new TestEntityWithStringKey("test-id");

        // Assert
        Assert.IsType<Guid>(guidEntity.Id);
        Assert.IsType<int>(intEntity.Id);
        Assert.IsType<string>(stringEntity.Id);
    }

    [Fact]
    public void DtoWithDifferentKeyTypes_WorkCorrectly()
    {
        // Arrange & Act
        var guidDto = new TestDtoWithGuidKey { Id = Guid.NewGuid() };
        var intDto = new TestDtoWithIntKey { Id = 42 };
        var stringDto = new TestDtoWithStringKey { Id = "test-id" };

        // Assert
        Assert.IsType<Guid>(guidDto.Id);
        Assert.IsType<int>(intDto.Id);
        Assert.IsType<string>(stringDto.Id);
    }

    [Fact]
    public void AuditedEntity_Serialization_WorksCorrectly()
    {
        // Arrange
        var entity = new TestAuditedProduct
        {
            Id = Guid.NewGuid(),
            Name = "Test Product",
            CreationTime = DateTime.UtcNow,
            CreatorId = Guid.NewGuid()
        };

        // Act & Assert
        Assert.True(entity is IAuditedObject);
        Assert.True(entity is IEntity<Guid>);
    }

    [Fact]
    public void Entity_InterfaceCompliance_WorksCorrectly()
    {
        // Arrange
        var entity = new TestProduct();

        // Act & Assert
        Assert.True(entity is IEntity);
        Assert.True(entity is IEntity<Guid>);
        Assert.True(entity is IAuditedObject);
        Assert.True(entity is ICreationAuditedObject);
        Assert.True(entity is IModificationAuditedObject);
        Assert.True(entity is IHasCreationTime);
        Assert.True(entity is IHasModificationTime);
        Assert.True(entity is IMayHaveCreator);
    }

    [Fact]
    public void Dto_InterfaceCompliance_WorksCorrectly()
    {
        // Arrange
        var dto = new TestProductDto();

        // Act & Assert
        Assert.True(dto is IEntityDto);
        Assert.True(dto is IEntityDto<Guid>);
        Assert.True(dto is IAuditedObject);
        Assert.True(dto is ICreationAuditedObject);
        Assert.True(dto is IModificationAuditedObject);
    }

    [Fact]
    public void Entity_DefaultValues_AreSetCorrectly()
    {
        // Act
        var entity = new TestProduct();

        // Assert
        Assert.Equal(default(Guid), entity.Id);
        Assert.Equal(default(DateTime), entity.CreationTime);
        Assert.Null(entity.LastModificationTime);
        Assert.Null(entity.CreatorId);
        Assert.Null(entity.LastModifierId);
    }

    [Fact]
    public void Dto_DefaultValues_AreSetCorrectly()
    {
        // Act
        var dto = new TestProductDto();

        // Assert
        Assert.Equal(default(Guid), dto.Id);
        Assert.Equal(default(DateTime), dto.CreationTime);
        Assert.Null(dto.LastModificationTime);
        Assert.Null(dto.CreatorId);
        Assert.Null(dto.LastModifierId);
    }

    [Fact]
    public void FullAuditedEntity_DefaultValues_AreSetCorrectly()
    {
        // Act
        var entity = new TestFullAuditedProduct();

        // Assert
        Assert.Equal(default(Guid), entity.Id);
        Assert.Equal(default(DateTime), entity.CreationTime);
        Assert.Null(entity.LastModificationTime);
        Assert.Null(entity.DeletionTime);
        Assert.Null(entity.CreatorId);
        Assert.Null(entity.LastModifierId);
        Assert.Null(entity.DeleterId);
        Assert.False(entity.IsDeleted);
    }

    // Test entity classes
    private class TestProduct : AuditedEntity<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }

    private class TestAuditedProduct : AuditedEntity<Guid>
    {
        public string Name { get; set; } = string.Empty;
    }

    private class TestFullAuditedProduct : FullAuditedEntity<Guid>
    {
        public string Name { get; set; } = string.Empty;
    }

    private class TestEntityWithGuidKey : Entity<Guid>
    {
        public TestEntityWithGuidKey(Guid id) : base(id) { }
    }

    private class TestEntityWithIntKey : Entity<int>
    {
        public TestEntityWithIntKey(int id) : base(id) { }
    }

    private class TestEntityWithStringKey : Entity<string>
    {
        public TestEntityWithStringKey(string id) : base(id) { }
    }

    private class TestProductDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }

    private class TestDtoWithGuidKey : EntityDto<Guid>
    {
    }

    private class TestDtoWithIntKey : EntityDto<int>
    {
    }

    private class TestDtoWithStringKey : EntityDto<string>
    {
    }
}