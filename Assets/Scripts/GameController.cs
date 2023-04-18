using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    FreeRoam,
    Dialog,
    Shop
}

public class GameController : MonoBehaviour
{
    public string[] itemsHeld;
    public Item[] referenceItems;

    public ItemIcon itemIcon;
    public string SelectedItem;
    public Item activeItem;

    public Text goldText;
    public int currentGold;

    [SerializeField] PlayerControls playerControls;
    public static GameController instance;

    GameState state;

    private void Start()
    {
        instance = this;

        DialogManager.Instance.OnShowDialog += () =>
        {
            state = GameState.Dialog;
        };
        DialogManager.Instance.OnHideDialog += () =>
        {
            if (state == GameState.Dialog)
            state = GameState.FreeRoam;
        };

        ShopManager.Instance.OnShowShop += () =>
        {
            state = GameState.Shop;
        };
        ShopManager.Instance.OnHideShop += () =>
        {
            if (state == GameState.Shop)
            state = GameState.FreeRoam;
        };
    }

    private void Update()
    {
        if (state == GameState.FreeRoam)
        {
            playerControls.HandleUpdate();
        }
        else if (state == GameState.Dialog)
        {
            DialogManager.Instance.HandleUpdate();
        }
        else if (state == GameState.Shop)
        {
            ShopManager.Instance.HandleUpdate();
        }

        if(Input.GetKeyDown(KeyCode.Equals))
        {
            if(itemIcon.currentItem >= itemsHeld.Length){return;}
            itemIcon.currentItem++;
        }
        if(Input.GetKeyDown(KeyCode.Minus))
        {
            if(itemIcon.currentItem <= 1){return;}
            itemIcon.currentItem--;
        }

        if(Input.GetKeyDown(KeyCode.J))
        {
           AddItem("Rock Hat");
           AddItem("Bruh Hat");
        }

        if(Input.GetKeyDown(KeyCode.K))
        {
           RemoveItem(itemIcon.currentItem - 1);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    goldText.text = currentGold.ToString();

    ShowItems();
    }

    

    public Item GetItemDetails(string itemToGrab)
    {
        for (int i = 0; i < referenceItems.Length; i++)
        {
            if(referenceItems[i].itemName == itemToGrab)
            {
                return referenceItems[i];
            }
        }

        return null;
    }

    public void ShowItems()
    {
        for (int i = 0; i < itemIcon.currentItem; i++)
        {
            if(itemsHeld[i] != "")
            {
                itemIcon.iconImage.enabled = true;
                itemIcon.iconImage.sprite = GetItemDetails(itemsHeld[i]).itemSprite;
            }
            else
            {
                itemIcon.iconImage.enabled = false;
            }
        }
    }

    public void SelectItem(Item newItem)
    {
        activeItem = newItem;
    }

    public void AddItem(string itemToAdd)
    {
        int newItemPos = 0;
        bool foundEmpty = false;

        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if(itemsHeld[i] == "")
            {
                newItemPos = i;
                i = itemsHeld.Length;
                foundEmpty = true;
            }
        }

        if(foundEmpty)
        {
            bool itemExists = false;

            for (int i = 0; i < referenceItems.Length; i++)
            {
                if(referenceItems[i].itemName == itemToAdd)
                {
                    itemExists = true;

                    i = referenceItems.Length;
                }
            }

            if(itemExists)
            {
                itemsHeld[newItemPos] = itemToAdd;
            }
            else
            {
                Debug.LogError(itemToAdd + " does not exist");
            }
        }

        itemIcon.currentItem = newItemPos + 1;
    }

    public void RemoveItem(int itemToRemove)
    {
        bool foundItem = false;
        int itemPos = 0;

        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if(i == itemToRemove)
            {
                foundItem = true;
                itemPos = i;

                i = itemsHeld.Length;
            }
        }

        if(itemsHeld[itemToRemove] == PlayerControls.instance.equippedHat)
        {
            PlayerControls.instance.hat.GetComponent<SpriteRenderer>().enabled = false;
        }

        if(foundItem)
        {
            itemsHeld[itemPos] = "";
        }
        else
        {
            Debug.LogError("Couldn't find " + itemToRemove);
        }
    }
}