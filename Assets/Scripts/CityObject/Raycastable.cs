using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycastable : MonoBehaviour
{
    [SerializeField] private LayerMask m_interactible;

    [SerializeField] private Color m_selectedColor;

    private Renderer m_thisRenderer;

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, 1000, m_interactible))
        {
            m_thisRenderer = gameObject.GetComponent<Renderer>();

            m_thisRenderer.material.color = m_selectedColor;
        }
    }
}
