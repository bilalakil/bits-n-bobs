using UnityEngine;

namespace BitsNBobs.UI.Image
{
    [RequireComponent(typeof(UnityEngine.UI.Image))]
    public class SpawnProgress : MonoBehaviour
    {
        UnityEngine.UI.Image _image;
        SpawnerProvider _spawnerProvider;

        public void Awake()
        {
            _image = GetComponent<UnityEngine.UI.Image>();
            _spawnerProvider = GetComponentInParent<SpawnerProvider>();
        }

        public void Update()
        {
            _image.fillAmount = _spawnerProvider.Spawner?.NextSpawnPct ?? 0;
        }
    }
}