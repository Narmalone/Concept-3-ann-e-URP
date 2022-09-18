using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    #region delegates
    public static CombatManager instance { get; private set; }

    public delegate bool ActionDelegate(bool boolValue);
    public delegate void FunctionDelegate(Character target, Spells fromSpell);

    public ActionDelegate OnStartCombat;
    public FunctionDelegate OnAttack;
    #endregion

    public bool canSelectMob = false;
    private void Awake()
    {
        //création mini-singleton
        if(instance != null)
        {
            return;
        }
        instance = this;

        OnStartCombat = StartCombat;
        OnAttack = CharactersAttack;
    }

    public void CharactersAttack(Character target, Spells fromSpell)
    {

    }

    //Si le joueur est en combat
    private bool StartCombat(bool boolValue)
    {
        if (boolValue)
        {
            Debug.Log("démarrage du combat: ");
            UiManagerSession.instance.CombatUi();
        }
        else
        {
            Debug.LogError("Quelque chose à lancé un combat mais la value est à false");
        }
        return boolValue;
    }

    // TO DO: When players select the spell, the cursor shown = the spell texture and if the players cliqued on a raycastable gameobject like enemies
    //The spell is launched and the visual element is disabled

    public void SelectEnemies()
    {
        
    }
}
