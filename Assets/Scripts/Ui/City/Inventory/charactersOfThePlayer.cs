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
    public List<Character> m_totalCharacters;
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
    }

    public void SaveData(GameData data)
    {
        return;
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
        Debug.Log(charaSelected.CharactersName);

        listView.Rebuild();
    }
    private void OnBackCliqued()
    {
        var thisDoc = m_InventoryDoc.rootVisualElement;
        thisDoc.style.display = DisplayStyle.None;
        var menuDoc = m_MainMenuDoc.rootVisualElement;
        menuDoc.style.display = DisplayStyle.Flex;
    }

    //Set au départ
    public void SetBasicSelection()
    {
        var container = m_InventoryDoc.rootVisualElement;

        life = container.Q<Label>("Life");
        damage = container.Q<Label>("Damage");
        defense = container.Q<Label>("Defense");
        rarity = container.Q<Label>("Rarity");


        //rework avec boucle for -> faire un count dans le player team également
        if (listView.selectedIndex == 0)
        {
            life.text = "Vie: " + m_totalCharacters[0].CharactersLife.ToString();
            damage.text = "Dégâts: " + m_totalCharacters[0].CharactersDamage.ToString();
            defense.text = "Défense: " + m_totalCharacters[0].CharactersDefense.ToString();
            rarity.text = "Rareté: " + m_totalCharacters[0].CharactersRarity.ToString();
        }

        else if (listView.selectedIndex == 1)
        {
            life.text = "Vie: " + m_totalCharacters[1].CharactersLife.ToString();
            damage.text = "Dégâts: " + m_totalCharacters[1].CharactersDamage.ToString();
            defense.text = "Défense: " + m_totalCharacters[1].CharactersDefense.ToString();
            rarity.text = "Rareté: " + m_totalCharacters[1].CharactersRarity.ToString();
        }  
        
        else if (listView.selectedIndex == 2)
        {
            life.text = "Vie: " + m_totalCharacters[2].CharactersLife.ToString();
            damage.text = "Dégâts: " + m_totalCharacters[2].CharactersDamage.ToString();
            defense.text = "Défense: " + m_totalCharacters[2].CharactersDefense.ToString();
            rarity.text = "Rareté: " + m_totalCharacters[2].CharactersRarity.ToString();
        } 
        
        else if (listView.selectedIndex == 3)
        {
            life.text = "Vie: " + m_totalCharacters[3].CharactersLife.ToString();
            damage.text = "Dégâts: " + m_totalCharacters[3].CharactersDamage.ToString();
            defense.text = "Défense: " + m_totalCharacters[3].CharactersDefense.ToString();
            rarity.text = "Rareté: " + m_totalCharacters[3].CharactersRarity.ToString();
        }
    }
    private void ListView_onSelectionChange(IEnumerable<object> obj)
    {
        Debug.Log(obj);
        //Peut être optimisé pour faire les changements automatiquement et pas à la mano comme un connard mais y'a pas le choix
        var container = m_InventoryDoc.rootVisualElement;

        life = container.Q<Label>("Life");
        damage = container.Q<Label>("Damage");
        defense = container.Q<Label>("Defense");
        rarity = container.Q<Label>("Rarity");

        selectedCharacter = listView.selectedIndex;

        charaSelected = m_totalCharacters[selectedCharacter];

        if (selectedCharacter == 0)
        {
            life.text = "Vie: " + charaSelected.CharactersName.ToString();
            damage.text = "Vie: " + m_totalCharacters[selectedCharacter].CharactersDamage.ToString();
            defense.text = "Défense: " + m_totalCharacters[selectedCharacter].CharactersDefense.ToString();
            rarity.text = "Rareté: " + m_totalCharacters[selectedCharacter].CharactersRarity.ToString();
        }

        else if (selectedCharacter == 1)
        {
            life.text = "Vie: " + m_totalCharacters[selectedCharacter].CharactersName.ToString();
            damage.text = "Vie: " + m_totalCharacters[selectedCharacter].CharactersDamage.ToString();
            defense.text = "Défense: " + m_totalCharacters[selectedCharacter].CharactersDefense.ToString();
            rarity.text = "Rareté: " + m_totalCharacters[selectedCharacter].CharactersRarity.ToString();
        }

        else if (selectedCharacter == 2)
        {
            life.text = "Vie: " + m_totalCharacters[selectedCharacter].CharactersName.ToString();
            damage.text = "Vie: " + m_totalCharacters[selectedCharacter].CharactersDamage.ToString();
            defense.text = "Défense: " + m_totalCharacters[selectedCharacter].CharactersDefense.ToString();
            rarity.text = "Rareté: " + m_totalCharacters[selectedCharacter].CharactersRarity.ToString();
        }

        else if (listView.selectedIndex == 3)
        {
            life.text = "Vie: " + m_totalCharacters[3].CharactersLife.ToString();
            damage.text = "Dégâts: " + m_totalCharacters[3].CharactersDamage.ToString();
            defense.text = "Défense: " + m_totalCharacters[3].CharactersDefense.ToString();
            rarity.text = "Rareté: " + m_totalCharacters[3].CharactersRarity.ToString();
        }
    }
}
