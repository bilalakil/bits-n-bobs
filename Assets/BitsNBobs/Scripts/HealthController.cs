using System;
using UnityEngine;

namespace BitsNBobs
{
    public class HealthController : MonoBehaviour
    {
        public event Action OnHealthChanged;
        public event Action OnDiedChanged;

        public string initialMaxHealthKey;

        int _maxHealth;
        int _currentHealth;

        public int MaxHealth
        {
            get => _maxHealth;
            set
            {
                if (_maxHealth == value)
                    return;
                var delta = value - _maxHealth;
                _maxHealth = value;
                
                if (delta > 0)
                    _currentHealth += delta;
                else if (_currentHealth > _maxHealth)
                    _currentHealth = _maxHealth;
                
                OnHealthChanged?.Invoke();
            }
        }
        public int CurrentHealth
        {
            get => _currentHealth;
            set
            {
                var targetValue = Mathf.Clamp(value, 0, _maxHealth);
                var wasDead = _currentHealth <= 0;
                if (_currentHealth == targetValue)
                    return;
                _currentHealth = targetValue;
                var isDead = _currentHealth <= 0;
                OnHealthChanged?.Invoke();
                
                if (wasDead != isDead)
                    OnDiedChanged?.Invoke();
            }
        }
        public bool IsDead => _currentHealth <= 0;

        public void Awake()
        {
            MaxHealth = CurrentHealth = Config.Get<int>(initialMaxHealthKey);
        }
    }
}