using System;
using System.Collections.Generic;

namespace BitsNBobs
{
    public static class Config
    {
        private static readonly Dictionary<string, object> Data = new()
        {
            { "Enemy_Slime_Basic.InitialMaxHealth", 15 },
            { "Enemy_Slime_Basic.MeleeAttack.BaseCooldownSeconds", 1f },
            { "Enemy_Slime_Basic.MeleeAttack.BaseDamage", 5 },
            { "Enemy_Slime_Basic.MovementSpeed", 2.5f },
            { "Enemy_Slime_Fast.InitialMaxHealth", 10 },
            { "Enemy_Slime_Fast.MeleeAttack.BaseCooldownSeconds", 1f },
            { "Enemy_Slime_Fast.MeleeAttack.BaseDamage", 5 },
            { "Enemy_Slime_Fast.MovementSpeed", 3.5f },
            { "Enemy_Slime_MoreDamage.InitialMaxHealth", 15 },
            { "Enemy_Slime_MoreDamage.MeleeAttack.BaseCooldownSeconds", 1f },
            { "Enemy_Slime_MoreDamage.MeleeAttack.BaseDamage", 15 },
            { "Enemy_Slime_MoreDamage.MovementSpeed", 2f },
            { "Enemy_Slime_Tanky.InitialMaxHealth", 35 },
            { "Enemy_Slime_Tanky.MeleeAttack.BaseCooldownSeconds", 1f },
            { "Enemy_Slime_Tanky.MeleeAttack.BaseDamage", 15 },
            { "Enemy_Slime_Tanky.MovementSpeed", 2f },
            { "Player.InitialMaxHealth", 50 },
            { "Player.MovementSpeed", 5f },
            { "Player.ProjectileAttack.BaseCooldownSeconds", 1f },
            { "Player.ProjectileAttack.BaseTargetRange", 10f },
            { "Player_Projectile.BaseDamage", 10 },
            { "Player_Projectile.MovementSpeed", 8f },
        };
        
        public static T Get<T>(string key)
        {
#if UNITY_EDITOR
            if (!Data.TryGetValue(key, out var value))
                throw new KeyNotFoundException($"Config key '{key}' does not exist.");
            try
            {
                return (T)value;
            }
            catch (InvalidCastException)
            {
                UnityEngine.Debug.LogError(
                    $"Config key '{key}' (of type {value.GetType()}) does not match requested type {typeof(T)}).");
                throw;
            }
#else
            return (T)Data[key];
#endif
        }
    }
}