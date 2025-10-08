using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using System.Text.RegularExpressions;
using System.Linq;
using UnityEditor;
#endif

namespace BitsNBobs.Data
{
    [CreateAssetMenu(menuName = "BitsNBobs/ItemLibrary", fileName = "ItemLibrary")]
    public class ItemLibrary : ScriptableObject
    {
        [SerializeField] List<Item> items = new();
        public IReadOnlyList<Item> Items => items;

        readonly Dictionary<string, Item> _itemsByKey = new();
        public IReadOnlyDictionary<string, Item> ItemsByKey => _itemsByKey;

        public void OnEnable() => RegenerateLookups();
        
        void RegenerateLookups()
        {
            _itemsByKey.Clear();
            foreach (var item in items)
                _itemsByKey[item.Key] = item;
        }

#if UNITY_EDITOR
        public void OnValidate() => RegenerateLookups();

        class RelevantConfigNames
        {
            public HashSet<string> ItemNames = new();
            public Dictionary<string, HashSet<string>> StatNamesByItemName = new();
        }
        RelevantConfigNames _configNames;

        [ContextMenu("Regenerate")]
        public void RegenerateLibrary()
        {
            items.Clear();

            _configNames = ExtractConfigNames();
            foreach (var itemName in _configNames.ItemNames)
            {
                if (!TryBuildItem(itemName, out var item))
                    continue;
                items.Add(item);
            }

            RegenerateLookups();
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }

        static readonly Regex s_itemNameInConfigRe = new(@"^(Item_[^\.]+)\.(.+)");
        static readonly Regex s_itemStatInConfigRemainderRe = new(@"^Stats\.([^.]+)$");
        static RelevantConfigNames ExtractConfigNames()
        {
            var result = new RelevantConfigNames();
            foreach (var match in Config.DataKeys.Select(key => s_itemNameInConfigRe.Match(key))
                         .Where(match => match.Success))
            {
                var itemName = match.Groups[1].Value;
                result.ItemNames.Add(itemName);

                var remainder = match.Groups[2].Value;
                var statMatch = s_itemStatInConfigRemainderRe.Match(remainder);
                if (statMatch.Success)
                {
                    if (!result.StatNamesByItemName.ContainsKey(itemName))
                        result.StatNamesByItemName.Add(itemName, new HashSet<string>());
                    result.StatNamesByItemName[itemName].Add(statMatch.Groups[1].Value);
                }
            }
            return result;
        }

        const string LOG_PREFIX = "[ItemLibrary] ";
        bool TryBuildItem(string itemName, out Item item)
        {
            var hasError = false;
            if (!TryGetCost(itemName, out var cost))
            {
                Debug.LogError($"{LOG_PREFIX} {itemName} is missing cost!");
                hasError = true;
            }

            if (!TryGetIcon(itemName, out var icon))
            {
                Debug.LogError($"{LOG_PREFIX} {itemName} is missing icon!");
                hasError = true;
            }

            if (!TryGetStats(itemName, out var stats))
            {
                // Logs its own errors...
                hasError = true;
            }
            
            item = hasError ? null : new Item(key: itemName, cost, icon, stats);
            return !hasError;
        }

        static bool TryGetCost(string itemName, out int cost)
        {
            cost = 0;
            var costKey = $"{itemName}.Cost";
            if (!Config.DataKeys.Contains(costKey))
                return false;
            cost = Config.Get<int>(costKey);
            return true;
        }

        static bool TryGetIcon(string itemName, out Sprite icon)
        {
            icon = AssetDatabase.LoadAssetAtPath<Sprite>($"Assets/BitsNBobs/Sprites/Items/{itemName}.png");
            return icon;
        }

        bool TryGetStats(string itemName, out Stats stats)
        {
            stats = new Stats();

            if (!_configNames.StatNamesByItemName.TryGetValue(itemName, out var statNames))
                return true;
            
            var hasError = false;
            foreach (var statName in statNames)
            {
                if (Stats.s_IntStatKeys.Contains(statName))
                {
                    int value;
                    try
                    {
                        value = Config.Get<int>($"{itemName}.Stats.{statName}");
                    }
                    catch
                    {
                        Debug.LogError($"{LOG_PREFIX} {itemName}'s {statName}'s value failed to parse as an int!");
                        hasError = true;
                        continue;
                    }
                    
                    stats.SetInt(statName, value);
                }
                else if (Stats.s_FloatStatKeys.Contains(statName))
                {
                    float value;
                    try
                    {
                        value = Config.Get<float>($"{itemName}.Stats.{statName}");
                    }
                    catch
                    {
                        Debug.LogError($"{LOG_PREFIX} {itemName}'s {statName}'s value failed to parse as a float!");
                        hasError = true;
                        continue;
                    }
                    
                    stats.SetFloat(statName, value);
                }
                else
                {
                    Debug.LogError($"{LOG_PREFIX} {itemName} has unsupported stat {statName}!");
                    hasError = true;
                }
            }

            return !hasError;
        }
#endif
    }
}