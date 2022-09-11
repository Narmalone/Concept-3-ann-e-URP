using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Navigate : MonoBehaviour
{

    [Header("UiDocument")]
    [SerializeField] private UIDocument m_UiDocument;

    [Header("Button")]
    [SerializeField, Tooltip("Référence au bouton servant à faire apparaître/disparaître l'interface de navigation")] private Button m_navigateButton;

    [Header("ActivePannels")]
    [SerializeField] private GameObject m_TestPannelObject;
    [SerializeField] private GameObject m_expeditionPannelObject;

    private Button m_backButton;
    private Button m_expeditionPannel;
    private Button m_leaveExpeditionPannel;
    private void Awake()
    {
        var rootElement = m_UiDocument.rootVisualElement;

        m_backButton = rootElement.Q<Button>("B_MainMenu");
        m_backButton.clickable.clicked += OnBackButtonClicked;

        m_expeditionPannel = rootElement.Q<Button>("B_ExpeditionPannel");
        m_expeditionPannel.clickable.clicked += OnExpeditionCliqued;
    }

    private void OnExpeditionCliqued()
    {
        m_TestPannelObject.SetActive(false);
        m_expeditionPannelObject.SetActive(true);
    }

    private void OnLeaveExpeditionCliqued()
    {

    }

    private void OnBackButtonClicked()
    {
        Debug.Log("OnBackButtonCliqued");
        //sauvegarder le jeux
        DataPersistenceManager.instance.SaveGame();

        //charger le Main Menu
        SceneManager.LoadSceneAsync("MainMenu");
    }

    private void OnDestroy()
    {
        m_backButton.clickable.clicked -= OnBackButtonClicked;
    }



}
