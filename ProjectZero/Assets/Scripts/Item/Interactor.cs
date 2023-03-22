using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sys;
public class Interactor : MonoBehaviour
{
    public Const.InteractType interactType;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interact(GameObject obj)
    {
        switch(interactType)
        {
            case Const.InteractType.ITEM:
                ItemPicked(obj);
                break;
        }
    }
    private void ItemPicked(GameObject owner) 
    {
        Item item = this.GetComponent<Item>();
        Inventory inventory = owner.GetComponent<Inventory>();
        inventory.AddNewItem(item);
        Destroy(this.gameObject);
    }
    
}
