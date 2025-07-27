using Fermat.Domain.Shared.Auditing;
using Fermat.Domain.Shared.Interfaces;

namespace Fermat.Domain.Shared.Test.Auditing;

public class AuditedEntityTests
{
    [Fact]
    public void AuditedEntity_CanBeInstantiated()
    {
        // Act
        var entity = new TestAuditedEntity();

        // Assert
        Assert.NotNull(entity);
        Assert.IsAssignableFrom<IAuditedObject>(entity);
    }

    [Fact]
    public void AuditedEntity_ImplementsAllAuditInterfaces()
    {
        // Act
        var entity = new TestAuditedEntity();

        // Assert
        Assert.IsAssignableFrom<IAuditedObject>(entity);
        Assert.IsAssignableFrom<ICreationAuditedObject>(entity);
        Assert.IsAssignableFrom<IModificationAuditedObject>(entity);
    }

    [Fact]
    public void AuditedEntity_DefaultValuesAreSet()
    {
        // Act
        var entity = new TestAuditedEntity();

        // Assert
        Assert.Equal(default(DateTime), entity.CreationTime);
        Assert.Null(entity.LastModificationTime);
        Assert.Null(entity.CreatorId);
        Assert.Null(entity.LastModifierId);
    }

    [Fact]
    public void AuditedEntity_PropertiesCanBeSet()
    {
        // Arrange
        var entity = new TestAuditedEntity();
        var creationTime = DateTime.UtcNow;
        var modificationTime = DateTime.UtcNow.AddHours(1);
        var creatorId = Guid.NewGuid();
        var modifierId = Guid.NewGuid();

        // Act
        entity.CreationTime = creationTime;
        entity.LastModificationTime = modificationTime;
        entity.CreatorId = creatorId;
        entity.LastModifierId = modifierId;

        // Assert
        Assert.Equal(creationTime, entity.CreationTime);
        Assert.Equal(modificationTime, entity.LastModificationTime);
        Assert.Equal(creatorId, entity.CreatorId);
        Assert.Equal(modifierId, entity.LastModifierId);
    }

    [Fact]
    public void CreationAuditedEntity_CanBeInstantiated()
    {
        // Act
        var entity = new TestCreationAuditedEntity();

        // Assert
        Assert.NotNull(entity);
        Assert.IsAssignableFrom<ICreationAuditedObject>(entity);
        Assert.IsAssignableFrom<IHasCreationTime>(entity);
        Assert.IsAssignableFrom<IMayHaveCreator>(entity);
    }

    [Fact]
    public void CreationAuditedEntity_DefaultValuesAreSet()
    {
        // Act
        var entity = new TestCreationAuditedEntity();

        // Assert
        Assert.Equal(default(DateTime), entity.CreationTime);
        Assert.Null(entity.CreatorId);
    }

    [Fact]
    public void FullAuditedEntity_CanBeInstantiated()
    {
        // Act
        var entity = new TestFullAuditedEntity();

        // Assert
        Assert.NotNull(entity);
        Assert.IsAssignableFrom<IFullAuditedObject>(entity);
    }

    [Fact]
    public void FullAuditedEntity_ImplementsAllAuditInterfaces()
    {
        // Act
        var entity = new TestFullAuditedEntity();

        // Assert
        Assert.IsAssignableFrom<IAuditedObject>(entity);
        Assert.IsAssignableFrom<ICreationAuditedObject>(entity);
        Assert.IsAssignableFrom<IModificationAuditedObject>(entity);
        Assert.IsAssignableFrom<IDeletionAuditedObject>(entity);
        Assert.IsAssignableFrom<IHasCreationTime>(entity);
        Assert.IsAssignableFrom<IHasModificationTime>(entity);
        Assert.IsAssignableFrom<IHasDeletionTime>(entity);
        Assert.IsAssignableFrom<IMayHaveCreator>(entity);
        Assert.IsAssignableFrom<ISoftDelete>(entity);
    }

    [Fact]
    public void FullAuditedEntity_DefaultValuesAreSet()
    {
        // Act
        var entity = new TestFullAuditedEntity();

        // Assert
        Assert.Equal(default(DateTime), entity.CreationTime);
        Assert.Null(entity.LastModificationTime);
        Assert.Null(entity.DeletionTime);
        Assert.Null(entity.CreatorId);
        Assert.Null(entity.LastModifierId);
        Assert.Null(entity.DeleterId);
        Assert.False(entity.IsDeleted);
    }

    [Fact]
    public void FullAuditedEntity_SoftDelete_WorksCorrectly()
    {
        // Arrange
        var entity = new TestFullAuditedEntity();
        var deletionTime = DateTime.UtcNow;
        var deleterId = Guid.NewGuid();

        // Act
        entity.IsDeleted = true;
        entity.DeletionTime = deletionTime;
        entity.DeleterId = deleterId;

        // Assert
        Assert.True(entity.IsDeleted);
        Assert.Equal(deletionTime, entity.DeletionTime);
        Assert.Equal(deleterId, entity.DeleterId);
    }

    [Fact]
    public void FullAuditedEntity_CanBeRestored()
    {
        // Arrange
        var entity = new TestFullAuditedEntity();
        entity.IsDeleted = true;
        entity.DeletionTime = DateTime.UtcNow;
        entity.DeleterId = Guid.NewGuid();

        // Act
        entity.IsDeleted = false;
        entity.DeletionTime = null;
        entity.DeleterId = null;

        // Assert
        Assert.False(entity.IsDeleted);
        Assert.Null(entity.DeletionTime);
        Assert.Null(entity.DeleterId);
    }

    [Fact]
    public void AuditedEntity_WithGuidKey_WorksCorrectly()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var entity = new TestAuditedEntityWithKey(id);

        // Assert
        Assert.Equal(id, entity.Id);
        Assert.IsAssignableFrom<IAuditedObject>(entity);
    }

    [Fact]
    public void AuditedEntity_WithIntKey_WorksCorrectly()
    {
        // Arrange
        var id = 42;

        // Act
        var entity = new TestAuditedEntityWithIntKey(id);

        // Assert
        Assert.Equal(id, entity.Id);
        Assert.IsAssignableFrom<IAuditedObject>(entity);
    }

    [Fact]
    public void AuditedEntity_CanTrackCreationAndModification()
    {
        // Arrange
        var entity = new TestAuditedEntity();
        var creationTime = DateTime.UtcNow;
        var modificationTime = creationTime.AddHours(1);
        var creatorId = Guid.NewGuid();
        var modifierId = Guid.NewGuid();

        // Act
        entity.CreationTime = creationTime;
        entity.LastModificationTime = modificationTime;
        entity.CreatorId = creatorId;
        entity.LastModifierId = modifierId;

        // Assert
        Assert.Equal(creationTime, entity.CreationTime);
        Assert.Equal(modificationTime, entity.LastModificationTime);
        Assert.Equal(creatorId, entity.CreatorId);
        Assert.Equal(modifierId, entity.LastModifierId);
    }

    // Test entity classes
    private class TestAuditedEntity : AuditedEntity
    {
    }

    private class TestAuditedEntityWithKey : AuditedEntity<Guid>
    {
        public TestAuditedEntityWithKey(Guid id) : base(id) { }
    }

    private class TestAuditedEntityWithIntKey : AuditedEntity<int>
    {
        public TestAuditedEntityWithIntKey(int id) : base(id) { }
    }

    private class TestCreationAuditedEntity : CreationAuditedEntity
    {
    }

    private class TestFullAuditedEntity : FullAuditedEntity
    {
    }
}