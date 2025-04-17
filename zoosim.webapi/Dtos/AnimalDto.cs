namespace zoosim.webapi.Dtos;

public record AnimalDto(float Health, ImgDto Img, string Type, bool IsHungry, bool IsAlive, bool CanMove);
