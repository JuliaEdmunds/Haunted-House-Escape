using UnityEngine;

public class MoveableObject : AInteractableObject
{
    [SerializeField] private int m_ObjectNumber;
    public int ObjectNumber => m_ObjectNumber;

    public bool Locked;
}