using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;
public class PlayersTitle : MonoBehaviour, IDataPersistence
{

    [Header("Ui Document")]
    [SerializeField] private UIDocument m_UiDocument;

    [Header("Titre du joueur, titre suivant à chaque fois que le joueur atteint un objectif")]
    [SerializeField, Tooltip("Liste des différents titre que le joueur obtient suivant sa progression.")] private string[] m_playersTitle;
    private int m_currentPlayerTitle;

    private Label m_playersTitleText;

    #region Database
    public void LoadData(GameData data)
    {
        m_playersTitleText.text = data.m_currentPlayersTitle;
    }

    public void SaveData(GameData data)
    {
        data.m_currentPlayersTitle = this.m_playersTitleText.text;
    }

    #endregion
    
    private void Awake()
    {
        var rootElement = m_UiDocument.rootVisualElement;
        m_playersTitleText = rootElement.Q<Label>("PlayersTitleLabel");
    }

    //Fonction lorsque le joueur a suffisamment d'or pour passer au titre suivant
    public void UpdatePlayersTitle()
    {
        if(m_currentPlayerTitle <= m_playersTitle.Length-1)
        {
            m_currentPlayerTitle++;
            m_playersTitleText.text = "" + m_playersTitle[m_currentPlayerTitle];
        }
    }
}
