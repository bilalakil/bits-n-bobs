using System;
using UnityEngine;

namespace BitsNBobs
{
    public static class TargetResolver
    {
        public struct Context
        {
            public readonly Transform Source;
            public readonly bool IsPlayer;

            public Context(Transform transform, bool isPlayer)
            {
                Source = transform;
                IsPlayer = isPlayer;
            }
        }
        
        [Serializable]
        public enum Target
        {
            Player,
            NearestFoe,
        }

        public static bool IsValid(Context context, Target targetType, Transform target) => targetType switch
        {
            Target.Player => target.GetComponentInParent<Player>(),
            Target.NearestFoe => context.IsPlayer
                ? target.GetComponentInParent<Enemy>()
                : target.GetComponentInParent<Player>(),
            _ => throw new ArgumentOutOfRangeException(nameof(targetType), targetType, null),
        };

        public static Transform Resolve(Context context, Target targetType) => targetType switch
        {
            Target.Player => Player.I?.transform,
            Target.NearestFoe => GetNearestFoe(context),
            _ => throw new ArgumentOutOfRangeException(nameof(targetType), targetType, null),
        };

        private static Transform GetNearestFoe(Context context)
        {
            if (!context.IsPlayer)
                throw new NotImplementedException();
            
            var curPos = context.Source.position;

            Transform closestEnemy = null;
            var closestDistance = float.MaxValue;
            foreach (var enemy in Enemy.All)
            {
                var enemyTfm = enemy.transform;
                var distance = Vector3.Distance(curPos, enemyTfm.position);
                if (distance > closestDistance)
                    continue;
                closestEnemy = enemyTfm;
                closestDistance = distance;
            }

            return closestEnemy;
        }
    }
}