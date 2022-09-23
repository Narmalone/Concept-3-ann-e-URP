using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class UiManagerSession : MonoBehaviour
{
    public static UiManagerSession instance { get; private set; }

    #region delegates

    public delegate void ActiveActionDelegate();

    public ActiveActionDelegate CombatUi;
    public ActiveActionDelegate delUpdateCombatUi;
    #endregion
    [Header("REFERENCES")]
    [SerializeField] private UIDocument m_uiDocCombat;
    [SerializeField] private VisualTreeAsset m_spellTemplate;
    private List<Button> m_buttons;
    private int index = 0;

    private List<Character> m_characters;
    public List<Spell> spellList;

    private List<Character> m_enemiesCharacter;

    //Noms des sorts
    private Label SpellName1;
    private Label SpellName2;
    private Label SpellName3;
    private Label SpellName4;

    //dégâts des sorts
    private Label SpellDamage1;
    private Label SpellDamage2;
    private Label SpellDamage3;
    private Label SpellDamage4;

    //Boutons des sorts
    private Button Spell1;
    private Button Spell2;
    private Button Spell3;
    private Button Spell4;

    // bouton du sort actuel -> équivaut au boutton Spell1, spell2, spell3 ou spell4
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
        m_buttons = new List<Button>();
        spellButtons = new List<Button>();
    }

    private void Start()
    {
        m_characters = LoadPlayerTeam.instance.m_charactersTeam;

        foreach(Character chara in m_characters)
        {
            chara.CurrentCharaSpell = SpellsManager.instance.GetSpellById(chara.m_spellID);
            Debug.Log(chara.CurrentCharaSpell.Damage);
        }
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

        GroupBox container = rootElement.Q<GroupBox>("MainGroupBox");

        //Génération de l'interface
        foreach(Character chara in m_characters)
        {
            //Instantier les templates en fonction du nombre de personnage (boucle for marche aussi)
            VisualElement ui = m_spellTemplate.CloneTree();

            Button btn = ui.Q<Button>();

            btn.name = "BSpell" + index;

            //Ajouter le bouton dans une liste de bouton pour redéfinir dans le OnSpellCliqued
            m_buttons.Add(btn);

            //Chercher le boutton et lui rajouter une clickable la fonction clickable
            //            btn.clickable.clicked += OnSpellCliqued;
            btn.clickable.clicked += () =>
            {
                CheckButtonCliqued(btn);
                //Debug.Log(btn.name);
            };

            //ajouter le sort du personnage actuel (le premier visual crée correspond à son premier sort)
            spellList.Add(chara.CurrentCharaSpell);

            //ajouter au container la template
            container.Add(ui);

            index++;
        }
    }

    public void CheckButtonCliqued(Button btn)
    {
        //Empêcher de le faire comme ça car on est dans un foreach
        if(btn == m_buttons[0])
        {
            m_characters[0].CurrentCharaSpell.CastSpell(spellList[0]);
        }
        Debug.Log(btn.name);
    }
    #region Spell
    public void FirstSpellCliqued()
    {
        m_spellUsed = Spell1;
        CombatManager.instance.canSelectMob = true;
        PlayerAttack.instance.m_currentSpellelected = m_characters[0].CurrentCharaSpell;
    } 
    
    public void SecondSpellCliqued()
    {
        m_spellUsed = Spell2;
        CombatManager.instance.canSelectMob = true;
        PlayerAttack.instance.m_currentSpellelected = m_characters[1].CurrentCharaSpell;
    }

    public void ThirdSpellCliqued()
    {
        m_spellUsed = Spell3;
        CombatManager.instance.canSelectMob = true;
        PlayerAttack.instance.m_currentSpellelected = m_characters[2].CurrentCharaSpell;
    }

    public void FourSpellCliqued()
    {
        m_spellUsed = Spell4;
        CombatManager.instance.canSelectMob = true;
        PlayerAttack.instance.m_currentSpellelected = m_characters[3].CurrentCharaSpell;
    }

    public void SpellUsed()
    {

        //Si le personnage rest spellused == null donc on return
        if(m_spellUsed == null) { return; }

        spellButtons.Add(m_spellUsed);
        
        //Vérifier chaque bouton dans la list si ils y sont désactiver les boutons
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
