using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private CollectablesDictionary m_CollectableItemsDictionary = new();

    private void Awake()
    {
        CollectableItem.OnItemCollected += OnItemCollected;
    }

    // TODO: Implement the inventory UI

    private void OnItemCollected(CollectableItem item)
    {
        InventorySlotUI slot = m_CollectableItemsDictionary[item];
        slot.CollectedItem();
    }
}



