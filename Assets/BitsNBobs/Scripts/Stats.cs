using System.Collections.Generic;

namespace BitsNBobs
{
    public class Stats
    {
        internal static readonly IReadOnlyCollection<string> s_IntStatKeys = new HashSet<string>()
        {
            "BaseDamage",
            "InitialMaxHealth",
        };
        internal static readonly IReadOnlyCollection<string> s_FloatStatKeys = new HashSet<string>()
        {
            "BaseTargetRange",
            "MovementSpeed",
            "ProjectileSpeed",
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