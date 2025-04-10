using NUnit.Framework;
using zoosim.core.Models.Animals;

namespace zoosim.core.tests.Models.Animals;

[TestFixture]
internal class GiraffeTest
{
    private Giraffe _sut;

    [SetUp]
    public void Setup()
    {
        _sut = new Giraffe();
    }

    [Test]
    public void IsAlive_CanMove_WhenHealthIs50OrMore()
    {
        // Arrange

        // Act
        _sut.TakeDamage(50);

        // Assert
        Assert.That(_sut.IsAlive);
        Assert.That(_sut.CanMove);
    }

    [Test]
    public void IsDead_CantMove_WhenHealthIsBelow50()
    {
        // Arrange

        // Act
        _sut.TakeDamage(51);

        // Assert

        Assert.That(!_sut.IsAlive);
        Assert.That(!_sut.CanMove);
    }

    [TearDown]
    public void Teardown()
    {
        _sut.Dispose();
    }
}
