using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    private const string HORIZONTAL_INPUT_NAME = "Horizontal";
    private const string VERTICAL_INPUT_NAME = "Vertical";

    [SerializeField] private float movementSpeed = 2f;

    private CharacterController charController;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
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

        charController.SimpleMove(movement);
    }
}

