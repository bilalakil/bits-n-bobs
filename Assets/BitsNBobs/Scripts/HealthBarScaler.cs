using UnityEngine;

namespace BitsNBobs
{
    public class HealthBarScaler : MonoBehaviour
    {
        public Transform scaleTarget;
        
        HealthController _healthController;

        bool _isDirty;

        public void Awake()
        {
            _healthController = GetComponentInParent<HealthController>();
        }

        public void OnEnable()
        {
            _healthController.OnHealthChanged += SetDirty;
            SetDirty();
        }

        public void OnDisable()
        {
            _healthController.OnHealthChanged -= SetDirty;
        }

        public void Update()
        {
            if (!_isDirty)
                return;
            _isDirty = false;
            RefreshScale();
        }

        void SetDirty() => _isDirty = true;

        void RefreshScale()
        {
            var pctLife = _healthController.MaxHealth == 0
                ? 0
                : Mathf.Clamp01((float)_healthController.CurrentHealth / _healthController.MaxHealth);
            scaleTarget.localScale = new Vector3(pctLife, 1f, 1f);
        }
    }
}