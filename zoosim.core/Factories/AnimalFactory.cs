using Microsoft.Extensions.DependencyInjection;
using zoosim.core.Enums;
using zoosim.core.Models;
using zoosim.core.Models.Animals;
using zoosim.webapi.Dtos;

namespace zoosim.core.Factories;

public class AnimalFactory : IAnimalFactory
{
    protected readonly IServiceProvider ServiceProvider;

    public AnimalFactory(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    public virtual IAnimal CreateAnimal(AnimalType animalType)
    {
        return animalType switch
        {
            AnimalType.Elephant => ServiceProvider.GetService<IElephant>(),
            AnimalType.Giraffe => ServiceProvider.GetService<IGiraffe>(),
            AnimalType.Monkey => ServiceProvider.GetService<IMonkey>(),
            _ => throw new NotImplementedException($"The type {animalType} was not handled in the CreateAnimal method"),
        };
    }
}
