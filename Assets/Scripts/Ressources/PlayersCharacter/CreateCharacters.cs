using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCharacters : MonoBehaviour, IDataPersistence
{
    private Character m_characters;
    private Character m_secondaryBasicCharacter;

    private List<Character> charactersPlayerList;
    public bool isCharaCreated = false;

    private int charactersRarity = 0;
    private float charactersLife = 0;
    private float charactersDamage = 30;
    private float charactersDefense = 0;

    [SerializeField] private string[] charactersName;

    public void LoadData(GameData data)
    {
        this.charactersPlayerList = data.m_playerCharactersOwnedData;
        this.isCharaCreated = data.isIniated;
    }

    public void SaveData(GameData data)
    {
        data.m_playerCharactersOwnedData = this.charactersPlayerList;
        data.isIniated = this.isCharaCreated;
    }

    private void Start()
    {
        if (isCharaCreated == false)
        {
            CreateCharacter();
        }
        else
        {
            return;
        }
    }
    public void CreateCharacter()
    {
        charactersRarity = Random.Range(1, 2);
        charactersLife = Random.Range(40, 50);
        charactersDamage = Random.Range(20, 30);
        charactersDefense = Random.Range(10, 15);

        string charaName = charactersName[Random.Range(0, charactersName.Length)];

        m_characters = new Character(charaName, charactersLife, charactersDamage, charactersDefense, charactersRarity);
        m_secondaryBasicCharacter = new Character(charaName, charactersLife, charactersDamage, charactersDefense, charactersRarity);
        
        charactersPlayerList.Add(m_characters);
        charactersPlayerList.Add(m_secondaryBasicCharacter);
        isCharaCreated = true;
    }
}
