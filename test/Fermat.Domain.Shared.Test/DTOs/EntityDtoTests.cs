using Fermat.Domain.Shared.DTOs;
using Fermat.Domain.Shared.Interfaces;

namespace Fermat.Domain.Shared.Test.DTOs;

public class EntityDtoTests
{
    [Fact]
    public void EntityDto_CanBeInstantiated()
    {
        // Act
        var dto = new TestEntityDto();

        // Assert
        Assert.NotNull(dto);
        Assert.IsAssignableFrom<IEntityDto>(dto);
    }

    [Fact]
    public void EntityDtoTKey_CanBeInstantiated()
    {
        // Act
        var dto = new TestEntityDtoWithKey();

        // Assert
        Assert.NotNull(dto);
        Assert.IsAssignableFrom<IEntityDto<Guid>>(dto);
        Assert.Equal(default(Guid), dto.Id);
    }

    [Fact]
    public void EntityDtoTKey_IdCanBeSet()
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
    public void EntityDtoTKey_WithIntKey_WorksCorrectly()
    {
        // Arrange
        var id = 42;
        var dto = new TestEntityDtoWithIntKey();

        // Act
        dto.Id = id;

        // Assert
        Assert.Equal(id, dto.Id);
        Assert.IsAssignableFrom<IEntityDto<int>>(dto);
    }

    [Fact]
    public void EntityDtoTKey_WithStringKey_WorksCorrectly()
    {
        // Arrange
        var id = "test-id";
        var dto = new TestEntityDtoWithStringKey();

        // Act
        dto.Id = id;

        // Assert
        Assert.Equal(id, dto.Id);
        Assert.IsAssignableFrom<IEntityDto<string>>(dto);
    }

    [Fact]
    public void EntityDto_CanBeUsedForDataTransfer()
    {
        // Arrange
        var sourceId = Guid.NewGuid();
        var sourceName = "Test Product";
        var sourcePrice = 99.99m;

        // Act
        var dto = new ProductDto
        {
            Id = sourceId,
            Name = sourceName,
            Price = sourcePrice
        };

        // Assert
        Assert.Equal(sourceId, dto.Id);
        Assert.Equal(sourceName, dto.Name);
        Assert.Equal(sourcePrice, dto.Price);
    }

    [Fact]
    public void EntityDto_SupportsNullValues()
    {
        // Arrange
        var dto = new TestEntityDtoWithStringKey();

        // Act
        dto.Id = null;

        // Assert
        Assert.Null(dto.Id);
    }

    // Test DTO classes
    private class TestEntityDto : EntityDto
    {
    }

    private class TestEntityDtoWithKey : EntityDto<Guid>
    {
    }

    private class TestEntityDtoWithIntKey : EntityDto<int>
    {
    }

    private class TestEntityDtoWithStringKey : EntityDto<string>
    {
    }

    private class ProductDto : EntityDto<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}