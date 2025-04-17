using NUnit.Framework;
using zoosim.core.Models.Animals;

namespace zoosim.core.tests.Models.Animals;

[TestFixture]
public class MonkeyTest
{
    private Monkey _sut;

    [SetUp]
    public void Setup()
    {
        _sut = new Monkey();
    }

    [Test]
    public void IsAlive_CanMove_WhenHealthIsEqualToOrAbove30()
    {
        // Arrange

        // Act
        _sut.TakeDamage(70);

        // Assert
        Assert.That(_sut.IsAlive);
        Assert.That(_sut.CanMove);
    }

    [Test]
    public void IsDead_CantMove_WhenHealthIsBelow30()
    {
        // Arrange

        // Act
        _sut.TakeDamage(71);

        // Assert
        Assert.That(!_sut.IsAlive);
        Assert.That(!_sut.CanMove);
    }
}
