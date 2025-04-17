using zoosim.core.Enums;

namespace zoosim.core.Models.Animals;

public abstract class Animal : IAnimal
{
    private const float MAX_HEALTH = 100;
    public Animal()
    {
        Health = MAX_HEALTH;
    }

    public void TakeDamage(int amount)
    {
        if (!IsAlive) return;

        Health -= amount;

        if (!IsAlive)
            Health = 0;
    }

    private float _health;

    public float Health
    {
        get => _health;
        private set
        {
            if(_health == value) return;

            float newValue = value;

            if (newValue <= 0f)
                newValue = 0f;
            if (newValue > 100f)
                newValue = 100f;

            _health = newValue;
        }
    }

    public bool IsHungry => IsAlive && Health < MAX_HEALTH;

    public void Eat(IFood food)
    {
        if (!IsAlive) return;
        if (!IsHungry) return;
        using (food)
        {
            Health += Health * (food.HealingValue / 100f);
        }
    }

    public virtual bool CanMove => IsAlive;
    public abstract bool IsAlive { get; }

    public abstract AnimalType AnimalType { get; }

    public void Revive()
    {
        Health = MAX_HEALTH;
    }

    public void Kill() => Health = 0;
}
