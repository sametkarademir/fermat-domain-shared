using Fermat.Domain.Shared.Interfaces;

namespace Fermat.Domain.Shared.Test.Interfaces;

public class EntityInterfaceTests
{
    [Fact]
    public void IEntity_CanBeImplemented()
    {
        // Act
        var entity = new TestEntity();

        // Assert
        Assert.NotNull(entity);
        Assert.IsAssignableFrom<IEntity>(entity);
    }

    [Fact]
    public void IEntityTKey_CanBeImplemented()
    {
        // Act
        var entity = new TestEntityWithKey();

        // Assert
        Assert.NotNull(entity);
        Assert.IsAssignableFrom<IEntity<Guid>>(entity);
    }

    [Fact]
    public void IEntityTKey_IdPropertyWorks()
    {
        // Arrange
        var entity = new TestEntityWithKey();
        var id = Guid.NewGuid();

        // Act
        entity.Id = id;

        // Assert
        Assert.Equal(id, entity.Id);
    }

    [Fact]
    public void IEntityDto_CanBeImplemented()
    {
        // Act
        var dto = new TestEntityDto();

        // Assert
        Assert.NotNull(dto);
        Assert.IsAssignableFrom<IEntityDto>(dto);
    }

    [Fact]
    public void IEntityDtoTKey_CanBeImplemented()
    {
        // Act
        var dto = new TestEntityDtoWithKey();

        // Assert
        Assert.NotNull(dto);
        Assert.IsAssignableFrom<IEntityDto<Guid>>(dto);
    }

    [Fact]
    public void IEntityDtoTKey_IdPropertyWorks()
    {
        // Arrange
        var dto = new TestEntityDtoWithKey();
        var id = Guid.NewGuid();

        // Act
        dto.Id = id;

        // Assert
        Assert.Equal(id, dto.Id);
    }

    [Fact]
    public void IHasCreationTime_CanBeImplemented()
    {
        // Arrange
        var entity = new TestEntityWithCreationTime();
        var creationTime = DateTime.UtcNow;

        // Act
        entity.CreationTime = creationTime;

        // Assert
        Assert.Equal(creationTime, entity.CreationTime);
        Assert.IsAssignableFrom<IHasCreationTime>(entity);
    }

    [Fact]
    public void IHasModificationTime_CanBeImplemented()
    {
        // Arrange
        var entity = new TestEntityWithModificationTime();
        var modificationTime = DateTime.UtcNow;

        // Act
        entity.LastModificationTime = modificationTime;

        // Assert
        Assert.Equal(modificationTime, entity.LastModificationTime);
        Assert.IsAssignableFrom<IHasModificationTime>(entity);
    }

    [Fact]
    public void IHasDeletionTime_CanBeImplemented()
    {
        // Arrange
        var entity = new TestEntityWithDeletionTime();
        var deletionTime = DateTime.UtcNow;

        // Act
        entity.DeletionTime = deletionTime;

        // Assert
        Assert.Equal(deletionTime, entity.DeletionTime);
        Assert.IsAssignableFrom<IHasDeletionTime>(entity);
    }

    [Fact]
    public void IMayHaveCreator_CanBeImplemented()
    {
        // Arrange
        var entity = new TestEntityWithCreator();
        var creatorId = Guid.NewGuid();

        // Act
        entity.CreatorId = creatorId;

        // Assert
        Assert.Equal(creatorId, entity.CreatorId);
        Assert.IsAssignableFrom<IMayHaveCreator>(entity);
    }

    [Fact]
    public void IMayHaveCreator_CanBeNull()
    {
        // Arrange
        var entity = new TestEntityWithCreator
        {
            // Act
            CreatorId = null
        };

        // Assert
        Assert.Null(entity.CreatorId);
    }

    [Fact]
    public void ISoftDelete_CanBeImplemented()
    {
        // Arrange
        var entity = new TestSoftDeleteEntity
        {
            // Act
            IsDeleted = true
        };

        // Assert
        Assert.True(entity.IsDeleted);
        Assert.IsAssignableFrom<ISoftDelete>(entity);
    }

    [Fact]
    public void ISoftDelete_DefaultValueIsFalse()
    {
        // Act
        var entity = new TestSoftDeleteEntity();

        // Assert
        Assert.False(entity.IsDeleted);
    }

    [Fact]
    public void IHasConcurrencyStamp_CanBeImplemented()
    {
        // Arrange
        var entity = new TestEntityWithConcurrencyStamp();
        var stamp = "test-stamp";

        // Act
        entity.ConcurrencyStamp = stamp;

        // Assert
        Assert.Equal(stamp, entity.ConcurrencyStamp);
        Assert.IsAssignableFrom<IHasConcurrencyStamp>(entity);
    }

