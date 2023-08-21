using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    public void CollectedItem()
    {
        gameObject.SetActive(true);
    }

    public void UseItem()
    {
        gameObject.SetActive(false);
    }
}
