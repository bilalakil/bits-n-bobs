using UnityEngine;
using UnityEngine.InputSystem;

namespace BitsNBobs
{
    public class PlayerInputController : MonoBehaviour
    {
        FreeformMovementController _freeformMovementController;

        public void Awake()
        {
            _freeformMovementController = GetComponent<FreeformMovementController>();
        }

        public void Update()
        {
            if (_freeformMovementController)
                TickMovement();
        }

        void TickMovement()
        {
            var direction = new Vector2(
                (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
                    ? 1
                    : ((Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
                        ? -1
                        : 0
                    ),
                (Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed)
                    ? 1
                    : ((Keyboard.current.downArrowKey.isPressed || Keyboard.current.sKey.isPressed)
                        ? -1
                        : 0
                    )
            ).normalized;
            _freeformMovementController.TickMovement(direction);
        }
    }
}