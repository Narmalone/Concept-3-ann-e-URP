using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class UiManagerSession : MonoBehaviour
{
    #region delegates
    public static UiManagerSession instance { get; private set; }

    public delegate void ActiveActionDelegate();

    public ActiveActionDelegate CombatUi;
    public ActiveActionDelegate delUpdateCombatUi;
    #endregion

    [SerializeField] private UIDocument m_uiDocCombat;
    private List<Character> m_characters;
    private List<Character> m_enemiesCharacter;

    //Noms des sorts
    private Label SpellName1;
    private Label SpellName2;
    private Label SpellName3;
    private Label SpellName4;

    //dégâts des sorts
    private Label SpellDamage1;
    private Label SpellDamage2;

    //Boutons des sorts
    private Button Spell1;

    //Vie des entités
    private ProgressBar lifeBar;

    //Floats des vies du joueurs
    private float maxPlayerHP;
    private float characterLifePercent;

    [SerializeField] private Texture2D fireball;
    [SerializeField] private Texture2D snowBall;
    private void Awake()
    {
        if(instance != null)
        {
            return;
        }
        instance = this;
      
        CombatUi = UiCombat;
        SetDisabledUi();
    }

    private void Start()
    {
        m_characters = new List<Character>();

        m_characters = LoadPlayerTeam.instance.m_charactersTeam;

        Debug.Log(m_characters[0].CharactersName);
    }
    public void SetDisabledUi()
    {
        var rootElement = m_uiDocCombat.rootVisualElement;
        rootElement.style.display = DisplayStyle.None;
    }
    public void UiCombat()
    {
        var rootElement = m_uiDocCombat.rootVisualElement;
        rootElement.style.display = DisplayStyle.Flex;

        SpellName1 = rootElement.Q<Label>("SpellName1");
        SpellName1.text = m_characters[0].CurrentCharaSpell.SpellName;
       
        SpellDamage1 = rootElement.Q<Label>("SpellDamage1");
        SpellDamage1.text = m_characters[0].CurrentCharaSpell.SpellBasicDamage.ToString();

        Spell1 = rootElement.Q<Button>("BSpell1");
       
        Spell1.clickable.clicked += FirstSpellCliqued;

        lifeBar = rootElement.Q<ProgressBar>("LifeBar");

        //Peut-être essayer de bind le visual element
        if (lifeBar != null)
        {
            //Formule mathématique de conversion
            //(currentValue / maxValue) * oneHundred = myPercentage;

            maxPlayerHP = m_characters[0].CharactersLife;
            characterLifePercent = (m_characters[0].CharactersLife / maxPlayerHP) * 100;
            lifeBar.SetValueWithoutNotify(characterLifePercent);
            lifeBar.title = "Vie";
        }
        else
        {
            Debug.Log("life bar pas trouvée");
        }
    }

    //To DO: Quand le spell est lancé -> joueur doit cliquer sur un ennemie et lui faire des dégâts
    //To do: Désactiver le visual element pendant X tour

    //Changer cette fonction
    public void FirstSpellCliqued()
    {
        CombatManager.instance.canSelectMob = true;
        PlayerAttack.instance.m_currentSpellSelected = m_characters[0].CurrentCharaSpell;
    } 
    
    public void SecondSpellCliqued()
    {
        CombatManager.instance.canSelectMob = true;
    } 
    
    public void ThirdSpellCliqued()
    {
        CombatManager.instance.canSelectMob = true;
    }
    
    public void FourSpellCliqued()
    {
        CombatManager.instance.canSelectMob = true;
    }

    public void UpdateCombatUi()
    {
        var rootElement = m_uiDocCombat.rootVisualElement;
        lifeBar = rootElement.Q<ProgressBar>("LifeBar");

        characterLifePercent = (m_characters[0].CharactersLife / maxPlayerHP) * 100;
        lifeBar.SetValueWithoutNotify(characterLifePercent);
    }
   
}
