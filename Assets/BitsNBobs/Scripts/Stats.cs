using System.Collections.Generic;

namespace BitsNBobs
{
    public class Stats
    {
        readonly Dictionary<string, int> _intStats = new();
        readonly Dictionary<string, float> _floatStats = new();
        
        public int GetInt(string key) => _intStats.GetValueOrDefault(key, 0);
        public float GetFloat(string key) => _floatStats.GetValueOrDefault(key, 0);
        
        public void SetInt(string key, int value) => _intStats[key] = value;
        public void SetFloat(string key, float value) => _floatStats[key] = value;

        public void IncInt(string key, int value) => _intStats[key] = _intStats.GetValueOrDefault(key, 0) + value;
        public void IncFloat(string key, float value) => _floatStats[key] = _floatStats.GetValueOrDefault(key, 0) + value;
    }
}