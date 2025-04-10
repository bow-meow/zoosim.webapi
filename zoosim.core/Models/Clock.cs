using zoosim.core.Systems;
using Timer = System.Timers.Timer;

namespace zoosim.core.Models;

public class Clock : IClock
{
    private readonly List<ISystem> _systems = [];
    private readonly Timer _timer;
    private DateTime _currentTime;
    private TimeSpan _timeAdded;

    private TimeSpan _timeToAddEachInterval;
    public TimeSpan TimeToAddEachInterval
    {
        get => _timeToAddEachInterval;
        set
        {
            if (_timeToAddEachInterval != value)
            {
                _timeToAddEachInterval = value;
            }
        }
    }

    public DateTime CurrentTime
    {
        get => _currentTime;
        private set
        {
            if (_currentTime != value)
            {
                _currentTime = value;

                foreach (var system in _systems)
                    system.Run(CurrentTime);

                CurrentTimeChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public event EventHandler CurrentTimeChanged;

    public Clock()
    {
        _currentTime = DateTime.Now;

        _timer = new Timer(TimeSpan.FromSeconds(1).TotalMilliseconds);
        _timer.Elapsed += (s, e) =>
        {
            _timeAdded = _timeAdded.Add(TimeToAddEachInterval);
            CurrentTime = DateTime.Now.Add(_timeAdded);
        };

        _timer.AutoReset = true;
    }

    public void AddTime(TimeSpan time)
    {
        _timeAdded = _timeAdded.Add(time);
        CurrentTime = DateTime.Now.Add(_timeAdded);
    }

    public void AddSystems(params ISystem[] systems) => _systems.AddRange(systems);

    public void AddSystems(IEnumerable<ISystem> systems) => _systems.AddRange(systems);

    public void Stop() => _timer.Stop();
    public void Start() => _timer.Start();
}
