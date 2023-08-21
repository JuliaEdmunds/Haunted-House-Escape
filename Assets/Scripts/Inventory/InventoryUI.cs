using System;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private CollectablesDictionary m_CollectableItemsDictionary = new();

    private void Awake()
    {
        CollectableItem.OnItemCollected += OnItemCollected;
        CollectableItem.OnItemUsed += OnItemUsed;
    }

    // TODO: Implement the inventory UI

    private void OnItemCollected(CollectableItem item)
    {
        InventorySlotUI slot = m_CollectableItemsDictionary[item];
        slot.CollectedItem();
    }

    private void OnItemUsed(CollectableItem item)
    {
        InventorySlotUI slot = m_CollectableItemsDictionary[item];
        slot.UseItem();
    }
}



