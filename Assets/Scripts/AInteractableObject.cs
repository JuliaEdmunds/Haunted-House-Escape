using UnityEngine;

public abstract class AInteractableObject : MonoBehaviour, IInteractable
{
    public abstract void Interact();

    public abstract void LookAt(GUIConfig guiController);


    // Called when adding the component
    protected virtual void Reset()
    {
        gameObject.layer = LayerMask.NameToLayer("InteractRaycast");
    }
}
