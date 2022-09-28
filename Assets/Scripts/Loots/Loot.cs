using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Loot", menuName = "Creater New Loot", order = 3)]
[System.Serializable]
public class Loot : ScriptableObject, ILoot
{
    public float m_money;
    public List<Spell> m_spellLoot;
    public List<Consommable> m_consommableLoot;
    public List<Spell> spellsToDrop { get => m_spellLoot; }

    public List<Consommable> consommables { get => m_consommableLoot; }

    public float Money { get => m_money; }

    public void OnLoot(Loot loot) { 

    }
}
