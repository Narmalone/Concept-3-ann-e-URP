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

    //Trouver un moyen de save la liste dans le fichier gamedata elle n'y apparait pas
    public List<PlayersCharacter> m_playerCharactersOwnedData;

    //valeurs définies dans le constructeur seront les valeurs par défauts
    //La game commence quand il n'ya a pas de données à charger
    public GameData()
    {
        this.m_playerMoney = 0;
        this.m_currentPlayersTitle = "Débutant de la mine";
        this.isMaxPlayerTitleReached = false;
        this.m_currentPlayersProgression = 0;
        this.m_playerCharactersOwnedData = new List<PlayersCharacter>();
    }

}
