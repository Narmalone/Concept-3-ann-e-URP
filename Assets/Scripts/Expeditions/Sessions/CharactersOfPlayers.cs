using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersOfPlayers : MonoBehaviour
{
    private Character thisCharacter;

    private float life;
    private float damage;
    private float defense;
    //itérer comme dans le script ennemies lorsqu'un des personnages prends/reçoits des dégâts et autre
    private void Start()
    {
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
        float damageTaken = target.SpellBasicDamage - (target.SpellBasicDamage / defense);
        ApplyDamage(damageTaken);

        Debug.Log(target.SpellBasicDamage);
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
}
