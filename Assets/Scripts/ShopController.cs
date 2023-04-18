using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour, Interface_interact
{
    public void Interact()
    {
        StartCoroutine(ShopManager.Instance.ShowShopMenu());
    }
}
