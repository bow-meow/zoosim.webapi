using zoosim.core.Enums;
using zoosim.webapi.Dtos;

namespace zoosim.core.Repositories;

public interface IAnimalRepository
{
    AnimalDto[] GetAnimalsByType(AnimalType type);
}
internal class AnimalRepository : IAnimalRepository
{
    private static readonly Dictionary<(AnimalType type, bool isAlive), ImageDto> _imageMap =
    new()
    {
        [(AnimalType.Monkey, true)] = new ImageDto("assets/mankey.png", "A monkey"),
        [(AnimalType.Monkey, false)] = new ImageDto("assets/mankey_dead.png", "A crossed out monkey"),
        [(AnimalType.Giraffe, true)] = new ImageDto("assets/girafarig.png", "A giraffe"),
        [(AnimalType.Giraffe, false)] = new ImageDto("assets/girafarig_dead.png", "A crossed out giraffe"),
        [(AnimalType.Elephant, true)] = new ImageDto("assets/phanpy.png", "An elephant"),
        [(AnimalType.Elephant, false)] = new ImageDto("assets/phanpy_dead.png", "A crossed out elephant"),
    };
    private static readonly Dictionary<AnimalType, int> _numofAnimalsMap =
    new()
    { 
        { AnimalType.Monkey, 5 },
        { AnimalType.Giraffe, 5 },
        { AnimalType.Elephant, 5 }
    };

    public AnimalDto[] GetAnimalsByType(AnimalType type)
    {
        var numofAnimals = _numofAnimalsMap[type];
        var animals = new AnimalDto[numofAnimals];
        for (int i = 0; i < numofAnimals; i++) 
        {
            animals[i] = new AnimalDto(type, _imageMap[(type, true)], _imageMap[(type, false)]);
        }
        return animals;
    }
}
