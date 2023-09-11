using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : AInteractableObject
{
    private const string ANIM_BOOL_NAME_PREFIX = "isOpen_Obj_";
    private int m_AnimHash;

    public static event Action<MoveableObject> OnObjectUnlocked;
    public static event Action<MoveableObject> OnObjectAnimationComplete;

    [SerializeField] private Animator m_Animator;
    private bool m_IsOpen;

    [SerializeField] private int m_ObjectNumber;
    public int ObjectNumber => m_ObjectNumber;

    [SerializeField] private bool m_IsLocked;
    public bool Locked => m_IsLocked;

    [SerializeField] private string m_UnlockKey;
    [SerializeField] private List<CollectableItem> m_ItemsToUnlock;

    private void Start()
    {
        // Set up animator hash and disable animation states by default
        m_AnimHash = Animator.StringToHash(ANIM_BOOL_NAME_PREFIX + ObjectNumber);
        m_Animator.enabled = false;  
    }

    public override void Interact()
    {
#if UNITY_EDITOR
        // If run in Unity Editor allow for the cheat locked door opening for faster testing
        if (Locked && Input.GetKey(KeyCode.LeftControl) && Input.GetButtonDown("Fire1"))
        {
            Unlock();
        }
#endif

        if (Locked)
        {
            return;
        }

        // Need current state for message
        bool isOpen = m_Animator.GetBool(m_AnimHash); 

        m_Animator.enabled = true;
        m_Animator.SetBool(m_AnimHash, !isOpen);

        OnObjectAnimationComplete?.Invoke(this);
    }

    public override void LookAt(GUIConfig guiController)
    {
        guiController.ShouldShowMsg = true;

        // Need current state for message
        m_IsOpen = m_Animator.GetBool(m_AnimHash); 

        if (Locked)
        {
            guiController.ShowInteractMsg("It's locked. You need to find a way to unlock it first.");
        }
        else
        {
            guiController.ShowInteractMsg(m_IsOpen);
        }
    }

    public void Unlock()
    {
        if (Locked)
        {
            m_IsLocked = false;

            if (m_ItemsToUnlock.Count != 0)
            {
                for (int i = 0; i < m_ItemsToUnlock.Count; i++)
                {
                    CollectableItem itemToUnlock = m_ItemsToUnlock[i];
                    itemToUnlock.UseItem();
                }
            }

            OnObjectUnlocked?.Invoke(this);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(TagManager.PLAYER_TAG))
        {
            if (Locked && FactDB.GetBoolFact(m_UnlockKey))
            {
                Unlock();
            }
        }
    }

    protected override void Reset()
    {
        base.Reset();
        if (m_Animator == null)
        {
            m_Animator = GetComponentInParent<Animator>();
        }
    }
}
