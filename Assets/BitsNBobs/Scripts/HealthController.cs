using System;
using System.Collections.Generic;
using UnityEngine;

namespace BitsNBobs
{
    [RequireComponent(typeof(IUnitProvider))]
    public class HealthController : MonoBehaviour
    {
        public event Action OnHealthChanged;
        public event Action OnDiedChanged;

        public string initialMaxHealthKey;
        public string initialBaseHealthRegenerationKey;

        int _maxHealth;
        int _currentHealth;

        public int MaxHealth
        {
            get => _maxHealth;
            private set
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
        
        public float HealthRegenerationPerSecond { get; private set; }

        IUnitProvider _unitProvider;
        int _initialMaxHealth;
        float _initialBaseHealthRegenerationPerSecond;

        float _accumulatedRegen;

        public void Awake()
        {
            _unitProvider = gameObject.GetComponent<IUnitProvider>();
            _initialMaxHealth = MaxHealth = CurrentHealth = Config.Get<int>(initialMaxHealthKey);
            _initialBaseHealthRegenerationPerSecond = HealthRegenerationPerSecond =
                string.IsNullOrEmpty(initialBaseHealthRegenerationKey)
                    ? 0f
                    : Config.Get<float>(initialBaseHealthRegenerationKey);
        }

        public void OnEnable()
        {
            if (_unitProvider.Stats == null)
                return;
            _unitProvider.Stats.RegisterChangeHandler(Stats.MAX_HEALTH, HandleMaxHealthStatChanged);
            HandleMaxHealthStatChanged();

            _unitProvider.Stats.RegisterChangeHandler(Stats.BASE_HEALTH_REGENERATION_PER_SECOND,
                HandleRegenStatChanged);
            HandleRegenStatChanged();
        }

        public void OnDisable()
        {
            _unitProvider.Stats?.DeregisterChangeHandler(Stats.MAX_HEALTH, HandleMaxHealthStatChanged);
            _unitProvider.Stats?.DeregisterChangeHandler(Stats.BASE_HEALTH_REGENERATION_PER_SECOND,
                HandleRegenStatChanged);
        }

        public void Update()
        {
            TickHealthRegen();
        }

        void HandleMaxHealthStatChanged()
        {
            MaxHealth = _initialMaxHealth + _unitProvider.Stats.IntStats.GetValueOrDefault(Stats.MAX_HEALTH);
        }

        void HandleRegenStatChanged()
        {
            HealthRegenerationPerSecond = _initialBaseHealthRegenerationPerSecond +
                                           _unitProvider.Stats.FloatStats.GetValueOrDefault(
                                               Stats.BASE_HEALTH_REGENERATION_PER_SECOND);
        }

        void TickHealthRegen()
        {
            if (CurrentHealth >= MaxHealth)
                return;
            _accumulatedRegen += HealthRegenerationPerSecond * Time.deltaTime;
            if (_accumulatedRegen < 1)
                return;
            var amountToHealth = (int)_accumulatedRegen;
            _accumulatedRegen -= amountToHealth;
            CurrentHealth += amountToHealth;
        }
    }
}