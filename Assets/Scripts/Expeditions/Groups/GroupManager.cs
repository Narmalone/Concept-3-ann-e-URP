using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupManager : MonoBehaviour
{
    [SerializeField] private List<Group> ennemyGroupList;

    private void Awake()
    {
        InitializeGroups();
    }

    public void InitializeGroups()
    {
        foreach(Group group in ennemyGroupList)
        {
            for (int i = 0; i < group.m_maxEnnemiesInGroup; i++)
            {
                float charactersLife = Random.Range(40, 50);
                float charactersDamage = Random.Range(20, 30);
                float charactersDefense = Random.Range(10, 15);
                string charaName = group.ennemiesPossibleName[Random.Range(0, group.ennemiesPossibleName.Length)];

                Character chara = new Character(charaName, charactersLife, charactersDamage, charactersDefense, 0, SpellsManager.instance.GetRandomSpell());

                group.EnnemiesList.Add(chara);
            }
        }
    }
}
