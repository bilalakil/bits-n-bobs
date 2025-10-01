using System;
using UnityEngine;

namespace BitsNBobs
{
    public class CoinController : MonoBehaviour
    {
        public static CoinController I { get; private set; }

        public event Action OnCoinAmountChanged;

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
        
        public void OnEnable()
        {
            I = this;
        }

        public void OnDisable()
        {
            I = null;
        }
    }
}