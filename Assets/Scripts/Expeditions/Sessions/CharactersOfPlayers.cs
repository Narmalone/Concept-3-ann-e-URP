using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersOfPlayers : MonoBehaviour
{
    [SerializeField] private int id;
    private Character thisCharacter;

    private float life;
    private float damage;
    private float defense;
    //itérer comme dans le script ennemies lorsqu'un des personnages prends/reçoits des dégâts et autre
    private void Start()
    {
        thisCharacter = LoadPlayerTeam.instance.m_charactersTeam[id];
        SetBasicStats();
    }

    //initialiser les variables pour que ce soit plus pratique//
    public void SetBasicStats()
    {
        life = thisCharacter.CharactersLife;
        damage = thisCharacter.CharactersDamage;
        defense = thisCharacter.CharactersDefense;

        GetComponentInChildren<LifeBarHandler>().SetMaxHealth(life);
    }
    public void GetDamage(Spells target)
    {
        if(id == AiManager.instance.index)
        {
            ApplyDamage(target.SpellBasicDamage);
            Debug.Log("Un character a subis des dégâts");
        }
        else
        {
            Debug.Log("character get damage mais pas dans la condition");
        }

    }

    public void ApplyDamage(float damage)
    {
        life -= damage;
        GetComponentInChildren<LifeBarHandler>().SetHealth(life);
        Debug.Log("Un character a pris des dégâts");
        if (life <= 0)
        {
            //Destroy(gameObject);
        }
    }
}
