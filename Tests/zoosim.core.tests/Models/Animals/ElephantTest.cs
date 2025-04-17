using NUnit.Framework;
using zoosim.core.Models.Animals;

namespace zoosim.core.tests.Models.Animals;

[TestFixture]
internal class ElephantTest
{
    private Elephant _sut;

    [SetUp]
    public void Setup()
    {
        _sut = new Elephant();
    }

    [Test]
    public void IsAlive_CanMove_WhenHealthIsEqualToOrAbove70()
    {
        // Arrange

        // Act
        _sut.TakeDamage(30);

        // Assert
        Assert.That(_sut.IsAlive);
        Assert.That(_sut.CanMove);
    }

    [Test]
    public void IsAlive_CantMove_WhenHealthIsBelow70()
    {
        // Arrange

        // Act
        _sut.TakeDamage(50);

        // Assert
        Assert.That(_sut.IsAlive);
        Assert.That(!_sut.CanMove);
    }

    [Test]
    public void IsDead_CantMove_WhenHealthIs0()
    {
        // Arrange

        // Act
        _sut.TakeDamage(100);

        // Assert
        Assert.That(!_sut.IsAlive);
        Assert.That(!_sut.CanMove);
    }
}
