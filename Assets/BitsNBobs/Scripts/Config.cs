using System;
using System.Collections.Generic;

namespace BitsNBobs
{
    public static class Config
    {
        private static readonly Dictionary<string, object> Data = new()
        {
            { "Enemy_Slime_Basic.DeathCoinAmount", 1 },
            { "Enemy_Slime_Basic.InitialMaxHealth", 15 },
            { "Enemy_Slime_Basic.MeleeAttack.BaseCooldownSeconds", 1f },
            { "Enemy_Slime_Basic.MeleeAttack.BaseDamage", 5 },
            { "Enemy_Slime_Basic.MovementSpeed", 2.5f },
            { "Enemy_Slime_Fast.DeathCoinAmount", 2 },
            { "Enemy_Slime_Fast.InitialMaxHealth", 10 },
            { "Enemy_Slime_Fast.MeleeAttack.BaseCooldownSeconds", 1f },
            { "Enemy_Slime_Fast.MeleeAttack.BaseDamage", 5 },
            { "Enemy_Slime_Fast.MovementSpeed", 3.5f },
            { "Enemy_Slime_MoreDamage.DeathCoinAmount", 2 },
            { "Enemy_Slime_MoreDamage.InitialMaxHealth", 15 },
            { "Enemy_Slime_MoreDamage.MeleeAttack.BaseCooldownSeconds", 1f },
            { "Enemy_Slime_MoreDamage.MeleeAttack.BaseDamage", 15 },
            { "Enemy_Slime_MoreDamage.MovementSpeed", 2f },
            { "Enemy_Slime_Tanky.DeathCoinAmount", 2 },
            { "Enemy_Slime_Tanky.InitialMaxHealth", 35 },
            { "Enemy_Slime_Tanky.MeleeAttack.BaseCooldownSeconds", 1f },
            { "Enemy_Slime_Tanky.MeleeAttack.BaseDamage", 15 },
            { "Enemy_Slime_Tanky.MovementSpeed", 2f },
            
            
            
            { "Item_T1Shoes.Cost", 1 },
            { "Item_T1Shoes.Stats.MovementSpeed", .25f },
            { "Item_T2Shoes.Cost", 2 },
            { "Item_T2Shoes.Stats.MovementSpeed", .5f },
            { "Item_T3Shoes.Cost", 4 },
            { "Item_T3Shoes.Stats.MovementSpeed", 1f },
            { "Item_T4Shoes.Cost", 8 },
            { "Item_T4Shoes.Stats.MovementSpeed", 2f },
            { "Item_T5Shoes.Cost", 16 },
            { "Item_T5Shoes.Stats.MovementSpeed", 4f },

            { "Item_T1Armour.Cost", 1 },
            { "Item_T1Armour.Stats.MaxHealth", 1 },
            { "Item_T2Armour.Cost", 2 },
            { "Item_T2Armour.Stats.MaxHealth", 2 },
            { "Item_T3Armour.Cost", 4 },
            { "Item_T3Armour.Stats.MaxHealth", 4 },
            { "Item_T4Armour.Cost", 8 },
            { "Item_T4Armour.Stats.MaxHealth", 8 },
            { "Item_T5Armour.Cost", 16 },
            { "Item_T5Armour.Stats.MaxHealth", 16 },

            { "Item_T1Crossbow.Cost", 1 },
            { "Item_T1Crossbow.Stats.BaseDamage", .5f },
            { "Item_T2Crossbow.Cost", 2 },
            { "Item_T2Crossbow.Stats.BaseDamage", 1f },
            { "Item_T3Crossbow.Cost", 4 },
            { "Item_T3Crossbow.Stats.BaseDamage", 2f },
            { "Item_T4Crossbow.Cost", 8 },
            { "Item_T4Crossbow.Stats.BaseDamage", 4f },
            { "Item_T5Crossbow.Cost", 16 },
            { "Item_T5Crossbow.Stats.BaseDamage", 8f },

            { "Item_T1Bolts.Cost", 1 },
            { "Item_T1Bolts.Stats.ProjectileSpeed", .25f },
            { "Item_T1Bolts.Stats.BaseDamage", .25f },
            { "Item_T2Bolts.Cost", 2 },
            { "Item_T2Bolts.Stats.ProjectileSpeed", .5f },
            { "Item_T2Bolts.Stats.BaseDamage", .5f },
            { "Item_T3Bolts.Cost", 4 },
            { "Item_T3Bolts.Stats.ProjectileSpeed", 1f },
            { "Item_T3Bolts.Stats.BaseDamage", 1f },
            { "Item_T4Bolts.Cost", 8 },
            { "Item_T4Bolts.Stats.ProjectileSpeed", 2f },
            { "Item_T4Bolts.Stats.BaseDamage", 2f },
            { "Item_T5Bolts.Cost", 16 },
            { "Item_T5Bolts.Stats.ProjectileSpeed", 4f },
            { "Item_T5Bolts.Stats.BaseDamage", 4f },

            { "Item_T1Binoculars.Cost", 1 },
            { "Item_T1Binoculars.Stats.BaseTargetRange", .2f },
            { "Item_T2Binoculars.Cost", 2 },
            { "Item_T2Binoculars.Stats.BaseTargetRange", .4f },
            { "Item_T3Binoculars.Cost", 4 },
            { "Item_T3Binoculars.Stats.BaseTargetRange", .8f },
            { "Item_T4Binoculars.Cost", 8 },
            { "Item_T4Binoculars.Stats.BaseTargetRange", 1.6f },
            { "Item_T5Binoculars.Cost", 16 },
            { "Item_T5Binoculars.Stats.BaseTargetRange", 3.2f },

            { "Item_T6EyePiece.Cost", 32 },
            { "Item_T6EyePiece.Stats.BaseTargetRange", 10f },
            { "Item_T6EyePiece.Stats.BaseDamage", 4f },
            { "Item_T6EyePiece.Stats.MaxHealth", -8f },

            { "Item_T6MedievalArmour.Cost", 32 },
            { "Item_T6MedievalArmour.Stats.MaxHealth", 200 },
            { "Item_T6MedievalArmour.Stats.MovementSpeed", -1000f },

            { "Item_T6LightningShoes.Cost", 32 },
            { "Item_T6LightningShoes.Stats.MovementSpeed", 16f },
            { "Item_T6LightningShoes.Stats.BaseTargetRange", -8f },
            
            
            
            { "Player.InitialMaxHealth", 50 },
            { "Player.MovementSpeed", 5f },
            { "Player.ProjectileAttack.BaseCooldownSeconds", 1f },
            { "Player.ProjectileAttack.BaseTargetRange", 10f },
            { "Player_Projectile.BaseDamage", 10 },
            { "Player_Projectile.MovementSpeed", 8f },
        };
        public static IReadOnlyCollection<string> DataKeys => Data.Keys;
        
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