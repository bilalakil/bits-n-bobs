using System;
using System.Collections.Generic;

namespace BitsNBobs
{
    public class Stats
    {
        public const string ATTACK_SPEED = "AttackSpeed";
        public const string BASE_DAMAGE = "BaseDamage";
        public const string BASE_HEALTH_REGENERATION_PER_SECOND = "BaseHealthRegenerationPerSecond";
        public const string MAX_HEALTH = "MaxHealth";
        public const string MOVEMENT_SPEED = "MovementSpeed";
        public const string PROJECTILE_SPEED = "ProjectileSpeed";
        
        internal static readonly IReadOnlyCollection<string> s_IntStatKeys = new HashSet<string>()
        {
            BASE_DAMAGE,
            MAX_HEALTH,
        };
        internal static readonly IReadOnlyCollection<string> s_FloatStatKeys = new HashSet<string>()
        {
            ATTACK_SPEED,
            BASE_HEALTH_REGENERATION_PER_SECOND,
            //"BaseTargetRange",
            MOVEMENT_SPEED,
            PROJECTILE_SPEED,
        };
        
        readonly Dictionary<string, int> _intStats = new();
        readonly Dictionary<string, float> _floatStats = new();

        Dictionary<string, HashSet<Action>> _handlersByKey;
        
        public IReadOnlyDictionary<string, int> IntStats => _intStats;
        public IReadOnlyDictionary<string, float> FloatStats => _floatStats;

        public void SetInt(string key, int value)
        {
            _intStats[key] = value;
            InvokeHandlers(key);
        }

        public void SetFloat(string key, float value)
        {
            _floatStats[key] = value;
            InvokeHandlers(key);
        }

        void InvokeHandlers(string key)
        {
            if (_handlersByKey?.TryGetValue(key, out var handlers) != true)
                return;
            foreach (var handler in handlers)
                handler.Invoke();
        }
        
        public void IncInt(string key, int value) => SetInt(key, _intStats.GetValueOrDefault(key, 0) + value);
        public void IncFloat(string key, float value) => SetFloat(key, _floatStats.GetValueOrDefault(key, 0) + value);

        public void RegisterChangeHandler(string key, Action handler)
        {
            _handlersByKey ??= new Dictionary<string, HashSet<Action>>();
            if (!_handlersByKey.TryGetValue(key, out var handlers))
            {
                handlers = new HashSet<Action>();
                _handlersByKey.Add(key, handlers);
            }

            if (!handlers.Add(handler))
                UnityEngine.Debug.LogWarning("Registered the same handler twice?!");
        }

        public void DeregisterChangeHandler(string key, Action handler)
        {
            if (
                _handlersByKey?.TryGetValue(key, out var handlers) != true ||
                !handlers.Remove(handler)
            ) UnityEngine.Debug.LogWarning("Whooshed when trying to deregister a handler?!");
        }
    }
}