using zoosim.core.Models;
using zoosim.core.Utils;

namespace zoosim.core.Factories;

public class FoodFactory : IFoodFactory
{
    private readonly IDice _dice;

    public FoodFactory(IDice dice)
    {
        _dice = dice;
    }

    public IFood[] GetFood(int numOfFood)
    {
        if (numOfFood <= 0)
            return [];

        var value = _dice.RollForHeal();

        var foods = new IFood[numOfFood];

        for (int i = 0; i < numOfFood; i++)
            foods[i] = new Food(value);

        return foods;
    }
}
