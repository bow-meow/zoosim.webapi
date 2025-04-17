using NSubstitute;
using NUnit.Framework;
using zoosim.core.Enums;
using zoosim.core.Managers;
using zoosim.core.Models;
using zoosim.core.Models.Animals;
using zoosim.core.Systems;
using zoosim.core.Utils;

namespace zoosim.core.tests.Systems;

[TestFixture]
public class FatigueSystemTest
{
    private IDice _dice;
    private IClock _clock;
    private IAnimalManager _animalManager;
    private FatigueSystem _sut;
    private IAnimal _elephant;
    private IAnimal _monkey;

    [SetUp]
    public void SetUp()
    {
        _dice = Substitute.For<IDice>();
        _clock = Substitute.For<IClock>();
        _clock.CurrentTime.Returns(DateTime.Now);
        _animalManager = Substitute.For<IAnimalManager>();

        _sut = new FatigueSystem(_dice, _animalManager);

        _elephant = Substitute.For<IElephant>();
        _elephant.AnimalType.Returns(AnimalType.Elephant);
        _elephant.CanMove.Returns(false);
        _elephant.Health.Returns(100);
        _elephant.IsAlive.Returns(true);

        _monkey = Substitute.For<IMonkey>();
        _monkey.AnimalType.Returns(AnimalType.Monkey);
        _monkey.CanMove.Returns(true);
        _monkey.Health.Returns(100);
        _monkey.IsAlive.Returns(true);
    }

    [Test]
    public void Should_ApplyDamage_EveryHour()
    {
        // Arrange
        _animalManager.AllAnimals.Returns([_monkey]);
        _dice.RollForFatigue().Returns(10);

        var after = DateTime.Now.AddHours(2);

        _clock.CurrentTime.Returns(after);

        // Act
        _sut.Run(after);

        // Assert
        _monkey.Received(2).TakeDamage(10);
    }

    [Test]
    public void Should_KillElephant_IfCannotMoveForMoreThan1Hour()
    {
        // Arrange
        _animalManager.AllAnimals.Returns([_elephant]);
        _dice.RollForFatigue().Returns(0);

        var after = DateTime.Now.AddHours(1);

        _clock.CurrentTime.Returns(after);

        // Act
        _sut.Run(after);

        // Assert
        _elephant.Received(1).Kill();
    }

    [Test]
    public void Should_ResetElephantHours_IfCanMove()
    {
        // Arrange
        _elephant.CanMove.Returns(true);

        _animalManager.AllAnimals.Returns([_elephant]);
        _dice.RollForFatigue().Returns(0);

        var after = DateTime.Now.AddHours(2);

        _clock.CurrentTime.Returns(after);

        // Act
        _sut.Run(after);

        // Assert
        _elephant.DidNotReceive().Kill();
        _elephant.Received().TakeDamage(0);
    }
}
