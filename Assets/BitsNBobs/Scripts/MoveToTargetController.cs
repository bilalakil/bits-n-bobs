using UnityEngine;

namespace BitsNBobs
{
    public class MoveToTargetController : MonoBehaviour
    {
        public MoveSpeedHelper.MovementType movementType;
        public string speedKey;
        public TargetResolver.Target target;

        IUnitProvider _unitProvider;
        Transform _target;

        public void Awake()
        {
            _unitProvider = GetComponent<IUnitProvider>();
        }

        public void Update()
        {
            if (!_target)
                _target = TargetResolver.Resolve(_unitProvider.Context, target);

            if (!_target)
                return;
            
            var speed = MoveSpeedHelper.CalculateMoveSpeed(_unitProvider, speedKey, movementType);
            transform.Translate((_target.position - transform.position).normalized * speed * Time.deltaTime);
        }
    }
}