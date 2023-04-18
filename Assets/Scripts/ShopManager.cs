using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] GameObject shopMenu;

    public event Action OnShowShop;
    public event Action OnHideShop;

    public static ShopManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator ShowShopMenu()
    {
        yield return new WaitForEndOfFrame();
        
        OnShowShop?.Invoke();
        shopMenu.SetActive(true);
    }

    public void HandleUpdate()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            OnHideShop?.Invoke();
            shopMenu.SetActive(false);
                
        }
    }
}
