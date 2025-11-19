using TMPro;
using UnityEngine;

namespace BitsNBobs.UI.Text
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class SpawnerUpgradeCost : MonoBehaviour
    {
        SpawnerProvider _spawnerProvider;

        TextMeshProUGUI _text;
        string _stringFormat;

        bool _started;

        public void Awake()
        {
            _spawnerProvider = GetComponentInParent<SpawnerProvider>();
            _text = GetComponent<TextMeshProUGUI>();
            _stringFormat = _text.text;
        }

        public void OnEnable()
        {
            if (!_started || _spawnerProvider.Spawner == null) return;
            _spawnerProvider.Spawner.OnUpgraded += Refresh;
            Refresh();
        }

        public void OnDisable()
        {
            if (_spawnerProvider.Spawner == null) return;
            _spawnerProvider.Spawner.OnUpgraded -= Refresh;
        }

        public void Start()
        {
            _started = true;
            OnEnable();
        }

        void Refresh()
        {
            _text.text = string.Format(_stringFormat, _spawnerProvider.Spawner.UpgradeCost);
        }
    }
}