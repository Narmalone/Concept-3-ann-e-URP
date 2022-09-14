using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpellsManager : MonoBehaviour
{
    public static SpellsManager instance { get; private set; }
    [SerializeField] private List<Spells> m_spellList;

    [Header("Textures 2D des spells")]
    [SerializeField] private Texture2D spelltexture;

    private Spells fireball;
    
    [SerializeField] public Spells RandomSpell;

    private void Awake()
    {
        if(instance != null)
        {
            return;
        }

        CreateSpell();
        instance = this;
    }
    public void CreateSpell()
    {
        fireball = new Spells(spelltexture, "fireball", "Envoie une boule de feu faisant 10 à 15 points de dégâts", 0, Random.Range(10,15));
        m_spellList.Add(fireball);

        AttributeRandomSpell();
    }

    public void AttributeRandomSpell()
    {
        RandomSpell = m_spellList[Random.Range(0, m_spellList.Count)];
        Debug.Log("Sort attribué: " + RandomSpell.SpellName);
    }
}
