using UnityEngine;

namespace BitsNBobs
{
    public class FreeformMovementController : MonoBehaviour
    {
        public string speedKey;

        public void TickMovement(Vector2 direction)
        {
            var speed = Config.Get<float>(speedKey);
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}
