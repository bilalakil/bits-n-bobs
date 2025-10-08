using System;
using System.Collections.Generic;
using UnityEngine;

namespace BitsNBobs.Data
{
    [Serializable]
    public class Item
    {
        [SerializeField] string key;
        public string Key => key;
        
        [SerializeField] Sprite icon;
        public Sprite Icon => icon;

        [SerializeField] int cost;
        public int Cost => cost;

        [SerializeField] List<string> intStatKeys = new();
        public IReadOnlyList<string> IntStatKeys => intStatKeys;
        
        [SerializeField] List<int> intStatValues = new();
        public IReadOnlyList<int> IntStatValues => intStatValues;
        
        [SerializeField] List<string> floatStatKeys = new();
        public IReadOnlyList<string> FloatStatKeys => floatStatKeys;
        
        [SerializeField] List<float> floatStatValues = new();
        public IReadOnlyList<float> FloatStatValues => floatStatValues;

        public Item(string key, int cost, Sprite icon, Stats stats)
        {
            this.key = key;
            this.cost = cost;
            this.icon = icon;

            foreach (var (intKey, intValue) in stats.IntStats)
            {
                intStatKeys.Add(intKey);
                intStatValues.Add(intValue);
            }

            foreach (var (floatKey, floatValue) in stats.FloatStats)
            {
                floatStatKeys.Add(floatKey);
                floatStatValues.Add(floatValue);
            }
        }
    }
}