using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CollectableItem : AInteractableObject
{
    public static event Action<CollectableItem> OnItemCollected;

    [SerializeField] private ECollectable m_ItemType;
    public ECollectable ItemType => m_ItemType;

    [SerializeField] private bool m_IsLocked;
    public bool Locked => m_IsLocked;

    public override void Interact()
    {
        if (Locked)
        {
            return;
        }

        // TODO: Implement picking up item, showing it in the inventory and updating the FactsDB
        FactDB.SetIntFact(ItemType.ToString(), EOperation.Add, 1);
        OnItemCollected?.Invoke(this);
        gameObject.SetActive(false);
    }

    public override void LookAt(GUIConfig guiController)
    {
        guiController.ShouldShowMsg = true;

        if (Locked)
        {
            guiController.ShowInteractMsg("It's locked. You need to find a way to unlock it first.");
        }
        else
        {
            guiController.ShowInteractMsg("Press E/Fire1 to pick up");
        }
    }
}
