using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sys;

[CreateAssetMenu(fileName ="New Item", menuName = "GameItem/New Item")]
public class Item : ScriptableObject
{
    public string itemName;
    [TextArea]
    public string itemDesc;
    public Sprite itemIcon;
    public Sprite itemSprite;
    public int itemCount;
    public int maxCount = 999;
    public int itemValue = 0;
    public Const.ItemType itemType = Const.ItemType.COMMON;
    public bool isEquipped = false;

    public void Add(int count)
    {
        if (itemCount + count <= maxCount)
        {
            itemCount += count;
        }
        else
        {
            itemCount = maxCount;
            Debug.Log("超过可持有的最大上限!");
        }
    }
    public void Remove(int count)
    {
        itemCount -= count;
    }
}
