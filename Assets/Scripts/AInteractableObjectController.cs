using System;
using UnityEngine;

public abstract class AInteractableObjectController<TInteractableObject> : MonoBehaviour where TInteractableObject : AInteractableObject
{
    // Holds generic code for interacting with objects

    public float reachRange = 1.8f;

    private Camera m_FpsCam;
    private GameObject m_Player;

    private bool m_PlayerEntered;

    private int m_RayLayerMask;

    protected GUIConfig m_GuiController;

    public event Action<TInteractableObject> OnLookedAt;
    public event Action<TInteractableObject> OnInteracted;

    protected virtual void Start()
    {
        //Initialize moveDrawController if script is enabled.
        m_Player = GameObject.FindGameObjectWithTag("Player");

        m_FpsCam = Camera.main;
        if (m_FpsCam == null)   //a reference to Camera is required for rayasts
        {
            throw new UnityException("A Camera tagged 'MainCamera' is missing.");
        }

        //the layer used to mask raycast for interactable objects only
        LayerMask iRayLM = LayerMask.NameToLayer("InteractRaycast");
        m_RayLayerMask = 1 << iRayLM.value;

        //setup GUI style settings for user prompts
        m_GuiController = new GUIConfig();
    }

    private void Update()
    {
        if (m_PlayerEntered)
        {
            //center point of viewport in World space.
            Vector3 rayOrigin = m_FpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));

            //if raycast hits a collider on the rayLayerMask
            if (!Physics.Raycast(rayOrigin, m_FpsCam.transform.forward, out RaycastHit hit, reachRange, m_RayLayerMask))
            {
                //m_GuiController.ShouldShowMsg = false;
                return;
            }

            HandleRaycastHit(hit);
        }
    }

    private void HandleRaycastHit(RaycastHit hit)
    {
        //is the object of the collider player is looking at the same as me?
        if (!IsEqualToParent(hit.collider, out TInteractableObject targetObject))
            return;

        if (targetObject == null) //hit object must inherit from AIteractableObject
            return;

        // TODO: Move GUI controller to specific objects
        // m_GuiController.ShouldShowMsg = true;

        OnLookingAtTarget(targetObject);
        OnLookedAt?.Invoke(targetObject);

        if (Input.GetKeyUp(KeyCode.E) || Input.GetButtonDown("Fire1"))
        {
            OnInteractedWithTarget(targetObject);
            OnInteracted?.Invoke(targetObject);
        }
    }

    //is current gameObject equal to the gameObject of other.  check its parents
    private bool IsEqualToParent(Collider other, out TInteractableObject draw)
    {
        draw = null;
        GameObject currentGO = other.gameObject;
        TInteractableObject foundDraw = other.GetComponent<TInteractableObject>();

        // Traverse the parents of currentGO
        while (currentGO != null)
        {
            if (currentGO == this.gameObject)
            {
                // If draw was not found in the other object itself, check its parent(s)
                if (foundDraw == null)
                {
                    foundDraw = currentGO.GetComponentInParent<TInteractableObject>();
                }

                draw = foundDraw;
                return true;
            }

            currentGO = currentGO.transform.parent?.gameObject;
        }

        return false;
    }

    protected abstract void OnLookingAtTarget(TInteractableObject target);

    protected abstract void OnInteractedWithTarget(TInteractableObject target);

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == m_Player)       //player has collided with trigger
        {
            m_PlayerEntered = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == m_Player)       //player has exited trigger
        {
            m_PlayerEntered = false;

            //hide interact message as player may not have been looking at object when they left
            // m_GuiController.ShouldShowMsg = false;
        }
    }
}
