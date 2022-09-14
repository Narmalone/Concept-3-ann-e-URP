using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance { get; private set; }

    public delegate bool ActionDelegate(bool boolValue);

    public ActionDelegate OnStartCombat;

    private void Awake()
    {
        //création mini-singleton
        if(instance != null)
        {
            return;
        }
        instance = this;

        OnStartCombat = StartCombat;
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
}
