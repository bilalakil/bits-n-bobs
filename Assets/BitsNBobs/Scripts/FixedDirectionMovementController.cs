using UnityEngine;

namespace BitsNBobs
{
    public class FixedDirectionMovementController : MonoBehaviour
    {
        public string speedKey;
        public bool alsoRotate;

        Vector2 _direction;

        public void Update()
        {
            var speed = Config.Get<float>(speedKey);
            transform.Translate(_direction * speed * Time.deltaTime, Space.World);
        }

        public void MoveTowards(Vector3 point)
        {
            var delta = point - transform.position;
            _direction = new Vector2(delta.x, delta.y).normalized;

            if (!alsoRotate)
                return;
            var angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
