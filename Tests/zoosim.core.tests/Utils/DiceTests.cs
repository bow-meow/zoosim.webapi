using NSubstitute;
using NUnit.Framework;
using zoosim.core.Utils;

namespace zoosim.core.tests.Utils;

[TestFixture]
internal class DiceTests
{
    private IRandomWrapper _randomWrapper;
    private Dice _sut;

    [SetUp]
    public void Setup()
    {
        _randomWrapper = Substitute.For<IRandomWrapper>();
        _sut = new Dice(_randomWrapper);
    }

    [Test]
    public void RollForFatigue_ShouldReturn_ValuesBetween0And20()
    {
        // Arrange
        var min = 0;
        var max = 21;
        var expected = 5;
        _randomWrapper.Next(0, 21).Returns(5);

        // Act
        var val = _sut.RollForFatigue();

        // Assert
        Assert.That(val, Is.EqualTo(expected));
        _randomWrapper.Received(1).Next(min, max);
    }

    [Test]
    public void RollForHeal_ShouldReturn_ValuesBetween10And26()
    {
        // Arrange
        var min = 10;
        var max = 26;
        var expected = 11;
        _randomWrapper.Next(min, max).Returns(expected);

        // Act
        var val = _sut.RollForHeal();

        // Assert
        Assert.That(val, Is.EqualTo(expected));
        _randomWrapper.Received(1).Next(min, max);
    }

    [Test]
    public void Roll_ShouldReturn_RandomValues()
    {
        // Arrange
        var min = 0;
        var max = 100;
        var expected = 69;
        _randomWrapper.Next(min, max).Returns(expected);

        // Act
        var val = _sut.Roll(min, max);

        // Assert
        Assert.That(val, Is.EqualTo(expected));
        _randomWrapper.Received(1).Next(min, max);
    }
}
