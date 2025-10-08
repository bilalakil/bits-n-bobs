using UnityEngine;

namespace BitsNBobs.UI.Image
{
    [RequireComponent(typeof(UnityEngine.UI.Image))]
    public class ItemIcon : MonoBehaviour
    {
        ItemButton _itemButton;
        UnityEngine.UI.Image _image;

        bool _started;

        public void Awake()
        {
            _itemButton = GetComponentInParent<ItemButton>();
            _image = GetComponent<UnityEngine.UI.Image>();
        }

        public void OnEnable()
        {
            _itemButton.OnItemUpdated += HandleItemChanged;
            if (_itemButton.Item != null) HandleItemChanged();
        }

        public void OnDisable()
        {
            if (_itemButton)
                _itemButton.OnItemUpdated -= HandleItemChanged;
        }

        void HandleItemChanged()
        {
            _image.sprite = _itemButton.Item.Icon;
        }
    }
}