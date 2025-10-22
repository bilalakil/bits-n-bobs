using System;
using System.Collections.Generic;
using BitsNBobs.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BitsNBobs
{
    public class ShopController : MonoBehaviour
    {
        public static ShopController I { get; private set; }

        public event Action OnShopChanged;
        public IReadOnlyList<Item> ShopItems => _shopItems;
        readonly List<Item> _shopItems = new();

        // TODO: Better dependency injection than this...
        [SerializeField] ItemLibrary itemLibrary;

        public void Awake()
        {
            RegenerateItems();
        }

        public void OnEnable()
        {
            I = this;
        }

        public void OnDisable()
        {
            I = null;
        }

        public void TryBuyItem(int index)
        {
            if (index < 0 || index >= ShopItems.Count)
                return;
            var item = ShopItems[index];
            if (item.Cost != 0 && (!CoinController.I || CoinController.I.CoinAmount < item.Cost))
                return;
            CoinController.I.CoinAmount -= item.Cost;
            GrantItem(item);
            _shopItems[index] = ChooseNewItem();
            OnShopChanged?.Invoke();
        }

        public void TryRefreshShop()
        {
            if (!CoinController.I)
                return;
            var cost = Config.Get<int>("Shop.RefreshCost");
            if (CoinController.I.CoinAmount < cost)
                return;
            CoinController.I.CoinAmount -= cost;
            RegenerateItems();
            OnShopChanged?.Invoke();
        }

        void RegenerateItems()
        {
            _shopItems.Clear();
            for (var i = 0; i < 3; ++i)
                _shopItems.Add(ChooseNewItem());
        }

        static void GrantItem(Item item)
        {
            if (!Player.I)
            {
                Debug.LogError("Player missing! Could not grant item.");
                return;
            }

            for (var i = 0; i < item.IntStatKeys.Count; ++i)
                Player.I.Stats.IncInt(item.IntStatKeys[i], item.IntStatValues[i]);

            for (var i = 0; i < item.FloatStatKeys.Count; ++i)
                Player.I.Stats.IncFloat(item.FloatStatKeys[i], item.FloatStatValues[i]);
        }

        Item ChooseNewItem() => itemLibrary.Items[Random.Range(0, itemLibrary.Items.Count)];
    }
}