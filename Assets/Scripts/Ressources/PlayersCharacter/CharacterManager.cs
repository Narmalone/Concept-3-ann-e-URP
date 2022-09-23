using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class CharacterManager : MonoBehaviour, IDataPersistence
{
    public static CharacterManager instance { get; set; }

    public List<Character> charactersPlayerList;
    public bool isCharaCreated = false;

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

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (isCharaCreated == false)
        {
            InitializeBasicsCharacters();
        }
        else
        {
            foreach(Character character in charactersPlayerList)
            {
                character.CurrentCharaSpell = SpellsManager.instance.GetSpellById(character.m_spellID);
            }
            return;
        }
    }
    //Fonction pour initialiser des personnages de base lorsque le joueur crée une partie
    public void InitializeBasicsCharacters()
    {

        for (int i = 0; i < 4; i++)
        {
            int charactersRarity = Random.Range(1, 2);
            float charactersLife = Random.Range(40, 50);
            float charactersDamage = Random.Range(20, 30);
            float charactersDefense = Random.Range(10, 15);
            string charaName = charactersName[Random.Range(0, charactersName.Length)];

            Character chara = new Character(charaName, charactersLife, charactersDamage, charactersDefense, charactersRarity, SpellsManager.instance.GetRandomSpell());

            charactersPlayerList.Add(chara);
        }

        SpellsManager.instance.m_baseCharaList = charactersPlayerList;

        SpellsManager.instance.InitializeBasicSpell();

        isCharaCreated = true;
    }

}
