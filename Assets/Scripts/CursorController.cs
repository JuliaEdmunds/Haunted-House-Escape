using UnityEngine;

public class CursorController : MonoBehaviour
{
    private const float MAX_X_AXIS_ANGLE = 90.0f;
    private const string MOUSE_X_INPUT_NAME = "Mouse X";
    private const string MOUSE_Y_INPUT_NAME = "Mouse Y";

    [SerializeField] private float mouseSensitivity = 300f;
    [SerializeField] private Transform playerBody;

    private float xAxisClamp;
    private bool m_cursorIsLocked = true;

    private void Awake()
    {
        LockCursor();
        xAxisClamp = 0.0f;
        transform.localEulerAngles = Vector3.zero;
    }

    private void LockCursor()
    {
        if (Input.GetKeyUp(KeyCode.Escape) || Input.GetMouseButtonUp(0))
        {
            ToggleCursorLock();
        }

        Cursor.lockState = m_cursorIsLocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !m_cursorIsLocked;
    }

    private void ToggleCursorLock()
    {
        m_cursorIsLocked = !m_cursorIsLocked;
    }

    private void Update()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
        float mouseX = GetMouseInput(MOUSE_X_INPUT_NAME);
        float mouseY = GetMouseInput(MOUSE_Y_INPUT_NAME);

        xAxisClamp -= mouseY;

        // allow the player to look up to 90 degrees but not look downward beyond the horizontal plane
        xAxisClamp = Mathf.Clamp(xAxisClamp, -MAX_X_AXIS_ANGLE, MAX_X_AXIS_ANGLE);

        transform.rotation = Quaternion.Euler(xAxisClamp, transform.eulerAngles.y, 0.0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private float GetMouseInput(string axisName)
    {
        return Input.GetAxis(axisName) * mouseSensitivity * Time.deltaTime;
    }
}
