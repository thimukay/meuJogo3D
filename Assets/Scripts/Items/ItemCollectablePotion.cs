using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Itens;

public class ItemCollectablePotion : ItemCollectableBase
{
    protected override void OnCollect()
    {
        base.OnCollect();
        ItemManager.Instance.AddByType(ItemType.LIFE_PACK);
    }
}
