using System;
using UnityEngine;

namespace BitsNBobs
{
    public class MoveToTargetController : MonoBehaviour
    {
        public string speedKey;
        public TargetResolver.Target target;

        TargetResolver.Context _targetContext;
        Transform _target;

        public void Awake()
        {
            bool isPlayer = GetComponentInParent<Player>();
            _targetContext = new TargetResolver.Context(transform, isPlayer);
        }

        public void Update()
        {
            if (!_target)
                _target = TargetResolver.Resolve(_targetContext, target);

            if (!_target)
                return;
            
            var speed = Config.Get<float>(speedKey);
            transform.Translate((_target.position - transform.position).normalized * speed * Time.deltaTime);
        }
    }
}