using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class MainMenu : Menu
{
    [Header("Navigation du Menu")]
    [SerializeField] private SaveSlotsMenu m_savesSlotsMenu;

    [Header("Ui Document")]
    [SerializeField] private UIDocument m_UiDocument;

    [Header("Références")]
    private Button m_newGameButton;
    private Button m_continueGameButton;
    private Button m_loadGameButton;
    private Button m_quitButton;

    private void Awake()
    {
        var rootElement = m_UiDocument.rootVisualElement;

        //Querrying variables
        m_newGameButton = rootElement.Q<Button>("NewGameButton");
        m_continueGameButton = rootElement.Q<Button>("ContinueGameButton");
        m_loadGameButton = rootElement.Q<Button>("LoadGameButton");
        m_quitButton = rootElement.Q<Button>("QuitGameButton");

        //Subscriptions aux fonctions
        m_newGameButton.clickable.clicked += OnNewGameClicked;
        m_continueGameButton.clickable.clicked += OnContinueGameClicked;
        m_loadGameButton.clickable.clicked += OnLoadGameClicked;
        m_quitButton.clickable.clicked += OnQuitGameClicked;
    }
    private void Start()
    {
        if (!DataPersistenceManager.instance.HasGameData())
        {
            m_continueGameButton.SetEnabled(false);
            m_loadGameButton.SetEnabled(false);
        }
    }
    private void OnEnable()
    {

        var rootElement = m_UiDocument.rootVisualElement;

        //Querrying variables
        m_newGameButton = rootElement.Q<Button>("NewGameButton");
        m_continueGameButton = rootElement.Q<Button>("ContinueGameButton");
        m_loadGameButton = rootElement.Q<Button>("LoadGameButton");
        m_quitButton = rootElement.Q<Button>("QuitGameButton");

        //Subscriptions aux fonctions
        m_newGameButton.clickable.clicked += OnNewGameClicked;
        m_continueGameButton.clickable.clicked += OnContinueGameClicked;
        m_loadGameButton.clickable.clicked += OnLoadGameClicked;
        m_quitButton.clickable.clicked += OnQuitGameClicked;
    }
    private void OnDisable()
    {
        m_newGameButton.clickable.clicked -= OnNewGameClicked;
        m_continueGameButton.clickable.clicked -= OnContinueGameClicked;
        m_loadGameButton.clickable.clicked -= OnLoadGameClicked;
        m_quitButton.clickable.clicked -= OnQuitGameClicked;
    }

    #region Boutons

    //Fonction lorsque le joueur clique sur new game
    public void OnNewGameClicked()
    {
        m_savesSlotsMenu.ActivateMenu(false);
        this.DeactivateMenu();
    }

    public void OnLoadGameClicked()
    {
        m_savesSlotsMenu.ActivateMenu(true);
        this.DeactivateMenu();
    }

    //Fonction quand le joueur clique sur charger une partie
    public void OnContinueGameClicked()
    {
        DisableMenusButtons();

        //Sauvegarder la partie avant de charger une nouvelle scene
        DataPersistenceManager.instance.SaveGame();

        //Charger la prochaine scene qui va également charger le jeu parceque OnSceneLoaded() dans le script DataPersistenceManager
        SceneManager.LoadSceneAsync("City");


        //Charger la partie avec les données du jeu
        //DataPersistenceManager.instance.LoadGame();
    }

    //Fonction quitter le jeu
    public void OnQuitGameClicked()
    {
        Application.Quit();
    }

    //Désactiver le MainMenu
    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }

    //Activer le main menu
    public void ActivateMenu()
    {
        this.gameObject.SetActive(true);
    }

    private void DisableMenusButtons()
    {
        m_newGameButton.SetEnabled(true);
        m_continueGameButton.SetEnabled(true);
    }

    #endregion
}