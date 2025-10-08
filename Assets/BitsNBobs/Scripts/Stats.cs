using System.Collections.Generic;

namespace BitsNBobs
{
    public class Stats
    {
        public const string BASE_DAMAGE = "BaseDamage";
        public const string MOVEMENT_SPEED = "MovementSpeed";
        internal static readonly IReadOnlyCollection<string> s_IntStatKeys = new HashSet<string>()
        {
            BASE_DAMAGE,
            //"MaxHealth",
        };
        internal static readonly IReadOnlyCollection<string> s_FloatStatKeys = new HashSet<string>()
        {
            //"BaseTargetRange",
            MOVEMENT_SPEED,
            //"ProjectileSpeed",
        };
        
        readonly Dictionary<string, int> _intStats = new();
        readonly Dictionary<string, float> _floatStats = new();
        
        public IReadOnlyDictionary<string, int> IntStats => _intStats;
        public IReadOnlyDictionary<string, float> FloatStats => _floatStats;
        
        public void SetInt(string key, int value) => _intStats[key] = value;
        public void SetFloat(string key, float value) => _floatStats[key] = value;

        public void IncInt(string key, int value) => _intStats[key] = _intStats.GetValueOrDefault(key, 0) + value;
        public void IncFloat(string key, float value) => _floatStats[key] = _floatStats.GetValueOrDefault(key, 0) + value;
    }
}