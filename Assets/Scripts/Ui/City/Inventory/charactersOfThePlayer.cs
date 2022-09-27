using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;

public class charactersOfThePlayer : MonoBehaviour, IDataPersistence
{

    public static charactersOfThePlayer instance { get; private set; }

    [SerializeField] private UIDocument m_InventoryDoc;
    [SerializeField] private UIDocument m_MainMenuDoc;
    [SerializeField] private GameObject m_activateGameobject;

    //Variables de données
    [HideInInspector] public List<Character> m_totalCharacters;
    private List<Character> m_characterTeam;
    private ListView listView;
    private Label life;
    private Label damage;
    private Label defense;
    private Label rarity;
    private Button backButton;

    private List<Character> chara = new List<Character>();
    private List<string> items;
    private List<string> itemsSpell;

    private int selectedCharacter;

    private Character charaSelected;

    private VisualElement labelElement;

    private Action<VisualElement, int> bindItem;
    private Action<VisualElement, int> bindItemSpells;

    //Pour changer le sort du personnage
    private Button m_BSCharaSpell;
    private ListView m_viewSpellList;
    [SerializeField] private VisualTreeAsset m_spellListViewTemplate;
    private int switchButtonCount = 0;
    private VisualElement parentToClone;
    public List<Spell> m_spellListOwned;

    public void LoadData(GameData data)
    {
        this.m_totalCharacters = data.m_playerCharactersOwnedData;
        this.m_characterTeam = data.m_playerTeam;
    }

    public void SaveData(GameData data)
    {
        data.m_playerTeam = this.m_characterTeam;
    }
    private void Awake()
    {
        instance = this;
    }

    //Dans le start quand les données sont initialisées
    private void Start()
    {

        var container = m_InventoryDoc.rootVisualElement;
        m_spellListOwned = SpellsManager.instance.spells;

        UpdatePlayerTeam();
        SetupPlayerTeam();
        #region bouton
        backButton = container.Q<Button>("B_BackButton");
        backButton.clickable.clicked += OnBackCliqued;
        #endregion

        m_BSCharaSpell = container.Q<Button>("BChangeSpell");
        m_BSCharaSpell.clickable.clicked += SpellPannel;

        m_viewSpellList = container.Q<ListView>("LVSpells");
        DisableListView(m_viewSpellList);
    }

    public void UpdatePlayerTeam()
    {
        var container = m_InventoryDoc.rootVisualElement;

        #region Data personnages
        // Create some list of data, here simply numbers in interval [1, 1000]
        items = new List<string>(m_totalCharacters.Count);

        foreach (Character chara in m_totalCharacters)
        {
            items.Add(chara.CharactersName);
        }

        // The "makeItem" function will be called as needed
        // when the ListView needs more items to render
        Func<VisualElement> makeItem = () => new Label();

        // As the user scrolls through the list, the ListView object
        // will recycle elements created by the "makeItem"
        // and invoke the "bindItem" callback to associate
        // the element with the matching data item (specified as an index in the list)
        bindItem = (labelElement, i) => (labelElement as Label).text = items[i];

        listView = container.Q<ListView>("ListView");
        listView.makeItem = makeItem;
        listView.bindItem = bindItem;
        listView.itemsSource = items;

        //Le joueur ne peut sélection que un seul personnage pour avoir ses données
        listView.selectionType = SelectionType.Single;

        //Créations de fonctions lorsque le callback items choisis change
        listView.SetSelection(0);

        SetBasicSelection();

        listView.onSelectionChange += ListView_onSelectionChange;
        listView.itemIndexChanged += ListView_itemIndexChanged;
        ActivateListView(listView);
        #endregion


        m_BSCharaSpell = container.Q<Button>("BChangeSpell");
        m_BSCharaSpell.text = m_totalCharacters[0].CurrentCharaSpell.Name;
    }

    private void ListView_itemIndexChanged(int oldValue, int newValue)
    {
        m_totalCharacters[selectedCharacter] = charaSelected;

        m_totalCharacters.RemoveAt(oldValue);
        m_totalCharacters.Insert(newValue, charaSelected);
        listView.Rebuild();
        SetupPlayerTeam();
    }

    private void OnBackCliqued()
    {
        var thisDoc = m_InventoryDoc.rootVisualElement;
        thisDoc.style.display = DisplayStyle.None;
        var menuDoc = m_MainMenuDoc.rootVisualElement;
        menuDoc.style.display = DisplayStyle.Flex;
    }

