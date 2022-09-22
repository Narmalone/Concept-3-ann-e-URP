using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCharacters : MonoBehaviour, IDataPersistence, ISpells
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
    [SerializeField] private List<Texture2D> m_iconTextures;
    [SerializeField] private List<Spells> m_SpellList;
    public void LoadData(GameData data)
    {
        this.charactersPlayerList = data.m_playerCharactersOwnedData;
        this.isCharaCreated = data.isIniated;
        this.m_SpellList = data.AllSpells;
    }

    public void SaveData(GameData data)
    {
        data.m_playerCharactersOwnedData = this.charactersPlayerList;
        data.isIniated = this.isCharaCreated;
        data.AllSpells = this.m_SpellList;
    }

    private void Start()
    {
        if (isCharaCreated == false)
        {
            InitializeBasicsCharacters();
        }
        else
        {
            return;
        }
    }
    //Fonction pour initialiser des personnages de base lorsque le joueur crée une partie
    public void InitializeBasicsCharacters()
    {
        charactersRarity = Random.Range(1, 2);
        charactersLife = Random.Range(40, 50);
        charactersDamage = Random.Range(20, 30);
        charactersDefense = Random.Range(10, 15);

        string charaName = charactersName[Random.Range(0, charactersName.Length)];

        m_characters = new Character(charaName, charactersLife, charactersDamage, charactersDefense, charactersRarity, null);

        m_secondaryBasicCharacter = new Character(charactersName[Random.Range(0, charactersName.Length)], Random.Range(40, 50), Random.Range(20, 30), Random.Range(10, 15), Random.Range(1, 2), null);

        m_thirdCharacter = new Character(charactersName[Random.Range(0, charactersName.Length)], Random.Range(40, 50), Random.Range(20, 30), Random.Range(10, 15), Random.Range(1, 2), null);

        m_fourCharacter = new Character(charactersName[Random.Range(0, charactersName.Length)], Random.Range(40, 50), Random.Range(20, 30), Random.Range(10, 15), Random.Range(1, 2), null);

        CreateSpell(m_characters.CurrentCharaSpell, m_characters);
        CreateSpell(m_secondaryBasicCharacter.CurrentCharaSpell, m_secondaryBasicCharacter);
        CreateSpell(m_thirdCharacter.CurrentCharaSpell, m_thirdCharacter);
        CreateSpell(m_fourCharacter.CurrentCharaSpell, m_fourCharacter);


        charactersPlayerList.Add(m_characters);
        charactersPlayerList.Add(m_secondaryBasicCharacter);
        charactersPlayerList.Add(m_thirdCharacter);
        charactersPlayerList.Add(m_fourCharacter);

        //SpellsManager.instance.CreateSpell();

        isCharaCreated = true;
    }

    //fonction pour re set les dégâts de son sort en fonction de son attaque
    public void SetDamage(Character target)
    {
        //formule pour calculer -> Damage = SpellDamage * CharaDamage / spellDamage
        target.CurrentCharaSpell.SpellBasicDamage = (target.CurrentCharaSpell.SpellBasicDamage * target.CharactersDamage) / target.CurrentCharaSpell.SpellBasicDamage;
    }

    //Interface ISpells
    public void CreateSpell(Spells newSpell, Character target)
    {
        newSpell = new Spells(null, "Fireball", "Boule de feu omg", 0, Random.Range(10, 15));
        m_SpellList.Add(newSpell);
        target.CurrentCharaSpell = newSpell;
        AttributeRandomSpell(newSpell, target);
    }

    public void AttributeRandomSpell(Spells randomSpell)
    {
       
    }

    //no need car besoin d'une target
    public void CreateSpell(Spells newSpell)
    {
        throw new System.NotImplementedException();
    }

    public void AttributeRandomSpell(Spells randomSpell, Character target)
    {
        randomSpell = m_SpellList[Random.Range(0, m_SpellList.Count)];
        SetDamage(target);

        Debug.Log(randomSpell.SpellName);
    }
}
