using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class EnemyScript: MonoBehaviour
{
    [SerializeField] private LayerMask charatersMask;
    private void OnEnable()
    {
        GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if((charatersMask.value & (1 << other.gameObject.layer)) > 0)
        {
            GetComponent<Collider>().enabled = false;
            CombatManager.instance.GroupIdInFight = GetComponentInParent<Ennemy>().groupID;
            CombatManager.instance.m_currentFightingGroup = GetComponentInParent<Ennemy>().m_thisEnemyGroup;
            CombatManager.instance.OnStartCombat(true);
        }
    }
}
