using NSubstitute;
using NUnit.Framework;
using zoosim.core.Engines;
using zoosim.core.Enums;
using zoosim.core.Factories;
using zoosim.core.Managers;
using zoosim.core.Models;
using zoosim.core.Systems;

namespace zoosim.core.tests.Engines;

[TestFixture]
public class ZooEngineTest
{
    private IAnimalManager _animalManager;
    private IFoodFactory _foodFactory;
    private ISystem _fatigueSystem;
    private ISystem _attackSystem;
    private IClock _clock;
    private ZooEngine _sut;

    [SetUp]
    public void SetUp()
    {
        _animalManager = Substitute.For<IAnimalManager>();
        _foodFactory = Substitute.For<IFoodFactory>();
        _fatigueSystem = Substitute.For<ISystem>();
        _attackSystem = Substitute.For<ISystem>();
        _clock = Substitute.For<IClock>();
        _clock.CurrentTime.Returns(DateTime.Now);


        _sut = new ZooEngine(_animalManager,
            _foodFactory,
            _clock,
            [_fatigueSystem, _attackSystem]);
    }

    [Test]
    public void FeedAnimals_CallsFactoryAndEats()
    {
        // Arrange
        var animal = Substitute.For<IAnimal>();
        _animalManager.GetAnimalsByType(AnimalType.Monkey).Returns([animal]);

        var food = Substitute.For<IFood>();
        _foodFactory.GetFood(1).Returns([food]);

        // Act
        _sut.FeedAnimals();

        // Assert
        animal.Received(1).Eat(food);
    }

    [Test]
    public void ReviveAnimals_CallsReviveOnAllAnimals()
    {
        // Arrange
        var animal1 = Substitute.For<IAnimal>();
        var animal2 = Substitute.For<IAnimal>();
        _animalManager.AllAnimals.Returns([animal1, animal2]);

        // Act
        _sut.ReviveAnimals();

        // Assert
        animal1.Received(1).Revive();
        animal2.Received(1).Revive();
    }

    [Test]
    public void AreAnimalsHungry_ReturnsTrueIfAnyAnimalIsHungry()
    {
        // Arrange
        var hungryAnimal = Substitute.For<IAnimal>();
        hungryAnimal.IsHungry.Returns(true);

        var fullAnimal = Substitute.For<IAnimal>();
        fullAnimal.IsHungry.Returns(false);

        _animalManager.AllAnimals.Returns([fullAnimal, hungryAnimal]);

        // Act

        // Assert
        Assert.That(_sut.AreAnimalsHungry);
    }

    [Test]
    public void CycleTime_AddsTimeToClock()
    {
        // Arrange
        var timespan = TimeSpan.FromMinutes(30);

        // Act
        _sut.CycleTime(timespan);

        // Assert
        _clock.Received(1).AddTime(timespan);
    }

    [Test]
    public void GetAnimalsByType_ReturnsExpectedAnimals()
    {
        // Arrange
        var animal = Substitute.For<IAnimal>();
        _animalManager.GetAnimalsByType(AnimalType.Giraffe).Returns([animal]);

        // Act
        var result = _sut.GetAnimalsByType(AnimalType.Giraffe);

        // Assert
        Assert.That(result.Length, Is.EqualTo(1));
        Assert.That(animal, Is.SameAs(result[0]));
    }
}
