using UnityEngine;
using UnityEngine.EventSystems;

namespace BitsNBobs.UI
{
    public class SpawnEnemyButton : MonoBehaviour, IPointerClickHandler
    {
        public GameObject prefab;

        public void OnPointerClick(PointerEventData eventData) => PlayerCommands.SpawnEnemy(prefab);
    }
}