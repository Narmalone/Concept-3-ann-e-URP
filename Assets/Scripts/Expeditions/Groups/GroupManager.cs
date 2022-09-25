using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupManager : MonoBehaviour
{
    public static GroupManager instance { get; private set; }
    [SerializeField] private List<Group> ennemyGroupList;
    [SerializeField] public List<Ennemy> m_group1;
    [SerializeField] public List<Ennemy> m_group2;

    private void Awake()
    {
        instance = this;
        InitializeGroups();
    }

    public void InitializeGroups()
    {
        foreach (Group group in ennemyGroupList)
        {
            if (group.IsDataInitialized) return;

            for (int i = 0; i < group.m_maxEnnemiesInGroup; i++)
            {
                
                float charactersMaxLife = Random.Range(40, 50);
                Debug.Log(charactersMaxLife);
                float currentLife = charactersMaxLife;
                float charactersDamage = Random.Range(20, 30);
                float charactersDefense = Random.Range(10, 15);
                string charaName = group.ennemiesPossibleName[Random.Range(0, group.ennemiesPossibleName.Length)];

                Character chara = new Character(charaName, charactersMaxLife, currentLife, charactersDamage, charactersDefense, 0, SpellsManager.instance.GetRandomSpell());

                group.EnnemiesList.Add(chara);
                group.IsDataInitialized = true;
            }
        }
    }
}
