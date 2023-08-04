using UnityEngine;
using System;

public class MoveObjectController : MonoBehaviour 
{
    public event Action OnPlayerEnter;
	public event Action OnPlayerExit;

    public float reachRange = 1.8f;			

	private Animator m_Animator;
	private Camera m_FpsCam;
	private GameObject m_Player;

	private const string ANIM_BOOL_NAME = "isOpen_Obj_";

	private bool m_PlayerEntered;
	// private bool m_ShowInteractMsg;
	// private GUIStyle guiStyle;
	// private string msg;

	private int m_RayLayerMask;

    private GUIConfig m_GuiController;


    void Start()
	{
		//Initialize moveDrawController if script is enabled.
		m_Player = GameObject.FindGameObjectWithTag("Player");

		m_FpsCam = Camera.main;
		if (m_FpsCam == null)	//a reference to Camera is required for rayasts
		{
			throw new UnityException("A Camera tagged 'MainCamera' is missing.");
		}

		//create AnimatorOverrideController to re-use animationController for sliding draws.
		m_Animator = GetComponent<Animator>(); 
		m_Animator.enabled = false;  //disable animation states by default.  

		//the layer used to mask raycast for interactable objects only
		LayerMask iRayLM = LayerMask.NameToLayer("InteractRaycast");
		m_RayLayerMask = 1 << iRayLM.value;  

		//setup GUI style settings for user prompts
		m_GuiController = new GUIConfig();
    }

	void Update()
	{
        if (m_PlayerEntered)
		{
            //center point of viewport in World space.
            Vector3 rayOrigin = m_FpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;

            //if raycast hits a collider on the rayLayerMask
            if (!Physics.Raycast(rayOrigin, m_FpsCam.transform.forward, out hit, reachRange, m_RayLayerMask))
            {
                m_GuiController.ShouldShowtMsg = false;
                return;
            }

            HandleRaycastHit(hit);
		}
	}

    void OnGUI()
    {
        m_GuiController.OnGUI();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == m_Player)       //player has collided with trigger
        {
            m_PlayerEntered = true;
            OnPlayerEnter?.Invoke();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == m_Player)       //player has exited trigger
        {
            m_PlayerEntered = false;

            //hide interact message as player may not have been looking at object when they left
            m_GuiController.ShouldShowtMsg = false;

            OnPlayerExit?.Invoke();
        }
    }

    private void HandleRaycastHit(RaycastHit hit)
    {
        MoveableObject moveableObject = null;

        //is the object of the collider player is looking at the same as me?
        if (!IsEqualToParent(hit.collider, out moveableObject))
        {
            return;
        }

        if (moveableObject == null) //hit object must have MoveableDraw script attached
            return;

        //guiController.ShowInteractMsg(showInteractMsg, isOpen);
        m_GuiController.ShouldShowtMsg = true;
        string animBoolNameNum = ANIM_BOOL_NAME + moveableObject.ObjectNumber.ToString();
        bool isOpen = m_Animator.GetBool(animBoolNameNum); //need current state for message.

        m_GuiController.ShowInteractMsg(isOpen);

        if (Input.GetKeyUp(KeyCode.E) || Input.GetButtonDown("Fire1"))
        {
            m_Animator.enabled = true;
            m_Animator.SetBool(animBoolNameNum, !isOpen);
            m_GuiController.ShowInteractMsg(!isOpen);
        }
    }

    //is current gameObject equal to the gameObject of other.  check its parents
    private bool IsEqualToParent(Collider other, out MoveableObject draw)
    {
        draw = null;
        GameObject currentGO = other.gameObject;
        MoveableObject foundDraw = other.GetComponent<MoveableObject>();

        // Traverse the parents of currentGO
        while (currentGO != null)
        {
            if (currentGO == this.gameObject)
            {
                // If draw was not found in the other object itself, check its parent(s)
                if (foundDraw == null)
                {
                    foundDraw = currentGO.GetComponentInParent<MoveableObject>();
                }

                draw = foundDraw;
                return true;
            }

            currentGO = currentGO.transform.parent?.gameObject;
        }

        return false;
    }
}
