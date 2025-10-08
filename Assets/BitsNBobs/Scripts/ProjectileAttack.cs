using System.Collections.Generic;
using UnityEngine;

namespace BitsNBobs
{
    public class ProjectileAttack : MonoBehaviour
    {
        public TargetResolver.Target targetType;
        public string baseDamageKey;

        IUnitProvider _unitProvider;
        TargetResolver.Context _context;
        float _nextActivationTime;
        

        public void Awake()
        {
            _unitProvider = GetComponentInParent<IUnitProvider>();
            _context = _unitProvider.Context;
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            var hitTarget = collision.transform;
            if (
                !TargetResolver.IsValid(_context, targetType, hitTarget) ||
                !hitTarget.TryGetComponent<HealthController>(out var targetHealthController)
            ) return;

            var damage = Config.Get<int>(baseDamageKey) +
                         _unitProvider.Stats?.IntStats.GetValueOrDefault(Stats.BASE_DAMAGE) ?? 0;
            targetHealthController.CurrentHealth -= damage;
            Destroy(gameObject);
        }
    }
}