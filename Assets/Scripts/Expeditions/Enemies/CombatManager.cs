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
        //cr�ation mini-singleton
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
            Debug.Log("d�marrage du combat: ");
            UiManagerSession.instance.CombatUi();
        }
        else
        {
            Debug.LogError("Quelque chose � lanc� un combat mais la value est � false");
        }
        return boolValue;
    }
}
