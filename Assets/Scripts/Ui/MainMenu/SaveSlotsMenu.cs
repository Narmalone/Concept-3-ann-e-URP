using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SaveSlotsMenu : Menu
{

    [Header("Ui Document")]
    [SerializeField] private UIDocument m_UiDocument;

    [Header("Navigation du Menu")]
    [SerializeField] private MainMenu m_mainMenu;

    [Header("Bouttons du Menu")]
    private Button m_backButton;
    private SaveSlots[] m_saveSlots;

    private bool m_isLoadingGame = false;

    private void Awake()
    {
        var rootElement = m_UiDocument.rootVisualElement;
        
        m_saveSlots = this.GetComponents<SaveSlots>();
    }

    //Quand le joueur appuie sur le bouton Back
    public void OnBackClicked()
    {
        m_mainMenu.ActivateMenu();
        this.DeactivateMenu();
    }

    //Quand le menu de Save Slots est actif get toutes les infos
    public void ActivateMenu(bool isLoadingGame)
    {
        //Rendre le menu actif
        this.gameObject.SetActive(true);

        //Set le mode
        this.m_isLoadingGame = isLoadingGame;

        //charger tous les profiles qui existent
        Dictionary<string, GameData> profilesGamedata = DataPersistenceManager.instance.GetAllProfilesGameData();

        //fix si la première slot ne peut pas être sélectionnée par défaut
        //GameObject firstSelected = m_backButtonObject;

        //looper à traver chaques dans l'interface et set le contenu de manière approriée
        foreach (SaveSlots saveSlot in m_saveSlots)
        {
            GameData profileData = null;
            profilesGamedata.TryGetValue(saveSlot.GetProfileID(), out profileData);
            saveSlot.SetData(profileData);

            if(profileData == null && isLoadingGame)
            {
                saveSlot.SetInteractible(false);
            }
            else
            {
                saveSlot.SetInteractible(true);
                //if (firstSelected.Equals(m_backButtonObject))
                //{
                    //firstSelected = saveSlot.gameObject;
                //}
            }
        }

        //Set le premier boutton sélectionné
        //Button firstSelectedButton = firstSelected.GetComponent<Button>();
        //this.SetFirstSelected(firstSelectedButton);
    }

    //Quand le menu est désactivé
    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }

    public void OnSaveSlotClicked(SaveSlots saveSlot)
    {
        Debug.Log("on save slot cliqued");
        //Désactiver tous les boutons quand la slot sauvegardées est cliqué pour éviter double click etc...
        DisableMenusButtons();

        //Update l'ID tu profil sélectionné pour qu'il soit uilisé par le script DataPersistenceManager
        DataPersistenceManager.instance.ChangeSelectedProfileID(saveSlot.GetProfileID());

        if (!m_isLoadingGame)
        {
            //créer la partie qui est initialisée avec les données par défauts
            DataPersistenceManager.instance.NewGame();
        }
        //sauvegarder le jeu avant de charger la prochaine scene
        DataPersistenceManager.instance.SaveGame();

        //Charger la scene
        SceneManager.LoadSceneAsync("City");
    }

    private void DisableMenusButtons()
    {
        foreach(SaveSlots saveSlot in m_saveSlots)
        {
            saveSlot.SetInteractible(false);
        }
        m_backButton.focusable = false;
    }
}
