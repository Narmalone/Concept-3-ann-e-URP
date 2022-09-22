using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class SpellsManager : MonoBehaviour
{
    public static SpellsManager instance;
   
    [Header("Fireballs")]
    public List<Spells> FireballSpells;
    public Texture2D fireballIcon;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        InitializeBasicSpells();
    }

    public void InitializeBasicSpells()
    {
        foreach(Spells spell in FireballSpells)
        {
            spell.SetBasicData(fireballIcon, "Firebolt", "Tire une boule de feu causant: "+ spell.m_damage.ToString() +" dégâts", 0, spell.m_damage, spell.m_minSpellDamage, spell.m_maxSpellDamage);
        }
    }
    public void SetData()
    {

    }

    public void GenerateRandomFloat(float random, float minValue, float maxValue)
    {
        random = Random.Range(minValue, maxValue);
    }
}
