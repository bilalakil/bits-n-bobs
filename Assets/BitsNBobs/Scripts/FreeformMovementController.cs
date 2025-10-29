using UnityEngine;

namespace BitsNBobs
{
    public class FreeformMovementController : MonoBehaviour
    {
        public MoveSpeedHelper.MovementType movementType;
        public string speedKey;
        
        IUnitProvider _unitProvider;

        public void Awake()
        {
            _unitProvider = GetComponentInParent<IUnitProvider>();
        }

        public void TickMovement(Vector2 direction)
        {
            var speed = MoveSpeedHelper.CalculateMoveSpeed(_unitProvider, speedKey, movementType);
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}
