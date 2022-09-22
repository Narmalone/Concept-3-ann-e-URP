using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spells", menuName = "Nouveau Sort", order = 1)]
public class Spells: ScriptableObject
{
    //So appelé dans une classe ou y'a besoin d'un sort
    public Texture2D m_icon;
    public string m_name;
    public string m_description;
    public int m_id;
    public float m_damage;
    public float m_minSpellDamage;
    public float m_maxSpellDamage;

    public void SetBasicData(Texture2D icon, string name, string description, int id, float damage, float minDamage, float maxDamage)
    {
        m_icon = icon;
        m_name = name;
        m_description = description;
        m_id = id;
        m_damage = Random.Range(minDamage, maxDamage);
        m_damage = damage;
    }
}