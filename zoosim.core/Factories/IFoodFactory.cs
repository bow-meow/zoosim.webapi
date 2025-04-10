using zoosim.core.Models;

namespace zoosim.core.Factories;

public interface IFoodFactory
{
    IFood[] GetFood(int numOfFood = 1);
}
