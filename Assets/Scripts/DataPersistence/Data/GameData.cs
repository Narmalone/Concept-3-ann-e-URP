using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData
{
    //Variables en non rapport avec le Gameplay
    public long m_lastUpdated;

    //Variables de gameplay
    public float m_playerMoney;
    public string m_currentPlayersTitle;
    public bool isMaxPlayerTitleReached;

    //valeurs définies dans le constructeur seront les valeurs par défauts
    //La game commence quand il n'ya a pas de données à charger
    public GameData()
    {
        this.m_playerMoney = 0;
        this.m_currentPlayersTitle = "Débutant de la mine";
        this.isMaxPlayerTitleReached = false;
    }
}
