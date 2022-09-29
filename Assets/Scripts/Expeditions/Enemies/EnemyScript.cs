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

            CamScript.instance.isLerping = true;
            GetComponent<Collider>().enabled = false;
            //appel delegate mettre le joueur en combat

            //Si le joueur collide on get son groupe Id par getComponent et on dit que si "GroupId == 0" alors la liste d'ennemi est la première
            CombatManager.instance.GroupIdInFight = GetComponentInParent<Ennemy>().groupID;
            CombatManager.instance.m_currentFightingGroup = GetComponentInParent<Ennemy>().m_thisEnemyGroup;
            CombatManager.instance.OnStartCombat(true);
        }
    }
}
