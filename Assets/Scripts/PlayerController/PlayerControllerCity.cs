using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerControllerCity : MonoBehaviour
{
    [SerializeField] private CharacterController charaController;
    [SerializeField] private float moveSpeed = 1;
    Vector3 moveDirection = Vector3.zero;
    [SerializeField] private Animator m_charaAnim;
    private bool isWalking = false;

    private float inputH;
    private float inputV;

    private void Awake()
    {
        charaController = GetComponent<CharacterController>();
    }
    private void Update()
    {
       
        if (isWalking)
        {
            m_charaAnim.SetBool("isWalking" , true);
        }
        else
        {
            m_charaAnim.SetBool("isWalking", false);
        }
        Debug.Log(m_charaAnim);
    }

    private void FixedUpdate()
    {
        inputV = Input.GetAxis("Vertical");
        inputH = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(inputH, 0f, inputV);
        charaController.SimpleMove(move * moveSpeed);

        if (charaController.velocity.x > 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

    }
}
