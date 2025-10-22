using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BitsNBobs
{
    public class ProjectileAttackShooter : MonoBehaviour
    {
        public FixedDirectionMovementController projectilePrefab;
        public TargetResolver.Target targetType;
        public string baseCooldownSecondsKey;
        public string baseTargetRangeKey;

        IUnitProvider _contextProvider;
        TargetResolver.Context _context;
        Transform _target;
        float _nextActivationTime;

        public void Awake()
        {
            _contextProvider = gameObject.GetComponentInParent<IUnitProvider>();
            _context = _contextProvider.Context;
        }

        public void Update()
        {
            if (Time.time < _nextActivationTime)
                return;
            
            _target = TargetResolver.Resolve(_context, targetType);
            if (!_target)
                return;

            if (!_target.TryGetComponent<HealthController>(out _))
            {
                _target = null;
                return;
            }

            var myPos = transform.position;
            var targetPos = _target.position;
            var targetRange = Config.Get<float>(baseTargetRangeKey);
            if (Vector3.Distance(myPos, targetPos) > targetRange)
                return;
            _nextActivationTime = GetNextActivationTime();

            SpawnProjectile(myPos, targetPos);
        }

        float GetNextActivationTime()
        {
            // TODO: De-duplicate with MeleeAttack
            var baseCooldown = Config.Get<float>(baseCooldownSecondsKey);
            var attackSpeed = _contextProvider.Stats?.FloatStats.GetValueOrDefault(Stats.ATTACK_SPEED) ?? 0;
            var attacksPerSecond = (1 / baseCooldown) * (1 + attackSpeed);
            var cooldown = 1 / attacksPerSecond;
            return Time.time + cooldown;
        }

        void SpawnProjectile(Vector3 myPos, Vector3 targetPos)
        {
            var projectileMovementController =
                Instantiate<FixedDirectionMovementController>(projectilePrefab, myPos, Quaternion.identity,
                    parent: DisabledGameObject.I.transform);
            projectileMovementController.MoveTowards(targetPos);

            if (projectileMovementController.TryGetComponent<InheritOwner>(out var inheritOwner))
                inheritOwner.Owner = _contextProvider;
            
            projectileMovementController.transform.SetParent(null);
        }

#if UNITY_EDITOR
        float _showRangeUntil;
        
        [ContextMenu("Show range")]
        public void Editor_ShowRange()
        {
            _showRangeUntil = (float)EditorApplication.timeSinceStartup + 10;
        }

        public void OnDrawGizmos()
        {
            if (EditorApplication.timeSinceStartup > _showRangeUntil)
                return;
            var targetRange = Config.Get<float>(baseTargetRangeKey);
            Gizmos.DrawWireSphere(transform.position, targetRange);
        }
#endif
    }
}