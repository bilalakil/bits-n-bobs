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

        [ContextMenu("Regenerate")]
        public void RegenerateLibrary()
        {
            items.Clear();

            var itemNames = ExtractItemNamesFromConfig();
            foreach (var itemName in itemNames)
            {
                if (!TryBuildItem(itemName, out var item))
                    continue;
                items.Add(item);
            }

            RegenerateLookups();
        }

        static readonly Regex s_itemNameInConfigRe = new(@"^(Item_[^\.]+)\..+");
        static IEnumerable<string> ExtractItemNamesFromConfig()
        {
            return Config.DataKeys
                .Select(key => s_itemNameInConfigRe.Match(key))
                .Where(match => match.Success)
                .Select(match => match.Groups[1].Value)
                .Distinct();
        }

        const string LOG_PREFIX = "[ItemLibrary] ";
        static bool TryBuildItem(string itemName, out Item item)
        {
            item = null;
            if (!TryGetCost(itemName, out var cost))
            {
                Debug.LogError($"{LOG_PREFIX} {itemName} is missing cost!");
                return false;
            }

            if (!TryGetIcon(itemName, out var icon))
            {
                Debug.LogError($"{LOG_PREFIX} {itemName} is missing icon!");
                return false;
            }
            
            item = new Item(key: itemName, cost, icon);
            return true;
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
#endif
    }
}