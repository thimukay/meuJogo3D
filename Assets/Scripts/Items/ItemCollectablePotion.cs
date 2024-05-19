using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectablePotion : ItemCollectableBase
{
    protected override void OnCollect()
    {
        base.OnCollect();
        ItemManager.Instance.AddPotion();
    }
}
