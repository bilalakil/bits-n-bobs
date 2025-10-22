using System;
using System.Collections.Generic;
using UnityEngine;

namespace BitsNBobs
{
    public class MeleeAttack : MonoBehaviour
    {
        public TargetResolver.Target targetType;
        public string baseCooldownSecondsKey;
        public string baseDamageKey;

        IUnitProvider _contextProvider;
        Transform _target;
        float _nextActivationTime;
        

        public void Awake()
        {
            _contextProvider = GetComponentInParent<IUnitProvider>();
        }

        public void OnCollisionEnter2D(Collision2D collision) => TryAttack(collision.transform);

        public void OnCollisionStay2D(Collision2D collision) => TryAttack(collision.transform);

        void TryAttack(Transform hitTarget)
        {
            if (Time.time < _nextActivationTime)
                return;

            if (
                !TargetResolver.IsValid(_contextProvider.Context, targetType, hitTarget) ||
                !hitTarget.TryGetComponent<HealthController>(out var targetHealthController)
            ) return;
            
            // TODO: Hook up damage...
            var damage = Config.Get<int>(baseDamageKey);
            _nextActivationTime = GetNextActivationTime();
            targetHealthController.CurrentHealth -= damage;
        }

        float GetNextActivationTime()
        {
            // TODO: De-duplicate with ProjectileAttackShooter
            var baseCooldown = Config.Get<float>(baseCooldownSecondsKey);
            var attackSpeed = _contextProvider.Stats?.FloatStats.GetValueOrDefault(Stats.ATTACK_SPEED) ?? 0;
            var attacksPerSecond = (1 / baseCooldown) * (1 + attackSpeed);
            var cooldown = 1 / attacksPerSecond;
            return Time.time + cooldown;
        }
    }
}