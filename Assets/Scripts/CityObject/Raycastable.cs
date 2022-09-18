using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycastable : MonoBehaviour
{
    [Header("R�f�rences et variables � donner par l'inspecteur")]
    [SerializeField] private LayerMask m_selectableMask;
    [SerializeField] private Color m_selectedColor;
    [SerializeField] private Color m_defaultColor;

    [SerializeField] public int mobId;

    //Variables
    private Renderer m_thisRenderer;
    private Collider collide;

    private Character charaSelected;

    private void Awake()
    {
        collide = gameObject.GetComponent<Collider>();
    }

    //V�rifier
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if ((m_selectableMask.value & (1 << collide.gameObject.layer)) > 0)
        {
            if (Physics.Raycast(ray, out hit, 1000, m_selectableMask))
            {
                if(CombatManager.instance.canSelectMob == true)
                {
                    m_thisRenderer = hit.collider.gameObject.GetComponent<Renderer>();
                    if (m_thisRenderer)
                    {
                        m_thisRenderer.material.SetColor("_Color", m_selectedColor);
                        if (Input.GetMouseButtonDown(0))
                        {
                            Debug.Log(hit.collider.GetComponent<Raycastable>().mobId);
                            if(hit.collider.GetComponent<Raycastable>().mobId == 0)
                            {
                                charaSelected = EnemyController.instance.mobGroup[0];
                                Debug.Log("L'ennemi " + hit.collider.GetComponent<Raycastable>().mobId + " a �t� touch�");
                            } 
                            if(hit.collider.GetComponent<Raycastable>().mobId == 1)
                            {
                                charaSelected = EnemyController.instance.mobGroup[1];
                                Debug.Log("L'ennemi " + hit.collider.GetComponent<Raycastable>().mobId + " a �t� touch�");
                                //Appliquer les d�gats sur l'ennemis s�lectionn�s

                            }

                        }
                    }
                }
            }
            else
            {
                m_thisRenderer = collide.gameObject.GetComponent<Renderer>();
                m_thisRenderer.material.SetColor("_Color", m_defaultColor);
            }
        }
    }
}
