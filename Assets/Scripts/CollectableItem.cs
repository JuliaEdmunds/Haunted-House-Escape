using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CollectableItem : AInteractableObject
{
    [SerializeField] private ECollectable m_ItemType;
    public ECollectable ItemType => m_ItemType;

    [SerializeField] private bool m_IsLocked;
    public bool Locked => m_IsLocked;
}
