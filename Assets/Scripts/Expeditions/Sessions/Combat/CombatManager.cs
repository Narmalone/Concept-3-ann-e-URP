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

    public ActionDelegate delTurn;
    #endregion

    public bool canSelectMob = false;
    public bool isFighting = false;

    public List<Character> m_ourTeam;
    public List<Character> m_ennemyTeam;

    //Spell qui correspond au spell du boutton que le joueur a cliqué
    private Spell m_currentSpellSelected;

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
            isFighting = true;
            FindObjectOfType<PlayerControllerCity>().enabled = false;
            Debug.Log("Joueur ne peut plus se déplacer car il est en combat: " + boolValue);
        }
        else
        {
            //Si joueur plus en combat on lui redonne le contrôle
            isFighting = false;
            FindObjectOfType<PlayerControllerCity>().enabled = true;
            delTurn(false);
            UiManagerSession.instance.NotPlayerTurn();
            Debug.Log("Le joueur n'est plus en combat");
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

            if (isFighting)
            {
                //Ai manager pour faire jouer l'IA
                AiManager.instance.MerFunction();
            }
        }
        return boolValue;
    }

    public void StartCorou()
    {
        StartCoroutine(CorouBeforeTurn());
    }

    public IEnumerator CorouBeforeTurn()
    {
        yield return new WaitForSeconds(2);

        if (isFighting)
        {
            //Désactiver le bouton sort que le joueur à lancé
            //SpellsManager.instance.CastSpell(spellToCast);
            UiManagerSession.instance.SpellUsed();
            delTurn(true);
        }
       
    }

}
