using zoosim.core.Managers;
using zoosim.core.Models;

namespace zoosim.core.Systems
{
    public abstract class System : ISystem
    {
        protected readonly IAnimalManager AnimalManager;
        private DateTime _lastTimeChecked = DateTime.Now;
        public System(IAnimalManager animalManager)
        {
            AnimalManager = animalManager;
        }

        public void Run(DateTime currentTime)
        {
            var elapsed = currentTime - _lastTimeChecked;
            if (elapsed >= TimeSpan.FromHours(1))
            {
                _lastTimeChecked = currentTime;

                var hoursPassed = (int)elapsed.TotalHours;

                for (var i = 0; i < hoursPassed; i++)
                    foreach (var animal in AnimalManager.AllAnimals.ToArray())
                        Execute(animal);
            }
        }

        protected abstract void Execute(IAnimal animal);
    }
}
