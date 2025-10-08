using System.Collections.Generic;
using UnityEngine;

namespace BitsNBobs.UI
{
    public class ShopUIController : MonoBehaviour
    {
        [SerializeField] List<ItemButton> itemButtons;

        bool _started;

        public void OnEnable()
        {
            if (!_started || !ShopController.I)
                return;
            ShopController.I.OnShopChanged += HandleShopChanged;
            HandleShopChanged();
        }

        public void Start()
        {
            _started = true;
            OnEnable();
        }

        public void OnDisable()
        {
            if (ShopController.I)
                ShopController.I.OnShopChanged -= HandleShopChanged;
        }

        void HandleShopChanged()
        {
            for (var i = 0; i != itemButtons.Count; i++)
            {
                var button = itemButtons[i];
                if (i >= ShopController.I.ShopItems.Count)
                {
                    button.gameObject.SetActive(false);
                    continue;
                }
                button.Use(i);
                button.gameObject.SetActive(true);
            }
        }
    }
}