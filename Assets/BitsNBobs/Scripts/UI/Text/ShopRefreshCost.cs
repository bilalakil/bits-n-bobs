using TMPro;
using UnityEngine;

namespace BitsNBobs.UI.Text
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class ShopRefreshCost : MonoBehaviour
    {
        TextMeshProUGUI _text;
        string _stringFormat;

        public void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _stringFormat = _text.text;
        }

        public void OnEnable()
        {
            _text.text = string.Format(_stringFormat, Config.Get<int>("Shop.RefreshCost"));
        }
    }
}