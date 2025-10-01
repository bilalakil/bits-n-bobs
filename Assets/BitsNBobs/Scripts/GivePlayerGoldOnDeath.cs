using System;
using System.Collections.Generic;
using UnityEngine;

namespace BitsNBobs
{
    [RequireComponent(typeof(HealthController))]
    public class GivePlayerGoldOnDeath : MonoBehaviour
    {
        public string coinAmountKey;

        HealthController _healthController;

        public void Awake()
        {
            _healthController = GetComponent<HealthController>();
        }

        public void OnEnable()
        {
            if (_healthController)
                _healthController.OnDiedChanged += HandleDiedChanged;
        }

        public void OnDisable()
        {
            if (_healthController)
                _healthController.OnDiedChanged -= HandleDiedChanged;
        }

        void HandleDiedChanged()
        {
            if (!_healthController.IsDead || !CoinController.I)
                return;
            CoinController.I.CoinAmount += Config.Get<int>(coinAmountKey);
        }
    }
}