    [Fact]
    public void IEntityCorrelationId_CanBeImplemented()
    {
        // Arrange
        var entity = new TestEntityWithCorrelationId();
        var correlationId = Guid.NewGuid();

        // Act
        entity.CorrelationId = correlationId;

        // Assert
        Assert.Equal(correlationId, entity.CorrelationId);
        Assert.IsAssignableFrom<IEntityCorrelationId>(entity);
    }

    [Fact]
    public void IEntitySessionId_CanBeImplemented()
    {
        // Arrange
        var entity = new TestEntityWithSessionId();
        var sessionId = Guid.NewGuid();

        // Act
        entity.SessionId = sessionId;

        // Assert
        Assert.Equal(sessionId, entity.SessionId);
        Assert.IsAssignableFrom<IEntitySessionId>(entity);
    }

    [Fact]
    public void IEntitySnapshotId_CanBeImplemented()
    {
        // Arrange
        var entity = new TestEntityWithSnapshotId();
        var snapshotId = Guid.NewGuid();

        // Act
        entity.SnapshotId = snapshotId;

        // Assert
        Assert.Equal(snapshotId, entity.SnapshotId);
        Assert.IsAssignableFrom<IEntitySnapshotId>(entity);
    }

    [Fact]
    public void IAuditedObject_CanBeImplemented()
    {
        // Act
        var entity = new TestAuditedObject();

        // Assert
        Assert.NotNull(entity);
        Assert.IsAssignableFrom<IAuditedObject>(entity);
    }

    [Fact]
    public void ICreationAuditedObject_CanBeImplemented()
    {
        // Act
        var entity = new TestCreationAuditedObject();

        // Assert
        Assert.NotNull(entity);
        Assert.IsAssignableFrom<ICreationAuditedObject>(entity);
    }

    [Fact]
    public void IModificationAuditedObject_CanBeImplemented()
    {
        // Act
        var entity = new TestModificationAuditedObject();

        // Assert
        Assert.NotNull(entity);
        Assert.IsAssignableFrom<IModificationAuditedObject>(entity);
    }

    [Fact]
    public void IDeletionAuditedObject_CanBeImplemented()
    {
        // Act
        var entity = new TestDeletionAuditedObject();

        // Assert
        Assert.NotNull(entity);
        Assert.IsAssignableFrom<IDeletionAuditedObject>(entity);
    }

    [Fact]
    public void IFullAuditedObject_CanBeImplemented()
    {
        // Act
        var entity = new TestFullAuditedObject();

        // Assert
        Assert.NotNull(entity);
        Assert.IsAssignableFrom<IFullAuditedObject>(entity);
    }

    // Test implementation classes
    private class TestEntity : IEntity
    {
    }

    private class TestEntityWithKey : IEntity<Guid>
    {
        public Guid Id { get; set; }
    }

    private class TestEntityDto : IEntityDto
    {
    }

    private class TestEntityDtoWithKey : IEntityDto<Guid>
    {
        public Guid Id { get; set; }
    }

    private class TestEntityWithCreationTime : IHasCreationTime
    {
        public DateTime CreationTime { get; set; }
    }

    private class TestEntityWithModificationTime : IHasModificationTime
    {
        public DateTime? LastModificationTime { get; set; }
    }

    private class TestEntityWithDeletionTime : IHasDeletionTime
    {
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
    }

    private class TestEntityWithCreator : IMayHaveCreator
    {
        public Guid? CreatorId { get; set; }
    }

    private class TestSoftDeleteEntity : ISoftDelete
    {
        public bool IsDeleted { get; set; }
    }

    private class TestEntityWithConcurrencyStamp : IHasConcurrencyStamp
    {
        public string? ConcurrencyStamp { get; set; }
    }

    private class TestEntityWithCorrelationId : IEntityCorrelationId
    {
        public Guid? CorrelationId { get; set; }
    }

    private class TestEntityWithSessionId : IEntitySessionId
    {
        public Guid? SessionId { get; set; }
    }

    private class TestEntityWithSnapshotId : IEntitySnapshotId
    {
        public Guid? SnapshotId { get; set; }
    }

    private class TestAuditedObject : IAuditedObject
    {
        public DateTime CreationTime { get; set; }
        public Guid? CreatorId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public Guid? LastModifierId { get; set; }
    }

    private class TestCreationAuditedObject : ICreationAuditedObject
    {
        public DateTime CreationTime { get; set; }
        public Guid? CreatorId { get; set; }
    }

    private class TestModificationAuditedObject : IModificationAuditedObject
    {
        public DateTime? LastModificationTime { get; set; }
        public Guid? LastModifierId { get; set; }
    }

    private class TestDeletionAuditedObject : IDeletionAuditedObject
    {
        public DateTime? DeletionTime { get; set; }
        public Guid? DeleterId { get; set; }
        public bool IsDeleted { get; set; }
    }

    private class TestFullAuditedObject : IFullAuditedObject
    {
        public DateTime CreationTime { get; set; }
        public Guid? CreatorId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public Guid? LastModifierId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public Guid? DeleterId { get; set; }
        public bool IsDeleted { get; set; }
    }
}