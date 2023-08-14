using UnityEngine.Events;
using UnityEngine;

public class ClickOverlayButton : MonoBehaviour
{
    public UnityEvent OnClick;


    public void OnMouseDown()
    {
        OnClick?.Invoke();
    }
}
