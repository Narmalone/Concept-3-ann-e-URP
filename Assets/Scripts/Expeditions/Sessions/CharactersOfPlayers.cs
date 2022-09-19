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
        GetComponentInChildren<LifeBarHandler>().SetMaxHealth(life);
        SetBasicStats();
    }

    //initialiser les variables pour que ce soit plus pratique//
    public void SetBasicStats()
    {
        life = thisCharacter.CharactersLife;
        damage = thisCharacter.CharactersDamage;
        defense = thisCharacter.CharactersDefense;
    }
    public void GetDamage(Spells target)
    {
        if(id == AiManager.instance.index)
        {
            ApplyDamage(target.SpellBasicDamage);

            //Passer au tour du joueur quand un de nos personnages prends des dégâts
            CombatManager.instance.delTurn(true);
        }
    }

    public void ApplyDamage(float damage)
    {
        life -= damage;
        Debug.Log("Vie restante de ce personnage: " + life);
        GetComponentInChildren<LifeBarHandler>().SetHealth(life);
        if (life <= 0)
        {
            Debug.Log("Le personnage: " + thisCharacter.CharactersName + " est mort");
            //Destroy(gameObject);
        }
    }
}
