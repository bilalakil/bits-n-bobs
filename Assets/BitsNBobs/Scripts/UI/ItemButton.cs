using System;
using BitsNBobs.Data;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BitsNBobs.UI
{
    public class ItemButton : MonoBehaviour, IPointerClickHandler
    {
        public event Action OnItemUpdated;

        public Item Item { get; private set; }
        int _index = -1;

        public void Use(int itemIndex)
        {
            _index = itemIndex;
            Item = ShopController.I.ShopItems[_index];
            OnItemUpdated?.Invoke();
        }

        public void OnPointerClick(PointerEventData eventData) => PlayerCommands.TryBuyItem(_index);
    }
}