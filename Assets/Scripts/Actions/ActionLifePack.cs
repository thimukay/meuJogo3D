using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Itens;
public class ActionLifePack : MonoBehaviour
{
    public KeyCode keyCode = KeyCode.L;
    public SOInt soInt;

    private void Start()
    {
        soInt = ItemManager.Instance.GetItemByType(ItemType.LIFE_PACK).soInt;
    }


    private void RecoverLife()
    {
        if(soInt.value > 0)
        {
            ItemManager.Instance.RemoveByType(ItemType.LIFE_PACK);
            if(Player.Instance.healthBase.getCurrentLife() != Player.Instance.healthBase.startLife) EffectsManager.Instance.ResetVignette();
            Player.Instance.healthBase.ResetLife();
            
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            RecoverLife();
        }
    }
}
