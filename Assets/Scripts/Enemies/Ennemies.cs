using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Ennemies : MonoBehaviour
{
    private float life;
    private float damage;
    private float defense;
    public Spells m_enemySpell;

    private void Awake()
    {
        life = Random.Range(0, 1);
        damage = Random.Range(15, 20);
        defense = Random.Range(5, 10);
        m_enemySpell = SpellsManager.instance.RandomSpell;
    }

    //Lorsque l'ennemi subis des dégâts
    public void GetDamage(Spells target)
    {
        ApplyDamage(target.SpellBasicDamage);

        //Désactiver le bouton sort que le joueur à lancé
        UiManagerSession.instance.SpellUsed();

        //le joueur a lancé un sort donc ce n'est plus son tour
        CombatManager.instance.delTurn(false);
    }

    //Dégâts reçus par un sort
    public void ApplyDamage(float damage)
    {
        //Dégâts réduits en fonction de la défense
        //Formule ou on divise par 100 defense pour avoir sa valeur de 0 à 1
        //puis pour les dégâts on multiplie dégâts - la défense pour que le sort fasse moins de dégâts

        defense = defense / 100;
        damage = damage * (1 - defense);
        Debug.Log("Dégâts subis: " + damage);
        Debug.Log("défense de l'ennemi: " + defense * 100);

        life -= damage;

        UiManagerSession.instance.UpdateCombatUi();
        CombatManager.instance.delTurn(false);
        if(life <= 0)
        {
            //Si le mob meurt on le retire de la list afin que le prochain sort sélectionné par un monstre à envoyer au joueur ne soit pas une error
            AiManager.instance.mobGroup1.Remove(this);
            Destroy(gameObject);
        }

    }
}
