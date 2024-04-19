using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private const string HORIZONTAL_INPUT_NAME = "Horizontal";
    private const string VERTICAL_INPUT_NAME = "Vertical";
    private const string TIME_PASSED_KEY = "TimePassed";
    private const string END_SCENE_NAME = "End";

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
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            StartCoroutine(EndGame());
        }
    }
    
    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(2.5f);

        SceneManager.LoadScene(END_SCENE_NAME);
    }

    private void OnDestroy()
    {
        float timePassed = Time.timeSinceLevelLoad;
        FactDB.SetIntFact(TIME_PASSED_KEY, EOperation.Set, (int)timePassed);
    }
}
