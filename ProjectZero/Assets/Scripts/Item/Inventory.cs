using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "GameItem/New Inventory")]
public class Inventory : ScriptableObject
{
    public string inventoryName;
    public Dictionary<string, Item> itemDic = new Dictionary<string, Item>();
    
    public void AddNewItem(Item item)
    {
        if (itemDic.ContainsKey(item.itemName))
        {
            itemDic[item.itemName].Add(item.itemCount);
        }
        else
        {
            itemDic[item.itemName] = item;
        }
    }
    public void RemoveItem(Item item,int count)
    {
        if (!itemDic.ContainsKey(item.itemName))
        {
            return;
        }
        itemDic[item.itemName].itemCount -= count;
        if (itemDic[item.itemName].itemCount <= 0)
        {
            itemDic.Remove(item.itemName);
        }
    }
    public int GetItemCount(Item item)
    {
        if (itemDic.ContainsKey(item.itemName))
        {
            return itemDic[item.itemName].itemCount;
        }

        else
        {
            return 0;
        }
    }
    
}
