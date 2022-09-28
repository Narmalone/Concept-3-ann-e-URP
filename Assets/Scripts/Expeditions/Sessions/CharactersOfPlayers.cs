using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CharactersOfPlayers : MonoBehaviour
{
    public Character m_thisCharacter;
    [SerializeField] private int id;

    public bool isTargetable = false;

    private float maxLife;
    private float life;
    private float damage;
    private float defense;

    //itérer comme dans le script ennemies lorsqu'un des personnages prends/reçoits des dégâts et autre
    private void Start()
    {
        m_thisCharacter = LoadPlayerTeam.instance.m_charactersTeam[id];
        SetBasicStats();
    }

    //initialiser les variables pour que ce soit plus pratique//
    public void SetBasicStats()
    {
        life = m_thisCharacter.CharactersLife;
        maxLife = m_thisCharacter.CharactersMaxLife;
        damage = m_thisCharacter.CharactersDamage;
        defense = m_thisCharacter.CharactersDefense;

        GetComponentInChildren<LifeBarHandler>().SetMaxHealth(maxLife);
    }

    public bool UpdateLife()
    {
        bool isAlive = true;
        if (life > maxLife)
        {
            life = maxLife;
            Debug.Log(life);
        }
        else if (life <= 0)
        {
            isAlive = false;
            CombatManager.instance.m_ennemyTeam.Remove(m_thisCharacter);
            Destroy(this.gameObject);
        }
        GetComponentInChildren<LifeBarHandler>().SetHealth(life);

        return isAlive;
    }

    public bool SpellCasted(Spell spell)
    {
        float RealDamage = spell.Damage * (1 - defense / 100);

        if (spell.GeneralId == 1)
        {
            life += spell.Damage;
        }
        else
        {
            life -= RealDamage;
        }

        return UpdateLife();
    }
}
