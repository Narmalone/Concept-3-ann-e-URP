using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    [Header("REFERENCES")]
    [SerializeField, Tooltip("SO auquel cet ennemi appartiens")] public Group m_thisEnemyGroup;
    [Tooltip("Permet de trouver ce script Ennemy et l'ajouter dans une liste la liste correspond à l'ID du mob")] public int groupID = 0;
    [SerializeField, Tooltip("Id de l'ennemi correspondant à sa position dans la liste du groupe")] private int m_id;

    public Character m_thisCharacter;

    //Si il est une cible du sort que le joueur va lancer
    public bool isTargetable = false;

    private float maxLife;
    private float life;
    private float damage;
    private float defense;

    private void Start()
    {
        SetData();
    }

    public void SetData()
    {
        //Le personnage équivaut à chercher dans son groupe la liste d'ennemies en fonction de son propre id
        m_thisCharacter = m_thisEnemyGroup.EnnemiesList[m_id];

        maxLife = m_thisCharacter.CharactersMaxLife;
        life = m_thisCharacter.CharactersLife;
        damage = m_thisCharacter.CharactersDamage;
        defense = m_thisCharacter.CharactersDefense;
        m_thisCharacter.CurrentCharaSpell = SpellsManager.instance.GetSpellById(m_thisCharacter.m_spellID);

        GetComponentInChildren<LifeBarHandler>().SetMaxHealth(maxLife);
    }

    public bool UpdateLife()
    {
        bool isAlive = true;
        if(life > maxLife)
        {
            life = maxLife;
        }
        else if(life <= 0)
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
        if(spell.GeneralId == 1) 
        {
            life += spell.Damage;
        }
        else
        {
            float RealDamageTaken = spell.Damage * (1 - defense / 100);
            life -= RealDamageTaken;
        }
        
        
        return UpdateLife();
    }
}
