using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    [Header("Premier boutton sélectionné")]
    [SerializeField] private Button m_firstSelected;

    //Protected virtual pour que d'autres scripts puissent l'override
    protected virtual void OnEnable()
    {
        SetFirstSelected(m_firstSelected);
    }

    public void SetFirstSelected(Button firstSelectedButton)
    {
        firstSelectedButton.Select();
    }
}
