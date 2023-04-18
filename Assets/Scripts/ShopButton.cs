using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButton : MonoBehaviour
{
    public string itemName;

    public void Click()
    {
        if((GameController.instance.currentGold - GameController.instance.GetItemDetails(itemName).value) < 0){return;}
        GameController.instance.currentGold -= GameController.instance.GetItemDetails(itemName).value;
        GameController.instance.AddItem(itemName);
    }
    
    public void Sell()
    {
        if(GameController.instance.itemsHeld[GameController.instance.itemIcon.currentItem - 1] == ""){Debug.Log("You're not holding anything"); return;}
        GameController.instance.currentGold += GameController.instance.GetItemDetails(GameController.instance.itemsHeld[GameController.instance.itemIcon.currentItem - 1]).value;
        GameController.instance.RemoveItem(GameController.instance.itemIcon.currentItem - 1);
    }
}