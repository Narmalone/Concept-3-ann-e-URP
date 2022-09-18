using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

    //Lorsque l'ennemi subis des d�g�ts
    public void GetDamage(Spells target)
    {
        ApplyDamage(target.SpellBasicDamage);

        //D�sactiver le bouton sort que le joueur � lanc�
        UiManagerSession.instance.SpellUsed();
    }

    //D�g�ts re�us par un sort
    public void ApplyDamage(float damage)
    {
        //D�g�ts r�duits en fonction de la d�fense
        damage = (damage - defense) / (damage + defense) / 2 * 100;
        Debug.Log("D�g�ts subis: " + damage);
        Debug.Log("d�fense de l'ennemi: " + defense);

        life -= damage;

        UiManagerSession.instance.UpdateCombatUi();

        if(life <= 0)
        {
            Destroy(gameObject);
        }

    }
}
