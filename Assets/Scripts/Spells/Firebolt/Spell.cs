using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "New Spell", order = 1)]
public class Spell : ScriptableObject, ISpell
{
    public string m_name;
    public int m_generalId;
    public float m_damage;
    public Texture2D m_icon;
    public string Name { get => m_name;}
    public string Description { get; set; }
    public int GeneralId { get => m_generalId; }
    public float Damage { get => m_damage;}
    public Texture2D Icon { get => m_icon;}

    public void CastSpell()
    {

    }
}
