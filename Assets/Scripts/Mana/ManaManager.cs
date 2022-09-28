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
    public int energieTourCount = 0;
    private int maxEnergie = 16;
    public int currentEnergie;
    private List<Character> characters;
    private void Awake()
    {
        instance = this;
    }

    public void SetFirstTurn()
    {
        energieTourCount = 5;
        currentEnergie = energieTourCount;
    }
    public void NextTurn()
    {
        //Quand prochain tour énergie max + 2
        //Et set l'énergie actuelle du joueur à l'énergie par tour
        energieTourCount += 2;
        currentEnergie = energieTourCount;
        UpdateManaUi();
        Debug.Log(energieTourCount);
    }
    public void UpdateManaUi()
    {
        uiDocRef = combatUiDoc.rootVisualElement;
        Label energyTour = uiDocRef.Q<Label>("ManaTurn");
        Label currentEnergy = uiDocRef.Q<Label>("PlayersMana");

        energyTour.text = "TOTAL MANA: " + energieTourCount.ToString();
        currentEnergy.text = "MANA: " + currentEnergie.ToString();
    }
    public void CheckEnergy(List<Spell> target, List<Button> buttonList)
    {
        characters = UiManagerSession.instance.m_characters;

        List<Button> buttonToDisable = new List<Button>();
        List<Button> buttonToEnable = new List<Button>();

        //Si l'énergie par tour est au max
        if (energieTourCount >= maxEnergie)
        {
            energieTourCount = maxEnergie;  
        }

        for(int i = 0; i < buttonList.Count; i++)
        {
            if (target[i].Cost <= energieTourCount)
            {
                buttonToEnable.Add(buttonList[i]);
            }
            else if (target[i].Cost > energieTourCount)
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
            btn.SetEnabled(false);
        }
    }
    public void EnableButtonList(List<Button> buttons)
    {
        foreach(Button btn in buttons)
        {
            btn.SetEnabled(true);
        }
    }

    public void UsedEnergy(Spell spell)
    {
        currentEnergie -= spell.Cost;
        Debug.Log(currentEnergie);
    }

}
