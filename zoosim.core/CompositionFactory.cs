using Microsoft.Extensions.DependencyInjection;
using zoosim.core.Configuration;
using zoosim.core.Engines;
using zoosim.core.Factories;
using zoosim.core.Managers;
using zoosim.core.Models;
using zoosim.core.Models.Animals;
using zoosim.core.Systems;
using zoosim.core.Utils;

namespace zoosim.core;

public class CompositionFactory
{
    public static void Compose(IServiceCollection serviceCollection)
    {
        var config = new ZooConfiguration(
        [
            new AnimalConfiguration(Enums.AnimalType.Monkey, 5),
            new AnimalConfiguration(Enums.AnimalType.Giraffe, 5),
            new AnimalConfiguration(Enums.AnimalType.Elephant, 5)
        ]);

        serviceCollection.AddSingleton(config);

        // Engines
        serviceCollection.AddScoped<IZooEngine, ZooEngine>();

        // Factories
        serviceCollection.AddTransient<IAnimalFactory, AnimalFactory>();
        serviceCollection.AddTransient<IFoodFactory, FoodFactory>();

        // Models
        serviceCollection.AddScoped<IClock, Clock>();
        serviceCollection.AddTransient<IElephant, Elephant>();
        serviceCollection.AddTransient<IMonkey, Monkey>();
        serviceCollection.AddTransient<IGiraffe, Giraffe>();

        // Managers
        serviceCollection.AddScoped<IAnimalManager, AnimalManager>();

        // Utils
        serviceCollection.AddSingleton<IDice, Dice>();
        serviceCollection.AddSingleton<IRandomWrapper, RandomWrapper>();

        // Systems
        serviceCollection.AddScoped<ISystem, FatigueSystem>();
    }
}
