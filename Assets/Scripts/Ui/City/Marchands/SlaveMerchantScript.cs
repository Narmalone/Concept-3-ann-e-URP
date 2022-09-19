using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using Random = UnityEngine.Random;
public class SlaveMerchantScript : MonoBehaviour
{
    [Header("UiDocument references")]
    [SerializeField] private UIDocument m_uiSlaveMerchant;
    [SerializeField] private UIDocument m_mainMenuDoc;

    //Labels
    //First slot
    private Label charaName1;
    private Label charaLife1;
    private Label charaDamage1;
    private Label charaDefense1;
    private Label charaRarity1;

    //Buttons
    private Button backButton;

    //First Slot
    private Button buyChara1;

    //Variables pour g�nerer les valeurs
    private float fCharaLife1;
    private float fCharaDamage1;
    private float fCharaDefense1;
    private Spells defaultSpell;

    //Couts du personnage
    private Label costDisplayed;
    private int goldCost;

    //Personnages g�n�r�s
    private Character m_firstGenerated;
    
    private void Start()
    {
        var rootElement = m_uiSlaveMerchant.rootVisualElement;

        backButton = rootElement.Q<Button>("BMenu");
        backButton.clickable.clicked += GoBack;

        buyChara1 = rootElement.Q<Button>("Cost1");
        buyChara1.clickable.clicked += BuyFirstCharacter;
        GenerateCharacters();
    }

    //g�n�rer les personnages � chaque r�initialisation de la boutique
    public void GenerateCharacters()
    {
        //g�n�ration du premier character
        fCharaLife1 = Random.Range(40,50);
        fCharaDamage1 = Random.Range(20, 30);
        fCharaDefense1 = Random.Range(10, 15);
        defaultSpell = SpellsManager.instance.RandomSpell;
        goldCost = Random.Range(150, 200);
        m_firstGenerated = new Character("default", fCharaLife1, fCharaDamage1, fCharaDefense1, 0, defaultSpell);

        UpdateUi();
    }

    public void UpdateUi()
    {
        var rootElement = m_uiSlaveMerchant.rootVisualElement;

        //Update du premier personnage
        charaLife1 = rootElement.Q<Label>("Life1");
        charaLife1.text = "Life: " + fCharaLife1.ToString();

        charaDamage1 = rootElement.Q<Label>("Damage1");
        charaDamage1.text = "Damage: " + fCharaDamage1.ToString();

        charaDefense1 = rootElement.Q<Label>("Defense1");
        charaDefense1.text = "Defense: " + fCharaDefense1.ToString();

        buyChara1 = rootElement.Q<Button>("Cost1");
        buyChara1.text = "COST: " + goldCost.ToString();
    }

    //Quand un personnage est achet� -> D�sactiver le visual element s�lectionn�
    public void BuyFirstCharacter()
    {
        var rootElement = m_uiSlaveMerchant.rootVisualElement;

        if(GoldText.instance.m_playerGoldCount >= goldCost)
        {
            GoldText.instance.m_playerGoldCount -= goldCost;
            var CharaSlot = rootElement.Q<GroupBox>("Chara1");
            CharaSlot.style.display = DisplayStyle.None;
            charactersOfThePlayer.instance.m_playerTeam.Add(m_firstGenerated);
            charactersOfThePlayer.instance.UpdatePlayerTeam();
        }
        else
        {
            Debug.Log("Vous n'avez pas assez d'argent");
        }
    }

    //To DO: si un des personnages a �t� achet�s on r�initialise
    public void TimeAndReload()
    {
        //TO DO: R�initialiser en relan�ant le Generate Characters apr�s X temps
    }

    //Quand le joueur appuis sur le bouton retour
    private void GoBack()
    {
        var rootElement = m_uiSlaveMerchant.rootVisualElement;
        rootElement.style.display = DisplayStyle.None;

        var uiDoc = m_mainMenuDoc.rootVisualElement;
        uiDoc.style.display = DisplayStyle.Flex;
    }

}
