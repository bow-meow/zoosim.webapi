using zoosim.core.Enums;

namespace zoosim.core.Configuration;

public record ZooConfiguration(AnimalConfiguration[] AnimalInfos);
public record AnimalConfiguration(AnimalType AnimalType, int NumOfAnimal);
