using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCharacters : MonoBehaviour, IDataPersistence
{
    private PlayersCharacter m_characters;

    private List<PlayersCharacter> m_playersCharactersOwned = new List<PlayersCharacter>();

    private int charactersRarity = 0;
    private float charactersLife = 0;
    private float charactersDamage = 30;
    private float charactersDefense = 0;
    private string charactersName = "Simone";

    private void Awake()
    {
        //set up basic variables
        //TO DO: Stocker les données dans les game data == list
        //TO DO: données différentes en fonction de la rareté 
        charactersRarity = Random.Range(1, 2);
        charactersLife = Random.Range(40, 50);
        charactersDamage = Random.Range(20, 30);
        charactersDefense = Random.Range(10, 15);
        m_characters = new PlayersCharacter(charactersName, charactersLife, charactersDamage, charactersDefense, charactersRarity);
        
    }
    private void Start()
    {
        m_playersCharactersOwned.Add(m_characters);
    }
    public void LoadData(GameData data)
    {
        data.m_playerCharactersOwnedData = this.m_playersCharactersOwned; 
        
    }

    public void SaveData(GameData data)
    {
        this.m_playersCharactersOwned = data.m_playerCharactersOwnedData;
    }
}
