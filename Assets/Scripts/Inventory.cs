using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private CollectablesDictionary m_CollectableItemsDictionary = new();

    // TODO: Implement the inventory UI
    void Start()
    {
        
    }

    private void OnItemCollected(CollectableItem item)
    {
        InventorySlotUI slot = m_CollectableItemsDictionary[item];
        slot.CollectedItem();
    }

    public void AddItem(CollectableItem item)
    {

    }

    public void ToggleInventory()
    {

    }
}



