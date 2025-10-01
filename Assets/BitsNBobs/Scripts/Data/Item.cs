using System;
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

        public Item(string key, int cost, Sprite icon)
        {
            this.key = key;
            this.cost = cost;
            this.icon = icon;
        }
    }
}