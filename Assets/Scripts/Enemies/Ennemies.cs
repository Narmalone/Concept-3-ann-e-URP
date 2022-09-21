using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ennemies : MonoBehaviour
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
    }

    private void Start()
    {
        m_enemySpell = new Spells(null, "Fireball", "Boule de feu", 0, Random.Range(5, 15));
        Debug.Log(m_enemySpell.SpellName);
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
}
