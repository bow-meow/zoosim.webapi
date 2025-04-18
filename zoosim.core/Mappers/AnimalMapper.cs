using zoosim.core.Models;
using zoosim.webapi.Dtos;

namespace zoosim.webapi.Mappers;

public class AnimalMapper
{
    public static void MapToDto(IAnimal animal, AnimalDto dto)
    {
        dto.CanMove = animal.CanMove;
        dto.IsAlive = animal.IsAlive;
        dto.Health = animal.Health;
        dto.IsHungry = animal.IsHungry;
    }
}
