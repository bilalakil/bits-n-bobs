using System;
using UnityEngine;

namespace BitsNBobs
{
    public class CoinController : MonoBehaviour
    {
        public static CoinController I { get; private set; }

        public event Action OnCoinAmountChanged;
        public string initialCoinAmountKey;

        int _coinAmount;

        public int CoinAmount
        {
            get => _coinAmount;
            set
            {
                var targetValue = value < 0 ? 0 : value;
                if (_coinAmount == targetValue)
                    return;
                _coinAmount = value;
                OnCoinAmountChanged?.Invoke();
            }
        }

        public void Awake()
        {
            _coinAmount = Config.Get<int>(initialCoinAmountKey);
        }
        
        public void OnEnable()
        {
            I = this;
        }

        public void OnDisable()
        {
            I = null;
        }

#if UNITY_EDITOR
        [ContextMenu("Grant 10000 Coins")]
        public void Grant10000Coins()
        {
            if (!Application.isPlaying)
                return;
            CoinAmount += 10000;
        }
#endif
    }
}