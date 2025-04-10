using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

                foreach (var animal in AnimalManager.AllAnimals.ToArray())
                    for (var i = 0; i < hoursPassed; i++)
                        Execute(animal);
            }
        }

        protected abstract void Execute(IAnimal animal);
    }
}
