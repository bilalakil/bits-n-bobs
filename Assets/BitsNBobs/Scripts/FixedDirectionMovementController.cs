using UnityEngine;

namespace BitsNBobs
{
    public class FixedDirectionMovementController : MonoBehaviour
    {
        public MoveSpeedHelper.MovementType movementType;
        public string speedKey;
        public bool alsoRotate;

        IUnitProvider _unitProvider;
        Vector2 _direction;

        public void Awake()
        {
            _unitProvider = GetComponent<IUnitProvider>();
        }

        public void Update()
        {
            var speed = MoveSpeedHelper.CalculateMoveSpeed(_unitProvider, speedKey, movementType);
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
