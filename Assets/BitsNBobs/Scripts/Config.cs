using System;
using System.Collections.Generic;

namespace BitsNBobs
{
    public static class Config
    {
        public const float MOVE_SPEED_CAP = 10f;
        public const float TARGET_RANGE_CAP = 20f;
        public const float PROJECTILE_SPEED_SPEED_CAP = 20f;
        
        const int BASE_ENEMY_COINS_DROPPED = 5;
        const int BASE_ENEMY_HEALTH = 100;
        const float BASE_ENEMY_ATTACK_SPEED = 1f;
        const int BASE_ENEMY_DAMAGE = 10;
        const float BASE_ENEMY_MOVEMENT_SPEED = 2.5f;

        const int BASE_ITEM_COST = 50;
        const int BASE_ITEM_HEALTH = 20;
        const float BASE_ITEM_HEALTH_REGENERATION = .2f;
        const float BASE_ITEM_MOVEMENT_SPEED = .25f;
        const int BASE_ITEM_DAMAGE = 6;
        const float BASE_ITEM_ATTACK_SPEED = .025f;
        const float BASE_ITEM_ATTACK_RANGE = 1f;
        const float BASE_ITEM_PROJECTILE_SPEED = 1.5f;

        static readonly Dictionary<string, object> Data = new()
        {
            { "Player.InitialMaxHealth", 100 },
            { "Player.BaseHealthRegenerationPerSecond", .6f },
            { "Player.MovementSpeed", 3.5f },
            { "Player.ProjectileAttack.BaseCooldownSeconds", 2f },
            { "Player.ProjectileAttack.BaseTargetRange", 6f },
            { "Player_Projectile.BaseDamage", 25 },
            { "Player_Projectile.MovementSpeed", 8f },


            { "Enemy_Slime_Basic.DeathCoinAmount", BASE_ENEMY_COINS_DROPPED },
            { "Enemy_Slime_Basic.InitialMaxHealth", BASE_ENEMY_HEALTH },
            { "Enemy_Slime_Basic.MeleeAttack.BaseCooldownSeconds", BASE_ENEMY_ATTACK_SPEED },
            { "Enemy_Slime_Basic.MeleeAttack.BaseDamage", BASE_ENEMY_DAMAGE },
            { "Enemy_Slime_Basic.MovementSpeed", BASE_ENEMY_MOVEMENT_SPEED },

            { "Enemy_Slime_Fast.DeathCoinAmount", BASE_ENEMY_COINS_DROPPED*2 },
            { "Enemy_Slime_Fast.InitialMaxHealth", BASE_ENEMY_HEALTH },
            { "Enemy_Slime_Fast.MeleeAttack.BaseCooldownSeconds", BASE_ENEMY_ATTACK_SPEED },
            { "Enemy_Slime_Fast.MeleeAttack.BaseDamage", BASE_ENEMY_DAMAGE },
            { "Enemy_Slime_Fast.MovementSpeed", BASE_ENEMY_MOVEMENT_SPEED*2 },

            { "Enemy_Slime_MoreDamage.DeathCoinAmount", BASE_ENEMY_COINS_DROPPED*2 },
            { "Enemy_Slime_MoreDamage.InitialMaxHealth", BASE_ENEMY_HEALTH },
            { "Enemy_Slime_MoreDamage.MeleeAttack.BaseCooldownSeconds", BASE_ENEMY_ATTACK_SPEED },
            { "Enemy_Slime_MoreDamage.MeleeAttack.BaseDamage", BASE_ENEMY_DAMAGE*2 },
            { "Enemy_Slime_MoreDamage.MovementSpeed", BASE_ENEMY_MOVEMENT_SPEED },

            { "Enemy_Slime_Tanky.DeathCoinAmount", BASE_ENEMY_COINS_DROPPED*2 },
            { "Enemy_Slime_Tanky.InitialMaxHealth", BASE_ENEMY_HEALTH*2 },
            { "Enemy_Slime_Tanky.MeleeAttack.BaseCooldownSeconds", BASE_ENEMY_ATTACK_SPEED },
            { "Enemy_Slime_Tanky.MeleeAttack.BaseDamage", BASE_ENEMY_DAMAGE },
            { "Enemy_Slime_Tanky.MovementSpeed", BASE_ENEMY_MOVEMENT_SPEED },

            { "Enemy_Bat_Basic.DeathCoinAmount", BASE_ENEMY_COINS_DROPPED },
            { "Enemy_Bat_Basic.InitialMaxHealth", BASE_ENEMY_HEALTH/2 },
            { "Enemy_Bat_Basic.MeleeAttack.BaseCooldownSeconds", BASE_ENEMY_ATTACK_SPEED/8 },
            { "Enemy_Bat_Basic.MeleeAttack.BaseDamage", BASE_ENEMY_DAMAGE },
            { "Enemy_Bat_Basic.MovementSpeed", BASE_ENEMY_MOVEMENT_SPEED*3 },

            { "Enemy_Snail_Basic.DeathCoinAmount", BASE_ENEMY_COINS_DROPPED*4 },
            { "Enemy_Snail_Basic.InitialMaxHealth", BASE_ENEMY_HEALTH*10 },
            { "Enemy_Snail_Basic.MeleeAttack.BaseCooldownSeconds", BASE_ENEMY_ATTACK_SPEED/4 },
            { "Enemy_Snail_Basic.MeleeAttack.BaseDamage", BASE_ENEMY_DAMAGE/2 },
            { "Enemy_Snail_Basic.MovementSpeed", BASE_ENEMY_MOVEMENT_SPEED/4 },


            { "Shop.RefreshCost", BASE_ITEM_COST/5 },

            { "Item_Shoes.Cost", BASE_ITEM_COST },
            { "Item_Shoes.Stats.MovementSpeed", BASE_ITEM_MOVEMENT_SPEED },

            { "Item_Helmet.Cost", BASE_ITEM_COST },
            { "Item_Helmet.Stats.MaxHealth", BASE_ITEM_HEALTH*2 },
            { "Item_Helmet.Stats.BaseTargetRange", -BASE_ITEM_ATTACK_RANGE*1.5 },

            { "Item_Armour.Cost", BASE_ITEM_COST },
            { "Item_Armour.Stats.MaxHealth", BASE_ITEM_HEALTH/2 },
            { "Item_Armour.Stats.BaseHealthRegenerationPerSecond", BASE_ITEM_HEALTH_REGENERATION/2 },

            { "Item_Crossbow.Cost", BASE_ITEM_COST },
            { "Item_Crossbow.Stats.BaseDamage", BASE_ITEM_DAMAGE },

            { "Item_Bolts.Cost", BASE_ITEM_COST },
            { "Item_Bolts.Stats.ProjectileSpeed", BASE_ITEM_PROJECTILE_SPEED/2 },
            { "Item_Bolts.Stats.BaseDamage", BASE_ITEM_DAMAGE/2 },

            { "Item_Binoculars.Cost", BASE_ITEM_COST },
            { "Item_Binoculars.Stats.BaseTargetRange", BASE_ITEM_ATTACK_RANGE },

            { "Item_Gloves.Cost", BASE_ITEM_COST },
            { "Item_Gloves.Stats.AttackSpeed", BASE_ITEM_ATTACK_SPEED },

            { "Item_Pants.Cost", BASE_ITEM_COST },
            { "Item_Pants.Stats.MaxHealth", BASE_ITEM_HEALTH/2 },
            { "Item_Pants.Stats.MovementSpeed", BASE_ITEM_MOVEMENT_SPEED/2 },

            { "Item_Hat.Cost", BASE_ITEM_COST },
            { "Item_Hat.Stats.MaxHealth", BASE_ITEM_HEALTH/2 },
            { "Item_Hat.Stats.BaseTargetRange", BASE_ITEM_ATTACK_RANGE/2 },

            { "Item_EyePiece.Cost", BASE_ITEM_COST*2 },
            { "Item_EyePiece.Stats.BaseTargetRange", BASE_ITEM_ATTACK_RANGE*5 },
            { "Item_EyePiece.Stats.BaseDamage", BASE_ITEM_DAMAGE*3 },
            { "Item_EyePiece.Stats.MaxHealth", -BASE_ITEM_HEALTH*3 },

            { "Item_MedievalArmour.Cost", BASE_ITEM_COST*2 },
            { "Item_MedievalArmour.Stats.MaxHealth", BASE_ITEM_HEALTH*5 },
            { "Item_MedievalArmour.Stats.BaseHealthRegenerationPerSecond", BASE_ITEM_HEALTH_REGENERATION/2 },
            { "Item_MedievalArmour.Stats.MovementSpeed", -BASE_ITEM_MOVEMENT_SPEED*10 },

            { "Item_LightningShoes.Cost", BASE_ITEM_COST*2 },
            { "Item_LightningShoes.Stats.MovementSpeed", BASE_ITEM_MOVEMENT_SPEED*10 },
            { "Item_LightningShoes.Stats.BaseTargetRange", -BASE_ITEM_ATTACK_RANGE*3 },

            { "Item_GoldCrown.Cost", -BASE_ITEM_COST },

            { "Item_DamageAmulet.Cost", BASE_ITEM_COST-BASE_ITEM_COST },
            { "Item_DamageAmulet.Stats.BaseDamage", BASE_ITEM_DAMAGE/2 },

            { "Item_HealthAmulet.Cost", BASE_ITEM_COST-BASE_ITEM_COST },
            { "Item_HealthAmulet.Stats.MaxHealth", BASE_ITEM_HEALTH/2 },

            { "Item_SpeedAmulet.Cost", BASE_ITEM_COST-BASE_ITEM_COST },
            { "Item_SpeedAmulet.Stats.MovementSpeed", BASE_ITEM_MOVEMENT_SPEED },


            { "Wave.End.TimeSeconds", 600 },
            { "Wave.End.SpawnCounts.Enemy_Slime_Basic", 100 },
            { "Wave.End.SpawnCounts.Enemy_Slime_Fast", 15 },
            { "Wave.End.SpawnCounts.Enemy_Slime_MoreDamage", 15 },
            { "Wave.End.SpawnCounts.Enemy_Slime_Tanky", 15 },
            { "Wave.End.SpawnCounts.Enemy_Bat_Basic", 15 },
            { "Wave.End.SpawnCounts.Enemy_Snail_Basic", 15 },
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

        public static bool TryGet<T>(string key, out T value)
        {
            var exists = Data.TryGetValue(key, out var valueObj);
            value = exists ? (T)valueObj : default;
            return exists;
        }
    }
}