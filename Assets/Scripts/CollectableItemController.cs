using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class CollectableItemController : AInteractableObjectController<CollectableItem>
{
    public event Action<CollectableItem> OnItemCollected;

    private void OnGUI()
    {
        m_GuiController.OnGUI();
    }

    protected override void OnLookingAtTarget(CollectableItem target)
    {
        m_GuiController.ShouldShowMsg = true;

        if (target.Locked)
        {
            m_GuiController.ShowInteractMsg("It's locked. You need to find a way to unlock it first.");
        }
        else
        {
            m_GuiController.ShowInteractMsg("Press E/Fire1 to pick up");
        }
    }

    protected override void OnInteractedWithTarget(CollectableItem target)
    {
        if (target.Locked)
        {
            return;
        }

        // TODO: Implement picking up item, showing it in the inventory and updating the FactsDB
        FactDB.SetIntFact(target.ItemType.ToString(), EOperation.Add, 1);
        OnItemCollected?.Invoke(target);
        target.gameObject.SetActive(false);
    }
}
