using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private CollectablesDictionary m_CollectableItemsDictionary;

    // TODO: Implement the inventory UI
    void Start()
    {
        foreach (var item in m_CollectableItemsDictionary.Keys)
        {
            item.OnItemCollected += OnItemCollected;
        }
    }

    private void OnItemCollected(CollectableItem item)
    {
        InventorySlotUI slot = m_CollectableItemsDictionary[item];
        slot.CollectedItem();
    }
}



