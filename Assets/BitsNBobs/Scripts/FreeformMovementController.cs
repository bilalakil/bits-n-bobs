using System.Collections.Generic;
using UnityEngine;

namespace BitsNBobs
{
    public class FreeformMovementController : MonoBehaviour
    {
        public string speedKey;
        
        IUnitProvider _unitProvider;

        public void Awake()
        {
            _unitProvider = GetComponentInParent<IUnitProvider>();
        }

        public void TickMovement(Vector2 direction)
        {
            var speed = Config.Get<float>(speedKey);
            if (_unitProvider != null)
                speed += _unitProvider.Stats?.FloatStats.GetValueOrDefault(Stats.MOVEMENT_SPEED) ?? 0;
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}
