using Microsoft.Extensions.DependencyInjection;
using zoosim.core.Engines;
using zoosim.core.Factories;
using zoosim.core.Managers;
using zoosim.core.Models;
using zoosim.core.Models.Animals;
using zoosim.core.Repositories;
using zoosim.core.Systems;
using zoosim.core.Utils;

namespace zoosim.core;

public class CompositionFactory
{
    public static void Compose(IServiceCollection serviceCollection)
    {
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

        // Repositories
        serviceCollection.AddTransient<IAnimalRepository, AnimalRepository>();
    }
}
