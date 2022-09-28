using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupManager : MonoBehaviour
{
    public static GroupManager instance { get; private set; }
    [SerializeField] private float minLife = 15;
    [SerializeField] private float maxLife = 20;
    [SerializeField] private float minDamage = 5;
    [SerializeField] private float maxDamage = 10;
    [SerializeField] private float minDefense = 5;
    [SerializeField] private float maxDefense = 10;
    [SerializeField] private List<Group> ennemyGroupList;
    [SerializeField] public List<Ennemy> m_group1;
    [SerializeField] public List<Ennemy> m_group2;

    private void Awake()
    {
        instance = this;
        foreach(Group grp in ennemyGroupList)
        {
            grp.IsDataInitialized = false;
        }
        InitializeGroups();
    }

    public void InitializeGroups()
    {
        foreach (Group group in ennemyGroupList)
        {
            if (group.IsDataInitialized) return;
            Debug.Log("data initialisées");
            for (int i = 0; i < group.m_maxEnnemiesInGroup; i++)
            {
                
                float charactersMaxLife = Random.Range(minLife, maxLife);
                float currentLife = charactersMaxLife;
                float charactersDamage = Random.Range(minDamage, maxDamage);
                float charactersDefense = Random.Range(minDefense, maxDefense);
                string charaName = group.ennemiesPossibleName[Random.Range(0, group.ennemiesPossibleName.Length)];

                charactersDamage = Mathf.RoundToInt(charactersDamage);
                charactersDefense = Mathf.RoundToInt(charactersDefense);
                charactersMaxLife = Mathf.RoundToInt(charactersMaxLife);

                Character chara = new Character(charaName, charactersMaxLife, currentLife, charactersDamage, charactersDefense, 0, SpellsManager.instance.GetRandomSpell());
                if(chara.CurrentCharaSpell.GeneralId != 1)
                {
                    chara.CurrentCharaSpell.Damage = chara.CurrentCharaSpell.Damage * chara.CharactersDamage / chara.CurrentCharaSpell.Damage;
                }

                group.EnnemiesList.Add(chara);

                group.IsDataInitialized = true;
            }
        }
    }
}
