using UnityEngine;

namespace BitsNBobs
{
    public class MeleeAttack : MonoBehaviour
    {
        public TargetResolver.Target targetType;
        public string baseCooldownSecondsKey;
        public string baseDamageKey;
        
        TargetResolver.Context _context;
        Transform _target;
        private float _nextActivationTime;
        

        public void Awake()
        {
            var contextProvider = GetComponentInParent<ITargetContextProvider>();
            _context = contextProvider.Context;
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (Time.time < _nextActivationTime)
                return;

            var hitTarget = collision.transform;
            if (
                !TargetResolver.IsValid(_context, targetType, hitTarget) ||
                !hitTarget.TryGetComponent<HealthController>(out var targetHealthController)
            ) return;
            
            var cooldown = Config.Get<float>(baseCooldownSecondsKey);
            var damage = Config.Get<int>(baseDamageKey);
            _nextActivationTime = Time.time + cooldown;
            targetHealthController.CurrentHealth -= damage;
        }
    }
}