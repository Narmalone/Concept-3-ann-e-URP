using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
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

    //tout ce qui est en rapport avec les personnages
    public List<Character> m_characters;
    public List<Spell> spellList;
    private Spell currentSpellCliqued;
    private List<Label> m_spellsDescriptions;

    // bouton du sort actuel -> équivaut au boutton Spell1, spell2, spell3 ou spell4
    private Button m_spellUsed;
    private Button m_restButton;

    public List<Button> spellButtons;
    public List<Button> buttonsToActivate;    

    [HideInInspector] public bool isResting = false;
    private bool isUiCreated = false;

    [SerializeField] private Texture2D SpellImageUsed;
    [SerializeField] private Texture2D RestImage;
    private void Awake()
    {
        if(instance != null)
        {
            return;
        }
        instance = this;
      
        CombatUi = UiCombat;
        SetDisabledUi();

        m_buttons = new List<Button>();
        spellButtons = new List<Button>();
    }

    private void Start()
    {
        m_characters = LoadPlayerTeam.instance.m_charactersTeam;

        foreach (Character chara in m_characters)
        {
            chara.CurrentCharaSpell = SpellsManager.instance.GetSpellById(chara.m_spellID);
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

        m_restButton = rootElement.Q<Button>("BRest");
        m_restButton.clickable.clicked += OnRestButtonCliqued;
        m_restButton.style.backgroundImage = RestImage;

        GroupBox container = rootElement.Q<GroupBox>("MainGroupBox");

        Label energyTour = rootElement.Q<Label>("ManaTurn");
        Label currentEnergy = rootElement.Q<Label>("PlayersMana");

        ManaManager.instance.UpdateManaUi();

        index = 0;

        if (isUiCreated) return;

        //TO DO SI UN DES PERSONNAGES MEURT IL FAuT ACTUATLISER TOTALEMENT LES SORTS

        //Génération de l'interface
        foreach (Character chara in m_characters)
        {
            //Instantier les templates en fonction du nombre de personnage (boucle for marche aussi)
            VisualElement ui = m_spellTemplate.CloneTree();

            Button btn = ui.Q<Button>();

            IMGUIContainer img = ui.Q<IMGUIContainer>("SpellBoxImage");

            btn.style.backgroundImage = chara.CurrentCharaSpell.Icon;

            img.style.backgroundImage = SpellImageUsed;

            btn.name = "BSpell" + index;

            //Ajouter le bouton dans une liste de bouton pour redéfinir dans le OnSpellCliqued
            m_buttons.Add(btn);

            //Chercher le boutton et lui rajouter une clickable la fonction clickable
            //            btn.clickable.clicked += OnSpellCliqued;
            btn.clickable.clicked += () =>
            {
                CheckButtonCliqued(btn);
            };

            //ajouter le sort du personnage actuel (le premier visual crée correspond à son premier sort)
            spellList.Add(chara.CurrentCharaSpell);

            GroupBox boxToQuery = ui.Q<GroupBox>("BoxDescription");

            Label spellDescription = boxToQuery.Q<Label>("Description");

            spellDescription.name = "LDescription" + index;

            spellDescription.text = chara.CurrentCharaSpell.Description + chara.CurrentCharaSpell.Damage;

            Label spellCost = ui.Q<Label>("SpellCost");
            spellCost.text = chara.CurrentCharaSpell.Cost.ToString();

            //ajouter au container la template
            container.Add(ui);

            index++;
            isUiCreated = true;
        }

        //Désactiver les bouttons qu'on peut pas utiliser du au manque d'énergie
        ManaManager.instance.CheckEnergy(spellList, m_buttons);
        ManaManager.instance.UpdateManaUi();

    }
    public void CheckButtonCliqued(Button btn)
    {
        //Empêcher de le faire comme ça car on est dans un foreach
        if(btn == m_buttons[0])
        {
            //Si le bouton 0 est cliqué alors l'actuel sélectionné est celui du character 0 et on va dans combat manager pour vérifier la/les cibles
            //à l'aide des booléens dans le SO Si on doit toucher que un seul ennemis, plusieurs, un personnage de son équipe, nous-même, toute notre équipe
            //Depuis ces booléens cela renvoie des fonctions qui demandent en paramètres des target
            m_spellUsed = btn;
            Debug.Log(m_spellUsed.name);
            currentSpellCliqued = spellList[0];
            CombatManager.instance.CheckSpellTarget(currentSpellCliqued);
        }
        else if (btn == m_buttons[1])
        {
            m_spellUsed = btn;
            currentSpellCliqued = spellList[1];
            CombatManager.instance.CheckSpellTarget(currentSpellCliqued);
        }
        else if (btn == m_buttons[2])
        {
            m_spellUsed = btn;
            currentSpellCliqued = spellList[2];
            CombatManager.instance.CheckSpellTarget(currentSpellCliqued);
        }
        else if (btn == m_buttons[3])
        {
            m_spellUsed = btn;
            currentSpellCliqued = spellList[3];
            CombatManager.instance.CheckSpellTarget(currentSpellCliqued);
        }
    }

    public void SpellUsed()
    {

        ManaManager.instance.UsedEnergy(currentSpellCliqued);
        ManaManager.instance.CheckEnergy(spellList, m_buttons);
        ManaManager.instance.UpdateManaUi();

        //Si le personnage rest spellused == null donc on return
        if (m_spellUsed == null) { Debug.Log(m_spellUsed); }

        spellButtons.Add(m_spellUsed);

        //Vérifier chaque bouton dans la list si ils y sont désactiver les boutons
        foreach (Button btn in spellButtons)
        {
            //faire aussi changer l'image du sort en un truc gris genre il a été utilisé
            btn.visible = false;
        }

        CombatManager.instance.canSelectMob = false;
    }

    //QUAND ON REST LA FONCTION SPELLUSED EST APPELE DONC YA UNE ERREUR
    private void OnRestButtonCliqued()
    {
        ManaManager.instance.NextTurn();

        m_spellUsed = null;

        spellButtons = new List<Button>();

        foreach(Button btn in m_buttons)
        {
            btn.visible = true;
        }

        CombatManager.instance.delTurn(false);
    }

    #region Ui par rapport aux tours du joueur
    public void NotPlayerTurn()
    {
        var rootElement = m_uiDocCombat.rootVisualElement;
        rootElement.style.display = DisplayStyle.None;

        ManaManager.instance.CheckEnergy(spellList, m_buttons);
    }

    public void PlayerTurn()
    {
        var rootElement = m_uiDocCombat.rootVisualElement;
        rootElement.style.display = DisplayStyle.Flex;

        ManaManager.instance.CheckEnergy(spellList, m_buttons);
    }
    #endregion
}
