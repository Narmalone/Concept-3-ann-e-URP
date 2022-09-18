using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    #region delegates
    public static CombatManager instance { get; private set; }

    public delegate bool ActionDelegate(bool boolValue);

    public ActionDelegate OnStartCombat;
    public ActionDelegate delCombat;
    public ActionDelegate delLeaveCombat;

    public ActionDelegate delTurn;
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

        //delegates du joueur combat 
        OnStartCombat = StartCombat;
        delCombat = InCombat;
        delLeaveCombat = EndCombat;

        delTurn = IsPlayerTurn;
    }

    //Si le joueur est en combat
    private bool StartCombat(bool boolValue)
    {
        if (boolValue)
        {
            Debug.Log("démarrage du combat: " + boolValue);

            //Si combat commence alors c'est le tour du joueur
            delTurn(true);
            delCombat(true);
            UiManagerSession.instance.CombatUi();
        }
        else
        {
            Debug.LogError("Quelque chose à lancé un combat mais la value est à false ? " + boolValue);
        }
        return boolValue;
    }

    private bool InCombat(bool boolValue)
    {
        if (boolValue)
        {
            FindObjectOfType<PlayerControllerCity>().enabled = false;
            Debug.Log("Joueur ne peut plus se déplacer car il est en combat: " + boolValue);
        }
        else
        {
            Debug.LogError("Le joueur ne peut pas se déplacer mais il n'est plus en combat ? " + boolValue);
        }
        return boolValue;
    }

    private bool EndCombat(bool boolValue)
    {
        if (boolValue)
        {
            FindObjectOfType<PlayerControllerCity>().enabled = true;

            delTurn(false);

            //Info ui de reward

            Debug.Log("Le joueur a terminé le combat: " + boolValue);
        }
        else
        {
            Debug.LogError("Le joueur a terminé le combat mais il est en combat ? " + boolValue);
        }
        return boolValue;
    }

    //Vérifier si c'est au tour du joueur
    //Sinon désactiver tous les boutons dans l'interfaces du joueur
    //Si le spell a déjà été utilisé le spell resteras bloqué
    private bool IsPlayerTurn(bool boolValue)
    {
        if (boolValue)
        {
            //Si tour du joueur
            //Interface joueur activée et l'IA ne joue pas
            UiManagerSession.instance.PlayerTurn();
        }
        else
        {
            //si ce n'est pas le temps du joueur
            //désactiver l'interface puis c'est au tour de l'IA
            UiManagerSession.instance.NotPlayerTurn();

            //Ai manager pour faire jouer l'IA
            AiManager.instance.ChoiceSpellAgainstPlayer();
        }
        return boolValue;
    }

}
