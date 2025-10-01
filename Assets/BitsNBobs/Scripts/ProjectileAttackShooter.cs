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

        IUnitProvider _myContextProvider;
        TargetResolver.Context _context;
        Transform _target;
        HealthController _targetHealthController;
        float _nextActivationTime;

        public void Awake()
        {
            _myContextProvider = gameObject.GetComponentInParent<IUnitProvider>();
            _context = _myContextProvider.Context;
        }

        public void Update()
        {
            if (Time.time < _nextActivationTime)
                return;
            
            _target = TargetResolver.Resolve(_context, targetType);
            if (!_target)
                return;

            if (!_target.TryGetComponent(out _targetHealthController))
            {
                _target = null;
                return;
            }

            var myPos = transform.position;
            var targetPos = _target.position;
            var targetRange = Config.Get<float>(baseTargetRangeKey);
            if (Vector3.Distance(myPos, targetPos) > targetRange)
                return;
            var cooldown = Config.Get<float>(baseCooldownSecondsKey);
            _nextActivationTime = Time.time + cooldown;

            SpawnProjectile(myPos, targetPos);
        }

        void SpawnProjectile(Vector3 myPos, Vector3 targetPos)
        {
            var projectileMovementController =
                Instantiate<FixedDirectionMovementController>(projectilePrefab, myPos, Quaternion.identity,
                    parent: DisabledGameObject.I.transform);
            projectileMovementController.MoveTowards(targetPos);

            if (projectileMovementController.TryGetComponent<InheritOwner>(out var inheritOwner))
                inheritOwner.Owner = _myContextProvider;
            
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