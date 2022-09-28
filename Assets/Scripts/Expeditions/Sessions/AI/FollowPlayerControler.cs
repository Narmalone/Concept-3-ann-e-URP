using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class FollowPlayerControler : MonoBehaviour
{
    [SerializeField] private Transform m_pointToFollow;
    [SerializeField] private float agentSpeed = 1f;
    private void Update()
    {
        //float distance = Vector3.Distance(m_pointToFollow.position, transform.position);

        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_pointToFollow.position, agentSpeed * Time.deltaTime);
    }
}
