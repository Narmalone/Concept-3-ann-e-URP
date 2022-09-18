using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public static PlayerAttack instance { get; private set; }
    [SerializeField] private LayerMask m_enemyMask;

    //Savoir quel spell s�lectionne va �tre envoy� sur quelle cible
    [System.NonSerialized] public Spells m_currentSpellSelected;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Si le joueur n'a rien s�lectionn� on ne fait rien
            if(m_currentSpellSelected == null) return;

            //raycast quand le joueur clique envoyer le sort � la target
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000, m_enemyMask))
            {
                hit.collider.gameObject.GetComponent<Ennemies>().GetDamage(m_currentSpellSelected);
            }
        }
    }
}
