using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour
{
    public int currentItem = 0;
    public Image iconImage;

    public void Press()
    {
        if(GameController.instance.itemsHeld[currentItem] != "")
        {
            GameController.instance.SelectItem(GameController.instance.GetItemDetails(GameController.instance.itemsHeld[currentItem]));
        }
    }
}