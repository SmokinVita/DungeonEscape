using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    //Variable for currentItemSelected
    private int selectedItem;
    private int selectedItemCost;
    private Player _player;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _player = other.GetComponent<Player>();
            UIManager.Instance.OpenShop();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.CloseShop();
        }
    }

    public void SelectItem(int item)
    {
        Debug.Log("SelectItem()");
        switch(item)
        {
            case 0: //flame sword
                UIManager.Instance.UpdateSelection(87.693f);
                selectedItemCost = 200;
                break;
            case 1: //Boots of flight
                UIManager.Instance.UpdateSelection(-28.022f);
                selectedItemCost = 400;
                break;
            case 2://key
                UIManager.Instance.UpdateSelection(-128.5f);
                selectedItemCost = 100;
                break; 
        }
        selectedItem = item;
    }

    //BuyItemMethod
    //check if player gems is greater than or equal to item cost.
    //if it is, then awarditem(subtract cost from player gems.)
    //else cancle sale.
    public void BuyItem()
    {
        if (_player.CheckDiamonAmount() >= selectedItemCost)
        {
            //awardplayer
            switch(selectedItem)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    GameManager.instance.HasKeyToCastle = true;
                    break;
            }
            _player.SubtractDiamonds(selectedItemCost);
        }
        else
        {
            Debug.Log("Not Enough Gems");
            UIManager.Instance.CloseShop();
        }
    }
}
