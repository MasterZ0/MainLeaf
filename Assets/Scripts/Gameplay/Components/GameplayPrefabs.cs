using AdventureGame.BattleSystem;
using AdventureGame.Data;
using AdventureGame.Effects;
using AdventureGame.Items;
using AdventureGame.Items.Data;
using AdventureGame.ObjectPooling;
using AdventureGame.Shared;
using AdventureGame.Shared.ExtensionMethods;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AdventureGame.Gameplay
{
    public enum ParticleVFXType
    {
        RestoreHealth,
        RestoreMana,
        RestoreStamina,
    }

    public class GameplayPrefabs : Singleton<GameplayPrefabs>
    {
        [SerializeField] private DroppedItem itemPrefab;
        [Space]
        [SerializeField] private Dictionary<ParticleVFXType, ParticleVFX> particleVFX;

        private static DroppedItem ItemPrefab => Instance.itemPrefab;

        public static void DropItems(DropChanceData dropValueData, Transform transform)
        {
            DropItems(dropValueData, transform.position, transform.rotation);
        }

        public static void DropItems(DropChanceData dropChance, Vector3 position, Quaternion rotation)
        {
            //SpawnGold(dropValueData.GoldRange, position);
            InstantiateItems(ChanceItems(dropChance.Loot), position, rotation);
        }

        private static List<DroppedItem> InstantiateItems(List<ItemReference> itemsToDrop, Vector3 position, Quaternion rotation)
        {
            List<DroppedItem> instances = new List<DroppedItem>();
            foreach (ItemReference item in itemsToDrop)
            {
                DroppedItem newInstance = ObjectPool.SpawnPooledObject(ItemPrefab, position, rotation);

                newInstance.SetItem(item);
                instances.Add(newInstance);
            }

            return instances;
        }

        private static List<ItemReference> ChanceItems(List<DropChance<DropItem>> loot, float dropMultiplier = 1f)
        {
            List<ItemReference> itemReferences = new List<ItemReference>();

            foreach (DropChance<DropItem> dropChance in loot)
            {
                if (!CalculateDropChance(dropChance.chance * dropMultiplier))
                    continue;

                if (dropChance.drop.item is IQuantifiable)
                {
                    int amountRange = dropChance.drop.amountRange.RandomRange();
                    if (amountRange > 0)
                    {
                        ItemReference newItem = new ItemReference(dropChance.drop.item, amountRange);
                        itemReferences.Add(newItem);
                    }
                }
                else
                {
                    ItemReference newItem = new ItemReference(dropChance.drop.item);
                    itemReferences.Add(newItem);
                }
            }

            return itemReferences;
        }

        //private static void SpawnGold(Vector2Int range, Vector3 position, float goldMultiplier = 1)
        //{
        //    int amount = range.RandomRange();
        //    int newAmount = Mathf.RoundToInt(amount * goldMultiplier);

        //    for (int i = 0; i < newAmount; i++)
        //    {
        //        Coin itemInstance = ObjectPool.SpawnPooledObject(coin, position);
        //        itemInstance.AddRandomVelocity();
        //    }
        //}

        /// 0 can't be > 0, but 100 is > 99
        private static bool CalculateDropChance(float chance) => chance > Random.Range(0, 100);

        public static void RestoreFX(AttributePoint attribute, Transform center)
        {
            ParticleVFXType vfxType = attribute switch
            {
                AttributePoint.HealthPoint => ParticleVFXType.RestoreHealth,
                AttributePoint.ManaPoint => ParticleVFXType.RestoreMana,
                AttributePoint.StaminaPoint => ParticleVFXType.RestoreStamina,
                _ => throw new System.NotImplementedException(),
            };

            ParticleVFX(vfxType, center);
        }

        public static void ParticleVFX(ParticleVFXType vfxType, Transform center)
        {
            Instance.particleVFX[vfxType].SpawnPooledObject(center.position, center.rotation, center);
        }
    }
}