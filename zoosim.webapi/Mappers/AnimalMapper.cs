using zoosim.core.Enums;
using zoosim.core.Models;
using zoosim.webapi.Dtos;

namespace zoosim.webapi.Mappers;

public static class AnimalMapper
{
    private static readonly Dictionary<(AnimalType type, bool isAlive), ImgDto> _imageMap =
    new()
    {
        [(AnimalType.Monkey, true)] = new ImgDto("./assets/mankey.png", "A monkey"),
        [(AnimalType.Monkey, false)] = new ImgDto("assets/mankey_dead.png", "A crossed out monkey"),
        [(AnimalType.Giraffe, true)] = new ImgDto("assets/girafarig.png", "A giraffe"),
        [(AnimalType.Giraffe, false)] = new ImgDto("assets/girafarig_dead.png", "A crossed out giraffe"),
        [(AnimalType.Elephant, true)] = new ImgDto("assets/phanpy.png", "An elephant"),
        [(AnimalType.Elephant, false)] = new ImgDto("assets/phanpy_dead.png", "A crossed out elephant"),
    };
    public static AnimalDto MapToDto(IAnimal animal)
    {
        return new AnimalDto(animal.Health,
            GetImageDto(animal),
            animal.AnimalType.ToString(),
            animal.IsHungry,
            animal.IsAlive,
            animal.CanMove);
    }

    private static ImgDto GetImageDto(IAnimal animal)
    {
        if (_imageMap.TryGetValue((animal.AnimalType, animal.IsAlive), out var img))
            return img;
        throw new NotImplementedException($"There is no mapping for {animal.AnimalType} is alive {animal.IsAlive}");
    }
}
