using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    [Header("Premier boutton s�lectionn�")]
    [SerializeField] private Button m_firstSelected;

    //Protected virtual pour que d'autres scripts puissent l'override
    //protected virtual void OnEnable()
    //{
        //if (m_firstSelected != null)
        //{
            //SetFirstSelected(m_firstSelected);
        //}
    //}

    public void SetFirstSelected(Button firstSelectedButton)
    {
        if(firstSelectedButton != null)
        {
            firstSelectedButton.Select();
            return;
        }
    }
}
