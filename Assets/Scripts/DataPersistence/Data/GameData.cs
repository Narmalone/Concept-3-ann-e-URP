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

    
    public List<Character> m_playerCharactersOwnedData;
    public List<Character> m_playerTeam;

    public bool isIniated;

    //valeurs d�finies dans le constructeur seront les valeurs par d�fauts
    //La game commence quand il n'ya a pas de donn�es � charger
    public GameData()
    {
        //Or du joueur
        this.m_playerMoney = 0;

        //Titres du joueur
        this.m_currentPlayersTitle = "D�butant de la mine";
        this.isMaxPlayerTitleReached = false;
        this.m_currentPlayersProgression = 0;

        //Data si les persos ont d�j� �t� cr�es
        this.isIniated = false;
        this.m_playerCharactersOwnedData = new List<Character>();

        //Charger l'�quipe du joueur
        this.m_playerTeam = new List<Character>();
    }

}
