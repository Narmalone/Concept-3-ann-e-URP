using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character
{
    //Initialiser les statistiques d'un personnage
    [SerializeField] private string m_charactersName;
    [SerializeField] private float m_life;
    [SerializeField] private float m_damage;
    [SerializeField] private float m_defense;
    [SerializeField] private int m_rarity;
    private Spell m_currentSpell;

    [SerializeField] public int m_spellID;

    enum mRarity{
       Normal,
       Rare,
       Épique,
       Légendaire
    }

    //Créer les accesseurs qui pourront être modifiées
    public string CharactersName
    {
        get { return m_charactersName; }
        set { m_charactersName = value; }
    }

    public float CharactersLife
    {
        get { return m_life; }
        set { m_life = value; }
    }

    public float CharactersDamage
    {
        get { return m_damage; }
        set { m_damage = value; }
    }

    public float CharactersDefense
    {
        get { return m_defense; }
        set { m_defense = value; }
    }

    public int CharactersRarity
    {
        get { return m_rarity; }
        set { m_rarity = value; }
    }

    public Spell CurrentCharaSpell
    {
        get { return m_currentSpell; }
        set { m_currentSpell = value; }
    }
    public Character(string name, float life, float damage, float defense, int rarity, Spell currentSpell)
    {
        m_charactersName = name;
        m_life = life;
        m_damage = damage;
        m_defense = defense;
        m_rarity = rarity;
        m_currentSpell = currentSpell;
        m_spellID = currentSpell.GeneralId;
    }
        
}
