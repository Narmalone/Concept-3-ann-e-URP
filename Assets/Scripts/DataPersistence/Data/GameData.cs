using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameData
{
    //Variables en non rapport avec le Gameplay
    public long m_lastUpdated;

    //Variables de gameplay
    public float m_playerMoney;
    public string m_currentPlayersTitle;
    public bool isMaxPlayerTitleReached;

    public bool levelLocked;
    public int m_currentPlayersProgression;

    //valeurs d�finies dans le constructeur seront les valeurs par d�fauts
    //La game commence quand il n'ya a pas de donn�es � charger
    public GameData()
    {
        this.m_playerMoney = 0;
        this.m_currentPlayersTitle = "D�butant de la mine";
        this.isMaxPlayerTitleReached = false;
        this.m_currentPlayersProgression = 0;
    }

}
