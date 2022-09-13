using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class FollowPlayerControler : MonoBehaviour
{
    [SerializeField] private GameObject m_playerPosition;
    [SerializeField] private float agentSpeed = 1f;
    private void Update()
    {
        float distance = Vector3.Distance(m_playerPosition.transform.position, transform.position);

        if(distance >= 1.5f)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_playerPosition.transform.position, agentSpeed * Time.deltaTime);
        }
    }
}
