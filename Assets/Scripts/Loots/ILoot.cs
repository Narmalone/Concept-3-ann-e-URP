using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoot
{
    public List<Spell> spellsToDrop { get; }
    public List<Consommable> consommables { get; }
    public float Money { get; }
    public void OnLoot(Loot loot);
}
