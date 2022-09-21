using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerControllerCity : MonoBehaviour
{
    [SerializeField] private CharacterController charaController;
    [SerializeField] private float moveSpeed;
    public InputAction playerControls;
    Vector2 moveDirection = Vector2.zero;

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        moveDirection = playerControls.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        charaController.SimpleMove(new Vector2(moveDirection.x * moveSpeed, 0f));  
    }
}
