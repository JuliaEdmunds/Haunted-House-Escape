using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private CollectablesDictionary m_CollectableItemsDictionary = new();

    private void Awake()
    {
        foreach (CollectableItem item in m_CollectableItemsDictionary.Keys)
        {
            CollectableItemController collectableItemController = item.GetComponent<CollectableItemController>();
            collectableItemController.OnItemCollected += OnItemCollected;
        }
    }

    // TODO: Implement the inventory UI

    private void OnItemCollected(CollectableItem item)
    {
        InventorySlotUI slot = m_CollectableItemsDictionary[item];
        slot.CollectedItem();
    }
}



