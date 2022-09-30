using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerControllerCity : MonoBehaviour
{
    public static PlayerControllerCity Instance { get; private set; }
    [SerializeField] private CharacterController charaController;
    [SerializeField] private float moveSpeed = 1;
    Vector3 moveDirection = Vector3.zero;
    [SerializeField] private Animator m_charaAnim;
    private bool isWalking = false;

    private float inputH;
    private float inputV;

    private void Awake()
    {
        Instance = this;
        charaController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        if (CombatManager.instance.isFighting == false) 
        {
            if (isWalking)
            {
                m_charaAnim.SetBool("isWalking", true);
            }
            else
            {
                m_charaAnim.SetBool("isWalking", false);
            }
        }
        else
        {
            m_charaAnim.SetBool("isWalking", false);
        }
      
        Debug.Log(m_charaAnim);
    }

    public void StopAnim()
    {
        m_charaAnim.SetBool("isWalking", false);
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
