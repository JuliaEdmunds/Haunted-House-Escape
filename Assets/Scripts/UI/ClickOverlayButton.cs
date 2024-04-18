using UnityEngine.Events;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickOverlayButton : MonoBehaviour
{
    public UnityEvent OnClick;
    private BoxCollider m_BoxCollider;

    private void Start()
    {
        m_BoxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (!isActiveAndEnabled)
        {
            return;
        }
        
        // Check if mouse down or the "E" key is pressed and the UI button is focused
        if ((Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.E)) && IsInteractingWithButton())
        {
            OnClick?.Invoke();
        }
    }

    private bool IsInteractingWithButton()
    {
        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
        return hit.collider == m_BoxCollider;
    }
}
