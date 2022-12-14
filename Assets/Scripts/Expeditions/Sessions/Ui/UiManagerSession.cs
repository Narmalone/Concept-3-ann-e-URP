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

    //d?g?ts des sorts
    private Label SpellDamage1;
    private Label SpellDamage2;
    private Label SpellDamage3;
    private Label SpellDamage4;

    //Boutons des sorts
    private Button Spell1;
    private Button Spell2;
    private Button Spell3;
    private Button Spell4;

    // bouton du sort actuel -> ?quivaut au boutton Spell1, spell2, spell3 ou spell4
    private Button m_spellUsed;
    private Button m_restButton;

    public List<Button> spellButtons;
    public List<Button> buttonsToActivate;

    [HideInInspector] public bool isResting = false;

    private void Awake()
    {
        if(instance != null)
        {
            return;
        }
        instance = this;
      
        CombatUi = UiCombat;
        SetDisabledUi();

        buttonsToActivate = new List<Button>();
        spellButtons = new List<Button>();
    }

    private void Start()
    {
        m_characters = new List<Character>();
        m_characters = LoadPlayerTeam.instance.m_charactersTeam;
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

        #region SpellsButton
        //Spell 1
        SpellName1 = rootElement.Q<Label>("SpellName1");
        SpellName1.text = m_characters[0].CurrentCharaSpell.SpellName;
       
        SpellDamage1 = rootElement.Q<Label>("SpellDamage1");
        SpellDamage1.text = m_characters[0].CurrentCharaSpell.SpellBasicDamage.ToString();

        Spell1 = rootElement.Q<Button>("BSpell1");
        Spell1.clickable.clicked += FirstSpellCliqued;

        //Spell 2
        SpellName2 = rootElement.Q<Label>("SpellName2");
        SpellName2.text = m_characters[1].CurrentCharaSpell.SpellName;

        SpellDamage2 = rootElement.Q<Label>("SpellDamage2");
        SpellDamage2.text = m_characters[1].CurrentCharaSpell.SpellBasicDamage.ToString();

        Spell2 = rootElement.Q<Button>("BSpell2");
        Spell2.clickable.clicked += SecondSpellCliqued;

        //Spell 3
        SpellName3 = rootElement.Q<Label>("SpellName3");
        SpellName3.text = m_characters[2].CurrentCharaSpell.SpellName.ToString();

        SpellDamage3 = rootElement.Q<Label>("SpellDamage3");
        SpellDamage3.text = m_characters[2].CurrentCharaSpell.SpellBasicDamage.ToString();

        Spell3 = rootElement.Q<Button>("BSpell3");
        Spell3.clickable.clicked += ThirdSpellCliqued;

        //Spell 4
        SpellName4 = rootElement.Q<Label>("SpellName4");
        SpellName4.text = m_characters[3].CurrentCharaSpell.SpellName.ToString();

        SpellDamage4 = rootElement.Q<Label>("SpellDamage4");
        SpellDamage4.text = m_characters[3].CurrentCharaSpell.SpellBasicDamage.ToString();

        Spell4 = rootElement.Q<Button>("BSpell4");
        Spell4.clickable.clicked += FourSpellCliqued;
        #endregion
        m_restButton = rootElement.Q<Button>("BRest");
        m_restButton.clickable.clicked += OnRestButtonCliqued;


        buttonsToActivate.Add(Spell1);
        buttonsToActivate.Add(Spell2);
        buttonsToActivate.Add(Spell3);
        buttonsToActivate.Add(Spell4);
    }

 
    #region Spells
    public void FirstSpellCliqued()
    {
        m_spellUsed = Spell1;
        CombatManager.instance.canSelectMob = true;
        PlayerAttack.instance.m_currentSpellSelected = m_characters[0].CurrentCharaSpell;
    } 
    
    public void SecondSpellCliqued()
    {
        m_spellUsed = Spell2;
        CombatManager.instance.canSelectMob = true;
        PlayerAttack.instance.m_currentSpellSelected = m_characters[1].CurrentCharaSpell;
    }

    public void ThirdSpellCliqued()
    {
        m_spellUsed = Spell3;
        CombatManager.instance.canSelectMob = true;
        PlayerAttack.instance.m_currentSpellSelected = m_characters[2].CurrentCharaSpell;
    }

    public void FourSpellCliqued()
    {
        m_spellUsed = Spell4;
        CombatManager.instance.canSelectMob = true;
        PlayerAttack.instance.m_currentSpellSelected = m_characters[3].CurrentCharaSpell;
    }

    public void SpellUsed()
    {

        //Si le personnage rest spellused == null donc on return
        if(m_spellUsed == null) { return; }

        spellButtons.Add(m_spellUsed);
        
        //V?rifier chaque bouton dans la list si ils y sont d?sactiver les boutons
        foreach (Button btn in spellButtons)
        {
            btn.SetEnabled(false);
        }

        CombatManager.instance.canSelectMob = false;
    }

    #endregion

    //QUAND ON REST LA FONCTION SPELLUSED EST APPELE DONC YA UNE ERREUR
    private void OnRestButtonCliqued()
    {
        m_spellUsed = null;

        spellButtons = new List<Button>();

        foreach(Button btn in buttonsToActivate)
        {
            btn.SetEnabled(true);
        }

        CombatManager.instance.delTurn(false);
    }

    #region Ui par rapport aux tours du joueur
    public void NotPlayerTurn()
    {
        var rootElement = m_uiDocCombat.rootVisualElement;
        rootElement.style.display = DisplayStyle.None;
    }

    public void PlayerTurn()
    {
        var rootElement = m_uiDocCombat.rootVisualElement;
        rootElement.style.display = DisplayStyle.Flex;
    }
    #endregion
}
