using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSender : MonoBehaviour
{
    [SerializeField] private Event m_events;

    private void OnTriggerEnter(Collider other)
    {
        m_events?.VectorEvent(other.transform.position);
        Debug.Log(other.gameObject.name);
    }
}
