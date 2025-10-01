using UnityEngine;

namespace BitsNBobs
{
    public class ProjectileAttack : MonoBehaviour
    {
        public TargetResolver.Target targetType;
        public string baseDamageKey;

        TargetResolver.Context _context;
        float _nextActivationTime;
        

        public void Awake()
        {
            var contextProvider = GetComponentInParent<IUnitProvider>();
            _context = contextProvider.Context;
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            var hitTarget = collision.transform;
            if (
                !TargetResolver.IsValid(_context, targetType, hitTarget) ||
                !hitTarget.TryGetComponent<HealthController>(out var targetHealthController)
            ) return;
            
            var damage = Config.Get<int>(baseDamageKey);
            targetHealthController.CurrentHealth -= damage;
            Destroy(gameObject);
        }
    }
}