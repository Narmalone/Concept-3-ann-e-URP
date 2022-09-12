using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Spells : MonoBehaviour
{
    [SerializeField] private Texture2D m_spellIcon;
    [SerializeField] private string m_spellName;
    [SerializeField] private string m_spellDescription;
    [SerializeField] private int m_spellId;
    [SerializeField] private float m_spellBasicDamage;

    public Texture2D SpellIcon
    {
        get {return m_spellIcon; }
        set { m_spellIcon = value; }
    }
    public string SpellName
    {
        get {return m_spellName; }
        set { m_spellName = value; }
    }
    public string SpellDescription
    {
        get {return m_spellDescription; }
        set { m_spellDescription = value; }
    }
    public float SpellBasicDamage
    {
        get {return m_spellBasicDamage; }
        set { m_spellBasicDamage = value; }
    }

    public Spells(Texture2D icon, string spellName, string spellDescription, int spellId, float spellBasicDamage)
    {
        m_spellIcon = icon;
        m_spellName = spellName;
        m_spellDescription = spellDescription;
        m_spellId = spellId;
        m_spellBasicDamage = spellBasicDamage;
    }
}
