using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

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

    public void UpdateLife()
    {
        if(life > maxLife)
        {
            life = maxLife;
        }
        GetComponentInChildren<LifeBarHandler>().SetHealth(life);
    }

    public void GetDamage(Spell target)
    {
        if(target.GeneralId == 1)
        {
            float RealDamage = target.Damage * (1 - defense / 100);
            ApplyDamage(RealDamage);
        }
        else
        {
            ApplyDamage(damage);
        }
    }

    public void ApplyDamage(float damage)
    {
        life -= damage;

        GetComponentInChildren<LifeBarHandler>().SetHealth(life);

        if (life <= 0)
        {
            //Destroy(gameObject);
        }
    }

    public void SpellCasted(Spell spell)
    {
        if(spell.GeneralId == 1)
        {
            life += spell.Damage;
        }
        else
        {
            life -= spell.Damage;
        }

        UpdateLife();
    }
}
