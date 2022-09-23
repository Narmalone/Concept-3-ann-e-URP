using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupManager : MonoBehaviour
{
    public List<Character> group_1 = new List<Character>();

    public List<Character> group_2 = new List<Character>();

    [SerializeField] private string[] charactersName;

    //Max 4 characters
    [SerializeField] private int maxEnenemyInGroup = 4;


    private void Awake()
    {
        InitializeGroups();
    }

    public void InitializeGroups()
    {
        for(int i = 0; i < maxEnenemyInGroup; i++)
        {
            float charactersLife = Random.Range(40, 50);
            float charactersDamage = Random.Range(20, 30);
            float charactersDefense = Random.Range(10, 15);
            string charaName = charactersName[Random.Range(0, charactersName.Length)];

            Character chara = new Character(charaName, charactersLife, charactersDamage, charactersDefense, 0, SpellsManager.instance.GetRandomSpell());
            group_1.Add(chara);
        }
        
        for(int i = 0; i < maxEnenemyInGroup; i++)
        {
            float charactersLife = Random.Range(40, 50);
            float charactersDamage = Random.Range(20, 30);
            float charactersDefense = Random.Range(10, 15);
            string charaName = charactersName[Random.Range(0, charactersName.Length)];

            Character chara = new Character(charaName, charactersLife, charactersDamage, charactersDefense, 0, SpellsManager.instance.GetRandomSpell());
            group_2.Add(chara);
        }
    }
}
