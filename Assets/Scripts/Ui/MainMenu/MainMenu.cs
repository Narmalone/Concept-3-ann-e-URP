using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : Menu
{
    [Header("Navigation du Menu")]
    [SerializeField] private SaveSlotsMenu m_savesSlotsMenu;

    [Header("Références")]
    [SerializeField, Tooltip("Référence au boutton nouvelle partie")] private Button m_newGameButton;
    [SerializeField, Tooltip("Référence au boutton continuer une partie")] private Button m_continueGameButton;
    [SerializeField, Tooltip("Référence au bouton charger une partie")] private Button m_loadGameButton;


    private void Start()
    {
        if (!DataPersistenceManager.instance.HasGameData())
        {
            m_continueGameButton.interactable = false;
            m_loadGameButton.interactable = false;
        }
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
        m_newGameButton.interactable = false;
        m_continueGameButton.interactable = false;
    }

    #endregion
}
