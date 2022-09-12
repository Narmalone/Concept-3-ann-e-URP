using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersCharacter
{
    //Initialiser les statistiques d'un personnage
    private string m_charactersName;
    private float m_life;
    private float m_damage;
    private float m_defense;
    private int m_rarity;

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
}
