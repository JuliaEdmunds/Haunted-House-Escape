using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    [SerializeField] private int m_ObjectNumber;
    public int ObjectNumber => m_ObjectNumber;

    public bool Locked;
}