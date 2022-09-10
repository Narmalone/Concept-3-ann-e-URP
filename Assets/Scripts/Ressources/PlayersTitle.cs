using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class PlayersTitle : MonoBehaviour, IDataPersistence
{
    [Header("Titre du joueur, titre suivant à chaque fois que le joueur atteint un objectif")]
    [SerializeField, Tooltip("Liste des différents titre que le joueur obtient suivant sa progression.")] private string[] m_playersTitle;
    private int m_currentPlayerTitle;

    private TextMeshProUGUI m_TitleText;

    #region Database
    public void LoadData(GameData data)
    {
        m_TitleText.text = data.m_currentPlayersTitle;
    }

    public void SaveData(GameData data)
    {
        data.m_currentPlayersTitle = this.m_TitleText.text;
    }

    #endregion
    
    private void Awake()
    {
        m_TitleText = GetComponent<TextMeshProUGUI>();
    }

    //Fonction lorsque le joueur a suffisamment d'or pour passer au titre suivant
    public void UpdatePlayersTitle()
    {
        if(m_currentPlayerTitle <= m_playersTitle.Length-1)
        {
            m_currentPlayerTitle++;
            m_TitleText.text = "" + m_playersTitle[m_currentPlayerTitle];
        }
    }
}
