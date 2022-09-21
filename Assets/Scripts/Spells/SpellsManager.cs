using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpellsManager : MonoBehaviour, IDataPersistence
{
    public static SpellsManager instance { get; private set; }
    [SerializeField] private List<Spells> m_spellList;

    [Header("Textures 2D des spells")]
    [SerializeField] private Texture2D spelltexture;

    private Spells fireball;
    
    [SerializeField] public Spells RandomSpell;
    private List<Character> listChara;

    private void Awake()
    {
        if(instance != null)
        {
            return;
        }

        instance = this;
    }
    public void CreateSpell()
    {
        //Trouver un moyen de faire une liste de sorts que l'on peut appeler genre new Fireball()
        foreach(Character chara in listChara)
        {
            chara.CurrentCharaSpell = new Spells(spelltexture, "Fireball", "Envoie une boule de feu faisant: ", 2, Random.Range(5, 15));
            m_spellList.Add(chara.CurrentCharaSpell);
        }
        //fireball = new Spells(spelltexture, "fireball", "Envoie une boule de feu faisant X points de dégâts", 0, Random.Range(10,15));
        //m_spellList.Add(fireball);
        AttributeRandomSpell();
    }

    public void AttributeRandomSpell()
    {
        RandomSpell = m_spellList[Random.Range(0, m_spellList.Count)];
    }

    public void LoadData(GameData data)
    {
        this.listChara = data.m_playerCharactersOwnedData;
    }

    public void SaveData(GameData data)
    {
        data.m_playerCharactersOwnedData = this.listChara;
    }
}
