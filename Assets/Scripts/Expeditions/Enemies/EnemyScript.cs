using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class EnemyScript: MonoBehaviour
{
    //TO DO: Un script qui contient les propriétés basique d'une IA
    //Comme les personnages, leurs statistiques sont relativement aléatoires de bas niveau
    //nom, vie -> vie visible en combat par un slider/progressbar

    //Un autre script permettant de créer des groupes d'Ia directement dans le level afin de tester
    //Ce script contiendras des informations comme nombre d'ennemis dans un groupe

    //trouver un moyen d'afficher les données dans l'ui de combat avec les noms des adversaires et de nos personnages
    //sur des slots bien définies
    //Pas sur une nouvelle scene mais le playercontroller devra être désactivé

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

            //appel delegate mettre le joueur en combat
            CombatManager.instance.OnStartCombat(true);
        }
    }
}
