using Fermat.Domain.Shared.Auditing;
using Fermat.Domain.Shared.Interfaces;

namespace Fermat.Domain.Shared.Test.Entities;

public class EntityTests
{
    [Fact]
    public void Entity_CanBeInstantiated()
    {
        // Act
        var entity = new TestEntity();

        // Assert
        Assert.NotNull(entity);
        Assert.IsAssignableFrom<IEntity>(entity);
    }

    [Fact]
    public void EntityTKey_CanBeInstantiatedWithDefaultConstructor()
    {
        // Act
        var entity = new TestEntityWithKey();

        // Assert
        Assert.NotNull(entity);
        Assert.IsAssignableFrom<IEntity<Guid>>(entity);
        Assert.Equal(default(Guid), entity.Id);
    }

    [Fact]
    public void EntityTKey_CanBeInstantiatedWithId()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var entity = new TestEntityWithKey(id);

        // Assert
        Assert.NotNull(entity);
        Assert.Equal(id, entity.Id);
    }

    [Fact]
    public void EntityTKey_IdCanBeSet()
    {
        // Arrange
        var entity = new TestEntityWithKey();
        var newId = Guid.NewGuid();

        // Act
        entity.Id = newId;

        // Assert
        Assert.Equal(newId, entity.Id);
    }

    [Fact]
    public void EntityTKey_WithIntKey_WorksCorrectly()
    {
        // Arrange
        var id = 42;

        // Act
        var entity = new TestEntityWithIntKey(id);

        // Assert
        Assert.Equal(id, entity.Id);
        Assert.IsAssignableFrom<IEntity<int>>(entity);
    }

    [Fact]
    public void EntityTKey_WithStringKey_WorksCorrectly()
    {
        // Arrange
        var id = "test-id";

        // Act
        var entity = new TestEntityWithStringKey(id);

        // Assert
        Assert.Equal(id, entity.Id);
        Assert.IsAssignableFrom<IEntity<string>>(entity);
    }

    // Test entity classes
    private class TestEntity : Entity
    {
    }

    private class TestEntityWithKey : Entity<Guid>
    {
        public TestEntityWithKey() : base() { }
        public TestEntityWithKey(Guid id) : base(id) { }
    }

    private class TestEntityWithIntKey : Entity<int>
    {
        public TestEntityWithIntKey(int id) : base(id) { }
    }

    private class TestEntityWithStringKey : Entity<string>
    {
        public TestEntityWithStringKey(string id) : base(id) { }
    }
}