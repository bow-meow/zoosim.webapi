using zoosim.core.Systems;

namespace zoosim.core.Models;

public interface IClock
{
    DateTime CurrentTime { get; }
    void AddTime(TimeSpan time);
    TimeSpan TimeToAddEachInterval { get; set; }
    event EventHandler CurrentTimeChanged;
    public void Stop();
    public void Start();
    void AddSystems(params ISystem[] systems);
    void AddSystems(IEnumerable<ISystem> systems);
}
