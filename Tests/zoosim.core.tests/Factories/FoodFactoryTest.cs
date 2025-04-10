using NSubstitute;
using NUnit.Framework;
using zoosim.core.Factories;
using zoosim.core.Utils;

namespace zoosim.core.tests.Factories;

[TestFixture]
public class FoodFactoryTest
{
    private IDice _dice;
    private FoodFactory _sut;

    [SetUp]
    public void Setup()
    {
        _dice = Substitute.For<IDice>();
        _sut = new FoodFactory(_dice);
    }

    [Test]
    public void GetFood_CreatesFoodWithValuesWithin10to26()
    {
        // Arrange
        _dice.RollForHeal().Returns(15);

        // Act
        var food = _sut.GetFood(1);

        // Assert
        _dice.Received(1).RollForHeal();
        Assert.That(food[0].HealingValue, Is.EqualTo(15));
    }

    [Test]
    public void GetFood_ShouldReturnCorrectNumberOfFoodItems()
    {
        // Arrange
        var numofFood = 5;

        // Act
        var foods = _sut.GetFood(numofFood);

        // Assert
        Assert.That(foods.Length, Is.EqualTo(numofFood));
    }
}
