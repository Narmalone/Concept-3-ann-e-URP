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
    //Variables de donn�es
    public List<Character> m_playerTeam;
    private ListView listView;
    private Label life;
    private Label damage;
    private Label defense;
    private Label rarity;
    private Button backButton;
    public void LoadData(GameData data)
    {
        this.m_playerTeam = data.m_playerCharactersOwnedData;
    }

    public void SaveData(GameData data)
    {
        return;
    }
    private void Awake()
    {
        instance = this;
    }

    //Dans le start quand les donn�es sont initialis�es
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
        var items = new List<string>(m_playerTeam.Count);

        foreach (Character chara in m_playerTeam)
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
        Action<VisualElement, int> bindItem = (e, i) => (e as Label).text = items[i];

        listView = container.Q<ListView>();
        listView.makeItem = makeItem;
        listView.bindItem = bindItem;
        listView.itemsSource = items;

        //Le joueur ne peut s�lection que un seul personnage pour avoir ses donn�es
        listView.selectionType = SelectionType.Single;

        //Cr�ations de fonctions lorsque le callback items choisis change
        listView.SetSelection(0);
        SetBasicSelection();
        listView.onItemsChosen += ListView_onItemsChosen;
        listView.onSelectionChange += ListView_onSelectionChange;

        #endregion
    }
    private void OnBackCliqued()
    {
        var thisDoc = m_InventoryDoc.rootVisualElement;
        thisDoc.style.display = DisplayStyle.None;
        var menuDoc = m_MainMenuDoc.rootVisualElement;
        menuDoc.style.display = DisplayStyle.Flex;
    }

    //Set au d�part
    public void SetBasicSelection()
    {
        var container = m_InventoryDoc.rootVisualElement;

        life = container.Q<Label>("Life");
        damage = container.Q<Label>("Damage");
        defense = container.Q<Label>("Defense");
        rarity = container.Q<Label>("Rarity");


        //rework avec boucle for -> faire un count dans le player team �galement
        if (listView.selectedIndex == 0)
        {
            life.text = "Vie: " + m_playerTeam[0].CharactersLife.ToString();
            damage.text = "D�g�ts: " + m_playerTeam[0].CharactersDamage.ToString();
            defense.text = "D�fense: " + m_playerTeam[0].CharactersDefense.ToString();
            rarity.text = "Raret�: " + m_playerTeam[0].CharactersRarity.ToString();
        }

        else if (listView.selectedIndex == 1)
        {
            life.text = "Vie: " + m_playerTeam[1].CharactersLife.ToString();
            damage.text = "D�g�ts: " + m_playerTeam[1].CharactersDamage.ToString();
            defense.text = "D�fense: " + m_playerTeam[1].CharactersDefense.ToString();
            rarity.text = "Raret�: " + m_playerTeam[1].CharactersRarity.ToString();
        }  
        
        else if (listView.selectedIndex == 2)
        {
            life.text = "Vie: " + m_playerTeam[2].CharactersLife.ToString();
            damage.text = "D�g�ts: " + m_playerTeam[2].CharactersDamage.ToString();
            defense.text = "D�fense: " + m_playerTeam[2].CharactersDefense.ToString();
            rarity.text = "Raret�: " + m_playerTeam[2].CharactersRarity.ToString();
        } 
        
        else if (listView.selectedIndex == 3)
        {
            life.text = "Vie: " + m_playerTeam[3].CharactersLife.ToString();
            damage.text = "D�g�ts: " + m_playerTeam[3].CharactersDamage.ToString();
            defense.text = "D�fense: " + m_playerTeam[3].CharactersDefense.ToString();
            rarity.text = "Raret�: " + m_playerTeam[3].CharactersRarity.ToString();
        }
    }
    private void ListView_onItemsChosen(IEnumerable<object> obj)
    {
       
    }
    private void ListView_onSelectionChange(IEnumerable<object> obj)
    {

        //Peut �tre optimis� pour faire les changements automatiquement et pas � la mano comme un connard mais y'a pas le choix
        var container = m_InventoryDoc.rootVisualElement;

        life = container.Q<Label>("Life");
        damage = container.Q<Label>("Damage");
        defense = container.Q<Label>("Defense");
        rarity = container.Q<Label>("Rarity");

        if (listView.selectedIndex == 0)
        {
            life.text = "Vie: " + m_playerTeam[0].CharactersLife.ToString();
            damage.text = "D�g�ts: " + m_playerTeam[0].CharactersDamage.ToString();
            defense.text = "D�fense: " + m_playerTeam[0].CharactersDefense.ToString();
            rarity.text = "Raret�: " + m_playerTeam[0].CharactersRarity.ToString();
        }

        else if (listView.selectedIndex == 1)
        {
            life.text = "Vie: " + m_playerTeam[1].CharactersLife.ToString();
            damage.text = "D�g�ts: " + m_playerTeam[1].CharactersDamage.ToString();
            defense.text = "D�fense: " + m_playerTeam[1].CharactersDefense.ToString();
            rarity.text = "Raret�: " + m_playerTeam[1].CharactersRarity.ToString();
        }

        else if (listView.selectedIndex == 2)
        {
            life.text = "Vie: " + m_playerTeam[2].CharactersLife.ToString();
            damage.text = "D�g�ts: " + m_playerTeam[2].CharactersDamage.ToString();
            defense.text = "D�fense: " + m_playerTeam[2].CharactersDefense.ToString();
            rarity.text = "Raret�: " + m_playerTeam[2].CharactersRarity.ToString();
        }

        else if (listView.selectedIndex == 3)
        {
            life.text = "Vie: " + m_playerTeam[3].CharactersLife.ToString();
            damage.text = "D�g�ts: " + m_playerTeam[3].CharactersDamage.ToString();
            defense.text = "D�fense: " + m_playerTeam[3].CharactersDefense.ToString();
            rarity.text = "Raret�: " + m_playerTeam[3].CharactersRarity.ToString();
        }
    }
}
