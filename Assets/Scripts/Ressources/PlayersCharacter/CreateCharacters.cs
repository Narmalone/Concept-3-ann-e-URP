using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCharacters : MonoBehaviour, IDataPersistence
{
    private Character m_characters;
    private Character m_secondaryBasicCharacter;
    private Character m_thirdCharacter;
    private Character m_fourCharacter;

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

        m_characters = new Character(charaName, charactersLife, charactersDamage, charactersDefense, charactersRarity, SpellsManager.instance.RandomSpell);
        m_secondaryBasicCharacter = new Character(charactersName[Random.Range(0, charactersName.Length)], Random.Range(40, 50), Random.Range(20, 30), Random.Range(10, 15), Random.Range(1, 2), SpellsManager.instance.RandomSpell);
        m_thirdCharacter = new Character(charactersName[Random.Range(0, charactersName.Length)], Random.Range(40, 50), Random.Range(20, 30), Random.Range(10, 15), Random.Range(1, 2), SpellsManager.instance.RandomSpell);
        m_fourCharacter = new Character(charactersName[Random.Range(0, charactersName.Length)], Random.Range(40, 50), Random.Range(20, 30), Random.Range(10, 15), Random.Range(1, 2), SpellsManager.instance.RandomSpell);

        charactersPlayerList.Add(m_characters);
        charactersPlayerList.Add(m_secondaryBasicCharacter);
        charactersPlayerList.Add(m_thirdCharacter);
        charactersPlayerList.Add(m_fourCharacter);

        isCharaCreated = true;
    }
}
