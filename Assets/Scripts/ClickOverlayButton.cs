using UnityEngine.Events;
using UnityEngine;
using Unity.VisualScripting;

public class ClickOverlayButton : MonoBehaviour
{
    public UnityEvent OnClick;

    public bool IsEnabled;

    private void Start()
    {
        IsEnabled = true;
    }


    public void OnMouseDown()
    {
        if (!IsEnabled)
        {
            return;
        }

        OnClick?.Invoke();
    }
}
