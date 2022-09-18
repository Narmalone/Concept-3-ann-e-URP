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

    //Lorsque l'ennemi subis des d�g�ts
    public void GetDamage(Spells target)
    {
        ApplyDamage(target.SpellBasicDamage);

        //D�sactiver le bouton sort que le joueur � lanc�
        UiManagerSession.instance.SpellUsed();

        //le joueur a lanc� un sort donc ce n'est plus son tour
        CombatManager.instance.delTurn(false);
    }

    //D�g�ts re�us par un sort
    public void ApplyDamage(float damage)
    {
        //D�g�ts r�duits en fonction de la d�fense
        //|V1 - V2|/ [(V1 + V2)/2] � 100 Warning: pas la bonne formule
        //Final value - initial value / initial value * 100
        damage = (damage - defense / defense) * 100;
        Debug.Log("D�g�ts subis: " + damage);
        Debug.Log("d�fense de l'ennemi: " + defense);

        life -= damage;

        UiManagerSession.instance.UpdateCombatUi();
        CombatManager.instance.delTurn(false);
        if(life <= 0)
        {
            //Si le mob meurt on le retire de la list afin que le prochain sort s�lectionn� par un monstre � envoyer au joueur ne soit pas une error
            AiManager.instance.mobGroup1.Remove(this);
            Destroy(gameObject);
        }

    }
}
