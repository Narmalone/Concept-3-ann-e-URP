using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycastable : MonoBehaviour
{
    [Header("Références et variables à donner par l'inspecteur")]
    [SerializeField] private LayerMask m_selectableMask;
    [SerializeField] private Color m_selectedColor;
    [SerializeField] private Color m_defaultColor;

    //Variables
    private Renderer m_thisRenderer;
    private Collider collide;

    private void Awake()
    {
        collide = gameObject.GetComponent<Collider>();
    }

    //Vérifier
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if ((m_selectableMask.value & (1 << collide.gameObject.layer)) > 0)
        {
            if (Physics.Raycast(ray, out hit,1000, m_selectableMask))
            {
                m_thisRenderer = hit.collider.gameObject.GetComponent<Renderer>();
                Debug.Log(m_thisRenderer.gameObject.name);
                if (m_thisRenderer)
                {
                    m_thisRenderer.material.SetColor("_Color", m_selectedColor);
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
