using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
public class SaveSlots : MonoBehaviour
{

    [Header("Ui Document")]
    [SerializeField] private UIDocument m_UiDocument;

    [SerializeField] private SaveSlotsMenu m_saveSlotMenu;

    [Header("Profile")]
    [SerializeField] private string profileId = "";

    [Header("Content")]
    private GroupBox m_hasDataContentBox;
    private GroupBox m_noDataContentBox;

    //[SerializeField] private GameObject noDataContent;
    //[SerializeField] private GameObject hasDataContent;

    //[SerializeField] private TextMeshProUGUI titlePlayer;
    private Label m_titlePlayer;

    //[SerializeField] private TextMeshProUGUI playersMoney;
    private Label m_playersMoney;

    private Button m_saveSlotButton;

    private void Awake()
    {
        //Attributions des différentes variables et Querrying
        var rootElement = m_UiDocument.rootVisualElement;

        m_noDataContentBox = rootElement.Q<GroupBox>("NoDataContent");
        m_hasDataContentBox = rootElement.Q<GroupBox>("HasDataContent");

        m_titlePlayer = rootElement.Q<Label>("PlayerTitle");
        m_playersMoney = rootElement.Q<Label>("PlayerMoney");

        m_saveSlotButton = rootElement.Q<Button>("SaveSlot");

        //Trouver un moyen de souscrire à save slot menu
    }

    //Mettre les informations dans la save slot
    public void SetData(GameData data)
    {
        if(data == null)
        {
            m_hasDataContentBox.visible = false;
            m_noDataContentBox.visible = true;
            //noDataContent.SetActive(true);
            //hasDataContent.SetActive(false);
        }
        else
        {
            m_hasDataContentBox.visible = true;
            m_noDataContentBox.visible = false;

            //noDataContent.SetActive(false);
            //hasDataContent.SetActive(true);

            m_titlePlayer.text = data.m_currentPlayersTitle.ToString();
            m_playersMoney.text = "Gold: " + data.m_playerMoney.ToString();

        }
    }

    //fonction pour récupérer cette slot
    public string GetProfileID()
    {
        return this.profileId;
    }

    public void SetInteractible(bool interactable)
    {
        m_saveSlotButton.focusable = interactable;
    }
}
