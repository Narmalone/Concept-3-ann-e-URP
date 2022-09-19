using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ennemies : MonoBehaviour
{
    private float life;
    private float damage;
    private float defense;
    private float dmgSubis;

    [HideInInspector] public Spells m_enemySpell;

    private void Awake()
    {
        life = Random.Range(40, 50);
        damage = Random.Range(15, 20);
        defense = Random.Range(5, 10);
        m_enemySpell = SpellsManager.instance.RandomSpell;
        GetComponentInChildren<LifeBarHandler>().SetMaxHealth(life);
    }

    //Lorsque l'ennemi subis des d�g�ts
    public void GetDamage(Spells target)
    {
        //le joueur a lanc� un sort donc ce n'est plus son tour
        CombatManager.instance.delTurn(false);
        ApplyDamage(target.SpellBasicDamage);

        //D�sactiver le bouton sort que le joueur � lanc�
        UiManagerSession.instance.SpellUsed();
    }

    //D�g�ts re�us par un sort
    public void ApplyDamage(float damage)
    {
        //D�g�ts r�duits en fonction de la d�fense
        //Formule ou on divise par 100 defense pour avoir sa valeur de 0 � 1
        //puis pour les d�g�ts on multiplie d�g�ts - la d�fense pour que le sort fasse moins de d�g�ts

        defense = defense / 100;
        damage = damage * (1 - defense);
        Debug.Log("D�g�ts subis de l'ennemi: " + damage);
        life -= damage;

        dmgSubis = damage;

        GetComponentInChildren<LifeBarHandler>().SetHealth(life);
        CombatManager.instance.delTurn(false);

        if (life <= 0)
        {
            //Si le mob meurt on le retire de la list afin que le prochain sort s�lectionn� par un monstre � envoyer au joueur ne soit pas une error
            AiManager.instance.mobGroup1.Remove(this);
            Destroy(gameObject);
        }
    }
}
