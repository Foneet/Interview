using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, Interface_interact
{
    public string itemName;
    public int value;
    public Sprite itemSprite;

    public void Interact()
    {
        GameController.instance.AddItem(itemName);
        Destroy(gameObject);
    }
}