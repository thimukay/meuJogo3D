using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using TMPro;

public class ItemManager : Singleton<ItemManager>
{

   
    public SOInt coins;
    public SOInt potions;
    public TextMeshProUGUI coinNumber;
    public TextMeshProUGUI potionNumber;

    private void Start()
    {
        Reset();
    }


    private void Reset()
    {
        coins.value = 0;
        potions.value = 0;
        SetCoinUI();
    }

    public void AddCoins(int amount = 1)
    {
        coins.value += amount;
        Debug.Log("coins: " + coins.value);
        SetCoinUI();
    }

    public void AddPotion(int amount = 1)
    {
        potions.value += amount;
        Debug.Log("hearts: " + potions.value);
        SetCoinUI();
    }

    public bool UsePotion()
    {
        if (potions.value > 0)
        {
            potions.value--;
            return true;
        }
        else return false;
    }

    public void SetCoinUI()
    {
        //coinNumber.SetText("x "+coins.ToString());
        //UIInGameManager.UpdateTextCoins(coins.value.ToString());
    }
}
