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
    #endregion

    public bool canSelectMob = false;
    private void Awake()
    {
        //cr�ation mini-singleton
        if(instance != null)
        {
            return;
        }
        instance = this;

        OnStartCombat = StartCombat;
        delCombat = InCombat;
    }

    //Si le joueur est en combat
    private bool StartCombat(bool boolValue)
    {
        if (boolValue)
        {
            Debug.Log("d�marrage du combat: " + boolValue);
            UiManagerSession.instance.CombatUi();
        }
        else
        {
            Debug.LogError("Quelque chose � lanc� un combat mais la value est � false ? " + boolValue);
        }
        return boolValue;
    }

    private bool InCombat(bool boolValue)
    {
        if (boolValue)
        {
            Debug.Log("Joueur ne peut plus se d�placer car il est en combat: " + boolValue);
        }
        else
        {
            Debug.LogError("Le joueur ne peut pas se d�placer mais il n'est plus en combat ? " + boolValue);
        }
        return boolValue;
    }
}
