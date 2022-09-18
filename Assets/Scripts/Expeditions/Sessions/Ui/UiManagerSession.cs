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
    private Button Spell2;
    private Button Spell3;


    private Button m_spellUsed;
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
        Spell3 = rootElement.Q<Button>("BSpell3");
        Spell3.clickable.clicked += ThirdSpellCliqued;

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
        CombatManager.instance.canSelectMob = true;
        PlayerAttack.instance.m_currentSpellSelected = m_characters[3].CurrentCharaSpell;
    }

    //Lorsque le joueur utilise un sort doit désactiver le bouton -> lancé depuis classe ennemy dans GetDamage
    public void SpellUsed()
    {
        m_spellUsed.SetEnabled(false);
        CombatManager.instance.canSelectMob = false;
    }

    #endregion
    // TO DO: Fonction update la bar d'UI lancé depuis la classe enemy dans ApplyDamage
    //Actuellement non fonctionnelle car on display vie de nos persos au lieux de celui de l'ennemi touché
    //Barre de vie 3D ? Par l'ancienne interface ?
    public void UpdateCombatUi()
    {
        var rootElement = m_uiDocCombat.rootVisualElement;
        lifeBar = rootElement.Q<ProgressBar>("LifeBar");

        characterLifePercent = (m_characters[0].CharactersLife / maxPlayerHP) * 100;
        lifeBar.SetValueWithoutNotify(characterLifePercent);
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
