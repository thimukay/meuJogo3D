using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using TMPro;

namespace Itens
{
    public enum ItemType
    {
        COIN,
        LIFE_PACK
    }

    public class ItemManager : Singleton<ItemManager>
    {

        public List<ItemSetup> itemSetup;
   
 

        private void Start()
        {
            Reset();
            LoadItemsFromSave();
        }

        public void LoadItemsFromSave()
        {
            AddByType(ItemType.COIN, (int)SaveManager.Instance.Setup.coins);
            AddByType(ItemType.LIFE_PACK, (int)SaveManager.Instance.Setup.potion);
        }
        private void Reset()
        {
            foreach(var i in itemSetup)
            {
                i.soInt.value = 0;
            }
        }

        public ItemSetup GetItemByType(ItemType itemType)
        {
            return itemSetup.Find(i => i.itemType == itemType);
        }
        public void AddByType(ItemType itemType, int amount = 1)
        {
            if (amount < 0) return;
            itemSetup.Find(i => i.itemType == itemType).soInt.value += amount;
        }

        public void RemoveByType(ItemType itemType, int amount = 1)
        {
            var item = itemSetup.Find(i => i.itemType == itemType);
            item.soInt.value -= amount;

            if (item.soInt.value < 0) item.soInt.value = 0;
        }


         /*public bool UsePotion()
         {
            if (potions.value > 0)
            {
                potions.value--;
                return true;
            }
            else return false;
         }*/

        public void SetCoinUI()
        {
            //coinNumber.SetText("x "+coins.ToString());
            //UIInGameManager.UpdateTextCoins(coins.value.ToString());
        }
    }

    [System.Serializable]
    public class ItemSetup
    {
        public ItemType itemType;
        public SOInt soInt;
        public Sprite icon;
    }
}
