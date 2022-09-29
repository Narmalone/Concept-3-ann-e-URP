using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerControllerCity : MonoBehaviour
{
    [SerializeField] private CharacterController charaController;
    [SerializeField] private float moveSpeed = 1;
    Vector3 moveDirection = Vector3.zero;

    private void Awake()
    {
        charaController = GetComponent<CharacterController>();
    }
  
    private void FixedUpdate()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        charaController.SimpleMove(move * moveSpeed);
    }
}
