using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;
public class BagMenu : MonoBehaviour
{
    public List<Texture> itemTexture;
    public GameObject bag;
    public List<GameObject> itemPrefabs;
    TextMeshProUGUI iCount;
    GhostList ghostList;
    List<string> items = new List<string>();
    //int capsules;
    Item capsule;
    Item glue;
    Item armor;
    Item clock;
    List<Item> testi;
    Inventory inventory;

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
    }
    public void LoadBag()
    {
        capsule = new Item { itemType = Item.ItemType.Capsule, amount = 1 };
        glue = new Item { itemType = Item.ItemType.Glue, amount = 1 };
        armor = new Item { itemType = Item.ItemType.Armor, amount = 1 };
        clock = new Item { itemType = Item.ItemType.Clock, amount = 1 };
        //capsules = 0;
        iCount = itemPrefabs[0].GetComponent<TextMeshProUGUI>();
        foreach (Item item in inventory.GetItemList())
        {
            if (item.itemType == capsule.itemType)
            {
                iCount = itemPrefabs[0].GetComponent<TextMeshProUGUI>();
                iCount.text = item.amount.ToString();
            }

            if (item.itemType == glue.itemType)
            {
                iCount = itemPrefabs[1].GetComponent<TextMeshProUGUI>();
                iCount.text = item.amount.ToString();
            }

            if (item.itemType == armor.itemType)
            {
                iCount = itemPrefabs[2].GetComponent<TextMeshProUGUI>();
                iCount.text = item.amount.ToString();
            }

            if (item.itemType == clock.itemType)
            {
                iCount = itemPrefabs[3].GetComponent<TextMeshProUGUI>();
                iCount.text = item.amount.ToString();
            }
        }
                
        //capsules = Count(inventory.GetItemList(), Capsule);
        //Debug.Log(capsules);
        /*iCount = itemPrefabs[0].GetComponent<TextMeshProUGUI>();
        iCount.text = "" + capsules;*/
    }

    private int Count(List<Item> list, Item NameSearch)
    {
        return list.Count(n => n == NameSearch);
    }

}
