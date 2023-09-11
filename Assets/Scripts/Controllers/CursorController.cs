using UnityEngine;

public class CursorController : MonoBehaviour
{
    private const float MAX_X_AXIS_ANGLE = 90.0f;
    private const string MOUSE_X_INPUT_NAME = "Mouse X";
    private const string MOUSE_Y_INPUT_NAME = "Mouse Y";

    [SerializeField] private float m_MouseSensitivity;
    [SerializeField] private Transform m_PlayerBody;

    private float m_XAxisClamp;
    private bool m_CursorIsLocked = true;

    private void Awake()
    {
        LockCursor();
        m_XAxisClamp = 0.0f;
        transform.localEulerAngles = Vector3.zero;
    }

    private void LockCursor()
    {
        if (Input.GetKeyUp(KeyCode.Escape) || Input.GetMouseButtonUp(0))
        {
            ToggleCursorLock();
        }

        Cursor.lockState = m_CursorIsLocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !m_CursorIsLocked;
    }

    private void ToggleCursorLock()
    {
        m_CursorIsLocked = !m_CursorIsLocked;
    }

    private void Update()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
        float mouseX = GetMouseInput(MOUSE_X_INPUT_NAME);
        float mouseY = GetMouseInput(MOUSE_Y_INPUT_NAME);

        m_XAxisClamp -= mouseY;

        // Allow the player to look up to 90 degrees but not look downward beyond the horizontal plane
        m_XAxisClamp = Mathf.Clamp(m_XAxisClamp, -MAX_X_AXIS_ANGLE, MAX_X_AXIS_ANGLE);

        transform.rotation = Quaternion.Euler(m_XAxisClamp, transform.eulerAngles.y, 0.0f);
        m_PlayerBody.Rotate(Vector3.up * mouseX);
    }

    private float GetMouseInput(string axisName)
    {
        return Input.GetAxis(axisName) * m_MouseSensitivity * Time.deltaTime;
    }
}
