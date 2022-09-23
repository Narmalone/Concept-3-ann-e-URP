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

    public List<Spell> AllSpell;
    public bool isIniated;
    //valeurs définies dans le constructeur seront les valeurs par défauts
    //La game commence quand il n'ya a pas de données à charger
    public GameData()
    {
        //Or du joueur
        this.m_playerMoney = 0;

        //Titres du joueur
        this.m_currentPlayersTitle = "Débutant de la mine";
        this.isMaxPlayerTitleReached = false;
        this.m_currentPlayersProgression = 0;

        //Data si les persos ont déjà été crées
        this.isIniated = false;
        this.m_playerCharactersOwnedData = new List<Character>();

        this.m_playerTeam = new List<Character>();

        this.AllSpell = new List<Spell>();
    }

}