    //Fonction qui permet de donner l'équipe du joueur
    public void SetupPlayerTeam()
    {
        //Les 4 premiers personnages constituent l'équipe du joueur
        m_characterTeam = m_totalCharacters.GetRange(0, 4);

        //On sauvegarde pour sauvegarder l'équipe actuelle du joueur
        DataPersistenceManager.instance.SaveGame();
    }
    //Set au départ
    public void SetBasicSelection()
    {
        var container = m_InventoryDoc.rootVisualElement;

        life = container.Q<Label>("Life");
        damage = container.Q<Label>("Damage");
        defense = container.Q<Label>("Defense");
        rarity = container.Q<Label>("Rarity");

        //Pour chaques stats de chaques personnages dans l'index de la liste on affiche la valeur
        //de ce qui est actuellement sélectionné
        for (int i = listView.selectedIndex; i < m_totalCharacters.Count; i++)
        {
            life.text = "Vie: " + m_totalCharacters[listView.selectedIndex].CharactersLife.ToString();
            damage.text = "Dégâts: " + m_totalCharacters[listView.selectedIndex].CharactersDamage.ToString();
            defense.text = "Défense: " + m_totalCharacters[listView.selectedIndex].CharactersDefense.ToString();
            rarity.text = "Rareté: " + m_totalCharacters[listView.selectedIndex].CharactersRarity.ToString();
        }
    }
    private void ListView_onSelectionChange(IEnumerable<object> obj)
    {
        var container = m_InventoryDoc.rootVisualElement;

        life = container.Q<Label>("Life");
        damage = container.Q<Label>("Damage");
        defense = container.Q<Label>("Defense");
        rarity = container.Q<Label>("Rarity");

        selectedCharacter = listView.selectedIndex;

        m_BSCharaSpell = container.Q<Button>("BChangeSpell");
        charaSelected = m_totalCharacters[selectedCharacter];

        for (int i = listView.selectedIndex; i < m_totalCharacters.Count; i++)
        {
            life.text = "Vie: " + m_totalCharacters[listView.selectedIndex].CharactersLife.ToString();
            damage.text = "Dégâts: " + m_totalCharacters[listView.selectedIndex].CharactersDamage.ToString();
            defense.text = "Défense: " + m_totalCharacters[listView.selectedIndex].CharactersDefense.ToString();
            rarity.text = "Rareté: " + m_totalCharacters[listView.selectedIndex].CharactersRarity.ToString();

            m_BSCharaSpell.text = charaSelected.CurrentCharaSpell.Name;
        }

    }
 
    public void DisableButton(Button buttonToDisable)
    {
        buttonToDisable.SetEnabled(false);
    }
    public void SpellPannel()
    {
        switchButtonCount++;
        var container = m_InventoryDoc.rootVisualElement;
        m_viewSpellList = container.Q<ListView>("LVSpells");
        m_BSCharaSpell = container.Q<Button>("BChangeSpell");
        listView = container.Q<ListView>("ListView");

        if (switchButtonCount == 1)
        {
            m_viewSpellList.style.display = DisplayStyle.Flex;

            //DisableButton(m_BSCharaSpell);
            DisableListView(listView);
            ActivateListView(m_viewSpellList);

            itemsSpell = new List<string>(m_spellListOwned.Count);

            Func<VisualElement> makeItem = () => new Label();

            bindItemSpells = (labelElement, i) => (labelElement as Label).text = itemsSpell[i];

            m_viewSpellList.makeItem = makeItem;
            m_viewSpellList.bindItem = bindItemSpells;
            m_viewSpellList.itemsSource = itemsSpell;

            foreach (Spell spells in m_spellListOwned)
            {
                //VisualElement visualElement = m_spellListViewTemplate.CloneTree();
                itemsSpell.Add(spells.Name);
                //parentToContain.Add(items);
            }

            //Le joueur ne peut sélection que un seul personnage pour avoir ses données
            m_viewSpellList.selectionType = SelectionType.Single;

            //m_ActiveChangeSpellPannel.style.display = DisplayStyle.None;

            //Créations de fonctions lorsque le callback items choisis change
            m_viewSpellList.SetSelection(0);

            m_viewSpellList.onSelectionChange += ViewSpellOnSelectionChange;
        }
        else
        {
            switchButtonCount = 0;
            ActivateListView(listView);
            DisableListView(m_viewSpellList);
        }
      

    }

    public void ViewSpellOnSelectionChange(IEnumerable<object> obj)
    {
        VisualElement container = m_InventoryDoc.rootVisualElement;

        m_BSCharaSpell = container.Q<Button>("BChangeSpell");
        IMGUIContainer spellTexture = container.Q<IMGUIContainer>("SpellImage");

        charaSelected = m_totalCharacters[selectedCharacter];

        for (int i = m_viewSpellList.selectedIndex; i < m_spellListOwned.Count; i++)
        {
            charaSelected.CurrentCharaSpell = m_spellListOwned[m_viewSpellList.selectedIndex];
            //spellTexture.style.
            m_BSCharaSpell.text = "Current Spell: " + charaSelected.CurrentCharaSpell.Name;
        }
    }


    public void DisableListView(ListView listToDisable)
    {
        listToDisable.style.display = DisplayStyle.None;
    }
    public void DisableListViewObject(ListView listToDisable)
    {
        listToDisable.SetEnabled(false);
    }
    public void ActivateListView(ListView listToEnable)
    {
        listToEnable.style.display = DisplayStyle.Flex;
    }
    public void ActivateListViewObject(ListView listToEnable)
    {
        listToEnable.SetEnabled(true);
    }
    public void ActivateButton(Button buttonToEnable)
    {
        buttonToEnable.SetEnabled(true);
    }

}
