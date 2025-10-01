using TMPro;
using UnityEngine;

namespace BitsNBobs.UI.Text
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextCoinCount : MonoBehaviour
    {
        TextMeshProUGUI _text;
        string _stringFormat;

        bool _started;

        public void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _stringFormat = _text.text;
        }

        public void OnEnable()
        {
            if (!_started || !CoinController.I)
                return;
            CoinController.I.OnCoinAmountChanged += HandleCoinAmountChanged;
            HandleCoinAmountChanged();
        }

        public void Start()
        {
            _started = true;
            OnEnable();
        }

        public void OnDisable()
        {
            if (CoinController.I)
                CoinController.I.OnCoinAmountChanged -= HandleCoinAmountChanged;
        }

        void HandleCoinAmountChanged()
        {
            _text.text = string.Format(_stringFormat, CoinController.I.CoinAmount);
        }
    }
}