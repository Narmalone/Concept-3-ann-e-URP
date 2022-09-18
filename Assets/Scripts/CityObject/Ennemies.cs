using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemies : MonoBehaviour
{
    private float life;
    private float damage;
    private float defense;

    private void Awake()
    {
        life = Random.Range(30, 40);
        damage = Random.Range(15, 20);
        defense = Random.Range(5, 10);
    }

    //Lorsque l'ennemi subis des dégâts
    public void GetDamage(Spells target)
    {
        ApplyDamage(target.SpellBasicDamage);
    }

    //Dégâts reçus par un sort
    public void ApplyDamage(float damage)
    {
        //Dégâts réduits en fonction de la défense
        damage = (damage - defense) / (damage + defense) / 2 * 100;
        Debug.Log("Dégâts subis: " + damage);
        Debug.Log("défense de l'ennemi: " + defense);

        life -= damage;
        
        if(life <= 0)
        {
            Destroy(gameObject);
        }

    }
}
