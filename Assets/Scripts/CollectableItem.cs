using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CollectableItem : MonoBehaviour
{
    public bool IsCollected { get; private set; }

    public event Action<CollectableItem> OnItemCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !IsCollected)
        {
            OnItemCollected?.Invoke(this);
            IsCollected = true;
        }
    }
}
