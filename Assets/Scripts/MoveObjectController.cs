using UnityEngine;
using System;
using Unity.VisualScripting.Antlr3.Runtime;
using static UnityEngine.GraphicsBuffer;

public class MoveObjectController : AInteractableObjectController<MoveableObject>
{
    private const string ANIM_BOOL_NAME = "isOpen_Obj_";

    private Animator m_Animator;
    private bool m_IsOpen;

    protected override void Start()
    {
        base.Start();

        //create AnimatorOverrideController to re-use animationController for sliding draws.
        m_Animator = GetComponent<Animator>();
        m_Animator.enabled = false;  //disable animation states by default.  
    }

    void OnGUI()
    {
        m_GuiController.OnGUI();
    }

    protected override void OnLookingAtTarget(MoveableObject target)
    {
        m_GuiController.ShouldShowMsg = true;
        string animBoolNameNum = ANIM_BOOL_NAME + target.ObjectNumber.ToString();
        m_IsOpen = m_Animator.GetBool(animBoolNameNum); //need current state for message.

        if (target.Locked)
        {
            m_GuiController.ShowInteractMsg("It's locked. You need to find a way to unlock it first.");
        }
        else
        {
            m_GuiController.ShowInteractMsg(m_IsOpen);
        }
    }

    protected override void OnInteractedWithTarget(MoveableObject target)
    {
        m_GuiController.ShouldShowMsg = true;
        string animBoolNameNum = ANIM_BOOL_NAME + target.ObjectNumber.ToString();
        bool isOpen = m_Animator.GetBool(animBoolNameNum); //need current state for message.

        m_Animator.enabled = true;
        m_Animator.SetBool(animBoolNameNum, !isOpen);
        m_GuiController.ShowInteractMsg(!isOpen);
    }
}
