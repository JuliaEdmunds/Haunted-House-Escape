using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CollectableItem : AInteractableObject
{
    [SerializeField] private ECollectable m_ItemType;

    // TODO: Lots of repetitive code with MoveObjectController.  Refactor to a base class.
       
}
