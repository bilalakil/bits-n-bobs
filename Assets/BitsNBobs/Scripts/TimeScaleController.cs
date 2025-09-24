using UnityEngine;

namespace BitsNBobs
{
    public class TimeScaleController : MonoBehaviour
    {
        HealthController _playerHealthController;

        bool _started;
        bool _isPlayerDead;
        bool _isDirty;

        public void OnEnable()
        {
            if (!_started)
                return;
            _playerHealthController = Player.I.GetComponent<HealthController>();
            if (!_playerHealthController)
                return;
            _playerHealthController.OnDiedChanged += CheckPlayerDied;
            CheckPlayerDied();
        }

        public void Start()
        {
            _started = true;
            OnEnable();
        }

        public void OnDisable()
        {
            if (!_playerHealthController)
                return;
            _playerHealthController.OnDiedChanged -= CheckPlayerDied;
        }

        void Update()
        {
            if (!_isDirty)
                return;
            _isDirty = false;
            RefreshTimeScale();
        }

        void SetDirty() => _isDirty = true;

        void CheckPlayerDied()
        {
            _isPlayerDead = _playerHealthController.IsDead;
            SetDirty();
        }

        void RefreshTimeScale()
        {
            Time.timeScale = _isPlayerDead ? 0 : 1;
        }
    }
}