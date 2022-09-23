using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mob Group", menuName = "New Ennemy Group", order = 2)]
public class Group : ScriptableObject, IGroups
{
    public List<Character> m_enemyGroup;
    public int m_maxEnnemiesInGroup = 4;
    public int m_groupId = 0;
    public string[] ennemiesPossibleName;
    public List<Character> EnnemiesList { get => m_enemyGroup; }

    public int MaxEnnemiesInGroup { get => m_maxEnnemiesInGroup; }

    public int GroupId { get => m_groupId; }

    public string[] EnnemiesPossibleName { get => ennemiesPossibleName; }
}
