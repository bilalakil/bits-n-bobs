using UnityEngine;
using UnityEngine.EventSystems;

namespace BitsNBobs.UI
{
    public class SpawnerUpgradeButton : MonoBehaviour, IPointerClickHandler
    {
        SpawnerProvider _spawnerProvider;

        public void Awake()
        {
            _spawnerProvider = GetComponentInParent<SpawnerProvider>();
        }

        public void OnPointerClick(PointerEventData eventData) =>
            PlayerCommands.UpgradeEnemySpawner(_spawnerProvider.enemyKey);
    }
}