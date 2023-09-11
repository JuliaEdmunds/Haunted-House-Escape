using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private const string HORIZONTAL_INPUT_NAME = "Horizontal";
    private const string VERTICAL_INPUT_NAME = "Vertical";
    private const string TIME_PASSED_KEY = "TimePassed";

    [SerializeField, Range(2f, 4f)] private float movementSpeed;

    private CharacterController m_CharController;

    private void Awake()
    {
        m_CharController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        float vertInput = Input.GetAxis(VERTICAL_INPUT_NAME) * movementSpeed;
        float horizInput = Input.GetAxis(HORIZONTAL_INPUT_NAME) * movementSpeed;

        Vector3 movement = (transform.forward * vertInput) + (transform.right * horizInput);

        m_CharController.SimpleMove(movement);
    }

    private void OnDestroy()
    {
        float timePassed = Time.timeSinceLevelLoad;
        FactDB.SetIntFact(TIME_PASSED_KEY, EOperation.Set, (int)timePassed);
    }
}
