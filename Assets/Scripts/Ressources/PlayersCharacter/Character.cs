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
    [SerializeField] private Spells m_characterSpells;
    enum mRarity{
       Normal,
       Rare,
       Épique,
       Légendaire
    }
    //Ajouter l'esquive

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

    
    public Character(string name, float life, float damage, float defense, int rarity, Spells spells)
    {
        m_charactersName = name;
        m_life = life;
        m_damage = damage;
        m_defense = defense;
        m_rarity = rarity;
        m_characterSpells = spells;
    }
        
}
