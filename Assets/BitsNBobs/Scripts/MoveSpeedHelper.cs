using System;
using System.Collections.Generic;
using UnityEngine;
using static BitsNBobs.MoveSpeedHelper.MovementType;

namespace BitsNBobs
{
    public static class MoveSpeedHelper
    {
        [Serializable]
        public enum MovementType
        {
            Unknown = 0,
            UnitMovement = 1,
            ProjectileMovement = 2,
        }

        public static float CalculateMoveSpeed(IUnitProvider unitProvider, string speedKey, MovementType movementType)
        {
            var speed = Config.Get<float>(speedKey);
            if (unitProvider != null)
            {
                var relevantStat = GetRelevantMoveSpeedStat(movementType);
                if (relevantStat != null)
                    speed += unitProvider.Stats?.FloatStats.GetValueOrDefault(relevantStat) ?? 0;
            }
            var cap = GetCap(movementType);
            return Mathf.Max(0, Mathf.Min(speed, cap));
        }

        private static float GetCap(MovementType movementType) => movementType switch
        {
            UnitMovement => Config.MOVE_SPEED_CAP,
            ProjectileMovement => Config.PROJECTILE_SPEED_SPEED_CAP,
            // ReSharper disable once PatternIsRedundant - to remove the suggestion to add a branch for it
            Unknown or _ => throw new ArgumentOutOfRangeException(nameof(movementType), movementType, null)
        };

        private static string GetRelevantMoveSpeedStat(MovementType movementType) => movementType switch
        {
            UnitMovement => Stats.MOVEMENT_SPEED,
            ProjectileMovement => Stats.PROJECTILE_SPEED,
            _ => "",
        };
    }
}