using UnityEngine;
using UnityEngine.EventSystems;

namespace BitsNBobs.UI
{
    public class ShopRefreshButton : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            PlayerCommands.TryRefreshShop();
        }
    }
}