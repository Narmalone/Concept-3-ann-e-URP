using System;
using System.Collections;
using System.Collections.Generic;
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

    private int selectedCharacter;

    private Character charaSelected;

    private VisualElement labelElement;

    private Action<VisualElement, int> bindItem;

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

        UpdatePlayerTeam();
        SetupPlayerTeam();
        #region bouton
        backButton = container.Q<Button>("B_BackButton");
        backButton.clickable.clicked += OnBackCliqued;
        #endregion
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

        listView = container.Q<ListView>();
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
        #endregion
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

        charaSelected = m_totalCharacters[selectedCharacter];

        for (int i = listView.selectedIndex; i < m_totalCharacters.Count; i++)
        {
            life.text = "Vie: " + m_totalCharacters[listView.selectedIndex].CharactersLife.ToString();
            damage.text = "Dégâts: " + m_totalCharacters[listView.selectedIndex].CharactersDamage.ToString();
            defense.text = "Défense: " + m_totalCharacters[listView.selectedIndex].CharactersDefense.ToString();
            rarity.text = "Rareté: " + m_totalCharacters[listView.selectedIndex].CharactersRarity.ToString();
        }
      
    }
}
