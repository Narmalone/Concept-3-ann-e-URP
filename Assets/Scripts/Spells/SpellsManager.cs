using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpellsManager : MonoBehaviour
{
    public static SpellsManager instance { get; private set; }

    [Header("BASIC CHARCATER LIST")]
    public List<Character> m_baseCharaList;

    [Header("SPELLS")]
    public List<Spell> spells;
    public List<Spell> ennemySpells;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        instance = this;
    }
    public Spell GetRandomSpell()
    {
        return ennemySpells[Random.Range(0, ennemySpells.Count)];
    }  
    
    public Spell GetRandomCharaSpell()
    {
        Spell randomSpell = spells[Random.Range(0, spells.Count)];
        return randomSpell;
    }

    public Spell GetSpellById(int id)
    {
        Spell spellSelected = spells.Find(spell => spell.GeneralId == id);
        return spellSelected;
    }

    public Spell CastSpell(Spell spellToCast)
    {

        return spellToCast;
    }
}
