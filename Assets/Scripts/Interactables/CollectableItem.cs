using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CollectableItem : AInteractableObject
{
    public static event Action<CollectableItem> OnItemCollected;

    public static event Action<CollectableItem> OnItemUsed;

    [SerializeField] private ECollectable m_ItemType;
    public ECollectable ItemType => m_ItemType;

    private const string PICKUP_MESSAGE = "Press E/Left mouse button to Pick up";

    public override void Interact()
    {
        FactDB.SetIntFact(ItemType.ToString(), EOperation.Add, 1);
        OnItemCollected?.Invoke(this);
        gameObject.SetActive(false);
    }

    public override void LookAt(GUIConfig guiController)
    {
        guiController.ShouldShowMsg = true;
        guiController.ShowInteractMsg(PICKUP_MESSAGE);
    }

    public void UseItem()
    {
        OnItemUsed?.Invoke(this);
    }
}
