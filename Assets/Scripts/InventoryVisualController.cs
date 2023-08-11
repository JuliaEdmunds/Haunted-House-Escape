using UnityEngine;

public class InventoryVisualController : MonoBehaviour
{
    [SerializeField] private InventoryUI m_InventoryUI;

    private void Start()
    {
        m_InventoryUI.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleInventoryScreen();
        }
    }

    private void ToggleInventoryScreen()
    {
        bool isVisible = m_InventoryUI.gameObject.activeSelf;
        m_InventoryUI.gameObject.SetActive(!isVisible);
    }
}

