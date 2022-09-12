using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsCreator : MonoBehaviour
{
    [SerializeField] private Texture2D fireballIcon;
    [SerializeField] private List<Spells> m_BasicCharaSpellList;

   public void BasicSpells()
    {
        var m_fireballSpell = new Spells(fireballIcon, "Fireball", "Un sort basique mais efficace.", 0, 25);
        m_BasicCharaSpellList.Add(m_fireballSpell);
    }
}
