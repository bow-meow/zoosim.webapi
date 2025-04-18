using zoosim.core.Enums;

namespace zoosim.webapi.Dtos;

public record AnimalDto(AnimalType Type,
    ImageDto AliveImg,
    ImageDto DeadImg)
{
    public float Health { get; set; } = 100;
    public bool IsHungry { get; set; } = false;
    public bool IsAlive { get; set; } = true;
    public bool CanMove { get; set; } = true;
}
public record ImageDto(string Src,
    string Alt);
