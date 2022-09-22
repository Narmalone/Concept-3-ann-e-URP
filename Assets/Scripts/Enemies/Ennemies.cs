using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ennemies : MonoBehaviour, ISpells
{
    [Header("Basiques variables des ennemies")]

    [SerializeField] private float minLife;
    [SerializeField] private float maxLife;
    [SerializeField] private float minDamage;
    [SerializeField] private float maxDamage;
    [SerializeField] private float minDefense;
    [SerializeField] private float maxDefense;

    private float damage;
    private float defense;
    private float dmgSubis;
    private float life;

    [HideInInspector] public Spells m_enemySpell;

    private void Awake()
    {
        life = Random.Range(minLife, maxLife);
        damage = Random.Range(minDamage, maxDamage);
        defense = Random.Range(minDefense, maxDefense);
        GetComponentInChildren<LifeBarHandler>().SetMaxHealth(life);
        CreateSpell(m_enemySpell);
    }

    //Lorsque l'ennemi subis des d�g�ts
    public void GetDamage(Spells target)
    {
        ApplyDamage(target.SpellBasicDamage);
    }

    //D�g�ts re�us par un sort
    public void ApplyDamage(float damage)
    {
        //D�g�ts r�duits en fonction de la d�fense
        //Formule ou on divise par 100 defense pour avoir sa valeur de 0 � 1
        //puis pour les d�g�ts on multiplie d�g�ts - la d�fense pour que le sort fasse moins de d�g�ts

        defense = defense / 100;

        damage = damage * (1 - defense);

        life -= damage;

        dmgSubis = damage;

        GetComponentInChildren<LifeBarHandler>().SetHealth(life);

        CombatManager.instance.delTurn(false);

        if (life <= 0)
        {
            //Si le mob meurt on le retire de la list afin que le prochain sort s�lectionn� par un monstre � envoyer au joueur ne soit pas une error
            AiManager.instance.mobGroup1.Remove(this);
            AiManager.instance.CheckMobGroup();
            Destroy(gameObject);
        }
    }

    public void CreateSpell(Spells newSpell)
    {
        newSpell = new Spells(null, "Fireball", "", 0, Random.Range(5, 10));

        //Puis on demande un sort random
        //AttributeRandomSpell(newSpell);
        m_enemySpell = newSpell;
        Debug.Log(m_enemySpell.SpellName);

    }

    //Pour le moment pas besoin de sorts randoms
    public void AttributeRandomSpell(Spells randomSpell)
    {
    }

    //No need cause no character to target
    public void CreateSpell(Spells newSpell, Character target)
    {
        throw new System.NotImplementedException();
    }
}
