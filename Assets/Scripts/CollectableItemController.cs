using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class CollectableItemController : AInteractableObjectController<CollectableItem>
{
    // TODO: Implement this class

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

        // TODO: Implement picking up item, moving to invenotry and updating the FactsDB
    }
}
