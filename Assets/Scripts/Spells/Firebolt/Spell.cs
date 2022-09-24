using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "New Spell", order = 1)]
public class Spell : ScriptableObject, ISpell
{
    public string m_name;
    public int m_generalId;
    public float m_damage;
    public Texture2D m_icon;
    public string m_description;

    [Tooltip("Si true la cible est multiple")] public bool m_isSolorOrMultipleTarget;
    [Tooltip("Si true la target est team")]public bool m_isEnnemiesTargetOrFromTeamTarget;
    public string Name { get => m_name;}
    public string Description { get => m_description; }
    public int GeneralId { get => m_generalId; }
    public float Damage { get => m_damage;}
    public Texture2D Icon { get => m_icon;}

    public bool IsSoloTarget { get => m_isSolorOrMultipleTarget; }

    public bool IsEnemyTarget { get => m_isEnnemiesTargetOrFromTeamTarget; }

    public void CastSpell(Spell spell)
    {

    }
}
