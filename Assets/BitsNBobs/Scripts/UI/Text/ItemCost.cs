using TMPro;
using UnityEngine;

namespace BitsNBobs.UI.Text
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class ItemCost : MonoBehaviour
    {
        ItemButton _itemButton;
        TextMeshProUGUI _text;
        string _stringFormat;

        bool _started;

        public void Awake()
        {
            _itemButton = GetComponentInParent<ItemButton>();
            _text = GetComponent<TextMeshProUGUI>();
            _stringFormat = _text.text;
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
            _text.text = string.Format(_stringFormat, _itemButton.Item.Cost);
        }
    }
}