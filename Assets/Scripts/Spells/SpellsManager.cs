using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpellsManager : MonoBehaviour
{
    public static SpellsManager instance;

    [Header("BASIC CHARCATER LIST")]
    public List<Character> m_baseCharaList;

    [Header("SPELLS")]
    public List<Spell> spells;

    private void Awake()
    {
        instance = this;
    }

    public void InitializeBasicSpell()
    {
        if (spells.Count == 0) return;
        foreach(Character chara in m_baseCharaList)
        {
            chara.CurrentCharaSpell = spells[Random.Range(0, spells.Count)];
        }

        //Après avoir initalisé les personnages de bases
    }
    public Spell GetRandomSpell()
    {
        return spells[Random.Range(0, spells.Count)];
    }

    public Spell GetSpellById(int id)
    {
        Spell spellSelected = spells.Find(spell => spell.GeneralId == id);
        return spellSelected;
    }
}
