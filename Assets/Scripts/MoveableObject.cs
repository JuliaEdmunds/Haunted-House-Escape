using UnityEngine;

public class MoveableObject : AInteractableObject
{
    [SerializeField] private int m_ObjectNumber;
    public int ObjectNumber => m_ObjectNumber;

    [SerializeField] private bool m_IsLocked;
    public bool Locked => m_IsLocked;
}