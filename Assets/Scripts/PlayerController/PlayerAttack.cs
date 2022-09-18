using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public static PlayerAttack instance { get; private set; }
    [SerializeField] private LayerMask m_enemyMask;

    //Savoir quel spell sélectionne va être envoyé sur quelle cible
    [System.NonSerialized] public Spells m_currentSpellSelected;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Si le joueur n'a rien sélectionné on ne fait rien
            if(m_currentSpellSelected == null) return;

            //raycast quand le joueur clique envoyer le sort à la target
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000, m_enemyMask))
            {
                hit.collider.gameObject.GetComponent<Ennemies>().GetDamage(m_currentSpellSelected);

                //une fois que le joueur a appuyé il ne doit plus avoir le sort comme sélectionné
                m_currentSpellSelected = null;
            }
        }
    }
}
