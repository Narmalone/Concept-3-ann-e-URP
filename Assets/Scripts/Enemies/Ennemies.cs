using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ennemies : MonoBehaviour, ISpell
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

    //Lorsque l'ennemi subis des dégâts
    public void GetDamage(Spells target)
    {
        ApplyDamage(target.SpellBasicDamage);
    }

    //Dégâts reçus par un sort
    public void ApplyDamage(float damage)
    {
        //Dégâts réduits en fonction de la défense
        //Formule ou on divise par 100 defense pour avoir sa valeur de 0 à 1
        //puis pour les dégâts on multiplie dégâts - la défense pour que le sort fasse moins de dégâts

        defense = defense / 100;

        damage = damage * (1 - defense);

        life -= damage;

        dmgSubis = damage;

        GetComponentInChildren<LifeBarHandler>().SetHealth(life);

        CombatManager.instance.delTurn(false);

        if (life <= 0)
        {
            //Si le mob meurt on le retire de la list afin que le prochain sort sélectionné par un monstre à envoyer au joueur ne soit pas une error
            AiManager.instance.mobGroup1.Remove(this);
            AiManager.instance.CheckMobGroup();
            Destroy(gameObject);
        }
    }

    public void SetDamage(Spells target)
    {
        //V1 = V1 * V2 / V2
        target.SpellBasicDamage = target.SpellBasicDamage * damage / target.SpellBasicDamage;
    }

    #region interface ISpell
    public void CreateSpell(Spells newSpell)
    {
        newSpell = new Spells(null, "Fireball", "", 0, Random.Range(5, 10));

        //Un moment mettre la fonction qui permet d'avoir un truc random ?

        m_enemySpell = newSpell;

        SetDamage(newSpell);
        Debug.Log(m_enemySpell.SpellName);
    }

    //Pour le moment pas besoin de sorts randoms
    public void AttributeRandomSpell(Spells randomSpell)
    {
        throw new System.NotImplementedException();
    }

    //No need cause no character to target
    public void CreateSpell(Spells newSpell, Character target)
    {
        throw new System.NotImplementedException();
    }

    public void AttributeRandomSpell(Spells randomSpell, Character target)
    {
        throw new System.NotImplementedException();
    }
    #endregion
}
