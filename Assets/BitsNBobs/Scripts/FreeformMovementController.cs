using System.Collections.Generic;
using UnityEngine;

namespace BitsNBobs
{
    public class FreeformMovementController : MonoBehaviour
    {
        public string speedKey;
        
        IUnitProvider _unitProvider;
        float _speedCap;

        public void Awake()
        {
            _unitProvider = GetComponentInParent<IUnitProvider>();
            _speedCap = _unitProvider.Context.IsPlayer ? Config.Get<float>("Player.MovementSpeedCap") : float.MaxValue;
        }

        public void TickMovement(Vector2 direction)
        {
            var speed = Config.Get<float>(speedKey);
            if (_unitProvider != null)
                speed += _unitProvider.Stats?.FloatStats.GetValueOrDefault(Stats.MOVEMENT_SPEED) ?? 0;
            speed = Mathf.Min(speed, _speedCap);
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}
