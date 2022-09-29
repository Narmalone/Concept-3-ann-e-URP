using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class ManaManager : MonoBehaviour
{
    public static ManaManager instance { get; private set; }
    [SerializeField] private UIDocument combatUiDoc;
    VisualElement uiDocRef;
    public int currentEneryTourCount = 0;
    private int ennergyTourCount = 0;
    private int maxEnergie = 16;
    public int currentEnergie;
    private List<Character> characters;
    List<Button> globalListToCheck;
    private void Awake()
    {
        instance = this;
    }

    public void SetFirstTurn()
    {
        ennergyTourCount = 5;
        currentEneryTourCount = ennergyTourCount;
        currentEnergie = currentEneryTourCount;
    }
    public void NextTurn()
    {
        //Quand prochain tour énergie max + 2
        //Et set l'énergie actuelle du joueur à l'énergie par tour
        ennergyTourCount += 2;
        currentEneryTourCount = ennergyTourCount;
        currentEnergie = currentEneryTourCount;
        UpdateManaUi();
    }
    public void UpdateManaUi()
    {
        uiDocRef = combatUiDoc.rootVisualElement;
        Label energyTour = uiDocRef.Q<Label>("ManaTurn");
        Label currentEnergy = uiDocRef.Q<Label>("PlayersMana");

        energyTour.text = "TOTAL ÉNERGIE: " + currentEneryTourCount.ToString();
        currentEnergy.text = "ÉNERGIE: " + currentEnergie.ToString();
    }
    public void CheckEnergy(List<Spell> target, List<Button> buttonList, Button BtnSpellUsed)
    {
        characters = UiManagerSession.instance.m_characters;
        List<Button> buttonToDisable = new List<Button>();
        List<Button> buttonToEnable = new List<Button>();

        //Si l'énergie par tour est au max
        if (currentEneryTourCount >= maxEnergie)
        {
            currentEneryTourCount = maxEnergie;  
        }
        
        for(int i = 0; i < buttonList.Count; i++)
        {
            if (target[i].Cost <= currentEneryTourCount)
            {
                if(BtnSpellUsed != null)
                {
                    if (BtnSpellUsed == buttonList[i])
                    {
                        buttonToDisable.Add(buttonList[i]);
                    }
                    else
                    {
                        buttonToEnable.Add(buttonList[i]);
                    }
                }
                else
                {
                    buttonToEnable.Add(buttonList[i]);
                }

            }
            else if (target[i].Cost > currentEneryTourCount)
            {
                buttonToDisable.Add(buttonList[i]);
            }
        }
        DisableButtonList(buttonToDisable);
        EnableButtonList(buttonToEnable);
    }

    public void DisableButtonList(List<Button> buttons)
    {
        foreach(Button btn in buttons)
        {
            btn.visible = false;
            //btn.SetEnabled(false);
        }
    }
    public void EnableButtonList(List<Button> buttons)
    {
        foreach(Button btn in buttons)
        {
            btn.visible = true;
            //btn.SetEnabled(true);
        }
    }

    public void UsedEnergy(Spell spell)
    {
        currentEnergie -= spell.Cost;
        currentEneryTourCount = currentEnergie;
    }

}
