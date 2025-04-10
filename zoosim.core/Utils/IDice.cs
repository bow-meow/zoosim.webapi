using zoosim.core.Enums;

namespace zoosim.core.Utils;

public interface IDice
{
    int Roll(int min, int max);
    int RollForHeal();
    int RollForFatigue();
}
