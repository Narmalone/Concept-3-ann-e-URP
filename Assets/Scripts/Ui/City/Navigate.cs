using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Navigate : MonoBehaviour
{

    [Header("UiDocument")]
    [SerializeField] private UIDocument m_menuDoc;
    [SerializeField] private UIDocument m_inventoryDoc;
    [SerializeField] private UIDocument m_expeditionDoc;
    [SerializeField] private UIDocument m_SlaveMerchantDoc;

    [Header("Button")]
    [SerializeField, Tooltip("Référence au bouton servant à faire apparaître/disparaître l'interface de navigation")] private Button m_navigateButton;

    private Button m_backButton;
    private Button m_expeditionPannel;
    private Button m_InventoryPannel;
    private Button m_slaveMerchantPannel;

    private void OnDisable()
    {
        m_backButton.clickable.clicked -= OnBackButtonClicked;
        m_expeditionPannel.clickable.clicked -= OnExpeditionCliqued;
        m_InventoryPannel.clickable.clicked -= OnInventoryCliqued;
        m_slaveMerchantPannel.clickable.clicked -= OnSlaveMerchantCliqued;
    }

    private void OnEnable()
    {
        var rootElement = m_menuDoc.rootVisualElement;

        m_backButton = rootElement.Q<Button>("B_MainMenu");
        m_backButton.clickable.clicked += OnBackButtonClicked;

        m_expeditionPannel = rootElement.Q<Button>("B_ExpeditionPannel");
        m_expeditionPannel.clickable.clicked += OnExpeditionCliqued;

        m_InventoryPannel = rootElement.Q<Button>("B_InventoryPannel");
        m_InventoryPannel.clickable.clicked += OnInventoryCliqued;

        m_slaveMerchantPannel = rootElement.Q<Button>("B_SlaveMerchandPannel");
        m_slaveMerchantPannel.clickable.clicked += OnSlaveMerchantCliqued;

        var rootExpedition = m_expeditionDoc.rootVisualElement;
        rootExpedition.style.display = DisplayStyle.None;

        var rootInventory = m_inventoryDoc.rootVisualElement;
        rootInventory.style.display = DisplayStyle.None;

        var rootMerchant = m_SlaveMerchantDoc.rootVisualElement;
        rootMerchant.style.display = DisplayStyle.None;
    }

    private void OnExpeditionCliqued()
    {
        var rootMenu = m_menuDoc.rootVisualElement;
        rootMenu.style.display = DisplayStyle.None;

        var rootExpedition = m_expeditionDoc.rootVisualElement;
        rootExpedition.style.display = DisplayStyle.Flex;
    }

    private void OnInventoryCliqued()
    {
        var rootMenu = m_menuDoc.rootVisualElement;
        rootMenu.style.display = DisplayStyle.None;

        var rootInventory = m_inventoryDoc.rootVisualElement;
        rootInventory.style.display = DisplayStyle.Flex;
    }

    private void OnSlaveMerchantCliqued()
    {
        var rootMenu = m_menuDoc.rootVisualElement;
        rootMenu.style.display = DisplayStyle.None;

        var rootSlaveMerchant = m_SlaveMerchantDoc.rootVisualElement;
        rootSlaveMerchant.style.display = DisplayStyle.Flex;
    }

    private void OnBackButtonClicked()
    {
        //sauvegarder le jeux
        DataPersistenceManager.instance.SaveGame();

        //charger le Main Menu
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
